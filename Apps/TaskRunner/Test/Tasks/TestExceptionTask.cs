using System;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Test.Tasks
{
    public class TestExceptionTask
        : Task
	{
        public TestExceptionTask()
            : base(new EventSource<TestExceptionTask>())
        {
        }

        public override void ExecuteTask()
		{
			throw new NotImplementedException("this should be caught and handled without affecting other tasks");
		}
	}
}