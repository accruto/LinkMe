using System;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// A marhsal-by-ref wrapper for a .NET object that can be used to read and modify the object's
	/// properties and fields from another domain.
	/// </summary>
	public class GenericWrapper : MarshalByRefObject
	{
		#region Nested types

		public delegate string ObjectToXml(object value);
		public delegate object XmlToObject(System.Type type, string xml);
		public delegate object WorkerDelegate(GenericWrapper wrapper, object state);

		/// <summary>
		/// A comparer that never returns 0, used to sort member names for display purposes.
		/// </summary>
		private class MemberNameComparer : IComparer
		{
			internal MemberNameComparer()
			{
			}

			#region IComparer Members

			public int Compare(object x, object y)
			{
				int result = string.Compare((string)x, (string)y);
				return (result == 0 ? 1 : result);
			}

			#endregion
		}

		#endregion

		internal const int MaxListElements = 4000;

		private object m_wrapped;
		private string m_xml = null;
		private string m_string = null;
		private object m_rootReferenceObject = null;
		private FieldInfo[] m_fieldPath = null;
		private Type m_type;
		private WrappedValueFormats m_formats;

		public GenericWrapper(object wrapped)
		{
			if (wrapped == null)
				throw new ArgumentNullException("wrapped");

			m_wrapped = wrapped;
			m_type = m_wrapped.GetType();
			m_formats = WrappedValueFormats.Object;
		}

		public GenericWrapper(object wrapped, Type type)
		{
			if (wrapped == null)
				throw new ArgumentNullException("wrapped");
			if (type == null)
				throw new ArgumentNullException("type");

			m_wrapped = wrapped;
			m_type = type;
			m_formats = WrappedValueFormats.Object;
		}

		public GenericWrapper(object wrapped, string xmlValue, Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			m_wrapped = wrapped;
			m_xml = xmlValue;
			m_type = type;

			m_formats = WrappedValueFormats.None;
			if (wrapped != null)
			{
				m_formats |= WrappedValueFormats.Object;
			}
			if (m_xml != null)
			{
				m_formats |= WrappedValueFormats.Xml;
			}
		}

		public GenericWrapper(object wrapped, string xmlValue, string stringValue, Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			m_wrapped = wrapped;
			m_xml = xmlValue;
			m_string = stringValue;
			m_type = type;

			m_formats = WrappedValueFormats.String; // String format is "available", even if the string value is null.
			if (wrapped != null)
			{
				m_formats |= WrappedValueFormats.Object;
			}
			if (m_xml != null)
			{
				m_formats |= WrappedValueFormats.Xml;
			}
		}

		internal GenericWrapper(object wrapper, object rootReferenceObject, FieldInfo[] fieldPath)
			: this(wrapper)
		{
			m_rootReferenceObject = rootReferenceObject;
			m_fieldPath = fieldPath;
		}

		public AppDomain Domain
		{
			get { return AppDomain.CurrentDomain; }
		}

		public WrappedValueFormats AvailableFormats
		{
			get { return m_formats; }
		}

		public string Xml
		{
			get { return m_xml; }
			set { m_xml = value; }
		}

		public string String
		{
			get { return m_string; }
			set { m_string = value; }
		}

		private static object GetDefaultValueForType(Type type)
		{
			if (type.IsValueType)
				return Activator.CreateInstance(type, false);
			else
				return null;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public bool IsDefaultValue()
		{
			if (m_wrapped == null)
			{
				throw new InvalidOperationException("The GenericWrapper cannot determine whether it wraps"
					+ " the default value for of type '" + m_type.FullName + "', because it does not have"
					+ " the actual object (HasObject is false).");
			}

			return object.Equals(m_wrapped, GetDefaultValueForType(m_type));
		}

		public object GetWrappedObject()
		{
			return m_wrapped;
		}

		public void SetWrappedObject(object wrapped)
		{
			m_wrapped = wrapped;
		}

		public virtual MemberWrappers GetMembers()
		{
			if ((AvailableFormats & WrappedValueFormats.Object) != WrappedValueFormats.Object)
			{
				throw new InvalidOperationException("Unable to get properties from a GenericWrapper object,"
					+ " because it does not contain the actual object value (only XML or string representation).");
			}

			MemberWrappers list = new MemberWrappers();

			if (m_type.IsArray)
			{
				AddArrayElements(list); // Don't return the fields of an array, just the elements.
			}
			else if (typeof(ArrayList).IsAssignableFrom(m_type))
			{
				AddListElements(list); // A pure collection class, just return the list elements.
			}
			else if (typeof(Hashtable).IsAssignableFrom(m_type) || typeof(SortedList).IsAssignableFrom(m_type))
			{
				AddDictionaryElements(list); // A pure dictionary class, just return the dictionary entries.
			}
			else
			{
				AddFieldsAndProperties(list); // Normal object - return its fields and properties.

				if (typeof(IList).IsAssignableFrom(m_type))
				{
					AddListElements(list); // Implements IList - add the list elements.
				}

				if (typeof(IDictionary).IsAssignableFrom(m_type))
				{
					AddDictionaryElements(list); // Implements IDictionary - add the dictionary entries.
				}
			}

			// Add the base type first (if any) before all the other members.

			Type baseType = m_type.BaseType;
			if (baseType != null && baseType != typeof(object) && baseType != typeof(Array))
			{
				list.Insert(0, new BaseWrapper(m_type.BaseType, m_wrapped));
			}

			return list;
		}

		/// <summary>
		/// Update the XML and string values from the object value. The caller needs to pass in a delegate to
		/// a method that can perform the conversion to XML.
		/// </summary>
		public void UpdateFromObject(ObjectToXml objectToXml)
		{
			if ((AvailableFormats & WrappedValueFormats.Object) != WrappedValueFormats.Object)
			{
				throw new InvalidOperationException("Unable to update the GenericWrapper XML and string values,"
					+ " because it does not have the actual object value.");
			}

			// Clear existing XML and string values.

			m_xml = null;
			m_string = null;
			m_formats = WrappedValueFormats.Object;

			// Try to read them from the object.

			if (objectToXml != null)
			{
				try
				{
					m_xml = objectToXml(m_wrapped);
					m_formats |= WrappedValueFormats.Xml;
				}
				catch (System.Exception)
				{
				}
			}

			try
			{
				m_string = m_wrapped.ToString();
				m_formats |= WrappedValueFormats.String;
			}
			catch (System.Exception)
			{
			}
		}

		/// <summary>
		/// Update the object and string values from the XML value. The caller needs to pass in a delegate to
		/// a method that can perform the conversion from XML.
		/// </summary>
		public void UpdateFromXml(XmlToObject xmlToObject)
		{
			if ((AvailableFormats & WrappedValueFormats.Xml) != WrappedValueFormats.Xml)
			{
				throw new InvalidOperationException("Unable to update the GenericWrapper object and string values,"
					+ " because it does not have the XML value.");
			}
			if (m_wrapped == null)
			{
				throw new InvalidOperationException("Unable to update the GenericWrapper object and string values,"
					+ " because it does not have an existing object value (needed to get the type).");
			}

			// Reset existing object and string values.

			m_wrapped = null;
			m_string = null;
			m_formats = WrappedValueFormats.Xml;

			if (xmlToObject != null)
			{
				try
				{
					m_wrapped = xmlToObject(m_type, m_xml);
					m_formats |= WrappedValueFormats.Object;
				}
				catch (System.Exception)
				{
				}
			}

			if (m_wrapped != null)
			{
				try
				{
					m_string = m_wrapped.ToString();
					m_formats |= WrappedValueFormats.String;
				}
				catch (System.Exception)
				{
				}
			}
		}

		/// <summary>
		/// Run a delegate passed by the caller in the GenericWrapper's AppDomain. The delegate will receive
		/// this object and the "state" parameter passed by the caller. This allows caller code to perform
		/// tasks that access the value many times without marshaling across AppDomains every time.
		/// </summary>
		public object RunCustomWorker(WorkerDelegate worker, object state)
		{
			if (worker == null)
				throw new ArgumentNullException("worker");

			return worker(this, state);
		}

		private void AddFieldsAndProperties(MemberWrappers list)
		{
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
					  | BindingFlags.DeclaredOnly;

			// Create a list sorted by name.

			SortedList properties = new SortedList(new MemberNameComparer());

			// Add fields.

			foreach (FieldInfo field in m_type.GetFields(flags))
			{
				try
				{
					FieldWrapper wrapper;
					if (field.FieldType.IsValueType)
					{
						wrapper = new FieldWrapper(field, m_wrapped, m_rootReferenceObject, m_fieldPath);
					}
					else
					{
						wrapper = new FieldWrapper(field, m_wrapped, null, null);
					}

					properties.Add(wrapper.DisplayName, wrapper);
				}
				catch (System.Exception)
				{
				}
			}

			// Add properties.

			foreach (PropertyInfo property in m_type.GetProperties(flags))
			{
				try
				{
					PropertyWrapper wrapper = new PropertyWrapper(property, m_wrapped);
					properties.Add(wrapper.DisplayName, wrapper);
				}
				catch (System.Exception)
				{
				}
			}

			// Copy the values (now in sorted order) to the MemberWrappers collection.

			foreach (MemberWrapper wrapper in properties.Values)
			{
				list.Add(wrapper);
			}
		}

		private void AddArrayElements(MemberWrappers list)
		{
			Array array = (Array)m_wrapped;
			int rank = array.Rank;

			// Create an array of indices and initialise it to the lower bound for each dimension.

			int[] indices = new int[rank];
			for (int i = 0; i < indices.Length; i++)
			{
				indices[i] = array.GetLowerBound(i);
			}

			// Iterate over every value for every dimension, starting from the last, eg.:
			// [0,0,0]
			// [0,0,1]
			// [0,1,0]
			// [0,1,1]
			// ...

			int dimension = rank - 1;
			int count = 0;
			while (true)
			{
				if (indices[dimension] > array.GetUpperBound(dimension))
				{
					// We've gone past the upper bound of this dimension, so return to the last dimension
					// that can be incremented.

					while (dimension > 0 && indices[dimension] > array.GetUpperBound(dimension))
					{
						dimension--;
						indices[dimension]++;
					}

					if (dimension == 0 && indices[0] > array.GetUpperBound(0))
						break; // Reached the upper bound of the first dimesion - all done.

					// Re-initialise the indices for all dimesions after this one to the lower bound.

					for (int i = dimension + 1; i < indices.Length; i++)
					{
						indices[i] = array.GetLowerBound(i);
					}

					dimension = rank - 1; // Start iterating over the last dimension again.
				}

				// Create an ArrayElementWrapper using a copy of the indices array.

				int[] copyIndices = new int[rank];
				Array.Copy(indices, copyIndices, rank);
				count++;

				if (count > MaxListElements)
				{
					list.Add(new ListTooLongWrapper(copyIndices));
					break;
				}
				else
				{
					list.Add(new ArrayElementWrapper(array, copyIndices));
				}

				indices[dimension]++; // Go to the next element in this dimension.
			}
		}

		private void AddListElements(MemberWrappers list)
		{
			IList wrapped = (IList)m_wrapped;

			for (int index = 0; index < wrapped.Count && index < MaxListElements; index++)
			{
				list.Add(new ListElementWrapper(wrapped, index));
			}

			if (wrapped.Count > MaxListElements)
			{
				list.Add(new ListTooLongWrapper(MaxListElements));
			}
		}

		private void AddDictionaryElements(MemberWrappers list)
		{
			IDictionary dictionary = (IDictionary)m_wrapped;

			int count = 0;
			foreach (object key in dictionary.Keys)
			{
				if (++count > MaxListElements)
				{
					list.Add(new ListTooLongWrapper("too many entries"));
				}
				else
				{
					list.Add(new DictionaryElementWrapper(dictionary, key));
				}
			}
		}
	}
}
