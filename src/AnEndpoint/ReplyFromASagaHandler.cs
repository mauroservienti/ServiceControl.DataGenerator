﻿using Messages;

namespace AnEndpoint
{
    class ReplyFromASagaHandler : IHandleMessages<ReplyFromASaga>
    {
        public Task Handle(ReplyFromASaga message, IMessageHandlerContext context)
        {
            RandomFailure.MaybeItFails();
            return context.Reply(new CompleteASaga() { SomeId = message.SomeId });
        }
    }
}
