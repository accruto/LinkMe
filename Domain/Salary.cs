using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain
{
    public class PrepareSalaryAttribute
        : InstancePreparationAttribute
    {
        public override void PrepareValue(object instance)
        {
            if (!(instance is Salary))
                return;

            var salary = (Salary)instance;
            if (salary.UpperBound != null)
            {
                if (salary.UpperBound < salary.LowerBound)
                {
                    if (salary.UpperBound == 0)
                    {
                        salary.UpperBound = null;
                    }
                    else
                    {
                        // Just swap the two values.

                        var temp = salary.LowerBound;
                        salary.LowerBound = salary.UpperBound;
                        salary.UpperBound = temp;
                    }
                }
            }
        }
    }

    public class SalaryValidationError
        : ValidationError
    {
        public SalaryValidationError(string name)
            : base(name)
        {
        }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { new ValidationErrorName(Name) };
        }
    }

    public class SalaryValidator
        : Validator
    {
        protected override bool IsValid(object value)
        {
            if (!(value is Salary))
                return true;

            var salary = (Salary)value;

            if (salary.LowerBound != null && salary.LowerBound < 0)
                return false;

            if (salary.UpperBound != null && salary.UpperBound < 0)
                return false;

            if (salary.Currency == null && (salary.LowerBound != null || salary.UpperBound != null))
                return false;

            return true;
        }

        protected override ValidationError[] GetValidationErrors(string name)
        {
            return new[] { new SalaryValidationError(name) };
        }
    }

    public class SalaryAttribute
        : ValidationAttribute
    {
        public SalaryAttribute()
            : base(new SalaryValidator())
        {
        }
    }

    [PrepareSalary]
    public sealed class Salary
        : ICloneable
    {
        // Need to round to some reasonable precision, otherwise comparisons always fail after a conversion.
        // The exact value is stored for conversion purposes, but accessors return values rounded to this
        // precision.
        private const decimal YearToYear = 1;
        private const decimal MonthToYear = 12;
        private const decimal WeekToYear = 52;
        private const decimal DayToYear = 250;
        private const decimal HourToYear = 2000;
        private static readonly decimal[] ConversionsToYear = new[] { 0, YearToYear, MonthToYear, WeekToYear, DayToYear, HourToYear };

        public decimal? LowerBound { get; set; }
        public decimal? UpperBound { get; set; }
        public Currency Currency { get; set; }
        public SalaryRate Rate { get; set; }

        public bool HasLowerBound
        {
            get { return LowerBound != null && LowerBound != 0; }
        }

        public bool HasUpperBound
        {
            get { return UpperBound != null; }
        }

        public bool IsEmpty
        {
            get { return Rate == SalaryRate.None || (!HasLowerBound && !HasUpperBound); }
        }

        public bool IsZero
        {
            get
            {
                if (IsEmpty)
                    return false;
                if (LowerBound == 0)
                    return UpperBound == 0 || !HasUpperBound;
                if (UpperBound == 0)
                    return !HasLowerBound;
                return false;
            }
        }

        public static bool Equals(Salary a, Salary b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a == null || b == null)
                return false;

            return a.LowerBound == b.LowerBound && a.UpperBound == b.UpperBound && Equals(a.Currency, b.Currency) && a.Rate == b.Rate;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Salary;
            return other == null ? false : Equals(this, other);
        }

        public override int GetHashCode()
        {
            return LowerBound.GetHashCode() ^ UpperBound.GetHashCode() ^ (Currency == null ? 0 : Currency.GetHashCode()) ^ Rate.GetHashCode();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public Salary Clone()
        {
            return new Salary
            {
                LowerBound = LowerBound,
                UpperBound = UpperBound,
                Rate = Rate,
                Currency = Currency
            };
        }

        public Salary ToRate(SalaryRate newRate)
        {
            if (Rate == SalaryRate.None || newRate == Rate)
                return new Salary {LowerBound = LowerBound, UpperBound = UpperBound, Currency = Currency, Rate = Rate};

            if (newRate == SalaryRate.None || !Enum.IsDefined(typeof(SalaryRate), Rate))
                throw new ArgumentException("Invalid salary rate: " + newRate, "newRate");
            
            var conversionRate = ConversionsToYear[(int)Rate] / ConversionsToYear[(int)newRate];
            var newLower = LowerBound == null ? null : LowerBound * conversionRate;
            var newUpper = UpperBound == null ? null : UpperBound * conversionRate;
            return new Salary { LowerBound = newLower, UpperBound = newUpper, Currency = Currency, Rate = newRate };
        }
    }
}
