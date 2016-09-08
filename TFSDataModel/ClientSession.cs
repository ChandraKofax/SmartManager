using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartManager.DataModel
{
    public class ClientSession
    {
        private bool isValid;
        private Guid sessionId;
        private ClientDetails client;

        public Guid Id
        {
            get
            {
                if(!isValid)
                {
                    throw new Exception("Session not initialized");
                }

                return sessionId;
            }
        }

    }
}
