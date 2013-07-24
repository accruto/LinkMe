namespace LinkMe.Framework.Utility.Validation
{
    public class ValidationErrorName
    {
        private readonly string _name;

        public ValidationErrorName(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
