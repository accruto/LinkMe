
namespace LinkMe.Environment.CommandLines
{
    public abstract class Command
    {
        internal void Initialise(CommandOptions options)
        {
            m_options = options;
        }

        public abstract void Execute();

        protected CommandOptions Options
        {
            get { return m_options; }
        }

        private CommandOptions m_options;
    }
}