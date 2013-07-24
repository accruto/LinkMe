using System.ServiceProcess;

namespace LinkMe.Framework.Host.Service
{
	internal class Service
		:	ServiceBase
	{
		#region Private Fields

		private readonly ServiceManager _manager;

		#endregion

		public Service(string serviceName, string applicationRootFolder, string configurationFile)
		{
			// Make a unique service name.

			ServiceName = serviceName;
			_manager = new ServiceManager(new ServiceParameters(serviceName, applicationRootFolder, configurationFile));

            CanPauseAndContinue = true;
		}

		#region ServiceBase Overrides.

        // If an exceptions occur here log them and rethrow for Service Manager to handle. Without this
        // logging only a small part of the stack trace is shown in the Windows Application log.

		protected override void OnStart(string[] args)
		{
#if BREAKONSTART
			Debugger.Break();
#endif
            _manager.Start();
		}
 
		protected override void OnStop()
		{
		    _manager.Stop();
        }

        protected override void OnPause()
		{
            _manager.Pause();
        }

        protected override void OnContinue()
		{
            _manager.Continue();
        }

		#endregion
	}
}
