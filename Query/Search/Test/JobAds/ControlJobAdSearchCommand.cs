using System;
using LinkMe.Environment;

namespace LinkMe.Query.Search.Test.JobAds
{
    public class ControlJobAdSearchCommand
        : IControlJobAdSearchCommand
    {
        private readonly IUpdateSearchEngine _updateSearchEngine;

        public ControlJobAdSearchCommand(IUpdateSearchEngine updateSearchEngine)
        {
            _updateSearchEngine = updateSearchEngine;
        }

        void IControlJobAdSearchCommand.Clear()
        {
            // This method is used for tests only. Prevent unit tests from accidentally clearing UAT indexes.

            if (RuntimeEnvironment.Environment != ApplicationEnvironment.Dev)
                throw new InvalidOperationException("Clear is only allowed in dev, but the current environment is " + RuntimeEnvironment.EnvironmentName);

            _updateSearchEngine.Clear();
        }

        void IControlJobAdSearchCommand.Reset()
        {
            // This method is used for tests only. Prevent unit tests from accidentally clearing UAT indexes.

            if (RuntimeEnvironment.Environment != ApplicationEnvironment.Dev)
                throw new InvalidOperationException("Reset is only allowed in dev, but the current environment is " + RuntimeEnvironment.EnvironmentName);

            _updateSearchEngine.Reset();
        }
    }
}
