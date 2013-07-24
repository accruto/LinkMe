using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSearchNameAttribute
        : RegexAttribute
    {
        public JobAdSearchNameAttribute()
            : base(RegularExpressions.CompleteJobAdSearchName, Constants.JobAdSearchNameMinLength, Constants.JobAdSearchNameMaxLength)
        {
        }
    }
}
