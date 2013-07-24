using System;
using System.Reflection;

namespace LinkMe.Framework.Tools.Performance
{
    public interface IProfileTestFixture
    {
        void SetUp(int users, Profile profile);
        void SetUpIteration(int currentUser, int currentIteration, StepTimer timer);
        void TearDownIteration();
    }

    public interface IAsyncTestResult
        : IAsyncResult
    {
        void SetComplete(bool completedSynchronously);
        void SetComplete(Exception ex, bool completedSynchronously);
    }

    public interface IAsyncProfileTestFixture
    {
        void Begin(IAsyncTestResult result, MethodInfo beginMethod, MethodInfo endMethod);
    }
}
