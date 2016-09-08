﻿using DataModel;
using System;
using TFS.WiQl;
using TFS.Reporting;
using System.Linq;
using System.Collections.Generic;

namespace TFS
{
    public class Manager2
    {
        private Server server;
        private ReportEngine reportEngine;

        public Manager2()
        {
            reportEngine = new ReportEngine();
        }

        public Manager2(Uri tfsUrl, string projectName, System.Net.ICredentials credentials)
        {
            server = new Server(tfsUrl, projectName, credentials);
            reportEngine = new ReportEngine();
        }

        public WorkItemCollection GetAssignedWorkItems(string tfsUrlPath, string projectName, BurnRetrievalOptions filter, string Query)
        {
            this.Connect(tfsUrlPath, projectName);

            this.PopulateFilterData(filter);
            QueryResult result = server.Execute(QueryStore.GetAssignedWorkItemsQuery(Query), filter.DateRange, true);
            return reportEngine.CompileAssignedWorkItems(result, tfsUrlPath, projectName);
        }

        public ChildItemCollection GetChildTasksDetails(string tfsUrlPath, string projectName, string parentId)
        {
            this.Connect(tfsUrlPath, projectName);
            QueryResult result = server.Execute(QueryStore.GetChildTasksQuery(parentId), null, false);
            ChildItemCollection ChildItemCollection = reportEngine.CompileChildTasks(result, tfsUrlPath, projectName);
            return ChildItemCollection;
        }

        public BurnReport GetBurnDetails(string tfsUrlPath, string projectName, BurnRetrievalOptions filter, string Query)
        {
            this.Connect(tfsUrlPath, projectName);

            this.PopulateFilterData(filter);
            QueryResult result = server.Execute(QueryStore.GetBurnQuery(filter, Query), filter.DateRange, true);
            BurnReport burnreport = reportEngine.CompileBurnData(result, filter, tfsUrlPath, projectName);
            burnreport.SetView(ViewType.Resource, filter.Team.Members);
            return burnreport;
        }

        public IterationCollection GetIterations(string tfsUrlPath, string projectName, string release)
        {
            this.Connect(tfsUrlPath, projectName);
            return server.GetIterations(projectName, release);
        }

        public ReleaseVersionCollection GetReleases(string tfsUrlPath, string projectName)
        {
            this.Connect(tfsUrlPath, projectName);
            return server.GetReleases(projectName);
        }

        private void Connect(string tfsUrlPath, string projectName)
        {
            Uri tfsUrl = new Uri(tfsUrlPath);
            System.Net.ICredentials cred = System.Net.CredentialCache.DefaultCredentials;
            server = new Server(tfsUrl, projectName, cred);
        }

        private void PopulateFilterData(BurnRetrievalOptions filter)
        {
            PopulateFilterWithDateRange(filter);
        }
       
        private void PopulateFilterWithDateRange(BurnRetrievalOptions filter)
        {
            if (filter.DateRange == null)
            {
                filter.BurnDuration = (BurnDuration)filter.BurnDuration2;
                Duration durationFilter = new Duration
                {
                    From = DateTime.Now,
                    To = DateTime.Now
                };

                switch (filter.BurnDuration)
                {
                    case BurnDuration.PreviousDay:
                        durationFilter.From = DateTime.Now.Date.AddDays(-1).Add(Constants.ClientTimeConstant);
                        durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                        break;
                    case BurnDuration.Last3Days:
                        durationFilter.From = DateTime.Now.Date.AddDays(-3).Add(Constants.ClientTimeConstant);
                        durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                        break;
                    case BurnDuration.Last7Days:
                        durationFilter.From = DateTime.Now.Date.AddDays(-7).Add(Constants.ClientTimeConstant);
                        durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                        break;
                    case BurnDuration.Last15Days:
                        durationFilter.From = DateTime.Now.Date.AddDays(-15).Add(Constants.ClientTimeConstant);
                        durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                        break;
                    case BurnDuration.Last30Days:
                        durationFilter.From = DateTime.Now.Date.AddDays(-30).Add(Constants.ClientTimeConstant);
                        durationFilter.To = DateTime.Now.Date.Add(Constants.ClientTimeConstant);
                        break;
                    case BurnDuration.Duration:
                        durationFilter.From = filter.DateRange.From.Date.Add(Constants.ClientTimeConstant);
                        durationFilter.To = filter.DateRange.To.Date.Add(Constants.ClientTimeConstant);
                        break;
                }

                filter.DateRange = durationFilter;
            }
        }


    }
}