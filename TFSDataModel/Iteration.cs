using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Iteration
    {
        public Iteration()
        {
        }

        public Iteration(string iterationPath)
        {
            this.IterationPath = iterationPath;
        }

        public string IterationPath { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
