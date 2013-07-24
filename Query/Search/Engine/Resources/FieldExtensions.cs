using LinkMe.Domain.Resources;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine.Resources
{
    internal static class FieldExtensions
    {
        public static string Encode(this ResourceType resourceType)
        {
            return NumericUtils.intToPrefixCoded((int)resourceType);
        }

        public static ResourceType DecodeToResourceType(this string resourceType)
        {
            return (ResourceType)NumericUtils.prefixCodedToInt(resourceType);
        }
    }
}
