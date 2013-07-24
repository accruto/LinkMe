using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using Win32 = LinkMe.Framework.Utility.Win32;
using TYPEDESC = System.Runtime.InteropServices.ComTypes.TYPEDESC;
using IDLFLAG = System.Runtime.InteropServices.ComTypes.IDLFLAG;
using FUNCDESC = System.Runtime.InteropServices.ComTypes.FUNCDESC;
using ELEMDESC = System.Runtime.InteropServices.ComTypes.ELEMDESC;
using INVOKEKIND = System.Runtime.InteropServices.ComTypes.INVOKEKIND;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	internal abstract class MemberDesc
	{
		protected MemberDesc()
		{
		}

		protected void Initialise(MemberTypes memberType, string name)
		{
			m_memberType = memberType;
			m_name = name;
		}

		public MemberTypes MemberType
		{
			get { return m_memberType; }
		}

		public string Name
		{
			get { return m_name; }
		}

		protected string GetComType(VarEnum varEnum)
		{
			if ((varEnum & VarEnum.VT_BYREF) == VarEnum.VT_BYREF)
				return GetComType(varEnum & ~VarEnum.VT_BYREF) + "&";
			else if ((varEnum & VarEnum.VT_ARRAY) == VarEnum.VT_ARRAY)
			{
				return ComBrowserSettings.GetKeyword(VarEnum.VT_ARRAY) + "("
					+ GetComType(varEnum & ~VarEnum.VT_ARRAY) + ")";
			}
			else if ((varEnum & VarEnum.VT_VECTOR) == VarEnum.VT_VECTOR)
				return GetComType(varEnum & ~VarEnum.VT_VECTOR) + "[]";

			switch (varEnum)
			{
				case VarEnum.VT_BOOL:
				case VarEnum.VT_UI1:
				case VarEnum.VT_UI2:
				case VarEnum.VT_UINT:
				case VarEnum.VT_UI4:
				case VarEnum.VT_UI8:
				case VarEnum.VT_I1:
				case VarEnum.VT_I2:
				case VarEnum.VT_I4:
				case VarEnum.VT_INT:
				case VarEnum.VT_I8:
				case VarEnum.VT_R4:
				case VarEnum.VT_R8:
				case VarEnum.VT_BSTR:
				case VarEnum.VT_VOID:
				case VarEnum.VT_HRESULT:
				case VarEnum.VT_UNKNOWN:
				case VarEnum.VT_DISPATCH:
				case VarEnum.VT_CLSID:
				case VarEnum.VT_CY:
				case VarEnum.VT_DATE:
				case VarEnum.VT_DECIMAL:
				case VarEnum.VT_FILETIME:
				case VarEnum.VT_VARIANT:
				case VarEnum.VT_ERROR:
				case VarEnum.VT_LPSTR:
				case VarEnum.VT_LPWSTR:
					return ComBrowserSettings.GetKeyword(varEnum);

				case VarEnum.VT_EMPTY:
				case VarEnum.VT_NULL:
				case VarEnum.VT_USERDEFINED:
				case VarEnum.VT_CARRAY:
				case VarEnum.VT_CF:
				case VarEnum.VT_RECORD:
				case VarEnum.VT_BLOB:
				case VarEnum.VT_BLOB_OBJECT:
				case VarEnum.VT_PTR:
				case VarEnum.VT_SAFEARRAY:
					Debug.Fail("GetComType(VarEnum) called for " + varEnum.ToString());
					return varEnum.ToString();

				default:
					Debug.Fail("Unexpected value of varEnum: " + varEnum.ToString());
					return "<VarEnum type '" + varEnum.ToString() + "'>";
			}
		}

		protected string GetComType(ITypeInfo typeInfo, TYPEDESC typeDesc)
		{
			VarEnum varEnum = (VarEnum) typeDesc.vt;
			switch ( varEnum )
			{
				case VarEnum.VT_PTR:
					TYPEDESC ptrTypedesc = (TYPEDESC)Marshal.PtrToStructure(typeDesc.lpValue, typeof(TYPEDESC));
					return GetComType(typeInfo, ptrTypedesc) + "*";

				case VarEnum.VT_SAFEARRAY:
					TYPEDESC arrayTypedesc = (TYPEDESC)Marshal.PtrToStructure(typeDesc.lpValue, typeof(TYPEDESC));
					return ComBrowserSettings.GetKeyword(VarEnum.VT_SAFEARRAY) + "("
						+ GetComType(typeInfo, arrayTypedesc) + ")";

                case VarEnum.VT_CARRAY:
                    TYPEDESC carrayTypedesc = (TYPEDESC)Marshal.PtrToStructure(typeDesc.lpValue, typeof(TYPEDESC));
                    return GetComType(typeInfo, carrayTypedesc) + "[]";

				case VarEnum.VT_USERDEFINED:
                    // The .NET definition of GetRefTypeInfo() incorrectly takes an int instead of IntPtr, which fails
                    // on 64-bit machines. At least don't throw an unhandled exception here.
                    if ((long)typeDesc.lpValue > int.MaxValue)
                        return "<GetRefTypeInfo bug>";

					int hreftype = (int)typeDesc.lpValue;
					ITypeInfo refTypeInfo;

					try
					{
						typeInfo.GetRefTypeInfo(hreftype, out refTypeInfo);
					}
					catch (COMException)
					{
						return "<GetRefTypeInfo failed>";
					}

					try
					{
						return Marshal.GetTypeInfoName(refTypeInfo);
					}
					finally
					{
						Marshal.ReleaseComObject(refTypeInfo);
					}

				default:
					return GetComType(varEnum);
			}
		}

		private MemberTypes m_memberType;
		private string m_name;
	}

	internal class FieldDesc
		:	MemberDesc
	{
		public FieldDesc(ITypeInfo typeInfo, Win32.VARDESC varDesc)
		{
			string name;
			string docString;
			int helpContext;
			string helpFile;
			typeInfo.GetDocumentation(varDesc.memid, out name, out docString, out helpContext, out helpFile);

			Initialise(MemberTypes.Field, name);

			m_type = GetComType(typeInfo, varDesc.elemdescVar.tdesc);

			if (varDesc.varkind == Win32.VARKIND.VAR_CONST)
			{
				object fieldValue = Marshal.GetObjectForNativeVariant(varDesc.descUnion.lpvarValue);
				Debug.Assert(fieldValue != null, "fieldValue != null");
				m_value = fieldValue.ToString();
			}
			else
			{
				m_value = null;
			}
		}

		public string Type
		{
			get { return m_type; }
		}

		public string Value
		{
			get { return m_value; }
		}

		private string m_type;
		private string m_value;
	}

	internal class ParameterDesc
	{
		public ParameterDesc(string name, string type, IDLFLAG flags)
		{
			m_name = name;
			m_type = type;
			m_flags = flags;
		}

		public string Name
		{
			get { return m_name; }
		}

		public string Type
		{
			get { return m_type; }
		}

		public bool IsIn
		{
			get { return (m_flags & IDLFLAG.IDLFLAG_FIN) != IDLFLAG.IDLFLAG_NONE; }
		}

		public bool IsOut
		{
			get { return (m_flags & IDLFLAG.IDLFLAG_FOUT) != IDLFLAG.IDLFLAG_NONE; }
		}

		public bool IsRetval
		{
			get { return (m_flags & IDLFLAG.IDLFLAG_FRETVAL) != IDLFLAG.IDLFLAG_NONE; }
		}

		private string m_name;
		private string m_type;
		private IDLFLAG m_flags;
	}

	internal class MethodDesc
		: MemberDesc
	{
		public MethodDesc(ITypeInfo typeInfo, FUNCDESC funcDesc)
		{
			// Initialise the standard member information.

			string name;
			string docString;
			int helpContext;
			string helpFile;
			typeInfo.GetDocumentation(funcDesc.memid, out name, out docString, out helpContext, out helpFile);
			Initialise(MemberTypes.Method, name);

			// Get the names of the parameters (index 0 corresponds to the method name itself).

			string[] names = new string[funcDesc.cParams + 1];
			int nameCount;
			typeInfo.GetNames(funcDesc.memid, names, funcDesc.cParams + 1, out nameCount);

			// Need to account for the return value if there is one.

			bool includeReturnParam = (VarEnum) funcDesc.elemdescFunc.tdesc.vt != VarEnum.VT_VOID
				&& (VarEnum) funcDesc.elemdescFunc.tdesc.vt != VarEnum.VT_HRESULT;

			int paramCount = funcDesc.cParams + (includeReturnParam ? 1 : 0);
			m_parameters = new ParameterDesc[paramCount];

			// Iterate over the specified parameters.

			for ( int index = 0; index < funcDesc.cParams; ++index )
			{
				// Extract the ELEMDESC.

				IntPtr ptr = (IntPtr)(funcDesc.lprgelemdescParam.ToInt64()
					+ (long)(Marshal.SizeOf(typeof(ELEMDESC)) * index));
				ELEMDESC elemdesc = (ELEMDESC) Marshal.PtrToStructure(ptr, typeof(ELEMDESC));

				m_parameters[index] = new ParameterDesc(names[index + 1], GetComType(typeInfo, elemdesc.tdesc), elemdesc.desc.idldesc.wIDLFlags);
			}

			// Now add the return value if needed.

			if ( includeReturnParam )
				m_parameters[paramCount - 1] = new ParameterDesc("ret", GetComType(typeInfo, funcDesc.elemdescFunc.tdesc) + "*", funcDesc.elemdescFunc.desc.idldesc.wIDLFlags | IDLFLAG.IDLFLAG_FOUT | IDLFLAG.IDLFLAG_FRETVAL);
		}

		private MethodDesc()
		{
		}

		public string ReturnType
		{
			// There doesn't seem to be a way to get the return value, so let the user assume that all methods
			// return an HRESULT for now.
			get { return null; }
		}

		public ParameterDesc[] Parameters
		{
			get { return m_parameters; }
		}

		internal MemberDesc CreateEventFromMethod()
		{
			Debug.Assert(MemberType == MemberTypes.Method, "MemberType == MemberTypes.Method");

			MethodDesc newEvent = new MethodDesc();
			newEvent.Initialise(MemberTypes.Event, Name);
			// Should be OK to keep a reference to the original parameters array here - it's not going to change.
			newEvent.m_parameters = m_parameters;

			return newEvent;
		}

		private ParameterDesc[] m_parameters;
	}

	internal class PropertyDesc
		:	MemberDesc
	{
		public PropertyDesc(ITypeInfo typeInfo, FUNCDESC funcDesc)
		{
			// Initialise the standard member information.

			string name;
			string docString;
			int helpContext;
			string helpFile;
			typeInfo.GetDocumentation(funcDesc.memid, out name, out docString, out helpContext, out helpFile);
			Initialise(MemberTypes.Property, name);

			switch ( funcDesc.invkind )
			{
				case INVOKEKIND.INVOKE_PROPERTYGET:
					m_canGet = true;
					break;

				case INVOKEKIND.INVOKE_PROPERTYPUT:
					m_canPut = true;
					break;

				case INVOKEKIND.INVOKE_PROPERTYPUTREF:
					m_canPutRef = true;
					break;
			}

			// Get the names of the parameters (index 0 corresponds to the method name itself).

			string[] names = new string[funcDesc.cParams + 1];
			int nameCount;
			typeInfo.GetNames(funcDesc.memid, names, funcDesc.cParams + 1, out nameCount);

			// Need to account for the return value if there is one.

			bool includeReturnParam = (VarEnum) funcDesc.elemdescFunc.tdesc.vt != VarEnum.VT_VOID
				&& (VarEnum) funcDesc.elemdescFunc.tdesc.vt != VarEnum.VT_HRESULT;

			int paramCount = funcDesc.cParams + (includeReturnParam ? 1 : 0);
			m_parameters = new ParameterDesc[paramCount];

			// Iterate over the specified parameters.

			for ( int index = 0; index < funcDesc.cParams; ++index )
			{
				// Extract the ELEMDESC.

				IntPtr ptr = (IntPtr)(funcDesc.lprgelemdescParam.ToInt64()
					+ (long)(Marshal.SizeOf(typeof(ELEMDESC)) * index));
				ELEMDESC elemdesc = (ELEMDESC) Marshal.PtrToStructure(ptr, typeof(ELEMDESC));

				m_parameters[index] = new ParameterDesc(names[index + 1] == null ? "val" : names[index + 1], GetComType(typeInfo, elemdesc.tdesc), elemdesc.desc.idldesc.wIDLFlags);
			}

			// Now add the return value if needed.

			if ( includeReturnParam )
				m_parameters[paramCount - 1] = new ParameterDesc("ret", GetComType(typeInfo, funcDesc.elemdescFunc.tdesc) + "*", funcDesc.elemdescFunc.desc.idldesc.wIDLFlags | IDLFLAG.IDLFLAG_FOUT | IDLFLAG.IDLFLAG_FRETVAL);
		}

		public bool CanGet
		{
			get { return m_canGet; }
		}

		public bool CanPut
		{
			get { return m_canPut; }
		}

		public bool CanPutRef
		{
			get { return m_canPutRef; }
		}

		public string ReturnType
		{
			// There doesn't seem to be a way to get the return value, so let the user assume that all properties
			// return an HRESULT for now.
			get { return null; }
		}

		public ParameterDesc[] Parameters
		{
			get { return m_parameters; }
		}

		public override string ToString()
		{
			if ( m_canGet )
				return "get_" + Name;
			else if ( m_canPut )
				return "put_" + Name;
			else
				return "putref_" + Name;
		}

		private ParameterDesc[] m_parameters;
		private bool m_canGet;
		private bool m_canPut;
		private bool m_canPutRef;
	}
}
