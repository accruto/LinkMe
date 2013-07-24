using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An ITypeBrowserInfo implementation that directly accesses .NET types (System.Type objects).
	/// </summary>
	public class NetTypeBrowserInfo : ElementBrowserInfo, ITypeBrowserInfo, IDisposable
	{
		private Type m_type;
		private NetNamespaceBrowserInfo m_namespace;
		private SortedList m_baseTypes = null;
		private SortedList m_members = null;
		private DescriptionText m_description = null;

		internal NetTypeBrowserInfo()
		{
		}

		internal NetTypeBrowserInfo(Type type, NetNamespaceBrowserInfo ns)
		{
			Debug.Assert(type != null && ns != null, "type != null && ns != null");

			m_type = type;
			m_namespace = ns;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is NetTypeBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			NetTypeBrowserInfo other = (NetTypeBrowserInfo)obj;

			if (other == this)
				return 0;

			switch (Settings.TypeOrder)
			{
				case TypeOrder.Alphabetical:
					return CompareTypeNames(m_type, other.m_type);

				case TypeOrder.ObjectType:
					// Compare object types
					int result1 = GetObjectType(m_type).CompareTo(GetObjectType(other.m_type));
					if (result1 != 0)
						return result1;

					// Same object type - compare names
					return CompareTypeNames(m_type, other.m_type);

				case TypeOrder.ObjectAccess:
					// Compare object types
					int result2 = GetEffectiveTypeAccess(m_type).CompareTo(GetEffectiveTypeAccess(other.m_type));
					if (result2 != 0)
						return result2;

					// Same object access - compare names
					return CompareTypeNames(m_type, other.m_type);

				default:
					throw new ApplicationException("Unsupported TypeOrder enum value: '"
						+ Settings.TypeOrder.ToString() + "'.");
			}
		}

		#endregion

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return GetTypeDisplayFullName(m_type); }
		}

		public override string NodeText
		{
			get { return GetTypeDisplayName(m_type); }
		}

		public override DescriptionText Description
		{
			get { return GetDescription(m_namespace, GetBaseTypeForDescription()); }
		}

		public override int ImageIndex
		{
			get
			{
				return ((int)GetObjectType(m_type) * 5
					+ NetAssemblyBrowserInfo.GetAccessModifierOffset(GetEffectiveTypeAccess(m_type)) - 3);
			}
		}

		#endregion

		#region ITypeBrowserInfo Members

		public virtual INamespaceBrowserInfo Namespace
		{
			get { return (m_namespace.IsNullNamespace ? null : m_namespace); }
		}

		public virtual IRepositoryBrowserInfo Repository
		{
			get { return m_namespace.Repository; }
		}

		public virtual IMemberBrowserInfo[] Members
		{
			get
			{
				ICollection members = MembersInternal;

				IMemberBrowserInfo[] array = new IMemberBrowserInfo[members.Count];
				members.CopyTo(array, 0);

				return array;
			}
		}

		public virtual ITypeBrowserInfo[] BaseTypes
		{
			get
			{
				ICollection baseTypes = BaseTypesInternal;

				ITypeBrowserInfo[] array = new ITypeBrowserInfo[baseTypes.Count];
				baseTypes.CopyTo(array, 0);

				return array;
			}
		}

		public virtual bool HasBaseTypes
		{
			get { return (BaseTypesInternal.Count > 0); }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			m_namespace = null;
		}

		#endregion

		#region Static Methods

		internal static AccessModifiers GetEffectiveTypeAccess(Type type)
		{
			if (type.DeclaringType == null)
				return GetTypeAccess(type);
			else
				return (AccessModifiers)Math.Max(
					(int)GetTypeAccess(type), (int)GetEffectiveTypeAccess(type.DeclaringType));
		}

		internal static string GetTypeDisplayName(Type type)
		{
			if (type.MemberType == MemberTypes.NestedType)
				return type.FullName.Substring(type.FullName.LastIndexOf(Type.Delimiter) + 1).Replace('+', '.');
			else
			{
				Debug.Assert(type.MemberType != MemberTypes.Custom, "MemberType = Custom for type '" + type.FullName + "'.");
				return type.Name;
			}
		}

		internal static void AppendParameterTypeDisplayName(NetBrowserManager manager,
			DescriptionBuilder sb, Type type, bool ignoreRef, bool link)
		{
			NetBrowserSettings settings = (NetBrowserSettings)manager.Settings;

			if (type.HasElementType)
			{
				if (type.IsByRef)
				{
					if (!ignoreRef)
					{
						sb.Append(settings.GetKeyword("Type.IsByRef"));
						sb.Append(" ");
					}
					AppendParameterTypeDisplayName(manager, sb, type.GetElementType(), ignoreRef, link);
				}
				else if (type.IsArray)
				{
					AppendParameterTypeDisplayName(manager, sb, type.GetElementType(), ignoreRef, link);

					sb.Append(settings.GetKeyword(NetBrowserSettings.KeywordArrayStart));

					int dimensions = type.GetArrayRank();
					for (int index = 1; index < dimensions; index++)
					{
						sb.Append(",");
					}

					sb.Append(settings.GetKeyword(NetBrowserSettings.KeywordArrayEnd));
				}
				else if (type.IsPointer)
				{
					AppendParameterTypeDisplayName(manager, sb, type.GetElementType(), ignoreRef, link);
					sb.Append(" *");
				}
				else
					throw new ApplicationException("Type '" + type.Name + "' has element type, but is not a"
						+ " reference type, array or pointer.");
			}
			else
			{
				string keyword = ((NetBrowserSettings)manager.Settings).GetKeyword(type.AssemblyQualifiedName);
				if (keyword == null)
				{
					if (link)
					{
						sb.AppendLink(GetTypeDisplayFullName(type), manager.GetTypeInfoForLink(
							type.Assembly.Location, type.FullName));
					}
					else
					{
						sb.Append(GetTypeDisplayFullName(type));
					}
				}
				else
				{
					sb.Append(keyword); // We don't want to make keywords into links
				}
			}
		}

		internal static ObjectType GetObjectType(Type type)
		{
			Debug.Assert(type != null, "type != null");

			if (type.IsClass)
			{
				if (type.IsSubclassOf(typeof(Delegate)))
					return ObjectType.Delegate;
				else
					return ObjectType.Class;
			}
			if (type.IsInterface)
				return ObjectType.Interface;
			// Need to check for enums first, because IsValueType returns true for them.
			if (type.IsEnum)
				return ObjectType.Enum;
			if (type.IsValueType || type == typeof(Enum))
				return ObjectType.Struct;

			throw new ApplicationException("Unsupported object type: '" + type.ToString() + "'.");
		}

		internal static void WriteParameterType(NetBrowserManager manager, DescriptionBuilder sb,
			ParameterInfo parameter, bool link)
		{
			NetBrowserSettings settings = (NetBrowserSettings)manager.Settings;

			if (parameter.IsOut)
			{
				sb.Append(settings.GetKeyword(ParameterAttributes.Out));
				sb.Append(" ");
				AppendParameterTypeDisplayName(manager, sb, parameter.ParameterType, true, link);
			}
			else
			{
				if (parameter.IsDefined(typeof(ParamArrayAttribute), false))
				{
					sb.Append(settings.GetKeyword(typeof(ParamArrayAttribute)));
					sb.Append(" ");
				}

				AppendParameterTypeDisplayName(manager, sb, parameter.ParameterType, false, link);
			}
		}

		private static string GetTypeDisplayFullName(Type type)
		{
			if (type.MemberType == MemberTypes.NestedType)
				return type.FullName.Replace('+', '.');
			else
			{
				Debug.Assert(type.MemberType != MemberTypes.Custom, "MemberType = Custom for type '" + type.FullName + "'.");
				return type.FullName;
			}
		}

		private static AccessModifiers GetTypeAccess(Type type)
		{
			Debug.Assert(type != null, "type != null");

			if (type.IsPublic || type.IsNestedPublic)
				return AccessModifiers.Public;
			if (type.IsNestedFamORAssem)
				return AccessModifiers.ProtectedInternal;
			if (type.IsNestedFamily)
				return AccessModifiers.Protected;
			if (type.IsNestedPrivate)
				return AccessModifiers.Private;
			if (type.IsNestedAssembly || type.IsNotPublic)
				return AccessModifiers.Internal;

			throw new ApplicationException("Unable to determine access modifier for type '" + type.FullName + "'.");
		}

		private static int CompareTypeNames(Type a, Type b)
		{
			// Compare the names with case-insensitive comparison first (for ordering purposes)

			string nameA = NetTypeBrowserInfo.GetTypeDisplayName(a);
			string nameB = NetTypeBrowserInfo.GetTypeDisplayName(b);

			int result = string.Compare(nameA, nameB, true);
			if (result != 0)
				return result;

			// Same - now do a case-sensitive comparison.

			result = string.Compare(nameA, nameB, false);
			if (result != 0)
				return result;

			// Type names are exactly the same - compare assembly-qualified names.

			return string.Compare(a.AssemblyQualifiedName, b.AssemblyQualifiedName, false);
		}

		#endregion

		public virtual string FullName
		{
			get { return m_type.FullName; }
		}

		internal virtual NetBrowserManager Manager
		{
			get { return m_namespace.Manager; }
		}

		internal ICollection MembersInternal
		{
			get
			{
				if (m_members == null)
				{
					m_members = GetMembers();
					Debug.Assert(m_members != null, "m_members != null");
				}

				return m_members.Keys;
			}
		}

		internal ICollection BaseTypesInternal
		{
			get
			{
				if (m_baseTypes == null)
				{
					m_baseTypes = GetBaseTypes();
					Debug.Assert(m_baseTypes != null, "m_baseTypes != null");
				}

				return m_baseTypes.Keys;
			}
		}

		internal NetNamespaceBrowserInfo NamespaceInternal
		{
			get { return m_namespace; }
		}

		private NetBrowserSettings Settings
		{
			get { return (NetBrowserSettings)Manager.Settings; }
		}

		public virtual bool IsSubclassOf(System.Type c)
		{
			return m_type.IsSubclassOf(c);
		}

		internal NetTypeBrowserInfo GetBaseTypeForDescription()
		{
			return (m_type.BaseType == null ? null : Manager.GetTypeInfo(m_type.BaseType));
		}

		internal DescriptionText GetDescription(NetNamespaceBrowserInfo ns, NetTypeBrowserInfo baseType)
		{
			if (m_description == null)
			{
				m_description = GetDescriptionInternal(ns, baseType);
				Debug.Assert(m_description != null, "m_description != null");
			}

			return m_description;
		}

		internal virtual NetMemberBrowserInfo GetMember(string nodeText)
		{
			if (m_members == null)
			{
				m_members = GetMembers();
				Debug.Assert(m_members != null, "m_members != null");
			}

			// Since the member is the key and the node text is the value we need to look up the key by value.

			int index = m_members.IndexOfValue(nodeText);
			return (index == -1 ? null : (NetMemberBrowserInfo)m_members.GetKey(index));
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
			Debug.Assert(m_type != null, "m_type != null");

			// Don't show any child nodes for delegates or enums - everyone knows what their base type is
			// (and if the user really wants it the base type is still shown in the description).

			if (m_type.IsSubclassOf(typeof(Delegate)) || m_type.IsEnum)
				return new SortedList();

			SortedList baseTypes = new SortedList();

			// Add the base type, unless it's "Object".

			if (m_type.BaseType != null && m_type.BaseType != typeof(object))
			{
				baseTypes.Add(Manager.GetTypeInfo(m_type.BaseType), null);
			}

			// Add the implemented interfaces.

			foreach (Type typeInterface in m_type.GetInterfaces())
			{
				if (Settings.TypeShouldBeVisible(typeInterface))
				{
					baseTypes.Add(Manager.GetTypeInfo(typeInterface), null);
				}
			}

			return baseTypes;
		}

		private SortedList GetMembers()
		{
			const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic
				| BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;

			try
			{
				MemberInfo[] memberInfos = m_type.GetMembers(flags);
				SortedList members = new SortedList();

				foreach (MemberInfo memberInfo in memberInfos)
				{
					if (memberInfo.MemberType == MemberTypes.NestedType)
						continue; // Nested types are shown separately
					if (!Settings.MemberShouldBeVisible(memberInfo))
						continue; // The user has chosen not to see members with this visibility.

					string memberNodeText;

					switch (memberInfo.MemberType)
					{
						case MemberTypes.Constructor:
							// Constructor - use the type name as the member name
							memberNodeText = m_type.Name + "(" + GetParameterTypes((MethodBase)memberInfo) + ")";
							break;

						case MemberTypes.Method:
							// Don't show private interface implementations - they are shown under the interface
							if (memberInfo.Name.IndexOf(Type.Delimiter) != -1)
								continue;

							// Don't show "special" methods - get/set for properties, add/remove for events, etc.,
							// except for operators.
							if ((((MethodInfo)memberInfo).Attributes & MethodAttributes.SpecialName) == MethodAttributes.SpecialName)
							{
								if (memberInfo.Name.StartsWith("op_"))
								{
									memberNodeText = NetMemberBrowserInfo.GetOperatorName(Settings, memberInfo.Name)
										+ "(" + GetParameterTypes((MethodBase)memberInfo) + ")";
								}
								else
									continue;
							}
							else
							{
								memberNodeText = memberInfo.Name + "(" + GetParameterTypes((MethodBase)memberInfo) + ")";
							}
							break;

						case MemberTypes.Field:
							// Don't show "special" fields - value__ for enums, etc.
							if (((FieldInfo)memberInfo).IsSpecialName)
								continue;

							// Don't show the private EventHandler fields for events. These fields have the
							// same name as the event.
							if (m_type.GetEvent(memberInfo.Name, flags) != null)
								continue;

							memberNodeText = memberInfo.Name;
							break;

						case MemberTypes.Property:
							// Even if we don't have permission to access a property, the permission
							// attributes only apply to the accessors - properties are "special" in
							// this sense. Check for this now and if we cannot get any accessors then
							// don't display the property at all.
							if (((PropertyInfo)memberInfo).GetAccessors(true).Length == 0)
								continue;

							// Don't show private interface implementations - they are shown under the interface
							if (memberInfo.Name.IndexOf(Type.Delimiter) != -1)
								continue;

							memberNodeText = memberInfo.Name;
							break;

						default:
							// Don't show private interface implementations - they are shown under the interface
							if (memberInfo.Name.IndexOf(Type.Delimiter) != -1)
								continue;

							memberNodeText = memberInfo.Name;
							break;
					}

					members.Add(new NetMemberBrowserInfo(this, memberInfo, memberNodeText), memberNodeText);
				}

				return members;
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to get the members of type '" + m_type.FullName + "'.", ex);
			}
		}

		private DescriptionText GetDescriptionInternal(NetNamespaceBrowserInfo ns, NetTypeBrowserInfo baseType)
		{
			try
			{
				DescriptionBuilder sb = new DescriptionBuilder(true);

				sb.Append(Settings.GetKeyword(GetTypeAccess(m_type)));
				sb.Append(" ");

				AppendTypeAttribute(sb, m_type, TypeAttributes.Abstract);
				AppendTypeAttribute(sb, m_type, TypeAttributes.Sealed);

				sb.Append(Settings.GetKeyword(GetObjectType(m_type)));
				sb.Append(" ");

				sb.AppendName(GetTypeDisplayName(m_type));

				if (baseType != null)
				{
					sb.Append(" : ");
					sb.AppendLink(baseType.DisplayName, baseType);
				}

				if (m_type.IsEnum)
				{
					Type underlying = Enum.GetUnderlyingType(m_type);
					sb.Append(" (");
					sb.AppendName(Settings.GetKeyword(underlying.AssemblyQualifiedName));
					sb.Append(")");
				}

				IElementBrowserInfo container = (ns.IsNullNamespace ? ns.Repository : (IElementBrowserInfo)ns);

				sb.EndFirstLine();
				sb.Append(@"     Member of ");
				sb.AppendLink(container.NodeText, container);
				sb.EndLine();

				return sb.GetText();
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write the type declaration for type '"
					+ m_type.FullName + "'.", ex);
			}
		}

		private void AppendTypeAttribute(DescriptionBuilder sb, Type type, TypeAttributes attribute)
		{
			if ((type.Attributes & attribute) == attribute)
			{
				sb.Append(Settings.GetKeyword(attribute));
				sb.Append(" ");
			}
		}

		private string GetParameterTypes(MethodBase method)
		{
			DescriptionBuilder sb = new DescriptionBuilder(false);

			bool first = true;
			foreach (ParameterInfo parameter in method.GetParameters())
			{
				if (first)
				{
					first = false;
				}
				else
				{
					sb.Append(", ");
				}

				if (parameter.IsOptional)
				{
					sb.Append("[");
				}
				WriteParameterType(Manager, sb, parameter, false);
				if (parameter.IsOptional)
				{
					sb.Append("]");
				}
			}

			return sb.ToString();
		}
	}
}
