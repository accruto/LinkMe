using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Instrumentation;
using Microsoft.Practices.Unity.Configuration;

namespace LinkMe.TaskRunner
{
	public static class TaskRunner
	{
	    public const string WaitArgument = "/wait";
        public const string AttachDebuggerArgument = "/attachDebugger";

	    private static readonly EventSource EventSource = new EventSource(typeof(TaskRunner));

		private static void Main(string[] args)
		{
            const string method = "Main";

            try
			{
                EventSource.Raise(Event.Information, method, "Starting TaskRunner. Command line: " + string.Join(" ", args));

				if (args.Length == 0)
				{
                    EventSource.Raise(Event.Error, method, "Incorrect TaskRunner syntax - arguments are needed.");
                    System.Environment.ExitCode = 2;
					return;
				}

                Configure();

                // Check whether the process should wait at the end for testing purposes.

                var wait = CheckUnitTestArguments(ref args);

			    try
			    {
                    if (CheckTestRun(ref args))
                        StartTestRun(args);
                    else
                        Run(args);
			    }
			    catch (Exception ex)
			    {
                    EventSource.Raise(Event.Error, method, "Could not run the task.", ex, new StandardErrorHandler());
                    System.Environment.ExitCode = 3;
                }

                if (wait)
                {
                    // This is for test cases. Wait until the test case has collected whatever information it needs.

                    using (var eventFinished = new EventWaitHandle(false, EventResetMode.AutoReset, "TaskRunnerTestFinished"))
                    {
                        using (var eventCollectionFinished = new EventWaitHandle(false, EventResetMode.AutoReset, "TaskRunnerTestCollectionFinished"))
                        {
                            // Set the event to say we are finished.

                            eventFinished.Set();

                            // Wait until the collection is over.

                            eventCollectionFinished.WaitOne(30000);
                        }
                    }
                }
			}
			catch (Exception e)
			{
                EventSource.Raise(Event.Error, method, "Unhandled exception caught in TaskRunner.Main().", e, new StandardErrorHandler());
                System.Environment.ExitCode = 1;
			}
		}

        private static bool CheckTestRun(ref string[] args)
        {
            if (args[0] == "/testrun")
            {
                var newArgs = new string[args.Length - 1];
                Array.Copy(args, 1, newArgs, 0, newArgs.Length);
                args = newArgs;
                return true;
            }
            
            return false;
        }

	    private static bool CheckUnitTestArguments(ref string[] args)
	    {
            bool wait = false;

            if (args[0] == WaitArgument)
            {
                var newArgs = new string[args.Length - 1];
                Array.Copy(args, 1, newArgs, 0, newArgs.Length);
                args = newArgs;
                wait = true;
            }

            if (args[0] == AttachDebuggerArgument)
            {
                var newArgs = new string[args.Length - 1];
                Array.Copy(args, 1, newArgs, 0, newArgs.Length);
                args = newArgs;

                Debugger.Launch();
            }

	        return wait;
	    }

	    private static void StartTestRun(string[] args)
		{
            if (args.Length < 1)
                throw new ApplicationException("Incorrect syntax, no task class name was supplied.");

            Assembly assembly = Assembly.GetExecutingAssembly();

            string taskClass = args[0];
            Type taskType = null;
            if (taskClass.IndexOf('.') == -1)
            {
                IEnumerable<Type> matchingTypes = assembly.GetTypes().Where(
                    t => t.Name == taskClass && typeof(ITask).IsAssignableFrom(t));

                int count = matchingTypes.Count();
                if (count != 1)
                    throw new ApplicationException(count + " tasks matching '" + taskClass + "' were found.");

                taskType = matchingTypes.ElementAt(0);
                taskClass = taskType.FullName;
            }

            ITask task;
            if (taskType != null)
            {
                // Use unity to create the task.

                task = Container.Current.Resolve(taskType) as ITask;
            }
            else
            {
                task = assembly.CreateInstance(taskClass, true) as ITask;
            }

			if (task == null)
                throw new ApplicationException("Failed to create an instance of task class '" + taskClass + "'.");

			var taskArgs = new string[args.Length - 1];
			Array.Copy(args, 1, taskArgs, 0, taskArgs.Length);

			try
			{
				task.Execute(taskArgs);
			}
			catch (Exception ex)
			{
                throw new ApplicationException("Unhandled exception thrown by task '" + taskClass
                    + "' during test run.", ex);
            }
		}

		private static void Run(string[] args)
		{
            const string method = "Run";
            EventSource.Raise(Event.Trace, method, "Initialising TaskRunner.");

            // Grab the task group.

            if (args.Length == 0)
                throw new ApplicationException("Incorrect syntax, a task group needs to be supplied.");

            var taskGroup = args[0];
            var taskArgs = new string[args.Length - 1];
            Array.Copy(args, 1, taskArgs, 0, taskArgs.Length);

            EventSource.Raise(Event.Trace, method, "Running task group '" + taskGroup + "'.");
           
            // Configure TaskRunner tasks.

            var taskSection = (UnityConfigurationSection)ConfigurationManager.GetSection("linkme.tasks.container");
            UnityContainerElement taskConfig = GetContainerCaseInsensitive(taskSection.Containers, taskGroup);
            if (taskConfig == null)
                throw new ApplicationException(string.Format("The task group container specified on the command line, '{0}', does not exist.", taskGroup));

            taskConfig.Configure(Container.Current);

            // Run the tasks.

            IEnumerable<ITask> tasks = Container.Current.ResolveAll<ITask>();

			foreach (ITask task in tasks)
                task.Execute(taskArgs);
		}

        private static void Configure()
        {
            // Set up the application to point to the web site if needed.

            ApplicationContext.SetupApplications(WebSite.LinkMe);

            new ContainerConfigurer()
                .Add(new DomainConfigurator())
                .Add(new RolesConfigurator())
                .Add(new UsersConfigurator())
                .Add(new QueryConfigurator())
                .Add(new SearchConfigurator())
                .Add(new QueryEngineConfigurator())
                .Add(new ReportsConfigurator())
                .Add(new AgentsConfigurator())
                .Add(new AspConfigurator())
                .Add(new PresentationConfigurator())
                .Add(new ServicesConfigurator())
                .Add("linkme.resources.container")
                .Add("linkme.environment.container")
                .Configure(Container.Current, new ContainerEventSource());
        }

	    private static UnityContainerElement GetContainerCaseInsensitive(UnityContainerElementCollection containers, string name)
	    {
            foreach (UnityContainerElement container in containers)
            {
                if (string.Equals(container.Name, name, StringComparison.InvariantCultureIgnoreCase))
                    return container;
            }

            return null;
	    }
	}
}