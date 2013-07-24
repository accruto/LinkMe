using System;
using System.Collections;
using System.Diagnostics;

namespace LinkMe.Framework.Utility.Expressions
{
    /// <summary>
    /// An IExpression prefixed with a modifier. Currently the only modifier supported is "=", meaning
    /// "do not use synonyms".
    /// </summary>
    [Serializable]
    [DebuggerDisplay(Expressions.Expression.DebuggerDisplayValue)]
    public class ModifierExpression
        : IExpression
    {
        private readonly IExpression _expression;
        private readonly ModificationFlags _flags;

        public ModifierExpression(IExpression expression, ModificationFlags flags)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (flags == ModificationFlags.None)
                throw new ArgumentException("The flags value must not be 'None'.", "flags");

            _expression = expression;
            _flags = flags;
        }

        #region IExpression Members

        public string GetRawExpression()
        {
            return _expression.GetRawExpression();
        }

        public string GetUserExpression()
        {
            return GetUserExpression(Expressions.Expression.DefaultBinaryOperator);
        }

        public string GetUserExpression(BinaryOperator defaultOperator)
        {
            return _expression.GetUserExpression(defaultOperator);
        }

        public string[] GetUniqueLiterals(bool originalOnly)
        {
            return _expression.GetUniqueLiterals(originalOnly);
        }

        public bool IsMatch(string text)
        {
            return _expression.IsMatch(text);
        }

        public bool Equals(IExpression obj, bool ignoreCase)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as ModifierExpression;
            if (other == null)
                return false;

            return (Flags == other.Flags && Expression.Equals(other.Expression, ignoreCase));
        }

        #endregion

        public IExpression Expression
        {
            get { return _expression; }
        }

        public ModificationFlags Flags
        {
            get { return _flags; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IExpression, true);
        }

        public override int GetHashCode()
        {
            return _flags.GetHashCode() ^ _expression.GetHashCode();
        }

        private IExpression GetNewExpression(IExpression newExpression)
        {
            if (newExpression == null)
                return null;
            return newExpression.Equals(_expression, false)
                ? this
                : new ModifierExpression(newExpression, _flags);
        }
    }
}