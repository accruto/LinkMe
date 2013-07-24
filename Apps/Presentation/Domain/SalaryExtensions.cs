using System;
using LinkMe.Domain;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class SalaryExtensions
    {
        public static Salary Parse(string lowerBoundText, string upperBoundText, SalaryRate type, bool truncateCents)
        {
            // When parsing treat "0" as "unknown". When using the constructor, however, user code can
            // specify 0 as valid values.

            var lowerBound = ParseSalaryPart(lowerBoundText, "minimum");
            var upperBound = ParseSalaryPart(upperBoundText, "maximum");

            if (lowerBound == null && upperBound == null)
                return null;

            if (truncateCents)
            {
                lowerBound = lowerBound == null ? lowerBound : decimal.Floor(lowerBound.Value);
                upperBound = upperBound == null ? upperBound : decimal.Floor(upperBound.Value);
            }

            // If the user swapped around the minimum and maximum salary just accept it and correct it.

            if (upperBound != null && lowerBound > upperBound)
                return new Salary { UpperBound = upperBound, LowerBound = lowerBound, Rate = type, Currency = Currency.AUD};

            return new Salary {LowerBound = lowerBound, UpperBound = upperBound, Rate = type, Currency = Currency.AUD};
        }

        public static string GetJobAdDisplayText(this Salary salary)
        {
            if (salary == null || salary.IsEmpty || salary.IsZero)
                return "Not specified";

            if (salary.Rate == SalaryRate.Hour)
                return GetDisplayText(salary) + " p.h. (Approximately " + GetDisplayText(salary.ToRate(SalaryRate.Year)) + " p.a.)";
            
            return GetDisplayText(salary) + " p.a.";
        }

        public static string GetDisplayText(this Salary salary)
        {
            if (salary == null)
                return string.Empty;

            // Formats:
            // - Both upper and lower bound: "$nn,nnn to $nn,nnn"
            // - Upper bound only: "$nn,nnn"
            // - Lower bound only: "$nn,nnn+"
            // - Neither (Empty): Empty string

            if (salary.Rate == SalaryRate.None)
                return string.Empty;

            if (salary.HasLowerBound)
            {
                if (salary.HasUpperBound)
                {
                    return salary.UpperBound == salary.LowerBound
                        ? string.Format("{0}", GetDisplayText(salary, salary.UpperBound.Value))
                        : string.Format("{0} to {1}", GetDisplayText(salary, salary.LowerBound.Value), GetDisplayText(salary, salary.UpperBound.Value));
                }

                return string.Format("{0}+", GetDisplayText(salary, salary.LowerBound.Value));
            }

            return salary.HasUpperBound
                ? string.Format("{0}", GetDisplayText(salary, salary.UpperBound.Value))
                : string.Empty;
        }

        private static decimal? ParseSalaryPart(string text, string description)
        {
            if (text == null)
                return null;

            text = text.Replace(",", "").Trim().Trim('$');

            if (text.Length == 0)
                return null;

            decimal multiplier = 1;

            char lastChar = text[text.Length - 1];
            if (lastChar == 'k' || lastChar == 'K')
            {
                text = text.Substring(0, text.Length - 1);
                multiplier = 1000;
            }

            decimal salary;
            try
            {
                salary = decimal.Parse(text) * multiplier;
            }
            catch (FormatException ex)
            {
                throw new UserException(String.Format("The {0} salary, '{1}',"
                    + " is not a valid number.", description, HtmlUtil.StrippedIfContainsHtml(text)), ex);
            }
            catch (OverflowException ex)
            {
                throw new UserException(String.Format("The {0} salary, '{1}',"
                    + " is too large.", description, text), ex);
            }

            if (salary < 0)
            {
                throw new UserException(String.Format("The {0} salary, '{1}',"
                    + " is a negative number.", description, text));
            }

            return salary;
        }

        private static string GetDisplayText(Salary salary, decimal value)
        {
            return Math.Round(value, salary.Currency.CultureInfo.NumberFormat.CurrencyDecimalDigits).ToString("C0", salary.Currency.CultureInfo);
        }
    }
}
