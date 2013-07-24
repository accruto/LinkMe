using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Reports
{
    public abstract class Report
    {
        private IList<ReportParameter> _parameters;

        [DefaultNewGuid]
        public Guid Id { get; set; }

        [Required]
        public string Type { get; protected set; }

        [IsSet]
        public Guid ClientId { get; set; }

        public bool SendToAccountManager { get; set; }
        public bool SendToClient { get; set; }

        [Prepare, Validate]
        public IList<ReportParameter> Parameters
        {
            get { return _parameters ?? (_parameters = new List<ReportParameter>()); }
        }

        internal void SetParameters(IList<ReportParameter> parameters)
        {
            _parameters = parameters;
        }

        protected T GetParameterValue<T>(string name, T defaultValue)
            where T : IConvertible
        {
            if (_parameters == null)
                return defaultValue;

            var parameter = (from p in Parameters where p.Name == name select p).SingleOrDefault();
            if (parameter == null)
                return defaultValue;

            return parameter.GetValue<T>();
        }

        protected void SetParameterValue(string name, object value)
        {
            if (_parameters == null)
                _parameters = new List<ReportParameter>();

            var parameter = (from p in _parameters where p.Name == name select p).SingleOrDefault();
            if (parameter == null)
                _parameters.Add(new ReportParameter { Name = name, Value = value });
            else
                parameter.Value = value;
        }
    }
}