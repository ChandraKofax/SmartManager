using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Team
    {
        public string Name { get; set; }
        public List<Resource> Members { get; set; }
        public Resource Manager { get; set; }
        public List<Resource> TechSpecialist { get; set; }

        public bool IsResourceMemberOfTeam(Resource resource)
        {
            if(Members != null)
            {
                return Members.Any(r => r.Equals(resource));
            }
            return false;
        }
    }
}
