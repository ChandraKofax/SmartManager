using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ReleaseVersion
    {
        public ReleaseVersion()
        {
        }

        public ReleaseVersion(string release)
        {
            this.Release = release;
        }

        public string Release { get; set; }
    }
}
