using LinkMe.Framework.Host.Exceptions;

namespace LinkMe.Framework.Host.Exceptions
{
    #region AsyncCallException

    [System.Serializable]
    public class AsyncCallException
        : Exception
    {
        protected AsyncCallException()
        {
        }

        public AsyncCallException(System.Type source, string method, System.Exception innerException)
            : base(source, method, innerException)
        {
        }

        protected override PropertyInfo[] PropertyInfos
        {
            get { return new PropertyInfo[0]; }
        }
    }

    #endregion
}
