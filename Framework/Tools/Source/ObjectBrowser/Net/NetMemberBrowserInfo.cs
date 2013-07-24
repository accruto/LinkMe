using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An IMemberBrowserInfo implementation that directly accesses .NET members (System.Reflection.MemberInfo objects).
	/// </summary>
	public class NetMemberBrowserInfo : ElementBrowserInfo, IMemberBrowserInfo
	{
		private const int ImageIndexEnumIdentifier = 0;
		private const int ImageIndexOperator = 1;
		private const int FirstOrderedImageIndex = 2;

		private NetTypeBrowserInfo m_type;
		private MemberInfo m_memberInfo;
		private string m_nodeText;
		private DescriptionText m_description = null;

		internal NetMemberBrowserInfo()
		{
		}

		internal NetMemberBrowserInfo(NetTypeBrowserInfo type, MemberInfo memberInfo, string nodeText)
		{
			Debug.Assert(type != null && memberInfo != null && nodeText != null,
				"type != null && memberInfo != null && nodeText != null");

			m_type = type;
			m_memberInfo = memberInfo;
			m_nodeText = nodeText;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is NetMemberBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			NetMemberBrowserInfo other = (NetMemberBrowserInfo)obj;

			if (other == this)
				return 0;

			switch (Settings.MemberOrder)
			{
				case MemberOrder.Alphabetical:
					// Compare names
					int result1 = CompareMemberNames(Settings, m_memberInfo, other.m_memberInfo);
					if (result1 != 0)
						return result1;

					// Compare member types
					return GetMemberTypePriority(m_memberInfo).CompareTo(
						GetMemberTypePriority(other.m_memberInfo));

				case MemberOrder.MemberType:
					// Compare member types
					int result2 = GetMemberTypePriority(m_memberInfo).CompareTo(
						GetMemberTypePriority(other.m_memberInfo));
					if (result2 != 0)
						return result2;

					// Compare names
					return CompareMemberNames(Settings, m_memberInfo, other.m_memberInfo);

				case MemberOrder.MemberAccess:
					// Compare member access
					int result3 = GetMemberAccess(m_memberInfo).CompareTo(
						GetMemberAccess(other.m_memberInfo));
					if (result3 != 0)
						return result3;

					// Compare names
					return CompareMemberNames(Settings, m_memberInfo, other.m_memberInfo);

				default:
					throw new ApplicationException("Unsupported MemberOrder enum value: '"
						+ Settings.MemberOrder.ToString() + "'.");
			}
		}

		#endregion

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return Name; }
		}

		public override string NodeText
		{
			get { return m_nodeText; }
		}

		public override DescriptionText Description
		{
			get { return GetDescription(Manager, m_type); }
		}

		public override int ImageIndex
		{
			get
			{
				int memberType;

				switch (m_memberInfo.MemberType)
				{
					case MemberTypes.Constructor:
						memberType = (((MethodBase)m_memberInfo).IsStatic ? 5 : 0);
						break;

					case MemberTypes.Method:
						if (MethodIsOperator((MethodInfo)m_memberInfo))
							return ImageIndexOperator;
						else
						{
							memberType = (((MethodBase)m_memberInfo).IsStatic ? 5 : 0);
						}
						break;

					case MemberTypes.Property:
						MethodInfo[] accessors = ((PropertyInfo)m_memberInfo).GetAccessors(true);
						Debug.Assert(accessors.Length > 0, "Unable to get the accessors for property '"
							+ m_memberInfo.DeclaringType.ToString() + "." + m_memberInfo.Name
							+ "' - this property should not have been displayed.");
						memberType = (accessors[0].IsStatic ? 6 : 1);
						break;

					case MemberTypes.Field:
						FieldInfo fieldInfo = (FieldInfo)m_memberInfo;

						if (fieldInfo.IsLiteral)
						{
							if (m_memberInfo.DeclaringType.IsEnum)
								return ImageIndexEnumIdentifier; // Enum identifier
							else
							{
								memberType = 2; // Constant field
							}
						}
						else
						{
							memberType = (fieldInfo.IsStatic ? 7 : 3); // Variable field
						}
						break;

					case MemberTypes.Event:
						memberType = 4;
						break;

					default:
						throw new ApplicationException("Unhandled member type: " + m_memberInfo.MemberType.ToString());
				}

				return (memberType * 5 + NetAssemblyBrowserInfo.GetAccessModifierOffset(GetMemberAccess(m_memberInfo))
					+ FirstOrderedImageIndex);
			}
		}

		#endregion

		#region IMemberBrowserInfo Members

		public virtual ITypeBrowserInfo Type
		{
			get { return m_type; }
		}

		#endregion

		#region Static methods

		internal static AccessModifiers GetMemberAccess(MemberInfo member)
		{
			MethodBase method;

			switch (member.MemberType)
			{
				case MemberTypes.Constructor:
				case MemberTypes.Method:
					method = (MethodBase)member;
					break;

				case MemberTypes.Property:
					MethodInfo[] accessors = ((PropertyInfo)member).GetAccessors(true);
					if (accessors.Length == 0)
						return AccessModifiers.Unknown;

					method = accessors[0];
					break;

				case MemberTypes.Field:
					// Fields don't have a method - check the access here
					FieldInfo field = (FieldInfo)member;

					if (field.IsPublic)
						return AccessModifiers.Public;
					if (field.IsFamilyOrAssembly)
						return AccessModifiers.ProtectedInternal;
					if (field.IsFamily)
						return AccessModifiers.Protected;
					if (field.IsAssembly)
						return AccessModifiers.Internal;
					if (field.IsPrivate)
						return AccessModifiers.Private;

					throw new ApplicationException("Unsupported member access for member '" + member.ToString() + "'.");

				case MemberTypes.Event:
					method = ((EventInfo)member).GetAddMethod(true);
					break;

				default:
					throw new ApplicationException("Unsupported member type: '" + member.MemberType.ToString() + "'.");
			}

			Debug.Assert(method != null, "method != null");

			if (method.IsPublic)
				return AccessModifiers.Public;
			if (method.IsFamilyOrAssembly)
				return AccessModifiers.ProtectedInternal;
			if (method.IsFamily)
				return AccessModifiers.Protected;
			if (method.IsAssembly)
				return AccessModifiers.Internal;
			if (method.IsPrivate)
				return AccessModifiers.Private;

			throw new ApplicationException("Unsupported member access for member '" + member.ToString() + "'.");
		}

		internal static string GetOperatorName(NetBrowserSettings settings, string methodName)
		{
			// Replace the method name with the operator declaration, like VS.NET does.

			string opName = settings.GetKeyword(methodName);

			if (opName == null)
			{
				Debug.Fail("Unexpected operator name: '" + methodName + "'.");
				return methodName;
			}
			else
				return opName;
		}

		private static bool MethodIsOperator(MethodInfo method)
		{
			return ((method.Attributes & MethodAttributes.SpecialName) == MethodAttributes.SpecialName
				&& method.Name.StartsWith("op_"));
		}

		private static string GetMemberName(NetBrowserSettings settings, MemberInfo member)
		{
			if (member.MemberType == MemberTypes.Constructor)
				return member.DeclaringType.Name;
			else if (member.MemberType == MemberTypes.Method && MethodIsOperator((MethodInfo)member))
				return GetOperatorName(settings, member.Name);
			else
				return member.Name;
		}

		private static int GetMemberTypePriority(MemberInfo member)
		{
			// Order of priority: constructor, method, property, field, event

			switch (member.MemberType)
			{
				case MemberTypes.Constructor:
					return 1;
				case MemberTypes.Method:
					return 2;
				case MemberTypes.Property:
					return 3;
				case MemberTypes.Field:
					if (((FieldInfo)member).IsLiteral)
						return 5; // Constant
					else
						return 4; // Variable
				case MemberTypes.Event:
					return 6;
				default:
					Debug.Fail("Unsupported member type: '" + member.MemberType.ToString() + "'.");
					return 0;
			}
		}

		private static int CompareMemberNames(NetBrowserSettings settings, MemberInfo a, MemberInfo b)
		{
			// Compare the names with case-insensitive comparison first (for ordering purposes)

			string nameA = GetMemberName(settings, a);
			string nameB = GetMemberName(settings, b);

			int result = string.Compare(nameA, nameB, true);
			if (result != 0)
				return result;

			// Now try case-sensitive comparison

			result = string.Compare(nameA, nameB, false);
			if (result != 0)
				return result;

			// Names are the same - must be methods or properties with different signatures

			Debug.Assert(a.MemberType == MemberTypes.Method || a.MemberType == MemberTypes.Constructor
				|| a.MemberType == MemberTypes.Property, "a.MemberType == MemberTypes.Method"
				+ " || a.MemberType == MemberTypes.Constructor || a.MemberType == MemberTypes.Property");
			return string.Compare(a.ToString(), b.ToString(), true);
		}

		private static string[] GetParameterTypes(ParameterInfo[] parameters)
		{
			string[] types = new string[parameters.Length];
			
			int index = 0;
			foreach (ParameterInfo parameter in parameters)
			{
				types[index++] = parameter.ParameterType.FullName;
			}

			return types;
		}

		private static void AppendParameters(DescriptionBuilder sb, NetBrowserManager manager, ParameterInfo[] parameters)
		{
			bool first = true;
			foreach (ParameterInfo parameter in parameters)
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

				NetTypeBrowserInfo.WriteParameterType(manager, sb, parameter, true);

				sb.Append(" ");
				sb.Append(parameter.Name);
				if (parameter.IsOptional)
				{
					sb.Append("]");
				}
			}
		}

		#endregion

		public virtual string Name
		{
			get { return m_memberInfo.Name; }
		}

		public virtual MemberTypes MemberType
		{
			get { return m_memberInfo.MemberType; }
		}

		private NetBrowserManager Manager
		{
			get { return m_type.Manager; }
		}

		private NetBrowserSettings Settings
		{
			get { return (NetBrowserSettings)Manager.Settings; }
		}

		public virtual string[] GetParameterTypes()
		{
			if (m_memberInfo is ConstructorInfo)
				return GetParameterTypes(((ConstructorInfo)m_memberInfo).GetParameters());
			else if (m_memberInfo is MethodInfo)
				return GetParameterTypes(((MethodInfo)m_memberInfo).GetParameters());
			else if (m_memberInfo is PropertyInfo)
				return GetParameterTypes(((PropertyInfo)m_memberInfo).GetIndexParameters());
			else
				throw new ApplicationException("Unable to get the parameter types for a member of type '"
					+ m_memberInfo.MemberType.ToString() + "'.");
		}

		internal DescriptionText GetDescription(NetBrowserManager manager, NetTypeBrowserInfo type)
		{
			if (m_description == null)
			{
				m_description = GetDescriptionInternal(manager, type);
				Debug.Assert(m_description != null, "m_description != null");
			}

			return m_description;
		}

		private DescriptionText GetDescriptionInternal(NetBrowserManager manager, NetTypeBrowserInfo type)
		{
			DescriptionBuilder sb = new DescriptionBuilder(true);

			sb.Append(Settings.GetKeyword(GetMemberAccess(m_memberInfo)));
			sb.Append(" ");

			switch (m_memberInfo.MemberType)
			{
				case MemberTypes.Constructor:
					AppendConstructor(sb, manager, (ConstructorInfo)m_memberInfo);
					break;

				case MemberTypes.Method:
					AppendMethod(sb, manager, (MethodInfo)m_memberInfo);
					break;

				case MemberTypes.Property:
					AppendProperty(sb, manager, (PropertyInfo)m_memberInfo);
					break;

				case MemberTypes.Field:
					AppendField(sb, manager, (FieldInfo)m_memberInfo);
					break;

				case MemberTypes.Event:
					AppendEvent(sb, manager, (EventInfo)m_memberInfo);
					break;

				default:
					throw new ApplicationException("Unsupported member type: '" + m_memberInfo.MemberType.ToString() + "'.");
			}

			sb.EndFirstLine();
			sb.Append(@"     Member of ");
			sb.AppendLink(type.DisplayName, type);
			sb.EndLine();

			return sb.GetText();
		}

		private void AppendConstructor(DescriptionBuilder sb, NetBrowserManager manager, ConstructorInfo constructor)
		{
			AppendMethodAttribute(sb, constructor, MethodAttributes.Static);
			sb.AppendName(constructor.DeclaringType.Name);

			sb.Append(" ( ");
			AppendParameters(sb, manager, constructor.GetParameters());
			sb.Append(" )");
		}

		private void AppendMethod(DescriptionBuilder sb, NetBrowserManager manager, MethodInfo method)
		{
			AppendMethodAttributes(sb, method);

			NetTypeBrowserInfo.AppendParameterTypeDisplayName(manager, sb, method.ReturnType, false, true);
			sb.Append(" ");

			if (MethodIsOperator(method))
			{
				sb.AppendName(Settings.GetKeyword(method.Name));
			}
			else
			{
				sb.AppendName(method.Name);
			}

			sb.Append(" ( ");
			AppendParameters(sb, manager, method.GetParameters());
			sb.Append(" )");
		}

		private void AppendField(DescriptionBuilder sb, NetBrowserManager manager, FieldInfo field)
		{
			AppendFieldAttribute(sb, field, FieldAttributes.Static);
			AppendFieldAttribute(sb, field, FieldAttributes.InitOnly);
			AppendFieldAttribute(sb, field, FieldAttributes.Literal);

			NetTypeBrowserInfo.AppendParameterTypeDisplayName(manager, sb, field.FieldType, false, true);
			sb.Append(" ");

			sb.AppendName(field.Name);

			// Show values of constants, static readonly variables and enum identifiers.

			if (field.IsLiteral || (field.IsStatic && field.IsInitOnly))
			{
				sb.Append(" = ");
				sb.Append(GetConstantDisplayValue(field.GetValue(null), true));
			}
		}

		private void AppendProperty(DescriptionBuilder sb, NetBrowserManager manager, PropertyInfo property)
		{
			// Properties have many of the same attributes as methods, but these attributes are declared
			// on the accessors, not on the properties themselves.

			MethodInfo[] accessors = property.GetAccessors(true);
			Debug.Assert(accessors.Length > 0, "Unable to get the accessors for property '"
				+ property.DeclaringType.ToString() + "." + property.Name
				+ "' - this property should not have been displayed.");

			AppendMethodAttributes(sb, accessors[0]);

			NetTypeBrowserInfo.AppendParameterTypeDisplayName(manager, sb, property.PropertyType, false, true);
			sb.Append(" ");
			sb.AppendName(property.Name);

			if (property.CanRead)
			{
				if (property.CanWrite)
				{
					sb.Append(" [ get, set ]");
				}
				else
				{
					sb.Append(" [ get ]");
				}
			}
			else if (property.CanWrite)
			{
				sb.Append(" [ set ]");
			}
			else
			{
				Debug.Fail("Property '" + property.Name + "' is not readable and not writeable.");
			}

			// Show indexer parameters as well (which the VS.NET object browser doesn't do).

			ParameterInfo[] indexers = property.GetIndexParameters();

			if (indexers.Length > 0)
			{
				sb.Append(" ( ");
				AppendParameters(sb, manager, indexers);
				sb.Append(" )");
			}
		}

		private void AppendEvent(DescriptionBuilder sb, NetBrowserManager manager, EventInfo eventInfo)
		{
			sb.Append(Settings.GetKeyword(MemberTypes.Event));
			sb.Append(" ");

			NetTypeBrowserInfo.AppendParameterTypeDisplayName(manager, sb, eventInfo.EventHandlerType, false, true);
			sb.Append(" ");

			sb.AppendName(eventInfo.Name);
		}

		private void AppendMethodAttributes(DescriptionBuilder sb, MethodInfo method)
		{
			AppendMethodAttribute(sb, method, MethodAttributes.Static);
			AppendMethodAttribute(sb, method, MethodAttributes.PinvokeImpl);

			// Sorting out "virtual", "new" and "override" methods requires more complex logic.
			// The relevant MethodAttribute values are:
			//
			// override: Virtual
			// virtual: Virtual, NewSlot
			// abstract: Virtual, NewSlot, Abstract

			if ((method.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual)
			{
				if ((method.Attributes & MethodAttributes.NewSlot) == MethodAttributes.NewSlot)
				{
					if (!AppendMethodAttribute(sb, method, MethodAttributes.Abstract))
					{
						sb.Append(Settings.GetKeyword(MethodAttributes.Virtual));
						sb.Append(" ");
					}
				}
				else
				{
					sb.Append(Settings.GetKeyword(MethodAttributes.ReuseSlot));
					sb.Append(" ");
				}
			}
		}

		private bool AppendMethodAttribute(DescriptionBuilder sb, MethodBase method, MethodAttributes attribute)
		{
			if ((method.Attributes & attribute) == attribute)
			{
				sb.Append(Settings.GetKeyword(attribute));
				sb.Append(" ");

				return true;
			}
			else
				return false;
		}

		private void AppendFieldAttribute(DescriptionBuilder sb, FieldInfo field, FieldAttributes attribute)
		{
			if ((field.Attributes & attribute) == attribute)
			{
				sb.Append(Settings.GetKeyword(attribute));
				sb.Append(" ");
			}
		}

		private string GetConstantDisplayValue(object value, bool longFormat)
		{
			const int MaxLength = 100;

			if (value == null)
				return Settings.GetKeyword("null");

			TypeCode typeCode = System.Type.GetTypeCode(value.GetType());

			switch (typeCode)
			{
				case TypeCode.String:
					// If the string is too long truncate it and show ... at the end - and don't close the
					// quotes.

					string str = (string)value;
					return (str.Length > MaxLength ? "\"" + str.Substring(0, MaxLength) + "..." :
						"\"" + str + "\"");

				case TypeCode.Char:
					return (longFormat ? string.Format("\'{0}\' (0x{1:x4})", value, Convert.ToUInt32((char)value))
						: value.ToString());

				case TypeCode.Boolean:
					return Settings.GetKeyword(value);

				case TypeCode.Byte:
				case TypeCode.Decimal:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return (longFormat ? string.Format("{0:d} (0x{0:x})", value) : string.Format("{0:d}", value));

				case TypeCode.Single:
				case TypeCode.Double:
					return value.ToString();
			}

			if (value is Enum)
				return ((Enum)value).ToString("d");

			if (value is Array)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("{");

				bool first = true;
				foreach (object item in (Array)value)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						sb.Append(", ");
					}

					sb.Append(GetConstantDisplayValue(item, false));

					if (sb.Length > MaxLength)
						break;
				}

				if (sb.Length > MaxLength)
				{
					return sb.ToString(0, MaxLength) + "...";
				}
				else
				{
					sb.Append("}");
					return sb.ToString();
				}
			}

			return "{" + value.ToString() + "}";
		}
	}
}
