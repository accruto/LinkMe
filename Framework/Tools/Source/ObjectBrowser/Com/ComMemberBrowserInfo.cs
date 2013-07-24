using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	/// <summary>
	/// An IMemberBrowserInfo implementation that provides access to COM type members.
	/// </summary>
	public class ComMemberBrowserInfo : ElementBrowserInfo, IMemberBrowserInfo
	{
		private const int ImageIndexEnumIdentifier = 0;

		private ComTypeBrowserInfo m_type;
		private MemberDesc m_member;
		private string m_nodeText;
		private DescriptionText m_description = null;

		internal ComMemberBrowserInfo()
		{
		}

		internal ComMemberBrowserInfo(ComTypeBrowserInfo type, MemberDesc member, string nodeText)
		{
			Debug.Assert(type != null && nodeText != null,
				"type != null && && nodeText != null");

			m_type = type;
			m_member = member;
			m_nodeText = nodeText;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is ComMemberBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			ComMemberBrowserInfo other = (ComMemberBrowserInfo)obj;

			if (other == this)
				return 0;

			switch (Manager.Settings.MemberOrder)
			{
				case MemberOrder.Alphabetical:
					// Compare names
					int result1 = CompareMemberNames(m_member, other.m_member);
					if (result1 != 0)
						return result1;

					// Compare member types
					return GetMemberTypePriority(m_member).CompareTo(
						GetMemberTypePriority(other.m_member));

				case MemberOrder.MemberType:
					// Compare member types
					int result2 = GetMemberTypePriority(m_member).CompareTo(
						GetMemberTypePriority(other.m_member));
					if (result2 != 0)
						return result2;

					// Compare names
					return CompareMemberNames(m_member, other.m_member);

				default:
					throw new ApplicationException("Unsupported MemberOrder enum value: '"
						+ Manager.Settings.MemberOrder.ToString() + "'.");
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
				switch (m_member.MemberType)
				{
					case MemberTypes.Method:
						return 1;

					case MemberTypes.Property:
						return 2;

					case MemberTypes.Field:
						return (m_type.ObjectType == ObjectType.Enum ? 0 : 3);

					case MemberTypes.Event:
						return 4;

					default:
						throw new ApplicationException("Unhandled member type: " + m_member.MemberType.ToString());
				}
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

		private static int GetMemberTypePriority(MemberDesc member)
		{
			// Order of priority: method, property, field, event.

			switch (member.MemberType)
			{
				case MemberTypes.Method:
					return 1;
				case MemberTypes.Property:
					return 2;
				case MemberTypes.Field:
					return 3;
				case MemberTypes.Event:
					return 4;
				default:
					Debug.Fail("Unsupported member type: '" + member.MemberType.ToString() + "'.");
					return 0;
			}
		}

		private static int CompareMemberNames(MemberDesc a, MemberDesc b)
		{
			// Compare the names with case-insensitive comparison first (for ordering purposes)

			string nameA = a.Name;
			string nameB = b.Name;

			int result = string.Compare(nameA, nameB, true);
			if (result != 0)
				return result;

			// Now try case-sensitive comparison

			result = string.Compare(nameA, nameB, false);
			if (result != 0)
				return result;

			// Names are the same - must be methods or properties with different signatures

			Debug.Assert(a.MemberType == MemberTypes.Method || a.MemberType == MemberTypes.Property,
				"a.MemberType == MemberTypes.Method || a.MemberType == MemberTypes.Property");
			return string.Compare(a.ToString(), b.ToString(), true);
		}

		private static void AppendField(DescriptionBuilder sb, FieldDesc field)
		{
			sb.Append(field.Type);
			sb.Append(" ");

			sb.AppendName(field.Name);

			if (field.Value != null)
			{
				sb.Append(" = ");
				sb.Append(field.Value);
			}
		}

		private static void AppendParameters(DescriptionBuilder sb, ParameterDesc[] parameters)
		{
			bool first = true;
			foreach (ParameterDesc parameter in parameters)
			{
				if ( parameter != null )
				{
					if (first)
					{
						first = false;
					}
					else
					{
						sb.Append(", ");
					}

					ComTypeBrowserInfo.WriteParameterAttributes(sb, parameter);

					sb.Append(parameter.Type);
					sb.Append(" ");

					sb.Append(parameter.Name);
				}
			}
		}

		private static void AppendMethod(DescriptionBuilder sb, MethodDesc method)
		{
			if (method.ReturnType != null)
			{
				sb.Append(method.ReturnType);
				sb.Append(" ");
			}

			sb.AppendName(method.Name);

			sb.Append(" ( ");
			AppendParameters(sb, method.Parameters);
			sb.Append(" )");
		}

		private static void AppendProperty(DescriptionBuilder sb, PropertyDesc property)
		{
			if (property.ReturnType != null)
			{
				sb.Append(property.ReturnType);
				sb.Append(" ");
			}

			sb.AppendName(property.Name);
			sb.Append(ComTypeBrowserInfo.GetPropertyKind(property));

			// Show indexer parameters as well.

			ParameterDesc[] parameters = property.Parameters;

			if (parameters.Length > 0)
			{
				sb.Append(" ( ");
				AppendParameters(sb, parameters);
				sb.Append(" )");
			}
		}

		#endregion

		public virtual string Name
		{
			get { return m_member.Name; }
		}

		public virtual OperationMemberType MemberType
		{
			get
			{
				switch (m_member.MemberType)
				{
					case MemberTypes.Method:
						return OperationMemberType.Method;

					case MemberTypes.Property:
						return (((PropertyDesc)m_member).CanGet ?
							OperationMemberType.PropertyGet : OperationMemberType.PropertySet);

					default:
						return OperationMemberType.None;
				}
			}
		}

		internal MemberDesc MemberDesc
		{
			get { return m_member; }
		}

		private ComBrowserManager Manager
		{
			get { return m_type.Manager; }
		}

		private DescriptionText GetDescription()
		{
			DescriptionBuilder sb = new DescriptionBuilder(true);

			switch (m_member.MemberType)
			{
				case MemberTypes.Method:
				case MemberTypes.Event:
					AppendMethod(sb, (MethodDesc)m_member);
					break;

				case MemberTypes.Property:
					AppendProperty(sb, (PropertyDesc)m_member);
					break;

				case MemberTypes.Field:
					AppendField(sb, (FieldDesc)m_member);
					break;

				default:
					throw new ApplicationException("Unsupported member type: '" + m_member.MemberType.ToString() + "'.");
			}

			sb.EndFirstLine();
			sb.Append(@"     Member of ");
			sb.AppendLink(m_type.DisplayName, m_type);
			sb.EndLine();

			return sb.GetText();
		}
	}
}
