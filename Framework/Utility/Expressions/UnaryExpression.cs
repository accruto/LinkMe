using System;
using System.Collections;
using System.Diagnostics;

namespace LinkMe.Framework.Utility.Expressions
{
    /// <summary>
    /// Represents an expression consisting of a unary operator and a single term, ie. "op term".
    /// </summary>
    [Serializable]
    [DebuggerDisplay(Expression.DebuggerDisplayValue)]
    public class UnaryExpression
        : IExpression
    {
        private readonly UnaryOperator _op;
        private readonly IExpression _term;

        public UnaryExpression(UnaryOperator op, IExpression term)
        {
            if (term == null)
                throw new ArgumentNullException("term");
            if (op != UnaryOperator.Not)
                throw new ArgumentException("Operator '" + Expression.OperatorToString(op) + " is invalid for a UnaryExpression. Only NOT is supported.", "op");

            _op = op;
            _term = term;
        }

        #region IExpression Members

        public string GetRawExpression()
        {
            return string.Format("({0} {1})", Expression.OperatorToString(_op), _term.GetRawExpression());
        }

        public string GetUserExpression()
        {
            return GetUserExpression(Expression.DefaultBinaryOperator);
        }

        public string GetUserExpression(BinaryOperator defaultOperator)
        {
            // No brackets for literals, since NOT binds the tightest.

            string format = (_term is LiteralTerm ? "{0} {1}" : "{0} ({1})");
            return string.Format(format, Expression.OperatorToString(_op), _term.GetUserExpression(defaultOperator));
        }

        public string[] GetUniqueLiterals(bool originalOnly)
        {
            return _op == UnaryOperator.Not ? new string[0] : _term.GetUniqueLiterals(originalOnly);
        }

        public bool IsMatch(string text)
        {
            switch (_op)
            {
                case UnaryOperator.Not:
                    return !_term.IsMatch(text);

                default:
                    throw new ApplicationException("Unexpected operator: " + _op);
            }
        }

        public bool Equals(IExpression obj, bool ignoreCase)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as UnaryExpression;
            if (other == null)
                return false;

            return (Operator == other.Operator && Term.Equals(other.Term, ignoreCase));
        }

        #endregion

        public UnaryOperator Operator
        {
            get { return _op; }
        }

        public IExpression Term
        {
            get { return _term; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IExpression, true);
        }

        public override int GetHashCode()
        {
            return _op.GetHashCode() ^ _term.GetHashCode();
        }

        private IExpression GetNewExpression(IExpression newTerm)
        {
            if (newTerm == null)
                return null;
            return newTerm.Equals(_term, false) ? this : new UnaryExpression(_op, newTerm);
        }
    }
}