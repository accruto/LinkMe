using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Test.Communications.Mocks
{
    public class MockEmailServer
        : IMockEmailServer
    {
        private static readonly List<MockEmail> Emails = new List<MockEmail>();
        private static readonly List<string> BadEmailAddresses = new List<string>();

        void IMockEmailServer.Send(MockEmail email)
        {
            // Look for bad email addresses.

            foreach (var to in email.To)
            {
                foreach (var badEmailAddress in BadEmailAddresses)
                {
                    if (to.Address == badEmailAddress)
                        throw new ApplicationException("Cannot send email to '" + badEmailAddress + "'.");
                }
            }

            Emails.Add(email);
        }

        IList<MockEmail> IMockEmailServer.GetEmails()
        {
            return Emails;
        }

        void IMockEmailServer.ClearEmails()
        {
            Emails.Clear();
        }

        void IMockEmailServer.RegisterBadEmailAddress(string badEmailAddress)
        {
            BadEmailAddresses.Add(badEmailAddress);
        }
    }
}
