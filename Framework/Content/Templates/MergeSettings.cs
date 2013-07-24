using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LinkMe.Framework.Content.Templates
{
    public class MergeReferences
        : IEnumerable<string>
    {
        private readonly IList<string> _references = new List<string>();

        internal MergeReferences()
        {
        }

        public void Add(Assembly assembly)
        {
            string codebase = assembly.CodeBase;
            if (codebase.StartsWith("file:///"))
                codebase = codebase.Substring("file:///".Length);
            if (!_references.Contains(codebase))
                _references.Add(codebase);
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _references.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _references.GetEnumerator();
        }

        public bool Contains(string reference)
        {
            return _references.Contains(reference);
        }

        internal MergeReferences Clone()
        {
            MergeReferences references = new MergeReferences();
            foreach (string reference in _references)
                references._references.Add(reference);
            return references;
        }
    }

    public class MergeUsings
        : IEnumerable<string>
    {
        private readonly IList<string> _usings = new List<string>();

        internal MergeUsings()
        {
        }

        public void Add(Type type)
        {
            // Grab the namespace of the type.

            if (!_usings.Contains(type.Namespace))
                _usings.Add(type.Namespace);
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _usings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _usings.GetEnumerator();
        }

        public bool Contains(string ns)
        {
            return _usings.Contains(ns);
        }

        internal MergeUsings Clone()
        {
            MergeUsings usings = new MergeUsings();
            foreach (string @using in _usings)
                usings._usings.Add(@using);
            return usings;
        }
    }

    public class MergeField
    {
        private readonly string _name;
        private readonly Type _type;
        private readonly string _initialValue;

        public MergeField(string name, Type type, string initialValue)
        {
            _name = name;
            _type = type;
            _initialValue = initialValue;
        }

        public MergeField(string name, string initialValue)
        {
            _name = name;
            _type = typeof(string);
            _initialValue = "\"" + initialValue + "\"";
        }

        public string Name
        {
            get { return _name; }
        }

        public Type Type
        {
            get { return _type; }
        }

        public string InitialValue
        {
            get { return _initialValue; }
        }
    }

    public class MergeFields
        : IEnumerable<MergeField>
    {
        private readonly IDictionary<string, MergeField> _fields = new Dictionary<string, MergeField>();

        public MergeField this[string name]
        {
            get { return _fields[name]; }
        }

        public void Add(MergeField field)
        {
            _fields[field.Name] = field;
        }

        IEnumerator<MergeField> IEnumerable<MergeField>.GetEnumerator()
        {
            return _fields.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _fields.Values.GetEnumerator();
        }

        internal MergeFields Clone()
        {
            MergeFields fields = new MergeFields();
            foreach (MergeField field in this)
                fields.Add(new MergeField(field.Name, field.Type, field.InitialValue));
            return fields;
        }
    }

    public class MergeMethods
        : IEnumerable<string>
    {
        private readonly IList<string> _methods = new List<string>();

        public void Add(string method)
        {
            _methods.Add(method);
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _methods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _methods.GetEnumerator();
        }

        internal MergeMethods Clone()
        {
            MergeMethods methods = new MergeMethods();
            foreach (string method in _methods)
                methods._methods.Add(method);
            return methods;
        }
    }

    public class MergeSettings
    {
        private readonly MergeReferences _references = new MergeReferences();
        private readonly MergeUsings _usings = new MergeUsings();
        private readonly MergeFields _fields = new MergeFields();
        private readonly MergeMethods _methods = new MergeMethods();

        public MergeSettings()
        {
            _references = new MergeReferences();
            _usings = new MergeUsings();
            _fields = new MergeFields();
            _methods = new MergeMethods();

            // Add some standard stuff that will be needed.

            _references.Add(typeof(string).Assembly);
            _usings.Add(typeof(MemoryStream));
        }

        internal MergeSettings(MergeReferences references, MergeUsings usings, MergeFields fields, MergeMethods methods)
        {
            _references = references;
            _usings = usings;
            _fields = fields;
            _methods = methods;
        }

        public MergeReferences References
        {
            get { return _references; }
        }

        public MergeUsings Usings
        {
            get { return _usings; }
        }

        public MergeFields Fields
        {
            get { return _fields; }
        }

        public MergeMethods Methods
        {
            get { return _methods; }
        }

        public MergeSettings Clone()
        {
            return new MergeSettings(_references.Clone(), _usings.Clone(), _fields.Clone(), _methods.Clone());
        }
    }
}