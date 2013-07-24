using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

using LinkMe.Framework.Tools.ObjectBrowser;

using TYPEFLAGS = System.Runtime.InteropServices.ComTypes.TYPEFLAGS;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	public class ComBrowserSettings : ObjectBrowserSettings
	{
		private const bool ObjectAccessSupportedValue = false;

		private static readonly Hashtable m_keywords;

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList imgMemberIcons;
		private System.Windows.Forms.ImageList imgTypeIcons;

		#region Constructors

		static ComBrowserSettings()
		{
			m_keywords = new Hashtable();

			// Primitive types

			m_keywords.Add(VarEnum.VT_BOOL, "VARIANT_BOOL");
			m_keywords.Add(VarEnum.VT_UI1, "unsigned char");
			m_keywords.Add(VarEnum.VT_I1, "char");
			m_keywords.Add(VarEnum.VT_I2, "short");
			m_keywords.Add(VarEnum.VT_UI2, "unsigned short");
			m_keywords.Add(VarEnum.VT_I4, "int");
			m_keywords.Add(VarEnum.VT_INT, "int");
			m_keywords.Add(VarEnum.VT_UI4, "unsigned int");
			m_keywords.Add(VarEnum.VT_UINT, "unsigned int");
			m_keywords.Add(VarEnum.VT_I8, "int64");
			m_keywords.Add(VarEnum.VT_UI8, "uint64");
			m_keywords.Add(VarEnum.VT_R8, "double");
			m_keywords.Add(VarEnum.VT_R4, "float");
			m_keywords.Add(VarEnum.VT_BSTR, "BSTR");
			m_keywords.Add(VarEnum.VT_VOID, "void");
			m_keywords.Add(VarEnum.VT_HRESULT, "HRESULT");
			m_keywords.Add(VarEnum.VT_UNKNOWN, "IUnknown*");
			m_keywords.Add(VarEnum.VT_DISPATCH, "IDispatch*");
			m_keywords.Add(VarEnum.VT_CLSID, "CLSID");
			m_keywords.Add(VarEnum.VT_CY, "CURRENCY");
			m_keywords.Add(VarEnum.VT_DATE, "DATE");
			m_keywords.Add(VarEnum.VT_DECIMAL, "DECIMAL");
			m_keywords.Add(VarEnum.VT_FILETIME, "FILETIME");
			m_keywords.Add(VarEnum.VT_VARIANT, "VARIANT");
			m_keywords.Add(VarEnum.VT_ERROR, "SCODE");
			m_keywords.Add(VarEnum.VT_LPSTR, "LPSTR");
			m_keywords.Add(VarEnum.VT_LPWSTR, "LPWSTR");
			m_keywords.Add(VarEnum.VT_SAFEARRAY, "SAFEARRAY");
			m_keywords.Add(VarEnum.VT_ARRAY, "SAFEARRAY");

			// Other types that have aliases

			m_keywords.Add(typeof(string).AssemblyQualifiedName, "BSTR");
			m_keywords.Add(typeof(decimal).AssemblyQualifiedName, "DECIMAL");
			m_keywords.Add(typeof(void).AssemblyQualifiedName, "void");

			// Object types

			m_keywords.Add(ObjectType.Class, "coclass");
			m_keywords.Add(ObjectType.Enum, "enum");
			// Interface is listed below because the keywords are different depending on the kind of interface.
			m_keywords.Add(ObjectType.Struct, "struct");
			m_keywords.Add(ObjectType.Union, "union");

			// Interface types

			m_keywords.Add(ComInterfaceType.InterfaceIsDual, "dual interface");
			m_keywords.Add(ComInterfaceType.InterfaceIsIDispatch, "dispinterface");
			m_keywords.Add(ComInterfaceType.InterfaceIsIUnknown, "interface");

			// Type attributes

			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FAGGREGATABLE, "aggregatable");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FAPPOBJECT, "appobject");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FCONTROL, "control");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FHIDDEN, "hidden");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FLICENSED, "licensed");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FNONEXTENSIBLE, "nonextensible");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION, "oleautomation");
			m_keywords.Add(TYPEFLAGS.TYPEFLAG_FRESTRICTED, "restricted");

			// Parameter declaration

			m_keywords.Add(ParameterAttributes.In, "in");
			m_keywords.Add(ParameterAttributes.Out, "out");
			m_keywords.Add(ParameterAttributes.Retval, "retval");
			m_keywords.Add("Type.IsByRef", "[in, out]");
			m_keywords.Add("[", "["); // Start of array declaration
			m_keywords.Add("]", "]"); // End of array declaration

			// Values

			m_keywords.Add("null", "NULL");
			m_keywords.Add(true, "true");
			m_keywords.Add(false, "false");
		}

		public ComBrowserSettings()
		{
			InitializeComponent();
		}

		#endregion

		public override bool ObjectAccessSupported
		{
			get { return ObjectAccessSupportedValue; }
		}

		protected internal override ImageList TypeIcons
		{
			get { return imgTypeIcons; }
		}

		protected internal override ImageList MemberIcons
		{
			get { return imgMemberIcons; }
		}

		#region Static Methods

		internal static string GetKeyword(object key)
		{
			return (string)m_keywords[key];
		}

		#endregion

		public override object Clone()
		{
			ComBrowserSettings cloned = new ComBrowserSettings();
			cloned.CloneFrom(this);
			return cloned;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ComBrowserSettings));
			this.imgMemberIcons = new System.Windows.Forms.ImageList(this.components);
			this.imgTypeIcons = new System.Windows.Forms.ImageList(this.components);
			// 
			// imgMemberIcons
			// 
			this.imgMemberIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgMemberIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMemberIcons.ImageStream")));
			this.imgMemberIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imgTypeIcons
			// 
			this.imgTypeIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgTypeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTypeIcons.ImageStream")));
			this.imgTypeIcons.TransparentColor = System.Drawing.Color.Transparent;
		}
	}
}
