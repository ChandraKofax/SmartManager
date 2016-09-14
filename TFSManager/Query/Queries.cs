using DataModel;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFS.WiQl
{
    public class QueryStore
    {
        private static readonly string BurnDetailsQuery =
            "SELECT [System.ChangedBy], [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], [System.ChangedDate], " +
            "[System.CreatedDate], [Microsoft.VSTS.Scheduling.OriginalEstimate], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork] " +
            "FROM WorkItemLinks " +
            "WHERE " +
            "([Source].[System.TeamProject] = @project) And " +
            "([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') And " +
            "([Target].[System.WorkItemType] = 'Task')  And " +
            "{1}" +
            "([Target].[System.AssignedTo] IN ({0}) AND  " +
            "{2}" +
            "[Target].[System.ChangedDate] >= @startDate And " +
            "[Target].[System.CreatedDate] <= @endDate) " +
            "ORDER BY [System.Id] mode(Recursive,ReturnMatchingChildren)";

            //"SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
            //"FROM WorkItems " +
            //"WHERE [System.TeamProject] = @project  AND  [System.WorkItemType] = 'Bug'  AND  [System.State] = 'Open'  AND  [System.AssignedTo] = 'Chandrashekar Machipeddi' ORDER BY [System.Id]";

        private static readonly string BugFixCauseAndResolutionQuery =
            "SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], [Custom.Cause], [Custom.HowFixed] " +
            "FROM WorkItems " +
            "WHERE " +
            "[System.TeamProject] = @project  AND  [System.WorkItemType] = 'Bug'  " +
            "AND  [System.State] IN ('Resolved', 'Closed')  AND  " +
            "[System.AssignedTo] IN ({0}) AND" +
            "[Microsoft.VSTS.Common.ResolvedDate] >= @startDate AND " +
            "[Microsoft.VSTS.Common.ResolvedDate] <= @endDate " +
            "ORDER BY [System.Id]";

        private static readonly string TaskUpdateIssueQuery =
            "SELECT [System.Id], [System.Links.LinkType], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
            "FROM WorkItemLinks " +
            "WHERE " +
            "([Source].[System.TeamProject] = @project  AND  [Source].[System.State] IN ('Resolved', 'Closed')  AND  " +
            "[Source].[System.AssignedTo] IN ({0})  AND  " +
            "( [Source].[Microsoft.VSTS.Common.ResolvedDate] >= @startDate OR  [Source].[Microsoft.VSTS.Common.ClosedDate] >= @startDate ) AND " +
            "( [Source].[Microsoft.VSTS.Common.ResolvedDate] <= @endDate  OR  [Source].[Microsoft.VSTS.Common.ClosedDate] <= @endDate )) And " +
            "([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') And ([Target].[System.WorkItemType] = 'Task' AND  " +
            "[Target].[System.State] IN ('New', 'Active')) ORDER BY [System.Id] mode(MustContain)";

        private static readonly string BugsResolvedQuery =
            "SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
            "FROM WorkItems " +
            "WHERE " +
            "[System.TeamProject] = @project  AND  [System.WorkItemType] = 'Bug'  AND " +
            "[System.AssignedTo] IN ({0}) AND " +
            "[System.State] IN ('Resolved', 'Closed') AND " +
            "[Microsoft.VSTS.Common.ResolvedDate] >= @startDate  AND  " +
            "[Microsoft.VSTS.Common.ResolvedDate] <= @endDate " +
            "ORDER BY [System.Id]";

        private static readonly string BugsClosedQuery =
            "SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
            "FROM WorkItems " +
            "WHERE " +
            "[System.TeamProject] = @project  AND  [System.WorkItemType] = 'Bug'  AND " +
            "[System.State] = 'Closed' AND " +
            "[Microsoft.VSTS.Common.ClosedBy] IN ({0}) AND " +
            "[Microsoft.VSTS.Common.ClosedDate] >= @startDate  AND  " +
            "[Microsoft.VSTS.Common.ClosedDate] <= @endDate " +
            "ORDER BY [System.Id]";

        private static readonly string ChildTasksQuery =
           "SELECT [System.Id], [System.Links.LinkType], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
           "FROM WorkItemLinks " +
           "WHERE " +
           "[Source].[System.TeamProject] = @project AND  " +          
           "([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') And [Target].[System.WorkItemType] = 'Task' AND [Source].[System.Id] IN ({0}) " +
           "ORDER BY [System.Id] mode(MustContain)";

        private static readonly string ChildDetailsTasksQuery =
           "SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], " +
           "[Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.OriginalEstimate] " +
           "FROM WorkItems " +
           "WHERE " +
           "[Source].[System.TeamProject] = @project  AND  " +
           "[System.WorkItemType] = 'Task'  AND  [System.Id] IN (SELECT [System.Id]" +
           "FROM WorkItemLinks " +
           "WHERE " +
           "[Source].[System.TeamProject] = @project AND  " +          
           "([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') And [Target].[System.WorkItemType] = 'Task' AND [Source].[System.Id] IN ({0}) " +
           "ORDER BY [System.Id] mode(MustContain))" + "ORDER BY [System.Id] ";

        private static readonly string ListOfWorkItemsQuery =
           "SELECT [System.Id], [System.Links.LinkType], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State] " +
           "FROM WorkItemLinks " +
           "WHERE " +
           "[Source].[System.TeamProject] = @project AND  " +
           "[Target].[System.WorkItemType] = 'Task' AND [Target].[System.Id] IN ({0}) " +
           "ORDER BY [System.Id] mode(MustContain)";

        internal static TFSQuery GetBurnQueryOLD(BurnRetrievalOptions filter)
        {
            string burnQuery = string.Format(BurnDetailsQuery, BuildResourceString(filter), BuildRemovedTasksString(filter), BuildSprintString(filter));
            TFSQuery query = new TFSQuery { QueryString = burnQuery };
            query.Context.Add("project", "KTA");
            query.Context.Add("startDate", filter.DateRange.From.Date);
            query.Context.Add("endDate", filter.DateRange.To.Date.AddDays(1));
            return query;
        }

        internal static TFSQuery GetBurnQuery(BurnRetrievalOptions filter, string Query)
        {
            TFSQuery query = new TFSQuery { QueryString = Query };
            query.Context.Add("project", "KTA");
            query.Context.Add("startDate", filter.DateRange.From.Date);
            query.Context.Add("endDate", filter.DateRange.To.Date.AddDays(1));
            return query;
        }

        internal static TFSQuery GetAssignedWorkItemsQuery(string Query)
        {
            string burnQuery = Query;
            TFSQuery query = new TFSQuery { QueryString = burnQuery };
            query.Context.Add("project", "KTA");
            return query;
        }

        internal static TFSQuery GetChildTasksQuery(string parentId)
        {
            string childTasksQuery = string.Format(ChildTasksQuery, parentId);
            TFSQuery query = new TFSQuery { QueryString = childTasksQuery };
            query.Context.Add("project", "KTA");
            return query;
        }

        internal static TFSQuery GetListOfWorkItemsQuery(string[] workItemIds)
        {
            string childTasksQuery = string.Format(ListOfWorkItemsQuery, BuildWorkItemIdsString(workItemIds));
            TFSQuery query = new TFSQuery { QueryString = childTasksQuery };
            query.Context.Add("project", "KTA");
            return query;
        }       

        internal static TFSQuery GetBugResolutionDataQuery(BurnRetrievalOptions filter)
        {
            string bugResolutionQuery = string.Format(BugFixCauseAndResolutionQuery, BuildResourceString(filter));
            TFSQuery query = new TFSQuery { QueryString = bugResolutionQuery };
            query.Context.Add("project", "KTA");
            query.Context.Add("startDate", filter.DateRange.From + Constants.TFSServerTimeDifferenceConstant);
            query.Context.Add("endDate", filter.DateRange.To + Constants.TFSServerTimeDifferenceConstant);
            return query;
        }
        internal static TFSQuery GetTaskUpdatesDataQuery(BurnRetrievalOptions filter)
        {
            string taskUpdatesQuery = string.Format(TaskUpdateIssueQuery, BuildResourceString(filter));
            TFSQuery query = new TFSQuery { QueryString = taskUpdatesQuery };
            query.Context.Add("project", "KTA");
            query.Context.Add("startDate", filter.DateRange.From + Constants.TFSServerTimeDifferenceConstant);
            query.Context.Add("endDate", filter.DateRange.To + Constants.TFSServerTimeDifferenceConstant);
            return query;
        }
        internal static TFSQuery GetBugsResolvedQuery(BurnRetrievalOptions filter)
        {
            string bugsResolvedQuery = string.Format(BugsResolvedQuery, BuildResourceString(filter));
            TFSQuery query = new TFSQuery { QueryString = bugsResolvedQuery };
            query.Context.Add("project", "KTA");
            query.Context.Add("startDate", filter.DateRange.From + Constants.TFSServerTimeDifferenceConstant);
            query.Context.Add("endDate", filter.DateRange.To + Constants.TFSServerTimeDifferenceConstant);
            return query;
        }
        internal static TFSQuery GetBugsClosedQuery(BurnRetrievalOptions filter)
        {
            string bugsResolvedQuery = string.Format(BugsClosedQuery, BuildResourceString(filter));
            TFSQuery query = new TFSQuery { QueryString = bugsResolvedQuery };
            query.Context.Add("project", "KTA");
            query.Context.Add("startDate", filter.DateRange.From + Constants.TFSServerTimeDifferenceConstant);
            query.Context.Add("endDate", filter.DateRange.To + Constants.TFSServerTimeDifferenceConstant);
            return query;
        }

        private static string BuildSprintString(BurnRetrievalOptions filter)
        {
            if (filter.Sprint == null)
            {
                return string.Empty;
            }

            return string.Empty;
            //return string.Format("([Target].[System.IterationPath] Under '{0}')  And ", filter.Sprint.IterationPath);
        }

        private static string BuildRemovedTasksString(BurnRetrievalOptions filter)
        {
            return filter.IncludeRemovedTasks ? string.Empty : "([Target].[System.State] <> 'Removed')  And ";
        }

        private static string BuildResourceString(BurnRetrievalOptions filter)
        {
            string resourceString = string.Empty;
            List<Resource> resources;
            if (filter.Team.Name == "All Hyd")
            {
                (resources = new List<Resource>()).AddRange(Constants.AllResources);
            }
            else
            { resources = filter.Team.Members; }

            bool firstTime = true;
            string resourceFormatString = " '{0}'";
            foreach (Resource r in resources)
            {
                if (!firstTime)
                {
                    resourceString += "," + string.Format(resourceFormatString, r.Name);
                }
                else { resourceString = string.Format(resourceFormatString, r.Name); }
                firstTime = false;
            }
            return resourceString;
        }

        private static string BuildWorkItemIdsString(string[] workItemIds)
        {
            string listOfWorkItemIds = string.Empty;
            if (workItemIds != null && workItemIds.Length > 0)
            {
                bool firstTime = true;
                foreach (var item in workItemIds)
                {
                    if (!firstTime)
                    {
                        listOfWorkItemIds += "," + item;
                    }
                    else
                    {
                        listOfWorkItemIds = listOfWorkItemIds + item;
                    }

                    firstTime = false;
                }
            }

            return listOfWorkItemIds;
        }
    }
}
