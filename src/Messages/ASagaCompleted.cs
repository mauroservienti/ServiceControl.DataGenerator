namespace Messages
{
    public class ASagaCompleted : IEvent
    {
        public string SomeId { get; set; }
    }
}

