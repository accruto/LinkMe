using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Settings;
using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	public class NetBrowserSettings : ObjectBrowserSettings
	{
		internal const bool ObjectAccessSupportedValue = true;
		internal const string NullNamespaceName = "<null namespace>";

		internal const string KeywordArrayStart = "ArrayStart";
		internal const string KeywordArrayEnd = "ArrayEnd";
		internal const string KeywordAttributeStart = "AttributeStart";
		internal const string KeywordAttributeEnd = "AttributeEnd";
		internal const string KeywordNamedParameterEquals = "NamedParameterEquals";

		private static readonly Hashtable m_keywordsCSharp;

		private Language m_language;

		private System.Windows.Forms.ImageList imgTypeIcons;
		private System.Windows.Forms.ImageList imgMemberIcons;
		private System.ComponentModel.IContainer components;

		#region Constructors

		static NetBrowserSettings()
		{
			m_keywordsCSharp = new Hashtable();

			// Primitive types

			m_keywordsCSharp.Add(typeof(bool).AssemblyQualifiedName, "bool");
			m_keywordsCSharp.Add(typeof(byte).AssemblyQualifiedName, "byte");
			m_keywordsCSharp.Add(typeof(sbyte).AssemblyQualifiedName, "sbyte");
			m_keywordsCSharp.Add(typeof(short).AssemblyQualifiedName, "short");
			m_keywordsCSharp.Add(typeof(ushort).AssemblyQualifiedName, "ushort");
			m_keywordsCSharp.Add(typeof(int).AssemblyQualifiedName, "int");
			m_keywordsCSharp.Add(typeof(uint).AssemblyQualifiedName, "uint");
			m_keywordsCSharp.Add(typeof(long).AssemblyQualifiedName, "long");
			m_keywordsCSharp.Add(typeof(ulong).AssemblyQualifiedName, "ulong");
			m_keywordsCSharp.Add(typeof(char).AssemblyQualifiedName, "char");
			m_keywordsCSharp.Add(typeof(double).AssemblyQualifiedName, "double");
			m_keywordsCSharp.Add(typeof(float).AssemblyQualifiedName, "float");

			// Other types that have aliases

			m_keywordsCSharp.Add(typeof(object).AssemblyQualifiedName, "object");
			m_keywordsCSharp.Add(typeof(string).AssemblyQualifiedName, "string");
			m_keywordsCSharp.Add(typeof(decimal).AssemblyQualifiedName, "decimal");
			m_keywordsCSharp.Add(typeof(void).AssemblyQualifiedName, "void");

			// Access modifiers

			m_keywordsCSharp.Add(AccessModifiers.Public, "public");
			m_keywordsCSharp.Add(AccessModifiers.ProtectedInternal, "protected internal");
			m_keywordsCSharp.Add(AccessModifiers.Protected, "protected");
			m_keywordsCSharp.Add(AccessModifiers.Internal, "internal");
			m_keywordsCSharp.Add(AccessModifiers.Private, "private");

			// Object types

			m_keywordsCSharp.Add(ObjectType.Class, "class");
			m_keywordsCSharp.Add(ObjectType.Delegate, "delegate");
			m_keywordsCSharp.Add(ObjectType.Enum, "enum");
			m_keywordsCSharp.Add(ObjectType.Interface, "interface");
			m_keywordsCSharp.Add(ObjectType.Struct, "struct");

			// Type declaration

			m_keywordsCSharp.Add(TypeAttributes.Abstract, "abstract");
			m_keywordsCSharp.Add(TypeAttributes.Sealed, "sealed");

			// Member types

			m_keywordsCSharp.Add(MemberTypes.Event, "event");

			// Field declaration

			m_keywordsCSharp.Add(FieldAttributes.Literal, "const");
			m_keywordsCSharp.Add(FieldAttributes.InitOnly, "readonly");
			m_keywordsCSharp.Add(FieldAttributes.Static, "static");

			// Method declaration

			m_keywordsCSharp.Add(MethodAttributes.Abstract, "abstract");
			m_keywordsCSharp.Add(MethodAttributes.NewSlot, "new");
			m_keywordsCSharp.Add(MethodAttributes.ReuseSlot, "override");
			m_keywordsCSharp.Add(MethodAttributes.Static, "static");
			m_keywordsCSharp.Add(MethodAttributes.PinvokeImpl, "extern");
			m_keywordsCSharp.Add(MethodAttributes.Virtual, "virtual");

			// Parameter declaration

			m_keywordsCSharp.Add(ParameterAttributes.Out, "out");
			m_keywordsCSharp.Add("Type.IsByRef", "ref");
			m_keywordsCSharp.Add(KeywordArrayStart, "["); // Start of array declaration
			m_keywordsCSharp.Add(KeywordArrayEnd, "]"); // End of array declaration
			m_keywordsCSharp.Add(typeof(ParamArrayAttribute), "params");

			// Attributes

			m_keywordsCSharp.Add(KeywordAttributeStart, "[");
			m_keywordsCSharp.Add(KeywordAttributeEnd, "]");
			m_keywordsCSharp.Add(KeywordNamedParameterEquals, "=");

			// Values

			m_keywordsCSharp.Add("null", "null");
			m_keywordsCSharp.Add(true, "true");
			m_keywordsCSharp.Add(false, "false");

			// Operator declarations

			m_keywordsCSharp.Add("op_UnaryPlus", "operator +");
			m_keywordsCSharp.Add("op_UnaryNegation", "operator -");
			m_keywordsCSharp.Add("op_LogicalNot", "operator !");
			m_keywordsCSharp.Add("op_OnesComplement", "operator ~");
			m_keywordsCSharp.Add("op_Increment", "operator ++");
			m_keywordsCSharp.Add("op_Decrement", "operator --");
			m_keywordsCSharp.Add("op_True", "operator true");
			m_keywordsCSharp.Add("op_False", "operator false");

			m_keywordsCSharp.Add("op_Addition", "operator +");
			m_keywordsCSharp.Add("op_Subtraction", "operator -");
			m_keywordsCSharp.Add("op_Multiply", "operator *");
			m_keywordsCSharp.Add("op_Division", "operator /");
			m_keywordsCSharp.Add("op_Modulus", "operator %");
			m_keywordsCSharp.Add("op_BitwiseAnd", "operator &");
			m_keywordsCSharp.Add("op_BitwiseOr", "operator |");
			m_keywordsCSharp.Add("op_ExclusiveOr", "operator ^");
			m_keywordsCSharp.Add("op_LeftShift", "operator <<");
			m_keywordsCSharp.Add("op_RightShift", "operator >>");

			m_keywordsCSharp.Add("op_Equality", "operator ==");
			m_keywordsCSharp.Add("op_Inequality", "operator !=");
			m_keywordsCSharp.Add("op_LessThan", "operator <");
			m_keywordsCSharp.Add("op_GreaterThan", "operator >");
			m_keywordsCSharp.Add("op_LessThanOrEqual", "operator <=");
			m_keywordsCSharp.Add("op_GreaterThanOrEqual", "operator >=");

			m_keywordsCSharp.Add("op_Explicit", "operator explicit");
			m_keywordsCSharp.Add("op_Implicit", "operator implicit");

		}

		public NetBrowserSettings()
			: this(Language.CSharp)
		{
		}

		public NetBrowserSettings(Language language)
			: this(true, false, language)
		{
		}

		public NetBrowserSettings(bool showMembers, bool showTypeCheckBoxes, Language language)
			: base(showMembers, showTypeCheckBoxes)
		{
			InitializeComponent();

			m_language = language;
		}

		#endregion

		public override bool ObjectAccessSupported
		{
			get { return ObjectAccessSupportedValue; }
		}

		/// <summary>
		/// The programming language used to generate type and member descriptions. The default is CSharp.
		/// </summary>
		[Browsable(false)]
		public Language Language
		{
			get { return m_language; }
		}

		protected internal override ImageList TypeIcons
		{
			get { return imgTypeIcons; }
		}

		protected internal override ImageList MemberIcons
		{
			get { return imgMemberIcons; }
		}

		public override object Clone()
		{
			NetBrowserSettings cloned = new NetBrowserSettings();
			cloned.CloneFrom(this);
			return cloned;
		}

		public override bool SettingsEqual(ISettingsObject obj)
		{
			if (!base.SettingsEqual(obj))
				return false;

			NetBrowserSettings other = obj as NetBrowserSettings;
			if (other == null)
				return false;

			return (Language == other.Language);
		}

		protected override void CloneFrom(ObjectBrowserSettings source)
		{
			base.CloneFrom(source);

			m_language = ((NetBrowserSettings)source).m_language;
		}

		internal string GetKeyword(object key)
		{
			Debug.Assert(key != null, "key != null");

			switch (m_language)
			{
				case Language.CSharp:
					return (string)m_keywordsCSharp[key];

				default:
					throw new ApplicationException("Unsupported language: '" + m_language.ToString() + "'.");
			}
		}

		internal bool TypeShouldBeVisible(Type type)
		{
			if (ShowNonPublic)
				return true;

			AccessModifiers effectiveAccess = NetTypeBrowserInfo.GetEffectiveTypeAccess(type);
			return (effectiveAccess != AccessModifiers.Private && effectiveAccess != AccessModifiers.Internal);
		}

		internal bool MemberShouldBeVisible(MemberInfo member)
		{
			if (ShowNonPublic)
				return true;

			AccessModifiers effectiveAccess = NetMemberBrowserInfo.GetMemberAccess(member);

			switch (effectiveAccess)
			{
				case AccessModifiers.Public:
				case AccessModifiers.Protected:
				case AccessModifiers.ProtectedInternal:
					return true;

				case AccessModifiers.Internal:
				case AccessModifiers.Private:
				case AccessModifiers.Unknown:
					return false;

				default:
					Debug.Fail("Unexpected AccessModifiers value: " + effectiveAccess.ToString());
					return false;
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NetBrowserSettings));
			this.imgTypeIcons = new System.Windows.Forms.ImageList(this.components);
			this.imgMemberIcons = new System.Windows.Forms.ImageList(this.components);
			// 
			// imgTypeIcons
			// 
			this.imgTypeIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgTypeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTypeIcons.ImageStream")));
			this.imgTypeIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imgMemberIcons
			// 
			this.imgMemberIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgMemberIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMemberIcons.ImageStream")));
			this.imgMemberIcons.TransparentColor = System.Drawing.Color.Transparent;

		}
	}
}
