using Messages;
using NServiceBus;

namespace AnEndpoint
{
    class ReplyFromASagaHandler : IHandleMessages<ReplyFromASaga>
    {
        public Task Handle(ReplyFromASaga message, IMessageHandlerContext context)
        {
            return context.Reply(new CompleteASaga() { SomeId = message.SomeId });
        }
    }
}
