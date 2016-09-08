using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFS;

namespace SmartManagerWindows
{
    public static class ClientApplication
    {
        private static Manager manager;

        public static void Initialize(Uri teamCollectionUrl, string projectName, System.Net.ICredentials credentials)
        {
            manager = new Manager(teamCollectionUrl, projectName, credentials);
        }

        public static Manager Manager
        {
            get
            {
                return manager;
            }
        }
    }
}
