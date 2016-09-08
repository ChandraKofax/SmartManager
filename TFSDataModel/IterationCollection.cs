using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class IterationCollection
    {
        public IterationCollection()
        {
            this.Iterations = new List<Iteration>();
        }

        public List<Iteration> Iterations { get; set; }
    }
}
