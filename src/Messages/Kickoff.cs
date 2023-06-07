namespace Messages
{
    public class Kickoff : ICommand
    {
        public string SomeId { get; set; } = Guid.NewGuid().ToString();
    }
}