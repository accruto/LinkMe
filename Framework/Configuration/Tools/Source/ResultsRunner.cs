using System.Threading;
using System.Collections;

using LinkMe.Framework.Configuration.Tools.Forms;

namespace LinkMe.Framework.Configuration.Tools
{
	internal class Result
	{
		public Result(ConfigurationEvent configurationEvent, string message, System.Exception e)
		{
			m_configurationEvent = configurationEvent;
			m_message = message;
			m_exception = e;
		}

		public Result(ConfigurationEvent configurationEvent, string message, string extraInfo)
		{
			m_configurationEvent = configurationEvent;
			m_message = message;
			m_extraInfo = extraInfo;
		}

		public Result(ConfigurationEvent configurationEvent, string message)
		{
			m_configurationEvent = configurationEvent;
			m_message = message;
		}

		public ConfigurationEvent Event
		{
			get { return m_configurationEvent; }
		}

		public string Message
		{
			get { return m_message; }
		}

		public System.Exception Exception
		{
			get { return m_exception; }
		}

		public string ExtraInfo
		{
			get { return m_extraInfo; }
		}

		private ConfigurationEvent m_configurationEvent;
		private string m_message;
		private System.Exception m_exception;
		private string m_extraInfo;
	}

	internal class ResultsConfigurationEventSource
		:	ConfigurationEventSource
	{
        public ResultsConfigurationEventSource(ResultsRunner runner)
		{
			m_runner = runner;
		}

		protected override void Raise(ConfigurationEvent configurationEvent, string message)
		{
			m_runner.Raise(configurationEvent, message);
		}

		protected override void Raise(ConfigurationEvent configurationEvent, string message, System.Exception ex)
		{
			m_runner.Raise(configurationEvent, message, ex);
		}

		protected override void Raise(ConfigurationEvent configurationEvent, string message, string extraInfo)
		{
			m_runner.Raise(configurationEvent, message, extraInfo);
		}

		private ResultsRunner m_runner;
	}

	public abstract class ResultsRunner
	{
		protected ResultsRunner(object data)
		{
			m_data = data;
			m_eventSource = new ResultsConfigurationEventSource(this);
		}

		public void Run()
		{
			// Create the form.

			m_form = CreateForm(m_data);

			// Create the thread.

			Thread thread = new Thread(new ThreadStart(RunThread));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();

			// Show the results dialog.

			m_form.ShowDialog();
		}

		private void RunThread()
		{
			try
			{
				Run(m_data, m_eventSource);
			}
			catch ( System.Exception e )
			{
				m_form.InvokeRaise(ConfigurationEvent.Error, "Exception thrown: " + e.Message, e);
			}

			// Once the thread is finished let the user be able to close the dialog.

			m_form.InvokeComplete();
		}

		public void Raise(ConfigurationEvent configurationEvent, string message)
		{
			m_form.InvokeRaise(configurationEvent, message);
		}

		public void Raise(ConfigurationEvent configurationEvent, string message, System.Exception ex)
		{
			m_form.InvokeRaise(configurationEvent, message, ex);
		}

		public void Raise(ConfigurationEvent configurationEvent, string message, string extraInfo)
		{
			m_form.InvokeRaise(configurationEvent, message, extraInfo);
		}

		protected abstract void Run(object data, IConfigurationEventSource eventSource);

		protected virtual ResultsForm CreateForm(object data)
		{
			return new ResultsForm("Results", null);
		}

		private object m_data;
		private ResultsForm m_form;
        private IConfigurationEventSource m_eventSource;
	}
}
