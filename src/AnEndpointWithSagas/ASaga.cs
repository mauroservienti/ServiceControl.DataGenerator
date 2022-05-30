using Messages;
using NServiceBus;

namespace AnEndpointWithSagas
{
    class ASaga : Saga<ASagaData>,
        IAmStartedByMessages<Kickoff>,
        IHandleMessages<CompleteASaga>,
        IHandleTimeouts<ATimeout>
    {
        public Task Handle(Kickoff message, IMessageHandlerContext context)
        {
            return RequestTimeout<ATimeout>(context, TimeSpan.FromSeconds(10));
        }

        public Task Handle(CompleteASaga message, IMessageHandlerContext context)
        {
            MarkAsComplete();
            return Task.CompletedTask;
        }

        public Task Timeout(ATimeout state, IMessageHandlerContext context)
        {
            var rnd = new Random();
            var number = rnd.Next(0, 10);
            if(number == 6)
            {
                throw new InvalidOperationException("Oooops, random error");
            }

            return ReplyToOriginator(context, new ReplyFromASaga() { SomeId = Data.SomeId });
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ASagaData> mapper)
        {
            mapper.MapSaga(d => d.SomeId)
                .ToMessage<Kickoff>(m => m.SomeId)
                .ToMessage<CompleteASaga>(m => m.SomeId);
        }
    }

    class ASagaData : ContainSagaData
    {
        public string SomeId { get; set; }
    }

    class ATimeout { }
}
