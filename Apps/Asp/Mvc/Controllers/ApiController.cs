using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Results;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Controllers
{
    [CompressActionFilter(Order = 5)]
    public abstract class ApiController
        : Controller
    {
        protected virtual JavaScriptConverter[] GetConverters()
        {
            return null;
        }

        protected JsonResponseResult Json(JsonResponseModel model)
        {
            Prepare(model);
            return new JsonResponseResult(model, GetConverters());
        }

        protected JsonResponseResult Json(JsonResponseModel model, string contentType)
        {
            Prepare(model);
            return new JsonResponseResult(model, contentType, GetConverters());
        }

        protected JsonResponseResult Json(JsonResponseModel model, JsonRequestBehavior behavior)
        {
            Prepare(model);
            return new JsonResponseResult(model, behavior, GetConverters());
        }

        protected JsonResponseResult JsonNotFound(string type, JsonRequestBehavior behavior)
        {
            return Json(new NotFoundException(type), behavior);
        }

        protected JsonResponseResult JsonNotFound(string type)
        {
            return JsonNotFound(type, JsonRequestBehavior.DenyGet);
        }

        private JsonResponseResult Json(UserException exception, JsonRequestBehavior behavior)
        {
            // Need to populate errors.

            var errorHandler = (IErrorHandler) new StandardErrorHandler();
            var message = errorHandler.FormatErrorMessage(exception);
            var errorCode = errorHandler.GetErrorCode(exception);

            return Json(new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError> { new JsonError { Code = errorCode, Message = message } }
            }, behavior);
        }

        protected XmlResult Xml(Stream stream)
        {
            return new XmlResult(stream); 
        }

        private void Prepare(JsonResponseModel model)
        {
            // Convert model state errors into errors on the model.

            foreach (var state in ModelState)
            {
                // Error codes themselves do not become errors on the model.

                if (IsErrorCode(state.Key))
                    continue;

                // Add a model error for each model state error.

                var errorCode = GetErrorCode(ModelState, state.Key);
                foreach (var error in state.Value.Errors)
                {
                    if (model.Errors == null)
                        model.Errors = new List<JsonError>();
                    model.Errors.Add(new JsonError
                    {
                        Key = CapitaliseKey(state.Key.NullIfEmpty()),
                        Code = errorCode,
                        Message = error.ErrorMessage,
                    });
                }
            }

            // Success simply means are there any errors or not.

            model.Success = model.Errors == null || model.Errors.Count == 0;
        }

        private static string CapitaliseKey(string key)
        {
            return string.IsNullOrEmpty(key) || key.Length == 0 || char.IsUpper(key[0])
                ? key
                : char.ToUpper(key[0]) + key.Substring(1);
        }

        private static bool IsErrorCode(string key)
        {
            return key != null && key.EndsWith("ErrorCode");
        }

        private string GetErrorCode(IDictionary<string, ModelState> modelState, string key)
        {
            string errorCode = null;

            var errorCodeKey = (key ?? "") + "ErrorCode";
            if (ModelState.ContainsKey(errorCodeKey))
            {
                var errorCodeState = modelState[errorCodeKey];
                errorCode = errorCodeState.Errors == null || errorCodeState.Errors.Count == 0
                    ? null
                    : errorCodeState.Errors[0].ErrorMessage;
            }

            return errorCode ?? ErrorCodes.GetDefaultErrorCode(ErrorCodeClass.Validation);
        }
    }
}
