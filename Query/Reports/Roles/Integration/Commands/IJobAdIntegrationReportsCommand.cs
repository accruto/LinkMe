namespace LinkMe.Query.Reports.Roles.Integration.Commands
{
    public interface IJobAdIntegrationReportsCommand
    {
        void CreateJobAdIntegrationEvent(JobAdIntegrationEvent evt);
    }
}
