using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using DISPPARAMS = System.Runtime.InteropServices.ComTypes.DISPPARAMS;
using EXCEPINFO = System.Runtime.InteropServices.ComTypes.EXCEPINFO;

namespace LinkMe.Framework.Utility.Win32
{
	[ComImport, Guid(Constants.Win32.IIDs.IID_IDispatch), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IDispatch
	{
		int GetTypeInfoCount();
		[return: MarshalAs(UnmanagedType.Interface)]
		ITypeInfo GetTypeInfo(uint iTInfo, uint lcid);
		[PreserveSig]
		int GetIDsOfNames([In] ref System.Guid riid, [In, MarshalAs(UnmanagedType.LPArray)] string[] rgszNames,
			uint cNames, uint lcid, [Out, MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);
		[PreserveSig]
		int Invoke(int dispIdMember, [In] ref System.Guid riid, uint lcid, ushort wFlags, ref DISPPARAMS pDispParams,
			out object pVarResult, out EXCEPINFO pExcepInfo, out uint pArgErr);
	}

	[ComImport, Guid(Constants.Win32.IIDs.IID_IProvideClassInfo), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IProvideClassInfo
	{
		void GetClassInfo(out ITypeInfo ppTI);
	}

	[ComImport, Guid(Constants.Win32.IIDs.IID_IWbemStatusCodeText), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IWbemStatusCodeText
	{
		string GetErrorCodeText(int hRes, uint LocaleId, int lFlags);
		string GetFacilityCodeText(int hRes, uint LocaleId, int lFlags);
	}

	[ComImport, Guid(Constants.Win32.IIDs.IID_IMofCompiler), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMofCompiler
	{
		void CompileFile([MarshalAs(UnmanagedType.LPWStr)] string FileName,
			[MarshalAs(UnmanagedType.LPWStr)] string ServerAndNamespace,
			[MarshalAs(UnmanagedType.LPWStr)] string User,
			[MarshalAs(UnmanagedType.LPWStr)] string Authority,
			[MarshalAs(UnmanagedType.LPWStr)] string Password,
			WBEM_COMPILER_OPTIONS lOptionFlags, WBEM_COMPILER_OPTIONS lClassFlags,
			WBEM_COMPILER_OPTIONS lInstanceFlags, ref WBEM_COMPILE_STATUS_INFO pInfo);

		void CompileBuffer(int BuffSize,
			[MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] byte[] pBuffer,
			[MarshalAs(UnmanagedType.LPWStr)] string ServerAndNamespace,
			[MarshalAs(UnmanagedType.LPWStr)] string User,
			[MarshalAs(UnmanagedType.LPWStr)] string Authority,
			[MarshalAs(UnmanagedType.LPWStr)] string Password,
			WBEM_COMPILER_OPTIONS lOptionFlags, WBEM_COMPILER_OPTIONS lClassFlags,
			WBEM_COMPILER_OPTIONS lInstanceFlags, ref WBEM_COMPILE_STATUS_INFO pInfo);

		void CreateBMOF([MarshalAs(UnmanagedType.LPWStr)] string TextFileName,
			[MarshalAs(UnmanagedType.LPWStr)] string BMOFFileName,
			[MarshalAs(UnmanagedType.LPWStr)] string ServerAndNamespace,
			WBEM_COMPILER_OPTIONS lOptionFlags, WBEM_COMPILER_OPTIONS lClassFlags,
			WBEM_COMPILER_OPTIONS lInstanceFlags, ref WBEM_COMPILE_STATUS_INFO pInfo);
	}

	[ComImport, Guid(Constants.Win32.IIDs.IID_IClassFactory), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IClassFactory 
	{
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.Error)]
		int CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
			[In] ref System.Guid riid, [Out] out object ppvObject);

		void LockServer(bool fLock);
	}
}
