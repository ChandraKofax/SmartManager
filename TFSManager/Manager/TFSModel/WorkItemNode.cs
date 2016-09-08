using DataModel;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Collections.Generic;

namespace TFS.Model
{
    public class WorkItemNode
    {
        public Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem Item { get; set; }
        public ActivityType Activity { get; set; }
        public List<WorkItemNode> Children { get; set; }

        internal EffortDetails GetEffortDetailsForDuration(Duration duration)
        {
            double currentOriginalEstimate = Item.Fields[TFSLiterals.OriginalEstimate].Value.GetDoubleValue();
            double currentRemainingTime = Item.Fields[TFSLiterals.RemainingWork].Value.GetDoubleValue();
            double currentTimeSpent = Item.Fields[TFSLiterals.CompletedWork].Value.GetDoubleValue();
            double deviation = 0;
            double initialRemainingTime = 0;
            double initialTimeSpent = 0;
            double initialOriginalEstimate = 0;

            if (Item.CreatedDate < duration.From)
            {
                Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem itemAsOf = Item.Store.GetWorkItem(Item.Id, duration.From);
                initialOriginalEstimate = itemAsOf.Fields[TFSLiterals.OriginalEstimate].Value.GetDoubleValue();
                initialRemainingTime = itemAsOf.Fields[TFSLiterals.RemainingWork].Value.GetDoubleValue();
                initialTimeSpent = itemAsOf.Fields[TFSLiterals.CompletedWork].Value.GetDoubleValue();
            }
            else
            {
                initialOriginalEstimate = Item.Fields[TFSLiterals.OriginalEstimate].Value.GetDoubleValue();
                initialRemainingTime = initialOriginalEstimate;
                deviation = initialOriginalEstimate;
                initialTimeSpent = 0;
            }

            return new EffortDetails
            {
                Burn = (initialRemainingTime - currentRemainingTime) + (currentOriginalEstimate - initialOriginalEstimate),
                TimeSpent = currentTimeSpent - initialTimeSpent,
                Deviation = deviation
            };
        }

        internal bool HasStatusChangedBetweenDuration(Duration duration)
        {
            string currentStatus = Item.State;
            string stateAtDuration = string.Empty;
            if (Item.CreatedDate < duration.From)
            {
                Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem itemAsOf = Item.Store.GetWorkItem(Item.Id, duration.From);
                stateAtDuration = itemAsOf.State;
            }
            else
            {
                stateAtDuration = StoryStatusType.New.ToString();
            }

            return currentStatus.ToLower().CompareTo(stateAtDuration.ToLower()) != 0;
        }
    }
}
