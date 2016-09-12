using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ResourceBurnDetails
    {
        public string ResourceName { get; set; }
        public double Burn { get; set; }
        public double Deviation { get; set; }
        public double ProgressOnPlan { get; set; }
        public double TimeSpent { get; set; }
        public string Summary  { get; set; }
    }
}
