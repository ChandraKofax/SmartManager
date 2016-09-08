using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Sprint
    {
        public string IterationPath { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public Duration Duration { get; set; }
    }
}
