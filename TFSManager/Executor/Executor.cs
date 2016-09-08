using DataModel;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFS.Model;
using TFS.WiQl;

namespace TFS.Execution
{
    public class Executor
    {
        public QueryResult Execute(WorkItemStore workItemStore, TFSQuery tfsQuery, Duration durationFilter, bool dayPrecision)
        {
            var queryResults = new QueryResult();
            // get the query
                var queryDefinition = new QueryDefinition(Guid.NewGuid().ToString(), tfsQuery.QueryString);
                var query = new Query(workItemStore, tfsQuery.QueryString, tfsQuery.Context, dayPrecision);
                if (queryDefinition.QueryType == QueryType.OneHop || queryDefinition.QueryType == QueryType.Tree)
                {
                    var workItemLinks = query.RunLinkQuery();

                    int[] ids = (from WorkItemLinkInfo info in workItemLinks
                                 select info.TargetId).ToArray();

                    //
                    // Next we want to create a new query that will retrieve all the column values from the original query, for
                    // each of the work item IDs returned by the original query.
                    //
                    var detailsWiql = new StringBuilder();
                    detailsWiql.AppendLine("SELECT");
                    bool first = true;

                    foreach (FieldDefinition field in query.DisplayFieldList)
                    {
                        detailsWiql.Append(" ");
                        if (!first)
                            detailsWiql.Append(",");
                        detailsWiql.AppendLine("[" + field.ReferenceName + "]");
                        first = false;
                    }

                    detailsWiql.AppendLine("FROM WorkItems");

                    //
                    // Get the work item details
                    //

                    int[] uniqueIds = ids.Distinct().ToArray();

                    var flatQuery = new Query(workItemStore, detailsWiql.ToString(), uniqueIds);
                    Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCollection flatQueryList = flatQuery.RunQuery();

                    //When query returns details, timestamps are converted to client zone. Use Client timestamp to further filter data
                    queryResults.Result = FilterOutItemsNotNeeded(WalkLinks(flatQueryList, workItemLinks, null), durationFilter);
                }
                else
                {
                    var workItems = query.RunQuery();
                    var result = new List<WorkItemNode>();
                    foreach (Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem in workItems)
                    {
                        result.Add(new WorkItemNode
                        {
                            Item = workItem,
                            Children = new List<WorkItemNode>()
                        });
                    }
                    queryResults.Result = result;
                }

                return queryResults;
        }

        private List<WorkItemNode> FilterOutItemsNotNeeded(List<WorkItemNode> list, Duration duration)
        {
            if (duration != null)
            {
                return FilterOutItemsOnDate(list, null, duration);
            }
            else
            {
                return list;
            }
            //List<WorkItemNode> finalList = FilterOutOrphanItems(workItems);
        }

        private List<WorkItemNode> FilterOutOrphanItems(List<WorkItemNode> workItems)
        {
            return null;   
        }

        private List<WorkItemNode> FilterOutItemsOnDate(List<WorkItemNode> list, List<WorkItemNode> finalList, Duration duration)
        {
            if (finalList == null) { finalList = new List<WorkItemNode>(); }

            list.ForEach(w =>
                {
                    try
                    {
                        if (w.Item.CreatedDate < duration.To)
                        {
                            WorkItemNode wiClone = new WorkItemNode {
                                Item = w.Item.ChangedDate > duration.To ? w.Item.Store.GetWorkItem(w.Item.Id, duration.To) : w.Item,
                                Children = new List<WorkItemNode>() };
                            finalList.Add(wiClone);
                            wiClone.Children = FilterOutItemsOnDate(w.Children, wiClone.Children, duration);
                        }
                    }
                    catch
                    { //i
                    }
                });
            return finalList;
        }

        private static int PrintTrees(WorkItemStore wiStore, WorkItemLinkInfo[] wiTrees, int sourceId, int iThis)
        {
            int iNext = 0;

            // Get the parent of this item, if it has one
            Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem source = null;
            if (sourceId != 0)
            {
                source = wiStore.GetWorkItem(wiTrees[iThis].SourceId);
            }

            // Process the items in the list that have the same parent as this user story
            while (iThis < wiTrees.Length && wiTrees[iThis].SourceId == sourceId)
            {
                // Get this user story
                Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem target = wiStore.GetWorkItem(wiTrees[iThis].TargetId);
                if (iThis < wiTrees.Length - 1)
                {
                    if (wiTrees[iThis].TargetId == wiTrees[iThis + 1].SourceId)
                    {
                        // The next item is this user story's child. Process the children
                        iNext = PrintTrees(wiStore, wiTrees, wiTrees[iThis + 1].SourceId, iThis + 1);
                    }
                    else
                    {
                        // The next item is not this user story's child.
                        iNext = iThis + 1;
                    }
                }
                else
                {
                    // This user story is the last one.
                    iNext = iThis + 1;
                }

                iThis = iNext;
            }

            return iNext;
        }

        private List<WorkItemNode> WalkLinks(Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemCollection workItemCollection, WorkItemLinkInfo[] workItemLinks, WorkItemNode current)
        {

            var currentId = 0;
            if (current != null)
            {
                currentId = current.Item.Id;
            }

            var workItems = (from linkInfo in workItemLinks
                             where linkInfo.SourceId == currentId
                             select new WorkItemNode()
                             {
                                 Item = workItemCollection[workItemCollection.IndexOf(linkInfo.TargetId)],
                                                              }).ToList();
            workItems.ForEach(w => w.Children = WalkLinks(workItemCollection, workItemLinks, w));
            return workItems;
        }
    }
}
