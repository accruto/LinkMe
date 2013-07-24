using System;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Products
{
    public enum CreditCardType
    {
        MasterCard = 0,
        Visa = 1,
        Amex = 2,
    }

    public struct ExpiryDate
    {
        private const string Pattern = "^(0[1-9]|10|11|12)/([0-9]{2})$";
        private static readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);

        private readonly DateTime _value;
        private static readonly ExpiryDate _minValue = new ExpiryDate(DateTime.MinValue);

        public static ExpiryDate MinValue
        {
            get { return _minValue; }
        }

        public ExpiryDate(int year, int month)
        {
            _value = new DateTime(year, month, 1);
        }

        public ExpiryDate(DateTime value)
        {
            // Only take the bits you need.

            _value = new DateTime(value.Year, value.Month, 1);
        }

        public int Month
        {
            get { return _value.Month; }
        }

        public int Year
        {
            get { return _value.Year; }
        }

        public static ExpiryDate Parse(string value)
        {
            var match = _regex.Match(value);
            if (!match.Success)
                throw new ArgumentException("The value '" + value + "' is not a valid format.");

            // Pick out the pieces.

            var month = int.Parse(match.Groups[1].Value);
            var year = 2000 + int.Parse(match.Groups[2].Value);
            return new ExpiryDate(new DateTime(year, month, 1));
        }

        public override string ToString()
        {
            return _value.ToString("MM/yy");
        }

        public static bool operator ==(ExpiryDate e1, ExpiryDate e2)
        {
            return e1.Equals(e2);
        }

        public static bool operator !=(ExpiryDate e1, ExpiryDate e2)
        {
            return !e1.Equals(e2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ExpiryDate))
                return false;
            return Equals((ExpiryDate)obj);
        }

        public bool Equals(ExpiryDate other)
        {
            return other._value.Equals(_value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public class CreditCard
    {
        [EnumValue]
        public CreditCardType CardType { get; set; }
        [Required, StringLength(100)]
        public string CardHolderName { get; set; }
        [Required, CreditCardNumber]
        public string CardNumber { get; set; }
        [Required, Cvv]
        public string Cvv { get; set; }
        [Required]
        public ExpiryDate ExpiryDate { get; set; }
    }

    public class CreditCardSummary
    {
        public string Pan { get; set; }
        public CreditCardType Type { get; set; }
    }

    public static class CreditCardExtensions
    {
        public static string GetCreditCardPan(this string creditCardNumber)
        {
            if (!RegularExpressions.CompleteCreditCardNumber.IsMatch(creditCardNumber))
                throw new ArgumentException("The credit card number is not valid.", "creditCardNumber");
            return creditCardNumber.Substring(0, 6) + "..." + creditCardNumber.Substring(creditCardNumber.Length - 3, 3);
        }
    }
}
