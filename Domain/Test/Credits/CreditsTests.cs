using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Credits
{
    public abstract class CreditsTests
        : TestClass
    {
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected readonly IExercisedCreditsCommand _exercisedCreditsCommand = Resolve<IExercisedCreditsCommand>();
        protected readonly IExercisedCreditsQuery _exercisedCreditsQuery = Resolve<IExercisedCreditsQuery>();

        [TestInitialize]
        public void CreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }
    }
}
