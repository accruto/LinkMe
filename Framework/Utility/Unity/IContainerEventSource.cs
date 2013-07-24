namespace LinkMe.Framework.Utility.Unity
{
    public interface IContainerEventSource
    {
        void RaiseError(string method, string message);
    }
}
