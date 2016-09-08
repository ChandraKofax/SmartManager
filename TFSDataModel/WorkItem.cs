using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class WorkItem
    {
        public WorkItem()
        {
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public ItemType Type { get; set; }

        public string Title { get; set; }

        public string AssignedTo { get; set; }

        public string State { get; set; }

        public string IterationPath { get; set; }

        public string ParentId { get; set; }

        public string ParentWorkItemUrl { get; set; }

        public string AssignedRelease { get; set; }
    }
}

