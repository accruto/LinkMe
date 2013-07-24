using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Criterias
{
    public abstract class Criteria
        : ICloneable
    {
        private IDictionary<string, CriteriaItem> _items = new Dictionary<string, CriteriaItem>();
        private readonly IDictionary<string, CriteriaDescription> _descriptions;

        internal protected class CriteriaItem
        {
            private readonly string _name;
            private readonly object _value;

            public CriteriaItem(string name, object value)
            {
                _name = name;
                _value = value;
            }

            public string Name
            {
                get { return _name; }
            }

            public object Value
            {
                get { return _value; }
            }
        }

        protected abstract class CriteriaDescription
        {
            private readonly IValuePersister _persister;

            protected CriteriaDescription(IValuePersister persister)
            {
                _persister = persister;
            }

            public object ToPersistantValue(object value, bool persistantIsString)
            {
                return _persister.ToPersistantValue(value, persistantIsString);
            }

            public object FromPersistantValue(object value, bool persistantIsString)
            {
                return _persister.FromPersistantValue(value, persistantIsString);
            }

            public abstract bool IsDefaultValue(object value);
            public abstract object CloneValue(object value);
            public abstract object GetValue(string name, IDictionary<string, CriteriaItem> items, CriteriaItem item);
            public abstract bool AreEqual(object value1, object value2);
        }

        protected class CriteriaValueDescription<TValue>
            : CriteriaDescription
        {
            private readonly TValue _defaultValue;

            public CriteriaValueDescription(TValue defaultValue, IValuePersister persister)
                : base(persister)
            {
                _defaultValue = defaultValue;
            }

            public CriteriaValueDescription(TValue defaultValue)
                : base(new ValuePersister())
            {
                _defaultValue = defaultValue;
            }

            public override bool IsDefaultValue(object value)
            {
                return Equals(_defaultValue, value);
            }

            public override object CloneValue(object value)
            {
                // If it can be cloned then clone it.

                if (value is ICloneable)
                    return ((ICloneable)value).Clone();

                // Should work for all structs etc.

                return value;
            }

            public override object GetValue(string name, IDictionary<string, CriteriaItem> items, CriteriaItem item)
            {
                return item != null ? item.Value : _defaultValue;
            }

            public override bool AreEqual(object value1, object value2)
            {
                return Equals(value1, value2);
            }
        }

        protected class CriteriaListDescription<TValue>
            : CriteriaDescription
        {
            public CriteriaListDescription(Func<TValue, string> toPersistantValue, Func<string, TValue> fromPersistantValue)
                : base(new ListValuePersister<TValue>(toPersistantValue, fromPersistantValue))
            {
            }

            public CriteriaListDescription(IValuePersister persister)
                : base(persister)
            {
            }

            public override bool IsDefaultValue(object value)
            {
                return value == null || ((IList<TValue>) value).Count == 0;
            }

            public override object CloneValue(object value)
            {
                if (value == null)
                    return null;

                // Create a new list.

                var clone = new List<TValue>();
                foreach (var t in (IList<TValue>)value)
                {
                    if (t is ICloneable)
                        clone.Add((TValue)((ICloneable)t).Clone());
                    else
                        clone.Add(t);
                }

                return clone;
            }

            public override object GetValue(string name, IDictionary<string, CriteriaItem> items, CriteriaItem item)
            {
                // Need to make sure that the same list is always returned.

                if (item != null)
                    return item.Value;

                var value = new List<TValue>();
                items[name] = new CriteriaItem(name, value);
                return value;
            }

            public override bool AreEqual(object value1, object value2)
            {
                var l1 = (IList<TValue>)value1;
                var l2 = (IList<TValue>)value2;
                if (l1.Count != l2.Count)
                    return false;

                for (var index = 0; index < l1.Count; ++index)
                {
                    if (!Equals(l1[index], l2[index]))
                        return false;
                }

                return true;
            }
        }

        protected Criteria(IDictionary<string, CriteriaDescription> descriptions)
        {
            _descriptions = descriptions;
        }

        [DefaultNewGuid]
        public Guid Id { get; set; }

        #region ICloneable

        object ICloneable.Clone()
        {
            OnCloning();

            // Create a new instance and clone all items.

            var clone = (Criteria)MemberwiseClone();
            clone._items = (from i in _items.Values select new CriteriaItem(i.Name, _descriptions[i.Name].CloneValue(i.Value))).ToDictionary(i => i.Name);

            // Give the derived class a chance to fix itself up if needed.

            clone.OnCloned();

            return clone;
        }

        protected virtual void OnCloning()
        {
        }

        protected virtual void OnCloned()
        {
        }

        #endregion

        internal IEnumerable<CriteriaItem> GetPersistantItems(bool persistantIsString)
        {
            foreach (var item in _items.Values)
            {
                if (!IsDefaultValue(item.Name, item.Value))
                    yield return new CriteriaItem(item.Name, ToPersistantValue(item.Name, item.Value, persistantIsString));
            }
        }

        internal void SetPersistantItem(string name, object value, bool persistantIsString)
        {
            SetValue(name, FromPersistantValue(name, value, persistantIsString));
        }

        private bool IsDefaultValue(string name, object value)
        {
            return _descriptions[name].IsDefaultValue(value);
        }

        private object ToPersistantValue(string name, object value, bool persistantIsString)
        {
            return _descriptions[name].ToPersistantValue(value, persistantIsString);
        }

        private object FromPersistantValue(string name, object value, bool persistantIsString)
        {
            return _descriptions[name].FromPersistantValue(value, persistantIsString);
        }

        private object GetValue(string name)
        {
            CriteriaItem item;
            _items.TryGetValue(name, out item);
            return _descriptions[name].GetValue(name, _items, item);
        }

        protected internal T GetValue<T>(string name)
        {
            return (T) GetValue(name);
        }

        protected internal void SetValue(string name, object value)
        {
            _items[name] = new CriteriaItem(name, value);
        }

        protected static IDictionary<string, CriteriaDescription> Combine(IDictionary<string, CriteriaDescription> descriptions1, IDictionary<string, CriteriaDescription> descriptions2)
        {
            return descriptions1.Concat(descriptions2).ToDictionary(d => d.Key, d => d.Value);
        }

        protected void AddDescription(string name, CriteriaDescription description)
        {
            if (!_descriptions.ContainsKey(name))
                _descriptions[name] = description;
        }

        protected ICollection<string> ItemNames
        {
            get { return _items.Keys; }
        }
    }
}