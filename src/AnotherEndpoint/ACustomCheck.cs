using NServiceBus.CustomChecks;

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
