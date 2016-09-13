using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Project
    {
        public Project()
        {
        }

        public Project(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}

