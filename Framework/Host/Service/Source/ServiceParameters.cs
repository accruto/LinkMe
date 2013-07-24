namespace LinkMe.Framework.Host.Service
{
	public class ServiceParameters
	{
		public ServiceParameters(string serviceName, string applicationRootFolder, string configurationFile)
		{
		    _serviceName = serviceName;
		    _applicationRootFolder = applicationRootFolder;
		    _configurationFile = configurationFile;
		}

	    public string ServiceName
	    {
            get { return _serviceName; }
	    }

        public string ApplicationRootFolder
        {
            get { return _applicationRootFolder; }
        }

        public string ConfigurationFile
        {
            get { return _configurationFile; }
        }

	    private readonly string _serviceName;
	    private readonly string _applicationRootFolder;
        private readonly string _configurationFile;
	}
}
