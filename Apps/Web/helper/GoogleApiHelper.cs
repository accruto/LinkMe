using System;
using System.Collections.Generic;
using System.Diagnostics;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Helper
{
    public static class GoogleApiHelper
    {
        private static readonly string _localhostKey = ApplicationContext.Instance.GetProperty(ApplicationContext.GOOGLE_MAPS_API_KEY_LOCALHOST);
        private static readonly IDictionary<string, string> _keysByHost = GetKeys();

        public static string GetMapsApiKey(ReadOnlyUrl url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            return GetMapsApiKey(url.Host);
        }

        public static string GetMapsApiKey(string host)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentException("The host must be specified.", "host");
 
            // The key you need depends not just on the environment, but on the exact hostname used.
            // It's a bit of a pain in the arse, to say the least! See case 3696.

            if (string.Equals(host, "localhost", StringComparison.OrdinalIgnoreCase))
                return _localhostKey;

            string value;
            if (_keysByHost.TryGetValue(host, out value))
                return value; // Got a value for the requested hostname, should work.

            return null; // No key for this hostname.
        }

        private static IDictionary<string, string> GetKeys()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            string[] keySettings = ApplicationContext.Instance.GetStringArrayProperty(
                ApplicationContext.GOOGLE_MAPS_API_KEY, ';');
            Debug.Assert(keySettings.Length > 0, "keySettings.Length > 0");

            foreach (string hostSetting in keySettings)
            {
                string[] pair = hostSetting.Split(':');
                if (pair.Length == 2)
                {
                    string host = pair[0].Trim();
                    if (host.Length == 0)
                    {
                        throw new ApplicationException(string.Format("The Google Maps API key setting '{0}' is"
                            + " invalid - the hostname must be specified.", hostSetting));
                    }

                    string apiKey = pair[1].Trim();
                    if (apiKey.Length == 0)
                    {
                        throw new ApplicationException(string.Format("The Google Maps API key setting '{0}' is"
                            + " invalid - the API key must be specified.", hostSetting));
                    }

                    dictionary.Add(host, apiKey);
                }
                else if (hostSetting.Trim().Length > 0)
                {
                    throw new ApplicationException(string.Format("The Google Maps API key setting '{0}' is not"
                        + " in a valid format. The correct format is 'hostname: key'", hostSetting));
                }
            }

            return dictionary;
        }
    }
}
