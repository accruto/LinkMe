using LinkMe.Domain;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine.Members
{
    internal static class FieldExtensions
    {
        public static string Encode(this CandidateStatus status)
        {
            return NumericUtils.intToPrefixCoded((int)status);
        }

        public static CandidateStatus DecodeToCandidateStatus(this string status)
        {
            return (CandidateStatus)NumericUtils.prefixCodedToInt(status);
        }

        public static string Encode(this VisaStatus? status)
        {
            return NumericUtils.intToPrefixCoded(status == null ? 0 : ((int)status + 1));
        }

        public static string Encode(this VisaStatus status)
        {
            return Encode((VisaStatus?)status);
        }

        public static VisaStatus? DecodeToVisaStatus(this string status)
        {
            var value = NumericUtils.prefixCodedToInt(status);
            return value == 0 ? (VisaStatus?) null : (VisaStatus) (value - 1);
        }
    }
}
