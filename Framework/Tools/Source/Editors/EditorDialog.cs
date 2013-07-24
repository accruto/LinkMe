using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// A dialog that can display a control that implements IEditor, given the type.
	/// </summary>
	public class EditorDialog : TC.Dialog, IRemoteEditor
	{
		private static Size m_defaultMinimumSize = new Size(216, 104);

		private IEditor m_currentEditor = null;
		private Hashtable m_loadedEditors = new Hashtable();
		private bool m_readOnly = false;

		private System.ComponentModel.Container components = null;

		public EditorDialog()
		{
			InitializeComponent();
		}

		public EditorDialog(MessageBoxButtons buttons)
			: base(buttons)
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// EditorDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 397);
			this.Name = "EditorDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Editor";

		}
		#endregion

		#region IEditor Members

		public bool Modified
		{
			get { return (CurrentEditor == null ? false : CurrentEditor.Modified); }
		}

		public bool ReadOnly
		{
			get { return m_readOnly; }
			set
			{
				m_readOnly = value;

				foreach (IEditor editor in m_loadedEditors.Values)
				{
					editor.ReadOnly = ReadOnly;
				}

				EnableDisableOK();
			}
		}

		public bool SupportsEditing
		{
			get { return (CurrentEditor == null ? false : CurrentEditor.SupportsEditing); }
		}

		public void BeginEditNew()
		{
			if (CurrentEditor != null)
			{
				((Control)CurrentEditor).Focus();
				CurrentEditor.BeginEditNew();
			}
		}

		public bool CanDisplay(Type type)
		{
			return (CurrentEditor == null ? false : CurrentEditor.CanDisplay(type));
		}

		public void Clear()
		{
			if (CurrentEditor == null)
				throw new InvalidOperationException("Unable to clear the value, because there is no current editor.");

			CurrentEditor.Clear();
		}

		public void DisplayValue(object value)
		{
			if (CurrentEditor == null)
				throw new InvalidOperationException("Unable to display the value, because there is no current editor.");

			CurrentEditor.DisplayValue(value);
		}

		public object GetValue(Type type)
		{
			if (CurrentEditor == null)
				throw new InvalidOperationException("Unable to get the value, because there is no current editor.");

			return CurrentEditor.GetValue(type);
		}

		public object GetValue()
		{
			if (CurrentEditor == null)
				throw new InvalidOperationException("Unable to get the value, because there is no current editor.");

			return CurrentEditor.GetValue();
		}

		#endregion

		#region IRemoteEditor Members

		public WrappedValueFormats UsedFormats
		{
			get
			{
				IRemoteEditor remoteEditor = CurrentEditor as IRemoteEditor;
				return (remoteEditor == null ? WrappedValueFormats.None : remoteEditor.UsedFormats);
			}
		}

		public void DisplayRemoteValue(GenericWrapper wrapper)
		{
			if (CurrentEditor == null)
				throw new InvalidOperationException("Unable to display the value, because there is no current editor.");

			IRemoteEditor remoteEditor = CurrentEditor as IRemoteEditor;
			if (remoteEditor == null)
			{
				throw new InvalidOperationException("Unable to display a remote value, because the current editor"
					+ " is not a remote editor.");
			}

			remoteEditor.DisplayRemoteValue(wrapper);
		}

		public GenericWrapper GetRemoteValue()
		{
			if (CurrentEditor == null)
				throw new InvalidOperationException("Unable to get the value, because there is no current editor.");

			IRemoteEditor remoteEditor = CurrentEditor as IRemoteEditor;
			if (remoteEditor == null)
			{
				throw new InvalidOperationException("Unable to get a remote value, because the current editor"
					+ " is not a remote editor.");
			}

			return remoteEditor.GetRemoteValue();
		}

		#endregion

		[Browsable(false)]
		public Type CurrentEditorType
		{
			get { return (m_currentEditor == null ? null : m_currentEditor.GetType()); }
			set { CurrentEditor = (value == null ? null : LoadEditor(value)); }
		}

		[Browsable(false)]
		public bool IsRemoteEditor
		{
			get { return (m_currentEditor is IRemoteEditor); }
		}

		private IEditor CurrentEditor
		{
			get { return m_currentEditor; }
			set
			{
				if (m_currentEditor != null)
				{
					((Control)m_currentEditor).Visible = false;
				}

				m_currentEditor = value;

				if (m_currentEditor != null)
				{
					((Control)m_currentEditor).Visible = true;
				}

				EnableDisableOK();
				EnforceMinimumEditorSize(); // Make sure the form is big enough for the new editor.
			}
		}

		public void FocusOnEditor()
		{
			if (CurrentEditor != null)
			{
				ActiveControl = (Control)CurrentEditor;
			}
		}

		private void EnableDisableOK()
		{
			bool enabled = (CurrentEditor == null ? false : !ReadOnly && CurrentEditor.SupportsEditing);
			SetButtonEnabled(DialogResult.OK, enabled);
		}

		private IEditor LoadEditor(Type editorType)
		{
			Debug.Assert(editorType != null, "editorType != null");

			// Check if we already have an instance of this editor.

			IEditor loaded = (IEditor)m_loadedEditors[editorType];
			if (loaded != null)
				return loaded;

			// Create a new instance of the editor.

			IEditor editor = EditorManager.CreateEditorInstance(editorType);

			// Add the editor control to this control.

			Control editorControl = (Control)editor;
			Controls.Add(editorControl);
			editorControl.Location = new Point(8, 8);
			editorControl.Size = new Size(ClientSize.Width - 16, ClientSize.Height - 16
				- TC.Dialog.PaddingBottom - TC.Dialog.ButtonHeight);
			editorControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			editorControl.TabIndex = 0; // Focus on the editor when first shown, not the buttons.
			editor.ReadOnly = ReadOnly;

			// Add it to the collection of loaded editors.

			m_loadedEditors.Add(editorType, editor);

			return editor;
		}

		private void EnforceMinimumEditorSize()
		{
			int minimumWidth = m_defaultMinimumSize.Width;
			int minimumHeight = m_defaultMinimumSize.Height;

			if (TC.UserControl.GetMinimumSizeRecursive((Control)m_currentEditor, Size, ref minimumWidth, ref minimumHeight))
			{
				MinimumSize = new Size(minimumWidth, minimumHeight);
			}
			else
			{
				MinimumSize = m_defaultMinimumSize;
			}
		}
	}
}
