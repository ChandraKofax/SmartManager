using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ReleaseVersionCollection
    {
        public ReleaseVersionCollection()
        {
            this.ReleaseVersions = new List<ReleaseVersion>();
        }

        public List<ReleaseVersion> ReleaseVersions { get; set; }
    }
}
