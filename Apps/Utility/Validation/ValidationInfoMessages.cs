using LinkMe.Utility.Utilities;

namespace LinkMe.Utility.Validation
{
	public class ValidationInfoMessages
	{
	    public const string INVITE_SENT = "Your invitation(s) have been sent.";
	    public const string INVITE_IGNORED = "{0} invitation was ignored.";
        public const string SEARCH_SAVED = "Your search has been saved as {0}.<BR>Go to saved searches to view other searches you have saved";
		public const string UPLOAD_RESUME_DELETES_OLD_ONE = "You already have a LinkMe resume. Posting a new resume will overwrite your current LinkMe resume.";
        public const string ALREADY_APPLIED_FOR_JOB = "You have already applied for this job.";
        public const string ALREADY_APPLIED_EXTERNAL_PENDING_FORMAT = "You have already submitted an application for this job, but have not yet completed the recruiter's application form.<br />{0}";
        public const string SIGNED_UP_COMPLETE_JOB_APP = "You are now a LinkMe member. Please complete your job application below.";
	    public const string YOUR_RESUME_EMAILED_FORMAT = "A copy of your resume has been emailed to {0}.";
	    public const string SUCCESSFULLY_APPLIED_FOR_JOB = "You have successfully applied for {0}";
        public const string SUCCESSFULLY_APPLIED_FOR_JOB_WITH_RESUME = "Your application and LinkMe resume for \"{0}\" have been sent.";
        public const string NO_CONTACTS_FOUND = "No contacts could be found.";
        public const string USER_DETAILS_UPDATED = "Your details were updated successfully.";
	    public const string CHANGES_SAVED = "Your changes have been saved.";
	    public const string CHANGES_SAVED_NEEDPHONE = "Your changes have been saved. However, for our partners to contact you we will need a phone number. You can update your profile with a phone number <a href=\"{0}\">here</a>.";
        public const string NO_CHANGES_MADE = "No changes have been made.";
	    public const string JOB_AD_SENT_TO_FRIEND = "The job ad has been sent to {0}";
        public const string YOUR_PASSWORD_CHANGED = "Your password has been changed.";
        public const string LOGGEDIN_ACCOUNT_ACTIVATED = "Thanks, your account has been activated.";
        public const string LOGGEDIN_EMAIL_VERIFIED = "Thanks, your email address has been verified.";
	    public const string ALREADYINVITED_TEXT = "You have already invited the following people:";
        public const string VISIBILITY_SETTINGS_SAVED = "Your visibility settings have been saved.";
	    public const string CONTACT_CREDIT_USED = "You have used one contact credit.";

	    public const string RESUME_UPLOADED_DURING_JOIN =
	        "Thanks, your account has been activated.<br />We know your resume is important.  You should review it to make sure everything is correct."+
	            "  We try hard but it doesn't always work perfectly.";

	    public const string RESUME_NOT_UPLOADED_DURING_JOIN =
	        "Thanks, your account has been activated.<br />You should create or upload your resume now so that potential employers can find you.";

        public const string ORGANISATION_UPDATED_FORMAT = "Organisation '{0}' has been updated.";
        public const string ORGANISATION_RENAMED_FORMAT = "Organisation '{0}' has been renamed to '{1}'.";
        public const string ORGANISATION_UPDATED_RENAMED_FORMAT = "Organisation '{0}' has been updated and renamed to '{1}'.";

        public const string MESSAGE_SENT = "Your message has been sent";
        public const string MESSAGE_ALREADY_SENT = "Your message has already been sent";

        public const string CREATING_ACCOUNTS_DONE_FORMAT = "Account '{0}' was created successfully."
            + " A total of {1} account{2} created for company '{3}'."
            + StringUtils.HTML_LINE_BREAK + StringUtils.HTML_LINE_BREAK
            + "You can now set up reports for this company if you wish.";

	    public const string LOGGEDIN_INSTEAD_OF_JOINING = "You have been logged in with your existing account. In the future you can simply <strong>log in</strong> instead of filling out the join form again.";

        public const string LOG_IN_TO_CONTINUE = "Please log in to continue.";
        public const string LOG_IN_OR_JOIN_TO_CONTINUE = "Please log in or join to continue.";
        public const string LOG_IN_OR_JOIN_TO_CONTINUE_FORMAT = "Please log in or <a href=\"{0}\">join</a> to continue.";
    }
}
