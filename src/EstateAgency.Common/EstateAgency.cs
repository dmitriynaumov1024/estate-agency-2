using System;
using Storage.Common;

namespace EstateAgency.Common
{
    public class EstateAgency 
    {
        protected EABackend backend;

        public EstateAgency(EABackend backend)
        {
            this.backend = backend;
        }
    }
}
