using Microsoft.Extensions.Logging;

namespace Hangfire.Demo.Shared.Listeners
{
    public class BetaQueueListener : IBetaQueueListener
    {
        private readonly ILogger<BetaQueueListener> _logger;

        public BetaQueueListener(ILogger<BetaQueueListener> logger)
        {
            _logger = logger;
        }

        [Queue("beta")]
        public void Execute()
        {
            Guid guid = Guid.NewGuid();
            Console.WriteLine($"Beta Executed {guid}");
        }
    }
}
