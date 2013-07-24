using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Files;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class JobApplicationEmail
        : EmployerEmail
    {
        private readonly InternalApplication _jobApplication;
        private readonly JobAd _jobAd;
        private readonly Stream _resumeContents;
        private readonly string _resumeFileName;

        public JobApplicationEmail(ICommunicationUser from, InternalApplication jobApplication, JobAd jobAd, Stream resumeContents, string resumeFileName)
            : base(GetUnregisteredEmployer(jobAd.ContactDetails.EmailAddress), from)
        {
            _jobApplication = jobApplication;
            _jobAd = jobAd;
            _resumeContents = resumeContents;
            _resumeFileName = resumeFileName;
        }

        public override IList<ICommunicationUser> Copy
        {
            get
            {
                return string.IsNullOrEmpty(_jobAd.ContactDetails.SecondaryEmailAddresses)
                    ? null
                    : TextUtil.SplitEmailAddresses(_jobAd.ContactDetails.SecondaryEmailAddresses).Select(e => GetUnregisteredEmployer(e)).Cast<ICommunicationUser>().ToList();
            }
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("JobTitle", _jobAd.Title);
            properties.Add("ExternalReferenceId", _jobAd.Integration.ExternalReferenceId);
            properties.Add("ApplicantId", _jobApplication.ApplicantId);
            properties.Add("Content", _jobApplication.CoverLetterText);
        }

        public override IList<CommunicationAttachment> GetAttachments()
        {
            return _resumeContents == null
                ? null
                : new List<CommunicationAttachment>
                  {
                      new ContentAttachment(_resumeContents, _resumeFileName, MediaType.GetMediaTypeFromExtension(Path.GetExtension(_resumeFileName), MediaType.Text))
                  };
        }
    }
}