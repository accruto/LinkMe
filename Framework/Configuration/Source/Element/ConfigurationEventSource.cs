using System;

namespace LinkMe.Framework.Configuration
{
	public enum ConfigurationEvent
	{
		Information,
		Error,
		Warning
	}

	public interface IConfigurationEventSource
	{
		void Raise(ConfigurationEvent resultEvent, string message);
		void Raise(ConfigurationEvent resultEvent, string message, System.Exception ex);
		void Raise(ConfigurationEvent resultEvent, string message, string extraInfo);
		int Informations { get; }
		int Errors { get; }
		int Warnings { get; }
	}

	public abstract class ConfigurationEventSource
		:	System.MarshalByRefObject,
			IConfigurationEventSource
	{
        void IConfigurationEventSource.Raise(ConfigurationEvent configurationEvent, string message)
		{
			Raise(configurationEvent);
			Raise(configurationEvent, message);
		}

        void IConfigurationEventSource.Raise(ConfigurationEvent configurationEvent, string message, System.Exception ex)
		{
			Raise(configurationEvent);
			Raise(configurationEvent, message, ex);
		}

        void IConfigurationEventSource.Raise(ConfigurationEvent configurationEvent, string message, string extraInfo)
		{
			Raise(configurationEvent);
			Raise(configurationEvent, message, extraInfo);
		}

        int IConfigurationEventSource.Informations
		{
			get { return m_informations; }
		}

        int IConfigurationEventSource.Errors
		{
			get { return m_errors; }
		}

        int IConfigurationEventSource.Warnings
		{
			get { return m_warnings; }
		}

		protected abstract void Raise(ConfigurationEvent configurationEvent, string message);
		protected abstract void Raise(ConfigurationEvent configurationEvent, string message, System.Exception ex);
		protected abstract void Raise(ConfigurationEvent configurationEvent, string message, string extraInfo);

		protected void Raise(ConfigurationEvent configurationEvent)
		{
			switch ( configurationEvent )
			{
				case ConfigurationEvent.Information:
					++m_informations;
					break;

				case ConfigurationEvent.Error:
					++m_errors;
					break;

				case ConfigurationEvent.Warning:
					++m_warnings;
					break;
			}
		}

		private int m_informations;
		private int m_errors;
		private int m_warnings;
	}

	public class NullConfigurationEventSource
        : ConfigurationEventSource
	{
		protected override void Raise(ConfigurationEvent configurationEvent, string message)
		{
		}

		protected override void Raise(ConfigurationEvent configurationEvent, string message, System.Exception ex)
		{
		}

		protected override void Raise(ConfigurationEvent configurationEvent, string message, string extraInfo)
		{
		}
	}

    public class ConsoleConfigurationEventSource
        : ConfigurationEventSource
    {
        protected override void Raise(ConfigurationEvent configurationEvent, string message)
        {
            Console.WriteLine(message);
        }

        protected override void Raise(ConfigurationEvent configurationEvent, string message, Exception ex)
        {
            Console.WriteLine(message + " Exception: " + ex.Message);
        }

        protected override void Raise(ConfigurationEvent configurationEvent, string message, string extraInfo)
        {
            Console.WriteLine(message + " Info: " + extraInfo);
        }
    }
}
