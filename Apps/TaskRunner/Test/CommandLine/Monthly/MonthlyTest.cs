namespace LinkMe.TaskRunner.Test.CommandLine.Monthly
{
    public abstract class MonthlyTest : CommandLineTests
    {
        protected override string GetTaskGroup()
        {
            return "Monthly";
        }
    }
}
