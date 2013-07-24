namespace LinkMe.Framework.Utility.Results
{
    public class CountResult<T>
    {
        private readonly T _item;
        private readonly int _count;

        public CountResult(T item, int count)
        {
            _item = item;
            _count = count;
        }

        public T Item
        {
            get { return _item; }
        }

        public int Count
        {
            get { return _count; }
        }
    }
}
