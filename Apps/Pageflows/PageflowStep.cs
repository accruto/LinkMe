namespace LinkMe.Apps.Pageflows
{
    public interface IPageflowStep
    {
        string Name { get; }
        bool CanBeMovedTo { get; }
        bool HasExecuted { get; }
    }

    public class PageflowStep
    {
        public string Name { get; private set; }
        public bool IsEnabled { get; set; }
        public bool IsAvailable { get; set; }
        public bool HasExecuted { get; set; }

        public PageflowStep(string name)
        {
            Name = name;
            IsEnabled = true;
        }
    }

    public class ReadOnlyPageflowStep
        : IPageflowStep
    {
        public string Name { get; internal set; }
        public bool CanBeMovedTo { get; internal set; }
        public bool HasExecuted { get; internal set; }
    }
}
