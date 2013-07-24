using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Validation
{
    [Serializable]
    public class InvalidFileNameException
        : UserException
    {
        public string FileName { get; set; }
        public string[] ValidFileExtensions { get; set; }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { FileName, "'" + string.Join("', '", ValidFileExtensions) + "'" };
        }
    }

    [Serializable]
    public class FileTooLargeException
        : UserException
    {
        public int MaxFileSize { get; set; }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { MaxFileSize / (1024 * 1024)};
        }
    }

    [Serializable]
    public class TotalFilesTooLargeException
        : UserException
    {
        public int MaxTotalFileSize { get; set; }

        public override object[] GetErrorMessageParameters()
        {
            return new object[] { MaxTotalFileSize / (1024 * 1024) };
        }
    }
}