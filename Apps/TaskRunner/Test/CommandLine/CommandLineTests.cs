using System.Diagnostics;
using System.IO;
using System.Threading;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.TaskRunner.Test.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine
{
    [TestClass]
    public abstract class CommandLineTests
        : TaskTests
    {
        protected static readonly ILocationQuery LocationQuery = Resolve<ILocationQuery>();

        protected bool AttachDebuggerToTaskRunner { get; set; }

        [TestInitialize]
        public void CommandLineTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            var container = Container.Push();
            container.RegisterType<IWebSiteQuery, WebSiteQuery>();

            _emailServer = EmailHost.Start();
        }

        protected void Execute(bool collectEmails)
        {
            _emailServer.ClearEmails();
            var process = new Process();

            try
            {
                process.StartInfo.FileName = GetFileName();

                string args = string.Empty;
                if (collectEmails)
                    args = TaskRunner.WaitArgument + " ";

                if (AttachDebuggerToTaskRunner)
                    args += " " + TaskRunner.AttachDebuggerArgument + " ";

                var task = GetTask();
                if (task != null)
                    args += "/testrun " + task;
                else
                    args += GetTaskGroup();
                   
                var taskArgs = GetTaskArgs();
                if (!string.IsNullOrEmpty(taskArgs))
                    args += " " + taskArgs;
                process.StartInfo.Arguments = args;

                // Start the process in a hidden window.

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;

                using (var eventFinished = new EventWaitHandle(false, EventResetMode.AutoReset, "TaskRunnerTestFinished"))
                {
                    using (var eventCollectionFinished = new EventWaitHandle(false, EventResetMode.AutoReset, "TaskRunnerTestCollectionFinished"))
                    {
                        process.Start();

                        // Allow the task to complete, waiting for 3 minutes (which is more than enough lee-way for site start-up etc).

                        const int waitFor = 180;
                        if (collectEmails)
                        {
                            // There is a little bit of timing going on here to collect the emails.

                            // Wait until the task is finished.

                            if (!eventFinished.WaitOne(waitFor * 1000))
                                Assert.Fail("Did not receive the finished signal after " + waitFor + " seconds.");

                            // Signal that this is done.

                            eventCollectionFinished.Set();
                        }

                        // Kill the process if it doesn't finish in one minute.

                        process.WaitForExit(waitFor * 1000);
                        if (!process.HasExited)
                        {
                            process.Kill();
                            Assert.Fail("Had to kill the '" + GetFileName() + "' process because it ran for more than " + waitFor + " seconds.");
                        }

                        // Check the exit code.

                        if (process.ExitCode != 0)
                            Assert.Fail("The '" + GetFileName() + "' process returned an exit code: " + process.ExitCode + ".");
                    }
                }
            }
            finally
            {
                process.Close();
            }
        }

        private static string GetFileName()
        {
            var exeName = typeof(TaskRunner).Assembly.GetName().Name + ".exe";

            return Path.Combine(RuntimeEnvironment.GetSourceFolder(), Path.Combine("Apps\\TaskRunner\\bin\\", exeName));
        }

        protected virtual string GetTask()
        {
            return null;
        }

        protected virtual string GetTaskGroup()
        {
            return null;
        }

        protected virtual string GetTaskArgs()
        {
            return null;
        }
    }
}
