using System;
using LinkMe.Environment.CommandLines;

namespace LinkMe.Environment.Util
{
    class Program
    {
        private static int Main(string[] args)
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
                return 2;
            }

            try
            {
                // Ask the manager to create a command.

                var commandLine = new CommandLine(args);
                Command command = manager.CreateCommand(commandLine);

                // If there is a command then execute it, else show the usage.

                if (command != null)
                {
                    command.Execute();
                    return 0;
                }
                
                Console.Write(manager.GetUsage());
                return 1;
            }
            catch (Exception ex)
            {
                manager.WriteException(ex);
                return 2;
            }
        }
    }
}