using System;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Framework.Communications;

namespace LinkMe.Apps.Agents.Domain.Roles.Recruiters.Affiliations.Handlers
{
    public class AffiliationsHandler
        : IAffiliationsHandler
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly ICommunitiesQuery _communitiesQuery;

        public AffiliationsHandler(IEmailsCommand emailsCommand, ICommunitiesQuery communitiesQuery)
        {
            _emailsCommand = emailsCommand;
            _communitiesQuery = communitiesQuery;
        }

        void IAffiliationsHandler.OnEnquiryCreated(Guid affiliateId, AffiliationEnquiry enquiry)
        {
            var community = _communitiesQuery.GetCommunity(affiliateId);

            // This is a hack in place for the moment until proper communication settings are in place
            // for community administrators.

            if (community.Name == "Monash University Business and Economics")
            {
                var owners = new[]
                {
                    new EmailRecipient("Catherine.barratt@buseco.monash.edu.au", "Catherine Barratt", "Catherine", "Barratt"),
                    new EmailRecipient("julie.ralph@buseco.monash.edu.au", "Julie Ralph", "Julie", "Ralph")
                };
                _emailsCommand.TrySend(new AdministratorEmployerEnquiryEmail(community, owners, enquiry));
            }
            else
            {
                // This is the proper implementation.

                //                var owners = from id in Container.Current.Resolve<ICommunityOwnersCommand>().GetOwners(e.CommunityId) select Container.Current.Resolve<IRegisteredUserEmailBroker>().GetEmailIdentityForUser(id, UserRoles.CommunityAdministrator);
                //              _communicationEngine.Send(new AdministratorEmployerEnquiryEmail(community, owners, e.Enquiry));
            }

            // Need to send two emails, one to the employer and one to client services.

            _emailsCommand.TrySend(new EmployerCommunityEnquiryEmail(community, enquiry));
        }
    }
}