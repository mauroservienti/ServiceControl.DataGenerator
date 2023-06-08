using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedConfig
{
    public static class CommonRecoverabilityConfiguration
    {
        public static void DisableRecoverability(this EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration
            .Recoverability()
                .Immediate(i => i.NumberOfRetries(0))
                .Delayed(d => d.NumberOfRetries(0));
        }
    }
}
