using Messages;

namespace OneMoreEndpoint
{
    class ASagaCompletedHandler : IHandleMessages<ASagaCompleted>
    {
        public Task Handle(ASagaCompleted message, IMessageHandlerContext context)
        {
            RandomFailure.MaybeItFails();
            Console.WriteLine($"Saga completed {message.SomeId}");

            return Task.CompletedTask;
        }
    }
}
