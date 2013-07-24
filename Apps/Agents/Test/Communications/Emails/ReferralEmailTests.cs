using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    public abstract class ReferralEmailTests
        : EmailTests
    {
        protected const string ContactFirstName = "Monty";
        protected const string ContactName = "Monty Burns";
        protected const string CompanyName = "Springfield Nuclear Power Plant";
        protected const string ContactPosition = "CEO";
        protected const string ContactPhoneNumber = "99999999";
        protected const string ContactEmailAddress = "monty@test.linkme.net.au";
        protected const string ContactState = "VIC";
        protected const string ContactWebsite = "http://test.external.com/";
        protected const string ContactEmployees = "50";
        protected const string ContactNotes = "Because they are good";

        protected static ReferralContact CreateContact(bool includeWebsite)
        {
            return new ReferralContact
            {
                Name = ContactName,
                Company = CompanyName,
                Job = ContactPosition,
                PhoneNumber = ContactPhoneNumber,
                EmailAddress = ContactEmailAddress,
                State = ContactState,
                Website = includeWebsite ? ContactWebsite : null,
                Employees = ContactEmployees,
            };
        }
    }
}