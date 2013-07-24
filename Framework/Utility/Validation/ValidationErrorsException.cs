using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Validation
{
    public class ValidationErrorsException
        : UserException
    {
        private readonly IList<ValidationError> _errors;

        public ValidationErrorsException(IEnumerable<ValidationError> errors)
        {
            _errors = errors.ToList();
        }

        public ValidationErrorsException(ValidationError error)
            : this(new[] { error })
        {
        }

        public override string Message
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(_errors.Count)
                    .Append(" validation error")
                    .Append(_errors.Count == 1 ? " has" : "s have")
                    .AppendLine(" been found:");

                foreach (var error in _errors)
                    sb.Append("\t").AppendLine(error.Message);

                return sb.ToString();
            }
        }

        public IList<ValidationError> Errors
        {
            get { return _errors; }
        }
    }
}
