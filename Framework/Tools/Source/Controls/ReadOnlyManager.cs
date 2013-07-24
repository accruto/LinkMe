using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Specialized;

namespace LinkMe.Framework.Tools.Controls
{
	public interface IReadOnlySettable
	{
		bool ReadOnly { get; set; }
	}

	public class ReadOnlyManager
		:	Component,
		ISupportInitialize
	{
		public ReadOnlyManager()
		{
		}

		public ReadOnlyManager(Control control)
		{
			m_control = control;
		}

		#region ISupportInitialize Members

		public void BeginInit()
		{
		}

		public void EndInit()
		{
		}

		#endregion
		
		#region Properties

		/// <summary>
		/// The container control to which the read-only setting is applied.
		/// </summary>
		public Control Control
		{
			get { return m_control; }
			set { m_control = value; }
		}

		#endregion

		#region Static methods

		private static void UpdateReadOnlyDefault(Control control)
		{
			if ( control is System.Windows.Forms.TextBox )
			{
				// Set the text box to read only.

				((System.Windows.Forms.TextBox) control).ReadOnly = true;
			}
			else if ( control is System.Windows.Forms.RichTextBox )
			{
				((System.Windows.Forms.RichTextBox) control).ReadOnly = true;
			}

			else if ( control is System.Windows.Forms.Label )
			{
				// Do nothing, leave it enabled.
			}
			else if ( !(control is System.Windows.Forms.GroupBox) )
			{
				// Disable everything else if it has no children.

				if ( control.Controls.Count == 0 )
					control.Enabled = false;
			}
		}

		#endregion

		public void SetReadOnly()
		{
			if ( m_control == null )
				return;

			SetReadOnlyInternal(m_control);
		}

		public void SetReadOnly(Control control)
		{
			if ( control == null )
				return;

			SetReadOnlyInternal(control);
		}

		protected virtual bool UpdateReadOnly(Control control)
		{
			return false;
		}

		private void SetReadOnlyInternal(Control control)
		{
			if ( UpdateReadOnly(control) )
			{
				// Iterate.

				foreach ( Control childControl in control.Controls )
					SetReadOnly(childControl);
			}
			else if ( control is IReadOnlySettable )
			{
				// Let the control itself take care of making its children read-only.

				((IReadOnlySettable) control).ReadOnly = true;
			}
			else
			{
				UpdateReadOnlyDefault(control);

				// Iterate.

				foreach ( Control childControl in control.Controls )
					SetReadOnly(childControl);
			}
		}

		private Control m_control = null;
	}
}
