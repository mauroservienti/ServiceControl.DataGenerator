﻿using Messages;

namespace AnEndpointWithSagas
{
    class ASaga : Saga<ASagaData>,
        IAmStartedByMessages<Kickoff>,
        IHandleMessages<CompleteASaga>,
        IHandleTimeouts<ATimeout>
    {
        public Task Handle(Kickoff message, IMessageHandlerContext context)
        {
            RandomFailure.MaybeItFails();

            return RequestTimeout<ATimeout>(context, TimeSpan.FromSeconds(10));
        }

        public Task Handle(CompleteASaga message, IMessageHandlerContext context)
        {
            RandomFailure.MaybeItFails();
            MarkAsComplete();
            return context.Publish<ASagaCompleted>(e => e.SomeId = Data.SomeId);
        }

        public Task Timeout(ATimeout state, IMessageHandlerContext context)
        {
            RandomFailure.MaybeItFails();

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
