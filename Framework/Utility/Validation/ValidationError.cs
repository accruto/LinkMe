namespace LinkMe.Framework.Utility.Validation
{
    public abstract class ValidationError
    {
        private readonly string _name;

        protected ValidationError(string name)
        {
            _name = name;
        }

        protected ValidationError()
        {
        }

        public string Name
        {
            get { return _name; }
        }

        public virtual string Message
        {
            get { return "A '" + GetType().FullName + "' error has occurred" + (string.IsNullOrEmpty(_name) ? "." : " for the " + _name + " property."); }
        }

        public virtual object[] GetErrorMessageParameters()
        {
            return new object[0];
        }

        public override string ToString()
        {
            return Message;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return Equals(obj as ValidationError);
        }

        public bool Equals(ValidationError obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return Equals(obj._name, _name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_name != null ? _name.GetHashCode() : 0)*397;
            }
        }
    }
}
