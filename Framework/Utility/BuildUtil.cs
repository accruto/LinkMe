using System.IO;
using Microsoft.Win32;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Provides static methods used in building .NET assemblies.
	/// </summary>
	public sealed class BuildUtil
	{
		private static string m_devenvPath = null;
		private static string m_netSdkPath = null;

		private BuildUtil()
		{
		}

		/// <summary>
		/// Returns the full path of the VS.NET 2003 IDE, devenv.exe.
		/// </summary>
		/// <value>The full path of the VS.NET 2003 IDE, devenv.exe.</value>
		public static string DevEnvPath
		{
			get
			{
				if (m_devenvPath == null)
				{
					m_devenvPath = GetDevEnvPath();
				}

				return m_devenvPath;
			}
		}

		/// <summary>
		/// Returns the directory where .NET Framework SDK 1.1 is installed.
		/// </summary>
		/// <value>The directory where .NET Framework SDK 1.1 is installed.</value>
		public static string SdkInstallRoot
		{
			get
			{
				if (m_netSdkPath == null)
				{
					m_netSdkPath = GetNetSdkPath();
				}

				return m_netSdkPath;
			}
		}

		/// <summary>
		/// Returns the full path of the Type Library to Assembly Converter, TlbImp.exe.
		/// </summary>
		/// <value>The full path of the Type Library to Assembly Converter, TlbImp.exe.</value>
		public static string TlbImpPath
		{
			get { return Path.Combine(SdkInstallRoot, "Bin" + Path.DirectorySeparatorChar + "TlbImp.exe"); }
		}

		private static string GetDevEnvPath()
		{
			const string keyPath = @"SOFTWARE\Microsoft\VisualStudio\7.1";
			const string valueName = "InstallDir";
			const string exeName = "devenv.exe";

			// Read the path to devenv.exe from the registry: HKLM\SOFTWARE\Microsoft\VisualStudio\7.1\@InstallDir

			RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, false);
			if (key == null)
			{
				throw new System.ApplicationException("Failed to open registry key 'HKLM\\" + keyPath
					+ "'. VS.NET 2003 may not be installed on this machine.");
			}

			object installDirValue = key.GetValue(valueName);
			if (installDirValue == null)
			{
				throw new System.ApplicationException("Failed to read the value '" + valueName
					+ "' from registry key 'HKLM\\" + keyPath + "'.");
			}
			else if (!(installDirValue is string))
			{
				throw new System.ApplicationException("The registry value 'HKLM\\" + m_devenvPath
					+ "\\@" + valueName + "', was read successfully, but it is not a string.");
			}

			return Path.Combine((string)installDirValue, exeName);
		}

		private static string GetNetSdkPath()
		{
			const string keyPath = @"SOFTWARE\Microsoft\.NETFramework";
			const string valueName = "sdkInstallRootv1.1";

			// Read the path to devenv.exe from the registry: HKLM\SOFTWARE\Microsoft\.NETFramework\@sdkInstallRootv1.1

			RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, false);
			if (key == null)
			{
				throw new System.ApplicationException("Failed to open registry key 'HKLM\\" + keyPath
					+ "'. The .NET 1.1 Framework SDK may not be installed on this machine.");
			}

			object sdkRootValue = key.GetValue(valueName);
			if (sdkRootValue == null)
			{
				throw new System.ApplicationException("Failed to read the value '" + valueName
					+ "' from registry key 'HKLM\\" + keyPath + "'.");
			}
			else if (!(sdkRootValue is string))
			{
				throw new System.ApplicationException("The registry value 'HKLM\\" + keyPath
					+ "\\@" + valueName + "', was read successfully, but it is not a string.");
			}

			return (string)sdkRootValue;
		}
	}
}
