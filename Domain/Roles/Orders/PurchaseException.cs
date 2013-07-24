using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Roles.Orders
{
    public abstract class PurchaseUserException
        : UserException
    {
        protected PurchaseUserException(string message)
            : base(message)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { Message };
        }
    }

    public class PurchaseSystemException
        : Exception
    {
        public PurchaseSystemException(Exception innerException)
            : base("The order cannot be processed.", innerException)
        {
        }

        public PurchaseSystemException(string message)
            : base(message)
        {
        }
    }
}