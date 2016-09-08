using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ChildItemCollection
    {
        public ChildItemCollection()
        {
            this.ChildItems = new List<ChildItem>();
        }

        public List<ChildItem> ChildItems { get; set; }
    }
}
