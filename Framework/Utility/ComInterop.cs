using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Win32;
using TYPEATTR = System.Runtime.InteropServices.ComTypes.TYPEATTR;
using TYPEKIND = System.Runtime.InteropServices.ComTypes.TYPEKIND;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Provides static utility methods for interoperating with COM.
	/// </summary>
	public sealed class ComInterop
	{
		private ComInterop()
		{
		}

		/// <summary>
		/// Registers the specified .NET control type for COM as an ActiveX control using a default
		/// value for MiscStatus.
		/// </summary>
		/// <param name="type">The .NET control type to register.</param>
		/// <remarks>Call this method from the [ComRegisterFunction] method of the control that will be
		/// used by COM. The type should also be marked with [Guid] and
		/// [ClassInterface(ClassInterfaceType.AutoDual)] attributes.
		/// </remarks>
		public static void RegisterActiveXControl(System.Type type)
		{
			RegisterActiveXControl(type, Win32.OLEMISC.SETCLIENTSITEFIRST
				| Win32.OLEMISC.ACTIVATEWHENVISIBLE | Win32.OLEMISC.INSIDEOUT
				| Win32.OLEMISC.RECOMPOSEONRESIZE);
		}

		/// <summary>
		/// Registers the specified .NET control type for COM as an ActiveX control using the specified
		/// value for MiscStatus.
		/// </summary>
		/// <param name="type">The .NET control type to register.</param>
		/// <param name="olemisc">The value to write to the MiscStatus registry key for the control.</param>
		/// <remarks>Call this method from the [ComRegisterFunction] method of the control that will be
		/// used by COM. The type should also be marked with [Guid] and
		/// [ClassInterface(ClassInterfaceType.AutoDual)] attributes.
		/// </remarks>
		public static void RegisterActiveXControl(System.Type type, Win32.OLEMISC olemisc)
		{
			if (type == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "RegisterActiveXControl", "type");

			string keyName = "CLSID\\" + type.GUID.ToString("B");

			using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(keyName, true))
			{
				key.CreateSubKey("Control").Close();

				using (RegistryKey subkey = key.CreateSubKey("MiscStatus"))
				{
					string status = ((int)olemisc).ToString();
					subkey.SetValue(null, status);
				}

				using (RegistryKey subkey = key.CreateSubKey("TypeLib"))
				{
					System.Guid libid = Marshal.GetTypeLibGuidForAssembly(type.Assembly);
					subkey.SetValue(null, libid.ToString("B"));
				}

				using (RegistryKey subkey = key.CreateSubKey("Version"))
				{
					System.Version ver = type.Assembly.GetName().Version;

					string version = (ver.Major == 0 && ver.Minor == 0 ? "1.0" :
						string.Format("{0}.{1}", ver.Major, ver.Minor));

					subkey.SetValue(null, version);
				}
			}
		}

		/// <summary>
		/// Unregisters the specified .NET type for COM by deleting the entire CLSID subkey for its GUID.
		/// </summary>
		/// <param name="type">The .NET type to unregister.</param>
		/// <remarks>Call this method from the [ComUnregisterFunction] method of a type that calls
		/// <see cref="RegisterActiveXControl" /> in its [ComRegisterFunction] method.
		/// </remarks>
		public static void UnregisterType(System.Type type)
		{
			if (type == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "UnregisterType", "type");

			// Delete entire CLSID\{clsid} subtree.

			string keyName = "CLSID\\" + type.GUID.ToString("B");
            bool exists = false;
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(keyName))
            {
                if (key != null)
                    exists = true;
            }

            if (exists)
                Registry.ClassesRoot.DeleteSubKeyTree(keyName);
		}

		public static System.Guid GetTypeIID(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetTypeIID", "typeInfo");

			System.IntPtr ptr;
			typeInfo.GetTypeAttr(out ptr);

			try
			{
				TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));
				return typeAttr.guid;
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(ptr);
			}
		}

		public static string GetTypeLibPath(System.Guid libraryGuid, System.Version version, CultureInfo culture)
		{
			ushort majorVer = (version == null ? ushort.MaxValue : (ushort)version.Major);
			ushort minorVer = (version == null ? ushort.MaxValue : (ushort)version.Minor);
			uint lcid = (culture == null ? 0 : (uint)culture.LCID);

			string path = string.Empty;
			try
			{
				Win32.SafeNativeMethods.QueryPathOfRegTypeLib(ref libraryGuid, majorVer, minorVer, lcid, ref path);
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException(string.Format("Failed to get the path of type library '{0}',"
					+ " version {1}.{2}, LCID {3}.", libraryGuid.ToString("B"), majorVer.ToString(),
					minorVer.ToString(), lcid.ToString()), ex);
			}

			return path;
		}

		public static string GetTypeLibPath(ITypeLib typeLibrary)
		{
			if (typeLibrary == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetTypeLibPath", "typeLibrary");

			System.IntPtr ptr = System.IntPtr.Zero; 
			typeLibrary.GetLibAttr(out ptr);

			try
			{
				Win32.TLIBATTR libAttr = (Win32.TLIBATTR)Marshal.PtrToStructure(ptr, typeof(Win32.TLIBATTR));
				Debug.Assert(libAttr.guid != System.Guid.Empty, "libAttr.guid != Guid.Empty");

				string path = string.Empty;
				Win32.SafeNativeMethods.QueryPathOfRegTypeLib(ref libAttr.guid, libAttr.wMajorVerNum,
					libAttr.wMinorVerNum, libAttr.lcid, ref path);
				return path;
			}
			finally
			{
				typeLibrary.ReleaseTLibAttr(ptr);
			}
		}

		public static string GetContainingTypeLibName(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetContainingTypeLibName", "typeInfo");

			ITypeLib typeLib;
			int typeIndex;
			typeInfo.GetContainingTypeLib(out typeLib, out typeIndex);

			try
			{
				return Marshal.GetTypeLibName(typeLib);
			}
			finally
			{
				Marshal.ReleaseComObject(typeLib);
			}
		}

		public static string GetMemberName(ITypeInfo typeInfo, int memberID)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetMemberName", "typeInfo");

			string name;
			string docString;
			int helpContext;
			string helpFile;

			typeInfo.GetDocumentation(memberID, out name, out docString, out helpContext, out helpFile);

			return name;
		}

		public static string GetContainingTypeLibPath(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetContainingTypeLibPath", "typeInfo");

			ITypeLib typeLib;
			int typeIndex;
			typeInfo.GetContainingTypeLib(out typeLib, out typeIndex);

			try
			{
				return GetTypeLibPath(typeLib);
			}
			finally
			{
				Marshal.ReleaseComObject(typeLib);
			}
		}

		public static System.Type GetNetTypeForComType(ITypeInfo typeInfo)
		{
			if (typeInfo== null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetNetTypeForComType", "typeInfo");

			System.IntPtr ptr = Marshal.GetIUnknownForObject(typeInfo);

			try
			{
				return Marshal.GetTypeForITypeInfo(ptr);
			}
			finally
			{
				if (ptr != System.IntPtr.Zero)
				{
					Marshal.Release(ptr); // If this pointer is not released the type library cannot be unloaded!
				}
			}
		}

		public static System.Type GetNetTypeForVarType(VarEnum varType)
		{
			switch (varType)
			{
				case VarEnum.VT_BOOL:
					return typeof(bool);

				case VarEnum.VT_UI1:
					return typeof(byte);

				case VarEnum.VT_UI2:
					return typeof(ushort);

				case VarEnum.VT_UI4:
				case VarEnum.VT_UINT:
					return typeof(uint);

				case VarEnum.VT_UI8:
					return typeof(ulong);

				case VarEnum.VT_I1:
					return typeof(sbyte);

				case VarEnum.VT_I2:
					return typeof(short);

				case VarEnum.VT_I4:
				case VarEnum.VT_INT:
					return typeof(int);

				case VarEnum.VT_I8:
					return typeof(long);

				case VarEnum.VT_R4:
					return typeof(float);

				case VarEnum.VT_R8:
				case VarEnum.VT_DATE:
					return typeof(double);

				case VarEnum.VT_BSTR:
				case VarEnum.VT_LPSTR:
				case VarEnum.VT_LPWSTR:
					return typeof(string);

				case VarEnum.VT_CLSID:
					return typeof(System.Guid);

				case VarEnum.VT_CY:
				case VarEnum.VT_DECIMAL:
					return typeof(decimal);

				case VarEnum.VT_UNKNOWN:
				case VarEnum.VT_DISPATCH:
				case VarEnum.VT_VARIANT:
					return typeof(object);

				default:
					return null;
			}
		}

		public static TYPEKIND GetTypeKind(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetTypeKind", "typeInfo");

			System.IntPtr ptr = System.IntPtr.Zero;
			typeInfo.GetTypeAttr(out ptr);

			try
			{
				TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));
				return typeAttr.typekind;
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(ptr);
			}
		}

		public static ITypeLib LoadTypeLibrary(string filePath)
		{
			if (filePath == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "LoadTypeLibrary", "filePath");

			try
			{
				return Win32.SafeNativeMethods.LoadTypeLibEx(filePath, Win32.REGKIND.REGKIND_NONE);
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException("Failed to load type library from file '" + filePath + "'.", ex);
			}
		}

		public static int GetImplTypeCount(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetImplTypeCount", "typeInfo");

			System.IntPtr ptr = System.IntPtr.Zero; 
			typeInfo.GetTypeAttr(out ptr);

			try
			{
				TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));
				return typeAttr.cImplTypes;
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(ptr);
			}
		}

		public static int GetFuncCount(ITypeInfo typeInfo)
		{
			if (typeInfo == null)
				throw new Exceptions.NullParameterException(typeof(ComInterop), "GetFuncCount", "typeInfo");

			System.IntPtr ptr = System.IntPtr.Zero; 
			typeInfo.GetTypeAttr(out ptr);

			try
			{
				TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));
				return typeAttr.cFuncs;
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(ptr);
			}
		}

		public static object CreateComInstance(System.Guid coClassIid, System.Type interfaceType)
		{
			return CreateComInstance(coClassIid, interfaceType, Win32.CLSCTX.CLSCTX_INPROC_SERVER);
		}

		public static object CreateComInstance(System.Guid coClassIid, System.Type interfaceType,
			Win32.CLSCTX context)
		{
			const string method = "CreateComInstance";

			System.Guid interfaceIid;
			if (interfaceType == null)
			{
				interfaceIid = new System.Guid(Constants.Win32.IIDs.IID_IUnknown);
			}
			else
			{
				if (!interfaceType.IsInterface)
					throw new Exceptions.TypeNotAnInterfaceException(typeof(ComInterop), method, interfaceType);

				interfaceIid = interfaceType.GUID;
			}

			object instance;
			try
			{
				instance = Win32.UnsafeNativeMethods.CoCreateInstance(ref coClassIid, System.IntPtr.Zero,
					context, ref interfaceIid);
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException(string.Format("Failed to create an instance of"
					+ " COM coclass {0} using interface '{1}' ({2}) and context {3}.",
					coClassIid.ToString("B"), (interfaceType == null ? "IUnknown" : interfaceType.Name),
					interfaceIid.ToString("B"), context.ToString()), ex);
			}
			Debug.Assert(instance != null, "instance != null");

			return instance;
		}

		public static void InitializeSecurity(Win32.RPC_C_AUTHN_LEVEL authenticationLevel,
			Win32.RPC_C_IMP_LEVEL impersonationLevel)
		{
			int hr = Win32.UnsafeNativeMethods.CoInitializeSecurity(System.IntPtr.Zero, -1,
				System.IntPtr.Zero, System.IntPtr.Zero, authenticationLevel, impersonationLevel,
				System.IntPtr.Zero, 0, System.IntPtr.Zero);

			if (hr != 0)
			{
				try
				{
					throw new Win32Exception(hr);
				}
				catch (Win32Exception ex)
				{
					throw new System.ApplicationException(string.Format("Failed to initialise COM security"
						+ " with authentication level '{0}' and impersonation level '{1}'.",
						authenticationLevel, impersonationLevel), ex);
				}
			}
		}
	}
}
