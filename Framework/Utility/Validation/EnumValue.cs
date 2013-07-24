using System;

namespace LinkMe.Framework.Utility.Validation
{
    public class EnumValueValidationError
        : ValidationError
    {
        public EnumValueValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class EnumValueValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var type = value.GetType();
            var values = Enum.GetValues(type);

            for (var index = 0; index < values.Length; ++index)
            {
                if (Equals(value, values.GetValue(index)))
                    return true;
            }

            return false;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new EnumValueValidationError(name) };
        }
    }

    public class FlagsEnumValueValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var type = value.GetType();
            var values = Enum.GetValues(type);

            var ivalue = 0;
            for (var index = 0; index < values.Length; ++index)
                ivalue |= (int)values.GetValue(index);

            return ((int)value & ivalue) != 0;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new EnumValueValidationError(name) };
        }
    }

    public class EnumValueAttribute
        : ValidationAttribute
    {
        public EnumValueAttribute()
            : base(new EnumValueValidator())
        {
        }
    }

    public class FlagsEnumValueAttribute
        : ValidationAttribute
    {
        public FlagsEnumValueAttribute()
            : base(new FlagsEnumValueValidator())
        {
        }
    }
}
