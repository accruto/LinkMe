using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type;

namespace LinkMe.Framework.Configuration
{
	[System.Serializable]
	public sealed class CatalogueElementName
		:	System.ICloneable,
		System.IComparable
	{
		public CatalogueElementName(string reference)
		{
			const string method = ".ctor";

			// Use a capturing regex to extract the name, namespace and version.

			Match match = CapturingElementReferenceRegex.Match(reference);
			if (!match.Success)
				throw new InvalidParameterFormatException(typeof(CatalogueElementName), method, "reference", reference, Constants.Validation.CompleteElementReferencePattern);

			m_catalogueName = CatalogueName.CreateUnchecked(match.Groups["CatalogueName"].Value);
			m_element = match.Groups["Element"].Value;
			m_name = match.Groups["Name"].Value;
		}

		public CatalogueElementName(CatalogueName catalogueName, System.Enum element, string name)
		{
			const string method = ".ctor";

			// Check catalogue name.

			if ( catalogueName == null )
				throw new NullParameterException(typeof(CatalogueElementName), method, "catalogueName");

			// Check element.

			if ( !NameRegex.IsMatch(element.ToString()) )
				throw new InvalidParameterFormatException(typeof(CatalogueElementName), method, "element", element.ToString(), Constants.Validation.CompleteNamePattern);

			// Check name.

			if ( name == null )
				throw new NullParameterException(typeof(CatalogueElementName), method, "name");
			if ( !NameRegex.IsMatch(name) )
				throw new InvalidParameterFormatException(typeof(CatalogueElementName), method, "name", name, Constants.Validation.CompleteNamePattern);

			// Assign.

			m_catalogueName = catalogueName.Clone();
			m_element = element.ToString();
			m_name = name;
		}

		/// <summary>
		/// Private constructor for cloning - performs no checks.
		/// </summary>
		private CatalogueElementName(string name, string element, CatalogueName catalogueName)
		{
			m_name = name;
			m_element = element;
			m_catalogueName = catalogueName.Clone();
		}

		#region ICloneable Members

		public CatalogueElementName Clone()
		{
			return new CatalogueElementName(m_name, m_element, m_catalogueName);
		}

		object System.ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

		#region IComparable Members

		public int CompareTo(CatalogueElementName other)
		{
			int result = m_catalogueName.CompareTo(other.m_catalogueName);
			if ( result != 0 )
				return result;
			result = m_element.CompareTo(other.m_element);
			if ( result != 0 )
				return result;
			return m_name.CompareTo(other.m_name);
		}

		int System.IComparable.CompareTo(object other)
		{
			const string method = "CompareTo";
			if ( !(other is CatalogueElementName) )
				throw new InvalidParameterTypeException(typeof(CatalogueElementName), method, "other", typeof(CatalogueElementName), other);
			return CompareTo((CatalogueElementName) other);
		}

		#endregion

		#region System.Object Members

		public override string ToString()
		{
			return FullReference;
		}

		public override bool Equals(object other)
		{
			CatalogueElementName otherName = other as CatalogueElementName;
			if ( otherName == null )
				return false;

			return m_catalogueName == otherName.m_catalogueName
				&& m_element == otherName.m_element
				&& m_name == otherName.m_name;
		}

		public static bool operator==(CatalogueElementName name1, CatalogueElementName name2)
		{
			return object.Equals(name1, name2);
		}

		public static bool operator!=(CatalogueElementName name1, CatalogueElementName name2)
		{
			return !object.Equals(name1, name2);
		}

		public override int GetHashCode()
		{
			return m_catalogueName.GetHashCode()
				^ m_element.GetHashCode()
				^ m_name.GetHashCode();
		}

		#endregion

		#region Properties

		public CatalogueName CatalogueName
		{
			get { return m_catalogueName; }
		}

		public string Element
		{
			get { return m_element; }
		}

		public string Name
		{
			get { return m_name; }
		}

		public string FullReference
		{
			get { return GetReferenceUnchecked(m_catalogueName, m_element, m_name); }
		}

		#endregion

		public void Write(BinaryWriter writer)
		{
			m_catalogueName.Write(writer);
			writer.Write(m_element);
			writer.Write(m_name);
		}

		public void Read(BinaryReader reader)
		{
			m_catalogueName.Read(reader);
			m_element = reader.ReadString();
			m_name = reader.ReadString();
		}

		private static string GetReferenceUnchecked(CatalogueName catalogueName, string element, string name)
		{
			return element + "=" + name + ", " + catalogueName.FullyQualifiedReference;
		}

		public static readonly Regex NameRegex = new Regex(Constants.Validation.CompleteNamePattern);
		public static readonly Regex ElementReferenceRegex = new Regex(Constants.Validation.CompleteElementReferencePattern);
		public static readonly Regex CapturingElementReferenceRegex = new Regex(Constants.Validation.CompleteCapturingElementReferencePattern);

		private CatalogueName m_catalogueName;
		private string m_element;
		private string m_name;
	}
}
