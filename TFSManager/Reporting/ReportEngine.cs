using System;
using System.Linq;
using DataModel;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TFS.Model;
using TFS.WiQl;

namespace TFS.Reporting
{
    public class ReportEngine
    {
        /// <summary>
        /// Compiles the burn data.
        /// </summary>
        /// <param name="burnQueryResult">The burn query result.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>BurnReport Class</returns>
        public BurnReport CompileBurnData(QueryResult burnQueryResult, BurnRetrievalOptions filter, string tfsUrlPath, string projectName)
        {
            BurnReport burnReport = new BurnReport();
            foreach (var workItem in burnQueryResult.Result)
            {
                var reportingItem = Reporting.ReportFactory.CreateItem(workItem);
                reportingItem.CreateBurnReport(filter);
                reportingItem.WorkItemUrl = tfsUrlPath + "/" + projectName + "/_workitems#_a=edit&id=" + reportingItem.Id;
                reportingItem.GetParentId(workItem);
                if (!string.IsNullOrEmpty(reportingItem.ParentId))
                {
                    reportingItem.ParentWorkItemUrl = tfsUrlPath + "/" + projectName + "/_workitems#_a=edit&id=" + reportingItem.ParentId;
                }

                burnReport.AllItems.Add(reportingItem);
            }

            burnReport.FinalizeReport();
            return burnReport;
        }

        /// <summary>
        /// Compiles the assigned work items.
        /// </summary>
        /// <param name="burnQueryResult">The burn query result.</param>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>WorkItem Collection</returns>
        public DataModel.WorkItemCollection CompileAssignedWorkItems(QueryResult burnQueryResult, string tfsUrlPath, string projectName)
        {
            DataModel.WorkItemCollection workItemCollection = new DataModel.WorkItemCollection();
            foreach (var workItemNode in burnQueryResult.Result)
            {
                var reportingItem = Reporting.ReportFactory.CreateItem(workItemNode);
                DataModel.WorkItem workItem = new DataModel.WorkItem();

                workItem.Id = reportingItem.Id;
                workItem.Title = reportingItem.Title;
                workItem.Url = tfsUrlPath + "/" + projectName + "/_workitems#_a=edit&id=" + reportingItem.Id;
                workItem.Type = reportingItem.Type;
                workItem.AssignedTo = reportingItem.AssignedTo != null ? reportingItem.AssignedTo.Name : string.Empty;
                workItem.State = reportingItem.State;
                if (workItemNode.Item.Fields.Contains(TFSLiterals.AssignedRelease))
                {
                    workItem.AssignedRelease = Convert.ToString(workItemNode.Item.Fields[TFSLiterals.AssignedRelease].Value);
                }
                else
                {
                    workItem.AssignedRelease = string.Empty;
                }

                reportingItem.GetParentId(workItemNode);
                workItem.ParentId = reportingItem.ParentId;
                if (!string.IsNullOrEmpty(reportingItem.ParentId))
                {
                    workItem.ParentWorkItemUrl = tfsUrlPath + "/" + projectName + "/_workitems#_a=edit&id=" + reportingItem.ParentId;
                }

                workItemCollection.WorkItems.Add(workItem);
            }

            return workItemCollection;
        }

        /// <summary>
        /// Compiles the child tasks.
        /// </summary>
        /// <param name="taskUpdatesQueryResult">The task updates query result.</param>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="workItem">The work item.</param>
        /// <returns>
        /// ChildItem Collection
        /// </returns>
        public ChildItemCollection CompileChildTasks(QueryResult taskUpdatesQueryResult, string tfsUrlPath, string projectName, Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem parentWorkItem)
        {
            ChildItemCollection childItemCollection = new ChildItemCollection();

            foreach (var workItemNode in taskUpdatesQueryResult.Result)
            {
                if (workItemNode.Children != null && workItemNode.Children.Count > 0)
                {
                    foreach (WorkItemNode childWorkItemNode in workItemNode.Children)
                    {
                        Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem = childWorkItemNode.Item;
                        if (Convert.ToString(workItemNode.Item.Fields[TFSLiterals.AssignedTo].Value) == Convert.ToString(workItem.Fields[TFSLiterals.AssignedTo].Value))
                        {
                            ChildItem childItem = GetChildItem(tfsUrlPath, projectName, workItem);
                            childItemCollection.ChildItems.Add(childItem);
                        }
                    }
                }
            }

            //// if we search only the Tasks it should return the same task as child item with detail info.
            if (parentWorkItem != null && parentWorkItem.Type != null && parentWorkItem.Type.Name == "Task")
            {
                ChildItem childItem = GetChildItem(tfsUrlPath, projectName, parentWorkItem);
                childItemCollection.ChildItems.Add(childItem);
            }

            return childItemCollection;
        }

        /// <summary>
        /// Compiles the list of work items.
        /// </summary>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="queryResult">The query result.</param>
        /// <returns>Child Item Collection</returns>
        public ChildItemCollection CompileListOfWorkItems(string tfsUrlPath, string projectName, QueryResult queryResult)
        {
            ChildItemCollection childItemCollection = new ChildItemCollection();

            foreach (var workItemNode in queryResult.Result)
            {
                foreach (WorkItemNode childWorkItemNode in workItemNode.Children)
                {
                    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem = childWorkItemNode.Item;
                    ChildItem childItem = GetChildItem(tfsUrlPath, projectName, workItem);
                    childItemCollection.ChildItems.Add(childItem);
                }
            }

            return childItemCollection;
        }

        /// <summary>
        /// Compiles the resolved or closed bugs.
        /// </summary>
        /// <param name="bugsResolvedQueryResult">The bugs resolved query result.</param>
        /// <param name="bugsClosedQueryResult">The bugs closed query result.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Report Class</returns>
        public Report CompileResolvedOrClosedBugs(QueryResult bugsResolvedQueryResult, QueryResult bugsClosedQueryResult, BurnRetrievalOptions filter)
        {
            Report bugsResolvedOrClosedReport = new Report();
            foreach (var workItem in bugsResolvedQueryResult.Result)
            {
                var reportingItem = Reporting.ReportFactory.CreateItem(workItem);
                bugsResolvedOrClosedReport.AllItems.Add(reportingItem);
            }

            foreach (var workItem in bugsClosedQueryResult.Result)
            {
                var reportingItem = Reporting.ReportFactory.CreateItem(workItem);
                bugsResolvedOrClosedReport.AllItems.Add(reportingItem);
            }

            bugsResolvedOrClosedReport.FinalizeReport();
            return bugsResolvedOrClosedReport;
        }

        /// <summary>
        /// Compiles the cause and resolution issues.
        /// </summary>
        /// <param name="bugFixUpdatesQueryResult">The bug fix updates query result.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Report Class</returns>
        public Report CompileCauseAndResolutionIssues(QueryResult bugFixUpdatesQueryResult, BurnRetrievalOptions filter)
        {
            Report causeAndResolutionIssueReport = new Report();
            foreach (var workItem in bugFixUpdatesQueryResult.Result)
            {
                var reportingItem = (Bug)Reporting.ReportFactory.CreateItem(workItem);
                if (reportingItem.IsCauseResolutionDetailsMissing())
                {
                    causeAndResolutionIssueReport.AllItems.Add(reportingItem);
                }
            }

            causeAndResolutionIssueReport.FinalizeReport();
            return causeAndResolutionIssueReport;
        }

        /// <summary>
        /// Compiles the task update issues.
        /// </summary>
        /// <param name="taskUpdatesQueryResult">The task updates query result.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Report Class</returns>
        public Report CompileTaskUpdateIssues(QueryResult taskUpdatesQueryResult, BurnRetrievalOptions filter)
        {
            Report taskUpdateIssuesReport = new Report();
            foreach (var workItem in taskUpdatesQueryResult.Result)
            {
                var reportingItem = Reporting.ReportFactory.CreateItem(workItem);
                if (reportingItem.HasTaskUpdateIssues())
                {
                    taskUpdateIssuesReport.AllItems.Add(reportingItem);
                }
            }

            taskUpdateIssuesReport.FinalizeReport();
            return taskUpdateIssuesReport;
        }

        #region private        
        /// <summary>
        /// Gets the child item.
        /// </summary>
        /// <param name="tfsUrlPath">The TFS URL path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="workItem">The work item.</param>
        /// <returns>Child Item</returns>
        private static ChildItem GetChildItem(string tfsUrlPath, string projectName, Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem)
        {
            ChildItem childItem = new ChildItem();
            childItem.ItemId = workItem.Id;
            childItem.IterationPath = workItem.IterationPath;
            childItem.State = workItem.State;
            childItem.Title = workItem.Title;

            Field assignedToField = workItem.Fields[TFSLiterals.AssignedTo];
            childItem.AssignedTo = (string)assignedToField.Value;

            Field workItemTypeField = workItem.Fields[TFSLiterals.WorkItemType];
            childItem.WorkItemType = (string)workItemTypeField.Value;

            if (workItem.Fields.Contains(TFSLiterals.CompletedWork))
            {
                Field completedWorkfield = workItem.Fields[TFSLiterals.CompletedWork];
                childItem.TaskEffortDetails.CompletedWork = (double)completedWorkfield.Value;
            }

            if (workItem.Fields.Contains(TFSLiterals.CompletedWork))
            {
                Field remainingWorkfield = workItem.Fields[TFSLiterals.RemainingWork];
                childItem.TaskEffortDetails.RemainingWork = (double)remainingWorkfield.Value;
            }

            if (workItem.Fields.Contains(TFSLiterals.CompletedWork))
            {
                Field OriginalWorkfield = workItem.Fields[TFSLiterals.OriginalEstimate];
                childItem.TaskEffortDetails.OriginalEstimate = (double)OriginalWorkfield.Value;
            }

            childItem.WorkItemUrl = tfsUrlPath + "/" + projectName + "/_workitems#_a=edit&id=" + workItem.Id;
            return childItem;
        }

        #endregion
    }
}
