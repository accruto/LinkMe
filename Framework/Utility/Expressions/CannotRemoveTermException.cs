namespace LinkMe.Framework.Utility.Expressions
{
    public class CannotRemoveTermException
        : ExpressionException
    {
        private readonly IExpression _term;

        internal CannotRemoveTermException(IExpression term, string message)
            : base(message)
        {
            _term = term;
        }

        public IExpression Term
        {
            get { return _term; }
        }
    }
}