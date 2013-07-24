using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using LinkMe.Utility.Utilities;

namespace LinkMe.Apps.Utility.Test
{
    /// <summary>
    /// Temporarily extracts a resource file as a temporary file, which is deleted when this object is disposed.
    /// </summary>
    public class ResourceAsTempFile : IDisposable
    {
        private string _filePath;

        public ResourceAsTempFile(Assembly assembly, string resourceName)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentException("The resource name must be specified.", "resourceName");

            var input = assembly.GetManifestResourceStream(resourceName);
            if (input == null)
            {
                throw new ApplicationException(string.Format("There is no resource named '{0}' in assembly '{1}'.",
                    resourceName, assembly.FullName));
            }

            using (input)
            {
                _filePath = Path.GetTempFileName();
                using (var output = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    StreamHelper.CopyStream(input, output);
                }
            }
        }

        ~ResourceAsTempFile()
        {
            Debug.Assert(_filePath == null, "Dispose() should have been called for a " + GetType().Name
                + " object to delete temporary file " + _filePath);
            DisposeInternal();
        }

        #region IDisposable members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            DisposeInternal();
        }

        #endregion

        public string FilePath
        {
            get { return _filePath; }
        }

        private void DisposeInternal()
        {
            if (_filePath != null && File.Exists(_filePath))
            {
                File.Delete(_filePath);
                _filePath = null;
            }
        }
    }
}
