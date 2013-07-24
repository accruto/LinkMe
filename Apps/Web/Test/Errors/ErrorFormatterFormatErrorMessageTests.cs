using System;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Images;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Lens;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Errors
{
    [TestClass]
    public class ErrorFormatterFormatErrorMessageTests
    {
        [TestMethod]
        public void TestRequiredValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The " + name + " is required.", new RequiredValidationError(name));
        }

        [TestMethod]
        public void TestNotFoundValidationError()
        {
            const string name = "address";
            var value = Guid.NewGuid();
            AssertErrorMessage("The " + name + " with value '" + value + "' cannot be found.", new NotFoundValidationError(name, value));
        }

        [TestMethod]
        public void TestNotChangedValidationError()
        {
            const string name = "address";
            const string from = "aaa";
            AssertErrorMessage("The new " + name + " needs to be different from its original value.", new NotChangedValidationError(name, from));
        }

        [TestMethod]
        public void TestLengthValidationError()
        {
            const string name = "address";
            const int length = 10;
            AssertErrorMessage("The " + name + " must be " + length + " characters in length.", new LengthValidationError(name, length));
        }

        [TestMethod]
        public void TestLengthRangeValidationError()
        {
            const string name = "address";
            const int min = 10;
            const int max = 20;
            AssertErrorMessage("The " + name + " must be between " + min + " and " + max + " characters in length.", new LengthRangeValidationError(name, min, max));
        }

        [TestMethod]
        public void TestMinimumLengthValidationError()
        {
            const string name = "address";
            const int min = 10;
            AssertErrorMessage("The " + name + " must be at least " + min + " characters in length.", new MinimumLengthValidationError(name, min));
        }

        [TestMethod]
        public void TestMaximumLengthValidationError()
        {
            const string name = "address";
            const int max = 20;
            AssertErrorMessage("The " + name + " must be no more than " + max + " characters in length.", new MaximumLengthValidationError(name, max));
        }

        [TestMethod]
        public void TestNumericValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The " + name + " must not have any invalid characters.", new NumericValidationError(name));
        }

        [TestMethod]
        public void TestNumericLengthRangeValidationError()
        {
            const string name = "address";
            const int min = 10;
            const int max = 20;
            AssertErrorMessage("The " + name + " must be between " + min + " and " + max + " characters in length and not have any invalid characters.", new NumericLengthRangeValidationError(name, min, max));
        }

        [TestMethod]
        public void TestAlphaNumericValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The " + name + " must not have any invalid characters.", new AlphaNumericValidationError(name));
        }

        [TestMethod]
        public void TestAlphaNumericLengthRangeValidationError()
        {
            const string name = "address";
            const int min = 10;
            const int max = 20;
            AssertErrorMessage("The " + name + " must be between " + min + " and " + max + " characters in length and not have any invalid characters.", new AlphaNumericLengthRangeValidationError(name, min, max));
        }

        [TestMethod]
        public void TestRegexValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The " + name + " must not have any invalid characters.", new RegexValidationError(name));
        }

        [TestMethod]
        public void TestRegexLengthValidationError()
        {
            const string name = "address";
            const int length = 10;
            AssertErrorMessage("The " + name + " must be " + length + " characters in length and not have any invalid characters.", new RegexLengthValidationError(name, length));
        }

        [TestMethod]
        public void TestRegexLengthRangeValidationError()
        {
            const string name = "address";
            const int min = 10;
            const int max = 20;
            AssertErrorMessage("The " + name + " must be between " + min + " and " + max + " characters in length and not have any invalid characters.", new RegexLengthRangeValidationError(name, min, max));
        }

        [TestMethod]
        public void TestRegexMinimumLengthValidationError()
        {
            const string name = "address";
            const int min = 10;
            AssertErrorMessage("The " + name + " must be at least " + min + " characters in length and not have any invalid characters.", new RegexMinimumLengthValidationError(name, min));
        }

        [TestMethod]
        public void TestRegexMaximumLengthValidationError()
        {
            const string name = "address";
            const int max = 20;
            AssertErrorMessage("The " + name + " must be valid and have less than " + max + " characters.", new RegexMaximumLengthValidationError(name, max));
        }
        
        [TestMethod]
        public void TestNumericValueRangeValidationError()
        {
            const string name = "address";
            const int min = 10;
            const int max = 20;
            AssertErrorMessage("The " + name + " must be between " + min + " and " + max + ".", new NumericValueRangeValidationError(name, min, max));
        }

        [TestMethod]
        public void TestNumericMinimumValueValidationError()
        {
            const string name = "address";
            const int min = 10;
            AssertErrorMessage("The " + name + " must be greater than or equal to " + min + ".", new NumericMinimumValueValidationError(name, min));
        }

        [TestMethod]
        public void TestNumericMaximumValueRangeValidationError()
        {
            const string name = "address";
            const int max = 20;
            AssertErrorMessage("The " + name + " must be less than or equal to " + max + ".", new NumericMaximumValueValidationError(name, max));
        }

        [TestMethod]
        public void TestEmailAddressValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The address must be valid and have less than 320 characters.", new EmailAddressValidationError(name));
        }

        [TestMethod]
        public void TestEmailAddressHostValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The address host name is not recognised.", new EmailAddressHostValidationError(name));
        }

        [TestMethod]
        public void TestDifferentValidationError()
        {
            const string name = "address";
            const string otherName = "otherAddress";
            AssertErrorMessage("The " + name + " and other address must match.", new DifferentValidationError(name, otherName));
        }

        [TestMethod]
        public void TestDuplicateValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The " + name + " is already being used.", new DuplicateValidationError(name));
        }

        [TestMethod]
        public void TestSalaryValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The lower and upper bounds for the " + name + " must not be negative.", new SalaryValidationError(name));
        }

        [TestMethod]
        public void TestHtmlValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The " + name + " appears to have some script in it.", new HtmlValidationError(name));
        }

        [TestMethod]
        public void TestEnumValueValidationError()
        {
            const string name = "address";
            AssertErrorMessage("The value for " + name + " is invalid.", new EnumValueValidationError(name));
        }

        [TestMethod]
        public void TestCircularValidationError()
        {
            const string name = "address";
            AssertErrorMessage("Setting the " + name + " will result in a circular dependency.", new CircularValidationError(name));
        }

        [TestMethod]
        public void TestTermsValidationError()
        {
            const string name = "address";
            AssertErrorMessage("Please accept the terms and conditions.", new TermsValidationError(name));
        }

        [TestMethod]
        public void TestCreditCardAuthorisationValidationError()
        {
            const string name = "address";
            AssertErrorMessage("Please authorise the credit card charge.", new CreditCardAuthorisationValidationError(name));
        }

        [TestMethod]
        public void TestLongNames()
        {
            AssertErrorMessage("The long name is required.", new RequiredValidationError("LongName"));
            AssertErrorMessage("The long name is required.", new RequiredValidationError("longName"));
            AssertErrorMessage("The long long name is required.", new RequiredValidationError("LongLongName"));
        }

        [TestMethod]
        public void TestReplaceNames()
        {
            AssertErrorMessage("The username is required.", new RequiredValidationError("LoginLoginId"));
            AssertErrorMessage("The username is required.", new RequiredValidationError("JoinLoginId"));
            AssertErrorMessage("The credit card number is required.", new RequiredValidationError("CardNumber"));
        }

        [TestMethod]
        public void TestDuplicateUserException()
        {
            AssertErrorMessage(GetDuplicateUserMessage(), new DuplicateUserException());
        }

        [TestMethod]
        public void TestAuthenticationFailedException()
        {
            AssertErrorMessage("Login failed. Please try again.", new AuthenticationFailedException());
        }

        [TestMethod]
        public void TestPermissionsException()
        {
            AssertErrorMessage("You do not have sufficient permissions.", new JobAdPermissionsException(new UnregisteredMember { Id = Guid.NewGuid() }, Guid.NewGuid()));
        }

        [TestMethod]
        public void TestInsufficientCreditsException()
        {
            const int required = 5;
            const int available = 2;
            AssertErrorMessage("You need " + required + " credits to perform this action but you only have " + available + " available.", new InsufficientCreditsException { Required = required, Available = available });
        }

        [TestMethod]
        public void TestTooManyAccessesException()
        {
            AssertErrorMessage("Please call LinkMe on 1800 546-563 to contact additional candidates.", new TooManyAccessesException());
        }

        [TestMethod]
        public void TestNotVisibleException()
        {
            AssertErrorMessage("The candidate details have been hidden by the candidate.", new NotVisibleException());
        }

        [TestMethod]
        public void TestNotFoundException()
        {
            const string type = "job";
            AssertErrorMessage("The " + type + " cannot be found.", new NotFoundException(type));
        }

        [TestMethod]
        public void TestNotLoggedInException()
        {
            AssertErrorMessage("The user is not logged in.", new UnauthorizedException());
        }

        [TestMethod]
        public void TestInvalidFileNameException()
        {
            const string fileName = "resume.xyz";
            var validFileExtensions = new[] { ".doc", ".pdf" };
            AssertErrorMessage("The file name '" + fileName + "' is not supported, as it must have one of the following extensions, '" + string.Join("', '", validFileExtensions) + "'.", new InvalidFileNameException { FileName = fileName, ValidFileExtensions = validFileExtensions });
        }

        [TestMethod]
        public void TestFileTooLargeException()
        {
            const int maxFileSize = 2097152;
            AssertErrorMessage("The size of the file exceeds the maximum allowed of " + maxFileSize / (1024 * 1024) + "MB.", new FileTooLargeException { MaxFileSize = maxFileSize });
        }

        [TestMethod]
        public void TestTotalFilesTooLargeException()
        {
            const int maxTotalFileSize = 2097152;
            AssertErrorMessage("The total size of all files exceeds the maximum allowed of " + maxTotalFileSize / (1024 * 1024) + "MB.", new TotalFilesTooLargeException { MaxTotalFileSize = maxTotalFileSize });
        }

        [TestMethod]
        public void TestChooseProductException()
        {
            AssertErrorMessage("At least one of the packs must be chosen.", new ChooseProductException());
        }

        [TestMethod]
        public void TestPurchaseUserException()
        {
            const string code = "0123";
            const string message = "Not enough funds.";
            AssertErrorMessage("The credit card could not be processed: " + message, new SecurePayNotApprovedException(code, message));
        }

        [TestMethod]
        public void TestInvalidResumeException()
        {
            AssertErrorMessage("Our system is unable to extract your profile information from this file. Please try another document or create your profile manually.", new InvalidResumeException());
        }

        [TestMethod]
        public void TestParserUnavailableException()
        {
            const string message = "Not here.";
            AssertErrorMessage("Our system is unable to extract your profile information from this file at this time. Please try again later or create your profile manually. We apologise for the inconvenience.", new ParserUnavailableException(new LensException(message)));
        }

        [TestMethod]
        public void TestInvalidResumeExtensionException()
        {
            const string extension = ".xyz";
            AssertErrorMessage("The resume extension '" + extension + "' is not supported.", new InvalidResumeExtensionException(extension));
        }

        [TestMethod]
        public void TestInvalidImageExtensionException()
        {
            const string extension = ".xyz";
            AssertErrorMessage("The extension '" + extension + "' is not supported.", new InvalidImageExtensionException(extension));
        }

        protected virtual IErrorHandler CreateErrorHandler()
        {
            return new StandardErrorHandler();
        }

        protected virtual string GetDuplicateUserMessage()
        {
            return "The username is already being used.";
        }

        protected void AssertErrorMessage(string expectedErrorMessage, ValidationError error)
        {
            Assert.AreEqual(expectedErrorMessage, CreateErrorHandler().FormatErrorMessage(error));
        }

        protected void AssertErrorMessage(string expectedErrorMessage, UserException ex)
        {
            Assert.AreEqual(expectedErrorMessage, CreateErrorHandler().FormatErrorMessage(ex));
        }
    }
}
