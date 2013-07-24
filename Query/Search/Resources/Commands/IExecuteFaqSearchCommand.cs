using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.Resources.Commands
{
    public interface IExecuteFaqSearchCommand
    {
        FaqSearchExecution Search(FaqSearchCriteria criteria, Range range);
    }
}
