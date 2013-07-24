namespace LinkMe.Framework.Tools.Performance
{
    internal static class Constants
    {
        internal static class Exceptions
        {
            public const string Profile = "Profile";
            public const string Step = "Step";
            public const string User = "User";
        }

        internal static class Counters
        {
            internal static class Scenario
            {
                internal const string Name = "LinkMe Performance Scenarios";
                internal const string TotalUsers = "Total Users";
                internal const string CurrentUsers = "Current Users";
                internal const string TotalCalls = "Total Calls";
                internal const string CallsPerSecond = "Calls Per Second";
                internal const string TotalErrors = "Total Errors";
            }

            internal static class Profile
            {
                internal const string Name = "LinkMe Performance Profiles";
                internal const string Enabled = "Enabled";
                internal const string CurrentlyRunning = "Currently Running";
                internal const string TotalRuns = "Total Runs";
                internal const string TotalErrors = "Total Errors";
                internal const string AverageExecutionTime = "Average Execution Time";
                internal const string AverageExecutionTimeBase = "Average Execution Time Base";
                internal const string LastExecutionTime = "Last Execution Time";
            }

            internal static class Step
            {
                internal const string Name = "LinkMe Performance Steps";
                internal const string Enabled = "Enabled";
                internal const string CurrentlyExecuting = "Currently Executing";
                internal const string TotalCalls = "Total Calls";
                internal const string CallsPerSecond = "Calls Per Second";
                internal const string TotalErrors = "Total Errors";
                internal const string AverageExecutionTime = "Average Execution Time";
                internal const string AverageExecutionTimeBase = "Average Execution Time Base";
                internal const string LastExecutionTime = "Last Execution Time";
            }
        }
    }
}
