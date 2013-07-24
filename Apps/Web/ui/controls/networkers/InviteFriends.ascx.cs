using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LinkMe.Common;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Donations.Commands;
using LinkMe.Domain.Donations.Queries;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Configuration;
using LinkMe.Utility.Validation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class InviteFriends : LinkMeUserControl
    {
        private static readonly IDonationsQuery _donationsQuery = Container.Current.Resolve<IDonationsQuery>();
        private static readonly IDonationsCommand _donationsCommand = Container.Current.Resolve<IDonationsCommand>();
        private static readonly IMemberFriendsCommand _memberFriendsCommand = Container.Current.Resolve<IMemberFriendsCommand>();
        private static readonly IMemberFriendsQuery _memberFriendsQuery = Container.Current.Resolve<IMemberFriendsQuery>();
        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();

        private const string SessionKey = "InviteFriends_emailAddresses";

        private static readonly decimal DefaultDonationAmount = ApplicationContext.Instance.GetDecimalProperty(ApplicationContext.DONATION_AMOUNT);

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            reqValEmailAddresses.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_TO;
            
            // The length validator infers the max length from the control it validates, so we need
            // to set it here.
            txtBody.MaxLength = DomainConstants.UserToUserRequestMessageMaxLength;
            valTextLength.ErrorMessage =
                String.Format(ValidationErrorMessages.MAX_LENGTH_EXCEEDED_FORMAT, "message field", DomainConstants.UserToUserRequestMessageMaxLength);

            // Setup the error image hack! Need to display a validator image after actually
            // running through the control, and repopulating it.
            invalidEmailsErrorImage.ImageUrl = ApplicationPath +"/ui/images/universal/error.png";

            SetUpDonationRecipients();
        }

        public void AddContacts(IList<ICommunicationRecipient> contacts)
        {
            RestoreEmailAddresses();

            foreach (var contact in contacts)
            {
                if (txtEmailAddresses.Text.Length == 0)
                    txtEmailAddresses.Text = contact.EmailAddress;
                else
                    txtEmailAddresses.Text += ", " + contact.EmailAddress;
            }
        }

        protected static string DonationAmount
        {
            get { return DefaultDonationAmount.ToString("C"); }
        }

        private void SetUpDonationRecipients()
        {
            var recipients = _donationsQuery.GetRecipients();
            if (recipients.Count == 0)
            {
                phDonation.Visible = false;
            }
            else
            {
                phDonation.Visible = true;

                // Assumed that there is only one donation recipient so use it.
                // If there are ever more than this then the use of checkboxes / radio buttons etc
                // needs to be revisited.  The main problem is once a reciepient has been chosen
                // how do you unchoose all recipients.

                Debug.Assert(recipients.Count == 1);
                DonationRecipient recipient = recipients[0];
                chkDonationRecipient.Text = recipient.Name;
                chkDonationRecipient.Checked = false;
            }
        }

        public string EmailAddresses
        {
            get { return txtEmailAddresses.Text;  }
            set { txtEmailAddresses.Text = value; }
        }

        public bool SendInvitations()
        {
            Page.Validate();
            if (!Page.IsValid)
                return false;

            try
            {
                string[] emailAddresses = TextUtil.SplitEmailAddresses(txtEmailAddresses.Text);
                
                if (emailAddresses != null)
                {
                    txtEmailAddresses.Text = "";

                    // Filter out all invalid emails
                    var validEmails = new List<string>();
                    var invalidEmails = new List<string>();
                    var alreadyInvitedEmails = new Dictionary<String, DateTime>();

                    bool ownEmailSupplied = false;
                    var existingInvites = _memberFriendsQuery.GetFriendInvitations(LoggedInMember.Id);

                    foreach (string email in emailAddresses)
                    {
                        IValidator validator = EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.SingleEmail, false);
                        var errors = validator.IsValid(email)
                            ? null
                            : validator.GetValidationErrors("EmailAddress");

                        if (errors == null || errors.Length == 0)
                        {
                            if ((String.Compare(LoggedInMember.GetBestEmailAddress().Address, email, true)) == 0)
                            {
                                ownEmailSupplied = true;
                                continue;
                            }

                            var existingInvite = GetInviteForEmail(email, existingInvites);

                            if (existingInvite != null && !_memberFriendsCommand.CanSendInvitation(existingInvite))
                            {
                                if (existingInvite.LastSentTime == null)
                                    throw new ArgumentNullException("The last sending time was not set, but invite sending was not allowed.");
                                
                                alreadyInvitedEmails.Add(email, existingInvite.LastSentTime.Value);
                                continue;
                            }

                            validEmails.Add(email);
                            continue;
                        }

                        invalidEmails.Add(email);
                    }

                    // Create a donation request.

                    DonationRequest request = GetDonationRequest();

                    // Create invitations.

                    var duplicateFriends = SendFriendInvitations(LoggedInMember.Id, validEmails, txtBody.Text, request == null ? (Guid?)null : request.Id);

                    foreach (string duplicateEmail in duplicateFriends)
                    {
                        validEmails.Remove(duplicateEmail);
                    }

                    // Display all errors
                    if(invalidEmails.Count > 0)
                    {
                        string invalidEmailsToReproccess = String.Empty;
                        for(int i = 0; i < invalidEmails.Count; i++)
                        {
                            if (i != 0)
                                invalidEmailsToReproccess += ",";

                            invalidEmailsToReproccess += invalidEmails[i];
                        }

                        txtEmailAddresses.Text = invalidEmailsToReproccess;

                        // Setup the styles to display the mock validator inline
                        txtEmailAddresses.Style.Add("float", "left");

                        invalidEmailsPanel.Style.Add("float", "left");
                        invalidEmailsPanel.Style.Add("width", "160px");
                        invalidEmailsPanel.Style.Add("padding-left", "20px");


                        // Show the error image and display error text
                        invalidEmailsPanel.Visible = true;
                    }
                    
                    if (duplicateFriends.Count > 0)
                    {
                        alreadyFriendsPanel.Visible = true;
                        var sb = new StringBuilder();
                        foreach (string email in duplicateFriends)
                            sb.AppendLine(email + "<br />");

                        alreadyFriendsList.InnerHtml = sb.ToString();
                    }

                    if (alreadyInvitedEmails.Keys.Count > 0)
                    {
                        duplicateInvitesPanel.Visible = true;
                        var sb = new StringBuilder();
                        foreach (KeyValuePair<String, DateTime> emailAndDatePair in alreadyInvitedEmails)
                        {
                            int resendableDays = Container.Current.Resolve<int>("linkme.domain.roles.networking.invitationResendableDays");
                            DateTime dateLastSent = emailAndDatePair.Value;
                            int daysBeforeResend = (dateLastSent.AddDays(resendableDays) - DateTime.Now).Days;

                            string whenCanBeResentDescription;

                            if (daysBeforeResend == 0)
                                whenCanBeResentDescription = String.Format("today at {0}", dateLastSent.ToShortTimeString());
                            else if (daysBeforeResend == 1)
                                whenCanBeResentDescription = "tomorrow";
                            else 
                                whenCanBeResentDescription = String.Format("in {0} days", daysBeforeResend);
                            
                            sb.AppendLine(String.Format("{0} (Can be resent {1}.)<br />", emailAndDatePair.Key, whenCanBeResentDescription));
                        }
                        duplicateList.InnerHtml = sb.ToString();
                    }

                    if(ownEmailSupplied)
                        LinkMePage.AddError(ValidationErrorMessages.INVITE_YOURSELF);

                    if (validEmails.Count > 0)
                    {
                        if (request != null)
                            donationWillBeMade.Visible = true;

                        invitesSentPanel.Visible = true;
                        var sb = new StringBuilder();
                        foreach (string email in validEmails)
                            sb.AppendLine(email + "<br />");

                        invitesSent.InnerHtml = sb.ToString();
                    }
                    
                    return true;
                }
                
                return false;
            }
            catch (DailyLimitException)
            {
                LinkMePage.AddError(ValidationErrorMessages.DAILY_LIMIT_EXCEEDED);
                return false;
            }
        }

        private static IList<string> SendFriendInvitations(Guid inviterId, IEnumerable<string> inviteeEmailAddresses, string text, Guid? donationRequestId)
        {
            var firstDegreeContacts = new List<string>();

            foreach (var emailAddress in inviteeEmailAddresses.Distinct())
            {
                if (emailAddress != null && emailAddress.Trim().Length != 0)
                {
                    // Check whether they are an existing member.

                    var invitee = _membersQuery.GetMember(emailAddress);
                    if (invitee != null)
                    {
                        // A member cannot invite themselves.

                        if (invitee.Id != inviterId)
                        {
                            // Do not invite if they are already linked.

                            if (_memberContactsQuery.AreFirstDegreeContacts(invitee.Id, inviterId))
                            {
                                firstDegreeContacts.Add(emailAddress);
                            }
                            else
                            {
                                // Invitation for an already existing member (no donation request for existing members).

                                _memberFriendsCommand.SendInvitation(new FriendInvitation {InviteeId = invitee.Id, InviterId = inviterId, Text = text});
                            }
                        }
                    }
                    else
                    {
                        // Not a member so send them a new invitation.

                        _memberFriendsCommand.SendInvitation(new FriendInvitation { DonationRequestId = donationRequestId, InviteeEmailAddress = emailAddress, InviterId = inviterId, Text = text });
                    }
                }
            }

            return firstDegreeContacts.ToList();
        }

        private static FriendInvitation GetInviteForEmail(string emailToMatch, IEnumerable<FriendInvitation> invites)
        {
            FriendInvitation existingInvite = null;

            foreach (var invite in invites)
            {
                // If an invite is created to an existing member already
                if ((invite.InviteeId != null && _membersQuery.GetMember(invite.InviteeId.Value).GetBestEmailAddress().Address == emailToMatch) || 
                    // Or if the invite is to join linkme
                    (invite.InviteeEmailAddress != null && invite.InviteeEmailAddress == emailToMatch))
                {
                    existingInvite = invite;
                    break;
                }
            }

            return existingInvite;
        }

        private DonationRequest GetDonationRequest()
        {
            if (chkDonationRecipient.Checked)
            {
                var recipients = _donationsQuery.GetRecipients();
                if (recipients.Count == 0)
                {
                    return null;
                }

                // Find the recipient.

                foreach (DonationRecipient recipient in recipients)
                {
                    if (recipient.Name == chkDonationRecipient.Text)
                        return FindOrCreateDonationRequest(recipient);
                }
            }

            return null;
        }

        private static DonationRequest FindOrCreateDonationRequest(DonationRecipient recipient)
        {
            var request = new DonationRequest {Amount = DefaultDonationAmount, RecipientId = recipient.Id};
            _donationsCommand.CreateRequest(request);
            return request;
        }

        public void SaveEmailAddresses()
        {
            if (txtEmailAddresses.Text.Length > 0)
            {
                Session[SessionKey] = txtEmailAddresses.Text;
            }
        }

        private void RestoreEmailAddresses()
        {
            txtEmailAddresses.Text = Session[SessionKey] as string;
            Session.Remove(SessionKey);
        }
    }
}
