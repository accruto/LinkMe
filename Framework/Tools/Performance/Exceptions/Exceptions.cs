using System.Runtime.Serialization;
using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Tools.Performance.Exceptions
{
    #region Exception

    public abstract class Exception
        : TypeException
    {
        protected Exception()
        {
        }

        protected Exception(System.Type source, string method, System.Exception innerException)
            : base(source, method, innerException)
        {
        }

        protected Exception(System.Type source, string method)
            : base(source, method)
        {
        }

        protected Exception(string source, string method, System.Exception innerException)
            : base(source, method, innerException)
        {
        }

        protected Exception(string source, string method)
            : base(source, method)
        {
        }

        protected Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected override string GetResourceBaseName()
        {
            return "LinkMe.Framework.Tools.Performance.Exceptions.Exceptions";
        }
    }

    #endregion

    #region StepFailedException

    [System.Serializable]
    public class StepFailedException
        : Exception
    {
        protected StepFailedException()
        {
        }

        public StepFailedException(System.Type source, string method, string profile, string step, int user, System.Exception innerException)
            : base(source, method, innerException)
        {
            Set(profile, step, user);
        }

        public StepFailedException(string source, string method, string profile, string step, int user, System.Exception innerException)
            : base(source, method, innerException)
        {
            Set(profile, step, user);
        }

        public StepFailedException(System.Type source, string method, string profile, string step, int user)
            : base(source, method)
        {
            Set(profile, step, user);
        }

        public StepFailedException(string source, string method, string profile, string step, int user)
            : base(source, method)
        {
            Set(profile, step, user);
        }

        protected StepFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public int User
        {
            get { return (int)GetPropertyValue(Constants.Exceptions.User, -1); }
        }

        protected override PropertyInfo[] PropertyInfos
        {
            get { return _propertyInfos; }
        }

        private void Set(string profile, string step, int user)
        {
            SetPropertyValue(Constants.Exceptions.Profile, profile);
            SetPropertyValue(Constants.Exceptions.Step, step);
            SetPropertyValue(Constants.Exceptions.User, user);
        }

        protected static readonly PropertyInfo[] _propertyInfos = new[]
		{
			new PropertyInfo(Constants.Exceptions.Profile, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.Step, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.User, PrimitiveType.Int32)
		};
    }

    #endregion
}
