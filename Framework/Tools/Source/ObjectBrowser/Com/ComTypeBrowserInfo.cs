using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

using LinkMe.Framework.Utility;
using Win32 = LinkMe.Framework.Utility.Win32;
using LinkMe.Framework.Tools.ObjectBrowser;

using TYPEKIND = System.Runtime.InteropServices.ComTypes.TYPEKIND;
using TYPEATTR = System.Runtime.InteropServices.ComTypes.TYPEATTR;
using TYPEFLAGS = System.Runtime.InteropServices.ComTypes.TYPEFLAGS;
using IMPLTYPEFLAGS = System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS;
using FUNCDESC = System.Runtime.InteropServices.ComTypes.FUNCDESC;
using FUNCFLAGS = System.Runtime.InteropServices.ComTypes.FUNCFLAGS;
using INVOKEKIND = System.Runtime.InteropServices.ComTypes.INVOKEKIND;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	/// <summary>
	/// An ITypeBrowserInfo implementation that reads COM types (ITypeInfo objects).
	/// </summary>
	public class ComTypeBrowserInfo : ElementBrowserInfo, ITypeBrowserInfo, IDisposable
	{
		#region Nested types

		/// <summary>
		/// A class that contains an unmanaged resource and no references to other classes, so it can have a
		/// destructor without too much of a performance penalty - as recommended in Rico Mariani's blog.
		/// </summary>
		private class ComTypeWrapper : IDisposable
		{
			private ITypeInfo m_comType;

			public ComTypeWrapper(ITypeInfo comType)
			{
				Debug.Assert(comType != null, "comType != null");
				m_comType = comType;
			}

			~ComTypeWrapper()
			{
				DisposeInternal();
			}

			#region IDisposable Members

			public void Dispose()
			{
				DisposeInternal();
				GC.SuppressFinalize(this);
			}

			#endregion

			public ITypeInfo ComType
			{
				get { return m_comType; }
			}

			private void DisposeInternal()
			{
				if (m_comType != null)
				{
					Marshal.ReleaseComObject(m_comType);
					m_comType = null;
				}
			}
		}

		#endregion

		private ComTypeWrapper m_typeInfo;
		private TypeLibraryNamespaceBrowserInfo m_namespace;
		private SortedList m_baseTypes = null;
		private SortedList m_members = null;
		private DescriptionText m_description = null;

		internal ComTypeBrowserInfo()
		{
		}

		internal ComTypeBrowserInfo(ITypeInfo typeInfo, TypeLibraryNamespaceBrowserInfo ns)
		{
			Debug.Assert(typeInfo != null && ns != null, "typeInfo != null && ns != null");

			m_typeInfo = new ComTypeWrapper(typeInfo);
			m_namespace = ns;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is ComTypeBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			ComTypeBrowserInfo other = (ComTypeBrowserInfo)obj;

			if (other == this)
				return 0;

			switch (Manager.Settings.TypeOrder)
			{
				case TypeOrder.Alphabetical:
					return CompareTypeNames(m_typeInfo, other.m_typeInfo);

				case TypeOrder.ObjectType:
					// Compare object types
					int result1 = GetObjectType(m_typeInfo.ComType).CompareTo(GetObjectType(other.m_typeInfo.ComType));
					if (result1 != 0)
						return result1;

					// Same object type - compare names
					return CompareTypeNames(m_typeInfo, other.m_typeInfo);

				default:
					throw new ApplicationException("Unsupported TypeOrder enum value: '"
						+ Manager.Settings.TypeOrder.ToString() + "'.");
			}
		}

		#endregion

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return FullName; }
		}

		public override string NodeText
		{
			get { return Marshal.GetTypeInfoName(m_typeInfo.ComType); }
		}

		public override DescriptionText Description
		{
			get
			{
				if (m_description == null)
				{
					m_description = GetDescription();
					Debug.Assert(m_description != null, "m_description != null");
				}

				return m_description;
			}
		}

		public override int ImageIndex
		{
			get
			{
				ObjectType type = GetObjectType(m_typeInfo.ComType);
				return (int)type + 1;
			}
		}

		#endregion

		#region ITypeBrowserInfo Members

		public virtual INamespaceBrowserInfo Namespace
		{
			get { return m_namespace; }
		}

		public virtual IRepositoryBrowserInfo Repository
		{
			get { return m_namespace.Repository; }
		}

		public virtual IMemberBrowserInfo[] Members
		{
			get
			{
				if (m_members == null)
				{
					m_members = GetMembers();
					Debug.Assert(m_members != null, "m_members != null");
				}

				IMemberBrowserInfo[] array = new IMemberBrowserInfo[m_members.Count];
				m_members.Keys.CopyTo(array, 0);

				return array;
			}
		}

		public virtual ITypeBrowserInfo[] BaseTypes
		{
			get
			{
				if (m_baseTypes == null)
				{
					m_baseTypes = GetBaseTypes();
					Debug.Assert(m_baseTypes != null, "m_baseTypes != null");
				}

				ITypeBrowserInfo[] array = new ITypeBrowserInfo[m_baseTypes.Count];
				m_baseTypes.Keys.CopyTo(array, 0);

				return array;
			}
		}

		public virtual bool HasBaseTypes
		{
			get
			{
				if (m_baseTypes == null)
				{
					m_baseTypes = GetBaseTypes();
					Debug.Assert(m_baseTypes != null, "m_baseTypes != null");
				}

				return (m_baseTypes.Count > 0); 
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (m_typeInfo != null)
			{
				m_typeInfo.Dispose();
				m_typeInfo = null;
			}

			if (m_baseTypes != null)
			{
				foreach (ComTypeBrowserInfo baseType in m_baseTypes.Keys)
				{
					baseType.Dispose();
				}
				m_baseTypes = null;
			}

			m_namespace = null;
		}

		#endregion

		internal ObjectType ObjectType
		{
			get { return GetObjectType(m_typeInfo.ComType); }
		}

		public virtual string FullName
		{
			get { return m_namespace.FullName + "." + Marshal.GetTypeInfoName(m_typeInfo.ComType); }
		}

		public TYPEKIND TypeKind
		{
			get { return ComInterop.GetTypeKind(m_typeInfo.ComType); }
		}

		internal virtual ComBrowserManager Manager
		{
			get { return m_namespace.Manager; }
		}

		#region Static Methods

		internal static ObjectType GetObjectType(ITypeInfo typeInfo)
		{
			Debug.Assert(typeInfo != null, "typeInfo != null");

			IntPtr ptr = IntPtr.Zero; 
			typeInfo.GetTypeAttr(out ptr);

			try
			{
				return GetObjectType((TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR)));
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(ptr);
			}
		}

		internal static ObjectType GetObjectType(TYPEATTR typeAttr)
		{
			switch (typeAttr.typekind)
			{
				case TYPEKIND.TKIND_COCLASS:
					return ObjectType.Class;

				case TYPEKIND.TKIND_INTERFACE:
				case TYPEKIND.TKIND_DISPATCH:
					return ObjectType.Interface;

				case TYPEKIND.TKIND_ENUM:
					return ObjectType.Enum;

				case TYPEKIND.TKIND_RECORD:
					return ObjectType.Struct;

				case TYPEKIND.TKIND_UNION:
					return ObjectType.Union;

				case TYPEKIND.TKIND_MODULE:
					return ObjectType.Module;

				case TYPEKIND.TKIND_ALIAS:
					return ObjectType.Alias;

				default:
					throw new ApplicationException("Unsupported object type: '" + typeAttr.typekind.ToString() + "'.");
			}
		}

		internal static void WriteParameterAttributes(DescriptionBuilder sb, ParameterDesc parameter)
		{
			bool first = true;
			if (parameter.IsIn)
			{
				if ( first )
				{
					first = false;
					sb.Append("[");
				}
				else
				{
					sb.Append(", ");
				}

				sb.Append(ComBrowserSettings.GetKeyword(ParameterAttributes.In));
			}
			if (parameter.IsOut)
			{
				if ( first )
				{
					first = false;
					sb.Append("[");
				}
				else
				{
					sb.Append(", ");
				}

				sb.Append(ComBrowserSettings.GetKeyword(ParameterAttributes.Out));
			}
			if (parameter.IsRetval)
			{
				if ( first )
				{
					first = false;
					sb.Append("[");
				}
				else
				{
					sb.Append(", ");
				}

				sb.Append(ComBrowserSettings.GetKeyword(ParameterAttributes.Retval));
			}

			if (!first)
				sb.Append("] ");
		}

		internal static string GetPropertyKind(PropertyDesc property)
		{
			if (property.CanGet)
				return " [ get ]";
			else if (property.CanPut)
				return " [ put ]";
			else if (property.CanPutRef)
				return " [ putref ]";

			Debug.Fail("Property '" + property.Name + "' is not gettable, puttable, or putrefable.");
			return string.Empty;
		}

		private static int CompareTypeNames(ComTypeWrapper a, ComTypeWrapper b)
		{
			// Compare the names with case-insensitive comparison first (for ordering purposes)

			string nameA = Marshal.GetTypeInfoName(a.ComType);
			string nameB = Marshal.GetTypeInfoName(b.ComType);

			int result = string.Compare(nameA, nameB, true);
			if (result != 0)
				return result;

			// Now do a case-sensitive comparison - if this returns 0 then it's really the same type

			return string.Compare(nameA, nameB, false);
		}

		private static ComInterfaceType GetInterfaceType(TYPEATTR typeAttr)
		{
			Debug.Assert(GetObjectType(typeAttr) == ObjectType.Interface,
				"GetObjectType(typeAttr) == ObjectType.Interface");

			if ((typeAttr.wTypeFlags & TYPEFLAGS.TYPEFLAG_FDUAL) == TYPEFLAGS.TYPEFLAG_FDUAL)
				return ComInterfaceType.InterfaceIsDual;
			else if ((typeAttr.wTypeFlags & TYPEFLAGS.TYPEFLAG_FDISPATCHABLE) == TYPEFLAGS.TYPEFLAG_FDISPATCHABLE)
				return ComInterfaceType.InterfaceIsIDispatch;
			else
				return ComInterfaceType.InterfaceIsIUnknown;
		}

		private static void AppendTypeAttribute(DescriptionBuilder sb, TYPEATTR typeAttr, TYPEFLAGS attribute)
		{
			if ((typeAttr.wTypeFlags & attribute) == attribute)
			{
				sb.Append(", ");
				sb.Append(ComBrowserSettings.GetKeyword(attribute));
			}
		}

		private static string GetParameterTypes(ParameterDesc[] parameters)
		{
			DescriptionBuilder sb = new DescriptionBuilder(false);

			bool first = true;
			foreach (ParameterDesc parameter in parameters)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					sb.Append(", ");
				}

				sb.Append(parameter.Type);
			}

			return sb.ToString();
		}

		#endregion

		public ComInterfaceType GetInterfaceType()
		{
			Debug.Assert(m_typeInfo != null, "m_typeInfo != null");

			IntPtr ptr = IntPtr.Zero; 
			m_typeInfo.ComType.GetTypeAttr(out ptr);

			try
			{
				TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));

				ObjectType type = GetObjectType(typeAttr);
				if (type != ObjectType.Interface)
					throw new ApplicationException("Unable to get the interface type for type '"
						+ DisplayName + "', because its object type is '" + ComBrowserSettings.GetKeyword(type)
						+ "', not 'interface'.");

				return GetInterfaceType(typeAttr);
			}
			finally
			{
				m_typeInfo.ComType.ReleaseTypeAttr(ptr);
			}
		}

		internal void OnMemberOrderChanged()
		{
			// The member order has changed, so re-sort our member list (if any).

			if (m_members != null)
			{
				m_members = new SortedList(m_members);
			}
		}

		private SortedList GetBaseTypes()
		{
			Debug.Assert(m_typeInfo != null, "m_typeInfo != null");

			SortedList baseTypes = new SortedList();

			// Add the implemented types.

			int implCount = ComInterop.GetImplTypeCount(m_typeInfo.ComType);

			for ( int index = 0; index < implCount; ++index )
			{
				int href;
				m_typeInfo.ComType.GetRefTypeOfImplType(index, out href);

				ITypeInfo refTypeInfo;
				m_typeInfo.ComType.GetRefTypeInfo(href, out refTypeInfo);
				Debug.Assert(refTypeInfo != null, "refTypeInfo != null");

				string name = Marshal.GetTypeInfoName(refTypeInfo);

				// Do not display IUnknown or IDispatch.

				if ( !(name == "IUnknown" || name == "IDispatch") )
				{
					baseTypes.Add(Manager.GetTypeInfo(refTypeInfo), null);
				}
				else
				{
					Marshal.ReleaseComObject(refTypeInfo);
				}
			}

			return baseTypes;
		}

		private SortedList GetMembers()
		{
			try
			{
				SortedList members = new SortedList();

				IntPtr ptr = IntPtr.Zero;
				m_typeInfo.ComType.GetTypeAttr(out ptr);
				TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));

				try
				{
					if (typeAttr.typekind  == TYPEKIND.TKIND_COCLASS)
					{
						// This is a co-class, so we want to show members of the default interfaces, same as
						// VB6 object browser does.

						AddCoClassMembers(members);
					}
					else
					{
						AddMemberFunctions(members, typeAttr.cFuncs);
						AddMemberVariables(members, typeAttr.cVars);
					}
				}
				finally
				{
					m_typeInfo.ComType.ReleaseTypeAttr(ptr);
				}

				return members;
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to get the members of type '" + Marshal.GetTypeInfoName(m_typeInfo.ComType) + "'.", ex);
			}
		}

		private void AddCoClassMembers(SortedList members)
		{
			int implCount = ComInterop.GetImplTypeCount(m_typeInfo.ComType);

			for ( int index = 0; index < implCount; ++index )
			{
                IMPLTYPEFLAGS implTypeFlags;
				m_typeInfo.ComType.GetImplTypeFlags(index, out implTypeFlags);

				if ((implTypeFlags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT) == IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT)
				{
					// This is a default interface - gets the corresponding ComTypeBrowserInfo object.

					int href;
					m_typeInfo.ComType.GetRefTypeOfImplType(index, out href);

					ITypeInfo refTypeInfo;
					m_typeInfo.ComType.GetRefTypeInfo(href, out refTypeInfo);
					Debug.Assert(refTypeInfo != null, "refTypeInfo != null");

					// Check if this interface is an event source.

					bool isEventSource = ((implTypeFlags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE) == IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE);

					// Add each member of the base interface to our members collection.

					ComTypeBrowserInfo baseType = Manager.GetTypeInfo(refTypeInfo);

					AddTypeMembersRecursive(baseType, members, isEventSource);
				}
			}
		}

		private void AddTypeMembersRecursive(ComTypeBrowserInfo sourceType, SortedList destination,
			bool isEventSource)
		{
			foreach (ComMemberBrowserInfo memberInfo in sourceType.Members)
			{
				ComMemberBrowserInfo newMemberInfo;

				if (isEventSource && memberInfo.MemberType == OperationMemberType.Method)
				{
					newMemberInfo = new ComMemberBrowserInfo(this,
						((MethodDesc)memberInfo.MemberDesc).CreateEventFromMethod(),
						memberInfo.NodeText);
				}
				else
				{
					newMemberInfo = new ComMemberBrowserInfo(this, memberInfo.MemberDesc,
						memberInfo.NodeText);
				}

				destination.Add(newMemberInfo, newMemberInfo.NodeText);
			}

			foreach (ComTypeBrowserInfo baseType in sourceType.BaseTypes)
			{
				AddTypeMembersRecursive(baseType, destination, isEventSource);
			}
		}

		private void AddMemberFunctions(SortedList members, int count)
		{
			for ( int index = 0; index < count; ++index )
			{
				IntPtr funcptr = IntPtr.Zero;
				m_typeInfo.ComType.GetFuncDesc(index, out funcptr);
				FUNCDESC funcDesc = (FUNCDESC)Marshal.PtrToStructure(funcptr, typeof(FUNCDESC));

				try
				{
					string memberNodeText;

					// Only add the method if it is not restricted (removes IUnknown, IDispatch methods etc).

					if ( (funcDesc.wFuncFlags & (short) FUNCFLAGS.FUNCFLAG_FRESTRICTED) == 0 )
					{
						switch (funcDesc.invkind)
						{
							case INVOKEKIND.INVOKE_FUNC:
								MethodDesc methodDesc = new MethodDesc(m_typeInfo.ComType, funcDesc);
								memberNodeText = methodDesc.Name + "(" + GetParameterTypes(methodDesc.Parameters) + ")";
								members.Add(new ComMemberBrowserInfo(this, methodDesc, memberNodeText), memberNodeText);
								break;

							case INVOKEKIND.INVOKE_PROPERTYGET:
							case INVOKEKIND.INVOKE_PROPERTYPUT:
							case INVOKEKIND.INVOKE_PROPERTYPUTREF:
								PropertyDesc propertyDesc = new PropertyDesc(m_typeInfo.ComType, funcDesc);
								memberNodeText = propertyDesc.Name + GetPropertyKind(propertyDesc)
									+ " (" + GetParameterTypes(propertyDesc.Parameters) + ")";
								members.Add(new ComMemberBrowserInfo(this, propertyDesc, memberNodeText), memberNodeText);
								break;

							default:
								Debug.Fail("Unexpected value of funcDesc.invkind: " + funcDesc.invkind.ToString());
								break;
						}
					}
				}
				finally
				{
					m_typeInfo.ComType.ReleaseFuncDesc(funcptr);
				}
			}
		}

		private void AddMemberVariables(SortedList members, int count)
		{
			for ( int index = 0; index < count; ++index )
			{
				IntPtr varptr = IntPtr.Zero;
				m_typeInfo.ComType.GetVarDesc(index, out varptr);
				Win32.VARDESC varDesc = (Win32.VARDESC)Marshal.PtrToStructure(varptr, typeof(Win32.VARDESC));

				try
				{
					MemberDesc desc = new FieldDesc(m_typeInfo.ComType, varDesc);
					members.Add(new ComMemberBrowserInfo(this, desc, desc.Name), desc.Name);
				}
				finally
				{
					m_typeInfo.ComType.ReleaseVarDesc(varptr);
				}
			}
		}

		private DescriptionText GetDescription()
		{
			try
			{
				DescriptionBuilder sb = new DescriptionBuilder(true);

				IntPtr ptr = IntPtr.Zero; 
				m_typeInfo.ComType.GetTypeAttr(out ptr);

				try
				{
					TYPEATTR typeAttr = (TYPEATTR)Marshal.PtrToStructure(ptr, typeof(TYPEATTR));

					// Type GUID and attributes.

					sb.Append("[ uuid(");
					sb.Append(typeAttr.guid.ToString().ToUpper());
					sb.Append(")");

					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FAGGREGATABLE);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FAPPOBJECT);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FCONTROL);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FLICENSED);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FHIDDEN);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FNONEXTENSIBLE);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION);
					AppendTypeAttribute(sb, typeAttr, TYPEFLAGS.TYPEFLAG_FRESTRICTED);

					sb.Append(" ]");
					sb.EndLine();

					// Object type (including interface type for an interface).

					ObjectType objectType = GetObjectType(typeAttr);

					if (objectType == ObjectType.Interface)
					{
						sb.Append(ComBrowserSettings.GetKeyword(GetInterfaceType(typeAttr)));
					}
					else
					{
						sb.Append(ComBrowserSettings.GetKeyword(objectType));
					}
					sb.Append(" ");
				}
				finally
				{
					m_typeInfo.ComType.ReleaseTypeAttr(ptr);
				}

				// Name and description.

				sb.AppendName(Marshal.GetTypeInfoName(m_typeInfo.ComType));
				sb.EndFirstLine();

				sb.Append(@"     Member of ");
				sb.AppendLink(Namespace.NodeText, Namespace);
				sb.EndLine();

				string description = GetDocString();
				if (description != null && description.Length != 0)
				{
					sb.AppendHeading("Description:");
					sb.Append(description);
				}

				return sb.GetText();
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write the type declaration for type '"
					+ Marshal.GetTypeInfoName(m_typeInfo.ComType) + "'.", ex);
			}
		}

		private string GetDocString()
		{
			string name;
			string docString;
			int helpContext;
			string helpFile;

			m_typeInfo.ComType.GetDocumentation(-1, out name, out docString, out helpContext, out helpFile);

			return docString;
		}
	}
}
