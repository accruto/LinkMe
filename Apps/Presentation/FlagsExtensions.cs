using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Presentation
{
    public static class FlagsExtensions
    {
        public static IEnumerable<string> GetDisplayTexts<TEnum>(this TEnum value, IEnumerable<TEnum> flags, Func<TEnum, string> getText)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The template argument T must be an enum.");
            if (flags == null)
                throw new ArgumentNullException("flags");

            var intValue = value.ToInt32(null);
            foreach (var flag in flags)
            {
                var enumValue = flag.ToInt32(null);
                if ((intValue & enumValue) == enumValue)
                    yield return getText(flag);
            }
        }
    }
}
