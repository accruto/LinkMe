using System;
using System.Collections;

namespace LinkMe.Environment.CommandLines
{
    public class CommandValues : IEnumerable
    {
        internal CommandValues()
        {
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        #endregion

        internal void Add(string value)
        {
            m_values.Add(value);
        }

        public int Count
        {
            get { return m_values.Count; }
        }

        public string this[int index]
        {
            get { return (string) m_values[index]; }
        }

        public string[] ToArray()
        {
            return (string[])m_values.ToArray(typeof(string));
        }

        private ArrayList m_values = new ArrayList();
    }

    public class CommandOption
    {
        internal CommandOption(string name)
        {
            m_name = name;
        }

        public string Name
        {
            get { return m_name; }
        }

        public bool IsValueSupplied
        {
            get { return m_values.Count > 0; }
        }

        public CommandValues Values
        {
            get { return m_values; }
        }

        private string m_name;
        private CommandValues m_values = new CommandValues();
    }

    public class CommandOptions : IEnumerable
    {
        internal CommandOptions()
        {
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return m_options.GetEnumerator();
        }

        #endregion

        public int Count
        {
            get { return m_options.Count; }
        }

        public CommandOption this[int index]
        {
            get { return (CommandOption)m_options[index]; }
        }

        public CommandOption this[string name]
        {
            get
            {
                foreach (CommandOption option in m_options)
                {
                    if (string.Compare(option.Name, name, true) == 0)
                        return option;
                }

                return null;
            }
        }

        public bool Contains(string name)
        {
            foreach (CommandOption option in m_options)
            {
                if (string.Compare(option.Name, name, true) == 0)
                    return true;
            }

            return false;
        }

        internal void Add(CommandOption option)
        {
            if (Contains(option.Name))
                throw new ApplicationException("Command option '" + option.Name + "' has already been added.");

            m_options.Add(option);
        }

        private ArrayList m_options = new ArrayList(); // Don't use a Hashtable as the order is important.
    }
}