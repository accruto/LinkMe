using System;
using System.Collections.Specialized;
using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Helper;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Service
{
    /// <summary>
    /// Logs a UserEvent with the details specified in POST data. This allows logging a UserEvent from JavaScript.
    /// </summary>
    public class LogUserEvent : SimpleWebServiceHandler
    {
        private const int ApplyForExternalJobAd = 27;

        public const string EventTypeParam = "type";
        public const string CheckCodeParam = "check";
        public const string JobAdIdParam = "jobAdId";

        #region Static properties

        private static readonly IApplicationsCommand _applicationsCommand = Container.Current.Resolve<IApplicationsCommand>();

        #endregion

        #region Static methods

        public static string GetCheckCode(NameValueCollection parameters)
        {
            string onestring = "";

            for (int i = 0; i < parameters.Count; ++i)
            {
                string key = parameters.GetKey(i);
                if (key != CheckCodeParam)
                {
                    onestring += key + "=" + parameters.Get(i) + "\n";
                }
            }

            return EncryptionHelper.GetKeyedHashAsHexString(onestring);
        }

        public static string GetParamsForJobAd(int eventType, Guid jobAdId)
        {
            var parameters = new NameValueCollection
                                 {
                                     {EventTypeParam, eventType.ToString()},
                                     {JobAdIdParam, jobAdId.ToString()}
                                 };
            parameters.Add(CheckCodeParam, GetCheckCode(parameters));

            return new QueryString(parameters).ToString();
        }


        private static void VerifyCheckCode(NameValueCollection parameters)
        {
            string expectedCheckCode = GetCheckCode(parameters);
            string actualCheckCode = parameters[CheckCodeParam];

            if (actualCheckCode != expectedCheckCode)
                throw new UserException("The check code is invalid.");
        }

        #endregion

        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override void ProcessRequestImpl(HttpContext context)
        {
            var parameters = context.Request.Form;
            if (parameters.Count == 0)
                throw new UserException("No parameters were specified.");

            VerifyCheckCode(parameters);

            var type = ParseUtil.ParseUserInputInt32(parameters[EventTypeParam], "event type");

            // Only allow logging specific event types via this service (though it can be easily extended
            // to handle others).

            switch (type)
            {
                case ApplyForExternalJobAd:

                    var jobAdId = ParseUtil.ParseUserInputGuid(parameters[JobAdIdParam], "job ad ID");
                    _applicationsCommand.CreateApplication(new ExternalApplication { PositionId = jobAdId, ApplicantId = GetMemberIdOrThrow(context) });
                    break;

                default:
                    throw new UserException("The specified event type is not supported.");
            }

            context.Response.Write(SuccessResponse);
        }
    }
}
