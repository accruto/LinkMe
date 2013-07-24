using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.Queries
{
    public interface IAffiliateEmailsQuery
    {
        Guid? GetAffiliateId(TemplateEmail email);
        ICommunicationUser GetReturnUser(Guid? affiliateId);
        ICommunicationUser GetServicesUser(ICommunicationUser from, Guid? affiliateId);
        ICommunicationUser GetMemberServicesUser(Guid? affiliateId);
        ICommunicationUser GetEmployerServicesUser(Guid? affiliateId);
    }
}