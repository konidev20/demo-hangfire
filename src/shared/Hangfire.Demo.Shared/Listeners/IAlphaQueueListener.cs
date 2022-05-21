namespace Hangfire.Demo.Shared.Listeners
{
    internal interface IAlphaQueueListener
    {
        void Execute(Guid guid);
    }
}
