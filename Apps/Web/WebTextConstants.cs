namespace LinkMe.Web
{
    public class WebTextConstants
    {
        public const string TEXT_NO_INVITATIONS_SENT_1 = "You have not sent any invitations or they have all been responded to.";
        public const string TEXT_NO_INVITATIONS_SENT_2 = "Staying in touch with current and former colleagues is a great way to increase your chances of winning that next great job and enhancing your career.";

        public const string LINK_WRITE_ON_MEMBERS_WHITEBOARD = "Write on {0} " + TEXT_NETWORKER_WHITEBOARD;
        public const string MESSAGE_WHITEBOARD_MESSAGE_DELETED = "{0} message deleted.";
        public const string NO_HTML_OR_XML_TAGS_ALLOWED = "Sorry, HTML and XML tags are not allowed.";
        public const string TEXT_NETWORKER_WHITEBOARD = "message wall";
        public const string TEXT_GROUP_WHITEBOARD = "Group notice board";
        public const string TEXT_EVENT_WHITEBOARD = "Event Whiteboard";

        public const string TEXT_WRITE_MESSAGE_HERE = "Write your message here";

        public const string GROUPS_CONFIRM_CREATION = "Thank you. Your group has been created.";
        public const string GROUPS_CONFIRM_EDIT = "Thank you. Your group's details have been saved.";

        public const string GROUPS_EMAIL_DOMAIN_NOT_ALLOWED =
            "The supplied email domain is not supported. Please choose another.";

        public const string GROUPS_CONTRIBUTOR_BANNED =
            "Sorry, you have been banned from joining this group. Please contact the group's administrator if you have any further questions.";

        public const string GROUPS_JOIN_RULES_CHANGED =
            "Sorry, the administrator of this group has just revised the membership rules and you are no longer allowed to join. Please contact the group's administrator if you have any further questions.";

        public const string GROUPS_CANNOT_REVOKE_LAST_ADMIN =
            "Cannot revoke your membership while you are the only administrator for this group.<br />Please nominate another administrator using the <a href=\"{0}\">Manage group wizard</a>.";

        public const string GROUPS_WHITEBOARD_DELETE_MESSAGE = "Whiteboard message deleted";

        public const string SENDER_NAME_FORMAT = "<span class=\"sender-name\">{0}</span>";
        public const string TEXT_YOU = "You";

        public static readonly string[] CREATE_GROUP_PAGE_NAMES = { "Group details", "Group privacy", "Invite people" };
        public static readonly string[] CREATE_GROUP_EVENT_PAGE_NAMES = { "Event details", "Event settings", "Send invitations" };
        public static readonly string[] EDIT_GROUP_PAGE_NAMES = { "Group details", "Group privacy" };

        public static readonly string[] BANNED_EMAIL_DOMAINS = {
                                                                "animail.net",
                                                                "aol.com",
                                                                "aussiemail.com.au",
                                                                "bluebottle.com",
                                                                "boardermail.com",
                                                                "canada.com",
                                                                "canoemail.com",
                                                                "dbzmail.com",
                                                                "dcemail.com",
                                                                "didamail.com",
                                                                "doramail.com",
                                                                "e-mailanywhere.com",
                                                                "emumail.com",
                                                                "fastmail.fm",
                                                                "freemailbox.net",
                                                                "gmail.com",
                                                                "hotmail.co.uk",
                                                                "hotmail.com",
                                                                "hotmail.com.au",
                                                                "inbox.com",
                                                                "live.com",
                                                                "mail.com",
                                                                "mail2web.com",
                                                                "mailinator.com",
                                                                "moose-mail.com",
                                                                "optuszoo.com.au",
                                                                "pobox.com",
                                                                "snail-mail.net",
                                                                "webmail.co.za",
                                                                "whale-mail.com",
                                                                "wildmail.com",
                                                                "yahoo.com",
                                                                "yahoo.com.au",
                                                                "yahoo.co.uk",
                                                            };
    }
}
