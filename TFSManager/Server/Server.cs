using DataModel;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using TFS.Execution;
using TFS.WiQl;
using System.Collections;

namespace TFS
{
    sealed class Server
    {
        private Uri teamUri;
        private WorkItemStore workItemStore;
        private bool isValid;
        private Executor executor;
        private TfsTeamProjectCollection tpc;
        private string projectName;
        private List<WorkItemLinkType> linkTypes;

        public Server(Uri tfsUrl, string projectName, System.Net.ICredentials credentials)
        {
            this.ResetDetails();
            try
            {
                teamUri = tfsUrl;
                this.projectName = projectName;
                tpc = new TfsTeamProjectCollection(teamUri, credentials);
                tpc.Connect(ConnectOptions.IncludeServices);
                workItemStore = tpc.GetService<WorkItemStore>();
                linkTypes = new List<WorkItemLinkType>(workItemStore.WorkItemLinkTypes);
                this.executor = new TFS.Execution.Executor();
                this.isValid = true;
            }
            catch
            {
                this.ResetDetails();
            }
        }

        public QueryResult Execute(TFSQuery query, Duration durationFilter, bool dayPrecision)
        {
            Validate();
            return executor.Execute(workItemStore, query, durationFilter, dayPrecision);
        }

        public QueryResult GetWIFeatureGroup()
        {
            Validate();
            Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem wiFeatureGroup = workItemStore.GetWorkItem(742678);

            return null;
        }

        private void ResetDetails()
        {
            this.isValid = false;
            this.teamUri = null;
            this.workItemStore = null;
            this.executor = null;
        }

        public void Validate()
        {
            if (!isValid)
            {
                throw new Exception("Invalid server details");
            }
        }

        public ReleaseVersionCollection GetReleases(string projectName)
        {
            ReleaseVersionCollection result = new ReleaseVersionCollection();
            foreach (Project project in workItemStore.Projects)
            {
                if (string.Compare(project.Name, projectName, true) != -1)
                {
                    foreach (Node node in project.IterationRootNodes)
                    {
                        ReleaseVersion releaseVersion = new ReleaseVersion(node.Name);
                        result.ReleaseVersions.Add(releaseVersion);
                    }

                    break;
                }
            }

            return result;
        }

        public IterationCollection GetIterations(string projectName, string release)
        {
            IterationCollection result = new IterationCollection();
            foreach (Project project in workItemStore.Projects)
            {
                if (string.Compare(project.Name, projectName, true) != -1)
                {
                    foreach (Node node in project.IterationRootNodes)
                    {
                        if (release == node.Name)
                        {
                            RecursiveAddIterationPath(node, result);
                            break;
                        }
                    }

                    break;
                }
            }

            return result;
        }

        private static void RecursiveAddIterationPath(Node node, IterationCollection result)
        {
            foreach (Node item in node.ChildNodes)
            {
                Iteration iteration = new Iteration(item.Name);
                result.Iterations.Add(iteration);
                if (item.HasChildNodes)
                {
                    RecursiveAddIterationPath(item, result);
                }
            }
        }
    }
}
