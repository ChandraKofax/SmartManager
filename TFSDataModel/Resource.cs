using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Resource
    {
        public string Name { get; set; }
        public decimal Capacity { get; set; }
        public decimal ExpectedVelocity { get; set; }
        public ResourceLevel Level { get; set; }
        public Site Location { get; set; }

        public override bool Equals(object obj)
        {
            if(obj != null)
            {
                Resource r = obj as Resource;
                if(r!= null)
                {
                    if (r.Name != "Unknown Resource")
                        return r.Name.Equals(Name, StringComparison.OrdinalIgnoreCase);
                    else
                        return false;
                }
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            if(!String.IsNullOrEmpty(Name))
            {
                return Name;
            }

            return base.ToString();
        }
    }
}
