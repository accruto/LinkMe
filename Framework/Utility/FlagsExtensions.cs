using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Utility
{
    public static class FlagsExtensions
    {
        public static bool IsFlagSet<TEnum>(this TEnum values, TEnum flag)
            where TEnum : struct, IConvertible
        {
            var iValues = values.ToInt32(null);
            var iFlag = flag.ToInt32(null);
            return (iValues & iFlag) == iFlag;
        }

        public static TEnum SetFlag<TEnum>(this TEnum values, TEnum flag, bool value)
            where TEnum : struct, IConvertible
        {
            return value ? SetFlag(values, flag) : ResetFlag(values, flag);
        }

        public static TEnum SetFlag<TEnum>(this TEnum values, TEnum flag)
            where TEnum : struct, IConvertible
        {
            var iValues = values.ToInt32(null);
            var iFlag = flag.ToInt32(null);
            return (TEnum)Enum.ToObject(typeof(TEnum), iValues | iFlag);
        }

        public static TEnum ResetFlag<TEnum>(this TEnum values, TEnum flag)
            where TEnum : struct, IConvertible
        {
            var iValues = values.ToInt32(null);
            var iFlag = flag.ToInt32(null);
            return (TEnum)Enum.ToObject(typeof(TEnum), iValues & ~iFlag);
        }

        public static IList<TEnum> GetFlagged<TEnum>(this TEnum values, params TEnum[] flagValues)
            where TEnum : struct, IConvertible
        {
            var iValues = values.ToInt32(null);
            return (from f in flagValues
                    let i = f.ToInt32(null)
                    where (iValues & i) == i
                    select f).ToList();
        }
    }
}
