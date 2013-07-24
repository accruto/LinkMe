using System;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace LinkMe.Environment.CommandLines
{
    public class CommandManager
    {
        private const string ExceptionFormatString = "<error: an exception of type '{0}' was thrown>";

        public static CommandManager ReadSection(string sectionName)
        {
            return (CommandManager) ConfigurationManager.GetSection(sectionName);
        }

        internal CommandManager()
        {
            m_commands = new CommandConfigurations();
            m_options = new OptionConfigurations();
        }

        #region Static methods

        public static void WriteExceptionSummary(System.Exception ex)
        {
            Console.Error.WriteLine("Error: " + GetExceptionMessageTree(ex));
            Console.Error.WriteLine("Stack: " + ex.StackTrace);
        }

        private static CommandOptions CreateOptions(CommandConfiguration commandConfiguration, CommandLine commandLine)
        {
            // Create a new option collection.

            CommandOptions options = new CommandOptions();

            // Work through the options.

            foreach (OptionConfiguration optionConfiguration in commandConfiguration.Options)
            {
                optionConfiguration.CreateOptions(commandLine, options);
            }

            // Check that the user didn't specify any unrecognised options.

            foreach (CommandOption option in commandLine.Options)
            {
                if (!options.Contains(option.Name) && string.Compare(option.Name, commandConfiguration.Name, true) != 0
                    && string.Compare(option.Name, "debug", true) != 0)
                {
                    throw new ApplicationException(string.Format("The '{0}' option was unexpected for the '{1}'"
                                                                 + " command.", option.Name, commandConfiguration.Name));
                }
            }

            return options;
        }

        private static string GetExceptionMessageTree(System.Exception ex)
        {
            string message;

            try
            {
                message = ex.Message;
            }
            catch (System.Exception exEx)
            {
                message = string.Format(ExceptionFormatString, exEx.GetType().FullName);
            }

            return (ex.InnerException == null ? message : message + "--> " + GetExceptionMessageTree(ex.InnerException));
        }

        #endregion

        public bool DebugMode
        {
            get { return m_debugMode; }
        }

        public Command CreateCommand(CommandLine commandLine)
        {
            if (commandLine.Values.Count > 0)
            {
                throw new ApplicationException("The command line contains some values before options: '"
                                               + string.Join(" ", commandLine.Values.ToArray()) + "'. Values should follow the options they"
                                               + " apply to.");
            }

            if ( commandLine.Options.Count == 0 )
                return null;

            // Extract the name of the command to be run.

            CommandOption commandOption = commandLine.Options[0];
            string commandName = commandOption.Name;

            if (string.Compare(commandName, "debug", true) == 0)
            {
                if (commandLine.Options.Count == 1)
                    return null;

                m_debugMode = true;
                commandOption = commandLine.Options[1];
                commandName = commandOption.Name;
            }

            if (commandOption.Values.Count > 0)
            {
                throw new ApplicationException("The '" + commandName + "' command has some values before options: '"
                                               + string.Join(" ", commandOption.Values.ToArray()) + "'. Values should follow the options they"
                                               + " apply to.");
            }

            // If there is none or the request is for help return.

            if (commandName.Length == 0 || commandName == "?")
                return null;

            // Iterate through the configurations looking for the command.

            foreach (CommandConfiguration commandConfiguration in m_commands)
            {
                if (string.Compare(commandConfiguration.Name, commandName, true) == 0)
                {
                    // Found it so create an instance.

                    object instance = Assembly.GetEntryAssembly().CreateInstance(commandConfiguration.ClassName);
                    if (instance == null)
                    {
                        throw new ApplicationException(string.Format("Cannot create an instance of the '{0}' class"
                                                                     + " corresponding to the '{1}' command.", commandConfiguration.ClassName, commandName));
                    }

                    Command command = instance as Command;
                    if ( command == null )
                    {
                        throw new ApplicationException(string.Format("An instance of the '{0}' class"
                                                                     + " corresponding to the '{1}' command was created successfully, but it does not"
                                                                     + " derive from the '{2}' class.", commandConfiguration.ClassName, commandName,
                                                                     typeof(Command).AssemblyQualifiedName));
                    }

                    // Initialise the command.

                    command.Initialise(CreateOptions(commandConfiguration, commandLine));
                    return command;
                }
            }

            return null;
        }

        public string GetUsage()
        {
            StringBuilder builder = new StringBuilder();

            // Main line.

            builder.Append(System.Environment.NewLine);
            builder.Append(System.Environment.NewLine);
            builder.Append("Usage: ");
            builder.Append(System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location));
            builder.Append(" <command> [ <options> ]");
            builder.Append(System.Environment.NewLine);

            // Commands.

            builder.Append("Commands:");
            builder.Append(System.Environment.NewLine);
            foreach ( CommandConfiguration command in m_commands )
            {
                command.GetCommandsUsage(builder);
                builder.Append(System.Environment.NewLine);
            }

            // Options.

            builder.Append("Options:");
            builder.Append(System.Environment.NewLine);
            foreach ( OptionConfiguration option in m_options )
                option.GetOptionsUsage(builder);

            return builder.ToString();
        }

        public void WriteException(System.Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException("ex");

            // If debug mode switch (/debug) is specified write the full exception text, otherwise just write
            // the summary (hierarchy of messages).

            if (DebugMode)
            {
                Console.Error.WriteLine("The following exception was thrown:");
                Console.Error.WriteLine(ex.ToString());
            }
            else
            {
                WriteExceptionSummary(ex);
            }
        }

        internal void Add(CommandConfiguration command)
        {
            m_commands.Add(command);
        }

        internal void Add(OptionConfiguration option)
        {
            m_options.Add(option);
        }

        internal OptionConfigurations Options
        {
            get { return m_options; }
        }

        private CommandConfigurations m_commands;
        private OptionConfigurations m_options;
        private bool m_debugMode = false;
    }
}