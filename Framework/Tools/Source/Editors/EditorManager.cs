using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using LinkMe.Environment;
using LinkMe.Framework.Tools.Settings;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// Stores lists of types of values and types of editors that can be used to edit those values.
	/// </summary>
	public class EditorManager : ISettingsObject
	{
		private static readonly string m_linkmeVersion = string.Format("{0}.{1}.0.0",
            StaticEnvironment.ProductVersion.Major, StaticEnvironment.ProductVersion.Minor);

		private ArrayList m_valueTypes = new ArrayList();
		private bool m_allValueTypesLoaded = true;
		private System.Exception m_valueTypesException = null;
		private ArrayList m_editorTypes = new ArrayList();
		private System.Type m_defaultEditor = null;
		private string m_defaultEditorTypeName = null;

		public EditorManager()
		{
		}

		#region ISettingsObject Members

		public bool SettingsEqual(ISettingsObject obj)
		{
			// Not likely to be used any time soon, so just the objects are different for now.
			return false;
		}

		public void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix,
			string readingFromPath)
		{
			Clear();

			XmlNode xmlDefaultEditor = xmlSetting.SelectSingleNode(xmlPrefix + "defaultEditor", xmlNsManager);
			if (xmlDefaultEditor != null)
			{
				m_defaultEditorTypeName = ReplaceVersionPlaceholders(xmlDefaultEditor.InnerText);
			}

			foreach (XmlNode xmlEditor in xmlSetting.SelectNodes(xmlPrefix + "editor", xmlNsManager))
			{
				XmlNode xmlValueType = xmlEditor.Attributes.GetNamedItem("valueType");
				if (xmlValueType == null)
					throw new System.ApplicationException("The <editor> element is missing the 'valueType' attribute.");

				Add(ReplaceVersionPlaceholders(xmlValueType.InnerText), ReplaceVersionPlaceholders(xmlEditor.InnerText));
			}
		}

		public void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath)
		{
			writer.WriteStartElement("editors", xmlns);
			WriteXmlSettingContents(writer, xmlns, writingToPath);
			writer.WriteEndElement();  // </editors>
		}

		public void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath)
		{
			if (m_defaultEditor != null || m_defaultEditorTypeName != null)
			{
				string defaultEditor = (m_defaultEditor == null ? m_defaultEditorTypeName :
					m_defaultEditor.AssemblyQualifiedName);
				writer.WriteElementString("defaultEditor", xmlns, defaultEditor);
			}

			for (int index = 0; index < m_valueTypes.Count; index++)
			{
				writer.WriteStartElement("editor", xmlns);
				writer.WriteAttributeString("valueType", GetTypeName(m_valueTypes[index]));
				writer.WriteString(GetTypeName(m_editorTypes[index]));
				writer.WriteEndElement(); // </editor>
			}
		}

		#endregion

		/// <summary>
		/// Returns the first exception that occurred during the last attempt to load the available value types
		/// (last call to <see cref="GetAvailableTypes"/> method) or null if they were loaded successfully.
		/// </summary>
		public System.Exception AvailableTypesException
		{
			get { return m_valueTypesException; }
		}

		/// <summary>
		/// The default editor to be used for any type that does not have an editor defined. May be null.
		/// </summary>
		public System.Type DefaultEditor
		{
			get
			{
				EnsureDefaultEditor();
				return m_defaultEditor;
			}
			set
			{
				if (value != null)
				{
					CheckEditorType(value);
				}

				m_defaultEditor = value;
			}
		}

		#region Static methods

		/// <summary>
		/// Create an instance of the <paramref name="editorType"/> type, checking that it is a Control and
		/// supports the IEditor type.
		/// </summary>
		/// <param name="editorType">The type of the editor to create.</param>
		/// <returns>An instance of the input type.</returns>
		public static IEditor CreateEditorInstance(System.Type editorType)
		{
			const string method = "CreateEditorInstance";

			if (editorType == null)
				throw new NullParameterException(typeof(EditorManager), method, "editorType");

			CheckEditorType(editorType);

			try
			{
				return (IEditor)System.Activator.CreateInstance(editorType);
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException(string.Format("Failed to create an instance of editor type"
					+ " '{0}'.", editorType.FullName), ex);
			}
		}

		public static string ReplaceVersionPlaceholders(string typeName)
		{
			Debug.Assert(typeName != null, "typeName != null");

			return typeName.Replace("{LinkMeVersion}", m_linkmeVersion);
		}

		private static System.Type LoadQualifiedType(string typeQualifiedName)
		{
			int index = typeQualifiedName.IndexOf(",");
			if (index == -1)
			{
				throw new System.ApplicationException("Type name '" + typeQualifiedName + "' is not a valid"
					+ " assembly qualified name.");
			}

			try
			{
				string assemblyName = typeQualifiedName.Substring(index + 2);
				string typeName = typeQualifiedName.Substring(0, index);

				Assembly assembly = Assembly.Load(assemblyName);
				return assembly.GetType(typeName, true);
			}
			catch (System.Exception ex)
			{
				throw new System.ApplicationException("Failed to load type with assembly qualified name '"
					+ typeQualifiedName + "'.", ex);
			}
		}

		private static void CheckEditorType(System.Type editorType)
		{
			if (!typeof(Control).IsAssignableFrom(editorType))
			{
				throw new System.ArgumentException("The editor type, '" + editorType.FullName
					+ "', is not a Windows Forms control.");
			}
			if (editorType.GetInterface(typeof(IEditor).FullName) != typeof(IEditor))
			{
				throw new System.ArgumentException("The editor type, '" + editorType.FullName
					+ "', does not implement the '" + typeof(IEditor).FullName + "' interface.");
			}
		}

		private static string GetTypeName(object type)
		{
			// The object should either be a System.Type or a string containing the assembly-qualified name.

			System.Type netType = type as System.Type;
			if (netType != null)
				return netType.AssemblyQualifiedName;

			Debug.Assert(type is string, "type is string");
			return (string)type;
		}

		#endregion

		/// <summary>
		/// Add an entry to the editor manager.
		/// </summary>
		/// <param name="valueType">The type of the value that can be edited as an assembly-qualified name.</param>
		/// <param name="editorType">The type of the editor that can edit <paramref name="valueType"/>
		/// as an assembly-qualified name.</param>
		public void Add(string valueType, string editorType)
		{
			const string method = "Add";

			if (valueType == null)
				throw new NullParameterException(GetType(), method, "valueType");
			if (editorType == null)
				throw new NullParameterException(GetType(), method, "editorType");

			// Add the types as strings, they will be loaded later as needed.

			m_valueTypes.Add(valueType);
			m_editorTypes.Add(editorType);
			m_allValueTypesLoaded = false;
		}

		/// <summary>
		/// Add an entry to the editor manager.
		/// </summary>
		/// <param name="valueType">The type of the value that can be edited.</param>
		/// <param name="editorType">The type of the editor that can edit <paramref name="valueType"/>.</param>
		public void Add(System.Type valueType, System.Type editorType)
		{
			const string method = "Add";

			if (valueType == null)
				throw new NullParameterException(GetType(), method, "valueType");
			if (editorType == null)
				throw new NullParameterException(GetType(), method, "editorType");

			CheckEditorType(editorType);

			m_valueTypes.Add(valueType);
			m_editorTypes.Add(editorType);
		}

		/// <summary>
		/// Clear all entries from the list.
		/// </summary>
		public void Clear()
		{
			m_valueTypes.Clear();
			m_editorTypes.Clear();
			m_defaultEditor = null;
			m_defaultEditorTypeName = null;
			m_allValueTypesLoaded = true;
			m_valueTypesException = null;
		}

		/// <summary>
		/// Returns the array of value types for which editors are available. If exceptions occurs while
		/// loading any of the types the remaining types are still returned. The
		/// <see cref="AvailableTypesException"/> property returns the first of these exceptions or null if
		/// no exceptions occurred.
		/// </summary>
		public System.Type[] GetAvailableTypes()
		{
			try
			{
				EnsureValueTypes();
				m_valueTypesException = null;
			}
			catch (System.Exception ex)
			{
				m_valueTypesException = ex;
			}

			if (m_valueTypesException == null)
				return (System.Type[])m_valueTypes.ToArray(typeof(System.Type));

			// Since an exception occurred some of the types in m_valueTypes are still strings. Copy
			// all System.Type objects to another collection in order to return them.

			ArrayList netTypes = new ArrayList();
			foreach (object type in m_valueTypes)
			{
				System.Type netType = type as System.Type;
				if (netType != null)
				{
					netTypes.Add(netType);
				}
			}

			return (System.Type[])netTypes.ToArray(typeof(System.Type));
		}

		/// <summary>
		/// Loads the editor type specified by <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The type of the editor as an assembly-qualified name.</param>
		/// <returns>A System.Type object for specified type or null if the type was not found.</returns>
		public System.Type GetEditorType(string type)
		{
			const string method = "GetEditorType";

			if (type == null)
				throw new NullParameterException(GetType(), method, "type");

			type = ReplaceVersionPlaceholders(type);

			for (int index = 0; index < m_editorTypes.Count; index++)
			{
				System.Type editorType = m_editorTypes[index] as System.Type;
				if (editorType != null)
				{
					if (editorType.AssemblyQualifiedName == type)
						return editorType;
				}
				else
				{
					string typeName = m_editorTypes[index] as string;
					Debug.Assert(typeName != null, "typeName != null");

					if (typeName == type)
					{
						editorType = LoadQualifiedType(typeName);
						m_editorTypes[index] = editorType;

						return editorType;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Get the type of an editor that can be used to edit a value of type <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The type of the value to be edited.</param>
		/// <param name="useDefault">True to return the default editor if no matching editor is found, false
		/// to return null.</param>
		/// <returns>The type of the editor that can be used to edit the value or null if none was found.</returns>
		public System.Type GetEditorTypeForValueType(System.Type type, bool useDefault)
		{
			const string method = "GetEditorTypeForValueType";

			if (type == null)
				throw new NullParameterException(GetType(), method, "type");

			// Try to load every type until we find the one we're looking for, but if one of them fails still
			// continue searching. If the value type is not found then re-throw the first exception.

			System.Exception firstException = null;

			for (int index = 0; index < m_valueTypes.Count; index++)
			{
				System.Type valueType = null;
				try
				{
					valueType = EnsureValueType(index);
				}
				catch (System.Exception ex)
				{
					if (firstException == null)
					{
						firstException = ex;
					}
				}

				if (valueType != null && valueType.IsAssignableFrom(type))
				{
					// Found this value type in the list. Try to load its editor if not already loaded.
					// Do not catch the exception in this case - assume that there is only one "correct" editor
					// for a given value type, so there's no point in looking further.

					object editor = m_editorTypes[index];

					string editorTypeName = editor as string;
					if (editorTypeName != null)
					{
						System.Type editorType;
						try
						{
							editorType = LoadQualifiedType(editorTypeName);
						}
						catch (System.Exception ex)
						{
							throw new System.ApplicationException("Failed to load the editor type for value type '"
								+ valueType.AssemblyQualifiedName + "'.", ex);
						}

						m_editorTypes[index] = editorType;
						return editorType;
					}
					else
					{
						Debug.Assert(editor is System.Type, "editor is System.Type");
						return (System.Type)editor;
					}
				}
			}

			// No editor found - if any exception occurred in loading the value types re-throw it now.

			if (firstException != null)
				throw firstException;

			return (useDefault ? DefaultEditor : null);
		}

		private void EnsureDefaultEditor()
		{
			if (m_defaultEditor == null && m_defaultEditorTypeName != null)
			{
				try
				{
					DefaultEditor = LoadQualifiedType(m_defaultEditorTypeName);
				}
				catch (System.Exception ex)
				{
					throw new System.ApplicationException("Failed to load the default editor type '"
						+ m_defaultEditorTypeName + "'.", ex);
				}
			}
		}

		private System.Type EnsureValueType(int index)
		{
			object type = m_valueTypes[index];

			string typeName = type as string;
			if (typeName != null)
			{
				System.Type netType = LoadQualifiedType(typeName);
				m_valueTypes[index] = netType;
				return netType;
			}
			else
			{
				Debug.Assert(type is System.Type, "type is System.Type");
				return (System.Type)type;
			}
		}

		private void EnsureValueTypes()
		{
			if (m_allValueTypesLoaded)
				return;

			System.Exception firstException = null;

			// Try to every value type that is not already loaded. If it fails continue loading the rest,
			// but store the first exception to re-throw at the end.

			for (int index = 0; index < m_valueTypes.Count; index++)
			{
				try
				{
					EnsureValueType(index);
				}
				catch (System.Exception ex)
				{
					if (firstException == null)
					{
						firstException = ex;
					}
				}
			}

			if (firstException == null)
			{
				m_allValueTypesLoaded = true;
			}
			else
			{
				throw new System.ApplicationException("One or more value types listed in the Editor Manager"
					+ " failed to load. Only the first exception is reported.", firstException);
			}
		}
	}
}
