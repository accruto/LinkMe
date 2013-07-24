using System;
using System.Globalization;
using LinkMe.Common;
using LinkMe.Domain;

namespace LinkMe.TaskRunner.Tasks.MyCareer
{
    public class SalaryParser
    {
        private static readonly char[] _separators = new[] { ' ', '-', '+', '/' };

        public bool TryParse(string text, out Salary salary)
        {
            salary = null;

            string[] textParts = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            if (textParts.Length == 0)
            {
                salary = new Salary();
                return true;
            }

            // Parse minimum salary.

            decimal salaryMin;
            if (!decimal.TryParse(textParts[0], NumberStyles.Currency, null, out salaryMin))
                return false;

            if (textParts.Length == 1)
            {
                salary = new Salary {LowerBound = salaryMin, Rate = SalaryRate.Year, Currency = Currency.AUD};
                return true;
            }

            if (textParts[1] == "hr")
            {
                salary = new Salary {LowerBound = salaryMin, Rate = SalaryRate.Hour, Currency = Currency.AUD};
                return true;
            }

            // Parse maximum salary.

            decimal salaryMax;
            if (!decimal.TryParse(textParts[1], NumberStyles.Currency, null, out salaryMax))
            {
                salary = new Salary { LowerBound = salaryMin, Rate = SalaryRate.Year, Currency = Currency.AUD };
                return true;
            }

            SalaryRate rateType = (textParts.Length > 2 && textParts[2] == "hr")
                ? SalaryRate.Hour
                : SalaryRate.Year;

            salary = new Salary { LowerBound = salaryMin, UpperBound = salaryMax, Rate = rateType, Currency = Currency.AUD };
            return true;
        }
    }
}
