using System;
using System.ComponentModel;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// Base implementation of IEditor. This class should be abstract, but that would prevent the
	/// Windows Forms Designer from working.
	/// </summary>
	public class Editor : UserControl, IEditor
	{
		protected const bool DefaultReadOnly = false;

		private bool m_readOnly = DefaultReadOnly;

		protected Editor()
		{
		}

		#region IEditor Members

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Modified
		{
			get { return false; }
		}

		[DefaultValue(DefaultReadOnly)]
		public virtual bool ReadOnly
		{
			get { return (SupportsEditing ? m_readOnly : true); }
			set { m_readOnly = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool SupportsEditing
		{
			get { return true; }
		}

		public virtual void BeginEditNew()
		{
		}

		public virtual void Clear()
		{
		}

		public virtual bool CanDisplay(System.Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			return false;
		}

		public virtual void DisplayValue(object value)
		{
		}

		public virtual object GetValue(System.Type type)
		{
			// If the derived class does not override this method then it should override GetValue()
			// without parameters. Call that method, but check that the type is what the caller expects.

			object value = GetValue();

			if (type != null && value != null && !type.IsAssignableFrom(value.GetType()))
			{
				throw new ApplicationException(string.Format("GetValue() was called on a '{0}' editor with"
					+ " type '{1}', but a value of type '{2}' was returned by the editor.",
					GetType().FullName, type.FullName, value.GetType().FullName));
			}

			return value;
		}

		public virtual object GetValue()
		{
			throw new NotSupportedException("GetValue() was called on a '" + GetType().FullName
				+ "' editor, but is not supported by the editor. Try passing a type to the GetValue() method.");
		}

		#endregion

		protected void CheckValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "The '" + GetType().FullName
					+ "' editor cannot display a null value.");
			}

			if (!CanDisplay(value.GetType()))
			{
				throw new ArgumentException("The '" + GetType().FullName
					+ "' editor cannot display a value of type '" + value.GetType().FullName + "'.", "value");
			}
		}
	}
}
