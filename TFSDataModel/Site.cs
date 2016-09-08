using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Site
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public Resource Manager { get; set; }
//        public GeographicLocation Location { get; set; }
        public Site()
        {
            Teams = new List<Team>();
        }
    }
}
