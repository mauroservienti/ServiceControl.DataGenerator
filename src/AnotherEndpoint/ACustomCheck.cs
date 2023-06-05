using NServiceBus.CustomChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherEndpoint
{
    internal class ACustomCheck : NServiceBus.CustomChecks.CustomCheck
    {
        public ACustomCheck()
            : base("ACustomCheck", "ACategory", TimeSpan.FromSeconds(10))
        {
            
        }

        public override Task<CheckResult> PerformCheck(CancellationToken cancellationToken = default)
        {
            var minutes = DateTime.Now.Minute;
            if (minutes % 2 == 1)
            {
                return Task.FromResult(CheckResult.Failed("Minutes is odd"));
            }

            return Task.FromResult(CheckResult.Pass);
        }
    }
}
