using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using LinkMe.Framework.Type;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Configuration
{
	[Serializable]
	public sealed class CatalogueName
		:	ICloneable,
			IComparable,
			IInternable,
			IBinarySerializable
	{
		public CatalogueName(string reference)
		{
			const string method = ".ctor";

			if ( reference == null )
				throw new NullParameterException(typeof(CatalogueName), method, "reference");

			// Use a capturing regex to extract the name, namespace and version.

			EnsureRegexes();

			Match match = _capturingQualifiedReferenceRegex.Match(reference);
			if ( !match.Success )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "reference", reference, Constants.Validation.CompleteQualifiedReferencePattern);

			_name = match.Groups["Name"].Value;
			_namespace = match.Groups["Namespace"].Value;
			Group versionGroup = match.Groups["Version"];
			_version = (versionGroup.Success ? new Version(versionGroup.Value) : null);
		}

		public CatalogueName(string ns, string name, Version version)
		{
			const string method = ".ctor";

			// Check namespace.

			if ( ns == null )
				ns = string.Empty;
			else if ( ns.Length != 0 && !IsFullName(ns) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "ns", ns, Constants.Validation.CompleteFullNamePattern);

			// Check name.

			if ( name == null )
				throw new NullParameterException(typeof(CatalogueName), method, "name");
			if ( !IsName(name) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "name", name, Constants.Validation.CompleteNamePattern);

			// Assign.

			_namespace = ns;
			_name = name;
			_version = (version == null) ? null : (Version) version.Clone();
		}

		public CatalogueName(string ns, string name)
			:	this(ns, name, null)
		{
		}

		public CatalogueName(System.Type type)
			:	this(type.Namespace, type.Name, type.Assembly.GetName().Version)
		{
		}

		public CatalogueName(PrimitiveType type)
			:	this(PrimitiveTypeInfo.GetPrimitiveTypeInfo(type).FullName)
		{
		}

		/// <summary>
		/// Private constructor for cloning - performs no checks.
		/// </summary>
		private CatalogueName(Version version, string name, string ns)
		{
			_namespace = ns;
			_name = name;
			_version = version;
		}

		#region ICloneable Members

		public CatalogueName Clone()
		{
			return new CatalogueName(_version, _name, _namespace);
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

		#region IComparable Members

		public int CompareTo(CatalogueName other)
		{
			int result = _namespace.CompareTo(other._namespace);
			if ( result != 0 )
				return result;
			result = _name.CompareTo(other._name);
			if ( result != 0 )
				return result;
			if ( _version == null )
				return other._version == null ? 0 : -1;
			return other._version == null ? 1 : _version.CompareTo(other._version);
		}

		int IComparable.CompareTo(object other)
		{
			const string method = "CompareTo";
			if ( !(other is CatalogueName) )
				throw new InvalidParameterTypeException(typeof(CatalogueName), method, "other", typeof(CatalogueName), other);
			return CompareTo((CatalogueName) other);
		}

		#endregion

		#region IInternable Members

		void IInternable.Intern(Interner interner)
		{
			_name = interner.Intern(_name);
			_namespace = interner.Intern(_namespace);
		}

		#endregion

		#region IBinarySerializable Members

		public void Write(BinaryWriter writer)
		{
			writer.Write(_namespace);
			writer.Write(_name);
			writer.Write(_version == null ? string.Empty : _version.ToString());
		}

		public void Read(BinaryReader reader)
		{
			_namespace = reader.ReadString();
			_name = reader.ReadString();
			string version = reader.ReadString();
			_version = version.Length == 0 ? null : new Version(version);
		}

		#endregion

		#region System.Object Members

		public override string ToString()
		{
			return FullyQualifiedReference;
		}

		public override bool Equals(object other)
		{
			var otherName = other as CatalogueName;
			if ( otherName == null )
				return false;

			return _namespace == otherName._namespace
				&& _name == otherName._name
				&& Equals(_version, otherName._version);
		}

		public static bool operator==(CatalogueName name1, CatalogueName name2)
		{
			return Equals(name1, name2);
		}

		public static bool operator!=(CatalogueName name1, CatalogueName name2)
		{
			return !Equals(name1, name2);
		}

		public override int GetHashCode()
		{
			return _namespace.GetHashCode()
				^ _name.GetHashCode()
				^ (_version == null ? 0 : _version.GetHashCode());
		}

		#endregion

		#region Properties

		public string Namespace
		{
			get { return _namespace; }
			set 
			{ 
				const string method = "set_Namespace";

				if ( value == null )
					value = string.Empty;
				else if ( value.Length != 0 && !IsFullName(value) )
					throw new InvalidParameterFormatException(typeof(CatalogueName), method, "value", value, Constants.Validation.CompleteFullNamePattern);

				_namespace = value;
			}
		}

		public string Name
		{
			get { return _name; }
			set 
			{
				const string method = "set_Name";

				if ( value == null )
					throw new NullParameterException(typeof(CatalogueName), method, "name");
				if ( !IsName(value) )
					throw new InvalidParameterFormatException(typeof(CatalogueName), method, "name", value, Constants.Validation.CompleteNamePattern);

				_name = value;
			}
		}

		public Version Version
		{
			get { return _version == null ? null : (Version) _version.Clone(); }
			set { _version = (value == null) ? null : (Version) value.Clone(); }
		}

		public string FullName
		{
			get { return GetFullNameUnchecked(_namespace, _name); }
		}

		public string FullyQualifiedReference
		{
			get { return GetQualifiedReferenceUnchecked(_namespace, _name, _version); }
		}

		public string RelativeQualifiedReference
		{
			get { return GetQualifiedReferenceUnchecked(string.Empty, _name, _version); }
		}

		public string XsiType
		{
			get { return (_version == null ? FullName : FullName + "-" + _version); }
		}

		#endregion

		public static string Combine(string fullName, string relativeQualifiedReference)
		{
			const string method = "Combine";

			// Check.

			if ( fullName == null )
				throw new NullParameterException(typeof(CatalogueName), method, "fullName");
			if ( !IsFullName(fullName) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "fullName", fullName, Constants.Validation.CompleteFullNamePattern);

			if ( relativeQualifiedReference == null )
				throw new NullParameterException(typeof(CatalogueName), method, "relativeQualifiedReference");
			if ( !IsQualifiedReference(relativeQualifiedReference) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "relativeQualifiedReference", relativeQualifiedReference, Constants.Validation.CompleteQualifiedReferencePattern);

			return fullName + "." + relativeQualifiedReference;
		}

		public static void SplitRootName(string fullName, out string rootName, out string relativeName)
		{
			const string method = "SplitRootName";

			if ( fullName == null )
				throw new NullParameterException(typeof(CatalogueName), method, "fullName");
			if ( !IsFullName(fullName) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "fullName", fullName, Constants.Validation.CompleteFullNamePattern);

			int pos = fullName.IndexOf('.');
			if ( pos == -1 )
			{
				rootName = fullName;
				relativeName = string.Empty;
			}
			else
			{
				rootName = fullName.Substring(0, pos);
				relativeName = fullName.Substring(pos + 1);
			}		
		}

		public static string GetNameFromFullName(string fullName)
		{
			const string method = "GetNameFromFullName";

			if ( fullName == null )
				throw new NullParameterException(typeof(CatalogueName), method, "fullName");

			EnsureRegexes();

			Match match = _capturingFullNameRegex.Match(fullName);
			if (!match.Success)
			{
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "fullName", fullName,
					Constants.Validation.CompleteCapturingFullNamePattern);
			}

			return match.Groups["Name"].Value;
		}

		public static string GetNameFromFullNameUnchecked(string fullName)
		{
			const string method = "GetNameFromFullNameUnchecked";

			if ( fullName == null )
				throw new NullParameterException(typeof(CatalogueName), method, "fullName");

			int index = fullName.LastIndexOf('.');
			return (index == -1 ? fullName : fullName.Substring(index + 1));
		}

		public static string GetFullName(string ns, string name)
		{
			const string method = "GetFullName";

			// Check namespace.

			if ( ns == null )
				ns = string.Empty;
			else if ( ns.Length != 0 && !IsFullName(ns) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "ns", ns, Constants.Validation.CompleteFullNamePattern);

			// Check name.

			if ( name == null )
				throw new NullParameterException(typeof(CatalogueName), method, "name");
			if ( !IsName(name) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "name", name, Constants.Validation.CompleteNamePattern);

			return GetFullNameUnchecked(ns, name);
		}

		public static string GetQualifiedReference(string ns, string name, Version version)
		{
			const string method = "GetQualifiedReference";

			// Check namespace.

			if ( ns == null )
				ns = string.Empty;
			else if ( ns.Length != 0 && !IsFullName(ns) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "ns", ns, Constants.Validation.CompleteFullNamePattern);

			// Check name.

			if ( name == null )
				throw new NullParameterException(typeof(CatalogueName), method, "name");
			if ( !IsName(name) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "name", name, Constants.Validation.CompleteNamePattern);

			return GetQualifiedReferenceUnchecked(ns, name, version);
		}

		public static string GetQualifiedReference(string name, Version version)
		{
			const string method = "GetQualifiedReference";

			// Check name.

			if ( name == null )
				throw new NullParameterException(typeof(CatalogueName), method, "name");
			if ( !IsName(name) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "name", name, Constants.Validation.CompleteNamePattern);

			return GetQualifiedReferenceUnchecked(name, version);
		}

		public static string GetQualifiedReference(string fullName, string relativeQualifiedReference)
		{
			const string method = "GetQualifiedReference";

			// Check full name.

			if ( fullName == null )
				fullName = string.Empty;
			else if ( fullName.Length != 0 && !IsFullName(fullName) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "fullName", fullName, Constants.Validation.CompleteFullNamePattern);

			// Check relative.

			if ( relativeQualifiedReference == null )
				throw new NullParameterException(typeof(CatalogueName), method, "relativeQualifiedReference");
			if ( !IsQualifiedReference(relativeQualifiedReference) )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "relativeQualifiedReference", relativeQualifiedReference, Constants.Validation.CompleteQualifiedReferencePattern);

			return GetFullNameUnchecked(fullName, relativeQualifiedReference);
		}

		public static string GetQualifiedReference(System.Type type)
		{
			return GetQualifiedReferenceUnchecked(type.Namespace, type.Name, type.Assembly.GetName().Version);
		}

		/// <summary>
		/// Create an instance of the class from a fully qualified reference without checking that it is valid.
		/// </summary>
		/// <remarks>This method does not validate the input values. You can use it to improve performance when
		/// the values are known to be valid.</remarks>
		public static CatalogueName CreateUnchecked(string ns, string name, Version version)
		{
			Debug.Assert(ns != null && name != null && version != null, "ns != null && name != null && version != null");
			return new CatalogueName(version, name, ns);
		}

		/// <summary>
		/// Create an instance of the class from a fully qualified reference without checking that it is valid.
		/// </summary>
		/// <param name="reference">The fully qualified reference.</param>
		/// <returns>A new CatalogueName object for the fully qualified reference.</returns>
		/// <remarks>This method does not validate the input string. You can use it to improve performance when
		/// the fully qualified reference is known to be valid.</remarks>
		public static CatalogueName CreateUnchecked(string reference)
		{
		    string name;
			string ns;

			// Look for the version.

			int posComma = reference.IndexOf(',');
			Version version = posComma == -1 ? null : new Version(reference.Substring(posComma + 10));

			// Look for a namespace.

			int posDot = reference.LastIndexOf('.', posComma == -1 ? reference.Length - 1 : posComma);
			if ( posDot == -1 )
			{
				ns = string.Empty;
				name = (posComma == -1 ? reference : reference.Substring(0, posComma));
			}
			else
			{
			    ns = reference.Substring(0, posDot);
			    name = posComma == -1 ? reference.Substring(posDot + 1) : reference.Substring(posDot + 1, posComma - posDot - 1);
			}

		    return new CatalogueName(version, name, ns);
		}

		public static CatalogueName FromXsiType(string xsiType)
		{
			const string method = "FromXsiType";

			if (xsiType == null)
				throw new NullParameterException(typeof(CatalogueName), method, "xsiType");

			// Use a capturing regex to extract the name, namespace and version.

			EnsureRegexes();

			Match match = _capturingXsiTypeRegex.Match(xsiType);
			if ( !match.Success )
				throw new InvalidParameterFormatException(typeof(CatalogueName), method, "xsiType", xsiType, Constants.Validation.CompleteXsiTypePattern);

			string name = match.Groups["Name"].Value;
			string ns  = match.Groups["Namespace"].Value;
			Group versionGroup = match.Groups["Version"];
			var version = (versionGroup.Success ? new Version(versionGroup.Value) : null);

			return new CatalogueName(version, name, ns);
		}

		public static string ToXsiType(CatalogueName catalogueName)
		{
			const string method = "ToXsiType";
			if ( catalogueName == null )
				throw new NullParameterException(typeof(CatalogueName), method, "catalogueName");

			string xsiType = catalogueName.FullName;
			if ( catalogueName.Version != null )
				xsiType += "-" + catalogueName.Version;
			return xsiType;
		}

		public static CatalogueName ReadXsiTypeAttribute(XmlReadAdaptor adaptor, bool mandatory)
		{
			const string method = "ReadEntityXsiType";

			string typeName = adaptor.ReadAttributeString(Constants.Xsi.TypeAttribute,
				Constants.Xsi.Namespace, mandatory);
			if (typeName == null)
			{
				Debug.Assert(!mandatory, "!mandatory");
				return null;
			}

			if (!mandatory && typeName.Length == 0)
				return null;

			int index = typeName.IndexOf(':');
			if (index == -1)
				return FromXsiType(typeName); // No XML prefix - just parse as a CatalogueName.

			// Get the prefix and look up the namespace.

			string prefix = typeName.Substring(0, index);
			string ns = adaptor.LookupNamespace(prefix);
			if (ns == null)
				throw new XmlPrefixNotFoundException(typeof(CatalogueName), method, prefix);

			// Replace the prefix with the namespace and parse as a CatalogueName.

			return FromXsiType(ns + "." + typeName.Substring(index + 1));
		}

		public static CatalogueName ReadXsiTypeAttribute(XmlAttribute attribute, bool mandatory)
		{
			const string method = "ReadEntityXsiType";
			if ( attribute == null )
				throw new NullParameterException(typeof(CatalogueName), method, "attribute");

			string typeName = attribute.Value;
			if ( !mandatory && typeName.Length == 0 )
				return null;

			int index = typeName.IndexOf(':');
			if ( index == -1 )
				return FromXsiType(typeName); // No XML prefix - just parse as a CatalogueName.

			// Get the prefix and look up the namespace.

			string prefix = typeName.Substring(0, index);
			string ns = attribute.OwnerElement.GetNamespaceOfPrefix(prefix);
			if ( ns == null )
				throw new XmlPrefixNotFoundException(typeof(CatalogueName), method, prefix);

			// Replace the prefix with the namespace and parse as a CatalogueName.

			return FromXsiType(ns + "." + typeName.Substring(index + 1));
		}

        public static string GetQualifiedReferenceUnchecked(string ns, string name)
        {
            return GetQualifiedReferenceUnchecked(ns, name, (string)null);
        }

        public static string GetQualifiedReferenceUnchecked(string ns, string name, Version version)
		{
			return GetQualifiedReferenceUnchecked(ns, name, version == null ? null : version.ToString());
		}

		public static string GetQualifiedReferenceUnchecked(string ns, string name, string version)
		{
			return (string.IsNullOrEmpty(version) ? GetFullNameUnchecked(ns, name) :
				GetFullNameUnchecked(ns, name) + ", Version=" + version);
		}

		public static string GetQualifiedReferenceUnchecked(string name, Version version)
		{
			return (version == null ? name : name + ", Version=" + version);
		}

		public static string GetFullNameUnchecked(string ns, string name)
		{
			return (ns.Length == 0 ? name : ns + "." + name);
		}

		#region Validation static methods

		// IsName() is by far the most commonly called validation method, so it's implemented directly, which
		// is about 10 times faster than the equivalent regular expression.
		public static bool IsName(string text)
		{
			const string method = "IsName";

			if ( text == null )
				throw new NullParameterException(typeof(CatalogueName), method, "text");

			if (text.Length == 0)
				return false;

			char first = text[0];
			if (!((first >= 'A' && first <= 'Z') || (first >= 'a' && first <= 'z')))
				return false;

			for (int index = 1; index < text.Length; index++)
			{
				char c = text[index];

                if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '_' || c == '-'))
					return false;
			}

			return true;
		}

		public static bool IsDisplayName(string text)
		{
			const string method = "IsDisplayName";

			if ( text == null )
				throw new NullParameterException(typeof(CatalogueName), method, "text");

			EnsureRegexes();
			return _displayNameRegex.IsMatch(text);
		}

		public static bool IsFullName(string text)
		{
			const string method = "IsFullName";

			if ( text == null )
				throw new NullParameterException(typeof(CatalogueName), method, "text");

			EnsureRegexes();
			return _fullNameRegex.IsMatch(text);
		}

		public static bool IsQualifiedReference(string text)
		{
			const string method = "IsQualifiedReference";

			if ( text == null )
				throw new NullParameterException(typeof(CatalogueName), method, "text");

			EnsureRegexes();
			return _qualifiedReferenceRegex.IsMatch(text);
		}

		#endregion

		private static void EnsureRegexes()
		{
			if ( _fullNameRegex == null )
			{
				lock ( typeof(CatalogueName) )
				{
					if ( _fullNameRegex == null )
					{
						_fullNameRegex = new Regex(Constants.Validation.CompleteFullNamePattern, RegexOptions.Compiled);
						_displayNameRegex = new Regex(Constants.Validation.CompleteDisplayNamePattern, RegexOptions.Compiled);
						_qualifiedReferenceRegex = new Regex(Constants.Validation.CompleteQualifiedReferencePattern, RegexOptions.Compiled);
						_capturingFullNameRegex = new Regex(Constants.Validation.CapturingFullNamePattern, RegexOptions.Compiled);
						_capturingQualifiedReferenceRegex = new Regex(Constants.Validation.CompleteCapturingQualifiedReferencePattern, RegexOptions.Compiled);
						_capturingXsiTypeRegex = new Regex(Constants.Validation.CompleteCapturingXsiTypePattern, RegexOptions.Compiled);
					}
				}
			}
		}

		public void WriteXsiTypeAttribute(XmlWriteAdaptor adaptor)
		{
			const string method = "WriteXsiTypeAttribute";

			if (adaptor == null)
				throw new NullParameterException(GetType(), method, "adaptor");

			string typeName;
			if (_namespace.Length > 0)
			{
				// Find the existing prefix for this namespace or write a new namespace declaration.

				string prefix = adaptor.LookupPrefix(_namespace);

				if (prefix == null)
				{
					prefix = GetNameFromFullNameUnchecked(_namespace).ToLower();
					adaptor.WriteNamespace(prefix, _namespace);
				}

				typeName = prefix + ":" + _name;
			}
			else
			{
				typeName = _name;
			}

			// Append the version with a "-", which is valid in a QName.

			if (_version != null)
			{
				typeName += "-" + _version;
			}

			// Write xsi:type.

			adaptor.WriteAttribute(Constants.Xsi.TypeAttribute, Constants.Xsi.Prefix,
				Constants.Xsi.Namespace, typeName);
		}

		public void WriteXsiTypeAttribute(XmlAttribute attribute)
		{
			const string method = "WriteXsiTypeAttribute";
			if ( attribute == null )
				throw new NullParameterException(GetType(), method, "attribute");

			string typeName;
			if ( _namespace.Length > 0 )
			{
				// Find the existing prefix for this namespace or write a new namespace declaration.

				string prefix = attribute.OwnerElement.GetPrefixOfNamespace(_namespace);
				if ( prefix == null )
				{
					prefix = GetNameFromFullNameUnchecked(_namespace).ToLower();
					attribute.OwnerElement.AppendChild(attribute.OwnerDocument.CreateAttribute(Constants.Xmlns.Prefix, prefix, Constants.Xmlns.Namespace));
				}

				typeName = prefix + ":" + _name;
			}
			else
			{
				typeName = _name;
			}

			// Append the version with a "-", which is valid in a QName.

			if ( _version != null )
				typeName += "-" + _version;

			// Write xsi:type.

			attribute.Value = typeName;
		}

		private static Regex _fullNameRegex;
		private static Regex _displayNameRegex;
		private static Regex _qualifiedReferenceRegex;
		private static Regex _capturingFullNameRegex;
		private static Regex _capturingQualifiedReferenceRegex;
		private static Regex _capturingXsiTypeRegex;

		private string _namespace;
		private string _name;
		private Version _version;
	}
}
