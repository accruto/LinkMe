namespace LinkMe.Framework.Utility.Validation
{
    public class HtmlValidationError
        : ValidationError
    {
        public HtmlValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }

        public override string Message
        {
            get { return string.Format("The {0} appears to have some script in it.", Name); }
        }
    }

    public class HtmlValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (!(value is string || value is string[]))
                return true;

            var s = value as string;
            if (s != null)
            {
                if (s.Length != 0 && HtmlUtil.ContainsScript(s))
                    return false;
            }
            else
            {
                var a = value as string[];
                foreach (var v in a)
                {
                    if (v.Length != 0 && HtmlUtil.ContainsScript(v))
                        return false;
                }
            }

            return true;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new ValidationError[] { new HtmlValidationError(name) };
        }
    }

    public class HtmlAttribute
        : ValidationAttribute
    {
        public HtmlAttribute()
            : base(new HtmlValidator())
        {
        }
    }
}
