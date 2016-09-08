using System.Linq;
using DataModel;

namespace TFS.Model
{
    public class Bug : Item
    {
        public Resource SQAResource { get; set; }

        public string Cause { get; set; }

        public string HowFixed { get; set; }

        public override ItemType Type
        {
            get
            {
                return ItemType.Bug;
            }
        }
        public override void Initialize(WorkItemNode workItem)
        {
            base.Initialize(workItem);
            Cause = workItem.Item.Fields[TFSLiterals.Cause].Value.ToString().Trim();
            HowFixed = (string)workItem.Item.Fields[TFSLiterals.HowFixed].Value.ToString().Trim();
            if(State == BugStatusType.Closed.ToString())
            {
                SQAResource = Constants.GetResource(workItem.Item.Fields[TFSLiterals.ClosedBy].Value.ToString().Trim());
            }
            else
            {
                SQAResource = Constants.GetResource(workItem.Item.Fields[TFSLiterals.SQAOwner].Value.ToString().Trim());
            }
        }
        public override string ToString()
        {
            string toString = base.ToString();

            return string.Format("{0} [{1}]", toString, this.State.ToString());
        }

        public override void CreateBurnReport(BurnRetrievalOptions filter)
        {
            base.CreateBurnReport(filter);
            HasChanged = HasChanged ? true : Node.HasStatusChangedBetweenDuration(filter.DateRange);
        }

        internal override bool IsOwner(Resource resource)
        {
            bool isOwner = SQAResource.Equals(resource);

            if (!isOwner)
            {
                return base.IsOwner(resource);
            }

            return isOwner;
        }
        
        public bool IsCauseResolutionDetailsMissing()
        {
            bool causeAndResolutionMissing = false;

            if(State == TFSLiterals.StatusResolved || State == TFSLiterals.StatusClosed)
            {
                if(string.IsNullOrEmpty(Cause) || string.IsNullOrEmpty(HowFixed))
                {
                    causeAndResolutionMissing = true;
                }
            }

            return causeAndResolutionMissing;
        }
        internal override bool HasTaskUpdateIssues()
        {
            bool hasActiveNonQATasks = false;
            if (State == TFSLiterals.StatusResolved)
            {
                hasActiveNonQATasks = Children.Any(c =>
                     {
                         if (c.Type == ItemType.Task)
                         {
                             Task task = c as Task;
                             if (task.Activity != ActivityType.Testing && ((task.State != TFSLiterals.StatusClosed && task.State != TFSLiterals.StatusRemoved) || task.RemainingWork > 0))
                             {
                                 return true;
                             }
                         }
                         return false;
                     });
            }
            if (hasActiveNonQATasks)
                return true;
            else
                return base.HasTaskUpdateIssues();
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
    }
}
