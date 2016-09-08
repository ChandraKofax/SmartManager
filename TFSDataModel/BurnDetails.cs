using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class BurnDetails
    {
        public BurnDetails()
        {
            this.ResourceBurnDetails = new List<ResourceBurnDetails>();
        }

        public List<ResourceBurnDetails> ResourceBurnDetails { get; set; }
    }
}
