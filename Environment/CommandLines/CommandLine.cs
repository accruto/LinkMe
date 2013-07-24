namespace LinkMe.Environment.CommandLines
{
    public class CommandLine
    {
        private CommandValues m_values = new CommandValues();
        private CommandOptions m_options = new CommandOptions();

        public CommandLine(string commandLine)
            :	this(commandLine.Split(' '))
        {
        }

        public CommandLine(string[] commandLineArgs)
        {
            if (commandLineArgs == null)
                throw new System.ArgumentNullException("commandLineArgs");

            CommandOption currentOption = null;

            foreach (string arg in commandLineArgs)
            {
                if (arg.StartsWith("/"))
                {
                    // This is an option. Create it if it doesn't already exist (allow the same option to be
                    // specified twice - the values are appended in that case).

                    string optName = arg.Substring(1);

                    currentOption = m_options[optName];
                    if (currentOption == null)
                    {
                        currentOption = new CommandOption(optName);
                        m_options.Add(currentOption);
                    }
                }
                else
                {
                    // This is a value. Add it to the current option or to this object's values collection if
                    // it appears before any options.

                    if (currentOption == null)
                    {
                        m_values.Add(arg);
                    }
                    else
                    {
                        currentOption.Values.Add(arg);
                    }
                }
            }
        }

        public CommandValues Values
        {
            get { return m_values; }
        }

        public CommandOptions Options
        {
            get { return m_options; }
        }
    }
}