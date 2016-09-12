using System;
using System.Collections.Generic;
using Agility.Sdk.Model.Resources;
using DataModel;
using TFS;
using TFS.Reporting;
using TFS.WiQl;
using TotalAgility.Sdk;

namespace SmartManager
{
    public class SmartManagerSDK
    {
        /// <summary>
        /// Gets the assigned work items.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="fieldFilter">The field filter.</param>
        /// <returns>
        /// BurnReport Object
        /// </returns>
        public WorkItemCollection GetAssignedWorkItems(string sessionId, string tfsUrlPath, string projectName, FieldFilter fieldFilter)
        {
            Manager2 manager = new Manager2();

            BurnRetrievalOptions retrievalOptions = new BurnRetrievalOptions();
            string Query = "SELECT [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], [Custom.ReleasedIn], [System.IterationPath]  FROM WorkItems ";

            Query = Query + GetWorkItemTypeWhereClause(fieldFilter);
            Query = Query + GetStateWhereClause(fieldFilter);
            Query = Query + " AND [System.AssignedTo] IN ";
            Query = Query + GetAssignedToWhereClause(sessionId, fieldFilter, retrievalOptions);
            Query = Query + GetIterationPathWhereClause(projectName, fieldFilter);

            Query = Query + " ORDER BY [System.Id] ";

            return manager.GetAssignedWorkItems(tfsUrlPath, projectName, retrievalOptions, Query);
        }

        /// <summary>
        /// Gets the child tasks.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="ParentId">The parent identifier.</param>
        /// <returns>ChildItem Collection</returns>
        public ChildItemCollection GetChildTasks(string sessionId, string tfsUrlPath, string projectName, string ParentId)
        {
            Manager2 manager = new Manager2();
            return manager.GetChildTasksDetails(tfsUrlPath, projectName, ParentId);
        }

        /// <summary>
        /// Gets the burn details.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="fieldFilter">The field filter.</param>
        /// <returns>Burn Report</returns>
        public BurnReport GetBurnDetails(string sessionId, string tfsUrlPath, string projectName, FieldFilter fieldFilter)
        {
            Manager2 manager = new Manager2();
            BurnRetrievalOptions retrievalOptions = new BurnRetrievalOptions();

            string Query = "SELECT [System.ChangedBy], [System.Id], [System.WorkItemType], [System.Title], [System.AssignedTo], [System.State], [System.ChangedDate], " +
                            "[System.CreatedDate], [Microsoft.VSTS.Scheduling.OriginalEstimate], [Microsoft.VSTS.Scheduling.RemainingWork], [Microsoft.VSTS.Scheduling.CompletedWork] " +
                            "FROM WorkItemLinks " +
                            "WHERE " +
                            "([Source].[System.TeamProject] = @project) And " +
                            "([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward') And " +
                            "([Target].[System.WorkItemType] = 'Task')  And " +
                            "([Target].[System.AssignedTo] IN  ";
            Query = Query + GetAssignedToWhereClause(sessionId, fieldFilter, retrievalOptions) + " And ";

            Query = Query + "[Target].[System.ChangedDate] >= @startDate And " +
                            "[Target].[System.CreatedDate] <= @endDate) " +
                            "ORDER BY [System.Id] mode(Recursive,ReturnMatchingChildren)";
           
            retrievalOptions.DateRange = fieldFilter.DateRange;

            return manager.GetBurnDetails(tfsUrlPath, projectName, retrievalOptions, Query);
        }

        /// <summary>
        /// Gets the releases.
        /// </summary>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>ReleaseVersion Collection</returns>
        public ReleaseVersionCollection GetReleases(string tfsUrlPath, string projectName)
        {
            Manager2 manager = new Manager2();
            return manager.GetReleases(tfsUrlPath, projectName);
        }

        /// <summary>
        /// Gets the iterations.
        /// </summary>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="release">The release.</param>
        /// <returns>Iteration Collection</returns>
        public IterationCollection GetIterations(string tfsUrlPath, string projectName, string release)
        {
            Manager2 manager = new Manager2();
            return manager.GetIterations(tfsUrlPath, projectName, release);
        }

        #region Where Clause       

        /// <summary>
        /// Gets the work item type where clause.
        /// </summary>
        /// <param name="fieldFilter">The field filter.</param>
        /// <returns>Work item type Where Clause</returns>
        private static string GetWorkItemTypeWhereClause(FieldFilter fieldFilter)
        {
            string whereClause = " WHERE [System.TeamProject] = @project";
            ItemType workItemType = (ItemType)fieldFilter.WorkItemType;

            switch (workItemType)
            {
                case ItemType.Item:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Item'";
                    break;
                case ItemType.Task:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Task'";
                    break;
                case ItemType.Bug:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Bug'";
                    break;
                case ItemType.UserStory:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'User Story'";
                    break;
                case ItemType.OutOfScope:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Out-of-scope'";
                    break;
                case ItemType.TechnicalDebt:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Technical Debt'";
                    break;
                case ItemType.Feature:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Feature'";
                    break;
                case ItemType.EnhancementRequest:
                    whereClause = whereClause + " AND  [System.WorkItemType] = 'Enhancement Request'";
                    break;
                default:
                    break;
            }

            return whereClause;
        }

        /// <summary>
        /// Gets the state where clause.
        /// </summary>
        /// <param name="fieldFilter">The field filter.</param>
        /// <returns>State Where Clause</returns>
        private static string GetStateWhereClause(FieldFilter fieldFilter)
        {
            string stateClause = " ";
            StatusType status = (StatusType)fieldFilter.State;
            switch (status)
            {
                case StatusType.Any:
                    break;
                case StatusType.Active:
                    stateClause = stateClause + "AND  [System.State] = 'Active'";
                    break;
                case StatusType.Closed:
                    stateClause = stateClause + "AND  [System.State] = 'Closed'";
                    break;
                case StatusType.Impeded:
                    stateClause = stateClause + "AND  [System.State] = 'Impeded'";
                    break;
                case StatusType.New:
                    stateClause = stateClause + "AND  [System.State] = 'New'";
                    break;
                case StatusType.Open:
                    stateClause = stateClause + "AND  [System.State] = 'Open'";
                    break;
                case StatusType.Resolved:
                    stateClause = stateClause + "AND  [System.State] = 'Resolved'";
                    break;
                default:
                    break;
            }

            return stateClause;
        }

        /// <summary>
        /// Gets the assigned to where clause.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="fieldFilter">The field filter.</param>
        /// <returns>
        /// Assigned to WhereClause
        /// </returns>
        private static string GetAssignedToWhereClause(string sessionId, FieldFilter fieldFilter, BurnRetrievalOptions retrievalOptions)
        {
            string assignedToClause = " ";
            ResourceSummaryCollection reesourceSummaryCollection = null;
            if (fieldFilter.AssignedTo != null && !string.IsNullOrEmpty(fieldFilter.AssignedTo.Name))
            {               
                if (fieldFilter.AssignedTo.Name.Contains("Team"))
                {
                    // Get Group Members
                    ResourceService resourceService = new ResourceService();
                    ResourceIdentity groupresourceIdenity = new ResourceIdentity() { Id = fieldFilter.AssignedTo.Id, Name = fieldFilter.AssignedTo.Name };
                    reesourceSummaryCollection = resourceService.GetMembersOfGroup(sessionId, groupresourceIdenity, false);
                    if (reesourceSummaryCollection != null && reesourceSummaryCollection.Count > 0)
                    {
                        assignedToClause = assignedToClause + string.Format("({0})", BuildResourceString(reesourceSummaryCollection));
                    }
                }
                else
                {
                    // Individual resource we can use directly as below.
                    assignedToClause = assignedToClause + string.Format("('{0}')", fieldFilter.AssignedTo.Name);

                    reesourceSummaryCollection = new ResourceSummaryCollection();
                    ResourceSummary resourceSummary = new ResourceSummary();
                    resourceSummary.Identity = new ResourceIdentity();
                    resourceSummary.Identity.Name = fieldFilter.AssignedTo.Name;
                    reesourceSummaryCollection.Add(resourceSummary);
                }

                PopulateTeamMembers(retrievalOptions, reesourceSummaryCollection);
            }

            return assignedToClause;
        }

        /// <summary>
        /// Gets the iteration path where clause.
        /// </summary>
        /// <param name="fieldFilter">The field filter.</param>
        /// <returns></returns>
        private static string GetIterationPathWhereClause(string projectName, FieldFilter fieldFilter)
        {
            string iterationPathClause = " ";

            if (!string.IsNullOrEmpty(fieldFilter.Iteration) && !string.IsNullOrEmpty(fieldFilter.Release))
            {
                //TODO If it is Group need to get the group members from TA SDK API.

                string iterationPath = projectName + "\\" + fieldFilter.Release + "\\" + fieldFilter.Iteration;
                // Individual resource we can use directly as below.
                iterationPathClause = iterationPathClause + string.Format("AND  [System.IterationPath] = '{0}'", iterationPath);
            }

            return iterationPathClause;
        }

        /// <summary>
        /// Builds the resource string.
        /// </summary>
        /// <param name="reesourceSummaryCollection">The reesource summary collection.</param>
        /// <returns>Formatted String</returns>
        private static string BuildResourceString(ResourceSummaryCollection reesourceSummaryCollection)
        {
            string resourceString = string.Empty;
            bool firstTime = true;
            string resourceFormatString = " '{0}'";
            foreach (ResourceSummary resourceSummary in reesourceSummaryCollection)
            {
                if (!firstTime)
                {
                    resourceString += "," + string.Format(resourceFormatString, resourceSummary.Identity.Name);
                }
                else
                { 
                    resourceString = string.Format(resourceFormatString, resourceSummary.Identity.Name);
                }

                firstTime = false;
            }
            return resourceString;
        }

        private static void PopulateTeamMembers(BurnRetrievalOptions retrievalOptions, ResourceSummaryCollection reesourceSummaryCollection)
        {
            if (retrievalOptions.Team.Members == null)
            {
                retrievalOptions.Team.Members = new List<global::DataModel.Resource>();
            }
                
            foreach (ResourceSummary resourceSummary in reesourceSummaryCollection)
            {
                global::DataModel.Resource resource = new global::DataModel.Resource();
                resource.Name = resourceSummary.Identity.Name;
                retrievalOptions.Team.Members.Add(resource);
            }
        }

        #endregion
    }
}
