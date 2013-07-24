using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility.Wmi
{
	/// <summary>
	/// Provides static methods for accessing WMI.
	/// </summary>
	public sealed class WmiUtil
	{
		private const char m_namespaceSeparator = '\\';
		private const string m_namespaceClassName = "__NAMESPACE";
		private const string m_namespacePropertyName = "Name";

		private static ArrayList m_eventWatchers = null;
		private static readonly object m_eventWatcherLock = new object();

		private WmiUtil()
		{
		}

		public static ManagementObject GetObject(string path)
		{
			const string method = "GetObject";

			if (path == null)
				throw new NullParameterException(typeof(WmiUtil), method, "path");

			ManagementObject managementObject = new ManagementObject(path);

			return (GetObject(managementObject) ? managementObject : null);
		}

		public static ManagementObject GetObject(ManagementScope scope, string path)
		{
			const string method = "GetObject";

			if (scope== null)
				throw new NullParameterException(typeof(WmiUtil), method, "scope");
			if (path == null)
				throw new NullParameterException(typeof(WmiUtil), method, "path");

			ManagementObject managementObject = new ManagementObject(scope, new ManagementPath(path),
				new ObjectGetOptions());

			return (GetObject(managementObject) ? managementObject : null);
		}

		public static ManagementClass GetClass(string path)
		{
			const string method = "GetClass";

			if (path == null)
				throw new NullParameterException(typeof(WmiUtil), method, "path");

			ManagementClass managementClass = new ManagementClass(path);

			return (GetObject(managementClass) ? managementClass : null);
		}

		public static ManagementClass GetClass(ManagementScope scope, string path)
		{
			const string method = "GetClass";

			if (scope== null)
				throw new NullParameterException(typeof(WmiUtil), method, "scope");
			if (path == null)
				throw new NullParameterException(typeof(WmiUtil), method, "path");

			ManagementClass managementClass = new ManagementClass(scope, new ManagementPath(path),
				new ObjectGetOptions());

			return (GetObject(managementClass) ? managementClass : null);
		}

		public static bool DoesClassExist(string namespacePath, string path)
		{
			const string method = "DoesClassExist";

			if (namespacePath == null)
				throw new NullParameterException(typeof(WmiUtil), method, "namespacePath");
			if (path == null)
				throw new NullParameterException(typeof(WmiUtil), method, "path");

			ManagementScope scope;
			try
			{
				scope = new ManagementScope(namespacePath);
				scope.Connect();
			}
			catch (System.Exception)
			{
				return false;
			}

			ManagementClass managementClass = new ManagementClass(scope, new ManagementPath(path), new ObjectGetOptions());
			return GetObject(managementClass);
		}

		public static void DeleteClass(string namespacePath, string path)
		{
			const string method = "DeleteClass";

			if (namespacePath == null)
				throw new NullParameterException(typeof(WmiUtil), method, "namespacePath");
			if (path == null)
				throw new NullParameterException(typeof(WmiUtil), method, "path");

			ManagementScope scope;
			try
			{
				scope = new ManagementScope(namespacePath);
				scope.Connect();
			}
			catch (System.Exception)
			{
				// On an error assume that there is no class.

				return;
			}

			ManagementClass managementClass = new ManagementClass(scope, new ManagementPath(path), new ObjectGetOptions());
			if (GetObject(managementClass))
				managementClass.Delete();
		}

		public static object GetPropertyValue(ManagementBaseObject wmiClass, string propertyName)
		{
			const string method = "GetPropertyValue";

			if (wmiClass == null)
				throw new NullParameterException(typeof(WmiUtil), method, "wmiClass");
			if (propertyName == null)
				throw new NullParameterException(typeof(WmiUtil), method, "propertyName");

			try
			{
				return wmiClass.GetPropertyValue(propertyName);
			}
			catch (ManagementException ex)
			{
				string classText = null;
				try
				{
					classText = wmiClass.GetText(TextFormat.Mof);
				}
				catch (System.Exception)
				{
				}

				throw new WmiPropertyGetException(typeof(WmiUtil), method, propertyName,
					wmiClass.ClassPath.ToString(), classText, ex);
			}
		}

		public static void SetPropertyValue(ManagementBaseObject wmiClass, string propertyName, object value)
		{
			const string method = "SetPropertyValue";

			if (wmiClass == null)
				throw new NullParameterException(typeof(WmiUtil), method, "wmiClass");
			if (propertyName == null)
				throw new NullParameterException(typeof(WmiUtil), method, "propertyName");

			try
			{
				wmiClass.SetPropertyValue(propertyName, value);
			}
			catch (ManagementException ex)
			{
				string classText = null;
				try
				{
					classText = wmiClass.GetText(TextFormat.Mof);
				}
				catch (System.Exception)
				{
				}

				throw new WmiPropertySetException(typeof(WmiUtil), method, propertyName,
					wmiClass.ClassPath.ToString(), classText, value, ex);
			}
		}

		public static bool DoesNamespaceExist(string namespacePath)
		{
			const string method = "DoesNamespaceExist";

			if (namespacePath == null)
				throw new NullParameterException(typeof(WmiUtil), method, "namespacePath");

			try
			{
				ManagementScope scope = new ManagementScope(namespacePath);
				scope.Connect();
			}
			catch (System.Exception)
			{
				return false;
			}

			return true;
		}

		public static void CreateNamespace(string namespacePath)
		{
			const string method = "CreateNamespace";

			if (namespacePath == null)
				throw new NullParameterException(typeof(WmiUtil), method, "namespacePath");

			ManagementPath path = new ManagementPath(namespacePath);
			string[] namespaces = path.NamespacePath.Split(m_namespaceSeparator);

			// Iterate over the array of namespaces until we find the first one that doesn't exist.

			ManagementPath nsPath = new ManagementPath();
			nsPath.Server = path.Server;
			nsPath.NamespacePath = namespaces[0];

			bool nsExists = true;
			int index = 0;
			while (nsExists && index < namespaces.Length - 1)
			{
				index++;

				ManagementScope scope = new ManagementScope(nsPath);
				scope.Connect();

				string query = "SELECT * FROM " + m_namespaceClassName + " WHERE Name=\"" + namespaces[index] + "\"";

				ManagementObjectSearcher searcher = new ManagementObjectSearcher(
					scope, new ObjectQuery(query));
				using (ManagementObjectCollection childNs = searcher.Get())
				{
					IEnumerator enumerator = childNs.GetEnumerator();
					nsExists = enumerator.MoveNext();
				}

				if (nsExists)
				{
					nsPath.NamespacePath += m_namespaceSeparator + namespaces[index];
				}
			}

			if (nsExists)
				return; // The namespace already exists, no need to create anything.

			// Iterate over the, starting from the first non-existing namespace and create the namespaces.

			ManagementPath classPath = new ManagementPath();
			classPath.Server = path.Server;
			classPath.NamespacePath = string.Join(m_namespaceSeparator.ToString(), namespaces, 0, index);
			classPath.ClassName = m_namespaceClassName;

			while (index < namespaces.Length)
			{
				ManagementClass nsClass = new ManagementClass(classPath);
				ManagementObject nsInstance = nsClass.CreateInstance();

				SetPropertyValue(nsInstance, m_namespacePropertyName, namespaces[index]);
				nsInstance.Put();

				classPath.NamespacePath += m_namespaceSeparator + namespaces[index];
				index++;
			}
		}

		public static void DeleteNamespace(string namespacePath)
		{
			const string method = "DeleteNamespace";

			if (namespacePath == null)
				throw new NullParameterException(typeof(WmiUtil), method, "namespacePath");

			// Connect to the parent namespace of the one we want to delete.

			ManagementPath path = new ManagementPath(namespacePath);
			int lastSeparator = path.NamespacePath.LastIndexOf(m_namespaceSeparator);
			if (lastSeparator == -1)
			{
				throw new InvalidParameterFormatException(typeof(WmiUtil), method, "namespacePath", namespacePath,
					"WMI namespace path");
			}

			ManagementPath nsPath = new ManagementPath();
			nsPath.Server = path.Server;
			nsPath.NamespacePath = path.NamespacePath.Substring(0, lastSeparator);

			ManagementScope scope = new ManagementScope(nsPath);
			scope.Connect();

			// Find the namespace and delete it.

			string nsToDelete = path.NamespacePath.Substring(lastSeparator + 1);
			string query = "SELECT * FROM " + m_namespaceClassName + " WHERE Name=\"" + nsToDelete + "\"";
			ManagementObjectSearcher searcher = new ManagementObjectSearcher(
				scope, new ObjectQuery(query));

			using (ManagementObjectCollection childNs = searcher.Get())
			{
				IEnumerator enumerator = childNs.GetEnumerator();
				if (enumerator.MoveNext())
				{
					((ManagementObject)enumerator.Current).Delete();
				}
			}
		}

		public static bool IsNamespaceEmpty(string namespacePath)
		{
			const string method = "IsNamespaceEmpty";

			if (namespacePath == null)
				throw new NullParameterException(typeof(WmiUtil), method, "namespacePath");

			// Search for all classes in the namespace.

			ManagementObjectSearcher searcher = new ManagementObjectSearcher(new ManagementScope(namespacePath),
				new WqlObjectQuery("select * from meta_class"), null);

			using (ManagementObjectCollection wmiClasses = searcher.Get())
			{
				foreach (ManagementClass wmiClass in wmiClasses)
				{
					// If there is one non-system class then it is not empty.

					string name = (string)GetPropertyValue(wmiClass, "__CLASS");
					if (!name.StartsWith("__"))
						return false;
				}
			}

			return true;
		}

		public static void CompileMofFile(string fileName, string serverAndNamespace)
		{
			CompileMofFile(fileName, serverAndNamespace, null, null, null);
		}

		public static void CompileMofFile(string fileName, string serverAndNamespace, string user,
			string authority, string password)
		{
			const string method = "CompileMofFile";

			if (fileName == null)
				throw new NullParameterException(typeof(WmiUtil), method, "fileName");
			if (serverAndNamespace == null)
				throw new NullParameterException(typeof(WmiUtil), method, "serverAndNamespace");

			// The MofCompiler returns a completely useless error if it cannot open the file, so
			// instead of calling CompileFile() read the MOF file into a buffer and call CompileBuffer().

			int length;
			byte[] buffer;
			using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				length = (int)stream.Length;
				buffer = new byte[length];
				stream.Read(buffer, 0, length);
			}

			// Create a MofCompiler COM object.

			Win32.IMofCompiler mofCompiler = (Win32.IMofCompiler)ComInterop.CreateComInstance(
				new System.Guid(Constants.Win32.IIDs.IID_MofCompiler), typeof(Win32.IMofCompiler));
			Debug.Assert(mofCompiler != null, "mofCompiler != null");

			// Compile the MOF file.

			Win32.WBEM_COMPILE_STATUS_INFO status = new Win32.WBEM_COMPILE_STATUS_INFO();
			try
			{
				mofCompiler.CompileBuffer(length, buffer, serverAndNamespace, user, authority, password,
					Win32.WBEM_COMPILER_OPTIONS.None, Win32.WBEM_COMPILER_OPTIONS.None,
					Win32.WBEM_COMPILER_OPTIONS.None, ref status);
			}
			finally
			{
				Marshal.ReleaseComObject(mofCompiler);
			}

			// Check the status code and throw an exception if the compilation failed.

			if (status.lPhaseError != 0)
			{
				string facility;
				string message = GetWmiErrorMessage(status.hRes, out facility);

				throw new WmiMofFileCompileException(typeof(WmiUtil), method, fileName,
					serverAndNamespace, status.lPhaseError, status.ObjectNum, status.FirstLine,
					status.LastLine, status.hRes, facility, message);
			}
		}

		public static void StartEventWatcher(ManagementScope scope, EventQuery query, EventArrivedEventHandler handler)
		{
			ManagementEventWatcher watcher = new ManagementEventWatcher(scope, query);
			watcher.EventArrived += handler;

			// Keep track of all the event watchers, so we can stop them before the AppDomain is unloaded.

			RegisterEventWatcher(watcher);

			try
			{
				watcher.Start();
			}
			catch (ManagementException ex)
			{
				if (ex.ErrorCode == ManagementStatus.InvalidParameter)
				{
					throw new System.ApplicationException(string.Format("Failed to start an event query watcher."
						+ " Use DCOMCNFG to check that the current user, '{0}\\{1}', has permission to launch the"
						+ " 'Microsoft WBEM Unsecured Apartment' DCOM application (GUID {{49BD2028-1523-11D1-AD79-00C04FD8FDFF}}).",
						System.Environment.UserDomainName, System.Environment.UserName));
				}
				else
					throw;
			}
		}

		private static bool GetObject(ManagementObject managementObject)
		{
			try
			{
				managementObject.Get();
			}
			catch ( ManagementException ex )
			{
				if ( ex.ErrorCode == ManagementStatus.NotFound )
					return false;
				else
					throw;
			}

			return true;
		}

		private static string GetWmiErrorMessage(int hResult, out string facility)
		{
			Win32.IWbemStatusCodeText statusCodeText = (Win32.IWbemStatusCodeText)ComInterop.CreateComInstance(
				new System.Guid(Constants.Win32.IIDs.IID_WbemClassObject), typeof(Win32.IWbemStatusCodeText));
			Debug.Assert(statusCodeText != null, "statusCodeText != null");

			try
			{
				facility = statusCodeText.GetFacilityCodeText(hResult, 0, 0);
				return statusCodeText.GetErrorCodeText(hResult, 0, 0).TrimEnd(new char[] { '\r', '\n' });
			}
			finally
			{
				Marshal.ReleaseComObject(statusCodeText);
			}
		}

		private static void RegisterEventWatcher(ManagementEventWatcher watcher)
		{
			lock (m_eventWatcherLock)
			{
				if (m_eventWatchers == null)
				{
					// This is the first time - register an event handler for DomainUnload.

					System.AppDomain.CurrentDomain.DomainUnload += new System.EventHandler(CurrentDomain_DomainUnload);
					m_eventWatchers = new ArrayList();
				}

				m_eventWatchers.Add(watcher);
			}
		}

		private static void CurrentDomain_DomainUnload(object sender, System.EventArgs e)
		{
		    try
		    {
			    lock (m_eventWatcherLock)
			    {
				    Debug.Assert(m_eventWatchers != null, "m_eventWatchers != null");

				    // The AppDomain is being unloaded. Stop all event watchers now, otherwise .NET will attempt to do it
				    // in the finalizers and the application may crash - see defect 57823.

				    foreach (ManagementEventWatcher watcher in m_eventWatchers)
				    {
                        try
                        {
                            watcher.Stop();
                            GC.SuppressFinalize(watcher);
                        }
                        catch (Exception)
                        {
                        }
				    }

				    m_eventWatchers = null;
			    }
            }
            catch (Exception)
            {
                // Swallow everything at this point.
            }
        }
	}
}
