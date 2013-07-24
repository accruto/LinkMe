using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Domain.Criterias
{
    public class GenericCriteria
        : Criteria, IEnumerable<KeyValuePair<string, object>>
    {
        public GenericCriteria()
            : base(new Dictionary<string, CriteriaDescription>())
        {
        }

        public new T GetValue<T>(string name)
        {
            return base.GetValue<T>(name);
        }

        public void SetValue<T>(string name, T value)
        {
            AddDescription(name, new CriteriaValueDescription<T>(default(T)));
            base.SetValue(name, value);
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var name in ItemNames)
                yield return new KeyValuePair<string, object>(name, GetValue<object>(name));
        }
    }
}
