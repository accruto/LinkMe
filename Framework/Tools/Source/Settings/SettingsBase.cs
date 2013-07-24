using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Tools.Settings
{
	/// <summary>
	/// Provides base functionality for storing settings, which can be persisted as XML.
	/// </summary>
	[Serializable]
	public abstract class SettingsBase : ISerializable
	{
		#region Nested types

		public class SettingNames
		{
			private ArrayList m_names = new ArrayList();

			internal SettingNames()
			{
			}

			internal int Count
			{
				get { return m_names.Count; }
			}

			public void Add(params string[] settingNames)
			{
				if (settingNames == null || settingNames.Length == 0)
					throw new ArgumentException("The setting names must not be null or an empty array.");

				m_names.AddRange(settingNames);
			}

			internal string[] ToArray()
			{
				return (string[])m_names.ToArray(typeof(string));
			}
		}

		public class SettingValues
		{
			private ArrayList m_values = new ArrayList();

			internal SettingValues()
			{
			}

			internal int Count
			{
				get { return m_values.Count; }
			}

			internal object this[int index]
			{
				get { return m_values[index]; }
			}

			public void Add(params object[] settingValues)
			{
				if (settingValues == null)
				{
					m_values.Add(null);
				}
				else if (settingValues.Length == 0)
					throw new ArgumentException("The setting values must not be an empty array.");
				else
				{
					m_values.AddRange(settingValues);
				}
			}
		}

		#endregion

		private const string m_rootElementName = "configuration";
		private const string m_secondElementName = "LinkMe.Framework";
		private const string m_defaultPrefix = "defaultPrefix";
		private const string m_settingsElementName = "settings";
		private const string m_sectionElementName = "section";
		private const string m_settingElementName = "setting";
		private const string m_versionAttributeName = "version";
		private const string m_nameAttributeName = "name";
		private const char m_intArraySeparator = ',';

		public event SettingChangedEventHandler SettingChanged;

		private Version m_version;
		// The default path to save to when SaveXml()is called without a file path.
		private string m_defaultFilePath = null;
		// The path that is currently being read from/written to. This is passed to IXmlSettings objects.
		private string m_currentFilePath = null;
		private string[] m_settingNames;
		private Hashtable m_settingDictionary;

		protected SettingsBase()
		{
			m_version = HighestSupportedVersion;

			SettingNames names = new SettingNames();
			AddSettingNames(names);

			if (names.Count == 0)
			{
				throw new ApplicationException("The '" + GetType().FullName + "' settings class did not"
					+ " return any setting names.");
			}
			m_settingNames = names.ToArray();

			// Get the values twice. The first time create a dictionary of initial values that we can use
			// to determine if the settings have changed from defaults.

			m_settingDictionary = CreateInitialSettingsDictionary();
		}

		protected SettingsBase(SerializationInfo info, StreamingContext context)
		{
			m_version = (Version)info.GetValue("m_version", typeof(Version));
			m_settingDictionary = (Hashtable)info.GetValue("m_settingDictionary", typeof(Hashtable));
			m_defaultFilePath = info.GetString("m_defaultFilePath");
		}

		#region ISerializable Members

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_version", m_version);
			info.AddValue("m_settingDictionary", m_settingDictionary);
			info.AddValue("m_defaultFilePath", m_defaultFilePath);
		}

		#endregion

		public abstract Version HighestSupportedVersion
		{
			get;
		}

		public abstract Version LowestSupportedVersion
		{
			get;
		}

		public abstract string SettingsXmlPrefix
		{
			get;
		}

		public abstract string SettingsXmlNamespace
		{
			get;
		}

		public abstract string DefaultSettingsFileName
		{
			get;
		}

		public virtual string DefaultSettingsDirectory
		{
			// Default to the roaming user ApplicationData folder.
			get { return RuntimeEnvironment.GetApplicationDataFolder(); }
		}

		public Version Version
		{
			get { return m_version; }
		}

		protected abstract string SettingsXmlName
		{
			get;
		}

		protected abstract string ObjectDisplayType
		{
			get;
		}

		protected object this[string settingName]
		{
			get
			{
				if (!m_settingDictionary.ContainsKey(settingName))
					throw new ApplicationException("Setting '" + settingName + "' does not exist in the dictionary.");

				return m_settingDictionary[settingName];
			}
			set
			{
				// Store the previous value (if any).

				object previousValue = m_settingDictionary[settingName];

				// Make the change.

				m_settingDictionary[settingName] = value;

				// Raise an event if the setting has actually changed.

				if (!object.Equals(value, previousValue))
				{
					OnSettingChanged(new SettingChangedEventArgs(settingName, previousValue));
				}
			}
		}

		protected string Prefix
		{
			get
			{
				return (SettingsXmlPrefix == null || SettingsXmlPrefix.Length == 0 ?
					m_defaultPrefix + ":" : SettingsXmlPrefix + ":");
			}
		}

		#region Static methods

		/// <summary>
		/// Writes sub-settings from a SettingsBase object.
		/// </summary>
		protected static void WriteSubSettings(XmlWriter writer, SettingsBase value)
		{
			if (value == null)
				return;

			value.WriteSettingsElement(writer);
		}

		/// <summary>
		/// Loads sub-settings into a SettingsBase object. Returns true if settings were loaded, otherwise false.
		/// </summary>
		protected static bool ReadSubSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager,
			SettingsBase settingsObject)
		{
			if (settingsObject == null)
				return false;

			try
			{
				return settingsObject.ReadSettingsElement(xmlSetting, xmlNsManager);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to load XML settings for a '"
					+ settingsObject.GetType() + "' object.", ex);
			}
		}

		private static string GetConversionErrorMessage(string value, string settingName,
			XmlNode xmlSection, string expectedType)
		{
			Debug.Assert(xmlSection != null, "xmlSection != null");

			XmlNode xmlSectionName = xmlSection.Attributes.GetNamedItem("name");
			Debug.Assert(xmlSectionName != null, "xmlSectionName != null");

			return string.Format("The value '{0}' for setting '{1}' in section '{2}' is not a valid {3} value.",
				value, settingName, xmlSectionName.InnerText, expectedType);
		}

		private static int[] StringArrayToIntArray(string[] valuesAsStrings)
		{
			int[] values = new int[valuesAsStrings.Length];

			for (int index = 0; index < valuesAsStrings.Length; index++)
			{
				values[index] = XmlConvert.ToInt32(valuesAsStrings[index]);
			}

			return values;
		}

		#endregion

		public bool LoadXml()
		{
			return LoadXml(Path.Combine(DefaultSettingsDirectory, DefaultSettingsFileName));
		}

		public bool LoadXml(string settingsFilePath)
		{
			if (settingsFilePath == null)
				throw new ArgumentNullException("settingsFilePath");

			if (!File.Exists(settingsFilePath))
				return false;

			m_currentFilePath = settingsFilePath;

			try
			{
				XmlDocument xmlSource = new XmlDocument();
				xmlSource.Load(settingsFilePath);

				// Set up namespaces.

				XmlNamespaceManager xmlNsManager = new XmlNamespaceManager(xmlSource.NameTable);

				if (SettingsXmlPrefix == null || SettingsXmlPrefix.Length == 0)
				{
					xmlNsManager.AddNamespace(m_defaultPrefix, SettingsXmlNamespace);
				}
				else
				{
					if (SettingsXmlPrefix.EndsWith(":"))
					{
						throw new ApplicationException("Invalid value of SettingsXmlPrefix ('"
							+ SettingsXmlPrefix + "') - it must not end with a ':'.");
					}
					xmlNsManager.AddNamespace(SettingsXmlPrefix, SettingsXmlNamespace);
				}

				// Get the <settings> node.

				XmlNode xmlSettingsRoot = xmlSource.SelectSingleNode(m_rootElementName + "/"
					+ m_secondElementName + "/" + Prefix + m_settingsElementName, xmlNsManager);
				if (xmlSettingsRoot == null)
					return false;

				// Load the right settings node from the XML file.

				XmlNode xmlSettings = xmlSettingsRoot.SelectSingleNode(Prefix + SettingsXmlName, xmlNsManager);
				if (xmlSettings == null)
					return false; // This file doesn't contain the type of settings we want.

				// Read its contents.

				if (!ReadSettingsElement(xmlSettings, xmlNsManager))
					return false;

				m_defaultFilePath = settingsFilePath; // Store the file path so we can save to it later.

				return true;
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to load settings from file '"
					+ settingsFilePath + "'.", ex);
			}
			finally
			{
				m_currentFilePath = null;
			}
		}

		public void SaveXml()
		{
			if (m_defaultFilePath == null)
			{
				m_defaultFilePath = Path.Combine(DefaultSettingsDirectory, DefaultSettingsFileName);
			}

			SaveXml(m_defaultFilePath);
		}

		/// <summary>
		/// Saves the object to the specified file as XML. If the file does not exist it is created. If it does
		/// exist only the settings element identified by SettingsXmlName is replaced - the rest of the XML
		/// is not changed.
		/// </summary>
		public void SaveXml(string settingsFilePath)
		{
			if (settingsFilePath == null)
				throw new ArgumentNullException("settingsFilePath");

			m_currentFilePath = settingsFilePath;

			try
			{
				// Do not create a settings file that contains only the default settings.

				if (!File.Exists(settingsFilePath) && !HaveValuesChangedFromInitial())
					return;

				// Write to a temporary MemoryStream first. This allows reading the previous file contents
				// if the file exists and also prevents the file from being corrupted if an exception occurs
				// part of the way through saving settings.

				MemoryStream tempStream = new MemoryStream();
				XmlTextWriter writer = new XmlTextWriter(tempStream, XmlWriteAdaptor.XmlEncoding);
				writer.Formatting = Formatting.Indented;

				if (!SaveToExistingFile(settingsFilePath, writer))
				{
					writer.WriteStartElement(m_rootElementName);
					writer.WriteStartElement(m_secondElementName);
					writer.WriteStartElement(SettingsXmlPrefix, m_settingsElementName, SettingsXmlNamespace);
					WriteSettingsElement(writer);
					writer.WriteEndElement(); // </settings>
					writer.WriteEndElement(); // </LinkMe.Framework>
					writer.WriteEndElement(); // </configuration>
				}

				writer.Flush();

				// Create the directory, it it does not exist.

				string dir = Path.GetDirectoryName(settingsFilePath);
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}

				// Copy from the temporary MemoryStream to the file.

				using (FileStream outStream = new FileStream(settingsFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					tempStream.WriteTo(outStream);
				}
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write settings to file '" + settingsFilePath + "'.", ex);
			}
			finally
			{
				m_currentFilePath = null;
			}
		}

		protected virtual void OnSettingChanged(SettingChangedEventArgs e) 
		{
			if (SettingChanged != null)
			{
				SettingChanged(this, e);
			}
		}

		/// <summary>
		/// Checks whether the settings have changed from their initial values.
		/// </summary>
		/// <returns>True if the settings have changed, otherwise false.</returns>
		/// <remarks>
		/// This method compares each of the values returned by GetInitialValues() to the current
		/// value for the same setting. If the setting value implements the <see cref="ISettingsObject"/>
		/// interface then the <see cref="ISettingsObject.SettingsEqual"/> is used to compare them,
		/// otherwise object.Equals() is used.
		/// </remarks>
		protected virtual bool HaveValuesChangedFromInitial()
		{
			// The names and values are assumed to stay constant, so we don't get the names again, but
			// need to get the initial values. Simply cloning the hashtable in the constructor may not
			// work for non-primitive types.

			Hashtable initialValues = CreateInitialSettingsDictionary();

			IDictionaryEnumerator enumerator = m_settingDictionary.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Debug.Assert(initialValues.ContainsKey(enumerator.Key), string.Format(
					"The '{0}' setting class contains a setting named '{1}', which was not returned"
					+ " by GetSettingNames().", GetType().FullName, enumerator.Key));

				object initialValue = initialValues[enumerator.Key];
				ISettingsObject initialSettings = initialValue as ISettingsObject;
				ISettingsObject currentSettings = enumerator.Value as ISettingsObject;

				// If the object implements ISettingsObject call it otherwise just use object equality.

				if (initialSettings != null && currentSettings != null)
				{
					if (!initialSettings.SettingsEqual(currentSettings))
						return true;
				}
				else
				{
					if (!object.Equals(initialValue, enumerator.Value))
						return true;
				}
			}

			return false;
		}

		/// <summary>
		/// When implemented by a derived class adds the names of all the settings in that class.
		/// </summary>
		/// <param name="names">The collection of setting names. The derived class may only add to
		/// this collection.</param>
		protected abstract void AddSettingNames(SettingNames names);

		/// <summary>
		/// When implemented by a derived class adds the initial values of all the settings in that class.
		/// The settings must be added in the same order as the names added by <c>AddSettingNames</c>, so
		/// that the names and values are matched correctly.
		/// </summary>
		/// <param name="values">The collection of initial setting values. The derived class may only
		/// add to this collection.</param>
		protected abstract void AddInitialValues(SettingValues values);

		protected abstract void ReadAllSettings(XmlNode xmlSettings, XmlNamespaceManager xmlNsManager);

		protected abstract void WriteAllSettings(XmlWriter writer);

		protected XmlNode GetSection(XmlNode xmlParent, XmlNamespaceManager xmlNsManager, string name, bool mandatory)
		{
			XmlNode xmlSection = xmlParent.SelectSingleNode(string.Format("{0}{1}[@{2}=\"{3}\"]",
				Prefix, m_sectionElementName, m_nameAttributeName, name), xmlNsManager);

			if (mandatory && xmlSection == null)
				throw new ApplicationException("The settings file is missing mandatory section '" + name + "'.");

			return xmlSection;
		}

		protected XmlNode GetSetting(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name, bool mandatory)
		{
			XmlNode xmlSetting = xmlSection.SelectSingleNode(string.Format("{0}{1}[@{2}=\"{3}\"]",
				Prefix, m_settingElementName, m_nameAttributeName, name), xmlNsManager);

			if (xmlSetting == null && mandatory)
			{
				throw new ApplicationException("The '" + xmlSection.Name + "' section is missing mandatory setting '"
					+ name + "'.");
			}

			return xmlSetting;
		}

		/// <summary>
		/// Reads a string value. Returns null if the value is optional and not specified.
		/// </summary>
		protected string ReadSettingString(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			bool mandatory)
		{
			XmlNode xmlSetting = GetSetting(xmlSection, xmlNsManager, name, mandatory);
			return (xmlSetting == null ? null : xmlSetting.InnerText);
		}

		/// <summary>
		/// Reads an optional string value. Returns the default value if the value is not specified.
		/// </summary>
		protected string ReadSettingString(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			string defaultValue)
		{
			XmlNode xmlSetting = GetSetting(xmlSection, xmlNsManager, name, false);
			return (xmlSetting == null ? defaultValue : xmlSetting.InnerText);
		}

		/// <summary>
		/// Reads a mandatory Int32 value.
		/// </summary>
		protected int ReadSettingInt32(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, true);

			try
			{
				return XmlConvert.ToInt32(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "Int32"), ex);
			}
		}

		/// <summary>
		/// Reads an optional Int32 value.
		/// </summary>
		protected int ReadSettingInt32(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			int defaultValue)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, false);
			if (setting == null)
				return defaultValue;

			try
			{
				return XmlConvert.ToInt32(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "Int32"), ex);
			}
		}

		/// <summary>
		/// Reads an array of Int32 values. Returns null if the value is optional and not specified.
		/// </summary>
		protected int[] ReadSettingInt32Array(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			bool mandatory)
		{
			XmlNode xmlSetting = GetSetting(xmlSection, xmlNsManager, name, mandatory);
			if (xmlSetting == null)
				return null;
			else
				return StringArrayToIntArray(xmlSetting.InnerText.Split(m_intArraySeparator));
		}

		/// <summary>
		/// Reads an array of Int32 values. Returns the default value if the value is not specified.
		/// </summary>
		protected int[] ReadSettingInt32Array(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			int[] defaultValue)
		{
			XmlNode xmlSetting = GetSetting(xmlSection, xmlNsManager, name, false);
			if (xmlSetting == null)
				return (defaultValue == null ? null : (int[])defaultValue.Clone());
			else
				return StringArrayToIntArray(xmlSetting.InnerText.Split(m_intArraySeparator));
		}

		/// <summary>
		/// Reads a mandatory Single value.
		/// </summary>
		protected float ReadSettingSingle(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, true);

			try
			{
				return XmlConvert.ToSingle(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "Single"), ex);
			}
		}

		/// <summary>
		/// Reads an optional Single value.
		/// </summary>
		protected float ReadSettingSingle(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			float defaultValue)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, false);
			if (setting == null)
				return defaultValue;

			try
			{
				return XmlConvert.ToSingle(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "Single"), ex);
			}
		}

		/// <summary>
		/// Reads a mandatory Boolean value.
		/// </summary>
		protected bool ReadSettingBoolean(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, true);

			try
			{
				return XmlConvert.ToBoolean(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "Boolean"), ex);
			}
		}

		/// <summary>
		/// Reads an optional Boolean value.
		/// </summary>
		protected bool ReadSettingBoolean(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			bool defaultValue)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, false);
			if (setting == null)
				return defaultValue;

			try
			{
				return XmlConvert.ToBoolean(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "Boolean"), ex);
			}
		}

		/// <summary>
		/// Reads a mandatory GUID value.
		/// </summary>
		protected Guid ReadSettingGuid(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, true);

			try
			{
				return XmlConvert.ToGuid(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "GUID"), ex);
			}
		}

		/// <summary>
		/// Reads an optional GUID value.
		/// </summary>
		protected Guid ReadSettingGuid(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			Guid defaultValue)
		{
			string setting = ReadSettingString(xmlSection, xmlNsManager, name, false);
			if (setting == null)
				return defaultValue;

			try
			{
				return XmlConvert.ToGuid(setting);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException(GetConversionErrorMessage(setting, name, xmlSection, "GUID"), ex);
			}
		}

		/// <summary>
		/// Loads settings into an object that implements IXmlSettings. Returns true if settings were loaded,
		/// otherwise false.
		/// </summary>
		protected bool ReadSettingIntoObject(XmlNode xmlSection, XmlNamespaceManager xmlNsManager, string name,
			ISettingsObject settingsObject)
		{
			if (settingsObject == null)
				return false;

			XmlNode xmlSetting = GetSetting(xmlSection, xmlNsManager, name, false);
			if (xmlSetting == null)
				return false;

			try
			{
				settingsObject.ReadXmlSettings(xmlSetting, xmlNsManager, Prefix, m_currentFilePath);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to load XML settings for a '"
					+ settingsObject.GetType() + "' object.", ex);
			}

			return true;
		}

		protected void StartSection(XmlWriter writer, string name)
		{
			writer.WriteStartElement(SettingsXmlPrefix, m_sectionElementName, SettingsXmlNamespace);
			writer.WriteAttributeString(m_nameAttributeName, name);
		}

		protected void EndSection(XmlWriter writer)
		{
			writer.WriteEndElement();
		}

		protected void StartSetting(XmlWriter writer, string name)
		{
			writer.WriteStartElement(SettingsXmlPrefix, m_settingElementName, SettingsXmlNamespace);
			writer.WriteAttributeString(m_nameAttributeName, name);
		}

		protected void EndSetting(XmlWriter writer)
		{
			writer.WriteEndElement();
		}

		protected void WriteSetting(XmlWriter writer, string name, string value)
		{
			if (value == null)
				return;

			StartSetting(writer, name);
			writer.WriteString(value);
			EndSetting(writer);
		}

		protected void WriteSetting(XmlWriter writer, string name, int[] value)
		{
			if (value == null)
				return;

			string[] valuesAsStrings = new string[value.Length];
			for (int index = 0; index < value.Length; index++)
			{
				valuesAsStrings[index] = XmlConvert.ToString(value[index]);
			}

			WriteSetting(writer, name, string.Join(m_intArraySeparator.ToString(), valuesAsStrings));
		}

		protected void WriteSetting(XmlWriter writer, string name, ISettingsObject value)
		{
			if (value == null)
				return;

			StartSetting(writer, name);
			value.WriteXmlSettingContents(writer, SettingsXmlNamespace, m_currentFilePath);
			EndSetting(writer);
		}

		private void SetVersion(string versionString)
		{
			Version version;

			try
			{
				version = new Version(versionString);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to read the test settings format version from string '"
					+ versionString + "'.", ex);
			}

			if (version < LowestSupportedVersion || version > HighestSupportedVersion)
				throw new ApplicationException(string.Format("{0} version {1} is not supported. Supported versions"
					+ " are {2} to {3}.", ObjectDisplayType, version, LowestSupportedVersion, HighestSupportedVersion));

			m_version = version;
		}

		private bool ReadSettingsElement(XmlNode xmlSettings, XmlNamespaceManager xmlNsManager)
		{
			// Read the settings version and check it.

			XmlNode xmlVersion = xmlSettings.Attributes.GetNamedItem(m_versionAttributeName);
			if (xmlVersion == null)
			{
				throw new ApplicationException("The '" + xmlSettings.Name
					+ "' settings element does not contain a 'version' attribute.");
			}

			SetVersion(xmlVersion.InnerText);

			ReadAllSettings(xmlSettings, xmlNsManager); // Let derived classes actually load the settings.

			return true;
		}

		private void WriteSettingsElement(XmlWriter writer)
		{
			writer.WriteStartElement(SettingsXmlPrefix, SettingsXmlName, SettingsXmlNamespace);
			writer.WriteAttributeString(m_versionAttributeName, Version.ToString());

			WriteAllSettings(writer);

			writer.WriteEndElement();
		}

		private void WriteSettingsContainerElement(XmlWriter writer)
		{
			writer.WriteStartElement(SettingsXmlPrefix, m_settingsElementName, SettingsXmlNamespace);
			WriteSettingsElement(writer);
			writer.WriteEndElement(); // </settings>
		}

		/// <summary>
		/// Tries to save to an existing file and returns true to indicate success or false to indicate that the
		/// file should be created from scratch or overwritten.
		/// </summary>
		private bool SaveToExistingFile(string settingsFilePath, XmlWriter writer)
		{
			if (!File.Exists(settingsFilePath))
				return false;

			using (FileStream inStream = new FileStream(settingsFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				XmlTextReader reader = new XmlTextReader(inStream);
				reader.WhitespaceHandling = WhitespaceHandling.Significant;

				if (!reader.Read())
					return false;

				if (!reader.IsStartElement(m_rootElementName, string.Empty))
				{
					// The root element is not the one we're looking for and since only one can exist we have
					// to overwrite it.
					return false;
				}

				bool secondLevelWritten = false;
				reader.ReadStartElement();
				writer.WriteStartElement(m_rootElementName, string.Empty);

				// We're inside the root <configuration> element, look for a <LinkMe.Framework> element.

				while (reader.IsStartElement())
				{
					if (!secondLevelWritten && reader.LocalName == m_secondElementName
						&& reader.NamespaceURI.Length == 0)
					{
						bool settingsContainerWritten = false;
						reader.ReadStartElement();
						writer.WriteStartElement(m_secondElementName, string.Empty);

						// We're inside the <LinkMe.Framework> element, look for a <settings> element.

						while (reader.IsStartElement())
						{
							if (!settingsContainerWritten && reader.LocalName == m_settingsElementName
								&& reader.NamespaceURI == SettingsXmlNamespace)
							{
								bool settingsWritten = false;
								reader.ReadStartElement();
								writer.WriteStartElement(SettingsXmlPrefix, m_settingsElementName, SettingsXmlNamespace);

								while (reader.IsStartElement())
								{
									// We're inside the <settings> element, find the custom settings element (SettingsXmlName).

									if (!settingsWritten && reader.LocalName == SettingsXmlName
										&& reader.NamespaceURI == SettingsXmlNamespace)
									{
										WriteSettingsElement(writer);
										settingsWritten = true;
										reader.Skip();
									}
									else
									{
										writer.WriteNode(reader, false);
									}
								}

								// We're at the end of the <settings> element - if the custom settings element hasn't
								// been written yet write it now.

								if (!settingsWritten)
								{
									WriteSettingsElement(writer);
								}

								reader.ReadEndElement();
								writer.WriteEndElement(); // </settings>

								settingsContainerWritten = true;
							}
							else
							{
								writer.WriteNode(reader, false);
							}
						}

						// We're at the end of the <LinkMe.Framework> element - if the <settings> element hasn't
						// been	 written yet write it now.

						if (!settingsContainerWritten)
						{
							WriteSettingsContainerElement(writer);
						}

						reader.ReadEndElement();
						writer.WriteEndElement(); // </LinkMe.Framework>

						secondLevelWritten = true;
					}
					else
					{
						writer.WriteNode(reader, false);
					}
				}

				// We're at the end of the <configuration> element - if the <LinkMe.Framework> element hasn't
				// been written yet write it now.

				if (!secondLevelWritten)
				{
					writer.WriteStartElement(m_secondElementName, string.Empty);
					WriteSettingsContainerElement(writer);
					writer.WriteEndElement(); // </LinkMe.Framework>
				}

				reader.ReadEndElement();
				writer.WriteEndElement(); // <configuration>

				return true;
			}
		}

		private Hashtable CreateInitialSettingsDictionary()
		{
			Debug.Assert(m_settingNames != null, "m_settingNames != null");

			SettingValues values = new SettingValues();
			AddInitialValues(values);

			if (values.Count == 0)
			{
				throw new ApplicationException("The '" + GetType().FullName + "' settings class did not"
					+ " return any initial setting values.");
			}
			if (values.Count != m_settingNames.Length)
			{
				throw new ApplicationException(string.Format("The array of initial values retruned by the"
					+ "'{0}' settings class has {1} elements, but the array of setting names has {2} elements.",
					GetType().FullName, values.Count, m_settingNames.Length));
			}

			Hashtable hashtable = new Hashtable(m_settingNames.Length);
			for (int index = 0; index < m_settingNames.Length; index++)
			{
				hashtable.Add(m_settingNames[index], values[index]);
			}

			return hashtable;
		}
	}
}
