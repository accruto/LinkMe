using System;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.WebControls
{
    public class EmailAddressValidator : LinkMeRegularExpressionValidator
    {
        private EmailAddressValidationMode _validationMode;
        public bool CheckDns { get; set; }

        public EmailAddressValidator()
        {
            EmailAddressValidationMode = EmailAddressValidationMode.SingleEmail;
        }

        public EmailAddressValidationMode EmailAddressValidationMode
        {
            get { return _validationMode; }
            set
            {
                switch (value)
                {
                    case EmailAddressValidationMode.UserOnly:
                        ValidationExpression = RegularExpressions.CompleteEmailUserPattern;
                        break;

                    case EmailAddressValidationMode.MultipleEmails:
                        ValidationExpression = RegularExpressions.CompleteMultipleEmailAddressesPattern;
                        break;

                    case EmailAddressValidationMode.SingleEmail:
                        ValidationExpression = RegularExpressions.CompleteEmailAddressPattern;
                        break;

                    default:
                        throw new ArgumentException("Unexpected EmailAddressValidation value: " + value, "value");
                }

                _validationMode = value;
            }
        }

        protected override bool EvaluateIsValid()
        {
            // Empty is valid (if it's not then include a LinkMeRequiredFieldValidator).

            string controlValidationValue = GetControlValidationValue(ControlToValidate);
            if (controlValidationValue == null || controlValidationValue.Trim().Length == 0)
                return true;

            // Validate.

            IValidator validator = EmailAddressValidatorFactory.CreateValidator(EmailAddressValidationMode, CheckDns);
            var errors = validator.IsValid(controlValidationValue)
                ? null
                : validator.GetValidationErrors("EmailAddress");

            ErrorMessage = errors == null || errors.Length == 0 ? null : ((IErrorHandler) new StandardErrorHandler()).FormatErrorMessage(errors[0]);
            return errors == null || errors.Length == 0;
        }

#if DEBUG

        protected override bool RequiresErrorMessage
        {
            get { return false; }
        }

        protected override bool RequiresValidationExpression
        {
            get { return false; }
        }

#endif
    }
}
