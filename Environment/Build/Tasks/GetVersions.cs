using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class GetVersions
        : Task
    {
        private string _buildVersion;
        private string _productVersion;

        /// <summary>
        /// e.g. 9.7.1.23
        /// </summary>
        public string BuildVersion
        {
            get { return _buildVersion ?? string.Empty; }
            set { _buildVersion = value; }
        }

        /// <summary>
        /// e.g. 9.7.1
        /// </summary>
        [Output]
        public string ProductVersion
        {
            get { return _productVersion ?? string.Empty; }
        }

        /// <summary>
        /// e.g. 9,7,1
        /// </summary>
        [Output]
        public string ProductVersionComma
        {
            get { return (_productVersion ?? string.Empty).Replace('.', ','); }
        }

        /// <summary>
        /// e.g. 9.7.1.0
        /// </summary>
        [Output]
        public string FullProductVersion
        {
            get { return _productVersion == null ? string.Empty : _productVersion + ".0"; }
        }

        /// <summary>
        /// e.g. 9,7,1,0
        /// </summary>
        [Output]
        public string FullProductVersionComma
        {
            get { return _productVersion == null ? string.Empty : _productVersion.Replace('.', ',') + ",0"; }
        }

        [Output]
        public string BuildVersionComma
        {
            get { return (_buildVersion ?? string.Empty).Replace('.', ','); }
        }

        public override bool Execute()
        {
            try
            {
                _productVersion = VersionUtil.GetProductVersion(_buildVersion);
                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }
    }
}