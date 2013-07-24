using System;
using LinkMe.Environment.CommandLines;

namespace LinkMe.Framework.Host.Service
{
	internal class ServiceMain
	{
		private static void Main(string[] args)
		{
            CommandManager manager;
            try
            {
                // Create a manager from the configuration information.

                manager = CommandManager.ReadSection(Constants.Config.UtilSection);
            }
            catch (Exception ex)
            {
                CommandManager.WriteExceptionSummary(ex);
                return;
            }

            try
            {
                // Ask the manager to create a command.

                var commandLine = new CommandLine(args);
                var command = manager.CreateCommand(commandLine);

                // If there is a command then execute it, else show the usage.

                if (command != null)
                    command.Execute();
                else
                    Console.Write(manager.GetUsage());
            }
            catch (Exception ex)
            {
                manager.WriteException(ex);
            }
		}
	}
}
