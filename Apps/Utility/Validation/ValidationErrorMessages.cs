namespace LinkMe.Utility.Validation
{
    public class ValidationErrorMessages
    {
        // Generic messages.
        public const string MAX_LENGTH_EXCEEDED_FORMAT = "The {0} must not be longer than {1} characters.";

        // REQUIRED FIELD MESSAGES
        public const string REQUIRED_FIELD_COVER_LETTER = "Cover letter field is required.";
        public const string REQUIRED_FIELD_SALARY = "Annual salary is required.";
        public const string REQUIRED_FIELD_STATE = "State field is required.";
        public const string REQUIRED_FIELD_STREET = "Street No/Name field is required.";
        public const string REQUIRED_FIELD_SUBURB = "Suburb/State/Postcode field is required.";
        public const string REQUIRED_FIELD_POST_CODE = "Postcode field is required.";
        public const string REQUIRED_FIELD_LOCATION = "Suburb / State / Postcode field is required.";
        public const string REQUIRED_FIELD_USERID = "Login ID field is required.";
        public const string REQUIRED_FIELD_FILE_NAME = "File Name field is required.";
        public const string REQUIRED_FIELD_TO = "Recipient's Email Address field is required.";
        public const string REQUIRED_FIELD_SUBJECT = "Please include a subject.";
        public const string REQUIRED_FIELD_BODY = "Please include a message.";
        public const string REQUIRED_FIELD_FROM = "From field is required";
        public const string REQUIRED_FIELD_SAVE_FILENAME = "Save As field is required";
        public const string REQUIRED_FIELD_ORGANISATION_NAME = "The organisation name is required.";
        public const string REQUIRED_FIELD_INSTITUTION = "Institution field is required.";
        public const string REQUIRED_FIELD_QUALIFICATION = "Qualification field is required.";
        public const string REQUIRED_FIELD_END_DATE = "End Date field is required.";
        public const string REQUIRED_FIELD_START_DATE = "Start Date field is required.";
        public const string REQUIRED_FIELD_DESCRIPTION = "Description field is required.";
        public const string REQUIRED_FIELD_TITLE = "Title field is required.";
        public const string REQUIRED_FIELD_FIRST_NAME = "First Name field is required.";
        public const string REQUIRED_FIELD_SEARCH_CRITERIA = "Search criteria is required.";
        public const string REQUIRED_FIELD_LAST_NAME = "Last Name field is required.";
        public const string REQUIRED_FIELD_EMAIL_ADDRESS = "Email Address field is required.";
        public const string REQUIRED_FIELD_CURRENT_PASSWORD = "Current Password field is required.";
        public const string REQUIRED_FIELD_NEW_PASSWORD = "Password field is required.";
        public const string REQUIRED_FIELD_CONFIRM_PASSWORD = "Confirm password field is required.";
        public const string REQUIRED_FIELD_RESUME_DETAILS = "You have not entered any resume details.";
        public const string REQUIRED_FIELD_CREDIT_CARD_NUMBER = "Credit Card Number required";
        public const string REQUIRED_FIELD_NAME = "Name field is required.";
        public const string REQUIRED_FIELD_CONTACT_PHONE = "A contact phone number is required.";
        public const string REQUIRED_FIELD_COUNTRY = "Country field is required.";
        public const string REQUIRED_FIELD_ORGANISATION = "You must include an organisation name.";
        public const string REQUIRED_FIELD_RECRUITER_ROLE = "You must select your role.";
        public const string REQUIRED_CREDIT_REASON = "A reason must be selected from the list when adding credits.";
        public const string REQUIRED_FIELD_REQUEST_OPPORTUNITY_NAME = "You must provide a unique opportunity name";
        public const string REQUIRED_FIELD_REQUEST_EXPIRY_DATE = "You must provide an expiry date (greater than today)";
        public const string REQUIRED_FIELD_REQUEST_WORK_TYPE = "You must select a work type";
        public const string REQUIRED_FIELD_REQUEST_COMPANY_SIZE = "You must select a company size";
        public const string REQUIRED_FIELD_REQUEST_INDUSTRY = "You must select an industry";
        public const string REQUIRED_FIELD_REQUEST_STATE_LOCATION = "You must enter a state and location or specify the other location field";
        public const string REQUIRED_FIELD_REQUEST_JOB_TITLE = "You must enter a job title";
        public const string REQUIRED_FIELD_REQUEST_SUMMARY = "You must select a summary";
        public const string REQUIRED_FIELD_USERID_LENGTH = "Login ID has a maximum length of 30 characters.";
        public const string REQUIRED_FIELD_NEW_PASSWORD_LENGTH = "Passwords must have a length between 6 and 50 characters.";
        public const string REQUIRED_FIELD_ADMIN_PASSWORD_LENGTH = "Administrator passwords must have a length between 8 and 50 characters.";
        public const string REQUIRED_FIELD_COUNTRY_LENGTH = "Country has a maximum length of 100 characters";
        public const string REQUIRED_FIELD_CURRENT_EMPLOYER_LENGTH = "Current Employer has a maximum of 100 characters";
        public const string REQUIRED_FIELD_CURRENT_JOB_TITLE_LENGTH = "Current Job Title has a maximum of 100 characters";
        public const string REQUIRED_FIELD_IDEAL_JOB_TITLE_LENGTH = "Ideal Job Title has a maximum of 100 characters";
        public const string REQUIRED_FIELD_INDUSTRY_LENGTH = "Industry Type has a maximum of 100 characters";
        public const string REQUIRED_FIELD_ORGANISATION_LENGTH = "Organisation Name has a maximum of 100 characters";
        public const string REQUIRED_FIELD_EMAIL_ADDRESS_LENGTH = "Email length should be at least 6 characters";
        public const string REQUIRED_FIELD_POST_CODE_LENGTH = "Post Code has a maximum of 10 characters";
        public const string REQUIRED_FIELD_EMPLOYER_ROLE = "You must select a role.";
        public const string REQUIRED_FIELD_JOB_LOCATION = "Job location is required.";
        public const string REQUIRED_FIELD_JOB_TYPE = "At least one job type is required.";
        public const string REQUIRED_FIELD_PRICING_PLAN = "You must select one plan.";
        public const string REQUIRED_FIELD_JOB_AD_TITLE = "You must assign a title to the Job Ad.";
        public const string REQUIRED_FIELD_JOB_POSITION_TITLE = "Please specify a Position Title.";
        public const string REQUIRED_FIELD_JOB_AD_TEXT = "You must add text to the Job Ad";
        public const string REQUIRED_FIELD_EMAIL_MESSAGE = "Email text cannot be empty.";
        public const string REQUIRED_FIELD_PHONE = "Phone number field is required.";
        public const string REQUIRED_AT_LEAST_ONE_PHONE_NUMBER = "At least one phone number is required";
        public const string REQUIRED_FIELD_RESUME = "Please include a resume.";
        public const string REQUIRED_FIELD_MISSING_GENERIC = "{0} is required";
        public const string REQUIRED_FIELD_WHITEBOARD_MESSAGE = "Please enter the message you want to post first";
        public const string REQUIRED_FIELD_REQUEST_ACCOUNT_MANAGER = "You must select an account manager.";

        // INVALID INPUT MESSAGES
        public const string INVALID_RELOCATION_REASON_LENGTH = "Relocation reason cannot be longer than 200 characters";
        public const string INVALID_PHONE_NUMBER = "Phone number must be composed of digits, '+' and '-' (eg. +61-403-123123) and must be from 8 to 20 characters";
        public const string INVALID_FAX_NUMBER = "Fax number must be composed of digits, '+' and '-' (eg. +61-403-123123) and must be from 8 to 20 characters";
        public const string INVALID_FIRST_NAME = "First Name entered includes invalid characters! Valid example: 'Jean-L`och' or 'Henry the 3rd'";
        public const string INVALID_LAST_NAME = "Last Name entered includes invalid characters! Valid example: 'O`connor' or 'Lacroix the 2nd'";
        public const string INVALID_SUBURB_CITY = "City/Suburb format is invalid. Valid formats can be: 'S`aine-Pourie' or 'town of 1770'.";
        public const string INVALID_USER = "User does not exist. Please enter again.";
        public const string INVALID_ENDORSEE = "You have already endorsed this contact.";
        public const string INVALID_SEARCH_CRITERIA = "The search criteria you have entered is invalid.";
        public const string INVALID_POST_CODE = "Postcode entered is invalid. It must contain 4-10 digits.";
        public const string INVALID_SUBURB_STATE_POSTCODE = "Suburb / State / Postcode is invalid. Try typing in the first few letters of the suburb or postcode and then select from the list.";
        public const string INVALID_ACTIVATION_CODE = "Invalid activation code";
        public const string INVALID_COMMUNICATION_CATEGORY = "Invalid category";
        public const string INVALID_COUNTRY_FORMAT = "Country name includes letters only";
        public const string INVALID_EMAIL_FORMAT = "The email address you entered is not valid.";
        public const string INVALID_FILE_NAME = "File not found.";
        public const string INVALID_FILE_TYPE = "File type invalid.";
        public const string ERROR_RESUME_REQUIRED = "Please select a valid resume file to upload.";
        public const string INVALID_RESUME = "The format of your resume was not recognised. LinkMe scans the text in your resume to create a online version that is searched by employers and recruiters. Acceptable formats include: .doc, .html, .rtf, .pdf, .txt where the content is mostly plain text.";
        public const string INVALID_INVITATION = "The invitation id provided does not correspond to a valid invitation.";

        public const string INVALID_JOB_AD = "The job ad you are trying to apply for does not exist. Please search again.";
        public const string INVALID_LOCATION = "The location entered was not recognised, please re-enter";
        public const string INVALID_LOGIN_DETAILS = "Invalid username or password. Please try again.";
        public const string INVALID_EMAIL_PROVIDER_FORMAT = "Sorry, but we do not support importing contacts from '{0}'.";
        public const string INVALID_DATE_OF_BIRTH = "The date of birth you entered is not a valid date.";
        public const string INVALID_DATE_OF_BIRTH_RANGE_FORMAT = "The year of birth must be between {0} and {1}.";
        public const string INVALID_COUNTRY = "Specified country is unknown.";
        public const string INVALID_CONTACTS_CSV_UPLOADED = "Unable to understand the data in that file.  Please try again and ensure that you follow the listed steps.";
        public const string INVALID_FIELD_GENERIC = "The {0} is in an invalid format.";
        public const string INVALID_NAME_SEARCH_CRITERIA = "The name you entered contains invalid characters.";
        public const string INCOMPLETE_DATE_OF_BIRTH = "The date of birth you entered is incomplete. Please enter either all of day, month and year or none of them.";

        // MISC MESSAGES
        public const string NO_HTML_OR_XML_TAGS_ALLOWED = "No HTML or XML tags allowed.";
        public const string COMPARISON_PASSWORD_FAILED = "Password and Confirm Password fields entered do not match.";
        public const string COMPARISON_NEW_OLD_PASSWORD = "The new password can not be the same as the old password.";
        public const string INCORRECT_CURRENT_PASSWORD = "Your Current Password is incorrect.";
        public const string LOGIN_FAILED_ONE_LINE = "Login Failed! Please try again";
        public const string LOGIN_ENTER_DATA = "Please enter a username and password.";
        public const string DUPLICATE_USER_PROFILE = "Login ID already exists. Please choose another one.";
        public const string NO_SEARCH_RESULTS_RETURNED = "Your criteria has not returned any search results.";
        public const string PASSWORD_LENGTH_INVALID = "Your password length must be between 6 and 50 characters.";
        public const string NO_CONTACTS_IN_NETWORK = "You do not have any contacts in your network.";
        public const string NO_PENDING_INVITATIONS = "You do not have any pending invitations.";
        public const string NO_SEARCH_SELECTED = "You have not selected any searches.";
        public const string NO_SELECTED_INVITES = "You have not selected anyone to invite.";
        public const string ORGANISATION_NAME_REQUIRED = "Organisation Name required.";
        public const string CONTACT_PHONE_NUMBER_IS_REQUIRED = "Contact Phone Number is required.";
        public const string INVITE_YOURSELF = "You cannot invite yourself to your network.";
        public const string DAILY_LIMIT_EXCEEDED = "This functionality is temporarily unavailable. Please try again later.";
        public const string INVITER_EMPLOYER = "An Employer/Recruiter cannot be included in your network.";
        public const string INVITEE_EMPLOYER = "As an Employer/Recruiter you cannot be included in a network.";
        public const string INVITER_NOT_EXIST = "Inviter no longer exists in LinkMe";
        public const string MESSAGE_YOURSELF = "You cannot send messages to yourself.";
        public const string TERMS_OF_USE_NOT_AGREED_TO = "In order to proceed, please agree to the <b>terms of use</b>.";
        public const string CURRENT_SALARY_INVALID_AMOUNT = "The current salary amount you have entered is invalid.";
        public const string IDEAL_SALARY_INVALID_AMOUNT = "The ideal salary amount you have entered is invalid.";
        public const string CURRENT_SUPER_INVALID_AMOUNT = "The current super amount you have entered is invalid.";
        public const string IDEAL_JOB_TITLE_REQUIRED_FOR_ALERT = "An ideal job title is required to create an email alert.";
        public const string NO_SEARCH_SELECTED_PURCHASE = "You have not selected any Resumes to purchase.";
        public const string MAX_SAVED_SEARCHES_EXCEEDED = "You have exceeded the maximum number of saved searches.";
        public const string NO_SAVED_SEARCH_SELECTED = "You have not selected a saved search";
        public const string INVITE_EXISTING_CONTACT = "{0} already exists in your network.";
        public const string PAYMENT_COULD_NOT_BE_PROCESSED = "Payment could not be processed.";
        public const string CONTENT_EXCEEDS_MAX_CHARACTERS = "The maximum number of Non-HTML characters available for this content is {0}";
        public const string INVITATION_ALREADY_ACCEPTED = "This invitation has already been accepted";
        public const string INVITATION_WRONG_EMAIL_ADDRESS = "Invitations may only be accepted by users with email addresses corresponding to the original invitation address.";
        public const string INVITER_DISABLED = "This Member who invited you to their Network is now disabled in the LinkMe System. You will not be able to Link to their Network.";
        public const string INVITER_DEACTIVATED = "This Member who invited you to their Network is now deactivated in the LinkMe System. You will not be able to Link to their Network.";
        public const string INVITEE_DEACTIVATED = "Your account is currently not activated. You cannot accept invitations until you activate.";
        public const string MISSING_MULTI_ITEM_TEXT = "You must enter a value when adding new items to the list.";
        public const string YEAR_REQUIRED = "You must specify a year.";
        public const string YEAR_CANNOT_BE_GREATER_THAN_THIS_YEAR = "The year entered cannot be greater than this year.";
        public const string POSITION_EXPIRED_OR_CLOSED = "The position has been filled or cancelled.";
        public const string OTHER_LOCATION_NO_NUMERIC_SYMBOL = "Other location must not contain digits or '@' symbol";
        public const string JOB_TITLE_NO_NUMERIC_SYMBOL = "Job title must not contain digits or '@' symbol";
        public const string OPPORTUNITY_NAME_NO_SYMBOL = "Opportunity name must not contain '@' symbol";
        public const string REFERENCE_MUST_BE_NUMERIC = "Reference number must be between 5 and 18 digits";
        public const string EXPIRY_DATE_GREATER = "Expiry date must be greater than today";
        public const string REMOVE_CLOSED_REQUEST_SELECT_CHECKBOX = "A checkbox must be selected to remove closed Resume Request(s)";
        public const string SALARY_MAX_MUST_BE_NUMERIC = "The maximum salary must be a maximum of 9 characters and contain only digits and commas.";
        public const string SALARY_MIN_MUST_BE_NUMERIC = "The minumum salary must be a maximum of 9 characters and contain only digits and commas.";
        public const string SALARY_MIN_OUT_OF_RANGE_FORMAT = "The minimum salary, {0:c0}, is invalid. It must be from {1:c0} to {2:c0}.";
        public const string SALARY_MAX_OUT_OF_RANGE_FORMAT = "The maximum salary, {0:c0}, is invalid. It must be from {1:c0} to {2:c0}.";
        public const string SALARY_MAX_GREATER_THAN_MIN = "The \"From\" salary must be less than or equal to the \"To\" salary.";
        public const string SALARY_PERIOD_MUST_BE_SELECTED = "The salary range period must be selected if the \"From\" or \"To\" contain salary search criteria.";
        public const string DUPLICATE_INVITEE_ADDRESS = "You can not send duplicate invitations on this page";
        public const string NO_ACCESS_TO_JOB_AD = "This job ad was posted by a different employer."
            + " You do not have access to manage it.";
        public const string NO_ACCESS_TO_CANDIDATE_LIST = "This candidate list belongs to a different employer."
            + " You do not have access to manage it.";
        public const string NO_ACCESS_TO_SAVED_SEARCH = "This saved search was created by a different employer."
            + " You do not have access to manage it.";
        public const string UNAVAILABLE_FEATURE = "We're sorry, this feature isn't available at the moment.";
        public const string NO_ACCESS_TO_RESUME = "Sorry, you do not have access to this information.";
        public const string ALL_QUERY_CHARACTERS_IGNORED = "The query contained only ignored characters.";
        public const string ALL_NAME_CHARACTERS_IGNORED = "The name contained only ignored characters.";
        public const string REQUIRED_FIELD_MISSING_1 = "Please enter a {0} and try again";
        public const string SESSION_EXPIRED_LOG_IN_AGAIN = "Your session has expired, please log in again";

        public const string MEMBERS_RESUME_NOT_AVAILABLE = "The member's resume is no longer available.";
        public const string REQUIRED_KEYWORDS = "You must specify the keywords to search for.";
        public const string REQUIRED_KEYWORDS_OR_LOCATION = "You must specify at least keywords or location.";

        public const string PLEASE_TRY_AGAIN = "Please try again.";

        //INVALID QUERY STRING/POST/GET PARAMETERS
        public const string INVALID_COUNTRY_ID_SPECIFIED = "No country with ID {0} found.";
        public const string INVALID_FORMAT = "User-supplied string is in invalid format.";
        public const string NOT_QUERY_NOT_SUPPORTED = "We're sorry, in order to search without the words \"{0}\" you must enter"
            + " keywords into one of the fields labeled \"with all the words\", \"with the exact phrase\" or"
            + " \"with at least one of the words\". Please try again.";

        public const string INVALID_EMAILS_IN_INVITE_FRIENDS = "At least one of the email addresses you entered were invalid. Please review them below, and resubmit.";

        public const string ATTEMPTED_TO_VIEW_NETWORKER_WITHOUT_ACCESS_TO_NAME = "You don't have access to this person's details";

        public const string NOT_YOUR_FOLDER_TO_VIEW = "The candidate folder you attempted to view either does not exist or"
            + " belongs to a different user and is not shared with you.";
        public const string NOT_YOUR_FOLDER_TO_EDIT = "The candidate folder you attempted to edit either does not exist or"
            + " belongs to a different user and is not shared with you.";
        public const string NOT_YOUR_FOLDER_TO_DELETE = "The candidate folder you attempted to delete either does not exist or"
            + " belongs to a different user and is not shared with you.";

        public const string CANDIDATE_SEARCH_UNAVAILABLE = "The candidate search is currently unavailable. Please try again later.";

        public const string ACCOUNT_DISABLED = "Your account is currently disabled. Please contact us to have your account enabled.";
        public const string EMAIL_ALREADY_EXISTS_FORMAT = "A user account with this email address already exists. Please <a href=\"{0}\">log in</a> or <a href=\"{1}\">recover your password</a>.";
        public const string ACCOUNT_EXISTS_BUT_DISABLED = "Your account already exists, but is currently disabled. Please contact us to have your account enabled.";
        public const string LOGGED_IN_AS_WRONG_ROLE_BUT_CAN_JOIN_FORMAT = "Ooops! You're logged in as {1}, but you're trying to access a page for {2}s. You can try logging in again as {3} if you already have {3} account, <a href=\"{5}\">join</a> as a new {2} or <span style=\"white-space: nowrap;\"><a href=\"{4}\">go to the {0} homepage</a></span>.";
        public const string LOGGED_IN_AS_WRONG_ROLE_CANNOT_JOIN_FORMAT = "Ooops! You're logged in as {1} but you're trying to access a page for {2}s. You can try logging in again as {3} if you have {3} account or <span style=\"white-space: nowrap;\"><a href=\"{4}\">go to the {0} homepage</a></span>.";
    }
}
