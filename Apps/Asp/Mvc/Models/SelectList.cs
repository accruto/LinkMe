using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public class SelectList<TItem, TValue>
        : IEnumerable<SelectListItem>
        where TItem : class
        where TValue : struct
    {
        private readonly IEnumerable<TItem> _items;
        private readonly object _selectedValue;
        private readonly Func<TItem, TValue?> _getValue;
        private readonly Func<TItem, string> _getText;

        public SelectList(IEnumerable<TItem> items)
        {
            _items = items;
        }

        public SelectList(IEnumerable<TItem> items, TValue selectedValue)
        {
            _items = items;
            _selectedValue = selectedValue;
        }

        public SelectList(IEnumerable<TItem> items, Func<TItem, TValue?> getValue, Func<TItem, string> getText, TValue selectedValue)
        {
            _items = items;
            _getValue = getValue;
            _getText = getText;
            _selectedValue = selectedValue;
        }

        public SelectList(IEnumerable<TItem> items, Func<TItem, TValue?> getValue, Func<TItem, string> getText, object selectedValue)
        {
            _items = items;
            _getValue = getValue;
            _getText = getText;
            _selectedValue = selectedValue;
        }

        IEnumerator<SelectListItem> IEnumerable<SelectListItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<SelectListItem> GetEnumerator()
        {
            foreach (var item in _items)
                yield return new SelectListItem {Value = GetValue(item), Text = GetText(item), Selected = GetSelected(item)};
        }

        private bool GetSelected(TItem item)
        {
            if (item == null)
                return Equals(null, _selectedValue);

            return _getValue == null
                ? Equals(item, _selectedValue)
                : Equals(_getValue(item), _selectedValue);
        }

        private string GetText(TItem item)
        {
            return _getText == null
                ? item == null ? string.Empty : item.ToString()
                : _getText(item) ?? string.Empty;
        }

        private string GetValue(TItem item)
        {
            if (_getValue == null)
                return item == null ? string.Empty : item.ToString();

            var value = _getValue(item);
            return value == null
                ? string.Empty
                : value.Value.ToString();
        }
    }

    public class SelectList<TValue>
        : IEnumerable<SelectListItem>
    {
        private readonly IEnumerable<TValue> _values;
        private readonly TValue _selectedValue;
        private readonly Func<TValue, string> _getText;

        public SelectList(IEnumerable<TValue> values)
        {
            _values = values;
        }

        public SelectList(IEnumerable<TValue> values, TValue selectedValue)
        {
            _values = values;
            _selectedValue = selectedValue;
        }

        public SelectList(IEnumerable<TValue> values, Func<TValue, string> getText)
        {
            _values = values;
            _getText = getText;
        }

        public SelectList(IEnumerable<TValue> values, TValue selectedValue, Func<TValue, string> getText)
        {
            _values = values;
            _selectedValue = selectedValue;
            _getText = getText;
        }

        IEnumerator<SelectListItem> IEnumerable<SelectListItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<SelectListItem> GetEnumerator()
        {
            foreach (var value in _values)
                yield return new SelectListItem
                {
                    Value = value.ToString(),
                    Text = _getText == null ? value.ToString() : _getText(value),
                    Selected = Equals(value, _selectedValue)
                };
        }
    }

    public class EnumSelectList<TValue>
        : IEnumerable<SelectListItem>
        where TValue : struct
    {
        private readonly IEnumerable<TValue> _values;
        private readonly Func<TValue, string> _getText;
        private readonly TValue _selectedValue;

        public EnumSelectList(TValue selectedValue, IEnumerable<TValue> values, IEnumerable<TValue> except, Func<TValue, string> getText)
        {
            _values = GetValues(values, except);
            _selectedValue = selectedValue;
            _getText = getText;
        }

        private static IEnumerable<TValue> GetValues(IEnumerable<TValue> values, IEnumerable<TValue> except)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TValue)))
                throw new ApplicationException("Cannot use a non-enum type.");

            if (values == null || values.Count() == 0)
                values = Enum.GetValues(typeof(TValue)).Cast<TValue>();
            return except != null ? values.Except(except) : values;
        }

        IEnumerator<SelectListItem> IEnumerable<SelectListItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<SelectListItem> GetEnumerator()
        {
            foreach (var value in _values)
                yield return new SelectListItem
                {
                    Value = value.ToString(),
                    Text = _getText == null ? value.ToString() : _getText(value),
                    Selected = Equals(value, _selectedValue)
                };
        }
    }

    public class OptionalEnumSelectList<TValue>
        : IEnumerable<SelectListItem>
        where TValue : struct
    {
        private readonly IEnumerable<TValue?> _values;
        private readonly Func<TValue?, string> _getText;
        private readonly TValue? _selectedValue;

        private class Comparer
            : IComparer<TValue?>
        {
            private readonly Func<TValue?, TValue?, int> _comparer;

            public Comparer(Func<TValue?, TValue?, int> comparer)
            {
                _comparer = comparer;
            }

            public int Compare(TValue? x, TValue? y)
            {
                return _comparer(x, y);
            }
        }

        public OptionalEnumSelectList(TValue? selectedValue, IEnumerable<TValue> values, IEnumerable<TValue> except, Func<TValue?, TValue?, int> comparer, Func<TValue?, string> getText)
        {
            // Add an extra empty item to make it optional.

            _values = new TValue?[1].Concat(GetValues(values, except).Cast<TValue?>());
            if (comparer != null)
                _values = _values.OrderBy(v => v, new Comparer(comparer));
            _selectedValue = selectedValue;
            _getText = getText;
        }

        private static IEnumerable<TValue> GetValues(IEnumerable<TValue> values, IEnumerable<TValue> except)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TValue)))
                throw new ApplicationException("Cannot use a non-enum type.");

            if (values == null || values.Count() == 0)
                values = Enum.GetValues(typeof(TValue)).Cast<TValue>();
            return except != null ? values.Except(except) : values;
        }

        IEnumerator<SelectListItem> IEnumerable<SelectListItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<SelectListItem> GetEnumerator()
        {
            foreach (var value in _values)
                yield return new SelectListItem
                {
                    Value = value == null ? string.Empty : value.ToString(),
                    Text = _getText == null
                        ? (value == null ? string.Empty : value.ToString())
                        : _getText(value),
                    Selected = Equals(value, _selectedValue)
                };
        }
    }

    public class MultiSelectList<TItem, TValue>
        : MultiSelectList
    {
        public MultiSelectList(IEnumerable<TItem> items, string valueField, string textField)
            : base(items, valueField, textField)
        {
        }

        public MultiSelectList(IEnumerable<TItem> items, string valueField, string textField, IEnumerable<TValue> selectedValues)
            : base(items, valueField, textField, selectedValues)
        {
        }
    }
}