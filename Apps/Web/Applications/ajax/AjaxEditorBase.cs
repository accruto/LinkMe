using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Apps.Asp.Exceptions;
using LinkMe.Apps.Asp.Security;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Applications.Ajax
{
    public abstract class AjaxEditorBase
    {
        protected static readonly IAuthenticationManager _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();

        public const string NoRecordId = "-1";

        #region Nested types

        [Serializable]
        private class EditingWhileNotLoggedInException : Exception
        {
            private readonly string _requiredRole;

            public EditingWhileNotLoggedInException(string requiredRole)
            {
                _requiredRole = requiredRole;
            }

            private EditingWhileNotLoggedInException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }

            public override string Message
            {
                get { return "A user not logged in as " + _requiredRole + " attempted to edit a resume."; }
            }
        }

        #endregion

        public const string ErrorSignout = "Your session has expired. Please log in again.";
        public const string ErrorServer = "Error occured on the server. Please try again later.";

        protected static RegisteredUser LoggedInUser
        {
            get { return _authenticationManager.GetUser(new HttpContextWrapper(HttpContext.Current)); }
        }

        protected static Member LoggedInMember
        {
            get { return LoggedInUser as Member; }
        }

        protected static Employer LoggedInEmployer
        {
            get { return LoggedInUser as Employer; }
        }

        protected static string ConvertMessagesToString(IEnumerable<string> messages)
        {
            var sb = new StringBuilder();
            foreach (string msg in messages)
            {
                sb.Append(msg);
                sb.Append("\n");
            }
            return HtmlUtil.TextToHtml(sb.ToString());
        }

        protected static AjaxResult HandleException(Exception ex)
        {
            if (ex is UserException)
                return new AjaxResult(AjaxResultCode.FAILURE, GetErrorMessage((UserException)ex, new StandardErrorHandler()));
            if (ex is EditingWhileNotLoggedInException)
                return new AjaxResult(AjaxResultCode.FAILURE, ErrorSignout); // Don't bother emailing this, nothing to worry about.

            ExceptionManager.HandleException(ex, new StandardErrorHandler());

            return new AjaxResult(AjaxResultCode.FAILURE, ErrorServer);
        }

        protected static void HandleException(string message)
        {
            // Throw and catch an exception to get the stack trace.

            try
            {
                throw new ApplicationException(message);
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, new StandardErrorHandler());
            }
        }

        private static string GetErrorMessage(UserException exception, IErrorHandler errorHandler)
        {
            if (exception is ValidationErrorsException)
                return GetErrorMessage(((ValidationErrorsException)exception).Errors, errorHandler);
            return errorHandler.FormatErrorMessage(exception);
        }

        private static string GetErrorMessage(IList<ValidationError> errors, IErrorHandler errorHandler)
        {
            // Just pick the first one and return that.

            if (errors.Count == 0)
                return string.Empty;
            return errorHandler.FormatErrorMessage(errors[0]);
        }

        protected static void EnsureMemberLoggedIn()
        {
            _authenticationManager.AuthenticateRequest(new HttpContextWrapper(HttpContext.Current));

            if (LoggedInMember == null)
                throw new EditingWhileNotLoggedInException("a member");
        }

        protected static void EnsureEmployerLoggedIn()
        {
            _authenticationManager.AuthenticateRequest(new HttpContextWrapper(HttpContext.Current));

            if (LoggedInEmployer == null)
                throw new EditingWhileNotLoggedInException("an employer");
        }
    }
}