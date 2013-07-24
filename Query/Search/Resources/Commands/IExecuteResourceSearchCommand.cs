using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.Resources.Commands
{
    public interface IExecuteResourceSearchCommand
    {
        ResourceSearchExecution Search(ResourceSearchCriteria criteria, Range range);
    }
}   