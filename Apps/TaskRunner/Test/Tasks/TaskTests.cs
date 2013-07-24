using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks
{
    [TestClass]
    public abstract class TaskTests
    {
	    private const string ReturnEmailAddress = "do_not_reply@test.linkme.net.au";
        private const string ReturnDisplayName = "LinkMe";
	    private const string SystemEmailAddress = "system@test.linkme.net.au";
	    private const string SystemDisplayName = "LinkMe";
	    private const string AllStaffEmailAddress = "allstaff@test.linkme.net.au";
	    private const string AllStaffDisplayName = "LinkMe Staff";
        protected readonly static EmailRecipient Return = new EmailRecipient(ReturnEmailAddress, ReturnDisplayName);
        protected readonly static EmailRecipient System = new EmailRecipient(SystemEmailAddress, SystemDisplayName);
        protected readonly static EmailRecipient AllStaff = new EmailRecipient(AllStaffEmailAddress, AllStaffDisplayName);

        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IOrganisationsQuery _organisationsQuery = Resolve<IOrganisationsQuery>();
        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected IMockEmailServer _emailServer;

        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }

        [TestInitialize]
        public void TaskTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _emailServer = EmailHost.Start();
            _emailServer.ClearEmails();
        }
    }
}
