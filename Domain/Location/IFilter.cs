namespace LinkMe.Domain.Location
{
    public interface IFilter<T>
    {
        bool Match(T t);
    }

    internal class EmptyFilter<T>
        : IFilter<T>
    {
        private EmptyFilter()
        {
        }

        public bool Match(T t)
        {
            return true;
        }

        public static IFilter<T> Instance
        {
            get { return instance; }
        }

        private static readonly EmptyFilter<T> instance = new EmptyFilter<T>();
    }
}
