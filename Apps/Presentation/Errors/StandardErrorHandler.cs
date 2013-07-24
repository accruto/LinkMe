using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Presentation.Errors
{
    public class StandardErrorHandler
        : IErrorHandler
    {
        private static readonly IErrorHandler DefaultErrorHandler = new DefaultErrorHandler();

        string IErrorHandler.FormatErrorMessage(UserException exception)
        {
            return FormatErrorMessage(exception);
        }

        string IErrorHandler.FormatErrorMessage(ValidationError error)
        {
            return FormatErrorMessage(error);
        }

        string IErrorHandler.GetErrorCode(UserException exception)
        {
            return GetErrorCode(exception);
        }

        string IErrorHandler.GetErrorCode(ValidationError error)
        {
            return GetErrorCode(error);
        }

        protected virtual string FormatErrorMessage(UserException ex)
        {
            var message = FormatErrorMessage(ErrorMessages.ResourceManager, ErrorMessages.Culture, ex);
            return message ?? DefaultErrorHandler.FormatErrorMessage(ex);
        }

        protected virtual string FormatErrorMessage(ValidationError error)
        {
            var message = FormatErrorMessage(ErrorMessages.ResourceManager, ErrorMessages.Culture, error);
            return message ?? DefaultErrorHandler.FormatErrorMessage(error);
        }

        protected virtual string GetErrorCode(UserException ex)
        {
            var code = GetErrorCode(ErrorCodes.ResourceManager, ErrorCodes.Culture, ex);
            return code ?? DefaultErrorHandler.GetErrorCode(ex);
        }

        protected virtual string GetErrorCode(ValidationError error)
        {
            var code = GetErrorCode(ErrorCodes.ResourceManager, ErrorCodes.Culture, error);
            return code ?? DefaultErrorHandler.GetErrorCode(error);
        }

        protected string FormatErrorMessage(ResourceManager resourceManager, CultureInfo cultureInfo, UserException ex)
        {
            if (ex is ValidationErrorsException)
                return FormatErrorMessage(resourceManager, cultureInfo, (ValidationErrorsException)ex);
            return FormatErrorMessage(resourceManager, cultureInfo, ex.GetType(), null, ex.GetErrorMessageParameters());
        }

        protected string FormatErrorMessage(ResourceManager resourceManager, CultureInfo cultureInfo, ValidationError error)
        {
            return FormatErrorMessage(resourceManager, cultureInfo, error.GetType(), error.Name, error.GetErrorMessageParameters());
        }

        protected string GetErrorCode(ResourceManager resourceManager, CultureInfo cultureInfo, UserException ex)
        {
            return GetErrorCode(resourceManager, cultureInfo, ex.GetType());
        }

        protected string GetErrorCode(ResourceManager resourceManager, CultureInfo cultureInfo, ValidationError error)
        {
            return GetErrorCode(resourceManager, cultureInfo, error.GetType());
        }

        private static string GetErrorMessageParameterValue(ResourceManager resourceManager, CultureInfo cultureInfo, ValidationErrorName validationErrorName)
        {
            if (validationErrorName.Name == null)
                return null;
            var name = resourceManager.GetString(validationErrorName.Name, cultureInfo);
            return name ?? validationErrorName.Name;
        }

        private static object[] GetErrorMessageParameters(ResourceManager resourceManager, CultureInfo cultureInfo, IEnumerable<object> parameters)
        {
            return parameters == null
                ? null
                : (from p in parameters select (p is ValidationErrorName ? GetErrorMessageParameterValue(GetErrorMessageParameterValue(resourceManager, cultureInfo, (ValidationErrorName)p)) : p)).ToArray();
        }

        private string FormatErrorMessage(ResourceManager resourceManager, CultureInfo cultureInfo, ValidationErrorsException ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine(resourceManager.GetString(ex.GetType().Name, cultureInfo));
            foreach (var error in ex.Errors)
                sb.Append("\t").AppendLine(FormatErrorMessage(resourceManager, cultureInfo, error));
            return sb.ToString();
        }

        protected string FormatErrorMessage(ResourceManager resourceManager, CultureInfo cultureInfo, Type type, string name, object[] errorMessageParameters)
        {
            if (type == typeof(UserException) || type == typeof(ValidationError))
                return null;

            // Try specific name first.

            resourceManager.IgnoreCase = true;

            string message = null;
            if (name != null)
                message = resourceManager.GetString(type.Name + "_" + name, cultureInfo);
            if (message == null)
                message = resourceManager.GetString(type.Name, cultureInfo);

            return message != null
                ? GetErrorMessage(message, GetErrorMessageParameters(resourceManager, cultureInfo, errorMessageParameters))
                : FormatErrorMessage(resourceManager, cultureInfo, type.BaseType, name, errorMessageParameters);
        }

        private static string GetErrorMessage(string format, object[] parameters)
        {
            var message = string.Format(format, parameters);
            if (char.IsLower(message[0]))
                return char.ToUpper(message[0]) + message.Substring(1);
            return message;
        }

        private static string GetErrorMessageParameterValue(string value)
        {
            if (value == null)
                return null;

            // Break apart words and lower case them all (relies on camelCasing etc) to work.

            var sb = new StringBuilder();
            for (var index = 0; index < value.Length; ++index)
            {
                var c = value[index];
                if (char.IsUpper(c))
                {
                    if (index != 0 && value[index - 1] != ' ')
                        sb.Append(' ');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private static string GetErrorCode(ResourceManager resourceManager, CultureInfo cultureInfo, Type type)
        {
            if (type == typeof(UserException) || type == typeof(ValidationError))
                return null;

            // Try specific name first.

            resourceManager.IgnoreCase = true;
            var code = resourceManager.GetString(type.Name, cultureInfo);
            return code ?? GetErrorCode(resourceManager, cultureInfo, type.BaseType);
        }
    }
}