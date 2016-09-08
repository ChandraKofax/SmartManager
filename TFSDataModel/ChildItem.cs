using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ChildItem
    {
        public ChildItem()
        {
            this.TaskEffortDetails = new TaskEffort();
        }

        public int ItemId { get; set; }

        public string WorkItemUrl { get; set; }

        public string WorkItemType { get; set; }

        public string Title { get; set; }

        public string AssignedTo { get; set; }

        public string State { get; set; }

        public TaskEffort TaskEffortDetails { get; set; }

        public string IterationPath { get; set; }
    }
}

