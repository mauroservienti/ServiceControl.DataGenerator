class ThisIsAMessageHandler : IHandleMessages<ThisIsAMessage>
{
    public Task Handle(ThisIsAMessage message, IMessageHandlerContext context)
    {
        if (message.Behavior == MessageBehavior.Succeed)
        {
            return Task.CompletedTask;
        }

        throw new InvalidOperationException("Oooops, message handling failed.");
    }
}
