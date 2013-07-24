using System;
using System.Diagnostics;
using System.Reflection;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Wraps a field obtained from a GenericWrapper.
	/// </summary>
	internal class FieldWrapper : MemberWrapper
	{
		private FieldInfo m_info;
		private object m_instance;
		private object m_rootReferenceObject;
		private FieldInfo[] m_fieldPath;

		internal FieldWrapper(FieldInfo info, object instance, object rootReferenceObject, FieldInfo[] fieldPath)
		{
			Debug.Assert(info != null, "info != null");

			/*
			 * The assertion below should succeed if the user only expands fields, but will fail for properties.
			 * A field on a value type that was obtained via a property cannot be changed (since the property
			 * returned a copy of the value-type object). In that case rootReferenceObject SHOULD be null and the
			 * field will appear as read-only to the user.

			Debug.Assert(!(info.FieldType.IsValueType && info.DeclaringType.IsValueType && rootReferenceObject == null),
				string.Format("Field '{0}' on type '{1}' is a value field (type '{2}') on a value type, but"
				+ " rootReferenceObject is null.", info.Name, info.DeclaringType.FullName, info.FieldType.FullName));
			*/

			m_info = info;
			m_instance = instance;
			m_rootReferenceObject = rootReferenceObject;
			m_fieldPath = fieldPath;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get
			{
				if (m_info.IsLiteral)
					return false;
				else if (m_info.FieldType.IsValueType && m_info.DeclaringType.IsValueType)
				{
					if (m_rootReferenceObject == null || m_info.IsStatic || m_info.IsInitOnly)
						return false;

					if (m_fieldPath != null)
					{
						foreach (FieldInfo info in m_fieldPath)
						{
							if (info.IsStatic || info.IsInitOnly)
								return false;
						}
					}

					return true;
				}
				else
					return true;
			}
		}

		public override string Name
		{
			get { return m_info.Name; }
		}

		public override GenericWrapper CreateWrapper()
		{
			object val = GetValue();
			if (val == null)
				return null;

			if (m_info.FieldType.IsValueType)
			{
				// The field is a value type, so we need a way to get back to a reference object to set it.

				if (m_fieldPath == null)
					return new GenericWrapper(val, m_instance, new FieldInfo[] { m_info });
				else
				{
					Debug.Assert(m_rootReferenceObject != null, "m_rootReferenceObject != null");

					FieldInfo[] fieldPath = new FieldInfo[m_fieldPath.Length + 1];
					Array.Copy(m_fieldPath, 0, fieldPath, 0, m_fieldPath.Length);
					fieldPath[fieldPath.Length - 1] = m_info;

					return new GenericWrapper(val, m_rootReferenceObject, fieldPath);
				}
			}
			else
			{
				// This instance of GenericWrapper won't need the "root reference object", but one of its fields
				// may (if it's a value type).

				object rootReferenceObject = (m_fieldPath == null ? m_instance : m_rootReferenceObject);
				return new GenericWrapper(val, rootReferenceObject, null);
			}
		}

		public override Type GetMemberType()
		{
			return m_info.FieldType;
		}

		protected override object GetValueImpl()
		{
			return m_info.GetValue(m_instance);
		}

		protected override void SetValueImpl(object value)
		{
			if (m_info.FieldType.IsValueType && m_info.DeclaringType.IsValueType)
			{
				Debug.Assert(m_rootReferenceObject != null, "m_rootReferenceObject != null");

				// The type that contains this field is a value type and the field itself is a value type,
				// so we cannot simply call SetValue() - that would only change it on our copy of the object,
				// not on the "real" one. Its parent may also be a value type and so on. We need to get a managed
				// pointer to this field from the first reference object up the hierarchy and call SetValueDirect()
				// on it. See http://www.codeproject.com/dotnet/pointers.asp for more information.

				// TODO: MakeTypedReference doesn't handle static or InitOnly fields - do something about those.
				// Can we replace the whole value object (startring from the property under m_rootReferenceObject)?
				// Also remember to change CanWrite [ set ] implementation.

				TypedReference refRoot = TypedReference.MakeTypedReference(m_rootReferenceObject, m_fieldPath);
				m_info.SetValueDirect(refRoot, value);
			}
			else
			{
				m_info.SetValue(m_instance, value);
			}
		}
	}
}
