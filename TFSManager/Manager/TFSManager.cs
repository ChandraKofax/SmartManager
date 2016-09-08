using DataModel;
using System;
using TFS.WiQl;
using TFS.Reporting;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFS
{
    public class Manager
    {
        private Server server;
        private ReportEngine reportEngine;

        public Manager()
        {
        }

        public Manager(Uri tfsUrl, string projectName, System.Net.ICredentials credentials)
        {
            server = new Server(tfsUrl, projectName, credentials);
            reportEngine = new ReportEngine();
        }

        public void Connect(string tfsUrlPath, string projectName)
        {
            Uri tfsUrl = new Uri(tfsUrlPath);
            System.Net.ICredentials cred = System.Net.CredentialCache.DefaultCredentials;
            server = new Server(tfsUrl, projectName, cred);
        }

        public BurnReport GetBurnDetails(BurnRetrievalOptions filter)
        {
            QueryResult result = server.Execute(QueryStore.GetBurnQueryOLD(filter), filter.DateRange, true);
            return reportEngine.CompileBurnData(result, filter, "", "");
        }

        public List<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem> GetChildTasks(List<string> WorkItemIds)
        {
            List<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem> WorkItems = null;
            string burnQuery = "";
            TFSQuery query = new TFSQuery { QueryString = burnQuery };
            query.Context.Add("project", "KTA");
            return WorkItems;
        }

        public QueryResult GetWIFeatureGroup()
        {
            QueryResult result = server.GetWIFeatureGroup();

            return result;
        }

        public Report GetCauseAndResolutionIssueDetails(BurnRetrievalOptions filter)
        {
            QueryResult result = server.Execute(QueryStore.GetBugResolutionDataQuery(filter), filter.DateRange, false);
            return reportEngine.CompileCauseAndResolutionIssues(result, filter);
        }

        public Report GetTaskUpdateDetails(BurnRetrievalOptions filter)
        {
            QueryResult result = server.Execute(QueryStore.GetTaskUpdatesDataQuery(filter), filter.DateRange, false);
            return reportEngine.CompileTaskUpdateIssues(result, filter);
        }

        public Report GetBugsResolvedOrClosedDetails(BurnRetrievalOptions filter)
        {
            QueryResult resultResolvedBugs = server.Execute(QueryStore.GetBugsResolvedQuery(filter), filter.DateRange, false);
            QueryResult resultClosedBugs = server.Execute(QueryStore.GetBugsClosedQuery(filter), filter.DateRange, false);
            return reportEngine.CompileResolvedOrClosedBugs(resultResolvedBugs, resultClosedBugs, filter);
        }
    }
}
