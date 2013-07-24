using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using log4net.Config;

namespace LinkMe.Utility.Configuration
{
	public sealed class ApplicationContext
	{
		#region Constants

        public const string SWEAR_WORDS_LIST = "swear.words.list";
        public const string TRACKERS_ENABLED = "trackers.enabled";
        public const string ENABLE_DEVELOPER_SHORTCUTS = "enable.developer.shortcuts";

		private const string APPLICATION_PROPERTY_PATH					= "application.property.path";

		public const string HIBERNATE_DIALECT				= "hibernate.dialect";
		public const string HIBERNATE_PROVIDER				= "hibernate.connection.provider";
		public const string HIBERNATE_DRIVER_CLASS			= "hibernate.connection.driver_class";
        public const string HIBERNATE_USE_REFLECTION_OPTIMISER = "hibernate.use.reflection.optimiser";

        public const string LOG4NET_CONFIG_FILE				= "log4Net.config.file";

		public const string STATIC_ENTITY_FILE				= "static.entity.file";

		public const string SEARCH_RESULTS_PER_PAGE = "search.results.per.page";
		public const string SEARCH_RESULTS_FRESH_DAYS = "search.results.fresh.days";
        public const string SEARCH_SPELL_SUGGESTIONS_ENABLED = "search.spell.suggestions.enabled";

		public const string STUB_LENS_RESUME_FILE = "stub.lens.resume.file";

		public const string RESUME_NAME_HEADING = "resume.name.heading";
		public const string RESUME_CONTACT_HEADING = "resume.contact.heading";
		public const string RESUME_EDUCATION_HEADING = "resume.education.heading";
		public const string RESUME_JOBS_HEADING = "resume.jobs.heading";
		public const string RESUME_CERTIFICATION_HEADING = "resume.certification.heading";

		public const string VERISIGN_MERCHANT_ACCOUNT_PARTNER = "verisign.merchant.account.partner";
		public const string VERISIGN_MERCHANT_ACCOUNT_VENDOR = "verisign.merchant.account.vendor";
		public const string VERISIGN_MERCHANT_ACCOUNT_USERNAME = "verisign.merchant.account.username";
		public const string VERISIGN_MERCHANT_ACCOUNT_PASSWORD = "verisign.merchant.account.password";

		public const string VERISIGN_HOST_ADDRESS = "verisign.host.address";
		public const string VERISIGN_HOST_PORT = "verisign.host.port";
		public const string VERISIGN_TIMEOUT = "verisign.timeout";

        public const string ALLOW_SAVING_ADMIN_PASSWORD = "allow.saving.admin.password";

		public const string ACTIVE_NETWORKER_INFO = "active.networker.info";
		public const string PASSIVE_NETWORKER_INFO = "passive.networker.info";

		public const string SHOW_AUTHENTICATION_INFO = "show.authentication.info";
		public const string SHOW_TOTAL_SEARCH_RESULTS = "show.total.search.results";

		public const string REMOTE_APPLICATION_CONTEXT = "remote.application.context";
		public const string REMOTE_APPLICATION_CONTEXT_PORT = "remote.application.context.port";
		public const string REMOTE_APPLICATION_CONTEXT_URL = "remote.application.context.url";
		public const string REMOTE_APPLICATION_CONTEXT_HOST = "remote.application.context.host";

        public const string REMOTE_RESETTABLECACHES = "remote.resettablecaches";
        public const string REMOTE_RESETTABLECACHES_URL = "remote.resettablecaches.url";
        public const string REMOTE_RESETTABLECACHES_HOST = "remote.resettablecaches.host";

		public const string ADDITIONAL_CREDIT_CARD_CHARGES = "additional.credit.card.charges";

		public const string PREVIOUS_JOB_SEARCHES_DISPLAY_COUNT = "previous.job.searches.display.count";

		public const string LINKME_CONFIG_FOLDER = "linkme.config.folder";

		public const string LENS_WATCHER_PING_INTERVAL = "lens.watcher.ping.interval";

		public const string LENS_ACTIVE_CONNECTIONS = "lens.active.connections";

		public const string LENS_FAILOVER_MESSAGES = "lens.failover.messages";

		public const string LINKME_CONTENT_MAX_LENGTH_WELCOME_PAGE_TEXT = "linkme.content.max.length.welcome.page.text";
		public const string LINKME_CONTENT_MAX_LENGTH_EMPLOYER_PAGE_TEXT = "linkme.content.max.length.employer.page.text";
		public const string LINKME_CONTENT_MAX_LENGTH_NETWORKER_DIDYOUKNOW = "linkme.content.max.length.networker.didyouknow";

		public const string EMAIL_ACTIVATION_EXPIRY_DAYS = "email.activation.expiry.days";
		public const string EMAIL_ACTIVATION_ALERT_MESSAGE = "email.activation.alert.message";

		public const string LOAD_YOUR_RESUME_ALERT_MESSAGE = "load.your.resume.alert.message";

		public const string MORE_LIKE_THIS_REQUIRES_JOB_TITLE = "more.like.this.requires.job.title";

		public const string EQUIVALENT_TERMS_ENABLED = "equivalent.terms.enabled";
        public const string EQUIVALENT_TERMS_WEIGHT = "equivalent.terms.weight";
		
        public const string JOB_AD_IRRELEVANT_TEXT_START = "job.ad.irrelevant.text.start";
        public const string JOB_ADS_PER_PAGE = "job.ads.per.page";
        public const string JOB_APPLICATIONS_PER_PAGE = "job.applications.per.page";

		public const string GOOGLE_ANALYTICS_UACCT = "google.analytics.uacct";

		public const string VALIDATE_WEB_SERVICE_REQUEST_XML = "validate.web.service.request.xml";

        public const string APPLICATION_CACHE_TIMEOUT_TOTALNETWORKERS = "application.cache.timeouts.totalnetworkersupdate";
		public const string APPLICATION_CACHE_TIMEOUT_RECENTRESUMESEARCHES =
			"application.cache.timeouts.recentresumesearchesupdate";

	    public const string DONATION_AMOUNT = "donation.amount";

        public const string ALLOWED_OUTBOUND_EMAIL_DOMAINS = "allowed.outbound.email.domains";

	    public const string FRIENDS_PER_PAGE = "friends.per.page";

        /// <summary>
        /// Entries in the job title synonym list (equivalent_terms table) that are not considered by
        /// Suggested Candidates, because they are too broad. These must be in lowercase.
        /// </summary>
        public const string IGNORED_JOB_TITLES = "ignored.job.titles";

	    public const string JOB_AD_FEED_SERVICE_CACHING_TIMEOUT_HOURS = "jobadfeed.service.cachetimeout.hours";

        public const string SSL_REDIRECT_LOGINS = "ssl.redirect-logins";

        public const string GOOGLE_MAPS_API_KEY = "google.maps.api.key";
        public const string GOOGLE_MAPS_API_KEY_LOCALHOST = "google.maps.api.key.localhost";

	    public const string NETWORKER_MAX_AUTOSUGGESTED_WHAT_IM_DOINGS = "networker.max.autosuggested.what.im.doings";

        public const string CAREERONE_MAX_NEW_JOBADS = "careerone.max.new.jobads";

        /// <summary>
        /// Set the default to be statically a "$" until we really need to internationalise.
        /// </summary>
	    public const string DEFAULT_CURRENCY_SYMBOL_FOR_SALARY = "$";

        public const string SEARCHME_SERVER = "searchme.server";

	    #endregion

        #region Nested types

        private static class InstanceHolder
        {
            internal static ApplicationContext Instance = new ApplicationContext(RuntimeEnvironment.Environment);
        }

        #endregion

		// Must be in the same order as ApplicationEnvironment enum values.

        private readonly ApplicationEnvironment _environment;

        private readonly string _applicationPropertyPath;
		private string _externalPropertyPath;
		private IDictionary<string, string> _properties; // property name -> property value
        private SortedList<string, string> _sources; // property name -> where it came from

		private ApplicationContext(ApplicationEnvironment environment)
		{
			try
			{
				_environment = environment;

				// The applicationPropertyPath may be null, eg. when running unit tests.

				_applicationPropertyPath = ConfigurationManager.AppSettings[APPLICATION_PROPERTY_PATH];

                // EP 10/09/08: DO NOT REMOVE this call to configure log4net. It's required to prevent
                // NHibernate from logging too much stuff, which is OK on a dev machine, but drastically
                // slows down the unit tests on the build server.
				XmlConfigurator.Configure();

                Configure();
            }
			catch (Exception e)
			{
                var eventLog = new EventLog {Source = "Application"};
			    eventLog.WriteEntry("Unable to configure application:" + System.Environment.NewLine
					+ MiscUtils.GetExceptionMessageTree(e), EventLogEntryType.Error);

                throw;
			}
        }

        public static void ChangeEnvironment(ApplicationEnvironment newEnvironment)
        {
            RuntimeEnvironment.ChangeEnvironment(newEnvironment);
            InstanceHolder.Instance = new ApplicationContext(RuntimeEnvironment.Environment);
        }

        public static void SetupApplications(WebSite currentWebSite)
        {
            ApplicationSetUp.SetSourceRootPath(RuntimeEnvironment.GetSourceFolder());

            var details = new Dictionary<WebSite, WebSiteDetails>();
            foreach (WebSite webSite in Enum.GetValues(typeof(WebSite)))
            {
                var host = Instance.GetProperty(GetWebSitePrefix(webSite) + ".host", true);
                var port = Instance.GetIntProperty(GetWebSitePrefix(webSite) + ".port", -1);
                var applicationPath = Instance.GetProperty(GetWebSitePrefix(webSite) + ".applicationpath", true);

                details.Add(webSite, new WebSiteDetails(host, port, applicationPath));
            }

            ApplicationSetUp.SetWebSites(details);
            ApplicationSetUp.SetCurrentApplication(currentWebSite);
        }

        private static string GetWebSitePrefix(WebSite webSite)
        {
            switch (webSite)
            {
                case WebSite.Management:
                    return "website.management";

                case WebSite.Api:
                    return "website.api";

                case WebSite.Integration:
                    return "website.integration";

                default:
                    return "website.linkme";
            }
        }

	    #region Static properties

        public static ApplicationContext Instance
        {
            get { return InstanceHolder.Instance; }
        }

	    #endregion

        #region Static methods

        private static bool ParseBoolSetting(string property, string value)
        {
            if (string.Compare(value, "true", true) == 0)
                return true;
            if (string.Compare(value, "false", true) == 0)
                return false;
            
            throw new ApplicationException(string.Format("The value of the '{0}' setting, '{1}', is not a valid boolean.", property, value));
        }

	    private static int ParseIntSetting(string property, string value)
		{
			Debug.Assert(value != null, "value != null");

			try
			{
				return int.Parse(value);
			}
			catch (FormatException)
			{
				throw new ApplicationException(string.Format("The value of the '{0}' setting, '{1}', is"
					+ " not a valid integer.", property, value));
			}
			catch (OverflowException)
			{
				throw new ApplicationException(string.Format("The value of the '{0}' setting, '{1}', is"
					+ " too small or too large for an integer.", property, value));
			}
		}

		private static decimal ParseDecimalSetting(string property, string value)
		{
			Debug.Assert(value != null, "value != null");

			try
			{
				return decimal.Parse(value);
			}
			catch (FormatException)
			{
				throw new ApplicationException(string.Format("The value of the '{0}' setting, '{1}', is"
					+ " not a valid decimal.", property, value));
			}
			catch (OverflowException)
			{
				throw new ApplicationException(string.Format("The value of the '{0}' setting, '{1}', is"
					+ " too small or too large for a decimal.", property, value));
			}
		}

		/// <summary>
		/// Combine property arrays "toOverride" and "overriding" into a dictionary. "overridden" is an array of
		/// property names that were in both "toOverride" and "overriding".
		/// </summary>
        private static IDictionary<string, string> CombineProperties(IDictionary<string, string> toOverride,
            IDictionary<string, string> overriding, out string[] overridden)
		{
		    overridden = null;

			if (toOverride != null)
			{
                var combined = new Dictionary<string, string>(toOverride);

				if (overriding != null)
				{
					var overriddenList = new List<string>();

                    foreach (KeyValuePair<string, string> kvp in overriding)
					{
						if (toOverride.ContainsKey(kvp.Key))
							overriddenList.Add(kvp.Key);
						combined[kvp.Key] = kvp.Value;
					}

					if (overriddenList.Count > 0)
					{
						overridden = overriddenList.ToArray();
					}
				}

				return combined;
			}

		    return overriding != null
                ? new Dictionary<string, string>(overriding)
                : new Dictionary<string, string>();
		}

        #endregion

        private void Configure()
		{
			const string mainFileName = "application.config";
			const string prefix = "application-";
			const string suffix = ".config";

			string envFileName = prefix + RuntimeEnvironment.GetApplicationEnvironmentName(_environment) + suffix;
            string hostFileName = prefix + RuntimeEnvironment.HostName + suffix;

			LoadApplicationPropertyPath();

            // Read property files as follows (for more details see "GuideToLinkMeConfiguration" on the Wiki):
			// 1) Embedded "application.config"
			// 2) Embedded "application-ENV.config" (the same setting must not be defined in 1 and 2).
			// 3) externalPropertyPath\"application-ENV.config" (overrides whatever is defined in 1 and 2.
            // 4) externalPropertyPath\"application-HOSTNAME.config" (overrides whatever is defined in 1 and 2, but
			// the same setting must not be defined in 3 and 4).
            // 5) sourceRootPath\config\"application-ENV.config" (overrides whatever is defined in 1, 2, 3 and 4.
            // 6) sourceRootPath\config\"application-HOSTNAME.config" (overrides whatever is defined in
            // 1, 2, 3 and 4, but the same setting must not be defined in 5 and 6).

            _sources = new SortedList<string, string>();

            IDictionary<string, string> embeddedMain = LoadEmbeddedProperties(mainFileName); // 1
            IDictionary<string, string> embeddedEnv = LoadEmbeddedProperties(envFileName); // 2

			string[] overridden;
            IDictionary<string, string> tempProperties = CombineProperties(embeddedMain, embeddedEnv,
                out overridden);

            if (overridden != null)
			{
				throw new ApplicationException(string.Format("The following configuration properties"
					+ " were defined in both '{0}' (embedded) and '{1}' (embedded): '{2}'. A given"
					+ " property is either environment-specific or not, so it must be defined either in"
					+ " '{0}' or in EACH of the environment-specific files.", mainFileName, envFileName,
					string.Join("', '", overridden)));
			}

            IDictionary<string, string> externalProperties = GetExternalProperties(RuntimeEnvironment.HostName,
                _externalPropertyPath, envFileName, hostFileName); // 3 + 4

            // Combine the embedded and external properties into (external config files override the
            // embedded ones).

			_properties = CombineProperties(tempProperties, externalProperties, out overridden);

            var sourceFolder = RuntimeEnvironment.GetSourceFolder();
            if (sourceFolder != null)
            {
                string sourceConfigPath = Path.Combine(sourceFolder, "config");
                IDictionary<string, string> sourceProperties = GetExternalProperties(
                    RuntimeEnvironment.HostName, sourceConfigPath, envFileName, hostFileName); // 5 + 6

                // Add in the the properties under <source root>\config, overriding anything else.

                string[] dummy;
                _properties = CombineProperties(_properties, sourceProperties, out dummy);
            }
		}

        private IDictionary<string, string> GetExternalProperties(string hostname, string externalPath,
            string envFileName, string hostFileName)
	    {
            string[] overridden;
            IDictionary<string, string> externalEnv = LoadExternalProperties(externalPath, envFileName);
            IDictionary<string, string> externalHost = null;

	        // Avoid confusion between environment and machine files - if the machine name is also an
	        // environment name then don't try to load the machine-specific file.

            hostname = hostname.ToLower();
	        if (hostname == "dev" || hostname == "uat" || hostname == "prod")
	        {
	        }
	        else
	        {
                externalHost = LoadExternalProperties(externalPath, hostFileName);
	        }

            IDictionary<string, string> externalProperties = CombineProperties(externalEnv, externalHost, out overridden);
	        if (overridden != null)
	        {
	            throw new ApplicationException(string.Format("The following configuration properties"
	                + " were defined in both '{0}' and '{1}': '{2}'. A given property may be overridden"
	                    + " in the environment-specific file or in the machine-specific file, but not both.",
                    GetExternalFilePath(externalPath, envFileName),
                    GetExternalFilePath(externalPath, hostFileName), string.Join("', '", overridden)));
	        }

            return externalProperties;
	    }

		private void LoadApplicationPropertyPath()
		{
		    if (_applicationPropertyPath != null)
			{
				_externalPropertyPath = _applicationPropertyPath;
			}
		}

        private static Stream GetResourceStream(string name)
        {
            string resourceName = "LinkMe.Utility.res." + name;

			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

			if (stream == null)
			{
				throw new ApplicationException(string.Format("Embedded configuration file '{0}' was"
					+ " not found in assembly '{1}'.",
					resourceName, Assembly.GetExecutingAssembly().Location));
			}

            return stream;
        }

        private IDictionary<string, string> LoadEmbeddedProperties(string propertyFilename)
		{
            using (Stream stream = GetResourceStream(propertyFilename))
			{
				try
				{
					return ReadConfigFile(stream, propertyFilename + " (embedded)");
				}
				catch (Exception ex)
				{
					throw new ApplicationException("Failed to read embedded configuration file '"
						+ propertyFilename + "'.", ex);
				}
			}
		}

		private static string GetExternalFilePath(string path, string fileName)
		{
		    return string.IsNullOrEmpty(path) ? null : Path.Combine(path, fileName);
		}

	    private IDictionary<string, string> LoadExternalProperties(string path, string propertyFilename)
		{
	        string filePath = GetExternalFilePath(path, propertyFilename);

			if (filePath == null)
			{
				return null;
			}
	
            if (File.Exists(filePath))
	        {
	            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
	            {
	                try
	                {
	                    return ReadConfigFile(stream, filePath);
	                }
	                catch (Exception ex)
	                {
	                    throw new ApplicationException("Failed to read external configuration file '"
	                        + filePath + "'.", ex);
	                }
	            }
	        }
	        
	        return null;
		}

        private IDictionary<string, string> ReadConfigFile(Stream stream, string sourceDescription)
		{
			var serializer = new XmlSerializer(typeof(Property[]));
			var array = (Property[])serializer.Deserialize(new StreamReader(stream));

            IDictionary<string, string> dictionary = new Dictionary<string, string>(array.Length);
			foreach (Property property in array)
			{
			    try
				{
					dictionary.Add(property.Name, property.Value);
				}
				catch (ArgumentException ex)
				{
					throw new ApplicationException("Property '" + property.Name
						+ "' was defined more than once in the same file.", ex);
				}

                _sources[property.Name] = sourceDescription;
			}

			return dictionary;
		}

		public string GetProperty(string property)
		{
			return GetProperty(property, false);
		}

		public string GetProperty(string property, bool allowNullOrEmpty)
		{
		    if (string.IsNullOrEmpty(property))
		        throw new ArgumentException("The property must be specified.", "property");

            string value;
            _properties.TryGetValue(property, out value);

			if (!allowNullOrEmpty && string.IsNullOrEmpty(value))
			{
				throw new ApplicationException(string.Format("There is no value for the '{0}' setting.",
					property));
			}

            return value;
		}

		public bool GetBoolProperty(string property)
		{
            return ParseBoolSetting(property, GetProperty(property));
		}

        public bool GetBoolProperty(string property, bool defaultValue)
        {
            string value = GetProperty(property, true);
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return ParseBoolSetting(property, value);
        }

        public int GetIntProperty(string property, int defaultValue)
		{
			string value = GetProperty(property, true);
			if (string.IsNullOrEmpty(value))
				return defaultValue;

			return ParseIntSetting(property, value);
		}

		public int GetIntProperty(string property)
		{
			return ParseIntSetting(property, GetProperty(property));
		}

		public decimal GetDecimalProperty(string property)
		{
			return ParseDecimalSetting(property, GetProperty(property));
		}

        public decimal GetDecimalProperty(string property, decimal defaultValue)
        {
            string value = GetProperty(property, true);
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return ParseDecimalSetting(property, value);
        }

        public string[] GetStringArrayProperty(string property)
		{
			return GetStringArrayProperty(property, ',');
		}

        public string[] GetStringArrayProperty(string property, char delimiter)
        {
            return GetStringArrayProperty(property, delimiter, false);
        }

		public string[] GetStringArrayProperty(string property, char delimiter, bool allowNullOrEmpty)
		{
            string value = GetProperty(property, allowNullOrEmpty);
            return (string.IsNullOrEmpty(value) ? null : value.Split(delimiter));
		}

		public void SetProperty(string property, string value)
		{
			_properties[property] = value;
            _sources[property] = "(set by SetProperty call)";
		}

		public void Reset()
		{
		    _properties = null;
            _sources = null;

            Configure();
		}

	    /// <summary>
        /// Returns the value of a property that contains a path, which may be absolute (usually in prod/UAT)
        /// or relative to the source root directory (in dev).
        /// </summary>
        public string GetSourcePathProperty(string property)
        {
            string value = GetProperty(property);
            if (FileSystem.IsAbsolutePath(value))
                return value;

	        var sourceFolder = RuntimeEnvironment.GetSourceFolder();
            if (sourceFolder == null)
            {
                throw new ApplicationException(string.Format("The value of the '{0}' application property is a relative path ('{1}'),"
                    + " but the source root directory does not exist. If you have moved the binaries from the directory or machine"
                    + " where they were built the property will have to be an absolute path.", property, value));
            }

	        return FileSystem.GetAbsolutePath(value, sourceFolder);
        }

        public string GetSourcePath(string property)
        {
            string value = GetProperty(property);
            if (FileSystem.IsAbsolutePath(value))
                return value;

            var sourceFolder = RuntimeEnvironment.GetSourceFolder();
            if (sourceFolder == null)
            {
                throw new ApplicationException(string.Format("The value of the '{0}' application property is a relative path ('{1}'),"
                    + " but the source root directory does not exist. If you have moved the binaries from the directory or machine"
                    + " where they were built the property will have to be an absolute path.", property, value));
            }

            return FileSystem.GetAbsolutePath(value, sourceFolder);
        }

        public string GetPathRelativeToConfig(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentException("The relative path must be specified.", "relativePath");

            return FileSystem.IsAbsolutePath(relativePath)
                ? relativePath
                : FileSystem.GetAbsolutePath(relativePath, GetSourcePathProperty(LINKME_CONFIG_FOLDER));
        }

	    /// <summary>
        /// Opens the configuration file located under the configuration folder.
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public System.Configuration.Configuration OpenConfiguration(string relativeFilePath)
        {
            var configFile = new ExeConfigurationFileMap
            {
                ExeConfigFilename = GetPathRelativeToConfig(relativeFilePath)
            };

	        return ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        }

        public IDictionary<string, string> GetPropertyValues()
        {
            // Clone to prevent changes by the caller.
            return new Dictionary<string, string>(_properties);
        }

        public IDictionary<string, string> GetPropertySources()
        {
            // Clone to prevent changes by the caller.
            return new Dictionary<string, string>(_sources);
        }
    }
}
