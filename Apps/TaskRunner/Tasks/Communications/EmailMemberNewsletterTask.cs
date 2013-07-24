using System;
using System.Collections.Generic;
using System.IO;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberUpdates;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Users.Members.Communications.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class EmailMemberNewsletterTask
        : CommunicationsTask
    {
        private static readonly EventSource EventSource = new EventSource<EmailMemberNewsletterTask>();
        private readonly ISettingsQuery _settingsQuery;
        private readonly Definition _definition;
        private readonly IMemberCommunicationsQuery _memberCommunicationsQuery; 
        private readonly IMembersQuery _membersQuery;

        private const int Period = 30;

        public EmailMemberNewsletterTask(IEmailsCommand emailsCommand, ISettingsQuery settingsQuery, IMemberCommunicationsQuery memberCommunicationsQuery, IMembersQuery membersQuery)
            : base(EventSource, emailsCommand)
        {
            _settingsQuery = settingsQuery;
            _definition = _settingsQuery.GetDefinition(typeof(MemberNewsletterEmail).Name);
            _memberCommunicationsQuery = memberCommunicationsQuery;
            _membersQuery = membersQuery;
        }

        public override void ExecuteTask()
        {
            ExecuteTask(new string[0]);
        }

        public override void ExecuteTask(string[] args)
        {
            const string method = "ExecuteTask";
            EventSource.Raise(Event.FlowEnter, method, "Entering task.", Event.Arg("args", args));

            // Check for a maximum number of members.

            string fileName = null;
            var range = new Range(0);
            if (args != null && args.Length > 0)
            {
                // The argument may be the max members to run or it might be a file name containing email addresses.

                int max;
                if (int.TryParse(args[0], out max))
                {
                    range = new Range(0, max);
                }
                else
                {
                    if (File.Exists(args[0]))
                        fileName = args[0];
                }
            }

            if (fileName == null)
            {
                // Users who have not already been sent an email.

                ExecuteNotSent(range);

                // Users who have already been sent an email but have not explicitly set the frequency.

                ExecuteAlreadySent(range);
            }
            else
            {
                // Email addresses as specified. This should only be used for testing as it bypasses the normal verification tests, etc.

                ExecuteFile(fileName);
            }

            EventSource.Raise(Event.FlowExit, method, "Exiting task.", Event.Arg("args", args));
        }

        public void ExecuteNotSent(Range range)
        {
            const string method = "ExecuteNotSent";
            EventSource.Raise(Event.Flow, method, "Sending status update emails to members who have not already been sent one.", Event.Arg("skip", range.Skip), Event.Arg("take", range.Take));

            var totalMembers = 0;
            var totalSent = 0;

            try
            {
                // All members with the appropriate status who have not been sent a status update email.

                var joinedBefore = DateTime.Now.AddDays(-1 * Period);
                var memberIds = _memberCommunicationsQuery.GetNotSentMemberIds(_definition.Id, joinedBefore, range);
                totalMembers = memberIds.Count;
                EventSource.Raise(Event.Flow, method, "Total members who have not been sent an email: " + totalMembers, Event.Arg("totalMembers", totalMembers));

                // Send each one an email.

                totalSent = Send(_membersQuery.GetMembers(memberIds), false);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.CriticalError, method, "Failed to send all status update emails.", ex, new StandardErrorHandler());
            }
            finally
            {
                EventSource.Raise(Event.Flow, method, totalSent + " status update emails have been sent to members who had not previously been sent one.", Event.Arg("skip", range.Skip), Event.Arg("take", range.Take), Event.Arg("totalMembers", totalMembers), Event.Arg("totalSent", totalSent));
            }
        }

        public void ExecuteAlreadySent(Range range)
        {
            const string method = "ExecuteAlreadySent";
            EventSource.Raise(Event.Flow, method, "Sending status update emails to members who have already been sent one.", Event.Arg("skip", range.Skip), Event.Arg("take", range.Take));

            var totalMembers = 0;
            var totalSent = 0;

            try
            {
                // All members with the appropriate status who have alredy been sent a newsletter email.

                var sentBefore = DateTime.Now.AddDays(-1 * Period);
                var memberIds = _memberCommunicationsQuery.GetSentMemberIds(_definition.Id, sentBefore, range);
                totalMembers = memberIds.Count;
                EventSource.Raise(Event.Flow, method, "Total members who have been sent an email: " + totalMembers, Event.Arg("totalMembers", totalMembers));

                // Send each one an email.

                totalSent = Send(_membersQuery.GetMembers(memberIds), false);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.CriticalError, method, "Failed to send all status update emails.", ex, new StandardErrorHandler());
            }
            finally
            {
                EventSource.Raise(Event.Flow, method, totalSent + " status update emails have been sent to members who have previously been sent one.", Event.Arg("skip", range.Skip), Event.Arg("take", range.Take), Event.Arg("totalMembers", totalMembers), Event.Arg("totalSent", totalSent));
            }
        }

        public void ExecuteFile(string fileName)
        {
            const string method = "ExecuteFile";
            EventSource.Raise(Event.Flow, method, "Sending status update emails to members contained in a file.", Event.Arg("fileName", fileName));

            var totalMembers = 0;
            var totalSent = 0;

            try
            {
                // Read the members.

                var members = new List<Member>();
                using (var reader = new StreamReader(fileName))
                {
                    var emailAddress = reader.ReadLine();
                    while (!string.IsNullOrEmpty(emailAddress))
                    {
                        var member = _membersQuery.GetMember(emailAddress);
                        if (member != null)
                            members.Add(member);
                        emailAddress = reader.ReadLine();
                    }
                }

                // All members with the appropriate status who have not been sent a status update email.

                totalMembers = members.Count;
                EventSource.Raise(Event.Flow, method, "Total members contained in file.", Event.Arg("totalMembers", totalMembers));

                // Send each one an email.

                totalSent = Send(members, true);
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.CriticalError, method, "Failed to send all status update emails.", ex, new StandardErrorHandler());
            }
            finally
            {
                EventSource.Raise(Event.Flow, method, "Status update emails have been sent to members in the file.", Event.Arg("fileName", fileName), Event.Arg("totalMembers", totalMembers), Event.Arg("totalSent", totalSent));
            }
        }

        private int Send(IEnumerable<Member> members, bool ignoreChecks)
        {
            const string method = "Send";

            // Send each one an email.

            var totalSent = 0;

            foreach (var member in members)
            {
                try
                {
                    // For the moment only send to non-community members.

                    if (member.AffiliateId == null)
                    {
                        if (_emailsCommand.TrySend(new MemberNewsletterEmail(member), ignoreChecks))
                            ++totalSent;
                    }
                }
                catch (Exception ex)
                {
                    EventSource.Raise(Event.NonCriticalError, method, "Failed to send a status update email to a member.", ex, new StandardErrorHandler(), Event.Arg("MemberEmailAddress", member.GetBestEmailAddress().Address));
                }
            }

            return totalSent;
        }
    }
}
