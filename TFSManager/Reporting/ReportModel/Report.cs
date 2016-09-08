using DataModel;
using System.Collections.Generic;
using System.Linq;
using TFS.Model;

namespace TFS.Reporting
{
    public class Report
    {
        private List<Item> RawData { get; set; }
        private ViewType CurrentViewType { get; set; }
        public List<Item> AllItems { get; set; }

        private int storiesCount;
        public int StoriesCount { get { return storiesCount; } }

        private EffortReportingEntity burn;
        public EffortReportingEntity Burn { get { return burn; } }

        private EffortReportingEntity bugBurn;
        public EffortReportingEntity BugBurn { get { return bugBurn; } }
        private EffortReportingEntity storyBurn;
        public EffortReportingEntity StoryBurn { get { return storyBurn; } }

        private EffortReportingEntity deviation;
        public EffortReportingEntity Deviation { get { return deviation; } }
        private EffortReportingEntity progress;
        public EffortReportingEntity Progress { get { return progress; } }

        private double timeSpent;
        public double TimeSpent { get { return timeSpent; } }

        private double noOfBugsClosed;
        public double NumberOfBugsClosed { get { return noOfBugsClosed; } }

        private double noOfBugsResolved;
        public double NumberOfBugsResolved { get { return noOfBugsResolved; } }

        public Report()
        {
            this.RawData = new List<Item>();
            AllItems = new List<Item>();
            burn = new EffortReportingEntity();
            bugBurn = new EffortReportingEntity();
            deviation = new EffortReportingEntity();
            progress = new EffortReportingEntity();
            storyBurn = new EffortReportingEntity();
        }

        private void AnalyseData()
        {
            storiesCount = AllItems.Sum(i => i.GetNumberOfStoriesWithStatus(StoryStatusType.None));

            burn.Effort = AllItems.Sum(i => i.GetBurn(ItemBroadType.None, ActivityType.None).Sum(e => e.EffortInHr));
            burn.DevEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.None, ActivityType.Development).Sum(e => e.EffortInHr));
            burn.QAEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.None, ActivityType.Testing).Sum(e => e.EffortInHr));
            burn.TWEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.None, ActivityType.Documentation).Sum(e => e.EffortInHr));

            timeSpent = AllItems.Sum(i => i.GetTimeSpent().Sum(e => e.EffortInHr));

            bugBurn.Effort = AllItems.Sum(i => i.GetBurn(ItemBroadType.Bug, ActivityType.None).Sum(e => e.EffortInHr));
            bugBurn.DevEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.Bug, ActivityType.Development).Sum(e => e.EffortInHr));
            bugBurn.QAEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.Bug, ActivityType.Testing).Sum(e => e.EffortInHr));
            bugBurn.TWEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.Bug, ActivityType.Documentation).Sum(e => e.EffortInHr));

            storyBurn.Effort = AllItems.Sum(i => i.GetBurn(ItemBroadType.UserStory, ActivityType.None).Sum(e => e.EffortInHr));
            storyBurn.DevEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.UserStory, ActivityType.Development).Sum(e => e.EffortInHr));
            storyBurn.QAEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.UserStory, ActivityType.Testing).Sum(e => e.EffortInHr));
            storyBurn.TWEffort = AllItems.Sum(i => i.GetBurn(ItemBroadType.UserStory, ActivityType.Documentation).Sum(e => e.EffortInHr));

            deviation.Effort = AllItems.Sum(i => i.GetDeviation().Sum(e => e.EffortInHr));
            deviation.DevEffort = AllItems.Sum(i => i.GetDeviation().Where(c => c.Activity == ActivityType.Development).Sum(e => e.EffortInHr));
            deviation.QAEffort = AllItems.Sum(i => i.GetDeviation().Where(c => c.Activity == ActivityType.Testing).Sum(e => e.EffortInHr));
            deviation.TWEffort = AllItems.Sum(i => i.GetDeviation().Where(c => c.Activity == ActivityType.Documentation).Sum(e => e.EffortInHr));

            progress.Effort = burn.Effort - deviation.Effort;
            progress.DevEffort = burn.DevEffort - deviation.DevEffort;
            progress.QAEffort = burn.QAEffort - deviation.QAEffort;
            progress.TWEffort = burn.TWEffort - deviation.TWEffort;

            noOfBugsResolved = AllItems.Sum(i => i.GetNumberOfBugsWithStatus(BugStatusType.Resolved));
            noOfBugsClosed = AllItems.Sum(i => i.GetNumberOfBugsWithStatus(BugStatusType.Closed));
        }

        public void FinalizeReport()
        {
            AnalyseData();
            this.RawData = new List<Item>();
            this.AllItems.ForEach(i => RawData.Add(i.GetCopy(true)));
        }
        private void RestoreData()
        {
            this.AllItems = new List<Item>();
            this.RawData.ForEach(i => AllItems.Add(i.GetCopy(true)));
        }
        public void SetView(ViewType preferredView, List<Resource> resources)
        {
            if (CurrentViewType != preferredView)
            {
                this.RestoreData();
                this.AnalyseData();
                this.ArrangeItemsBasedOnView(preferredView, resources);
                this.CurrentViewType = preferredView;
            }
        }

        private void ArrangeItemsBasedOnView(ViewType preferredView, List<Resource> resources)
        {
            switch (preferredView)
            {
                case ViewType.Story:
                    break;
                case ViewType.Resource:
                    AllItems = ArrangeResourceMajor(resources);
                    break;
            }
        }

        private List<Item> ArrangeResourceMajor(List<Resource> resources)
        {
            List<Item> allItems = new List<Item>();
            foreach (Resource resource in resources)
            {
                Item resourceBranch = new Item { Title = resource.Name };
                foreach (Item sourceItem in AllItems)
                {
                    Item branch = sourceItem.GetBranchResourceWorkedOn(resource);
                    if (branch != null)
                    {
                        resourceBranch.Children.Add(branch);
                    }
                }
                if (resourceBranch.Children.Count > 0)
                {
                    allItems.Add(resourceBranch);
                }
            }

            return allItems;
        }
    }
}
