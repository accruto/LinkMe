using System;
using System.Diagnostics;
using System.Globalization;

namespace LinkMe.Framework.Utility.Expressions
{
    [Serializable]
    [DebuggerDisplay(Expression.DebuggerDisplayValue)]
    public class LiteralTerm
        : IExpression
    {
        /// <summary>
        /// The quote character used in this query representation, ie. specified by the user.
        /// </summary>
        public const char Quote = '\"';

        private readonly string _value;
        private readonly float _weight;

        public LiteralTerm(string value)
            : this(value, 1.0f)
        {
        }

        public LiteralTerm(string value, float weight)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("The value of a literal token cannot be null or empty string.");

            if (value.Trim(Quote).Length == 0)
                throw new ArgumentException("The value of a literal token cannot consist only of quotes.");

            _value = value;
            _weight = weight;
            IsExact = IsEnclosedByQuotes(value);
        }

        #region IExpression Members

        public string GetRawExpression()
        {
            return (IsExact && !IsEnclosedByQuotes(_value) ? Quote + _value + Quote : _value);
        }

        public string GetUserExpression(BinaryOperator defaultOperator)
        {
            return GetUserExpression(); // The default operator makes no difference for this class.
        }

        public string GetUserExpression()
        {
            return GetRawExpression();
        }

        public string[] GetUniqueLiterals(bool originalOnly)
        {
            return new[] { _value };
        }

        public bool IsMatch(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            if (text.Length == 0)
                return false;

            CompareInfo compareInfo = CultureInfo.CurrentCulture.CompareInfo;

            // Find the text inside the literal term value and check that the previous and next characters
            // are word boundaries, ie. characters other than [a-zA-Z_0-9].

            int startIndex = compareInfo.IndexOf(text, _value, CompareOptions.IgnoreCase);
            while (startIndex != -1)
            {
                if (startIndex == 0 || !TextUtil.IsWordChar(text[startIndex - 1]))
                {
                    int nextCharIndex = startIndex + _value.Length;
                    if (nextCharIndex == text.Length || !TextUtil.IsWordChar(text[nextCharIndex]))
                        return true;
                }

                startIndex = compareInfo.IndexOf(text, _value, startIndex + 1, CompareOptions.IgnoreCase);
            }

            return false;
        }

        public bool Equals(IExpression obj, bool ignoreCase)
        {
            var other = obj as LiteralTerm;
            if (other == null)
                return false;

            return (string.Compare(_value, other._value, ignoreCase) == 0 && _weight == other._weight);
        }

        #endregion

        public string Value
        {
            get { return _value; }
        }

        public float Weight
        {
            get { return _weight; }
        }

        internal bool IsExact {get; private set;}

        public override bool Equals(object obj)
        {
            return Equals(obj as IExpression, true);
        }

        public override int GetHashCode()
        {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode(_value);
        }

        private static bool IsEnclosedByQuotes(string value)
        {
            return value.StartsWith(Quote.ToString()) && value.EndsWith(Quote.ToString());
        }
    }
}