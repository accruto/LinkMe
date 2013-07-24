using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility;
using LinkMe.Utility.Utilities;
using LinkMe.Web.Helper;
using LinkMe.Apps.Asp.Exceptions;
using LinkMe.Apps.Asp.Security;

namespace LinkMe.Web.Service
{
    public abstract class SimpleWebServiceHandler : IHttpHandler
    {
        protected static readonly IAuthenticationManager _authenticationManager = Container.Current.Resolve<IAuthenticationManager>();
        private static readonly IFilesQuery _filesQuery = Container.Current.Resolve<IFilesQuery>();

        public const string SuccessResponse = "Success";
        public const string NotLoggedInResponse = "NotLoggedIn";
        public const string GenericFailureResponse = "Failed";
        public const string ErrorPrefix = "Error:";

        #region IHttpHandler members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            CheckAuthorization();

            try
            {
                ProcessRequestImpl(context);
            }
            catch (ServiceEndUserException ex)
            {
                ExceptionManager.HandleException(ex, new StandardErrorHandler());
                WriteError(context.Response, ex.Message, true);
            }
            catch (UserException ex)
            {
                WriteError(context.Response, GetErrorMessage(ex, new StandardErrorHandler()), true);
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex, new StandardErrorHandler());

                string message = "Failed to process " + GetType().Name + " web service request.";
                if (Container.Current.Resolve<bool>("dev.always.logged.in"))
                {
                    message += System.Environment.NewLine + System.Environment.NewLine + ex;
                }
                else
                {
                    message += " Check the log for details.";
                }

                WriteError(context.Response, message, false);
            }
        }

        private void CheckAuthorization()
        {
            var userTypes = AuthorizedUserTypes;
            if (userTypes != null)
            {
                if (userTypes.Length == 0)
                    EnsureNotAuthorized();
                else
                    EnsureAuthorized(userTypes);
            }
        }

        private static void EnsureNotAuthorized()
        {
            var userId = HttpContext.Current.User.Id();
            if (userId != null)
                NavigationManager.Redirect(HttpContext.Current.GetReturnUrl());
        }

        private static void EnsureAuthorized(IList<UserType> userTypes)
        {
            // Check that the current user matches the role.

            var userId = HttpContext.Current.User.Id();
            if (userId == null)
            {
                // Redirect to the appropriate log in page for the first role.

                var userType = userTypes.Count == 0 ? UserType.Member : userTypes[0];
                if (userType == UserType.Employer)
                    NavigationManager.Redirect(HttpContext.Current.GetEmployerLoginUrl());
                NavigationManager.Redirect(HttpContext.Current.GetLoginUrl());
            }
            else
            {
                // If e.g. an employer is trying to access a member page, then redirect them home.

                if (!userTypes.Contains(HttpContext.Current.User.UserType()))
                    NavigationManager.Redirect(HttpContext.Current.GetHomeUrl());
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

        #endregion

        #region Static methods

        protected static void WriteFileToOutput(HttpContext context, FileReference file, bool asAttachment)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (file == null)
                throw new ArgumentNullException("file");

            using (var fileStream = _filesQuery.OpenFile(file))
            {
                context.Response.Clear();
                HttpHelper.SetResponseToFileDownload(context.Response, (asAttachment ? file.FileName : null), file.MediaType, fileStream.Length);
                StreamHelper.CopyStream(fileStream, context.Response.OutputStream);
            }
        }

        protected static Guid? GetMemberId(HttpContext context)
        {
            return GetUserId(context, UserType.Member);
        }

        protected static Guid GetMemberIdOrThrow(HttpContext context)
        {
            Guid? userId = GetMemberId(context);
            if (!userId.HasValue)
                throw new UserException("You must be logged on as a member to access this service.");

            return userId.Value;
        }

        protected static Guid? GetEmployerId(HttpContext context)
        {
            return GetUserId(context, UserType.Employer);
        }

        protected static Guid GetEmployerIdOrThrow(HttpContext context)
        {
            Guid? userId = GetEmployerId(context);
            if (!userId.HasValue)
                throw new UserException("You must be logged on as an employer to access this service.");

            return userId.Value;
        }

        protected static Guid? GetAdministratorId(HttpContext context)
        {
            return GetUserId(context, UserType.Administrator);
        }

        protected static Guid GetAdministratorIdOrThrow(HttpContext context)
        {
            Guid? userId = GetAdministratorId(context);
            if (!userId.HasValue)
                throw new UserException("You must be logged on as an administrator to access this service.");

            return userId.Value;
        }

        private static Guid? GetUserId(HttpContext context, UserType requiredRole)
        {
            Debug.Assert(context != null, "context != null");
            Debug.Assert(requiredRole != UserType.Anonymous, "requiredRole != UserRoles.Anonymous");

            return context.User.UserType() != requiredRole ? null : context.User.Id();
        }

        #endregion

        protected abstract UserType[] AuthorizedUserTypes { get; }
        protected abstract void ProcessRequestImpl(HttpContext context);

        /// <summary>
        /// Override to return a custom error message if the caller expects the response in a specific
        /// format. The base implementation simply returns the error message prefixed with "Error: ".
        /// </summary>
        protected virtual string GetOutputErrorMessage(string errorMessage, bool showErrorToEndUser)
        {
            if (errorMessage == NotLoggedInResponse)
                return errorMessage;
            if (showErrorToEndUser)
                return ErrorPrefix + " " + errorMessage;
            return GenericFailureResponse;
        }

        protected virtual string GetErrorContentType()
        {
            return "text/plain";
        }

        protected static T EnsureUserLoggedIn<T>(HttpContext context)
            where T : RegisteredUser
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var loggedIn = _authenticationManager.GetUser(new HttpContextWrapper(context)) as T;
            if (loggedIn == null)
                throw new UserException(NotLoggedInResponse);

            return loggedIn;
        }

        private void WriteError(HttpResponse response, string message, bool showErrorToEndUser)
        {
            response.ClearHeaders();
            response.ClearContent();
            response.ContentType = GetErrorContentType();
            response.Write(GetOutputErrorMessage(message, showErrorToEndUser));
        }
    }
}
