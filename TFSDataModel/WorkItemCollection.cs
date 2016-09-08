using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class WorkItemCollection
    {
        public WorkItemCollection()
        {
            this.WorkItems = new List<WorkItem>();
        }

        public List<WorkItem> WorkItems { get; set; }
    }
}
