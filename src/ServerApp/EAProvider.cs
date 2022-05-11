using System;
using EstateAgency.Common;
using EstateAgency.Backends;

namespace ServerApp 
{
    public static class EAProvider
    {
        private static string fPath = "./database.json";
        private static EABackend backend = null;

        public static void Init ()
        {
            backend = new FileBackend(fPath);
            backend.Activate();
        }

        public static void Shutdown ()
        {
            backend.Shutdown();
        }

        public static EABackend EA (this object obj)
        {
            return backend;
        }

        public static EABackend Instance {
            get { return backend; }
        }
    }
}
