using System;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Domain.Credits
{
    [Serializable]
    public class InsufficientCreditsException
        : UserException
    {
        public int? Available { get; set; }
        public int Required { get; set; }

        public override object[] GetErrorMessageParameters()
        {
            // This should somehow be moved into resources.

            return new object[]
            {
                Required,
                Required > 1 ? "s" : "",
                Available == 0 ? "" : " only",
                Available == 0 ? "none" : Available.ToString()
            };
        }
    }
}
