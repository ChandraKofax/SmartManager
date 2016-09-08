using System.Collections.Generic;

namespace TFS.WiQl
{
    public class TFSQuery
    {
        public string QueryString;
        public Dictionary<string, object> Context;
        public TFSQuery()
        {
            Context = new Dictionary<string, object>();
        }
    }
}
