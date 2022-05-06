using System;
using Storage.Common;

namespace EstateAgency.Common
{
    public class EstateAgency : IDisposable
    {
        protected EABackend backend;

        public EstateAgency(EABackend backend)
        {
            this.backend = backend;
        }

        public void Dispose ()
        {
            
        }
    }
}
