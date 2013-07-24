using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Criterias
{
    public interface IValuePersister
    {
        object ToPersistantValue(object value, bool persistantIsString);
        object FromPersistantValue(object value, bool persistantIsString);
    }

    public interface ICriteriaPersister
    {
        string GetCriteriaType(Criteria criteria);
        Criteria CreateCriteria(string type);
        TCriteria CreateCriteria<TCriteria>(string type) where TCriteria : Criteria;

        void OnSaving(Criteria criteria);
        void OnSaved(Criteria criteria);
        void OnLoading(Criteria criteria);
        void OnLoaded(Criteria criteria);
    }

    public class ValuePersister
        : IValuePersister
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            return value;
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            return value;
        }
    }

    public class EnumValuePersister<TValue>
        : IValuePersister
        where TValue : struct, IConvertible
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            if (value == null)
                return persistantIsString ? string.Empty : null;

            return persistantIsString
                ? (object) ((TValue?)value).Value.ToInt32(null).ToString()
                : ((TValue) value).ToInt32(null);
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            if (persistantIsString)
            {
                if ((string) value == string.Empty)
                    return null;
            }
            else if (value == null)
                return null;

            return persistantIsString
                ? (TValue)Enum.ToObject(typeof(TValue), int.Parse((string)value))
                : (TValue) Enum.ToObject(typeof (TValue), value);
        }
    }

    public class IntValuePersister
        : IValuePersister
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            if (value == null)
                return persistantIsString ? string.Empty : null;

            return persistantIsString ? value.ToString() : value;
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            if (persistantIsString)
            {
                if ((string)value == string.Empty)
                    return null;
            }
            else if (value == null)
                return null;

            return persistantIsString ? int.Parse((string)value) : value;
        }
    }

    public class GuidValuePersister
        : IValuePersister
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            if (value == null)
                return persistantIsString ? string.Empty : null;

            return persistantIsString ? ((Guid) value).ToString() : value;
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            if (persistantIsString)
            {
                if ((string)value == string.Empty)
                    return null;
            }
            else if (value == null)
                return null;

            return persistantIsString ? new Guid((string) value) : value;
        }
    }

    public class DecimalValuePersister
        : IValuePersister
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            if (value == null)
                return persistantIsString ? string.Empty : null;
            return persistantIsString ? value.ToString() : value;
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            if (persistantIsString)
            {
                if ((string)value == string.Empty)
                    return null;
            }
            else if (value == null)
                return null;

            return persistantIsString ? decimal.Parse((string) value) : value;
        }
    }

    public class BoolValuePersister
        : IValuePersister
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            if (value == null)
                return persistantIsString ? string.Empty : null;
            return persistantIsString
                ? (object)value.ToString()
                : (bool)value ? 1 : 0;
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            if (persistantIsString)
            {
                if ((string)value == string.Empty)
                    return null;
            }
            else if (value == null)
                return null;

            return persistantIsString
                ? bool.Parse((string)value)
                : (int)value == 1;
        }
    }

    public class TimeSpanValuePersister
        : IValuePersister
    {
        public object ToPersistantValue(object value, bool persistantIsString)
        {
            if (!(value is TimeSpan))
                return persistantIsString ? string.Empty : null;
            var ticks = ((TimeSpan) value).Ticks;
            return persistantIsString ? (object)ticks.ToString() : ticks;
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            if (persistantIsString)
            {
                if ((string)value == string.Empty)
                    return null;
            }
            else if (value == null)
                return null;

            return persistantIsString ? new TimeSpan(long.Parse((string)value)) : value;
        }
    }

    public class ListValuePersister<TValue>
        : IValuePersister
    {
        private readonly Func<TValue, string> _toPersistantValue;
        private readonly Func<string, TValue> _fromPersistantValue;

        public ListValuePersister(Func<TValue, string> toPersistantValue, Func<string, TValue> fromPersistantValue)
        {
            _toPersistantValue = toPersistantValue;
            _fromPersistantValue = fromPersistantValue;
        }

        public object ToPersistantValue(object value, bool persistantIsString)
        {
            return string.Join(",", ((IList<TValue>)value).Select(i => _toPersistantValue(i)).ToArray());
        }

        public object FromPersistantValue(object value, bool persistantIsString)
        {
            return ((string)value).Split(new[] { ',' }).Select(v => _fromPersistantValue(v)).ToList();
        }
    }
}