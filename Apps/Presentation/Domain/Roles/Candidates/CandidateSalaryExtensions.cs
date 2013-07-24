using LinkMe.Domain;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public static class CandidateSalaryExtensions
    {
        public static int? GetEffectiveLowerBound(this Salary desiredSalary)
        {
            if (desiredSalary == null || desiredSalary.Rate == SalaryRate.None || (desiredSalary.LowerBound == null && desiredSalary.UpperBound == null))
                return null;

            return desiredSalary.Rate == SalaryRate.Hour
                ? GetHourlyEffectiveLowerBound(desiredSalary)
                : GetYearlyEffectiveLowerBound(desiredSalary);
        }

        public static string GetSalaryBandDisplayText(this Salary salary)
        {
            if (!salary.LowerBound.HasValue && !salary.UpperBound.HasValue)
                return string.Empty;

            if (!salary.LowerBound.HasValue || salary.LowerBound.Value == 0)
                return "up to " + ConvertBandForViewing(salary.UpperBound.Value);

            if (!salary.UpperBound.HasValue || salary.UpperBound.Value == 0)
                return ConvertBandForViewing(salary.LowerBound.Value) + " and above";

            return ConvertBandForViewing(salary.LowerBound.Value) + "-" + ConvertBandForViewing(salary.UpperBound.Value);
        }

        private static string ConvertBandForViewing(decimal value)
        {
            return string.Format("{0:C0}", value);
        }

        private static int GetHourlyEffectiveLowerBound(Salary salary)
        {
            return salary.GetEffectiveLowerBound(Constants.SalaryHourlyIncrement, Constants.SalaryHourlyMaximumMinimum);
        }

        private static int GetYearlyEffectiveLowerBound(Salary salary)
        {
            // Convert to yearly rate for calculation.

            var yearlyEffectiveLowerBound = salary.ToRate(SalaryRate.Year).GetEffectiveLowerBound(Constants.SalaryYearlyIncrement, Constants.SalaryYearlyMaximumMinimum);

            // Convert back to original rate.

            return (int)new Salary { LowerBound = yearlyEffectiveLowerBound, Rate = SalaryRate.Year, Currency = salary.Currency }.ToRate(salary.Rate).LowerBound.Value;
        }

        private static int GetEffectiveLowerBound(this Salary salary, int increment, int maximumMinimum)
        {
            if (salary.LowerBound != null)
                return GetEffectiveLowerBound((int)salary.LowerBound.Value, increment, maximumMinimum);

            // Convert the upper bound to lower bound.

            var value = (int)(Constants.SalaryUpperToLowerConversion * salary.UpperBound.Value);
            if (value == 0 && (int)salary.UpperBound.Value != 0)
                value = increment;
            return GetEffectiveLowerBound(value, increment, maximumMinimum);
        }

        private static int GetEffectiveLowerBound(int value, int increment, int maximumMinimum)
        {
            if (value == 0)
                return 0;
            if (value < increment)
                return increment;
            if (value > maximumMinimum)
                return maximumMinimum;
            return (value / increment) * increment;
        }
    }
}
