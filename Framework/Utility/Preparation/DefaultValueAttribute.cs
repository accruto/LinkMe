namespace LinkMe.Framework.Utility.Preparation
{
    public class DefaultValueAttribute
        : PreparationAttribute
    {
        private readonly int? _intValue;
        private readonly string _stringValue;

        public DefaultValueAttribute(int value)
        {
            _intValue = value;
        }

        public DefaultValueAttribute(string value)
        {
            _stringValue = value;
        }

        public override bool GetValue(object currentValue, out object value)
        {
            value = null;
            if (currentValue is int)
            {
                if (_intValue == null || (int) currentValue != 0)
                    return false;
                value = _intValue.Value;
                return true;
            }

            if (_stringValue == null)
                return false;
            value = _stringValue;
            return true;
        }
    }
}