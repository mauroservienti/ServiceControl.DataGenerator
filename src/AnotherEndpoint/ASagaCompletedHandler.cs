using Messages;
using NServiceBus;

namespace AnotherEndpoint
{
    class ASagaCompletedHandler : IHandleMessages<ASagaCompleted>
    {
        public Task Handle(ASagaCompleted message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Saga completed {message.SomeId}");

            return Task.CompletedTask;
        }
    }
}
