using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

namespace LinkMe.Framework.Utility.Expressions
{
    /// <summary>
    /// Represents an expression consisting of two or more terms with joined by a commutative binary operator,
    /// ie. "term op term [op term [op term...]]"
    /// </summary>
    [Serializable]
    [DebuggerDisplay(Expression.DebuggerDisplayValue)]
    public class CommutativeExpression
        : IExpression
    {
        private readonly BinaryOperator _op;
        private readonly IExpression[] _terms;

        public CommutativeExpression(BinaryOperator op, ICollection<IExpression> terms)
        {
            if (terms == null)
                throw new ArgumentNullException("terms");
            if (terms.Count < 2)
                throw new ArgumentException("A commutative expression must have at least 2 terms.", "terms");

            if (op != BinaryOperator.And && op != BinaryOperator.Or)
            {
                throw new ArgumentException("Operator '" + Expression.OperatorToString(op)
                                            + " is invalid for a CommutativeExpression. Only AND and OR are supported.", "op");
            }

            _op = op;
            _terms = new IExpression[terms.Count];
            terms.CopyTo(_terms, 0);
        }

        #region IExpression Members

        public string GetRawExpression()
        {
            var sb = new StringBuilder("(");

            sb.Append(_terms[0].GetRawExpression());
            for (int i = 1; i < _terms.Length; i++)
            {
                sb.AppendFormat(" {0} {1}", Expression.OperatorToString(_op), _terms[i].GetRawExpression());
            }
            sb.Append(")");

            return sb.ToString();
        }

        public string GetUserExpression()
        {
            return GetUserExpression(Expression.DefaultBinaryOperator);
        }

        public string GetUserExpression(BinaryOperator defaultOperator)
        {
            var sb = new StringBuilder("");

            AppendUserExpression(sb, _terms[0], true);

            // Don't append the operator if it's the default one.
            string separator = (_op == defaultOperator ? " " : " " + Expression.OperatorToString(_op) + " ");

            for (int i = 1; i < _terms.Length; i++)
            {
                sb.Append(separator);
                AppendUserExpression(sb, _terms[i], false);
            }

            return sb.ToString();
        }

        public string[] GetUniqueLiterals(bool originalOnly)
        {
            // Use a linked list dictionary to maintain the orde.
            var dictionary = new ListDictionary(CaseInsensitiveComparer.Default);

            AddUniqueTerms(dictionary, this, originalOnly);

            var array = new string[dictionary.Count];
            dictionary.Keys.CopyTo(array, 0);

            return array;
        }

        public bool IsMatch(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (text.Length == 0)
                return false;

            switch (_op)
            {
                case BinaryOperator.And:
                    foreach (IExpression term in _terms)
                    {
                        if (!term.IsMatch(text))
                            return false;
                    }

                    return true;

                case BinaryOperator.Or:
                    foreach (IExpression term in _terms)
                    {
                        if (term.IsMatch(text))
                            return true;
                    }

                    return false;

                default:
                    throw new ApplicationException("Unexpected operator: " + _op);
            }
        }

        public bool Equals(IExpression obj, bool ignoreCase)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as CommutativeExpression;
            if (other == null)
                return false;

            if (Operator != other.Operator)
                return false;

            return _terms.NullableCollectionEqual(other._terms, ExpressionsEqualCaseInsensitive);
        }

        #endregion

        public BinaryOperator Operator
        {
            get { return _op; }
        }

        public IList<IExpression> Terms
        {
            get { return _terms; }
        }

        #region Static methods

        private static bool ExpressionsEqualCaseInsensitive(IExpression a, IExpression b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a == null || b == null)
                return false;

            return a.Equals(b, true);
        }

        private static void AddUniqueTerms(IDictionary terms, IExpression expression, bool originalOnly)
        {
            var commutative = expression as CommutativeExpression;
            if (commutative != null)
            {
                foreach (IExpression operand in commutative._terms)
                {
                    AddUniqueTerms(terms, operand, originalOnly);
                }
            }
            else
            {
                foreach (string term in expression.GetUniqueLiterals(originalOnly))
                {
                    terms[term] = null;
                }
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            return Equals(obj as IExpression, true);
        }

        public override int GetHashCode()
        {
            return _op.GetHashCode() ^ _terms.GetCollectionHashCode();
        }

        internal IExpression Simplify()
        {
            // If any terms are also CommutativeExpressions with the same operator - inline them, eg.
            // "one AND (two AND three)" becomes "one AND two AND three".

            var newTerms = new List<IExpression>();

            foreach (IExpression term in _terms)
            {
                var commutative = term as CommutativeExpression;
                if (commutative != null && commutative.Operator == Operator)
                {
                    Debug.Assert(commutative._terms.Length > 1, "commutative.terms.Length > 1");
                    newTerms.AddRange(commutative._terms);
                }
                else
                {
                    newTerms.Add(term);
                }
            }

            return (newTerms.Count == _terms.Length ? this : new CommutativeExpression(_op, newTerms));
        }

        private void AppendUserExpression(StringBuilder sb, IExpression expression, bool isFirstTermInExpression)
        {
            CommutativeExpression commutative;

            if (expression is LiteralTerm || expression is ModifierExpression)
            {
                sb.Append(expression.GetUserExpression());
            }
            else if ((commutative = (expression as CommutativeExpression)) != null)
            {
                // A sub-expression - enclose it in bracekts only if its operator has lower precedence than
                // ours.

                if (Expression.CompareOperatorPrecedence(commutative._op, _op) < 0)
                {
                    sb.AppendFormat("({0})", expression.GetUserExpression());
                }
                else
                {
                    sb.Append(expression.GetUserExpression());
                }
            }
            else if (expression is UnaryExpression)
            {
                // For an AND NOT append the "AND", unless it's the first term. The syntax "term NOT term" is not supported.

                if (isFirstTermInExpression)
                {
                    sb.Append(expression.GetUserExpression());
                }
                else
                {
                    sb.AppendFormat("{0} {1}", Expression.OperatorToString(_op), expression.GetUserExpression());
                }
            }
            else
            {
                Debug.Fail("Unexpected type of sub-expression: " + expression.GetType().FullName);
                sb.AppendFormat("({0})", expression.GetUserExpression());
            }
        }
    }
}