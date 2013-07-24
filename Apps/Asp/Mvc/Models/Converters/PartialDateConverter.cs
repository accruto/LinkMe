using System;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;

namespace LinkMe.Apps.Asp.Mvc.Models.Converters
{
    public class PartialDateConverter
    {
        private readonly bool _strict;

        public PartialDateConverter(bool strict)
        {
            _strict = strict;
        }

        public void Convert(PartialDate obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public PartialDate? Deconvert(string key, IGetValues values)
        {
            var month = values.GetIntValue(key + "Month");
            var year = values.GetIntValue(key + "Year");
            if (year == null)
                return null;

            return month != null
                ? new PartialDate(year.Value, month.Value)
                : _strict
                    ? (PartialDate?)null
                    : new PartialDate(year.Value);
        }
    }
}