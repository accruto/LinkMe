using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    public class ParserUnavailableException
        : UserException
    {
        public ParserUnavailableException(Exception innerException)
            : base(null, innerException)
        {
        }

        public override string Message
        {
            get { return "The resume parser is unavailable."; }
        }
    }
}
