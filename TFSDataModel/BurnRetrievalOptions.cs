using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class BurnRetrievalOptions
    {
        public BurnRetrievalOptions()
        {
            this.Team = new Team();
        }

        public short BurnDuration2 { get; set; }
        public BurnDuration BurnDuration { get; set; }
        public Sprint Sprint { get; set; }
        public Duration DateRange { get; set; }
        public Team Team { get; set; }
        public bool IncludeRemovedTasks { get; set; }
    }
}
