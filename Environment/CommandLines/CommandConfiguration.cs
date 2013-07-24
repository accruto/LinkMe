using System.Collections;
using System.Text;

namespace LinkMe.Environment.CommandLines
{
    /// <summary>
    /// Summary description for CommandConfiguration.
    /// </summary>
    internal class CommandConfiguration
    {
        public CommandConfiguration(string name, string className, string description)
        {
            m_name = name;
            m_class = className;
            m_description = description;
            m_options = new OptionConfigurations();
        }

        public string Name
        {
            get { return m_name; }
        }

        public string ClassName
        {
            get { return m_class; }
        }

        public OptionConfigurations Options
        {
            get { return m_options; }
        }

        public void Add(OptionConfiguration option)
        {
            m_options.Add(option);
        }

        public void GetCommandsUsage(StringBuilder builder)
        {
            builder.Append("  /");
            builder.Append(m_name);

            foreach ( OptionConfiguration option in m_options )
            {
                builder.Append(" ");
                option.GetCommandsUsage(builder);
            }

            builder.Append(System.Environment.NewLine);

            if ( m_description.Length != 0 )
            {
                builder.Append("    ");
                builder.Append(m_description);
                builder.Append(System.Environment.NewLine);
            }
        }

        private string m_name;
        private string m_class;
        private string m_description;
        private OptionConfigurations m_options;
    }

    internal class CommandConfigurations
    {
        public CommandConfigurations()
        {
            m_commands = new ArrayList();
        }

        public IEnumerator GetEnumerator()
        {
            return m_commands.GetEnumerator();
        }

        public void Add(CommandConfiguration command)
        {
            m_commands.Add(command);
        }

        private ArrayList m_commands;
    }
}