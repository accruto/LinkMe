using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Reports
{
    public class ReportParameter
    {
        [Required]
        public string Name { get; set; }
        public object Value { get; set; }
        //public Value { get; set; }

/*
        public ReportParameter(string name, object value)
            : this(name, Convert.ToString(value))
        {
        }

        internal ReportParameter(string typeName, string value)
        {
            TypeName = typeName;
            Value = value;
        }

        private ReportParameter(ReportParameterType type, string value)
        {
            Type = type;
            Value = value;
        }

        public ReportParameterType Type { get; set; }
        public string Value { get; set; }

        internal string TypeName
        {
            get { return Type.ToString(); }
            set { Type = (ReportParameterType)Enum.Parse(typeof(ReportParameterType), value); }
        }

 * public void SetValue<T>(T value)
        {
            _value = value;
        }
*/

        public T GetValue<T>()
            where T : IConvertible
        {
            return (T)Convert.ChangeType(Value, typeof(T));
        }
    }
}