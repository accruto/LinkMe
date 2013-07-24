using System;
using System.Collections.Generic;
using System.Reflection;

namespace LinkMe.Framework.Content.Templates
{
    public abstract class MergeTextTemplateEngine
        : ITextTemplateEngine
    {
        private const string Method = "Merge";

        private readonly Guid? _verticalId;
        private readonly string _mimeType;
        private readonly string _text;
        private readonly MergeSettings _settings;
        private readonly object _methodLock = new object();
        private volatile MethodInfo _methodInfo;

        protected MergeTextTemplateEngine(Guid? verticalId, string mimeType, string text, MergeSettings settings)
        {
            _verticalId = verticalId;
            _mimeType = mimeType;
            _text = text;
            _settings = settings;
        }

        string ITextTemplateEngine.GetCopyText(TemplateContext context, TemplateProperties properties)
        {
            return GetCopyText(context, properties);
        }

        protected Guid? VerticalId
        {
            get { return _verticalId; }
        }

        protected string MimeType
        {
            get { return _mimeType; }
        }

        protected virtual string GetCopyText(TemplateContext context, TemplateProperties properties)
        {
            // Get the method info for this template.

            if (_methodInfo == null)
            {
                lock (_methodLock)
                {
                    if (_methodInfo == null)
                        _methodInfo = CreateMethodInfo(properties);
                }
            }

            // Create the set of parameters and invoke.

            return (string)_methodInfo.Invoke(null, GetParameters(properties));
        }

        private static object[] GetParameters(TemplateProperties properties)
        {
            var parameters = new object[properties.Count];
            var index = 0;
            foreach (var property in properties)
                parameters[index++] = property.Value;
            return parameters;
        }

        private MethodInfo CreateMethodInfo(TemplateProperties properties)
        {
            // Parse the text and create an assembly from it.

            var settings = GetSettings(properties);
            var code = MergeParser.Parse(_text, _mimeType, Method, properties, settings);
            return MergeCompiler.Compile(code, _mimeType, Method, settings);
        }

        private MergeSettings GetSettings(IEnumerable<TemplateProperty> properties)
        {
            var newSettings = _settings.Clone();
            foreach (var property in properties)
                newSettings.References.Add(property.Type.Assembly);
            return newSettings;
        }
    }
}