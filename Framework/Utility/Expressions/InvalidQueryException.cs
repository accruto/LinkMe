namespace LinkMe.Framework.Utility.Expressions
{
    public class InvalidQueryException
        : ExpressionException
    {
        public InvalidQueryException(string message)
            : base(message)
        {
        }
    }

    public class NotQueryNotSupportedException
        : InvalidQueryException
    {
        public NotQueryNotSupportedException()
            : base("The entire expression is a \"NOT\" expression, which is not supported.")
        {
        }
    }
}