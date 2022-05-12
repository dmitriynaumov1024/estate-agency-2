using System;
using EstateAgency.Common;
using EstateAgency.Backends;
using Microsoft.AspNetCore.Mvc;

namespace ServerApp 
{
    public static class EAProvider
    {
        private static string fPath = "./database.json";
        private static EABackend backend = null;
        private static EstateAgency.Common.EstateAgency agency = null;

        public static void Init ()
        {
            backend = new FileBackend(fPath);
            backend.Activate();
            agency = new EstateAgency.Common.EstateAgency(backend);
        }

        public static void Shutdown ()
        {
            backend.Shutdown();
        }

        public static EstateAgency.Common.EstateAgency EA (this ControllerBase obj)
        {
            return agency;
        }
    }
}
