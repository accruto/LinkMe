using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkMe.Framework.Content.Templates
{
    internal class TemplateProperty
    {
        private readonly string _name;
        private readonly object _value;
        private readonly Type _type;

        public TemplateProperty(string name, object value, Type type)
        {
            _name = name;
            _value = value;
            _type = type ?? (value == null ? typeof(object) : value.GetType());
        }

        public TemplateProperty(string name, object value)
        {
            _name = name;
            _value = value;
            _type = (value == null ? typeof(object) : value.GetType());
        }

        public string Name
        {
            get { return _name; }
        }

        public object Value
        {
            get { return _value; }
        }

        public Type Type
        {
            get { return _type; }
        }
    }

    public class TemplateProperties
        : IEnumerable<TemplateProperty>
    {
        private readonly SortedList<string, TemplateProperty> _properties = new SortedList<string, TemplateProperty>();

        public int Count
        {
            get { return _properties.Count; }
        }

        public object this[string name]
        {
            get { return _properties[name].Value; }
        }

        public void Add(string name, object value)
        {
            _properties[name] = new TemplateProperty(name, value);
        }

        public void Add(string name, string value)
        {
            _properties[name] = new TemplateProperty(name, value ?? string.Empty);
        }

        public void Add(string name, object value, Type type)
        {
            _properties[name] = new TemplateProperty(name, value, type);
        }

        IEnumerator<TemplateProperty> IEnumerable<TemplateProperty>.GetEnumerator()
        {
            return _properties.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _properties.Values.GetEnumerator();
        }
    }
}