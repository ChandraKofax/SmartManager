using DataModel;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using TFS.Execution;
using TFS.WiQl;
using System.Collections;
using Microsoft.TeamFoundation.Server;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

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
        private ICommonStructureService4 css4;

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
                css4 = tpc.GetService<ICommonStructureService4>();
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

        public Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem GetWorkItem(int workItemID)
        {
            Validate();
            Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem = workItemStore.GetWorkItem(workItemID);
            return workItem;
        }

        public Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCollection GetListOfWorkItems(TFSQuery query)
        {
            Validate();
            Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCollection workItems = workItemStore.Query(query.QueryString);
            return workItems;
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

        public DataModel.ProjectCollection GetProjects()
        {
            DataModel.ProjectCollection result = new DataModel.ProjectCollection();
            foreach (Microsoft.TeamFoundation.WorkItemTracking.Client.Project project in workItemStore.Projects)
            {
                DataModel.Project internalProject = new DataModel.Project(project.Name);
                result.Projects.Add(internalProject);
            }

            return result;
        }

        public ReleaseVersionCollection GetReleases(string projectName)
        {
            ReleaseVersionCollection result = new ReleaseVersionCollection();
            foreach (Microsoft.TeamFoundation.WorkItemTracking.Client.Project project in workItemStore.Projects)
            {
                if (project.Name.Equals(projectName))
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
            Hashtable projectDatesHash = new Hashtable();
            foreach (Microsoft.TeamFoundation.WorkItemTracking.Client.Project project in workItemStore.Projects)
            {
                if (project.Name.Equals(projectName))
                {
                    NodeInfo[] structures = css4.ListStructures(project.Uri.ToString());
                    NodeInfo iterations = structures.FirstOrDefault(n => n.StructureType.Equals("ProjectLifecycle"));

                    if (iterations != null)
                    {
                        //XmlElement iterationsTree = css4.GetNodesXml(new[] { iterations.Uri }, true);
                        //XmlNodeList nodeList = iterationsTree.ChildNodes;
                        //XElement doc = XElement.Parse(nodeList[0].InnerXml);

                        //IEnumerable<XElement> elements = from node in doc.Elements("Node")
                        //                                 select node;

                        //int nodesCount = elements.Count();
                        //foreach (XElement element in elements)
                        //{
                        //    IEnumerable<XElement> fields = from field in element.Elements() select field;
                        //    foreach (XElement field in fields)
                        //    {
                        //        IEnumerable<XElement> projectdetails = from detail in field.Elements() select detail;
                        //        foreach (XElement detail in projectdetails)
                        //        {
                        //            try
                        //            {
                        //                Iteration iteration = new Iteration(item.Name);
                        //                result.Iterations.Add(detail.Attribute("Name").Value, new IterationDates(detail.Attribute("StartDate").Value, detail.Attribute("FinishDate").Value));
                        //            }
                        //            catch { }
                        //        }
                        //    }
                        //}
                    }

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
