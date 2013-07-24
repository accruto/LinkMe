using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Domain;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Presentation.Converters
{
    public interface ISetValues
    {
        void SetValue(string key, string value);
        void SetValue(string key, string[] value);
        void SetValue(string key, bool? value);
        void SetValue(string key, bool value);
        void SetValue(string key, int? value);
        void SetValue(string key, int value);
        void SetValue(string key, decimal? value);
        void SetValue(string key, decimal value);
        void SetValue(string key, Guid? value);
        void SetValue(string key, Guid value);
        void SetValue(string key, Guid[] value);
        void SetValue(string key, DateTime? value);
        void SetValue(string key, PartialDate? value);
        void SetValue<TEnum>(string key, TEnum value) where TEnum : struct;
        void SetValue<TEnum>(string key, TEnum? value) where TEnum : struct;
        void SetFlagsValue<TEnum>(TEnum value) where TEnum : struct, IConvertible;
        void SetFlagsValue<TEnum>(TEnum? value) where TEnum : struct, IConvertible;
        void SetArrayValue<TValue>(string key, IList<TValue> value);
        void SetChildValue<TValue>(string key, TValue value);
    }

    public interface IGetValues
    {
        string GetStringValue(string key);
        bool? GetBooleanValue(string key);
        int? GetIntValue(string key);
        int[] GetIntArrayValue(string key);
        string[] GetStringArrayValue(string key);
        decimal? GetDecimalValue(string key);
        Guid? GetGuidValue(string key);
        Guid[] GetGuidArrayValue(string key);
        DateTime? GetDateTimeValue(string key);
        PartialDate? GetPartialDateValue(string key);
        TEnum? GetValue<TEnum>(string key) where TEnum : struct;
        TEnum? GetFlagsValue<TEnum>() where TEnum : struct, IConvertible;
        IList<TValue> GetArrayValue<TValue>(string key);
        TValue GetChildValue<TValue>(string key) where TValue : class;
    }

    public abstract class Values
    {
        private static readonly Regex PartialDateRegex = new Regex(@"^(?<year>\d{4})(-(?<month>\d{2}))?$", RegexOptions.Compiled);

        protected virtual object GetValue(string key)
        {
            return null;
        }

        protected string GetStringValue(string key)
        {
            return GetStringValue(GetValue(key));
        }

        protected string[] GetStringArrayValue(string key)
        {
            return GetStringArrayValue(GetValue(key) ?? GetValue(key + "[]"));
        }

        protected bool? GetBooleanValue(string key)
        {
            return GetBooleanValue(GetValue(key)) ?? GetBooleanValue(GetValue(key + "[]"));
        }

        protected int? GetIntValue(string key)
        {
            return GetValue<int>(key, DeconvertInt);
        }

        protected int[] GetIntArrayValue(string key)
        {
            return GetArrayValue<int>(key, DeconvertInt);
        }

        protected decimal? GetDecimalValue(string key)
        {
            var value = GetValue(key);
            if (value is decimal)
                return (decimal)value;
            if (value is int)
                return (int) value;
            return GetValue<decimal>(value, DeconvertDecimal);
        }

        protected Guid? GetGuidValue(string key)
        {
            return GetValue<Guid>(key, DeconvertGuid);
        }

        protected Guid[] GetGuidArrayValue(string key)
        {
            return GetArrayValue<Guid>(key, DeconvertGuid);
        }

        protected DateTime? GetDateTimeValue(string key)
        {
            return GetValue<DateTime>(key, DeconvertDateTime);
        }

        protected PartialDate? GetPartialDateValue(string key)
        {
            return GetValue<PartialDate>(key, DeconvertPartialDate);
        }

        protected TEnum? GetValue<TEnum>(string key)
            where TEnum : struct
        {
            var value = GetValue(key);
            if (value is TEnum)
                return (TEnum)value;
            var svalue = GetStringValue(value);
            return svalue == null ? (TEnum?) null : (TEnum)Enum.Parse(typeof(TEnum), svalue, true);
        }

        protected TEnum? GetFlagsValue<TEnum>()
            where TEnum : struct, IConvertible
        {
            var keys = GetFlagsKeys<TEnum>();
            if (keys == null)
                return null;

            var values = new bool[keys.Length];
            for (var index = 0; index < keys.Length; ++index)
            {
                var value = GetBooleanValue(keys[index]);
                values[index] = value != null && value.Value;
            }

            return GetFlagsValue<TEnum>(values);
        }

        private static string GetStringValue(object value)
        {
            if (value is string)
                return (string) value;
            if (value is string[])
            {
                var avalue = (string[]) value;
                if (avalue.Length == 1 && !string.IsNullOrEmpty(avalue[0]))
                    return avalue[0];
            }

            return null;
        }

        private static string[] GetStringArrayValue(object value)
        {
            if (value == null)
                return null;
            if (value is string)
                return GetStringArrayValue(new[] { (string)value });
            if (value is string[])
                return GetStringArrayValue((string[])value);
            if (value is ArrayList)
            {
                var arrayList = (ArrayList)value;
                var list = new List<string>();
                for (var index = 0; index < arrayList.Count; ++index)
                {
                    if (arrayList[index] is string)
                        list.Add((string)arrayList[index]);
                }

                if (list.Count > 0)
                    return GetStringArrayValue(list.ToArray());
            }
            if (value is object[])
            {
                var oarray = (object[])value;
                var list = new List<string>();
                for (var index = 0; index < oarray.Length; ++index)
                {
                    if (oarray[index] is string)
                        list.Add((string)oarray[index]);
                }

                if (list.Count > 0)
                    return GetStringArrayValue(list.ToArray());
            }

            return null;
        }

        private static string[] GetStringArrayValue(string[] value)
        {
            if (value == null)
                return null;

            // Only return non-null/empty values.

            return (from v in value where string.IsNullOrEmpty(v) select v).Any()
                ? (from v in value where !string.IsNullOrEmpty(v) select v).ToArray()
                : value;
        }

        private static bool? GetBooleanValue(object value)
        {
            if (value is bool)
                return (bool)value;

            var bvalue = GetValue<bool>(value, DeconvertBoolean);
            if (bvalue != null)
                return bvalue;

            var avalue = GetBooleanArrayValue(value);
            if (avalue != null && avalue.Length > 0)
            {
                // If any of the values are true then return true.

                foreach (var abvalue in avalue)
                {
                    if (abvalue)
                        return true;
                }

                return false;
            }

            return null;
        }

        private static bool[] GetBooleanArrayValue(object value)
        {
            if (value is bool[])
                return (bool[]) value;

            if (value is object[])
            {
                var oarray = (object[])value;
                var list = new List<bool>();
                for (var index = 0; index < oarray.Length; ++index)
                {
                    if (oarray[index] is bool)
                        list.Add((bool)oarray[index]);
                }

                if (list.Count > 0)
                    return list.ToArray();
            }

            if (value is ArrayList)
            {
                var arrayList = (ArrayList)value;
                var list = new List<bool>();
                for (var index = 0; index < arrayList.Count; ++index)
                {
                    if (arrayList[index] is bool)
                        list.Add((bool)arrayList[index]);
                }

                if (list.Count > 0)
                    return list.ToArray();
            }

            return GetArrayValue<bool>(value, DeconvertBoolean);
        }

        private T? GetValue<T>(string key, Func<string, T?> deconvert)
            where T : struct
        {
            var value = GetValue(key);
            if (value is T)
                return (T) value;
            return GetValue(value, deconvert);
        }

        private static T? GetValue<T>(object value, Func<string, T?> deconvert)
            where T : struct
        {
            var svalue = GetStringValue(value);
            return string.IsNullOrEmpty(svalue) ? null : deconvert(svalue);
        }

        private T[] GetArrayValue<T>(string key, Func<string, T?> deconvert)
            where T : struct
        {
            var value = GetValue(key) ?? GetValue(key + "[]");
            if (value is T)
                return new[] { (T)value };
            if (value is T[])
                return (T[]) value;
            return GetArrayValue(value, deconvert);
        }

        private static T[] GetArrayValue<T>(object value, Func<string, T?> deconvert)
            where T : struct
        {
            var svalues = GetStringArrayValue(value);
            if (svalues == null)
                return null;
            var values = new List<T>();
            foreach (var svalue in svalues)
            {
                var tvalue = deconvert(svalue);
                if (tvalue != null)
                    values.Add(tvalue.Value);
            }

            return values.Count == 0 ? null : values.ToArray();
        }

        protected static string Convert(bool value)
        {
            return value ? "true" : "false";
        }

        private static bool? DeconvertBoolean(string svalue)
        {
            bool bvalue;
            if (bool.TryParse(svalue, out bvalue))
                return bvalue;

            // Try 1/0.

            int ivalue;
            if (int.TryParse(svalue, out ivalue))
            {
                if (ivalue == 1)
                    return true;
                if (ivalue == 0)
                    return false;
            }

            return null;
        }

        protected static string Convert(int value)
        {
            return value.ToString();
        }

        private static int? DeconvertInt(string svalue)
        {
            int ivalue;
            if (int.TryParse(svalue, out ivalue))
                return ivalue;
            return null;
        }

        protected static string Convert(decimal value)
        {
            return value.ToString();
        }

        private static decimal? DeconvertDecimal(string svalue)
        {
            decimal dvalue;
            if (decimal.TryParse(svalue, out dvalue))
                return dvalue;
            return null;
        }

        protected static string Convert(Guid value)
        {
            return value.ToString();
        }

        protected static string Convert(Guid? value)
        {
            return value == null ? null : Convert(value.Value);
        }

        private static Guid? DeconvertGuid(string value)
        {
            try
            {
                return new Guid(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected static string[] Convert(Guid[] value)
        {
            return value == null ? null : (from g in value select Convert(g)).ToArray();
        }

        protected static string Convert(DateTime? dateTime)
        {
            return dateTime == null
                ? null
                : dateTime.Value.Kind == DateTimeKind.Local
                    ? dateTime.Value.ToUniversalTime().ToString("o")
                    : dateTime.Value.ToString("o");
        }

        private static DateTime? DeconvertDateTime(string value)
        {
            DateTime dt;
            if (!DateTime.TryParse(value, out dt))
                return null;
            return dt.Kind == DateTimeKind.Local ? dt.ToUniversalTime() : dt;
        }

        protected static string Convert(PartialDate? partialDate)
        {
            // ISO 8601 format.

            return partialDate == null
                ? null
                : partialDate.Value.Month == null
                    ? new DateTime(partialDate.Value.Year, 1, 1).ToString("yyyy")
                    : new DateTime(partialDate.Value.Year, partialDate.Value.Month.Value, 1).ToString("yyyy-MM");
        }

        private static PartialDate? DeconvertPartialDate(string value)
        {
            try
            {
                if (value == null)
                    return null;

                var match = PartialDateRegex.Match(value);
                if (!match.Success)
                    return null;

                var year = match.Groups["year"].Value;
                var group = match.Groups["month"];
                var month = group != null
                    ? group.Value
                    : null;

                int iyear;
                if (int.TryParse(year, out iyear))
                {
                    if (string.IsNullOrEmpty(month))
                        return new PartialDate(iyear);

                    int imonth;
                    if (int.TryParse(month, out imonth))
                        return new PartialDate(iyear, imonth);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected static string Convert<TEnum>(TEnum value)
            where TEnum : struct
        {
            return value.ToString();
        }

        protected static string Convert<TEnum>(TEnum? value)
            where TEnum : struct
        {
            return value == null ? null : Convert(value.Value);
        }

        protected static IDictionary<string, bool> ConvertFlags<TEnum>(TEnum value)
            where TEnum : struct, IConvertible
        {
            var values = new Dictionary<string, bool>();

            // Iterate through the names.

            var names = Enum.GetNames(typeof(TEnum));
            var evalues = (TEnum[])Enum.GetValues(typeof(TEnum));

            for (var index = 0; index < names.Length; ++index)
            {
                var name = names[index];
                var evalue = evalues[index];
                if (evalue.ToInt32(null) != 0 && value.IsFlagSet(evalue))
                    values[name] = true;
            }

            return values;
        }

        protected static IDictionary<string, bool> ConvertFlags<TEnum>(TEnum? value)
            where TEnum : struct, IConvertible
        {
            return value == null ? null : ConvertFlags(value.Value);
        }

        private static string[] GetFlagsKeys<TEnum>()
        {
            return Enum.GetNames(typeof(TEnum));
        }

        private static TEnum? GetFlagsValue<TEnum>(bool[] values) where TEnum : struct, IConvertible
        {
            TEnum? evalue = null;

            var evalues = (TEnum[])Enum.GetValues(typeof(TEnum));
            for (var index = 0; index < values.Length; ++index)
            {
                if (values[index])
                {
                    if (evalue == null)
                        evalue = new TEnum();
                    evalue = evalue.Value.SetFlag(evalues[index]);
                }
            }

            return evalue;
        }
    }
}