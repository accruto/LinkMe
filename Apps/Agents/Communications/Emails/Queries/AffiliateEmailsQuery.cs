using System;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;

namespace LinkMe.Apps.Agents.Communications.Emails.Queries
{
    public class AffiliateEmailsQuery
        : IAffiliateEmailsQuery
    {
        private readonly IVerticalsCommand _verticalsCommand;

        public AffiliateEmailsQuery(IVerticalsCommand verticalsCommand)
        {
            _verticalsCommand = verticalsCommand;
        }

        Guid? IAffiliateEmailsQuery.GetAffiliateId(TemplateEmail email)
        {
            // If the communication has explicitly set it then return.

            if (email.AffiliateId != null)
                return email.AffiliateId;

            if (email is MemberEmail)
            {
                // Use who it is going to if they are a user.

                if (email.To.UserType != UserType.Anonymous)
                    return GetAffiliateId(email.To.AffiliateId);

                // Now try who it is being sent from.

                if (email.From != null && email.From.UserType != UserType.Anonymous)
                    return GetAffiliateId(email.From.AffiliateId);
            }
            else if (email is EmployerEmail)
            {
                var employer = GetEmployer(email.To);
                if (employer != null)
                {
                    // Look for a primary, approved community.

                    if (employer.Organisation != null && employer.Organisation.AffiliateId != null)
                        return GetAffiliateId(employer.Organisation.AffiliateId);
                }
            }

            // Check the current context.

            return ActivityContext.Current.Vertical.Id;
        }

        ICommunicationUser IAffiliateEmailsQuery.GetReturnUser(Guid? affiliateId)
        {
            return GetUser(affiliateId, v => v.ReturnEmailAddress);
        }

        ICommunicationUser IAffiliateEmailsQuery.GetServicesUser(ICommunicationUser from, Guid? affiliateId)
        {
            return GetUser(affiliateId, v => from.UserType == UserType.Employer ? v.EmployerServicesEmailAddress : v.MemberServicesEmailAddress);
        }

        ICommunicationUser IAffiliateEmailsQuery.GetMemberServicesUser(Guid? affiliateId)
        {
            return GetUser(affiliateId, v => v.MemberServicesEmailAddress);
        }

        ICommunicationUser IAffiliateEmailsQuery.GetEmployerServicesUser(Guid? affiliateId)
        {
            return GetUser(affiliateId, v => v.EmployerServicesEmailAddress);
        }

        private ICommunicationUser GetUser(Guid? affiliateId, Func<Vertical, string> getEmailAddress)
        {
            var vertical = GetVertical(affiliateId);
            if (vertical == null)
                return null;

            if (string.IsNullOrEmpty(getEmailAddress(vertical)) || string.IsNullOrEmpty(vertical.EmailDisplayName))
                return null;

            return new EmailUser(getEmailAddress(vertical), vertical.EmailDisplayName, null);
        }

        private Guid? GetAffiliateId(Guid? affiliateId)
        {
            // If the affiliation is a vertical then it needs to be enabled.

            return GetVertical(affiliateId) == null ? null : affiliateId;
        }

        private Vertical GetVertical(Guid? affiliateId)
        {
            if (affiliateId == null)
                return null;

            var vertical = _verticalsCommand.GetVertical(affiliateId.Value);
            if (vertical == null)
                return null;

            return vertical.IsDeleted ? null : vertical;
        }

        private static IEmployer GetEmployer(ICommunicationUser user)
        {
            if (user is IEmployer)
                return (IEmployer)user;
            return null;
        }
    }
}