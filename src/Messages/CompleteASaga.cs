using NServiceBus;

namespace Messages
{
    public class CompleteASaga : IMessage
    {
        public string SomeId { get; set; }
    }
}

