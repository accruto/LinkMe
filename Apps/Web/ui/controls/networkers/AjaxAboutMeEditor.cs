using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using AjaxPro;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Validation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Domain.Location.Queries;
using LinkMe.Web.Applications.Ajax;
using LinkMe.Web.Applications.Facade;
using RegularExpressions=LinkMe.Domain.RegularExpressions;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public class AjaxAboutMeEditor : AjaxEditorBase
    {
        private static readonly Regex PhoneNumberRegex = new Regex(RegularExpressions.CompletePhoneNumberPattern, RegexOptions.Compiled);

        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        [AjaxMethod]
        public AjaxResult SaveContactDetails(string homePhone, string workPhone, string mobilePhone, string emailAddress)
        {
            try
            {
                EnsureMemberLoggedIn();

                var errors = new List<string>();
                if (string.IsNullOrEmpty(homePhone) && string.IsNullOrEmpty(workPhone) && string.IsNullOrEmpty(mobilePhone))
                    errors.Add(ValidationErrorMessages.REQUIRED_AT_LEAST_ONE_PHONE_NUMBER);

                ValidatePhoneNumber(homePhone, errors);
                ValidatePhoneNumber(workPhone, errors);
                ValidatePhoneNumber(mobilePhone, errors);

                if (string.IsNullOrEmpty(emailAddress))
                {
                    errors.Add(ValidationErrorMessages.REQUIRED_FIELD_EMAIL_ADDRESS);
                }
                else
                {
                    IValidator validator = EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode.SingleEmail, false);
                    var validationErrors = validator.IsValid(emailAddress)
                        ? null
                        : validator.GetValidationErrors("EmailAddress");

                    if (validationErrors != null && validationErrors.Length > 0)
                        errors.Add(((IErrorHandler)new StandardErrorHandler()).FormatErrorMessage(validationErrors[0]));
                }

                if (errors.Count > 0)
                    return new AjaxResult(AjaxResultCode.FAILURE, errors.ToArray());

                // Check the email.

                var emailChanged = string.Compare(LoggedInMember.GetBestEmailAddress().Address, emailAddress, StringComparison.InvariantCultureIgnoreCase) != 0;
                if (emailChanged)
                {
                    // Check that the email address is not being used by someone else.

                    var user = _membersQuery.GetMember(emailAddress);
                    if (user != null && user.Id != LoggedInMember.Id)
                        return new AjaxResult(AjaxResultCode.FAILURE, ValidationErrorMessages.DUPLICATE_USER_PROFILE);
                }

                // Update.

                var member = LoggedInMember;
                member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
                member.PhoneNumbers = new List<PhoneNumber>();
                if (!string.IsNullOrEmpty(mobilePhone))
                    member.PhoneNumbers.Add(new PhoneNumber { Number = mobilePhone, Type = PhoneNumberType.Mobile });
                if (!string.IsNullOrEmpty(homePhone))
                    member.PhoneNumbers.Add(new PhoneNumber { Number = homePhone, Type = PhoneNumberType.Home });
                if (!string.IsNullOrEmpty(workPhone))
                    member.PhoneNumbers.Add(new PhoneNumber { Number = workPhone, Type = PhoneNumberType.Work });
                _memberAccountsCommand.UpdateMember(member);

                return new AjaxResult(AjaxResultCode.SUCCESS);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AjaxMethod]
        public AjaxResult SaveWorkStatus(string status, string desiredJob)
        {
            try
            {
                EnsureMemberLoggedIn();

                var candidate = _candidatesCommand.GetCandidate(LoggedInMember.Id);
                candidate.Status = string.IsNullOrEmpty(status)
                    ? CandidateStatus.Unspecified
                    : (CandidateStatus) Enum.Parse(typeof(CandidateStatus), status);
                candidate.DesiredJobTitle = desiredJob;
                _candidatesCommand.UpdateCandidate(candidate);

                return new AjaxResult(AjaxResultCode.SUCCESS);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [AjaxMethod]
        public AjaxResult SaveProfileDetails(string firstName, string lastName,
            string gender, int day, int month, string year, string countryStr, string location, int ethnicStatus,
            string ageControlName, string ethnicStatusControlName)
        {
            try
            {
                EnsureMemberLoggedIn();

                var errors = new List<string>();

                if (string.IsNullOrEmpty(firstName))
                    errors.Add(ValidationErrorMessages.REQUIRED_FIELD_FIRST_NAME);

                if (string.IsNullOrEmpty(lastName))
                    errors.Add(ValidationErrorMessages.REQUIRED_FIELD_LAST_NAME);

                PartialDate? dateOfBirth = null;
                if (day != 0 && month != 0 && !string.IsNullOrEmpty(year))
                {
                    try
                    {
                        dateOfBirth = new PartialDate(ParseUtil.ParseUserInputInt32(year, "year of birth"), month, day);
                    }
                    catch (ArgumentException)
                    {
                        errors.Add(ValidationErrorMessages.INVALID_DATE_OF_BIRTH);
                    }
                    catch (UserException ex)
                    {
                        errors.Add(ex.Message);
                    }
                }
                else if (day != 0 || month != 0 || !string.IsNullOrEmpty(year))
                {
                    errors.Add(ValidationErrorMessages.INCOMPLETE_DATE_OF_BIRTH);
                }

                Country country = null;
                if (string.IsNullOrEmpty(countryStr))
                {
                    errors.Add(ValidationErrorMessages.REQUIRED_FIELD_COUNTRY);
                }
                else
                {
                    country = _locationQuery.GetCountry(countryStr);
                    if (country == null)
                        errors.Add(ValidationErrorMessages.INVALID_COUNTRY);
                }

                if (string.IsNullOrEmpty(location))
                    errors.Add(ValidationErrorMessages.REQUIRED_FIELD_SUBURB);

                if (errors.Count > 0)
                    return new AjaxResult(AjaxResultCode.FAILURE, errors.ToArray());

                var member = LoggedInMember;
                member.FirstName = firstName;
                member.LastName = lastName;
                member.Address.Location = _locationQuery.ResolveLocation(country, location);
                member.DateOfBirth = dateOfBirth;
                member.Gender = GetGender(member, gender);
                member.EthnicStatus = (EthnicStatus) ethnicStatus;
                _memberAccountsCommand.UpdateMember(member);

                _authenticationManager.UpdateUser(new HttpContextWrapper(HttpContext.Current), member, false);

                var ageValue = member.DateOfBirth.GetAgeDisplayText();
                var ethnicStatusValue = NetworkerFacade.GetEthnicStatusText(member, true);

                var userData = new ElementValuesUserData(
                    new[] { ageControlName, ethnicStatusControlName },
                    new[] { ageValue, ethnicStatusValue });

                return new AjaxResult(AjaxResultCode.SUCCESS, null, userData);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private static Gender GetGender(IMember member, string gender)
        {
            if (!string.IsNullOrEmpty(gender))
                return (Gender)Enum.Parse(typeof(Gender), gender);
            return member.Gender;
        }

        private static void ValidatePhoneNumber(string phoneNumber, ICollection<string> errorList)
        {
            if (!string.IsNullOrEmpty(phoneNumber) && !PhoneNumberRegex.IsMatch(phoneNumber))
            {
                errorList.Add(ValidationErrorMessages.INVALID_PHONE_NUMBER);
            }
        }
    }
}
