using System;

namespace LinkMe.Domain.Roles.Resumes.Lens
{
    public class LensException
        : ApplicationException
    {
        protected LensException()
        {
        }

        public LensException(string message)
            : base(message)
        {
        }

        public LensException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class LensBusyException
        : LensException
    {
        public LensBusyException(string message)
            : base(message)
        {
        }

        public LensBusyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class LensDuplicateKeyException
        : LensException
    {
        public LensDuplicateKeyException()
        {
        }

        public LensDuplicateKeyException(string message)
            : base(message)
        {
        }

        public LensDuplicateKeyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class LensInvalidDocumentException
        : LensException
    {
        public LensInvalidDocumentException()
        {
        }

        public LensInvalidDocumentException(string message)
            : base(message)
        {
        }

        public LensInvalidDocumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class LensResumeDoesNotExistException
        : LensException
    {
        public LensResumeDoesNotExistException()
        {
        }

        public LensResumeDoesNotExistException(string message)
            : base(message)
        {
        }

        public LensResumeDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class LensUnavailableException
        : LensException
    {
        public LensUnavailableException(string message)
            : base(message)
        {
        }

        public LensUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class LensXmlInvalidException
        : LensException
    {
        public LensXmlInvalidException(string message)
            : base(message)
        {
        }

        public LensXmlInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}