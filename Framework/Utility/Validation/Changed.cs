namespace LinkMe.Framework.Utility.Validation
{
    public class ChangedValidationError
        : ValidationError
    {
        private readonly string _from;
        private readonly string _to;

        public ChangedValidationError(string name, string from, string to)
            : base(name)
        {
            _from = from;
            _to = to;
        }

        public string From
        {
            get { return _from; }
        }

        public string To
        {
            get { return _to; }
        }
    }
}
