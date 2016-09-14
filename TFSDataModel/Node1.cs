using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Node1
    {
        public Node1 Children { get; set; }

        public string StructureType { get; set; }
        public string ProjectID { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string NodeID { get; set; }
        public string FinishDate { get; set; }
        public string StartDate { get; set; }
        public string ParentID { get; set; }
    }
}
