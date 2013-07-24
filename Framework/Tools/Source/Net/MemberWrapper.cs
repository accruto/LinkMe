using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.Editors;
using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Base class for a field or property wrapper obtained from a GenericWrapper. This class is used by
	/// GenericEditorGrid.
	/// </summary>
	public abstract class MemberWrapper : MarshalByRefObject
	{
		#region Nested types

		[Serializable]
		private class ExceptionValue
		{
			private const string m_messageFormatString = "<error: {0}>";
			private const string m_exceptionFormatString = "<error: an exception of type '{0}' was thrown>";

			private string m_value;

			internal ExceptionValue(string message)
			{
				Debug.Assert(message != null, "message != null");
				m_value = message;
			}

			internal ExceptionValue(System.Exception ex)
			{
				Debug.Assert(ex != null, "ex != null");

				try
				{
					m_value = string.Format(m_messageFormatString, ex.Message);
				}
				catch (System.Exception)
				{
					m_value = string.Format(m_exceptionFormatString, ex.GetType().FullName);
				}
			}

			public override string ToString()
			{
				return m_value;
			}
		}

		#endregion

		/// <summary>
		/// Raised when the value of the member is set.
		/// </summary>
		public event EventHandler ValueChanged;

		private object m_tag = null;
		private object m_cachedValue = null;
		private System.Exception m_cachedException = null;
		private bool m_valueIsCached = false;
		private bool m_valueIsNull = false;
		private bool m_isParseable = false;
		private bool m_isParseableIsCached = false;
		private object m_stringValue = null;
		private bool m_stringValueIsCached = false;
		private bool m_modified = false;
		private EditorValueFormat m_valueFormat = GenericEditorGrid.DefaultValueFormat;

		protected MemberWrapper()
		{
		}

		/// <summary>
		/// True if the value of the member can be read.
		/// </summary>
		public abstract bool CanRead
		{
			get;
		}

		/// <summary>
		/// True if the value of the member can be changed.
		/// </summary>
		public abstract bool CanWrite
		{
			get;
		}

		/// <summary>
		/// True if CreateWrapper can be called to create a GenericWrapper object for this member.
		/// </summary>
		public virtual bool CanCreateWrapper
		{
			get { return true; }
		}

		/// <summary>
		/// True if member may itself have members (children), which can be obtained by creating a GenericWrapper.
		/// If this property returns false the caller can avoid creating a GenericWrapper to save time.
		/// </summary>
		public virtual bool MayHaveChildren
		{
			get
			{
				System.Type type = GetValueType();

				if (type == null || type == typeof(object))
					return false; // No strong type, so no members.
				else if (type.IsArray || type == typeof(ArrayList) || type == typeof(Hashtable)
					|| type == typeof(SortedList))
				{
					// Pure collection/dictionary classes only have their elements as members, so check the count.

					object val = GetValue();
					Debug.Assert(val is ICollection, "val is ICollection");

					return ((ICollection)val).Count > 0;
				}
				else if (type.GetInterface(typeof(ICustomWrapper).FullName, false) == typeof(ICustomWrapper))
				{
					ICustomWrapper custom = (ICustomWrapper)GetValue();
					return custom.MayHaveChildren;
				}
				else
					return true; // Don't know anything about this object, so assume it may have children.
			}
		}

		/// <summary>
		/// The name of this member as it should appear in messages to the user.
		/// </summary>
		public virtual string DisplayName
		{
			get { return Name; }
		}

		/// <summary>
		/// The name that uniquely identifies this member amongst its siblings.
		/// </summary>
		public abstract string Name
		{
			get;
		}

		/// <summary>
		/// The type of the member (eg. field type or property return value type) as a string.
		/// </summary>
		public string MemberTypeName
		{
			get { return GetTypeName(GetMemberType()); }
		}

		/// <summary>
		/// The type of the value as a string.
		/// </summary>
		public string ValueTypeName
		{
			get { return GetTypeName(GetValueType()); }
		}

        /// <summary>
        /// Same as ValueTypeName, but also shows the length for strings.
        /// </summary>
        public string ValueTypeDescription
        {
            get
            {
                string description = GetValueTypeDescription();
                if (!string.IsNullOrEmpty(description))
                    return description;

                Type type = GetValueType();
                description = GetTypeName(type);

                if (type == typeof(string))
                {
                    // Need to use the real value here, not ValueAsString, so escape characters are not counted.

                    string val = GetValue() as string;
                    if (val != null)
                    {
                        description += " (" + val.Length + ")";
                    }
                }

                return description;
            }
        }

		/// <summary>
		/// The string representation of the member value. The set accessor for this property attempts to parse
		/// the specified string as a literal or using the object's Parse method, if it has one.
		/// </summary>
		public object ValueAsString
		{
			get
			{
				if (!m_stringValueIsCached)
				{
					m_stringValue = GetValueAsStringImpl();
					m_stringValueIsCached = true;
				}

				return m_stringValue;
			}
			set
			{
				if (value == null)
				{
					SetValue(null);
				}
				else if (value is string)
				{
					SetValueAsStringImpl((string)value);
				}
				else
				{
					throw new ArgumentException("Only string values are supported, but a '"
						+ value.GetType().FullName + "' was passed in.");
				}
			}
		}

		/// <summary>
		/// True if the value is either null or cannot be read for any reason.
		/// </summary>
		public virtual bool ValueIsNullOrUnavailable
		{
			get
			{
				if (m_valueIsCached)
					return m_valueIsNull;
				else if (CanRead)
				{
					try
					{
						return (GetValue() == null);
					}
					catch (System.Exception)
					{
						return true;
					}
				}
				else
					return true;
			}
		}

		/// <summary>
		/// A user-supplied object. The MemberWrapper class itself does not use this value.
		/// </summary>
		public object Tag
		{
			get { return m_tag; }
			set { m_tag = value; }
		}

		internal bool Modified
		{
			get { return m_modified; }
			set { m_modified = value; }
		}

        internal bool IsString
        {
            get { return GetValueType() == typeof (string); }
        }

		#region Static methods

		private static bool TypeIsDerivedFrom(System.Type typeToCheck, System.Type derivedFromType)
		{
			return (typeToCheck == derivedFromType || typeToCheck.IsSubclassOf(derivedFromType)
				|| (derivedFromType.IsInterface && derivedFromType.IsAssignableFrom(typeToCheck)));
		}

		private static ValueType ParseCSharpLiteral(string value)
		{
			if (string.Compare(value, "true", true) == 0)
				return true;
			else if (string.Compare(value, "false", true) == 0)
				return false;
			else
				return TextUtil.ParseCSharpNumericLiteral(value);
		}

		private static object CreateDefaultInstance(string typeName)
		{
			Debug.Assert(typeName != null, "typeName != null");

			try
			{
				if (typeName.EndsWith("()"))
				{
					// Ignore the "()" that the user might have added at the end out of habit.
					typeName = typeName.Substring(0, typeName.Length - 2);
				}

				Type type = null;
				int index = typeName.IndexOf(',');

				if (index == -1)
				{
					// No assembly specified, so search all the loaded assemblies.

					foreach (Assembly loaded in AppDomain.CurrentDomain.GetAssemblies())
					{
						type = loaded.GetType(typeName, false, false);
						if (type != null)
							break;
					}

					if (type == null)
					{
						throw new ApplicationException("Failed to find type '" + typeName
							+ "' in the currently loaded assemblies. Try specifying a fully qualified type name.");
					}
				}
				else
				{
					// The type name has a comma, so assume it's a fully qualified name.

					string assemblyName = typeName.Substring(index + 2);
					typeName = typeName.Substring(0, index);

					Assembly assembly = Assembly.Load(assemblyName);
					type = assembly.GetType(typeName, true, false);
				}

				Debug.Assert(type != null, "type != null");

				return Activator.CreateInstance(type, true);
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to create a default instance of type '" + typeName + "'.", ex);
			}
		}

		#endregion

		/// <summary>
		/// When implemented in a derived class returns the type of the member (eg. field type or property return value).
		/// May return null.
		/// </summary>
		public abstract Type GetMemberType();

		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>
		/// Returns the value of the member as an object and caches it. Further calls to this method return the same
		/// value until ClearCache() is called.
		/// </summary>
		public object GetValue()
		{
			if (!m_valueIsCached)
			{
				try
				{
					m_cachedValue = GetValueImpl();
					m_cachedException = null;
					m_valueIsNull = (m_cachedValue == null);
				}
				catch (System.Exception ex)
				{
					m_cachedValue = null;
					m_cachedException = ex;
				}

				m_valueIsCached = true;
			}

			if (m_cachedException == null)
				return m_cachedValue;
			else
				throw m_cachedException;
		}

		/// <summary>
		/// Sets the value of the member.
		/// </summary>
		public void SetValue(object value)
		{
			ClearCache();

			SetValueImpl(value);
			OnValueChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Checks whether the type of the member is equal to or derived from the specified type.
		/// </summary>
		public bool MemberTypeIsDerivedFrom(System.Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			return TypeIsDerivedFrom(GetMemberType(), type);
		}

		/// <summary>
		/// Checks whether the type of the value is equal to or derived from the specified type.
		/// </summary>
		public bool ValueIsDerivedFrom(System.Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			if ((m_valueIsCached && m_cachedValue == null) || !CanRead)
				return false;

			object val = GetValue();
			if (val == null)
				return false;

			return TypeIsDerivedFrom(val.GetType(), type);
		}

		/// <summary>
		/// Checks whether the type of the property is equal to or derived from any of the specified types.
		/// If it is, returns the first type in the supplied list from which this type is derived, otherwise
		/// returns null.
		/// </summary>
		public System.Type ValueIsDerivedFromAny(System.Type[] types)
		{
			if (types == null)
				throw new ArgumentNullException("types");

			if ((m_valueIsCached && m_cachedValue == null) || !CanRead)
				return null;

			object val = GetValue();
			if (val == null)
				return null;

			Type valueType = val.GetType();

			foreach (System.Type type in types)
			{
				if (TypeIsDerivedFrom(valueType, type))
					return type;
			}

			return null;
		}	

		/// <summary>
		/// Returns true if the value is a system type that can be parsed from a string.
		/// </summary>
		public bool ValueTypeIsParseableSystemType()
		{
			if (!m_isParseableIsCached)
			{
				m_isParseable = ValueTypeIsParseableSystemTypeImpl();
				m_isParseableIsCached = true;
			}

			return m_isParseable;
		}

		/// <summary>
		/// Clears any cached data about the member, such as it's value as an object, value as a string, etc.
		/// </summary>
		public void ClearCache()
		{
			m_cachedValue = null;
			m_cachedException = null;
			m_valueIsCached = false;
			m_isParseableIsCached = false;
			m_stringValue = null;
			m_stringValueIsCached = false;;
		}

		/// <summary>
		/// Creates a GenericWrapper object for the member.
		/// </summary>
		public virtual GenericWrapper CreateWrapper()
		{
			if (!CanRead)
				return null;

			object val = GetValue();
			if (val == null)
				return null;

			ICustomWrapper custom = val as ICustomWrapper;
			if (custom != null)
				return custom.CreateWrapper();

			return new GenericWrapper(val);
		}

        /// <summary>
        /// When implemented in a derived class gets a description for the values type.
        /// </summary>
        protected virtual string GetValueTypeDescription()
        {
            return null;
        }

		/// <summary>
		/// When implemented in a derived class reads the member value.
		/// </summary>
		protected abstract object GetValueImpl();

		/// <summary>
		/// When implemented in a derived class writes the member value.
		/// </summary>
		protected abstract void SetValueImpl(object value);

		/// <summary>
		/// Returns the name of the specified type as it would appear in C# code.
		/// </summary>
		protected virtual string GetTypeName(Type type)
		{
			if (type == null)
				return "<null>";

			if (type == typeof(bool))
				return "bool";
			if (type == typeof(byte))
				return "byte";
			if (type == typeof(sbyte))
				return "sbyte";
			if (type == typeof(short))
				return "short";
			if (type == typeof(ushort))
				return "ushort";
			if (type == typeof(int))
				return "int";
			if (type == typeof(uint))
				return "uint";
			if (type == typeof(long))
				return "long";
			if (type == typeof(ulong))
				return "ulong";
			if (type == typeof(char))
				return "char";
			if (type == typeof(double))
				return "double";
			if (type == typeof(float))
				return "float";
			if (type == typeof(object))
				return "object";
			if (type == typeof(string))
				return "string";
			if (type == typeof(decimal))
				return "decimal";

			if (type.IsArray)
			{
				Type elementType = type.GetElementType();
				return type.FullName.Replace(elementType.FullName, GetTypeName(elementType));
			}

			return type.FullName;
		}

		/// <summary>
		/// Raises the ValueChanged event.
		/// </summary>
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}

		internal void SetModified()
		{
			if (m_valueIsCached && CanRead && CanWrite)
			{
				try
				{
					m_modified = !object.Equals(m_cachedValue, GetValueImpl());
				}
				catch (System.Exception)
				{
					m_modified = true;
				}
			}
			else
			{
				m_modified = false;
			}
		}

		internal void SetValueFormat(EditorValueFormat value)
		{
			m_valueFormat = value;
			m_stringValueIsCached = false;
		}

		private System.Type GetValueType()
		{
			if (!CanRead)
				return null;

			object val;
			try
			{
				val = GetValue();
			}
			catch (System.Exception)
			{
				return null;
			}

			if (val == null)
				return null;

			return val.GetType();
		}

		private bool ValueTypeIsParseableSystemTypeImpl()
		{
			System.Type type = GetValueType();
			if (type == null)
				return false;

			return (type == typeof(bool) || type == typeof(byte) || type == typeof(char)
				|| type == typeof(decimal) || type == typeof(double) || type == typeof(Guid)
				|| type == typeof(short) || type == typeof(int) || type == typeof(long)
				|| type == typeof(sbyte) || type == typeof(string) || type == typeof(float)
				|| type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong)
				|| type.IsSubclassOf(typeof(Enum)));
		}

		private object GetValueAsStringImpl()
		{
			try
			{
				object val = GetValue();

				if (val == null)
					return null;

				if (m_valueFormat == EditorValueFormat.CSharp)
				{
					if (val is string)
						return TextUtil.QuoteStringForCSharp((string)val);
					else if (val is ValueType)
					{
						// The value might be a numeric type. If so, display a type suffix to make it clear to the
						// user what the exact type of the value is (eg. to differentiate between int and long).

						return TextUtil.QuoteNumericLiteralForCSharp((ValueType)val);
					}
				}
				else
				{
					Debug.Assert(m_valueFormat == EditorValueFormat.PlainString,
						"Unexpected value of m_valueFormat: " + m_valueFormat.ToString());
				}

				if (val is Array)
					return "{Length=" + ((Array)val).Length.ToString() + "}";
				else if (val is ArrayList)
					return "{Count=" + ((ArrayList)val).Count.ToString() + "}";
				else
					return val.ToString();
			}
			catch (TargetInvocationException ex)
			{
				return new ExceptionValue(ex.InnerException);
			}
			catch (System.Exception ex)
			{
				return new ExceptionValue(ex);
			}
		}

		private void SetValueAsStringImpl(string value)
		{
			Debug.Assert(value != null, "value != null");

			object memberValue;
			Type type = GetMemberType();

			if (value.StartsWith("new "))
			{
				memberValue = CreateDefaultInstance(value.Substring(4));
			}
			else if (type.IsEnum)
			{
				memberValue = Enum.Parse(type, value);
			}
			else if (type == typeof(string))
			{
				if (m_valueFormat == EditorValueFormat.CSharp)
				{
					memberValue = TextUtil.ParseCSharpQuotedString(value);
				}
				else
				{
					Debug.Assert(m_valueFormat == EditorValueFormat.PlainString,
						"Unexpected value of m_valueFormat: " + m_valueFormat.ToString());
					memberValue = value;
				}
			}
			else if (type == typeof(Guid))
			{
				memberValue = new Guid(value);
			}
			else if (type == typeof(int) || type == typeof(uint) || type == typeof(long) || type == typeof(ulong)
				|| type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(bool))
			{
				if (m_valueFormat == EditorValueFormat.CSharp)
				{
					memberValue = ParseCSharpLiteral(value);
				}
				else
				{
					Debug.Assert(m_valueFormat == EditorValueFormat.PlainString,
						"Unexpected value of m_valueFormat: " + m_valueFormat.ToString());
					memberValue = Convert.ChangeType(value, type);
				}
			}
			else
			{
				MethodInfo parseMethod = type.GetMethod("Parse", new Type[] { typeof(string) });

				if (parseMethod != null)
				{
					memberValue = parseMethod.Invoke(null, new object[] { value });
				}
				else
				{
					// No parse method, but the value might still be a valid boolean, numeric or string literal.

					if (m_valueFormat == EditorValueFormat.CSharp)
					{
						try
						{
							memberValue = ParseCSharpLiteral(value);
						}
						catch (System.Exception)
						{
							memberValue = TextUtil.ParseCSharpQuotedString(value);
						}
					}
					else
					{
						if (type == typeof(object))
						{
							throw new System.ApplicationException("Unable to parse a string expression, because"
								+ " the type is 'object' and the editor value format is '"
								+ m_valueFormat.ToString() + "', which does not provide a way of determining"
								+ " the type of the expression.");
						}
						else
						{
							throw new System.ApplicationException("Unable to parse a string expression, because"
								+ " the type '" + type.FullName + "' does not have a Parse() method.");
						}
					}
				}
			}

			SetValue(memberValue);
		}
	}
}
