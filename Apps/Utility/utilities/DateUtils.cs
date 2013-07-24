using System;
using LinkMe.Domain;

namespace LinkMe.Utility.Utilities
{
	public static class DateUtils
	{
        public const string CurrentDateText = "current";

		/// <summary>
		/// Parse the specified date from a string, but if it is in the format "MMM xx" where xx is less than
		/// or equal to 31 (eg. "Jan 04") then assume xx refers to the year. The System.DateTime.Parse() method
		/// assumes it's a day in this case, so "Jan 04" would get parsed to "fourth of January of the current
		/// year". This function will parse it to "first of January 2004" instead.
		/// </summary>
		public static DateTime? ParseMonthAndYear(string date)
		{
            const int centuryPivotYear = 29;

			if (date == null)
				return null;

			date = date.Trim().Replace("\'", "");
			if (date.Length == 0)
				return null;

			int digitCount = 0;
			int index = date.Length - 1;

			while (index >= 0)
			{
				if (char.IsDigit(date[index]))
				{
					if (++digitCount > 2)
						break;
				}
				else if (digitCount == 2)
				{
					// This is the special case - two digits at the end of the date. Interpret is a year
					// between 1930 and 2029.

					int year = int.Parse(date.Substring(index + 1, 2));
					date = date.Insert(index + 1, (year > centuryPivotYear ? "19" : "20"));
					break;
				}
				else
					break;

				index--;
			}

			// Is it just a 2 or 4 digit number? Another special case that DateTime.Parse() doesn't handle.
			if (StringUtils.IsDigits(date))
			{
                if (date.Length == 4)
                {
                    // Case 3995 - make sure the year is valid, otherwise new DateTime() may throw.

                    int year = int.Parse(date);
                    if (year <= 1900 + centuryPivotYear || year > 2000 + centuryPivotYear)
                        return null;

                    return new DateTime(int.Parse(date), 1, 1);
                }
                else if (date.Length == 2)
                {
                    int year = int.Parse(date);
                    year += (year > centuryPivotYear ? 1900 : 2000);
                    return new DateTime(year, 1, 1);
                }
			}

            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            else
                return null;
		}

		public static bool IsCurrent(string date)
		{
			return (string.Compare(date, "present", true) == 0
                || string.Compare(date, CurrentDateText, true) == 0
				|| string.Compare(date, "now", true) == 0);
		}
	}
}