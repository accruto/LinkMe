using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.Members
{
    public class MemberSearchNameAttribute
        : RegexAttribute
    {
        public MemberSearchNameAttribute()
            : base(RegularExpressions.CompleteMemberSearchName, Constants.MemberSearchNameMinLength, Constants.MemberSearchNameMaxLength)
        {
        }
    }
}