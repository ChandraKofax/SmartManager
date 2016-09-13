using DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TFS.Common;
using TFS.Reporting;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFS.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Resource CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Resource LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<Item> Children { get; set; }
        public Item Parent { get; set; }
        public string State { get; set; }
        public string WorkItemUrl { get; set; }
        public string ParentWorkItemUrl { get; set; }
        public Resource AssignedTo { get; set; }
        protected WorkItemNode Node { get; set; }
        protected bool HasChanged { get; set; }
        public string ParentId { get; set; }

        public bool HasChildren
        {
            get { return Children.Count > 0; }
        }
        public virtual ItemType Type
        {
            get
            {
                return ItemType.Item;
            }
        }

        public Item()
        {
            Children = new List<Item>();
        }

        public virtual void Initialize(WorkItemNode workItem)
        {
            this.Node = workItem;
            this.Id = workItem.Item.Id;
            this.Title = workItem.Item.Title;
            this.CreatedBy = Constants.GetResource(workItem.Item.CreatedBy);
            this.CreatedDate = workItem.Item.CreatedDate;
            this.LastModifiedBy = Constants.GetResource(workItem.Item.ChangedBy);
            this.LastModifiedDate = workItem.Item.ChangedDate;
            this.State = workItem.Item.State;
            this.AssignedTo = Constants.GetResource((string)workItem.Item.Fields[TFSLiterals.AssignedTo].Value);
            EnumerateChildren();
        }

        internal virtual bool HasNotableChanges()
        {
            if (HasChanged) return true;
            return Children.Any(i => i.HasNotableChanges());
        }

        public virtual void CreateBurnReport(BurnRetrievalOptions filter)
        {
            if (filter.Team != null) { HasChanged = filter.Team.IsResourceMemberOfTeam(LastModifiedBy); }
            this.Children.ForEach(c =>
            {
                c.CreateBurnReport(filter);
            });
        }

        internal virtual List<Effort> GetBurn(ItemBroadType itemBroadType, ActivityType activityType)
        {
            List<Effort> burnList = new List<Effort>();
            Children.ForEach(i => burnList.AddRange(i.GetBurn(itemBroadType, activityType)));
            return burnList;
        }

        internal virtual List<Effort> GetDeviation()
        {
            List<Effort> diviationList = new List<Effort>();
            Children.ForEach(i => diviationList.AddRange(i.GetDeviation()));
            return diviationList;
        }

        internal virtual List<Effort> GetTimeSpent()
        {
            List<Effort> timeSpentList = new List<Effort>();
            Children.ForEach(i => timeSpentList.AddRange(i.GetTimeSpent()));
            return timeSpentList;
        }

        internal virtual bool HasTaskUpdateIssues()
        {
            if (State == TFSLiterals.StatusClosed)
            {
                return Children.Any(c =>
                {
                    if (c.State != TFSLiterals.StatusClosed || c.State != TFSLiterals.StatusRemoved)
                    {
                        return true;
                    }
                    return false;
                });
            }

            return Children.Any(c => c.HasTaskUpdateIssues());
        }
        internal virtual bool IsOwner(Resource resource)
        {
            return AssignedTo.Equals(resource);
        }

        internal virtual Item GetBranchResourceWorkedOn(Resource resource)
        {
            List<Item> tasks = new List<Item>();
            Item thisItem = this.GetCopy();
            Children.ForEach(i =>
                {
                    Item item = i.GetBranchResourceWorkedOn(resource);
                    if (item != null)
                    {
                        tasks.Add(item);
                    }
                }
                );
            if (tasks.Count > 0)
            {
                thisItem.Children.AddRange(tasks);
                return thisItem;
            }

            return null;
        }

        protected virtual ItemBroadType GetItemBroadType()
        {
            if (Type == ItemType.Bug)
            {
                return ItemBroadType.Bug;
            }
            else if (Type == ItemType.Feature || Type == ItemType.OutOfScope || Type == ItemType.TechnicalDebt || Type == ItemType.UserStory)
            {
                return ItemBroadType.UserStory;
            }
            else if (Parent != null)
            {
                return Parent.GetItemBroadType();
            }
            return ItemBroadType.None;
        }
        //protected virtual List<Item> GetBugsWithStatus(BugStatusType bugStatus)
        //{
        //    List<Item> bugs = new List<Item>();
        //    if (this.Type == ItemType.Bug && (bugStatus == BugStatusType.None || this.State.CompareTo(bugStatus.ToString()) == 0))
        //    { bugs.Add(this.GetCopy()); }
        //    foreach (var child in Children)
        //        bugs.AddRange(child.GetBugsWithStatus(bugStatus));
        //    return bugs;
        //}
        internal virtual int GetNumberOfBugsWithStatus(BugStatusType bugStatus)
        {
            int noOfBugs = 0;
            if (this.Type == ItemType.Bug && (bugStatus == BugStatusType.None || this.State.CompareTo(bugStatus.ToString()) == 0))
            { noOfBugs++; }
            foreach (var child in Children)
                noOfBugs += child.GetNumberOfBugsWithStatus(bugStatus);
            return noOfBugs;
        }
        //internal virtual List<Item> GetStoriesWithStatus(StoryStatusType storyStatusType)
        //{
        //    List<Item> stories = new List<Item>();
        //    if ((Type == ItemType.Feature || Type == ItemType.OutOfScope || Type == ItemType.TechnicalDebt || Type == ItemType.UserStory)
        //       && (storyStatusType == StoryStatusType.None || State.CompareTo(storyStatusType.ToString()) == 0))
        //    { stories.Add(this.GetCopy()); }
        //    foreach (var child in Children)
        //    { stories.AddRange(child.GetStoriesWithStatus(storyStatusType)); }
        //    return stories;
        //}
        internal virtual int GetNumberOfStoriesWithStatus(StoryStatusType storyStatusType)
        {
            int noOfStories = 0;
            if ((Type == ItemType.Feature || Type == ItemType.OutOfScope || Type == ItemType.TechnicalDebt || Type == ItemType.UserStory)
               && (storyStatusType == StoryStatusType.None || State.CompareTo(storyStatusType.ToString()) == 0))
            { noOfStories++; }
            foreach (var child in Children)
            { noOfStories += child.GetNumberOfStoriesWithStatus(storyStatusType); }
            return noOfStories;
        }

        internal virtual Item GetCopy(bool isDeepCopy = false)
        {
            Type t = GetType();
            PropertyInfo[] properties = t.GetProperties();

            Item newItem = (Item)Activator.CreateInstance(t);

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    if (pi.Name != "Children" || isDeepCopy)
                    {
                        pi.SetValue(newItem, pi.GetValue(this, null), null);
                    }
                }
            }

            return newItem;
        }

        internal IEnumerable<Item> GetFlatList()
        {
            if (this.Type != ItemType.Task)
                yield return this;
            foreach (var childItem in Children)
            {
                foreach (var child in childItem.GetFlatList())
                    yield return child;
            }
        }

        public virtual string GetBDTString()
        {
            string toString = ToString();
            List<Effort> burn = GetBurn(ItemBroadType.None, ActivityType.None);
            List<Effort> deviation = GetDeviation();
            List<Effort> timeSpent = GetTimeSpent();
            double totalBurn = burn.Sum(b => b.EffortInHr);
            double totalDeviation = deviation.Sum(d => d.EffortInHr);
            double totalTimeSpent = timeSpent.Sum(t => t.EffortInHr);
            int subStories = Children.Count(s => s.Type == ItemType.OutOfScope || s.Type == ItemType.TechnicalDebt || s.Type == ItemType.UserStory || s.Type == ItemType.Feature);
            toString += string.Format(" Burn:{0}, Deviation:{1}, Progress on plan:{2}, Time spent:{3} ",
                new object[] { 
                            Utilities.GetEffortString(totalBurn, 
                                            burn.Where(i => i.Activity == ActivityType.Development).ToList().Sum(e => e.EffortInHr),
                                            burn.Where(i => i.Activity == ActivityType.Testing).ToList().Sum(e => e.EffortInHr),
                                            burn.Where(i => i.Activity == ActivityType.Documentation).ToList().Sum(e => e.EffortInHr)),
                            Utilities.GetEffortString(totalDeviation, 
                                            deviation.Where(i => i.Activity == ActivityType.Development).ToList().Sum(e => e.EffortInHr),
                                            deviation.Where(i => i.Activity == ActivityType.Testing).ToList().Sum(e => e.EffortInHr),
                                            deviation.Where(i => i.Activity == ActivityType.Documentation).ToList().Sum(e => e.EffortInHr)),
                            totalBurn-totalDeviation,
                            Utilities.GetEffortString(totalTimeSpent, 
                                            timeSpent.Where(i => i.Activity == ActivityType.Development).ToList().Sum(e => e.EffortInHr),
                                            timeSpent.Where(i => i.Activity == ActivityType.Testing).ToList().Sum(e => e.EffortInHr),
                                            timeSpent.Where(i => i.Activity == ActivityType.Documentation).ToList().Sum(e => e.EffortInHr)),
                });
            return toString;
        }

        public void GetResourceBDTString(ResourceBurnDetails ResourceBurnDetails)
        {
            string toString = ToString();
            List<Effort> burn = GetBurn(ItemBroadType.None, ActivityType.None);
            List<Effort> deviation = GetDeviation();
            List<Effort> timeSpent = GetTimeSpent();
            double totalBurn = burn.Sum(b => b.EffortInHr);
            double totalDeviation = deviation.Sum(d => d.EffortInHr);
            double totalTimeSpent = timeSpent.Sum(t => t.EffortInHr);
            int subStories = Children.Count(s => s.Type == ItemType.OutOfScope || s.Type == ItemType.TechnicalDebt || s.Type == ItemType.UserStory || s.Type == ItemType.Feature);
            toString += string.Format(" Burn:{0}, Deviation:{1}, Progress on plan:{2}, Time spent:{3} ",
                new object[] { 
                            Utilities.GetEffortString(totalBurn, 
                                            burn.Where(i => i.Activity == ActivityType.Development).ToList().Sum(e => e.EffortInHr),
                                            burn.Where(i => i.Activity == ActivityType.Testing).ToList().Sum(e => e.EffortInHr),
                                            burn.Where(i => i.Activity == ActivityType.Documentation).ToList().Sum(e => e.EffortInHr)),
                            Utilities.GetEffortString(totalDeviation, 
                                            deviation.Where(i => i.Activity == ActivityType.Development).ToList().Sum(e => e.EffortInHr),
                                            deviation.Where(i => i.Activity == ActivityType.Testing).ToList().Sum(e => e.EffortInHr),
                                            deviation.Where(i => i.Activity == ActivityType.Documentation).ToList().Sum(e => e.EffortInHr)),
                            totalBurn-totalDeviation,
                            Utilities.GetEffortString(totalTimeSpent, 
                                            timeSpent.Where(i => i.Activity == ActivityType.Development).ToList().Sum(e => e.EffortInHr),
                                            timeSpent.Where(i => i.Activity == ActivityType.Testing).ToList().Sum(e => e.EffortInHr),
                                            timeSpent.Where(i => i.Activity == ActivityType.Documentation).ToList().Sum(e => e.EffortInHr)),
                });

            ResourceBurnDetails.ResourceName = Title;
            ResourceBurnDetails.Burn = totalBurn;
            ResourceBurnDetails.Deviation = totalDeviation;
            ResourceBurnDetails.ProgressOnPlan = totalBurn - totalDeviation;
            ResourceBurnDetails.TimeSpent = totalTimeSpent;
            ResourceBurnDetails.Summary = toString;
        }

        public override string ToString()
        {
            string toString = string.Empty;

            if (Id != 0)
            {
                toString = string.Format("{0} {1}: {2}.", Type.ToString(), Id, Title);
            }
            else
            {
                toString = Title;
            }

            return toString;
        }

        private void EnumerateChildren()
        {
            Node.Children.ForEach(c =>
            {
                var childItem = ReportFactory.CreateItem(c);
                childItem.Parent = this;
                this.Children.Add(childItem);
            });
        }

        internal virtual void GetParentId(WorkItemNode workItemNode)
        {
            this.ParentId = string.Empty;
            if (workItemNode != null && workItemNode.Item != null && workItemNode.Item.WorkItemLinks != null && workItemNode.Item.WorkItemLinks.Count > 0)
            {
                foreach (WorkItemLink workItemLink in workItemNode.Item.WorkItemLinks)
                {
                    if (workItemLink.LinkTypeEnd.Name == "Parent")
                    {
                        this.ParentId = workItemLink.TargetId.ToString();
                        break;
                    }
                }
            }
        }
    }
}
