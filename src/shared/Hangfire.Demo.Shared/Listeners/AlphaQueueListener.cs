using Microsoft.Extensions.Logging;

namespace Hangfire.Demo.Shared.Listeners
{
    public class AlphaQueueListener : IAlphaQueueListener
    {
        private readonly ILogger<AlphaQueueListener> _logger;

        public AlphaQueueListener(ILogger<AlphaQueueListener> logger)
        {
            _logger = logger;
        }

        [Queue("alpha")]
        public void Execute(Guid guid)
        {
            Console.WriteLine($"Alpha Execute {guid}");
        }
    }
}
