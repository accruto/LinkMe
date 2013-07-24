namespace LinkMe.Framework.Tools.Performance
{
    public abstract class ProfileTestFixture
        : IProfileTestFixture
    {
        private StepTimer _timer;

        protected int TotalUsers { get; private set; }
        protected int CurrentUser { get; private set; }
        protected int CurrentIteration { get; private set; }

        void IProfileTestFixture.SetUp(int users, Profile profile)
        {
            TotalUsers = users;
            SetUp();
        }

        void IProfileTestFixture.SetUpIteration(int currentUser, int currentIteration, StepTimer timer)
        {
            CurrentUser = currentUser;
            CurrentIteration = currentIteration;
            _timer = timer;
            SetUpIteration();
        }

        void IProfileTestFixture.TearDownIteration()
        {
            TearDownIteration();
        }

        protected StepTimer GetTimer()
        {
            return _timer;
        }

        protected virtual void SetUp()
        {
        }

        protected virtual void SetUpIteration()
        {
        }

        protected virtual void TearDownIteration()
        {
        }
    }
}
