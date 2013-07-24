using System;
using System.Drawing;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	/// <summary>
	/// A dialog that displays the Component Catalogue object propertySheet.
	/// </summary>
	public class ObjectPropertyForm
		:	Dialog
	{
		/// <summary>
		/// Raised when the object is changed in some way.
		/// </summary>
		public event ApplyEventHandler Apply;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private LinkMe.Framework.Tools.ObjectProperties.ObjectPropertySheet propertySheet;
		private Mode m_mode = Mode.Edit;

		private enum Mode
		{
			New,		// New object being created, show OK, Cancel and disabled Apply
			Edit,		// Existing object being edited, show OK, Cancel and Apply
			Prompt,		// Prompt for confirmation, read only, show OK and Cancel
			Form,		// Existing object being edited, show OK and Cancel
		}

		public ObjectPropertyForm(string text, string iconResource, bool isReadOnly)
			:	this(text, iconResource)
		{
			propertySheet.IsReadOnly = isReadOnly;
		}

		public ObjectPropertyForm(string text, string iconResource)
			:	base(HorizontalAlignment.Right, MessageBoxButtons.OKCancel, "&Apply")
		{
			InitializeComponent();
			SetIcon(iconResource);
			Text = text;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			this.propertySheet = new LinkMe.Framework.Tools.ObjectProperties.ObjectPropertySheet();
			this.SuspendLayout();
			// 
			// propertySheet
			// 
			this.propertySheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertySheet.IsReadOnly = false;
			this.propertySheet.Location = new System.Drawing.Point(8, 8);
			this.propertySheet.MinimumSize = new System.Drawing.Size(0, 0);
			this.propertySheet.Name = "propertySheet";
			this.propertySheet.Size = new System.Drawing.Size(642, 486);
			this.propertySheet.TabIndex = 101;
			this.propertySheet.Enable += new System.EventHandler(this.propertySheet_Enable);
			this.propertySheet.PageAdded += new System.EventHandler(this.propertySheet_PageAdded);
			this.propertySheet.Load += new System.EventHandler(this.propertySheet_Load);
			this.propertySheet.Disable += new System.EventHandler(this.propertySheet_Disable);
			this.propertySheet.ElementChanged += new System.EventHandler(this.propertySheet_ElementChanged);
			// 
			// ObjectPropertyForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(658, 543);
			this.Controls.Add(this.propertySheet);
			this.Name = "ObjectPropertyForm";
			this.Text = "Object Properties";
			this.ResumeLayout(false);

		}
		#endregion

		public object Object
		{
			get { return propertySheet.Object; }
		}

		public DialogResult ShowNew()
		{
			// When showing new it can never be read only.

			m_mode = Mode.New;
			propertySheet.IsReadOnly = false;
			propertySheet.ObjectInfo.IsEditMode = false;
			return ShowDialog();
		}

		public DialogResult ShowPrompt()
		{
			// Reset the buttons for the choice.

			ResetButtons(HorizontalAlignment.Center, MessageBoxButtons.OKCancel, null);

			// Do not allow changes in the properties.

			m_mode = Mode.Prompt;
			propertySheet.IsReadOnly = true;
			propertySheet.ObjectInfo.IsEditMode = false;
			return ShowDialog();
		}

		public DialogResult ShowForm()
		{
			// Reset the buttons for the choice.

			ResetButtons(HorizontalAlignment.Center, MessageBoxButtons.OKCancel, null);

			// Do not allow changes in the properties.

			m_mode = Mode.Form;
			propertySheet.IsReadOnly = false;
			propertySheet.ObjectInfo.IsEditMode = true;
			return ShowDialog();
		}

		protected void Initialize(ObjectPropertySettings settings, IObjectPropertyInfo objectInfo, object selectedElement, string iconResource)
		{
			objectInfo.Settings = settings;
			propertySheet.Settings = settings;
			propertySheet.ObjectInfo = objectInfo;
			propertySheet.InitialSelectedElement = selectedElement;
			if ( iconResource != null )
				SetIcon(iconResource);
		}

		protected void Initialize(ObjectPropertySettings settings, IObjectPropertyInfo objectInfo, object selectedElement)
		{
			Initialize(settings, objectInfo, selectedElement, null);
		}

		protected void Initialize(ObjectPropertySettings settings, IObjectPropertyInfo objectInfo)
		{
			Initialize(settings, objectInfo, null, null);
		}

		private void propertySheet_ElementChanged(object sender, System.EventArgs e)
		{
			if ( m_mode != Mode.Prompt )
				Text = propertySheet.ObjectInfo.Name + " Properties";
			UpdateButtons();
		}

		private void propertySheet_Enable(object sender, System.EventArgs e)
		{
			UpdateButtons(false, true);
		}

		private void propertySheet_Disable(object sender, System.EventArgs e)
		{
			UpdateButtons(false, false);
		}

		private void propertySheet_PageAdded(object sender, System.EventArgs e)
		{
			// When the page is first added update the buttons.

			UpdateButtons();
		}

		protected override bool OnClick(DialogResult result)
		{
			if ( result == DialogResult.OK )
			{
				if ( !OnApply() )
					return false;
			}

			return true;
		}

		protected override bool OnClick(string name)
		{
			if ( name == "Apply" )
			{
				if ( !OnApply() )
					return false;
				UpdateButtons();
			}

			return true;
		}

		private bool OnApply()
		{
			// Only try to do something if it is not read only.

			if ( !propertySheet.IsReadOnly )
			{
				if ( !propertySheet.OnApply() )
					return false;

				// Only fire the event if in edit mode.

				if ( m_mode == Mode.Edit )
					OnApply(new ApplyEventArgs(propertySheet.ObjectInfo.Object));
			}

			return true;
		}

		private void OnApply(ApplyEventArgs e)
		{
			if ( Apply != null )
				Apply(this, e);
		}

		private void UpdateButtons()
		{
			UpdateButtons(true, true);
		}

		private void propertySheet_Load(object sender, System.EventArgs e)
		{
			UpdateButtons();
		}

		private void UpdateButtons(bool checkIsChanged, bool enable)
		{
			if ( propertySheet.IsReadOnly )
			{
				// Can't change anything.

				SetButtonEnabled("Apply", false);
				if ( m_mode == Mode.Edit )
					SetButtonEnabled(DialogResult.OK, false);
				else if ( m_mode == Mode.Prompt )
					SetButtonEnabled(DialogResult.OK, true);
			}
			else
			{
				switch ( m_mode )
				{
					case Mode.Edit:

						// Update the apply button depending on the state of the object.

						SetButtonEnabled("Apply", propertySheet.IsValid && (checkIsChanged ? propertySheet.IsChanged : enable));

						// Disable the OK button until the data in the property sheet 
						// is complete.

						SetButtonEnabled(DialogResult.OK, propertySheet.IsValid);
						break;

					case Mode.Form:

						// OK should be set if everything is valid.

						SetButtonEnabled(DialogResult.OK, propertySheet.IsValid);
						break;

					default:

						// Creating a new object so apply is always disabled.

						SetButtonEnabled("Apply", false);

						// OK should be set if everything is valid.

						SetButtonEnabled(DialogResult.OK, propertySheet.IsValid);
						break;
				 }
			}
		}

		private void SetIcon(string iconResource)
		{
			object image = Images.FindImage(iconResource);
			if ( image != null )
			{
				Icon icon = image as Icon;
				if ( icon == null )
					throw new ApplicationException(string.Format("A resource named '{0}' was loaded successfully, but its type is '{1}' when an Icon is expected.", iconResource, image.GetType().FullName));

				Icon = icon;
			}
		}
	}
}
