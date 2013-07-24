using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public static class ModelStateKeys
    {
        public const string Confirmation = "ConfirmationNotification";
        public const string Information = "InformationNotification";
    }

    public static class ModelStateExtensions
    {
        private static readonly EventSource EventSource = new EventSource<ModelState>();
        private const string Method = "Log Model Error";

        public static void AddModelError(this ModelStateDictionary modelState, ValidationError error, IErrorHandler errorHandler)
        {
            modelState.AddModelError(error.Name, errorHandler.FormatErrorMessage(error), errorHandler.GetErrorCode(error));
        }

        public static void AddModelError(this ModelStateDictionary modelState, IEnumerable<ValidationError> errors, IErrorHandler errorHandler)
        {
            foreach (var error in errors)
                modelState.AddModelError(error.Name, errorHandler.FormatErrorMessage(error), errorHandler.GetErrorCode(error));
        }

        public static void AddModelError(this ModelStateDictionary modelState, UserException exception, IErrorHandler errorHandler)
        {
            if (exception is ValidationErrorsException)
            {
                modelState.AddModelError(((ValidationErrorsException) exception).Errors, errorHandler);
            }
            else
            {
                EventSource.Raise(Event.NonCriticalError, Method, "User exception.", exception, errorHandler);
                modelState.AddModelError("", errorHandler.FormatErrorMessage(exception), errorHandler.GetErrorCode(exception));
            }
        }

        public static void AddModelError(this ModelStateDictionary modelState, string message)
        {
            modelState.AddModelError("", message);
        }

        private static void AddModelError(this ModelStateDictionary modelState, string key, string errorMessage, string errorCode)
        {
            modelState.AddModelError(key, errorMessage);

            // The model state does not allow adding anything except an error message so add the error code by adding it under a separate key.

            if (!string.IsNullOrEmpty(errorCode))
                modelState.AddModelError(key + "ErrorCode", errorCode);
        }

        public static string[] GetErrorMessages(this ModelStateDictionary modelState)
        {
            return modelState.Where(s => s.Key == null || (!s.Key.EndsWith("ErrorCode") && s.Key != ModelStateKeys.Confirmation && s.Key != ModelStateKeys.Information)).SelectMany(s => s.Value.Errors).Select(e => e.ErrorMessage).ToArray();
        }

        public static void AddModelConfirmation(this ModelStateDictionary modelStateDictionary, string message)
        {
            modelStateDictionary.AddModelMessage(ModelStateKeys.Confirmation, message);
        }

        public static void AddModelInformation(this ModelStateDictionary modelStateDictionary, string message)
        {
            modelStateDictionary.AddModelMessage(ModelStateKeys.Information, message);
        }

        private static void AddModelMessage(this IDictionary<string, ModelState> modelStateDictionary, string key, string message)
        {
            // Add the message as an error.

            ModelState modelState;
            if (modelStateDictionary.ContainsKey(key))
            {
                modelState = modelStateDictionary[key];
            }
            else
            {
                modelState = new ModelState { Value = new ValueProviderResult(null, null, CultureInfo.CurrentCulture) };
                modelStateDictionary.Add(key, modelState);
            }

            // Add the message as an error.

            modelState.Errors.Add(message);
        }
    }
}
