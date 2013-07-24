using System;
using System.Collections.Generic;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.Commands
{
    public interface IEmailsCommand
    {
        bool TrySend(TemplateEmail email);
        bool TrySend(TemplateEmail email, bool ignoreChecks);
        bool TrySend(TemplateEmail email, DateTime? notIfLastSentLaterThanThis);

        int TrySend(IEnumerable<TemplateEmail> emails, TemplateContentItem templateContentItem, bool ignoreChecks);
        int TrySend(IEnumerable<TemplateEmail> emails, TemplateContentItem templateContentItem, DateTime? notIfLastSentLaterThanThis);

        Communication GeneratePreview(TemplateEmail email);
        Communication GeneratePreview(TemplateEmail email, TemplateContentItem templateContentItem);
    }
}