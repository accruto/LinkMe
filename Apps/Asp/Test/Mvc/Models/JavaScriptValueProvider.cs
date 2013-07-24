using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LinkMe.Apps.Asp.Test.Mvc.Models
{
    public class JavaScriptValueProvider
        : IValueProvider
    {
        private readonly IValueProvider _valueProvider;

        public JavaScriptValueProvider(string serialization)
        {
            var serializer = new JavaScriptSerializer();
            var obj = serializer.DeserializeObject(serialization);

            var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            AddToBackingStore(backingStore, string.Empty, obj);
            _valueProvider = new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
        }

        bool IValueProvider.ContainsPrefix(string prefix)
        {
            return _valueProvider.ContainsPrefix(prefix);
        }

        ValueProviderResult IValueProvider.GetValue(string key)
        {
            return _valueProvider.GetValue(key);
        }

        private static void AddToBackingStore(IDictionary<string, object> backingStore, string prefix, object value)
        {
            var dictionary = value as IDictionary<string, object>;
            if (dictionary != null)
            {
                foreach (var entry in dictionary)
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                return;
            }
/*
            var list = value as IList;
            if (list != null)
            {
                for (var index = 0; index < list.Count; index++)
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, index), list[index]);
                return;
            }
*/

            backingStore[prefix] = value;
        }

        private static string MakeArrayKey(string prefix, int index)
        {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return (String.IsNullOrEmpty(prefix)) ? propertyName : prefix + "." + propertyName;
        }
    }
}
