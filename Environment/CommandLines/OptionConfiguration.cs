using System;
using System.Collections;
using System.Text;

namespace LinkMe.Environment.CommandLines
{
    internal abstract class OptionConfiguration
    {
        protected OptionConfiguration(string name, bool required)
        {
            m_name = name;
            m_required = required;
        }

        public string Name
        {
            get { return m_name; }
        }

        public abstract void GetCommandsUsage(StringBuilder builder);
        public abstract void GetOptionsUsage(StringBuilder builder);
        public abstract bool CreateOptions(CommandLine commandLine, CommandOptions options);

        protected string m_name;
        protected bool m_required;
    }

    /// <summary>
    /// Summary description for OptionConfiguration.
    /// </summary>
    internal class FlagOptionConfiguration
        :	OptionConfiguration
    {
        public FlagOptionConfiguration(string name, bool required, bool value, bool multiple, string description)
            :	base(name, required)
        {
            m_value = value;
            m_description = description;
            m_multiple = multiple;
            m_valueNames = new ArrayList();
        }

        public void AddValueName(string name)
        {
            m_valueNames.Add(name);
        }

        public override void GetCommandsUsage(StringBuilder builder)
        {
            if ( !m_required )
                builder.Append("[ ");

            builder.Append("/");
            builder.Append(m_name);
            if ( m_valueNames.Count > 0 )
                builder.Append(" <...>");

            if ( !m_required )
                builder.Append(" ]");
        }

        public override void GetOptionsUsage(StringBuilder builder)
        {
            builder.Append("  /");
            builder.Append(m_name);

            foreach ( string name in m_valueNames )
            {
                builder.Append(" <");
                builder.Append(name);
                builder.Append(">");
            }

            builder.Append(System.Environment.NewLine);

            if ( m_description.Length != 0 )
            {
                builder.Append("    ");
                builder.Append(m_description);
                builder.Append(System.Environment.NewLine);
            }

            builder.Append(System.Environment.NewLine);
        }

        public override bool CreateOptions(CommandLine commandLine, CommandOptions options)
        {
            CommandOption option = commandLine.Options[m_name];

            if (option == null)
            {
                // Check whether this option is required.

                if ( m_required )
                    throw new ApplicationException("The '" + m_name + "' option is required.");
                else
                    return false;
            }

            // Check for a value.

            int valueCount = option.Values.Count;
            if (m_value)
            {
                if (m_multiple)
                {
                    // Check for multiples

                    if (valueCount == 0)
                    {
                        throw new ApplicationException("One or more values are expected for the '"
                                                       + m_name + "' option, but none were supplied.");
                    }
                }
                else
                {
                    if (valueCount == 0)
                    {
                        throw new ApplicationException("A value is expected for the '"
                                                       + m_name + "' option, but none was supplied.");
                    }
                    else if (valueCount > 1)
                    {
                        throw new ApplicationException(string.Format("Only one value is expected for the '{0}'"
                                                                     + " option, but {1} were supplied.", m_name, valueCount));
                    }
                }
            }
            else if (valueCount > 0)
            {
                throw new ApplicationException(string.Format("No values are expected for the '{0}'"
                                                             + " option, but {1} {2} supplied.", m_name, valueCount, valueCount == 1 ? "was" : "were"));
            }

            // The option is valid, so add it to the collection.

            options.Add(option);

            return true;
        }

        private bool m_value;
        private string m_description;
        private bool m_multiple;
        private ArrayList m_valueNames;
    }

    internal class OrOptionConfiguration
        :	OptionConfiguration
    {
        public OrOptionConfiguration(string name, bool required)
            :	base(name, required)
        {
            m_options = new OptionConfigurations();
        }

        public override void GetCommandsUsage(StringBuilder builder)
        {
            if ( m_options.Count > 0 )
            {
                if ( !m_required )
                    builder.Append("[ ");

                bool first = true;
                foreach ( OptionConfiguration option in m_options )
                {
                    if ( first )
                        first = false;
                    else
                        builder.Append(" | ");

                    option.GetCommandsUsage(builder);
                }

                if ( !m_required )
                    builder.Append(" ]");
            }
        }

        public override void GetOptionsUsage(StringBuilder builder)
        {
            foreach ( OptionConfiguration option in m_options )
                option.GetOptionsUsage(builder);
        }

        public override bool CreateOptions(CommandLine commandLine, CommandOptions options)
        {
            // Look for which option is supplied.

            foreach ( OptionConfiguration configuration in m_options )
            {
                if ( configuration.CreateOptions(commandLine, options) )
                    return true;
            }

            // None of the options were supplied.

            if ( m_required )
            {
                string optionList = string.Empty;
                foreach ( OptionConfiguration configuration in m_options )
                    optionList += optionList.Length == 0 ? configuration.Name : ", " + configuration.Name;
                throw new ApplicationException("One of the options (" + optionList + ") must be supplied.");
            }

            return false;
        }

        public void Add(OptionConfiguration option)
        {
            m_options.Add(option);
        }

        private OptionConfigurations m_options;
    }

    internal class AndOptionConfiguration
        :	OptionConfiguration
    {
        public AndOptionConfiguration(string name, bool required)
            :	base(name, required)
        {
            m_options = new OptionConfigurations();
        }

        public override void GetCommandsUsage(StringBuilder builder)
        {
            if ( m_options.Count > 0 )
            {
                if ( !m_required )
                    builder.Append("[ ");
                else
                    builder.Append("( ");

                bool first = true;
                foreach ( OptionConfiguration option in m_options )
                {
                    if ( first )
                        first = false;
                    else
                        builder.Append(" ");
                    option.GetCommandsUsage(builder);
                }

                if ( !m_required )
                    builder.Append(" ]");
                else
                    builder.Append(" )");
            }
        }

        public override void GetOptionsUsage(StringBuilder builder)
        {
            foreach ( OptionConfiguration option in m_options )
                option.GetOptionsUsage(builder);
        }

        public override bool CreateOptions(CommandLine commandLine, CommandOptions options)
        {
            return false;
        }

        public void Add(OptionConfiguration option)
        {
            m_options.Add(option);
        }

        private OptionConfigurations m_options;
    }

    internal class OptionConfigurations
    {
        public OptionConfigurations()
        {
            m_options = new ArrayList();
        }

        public IEnumerator GetEnumerator()
        {
            return m_options.GetEnumerator();
        }

        public int Count
        {
            get { return m_options.Count; }
        }

        public OptionConfiguration this[string name]
        {
            get
            {
                foreach ( OptionConfiguration option in m_options )
                {
                    if ( string.Compare(option.Name, name, true) == 0 )
                        return option;
                }

                return null;
            }
        }

        public void Add(OptionConfiguration option)
        {
            m_options.Add(option);
        }

        private ArrayList m_options;
    }
}