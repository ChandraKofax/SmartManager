using DataModel;
using System.Collections.Generic;
using TFS.Common;
using System.Linq;

namespace TFS.Model
{
    public class Task : Item
    {
        public ActivityType Activity { get; set; }
        public double Burn { get; set; }
        public double Deviation { get; set; }
        public double RemainingWork { get; set; }
        public double OriginalWork { get; set; }
        public double TimeSpent { get; set; }

        public override ItemType Type
        {
            get
            {
                return ItemType.Task;
            }
        }

        public override void Initialize(WorkItemNode workItem)
        {
            base.Initialize(workItem);
            this.Activity = Utilities.GetActityType(workItem.Item.Fields[TFSLiterals.Activity].Value.ToString());
            this.OriginalWork = workItem.Item.Fields[TFSLiterals.OriginalEstimate].Value.GetDoubleValue();
        }

        public override void CreateBurnReport(BurnRetrievalOptions filter)
        {
            base.CreateBurnReport(filter);
            EffortDetails effort = Node.GetEffortDetailsForDuration(filter.DateRange);
            Burn = effort.Burn;
            Deviation = effort.Deviation;
            TimeSpent = effort.TimeSpent;
            HasChanged = HasChanged ? true : effort.Burn != 0 || effort.Deviation != 0 || effort.TimeSpent != 0;
        }

        internal override List<Effort> GetBurn(ItemBroadType itemBroadType, ActivityType activityType)
        {
            List<Effort> burnList = new List<Effort>();
            Effort currentEffort = new Effort { Activity = Activity, EffortInHr = Burn };
            ItemBroadType parentOfThisItem = GetItemBroadType();
            if (itemBroadType == ItemBroadType.None || itemBroadType == parentOfThisItem)
            {
                if (activityType == ActivityType.None || activityType == Activity)
                {
                    burnList.Add(currentEffort);
                }
            }
            Children.ForEach(i => burnList.AddRange(i.GetBurn(itemBroadType, activityType)));
            return burnList;
        }

        internal override List<Effort> GetDeviation()
        {
            List<Effort> deviationList = new List<Effort>();
            deviationList.Add(new Effort { Activity = Activity, EffortInHr = Deviation });
            Children.ForEach(i => deviationList.AddRange(i.GetDeviation()));
            return deviationList;
        }

        internal override List<Effort> GetTimeSpent()
        {
            List<Effort> timeSpentList = new List<Effort>();
            timeSpentList.Add(new Effort { Activity = Activity, EffortInHr = TimeSpent });
            Children.ForEach(i => timeSpentList.AddRange(i.GetTimeSpent()));
            return timeSpentList;
        }

        internal override Item GetBranchResourceWorkedOn(Resource resource)
        {
            if (Children.Count > 0)
            {
                return base.GetBranchResourceWorkedOn(resource);
            }
            else
            {
                if (IsOwner(resource))
                {
                    return this.GetCopy();
                }
            }

            return null;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", base.ToString(), AssignedTo.Name);
        }
    }
}
