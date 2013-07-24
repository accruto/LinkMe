using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberEmails
{
    public class ResumeAttachmentEmail
        : MemberEmail
    {
        private readonly ResumeFile _resumeFile;

        public ResumeAttachmentEmail(ICommunicationUser to, ResumeFile resumeFile)
            : base(to)
        {
            _resumeFile = resumeFile;
        }

        public override TempFileCollection GetTempFileAttachments()
        {
            return Container.Current.Resolve<IFilesCommand>().SaveTempFile(_resumeFile.Contents, _resumeFile.FileName);
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}