using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Expressions
{
    public abstract class ExpressionException
        : UserException
    {
        protected ExpressionException()
        {
        }

        protected ExpressionException(string message)
            : base(message)
        {
        }
    }
}
