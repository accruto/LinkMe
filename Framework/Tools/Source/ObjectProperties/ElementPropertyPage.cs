using System;
using System.ComponentModel;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;
using UserControl = LinkMe.Framework.Tools.Controls.UserControl;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	public class ElementPropertyPage
		:	UserControl,
			IPropertyPageParent
	{
		public event EventHandler ElementChanged;

		public ElementPropertyPage()
		{
			InitializeComponent();
		}

		#region IPropertyPageParent Members

		bool IPropertyPageParent.IsReadOnly
		{
			get { return IsReadOnly; }
		}

		#endregion

		[Browsable(false)]
		public IPropertyPageParent PropertyPageParent
		{
			get { return m_parent; }
			set { m_parent = value; }
		}

		#region Virtual Methods

		[Browsable(false)]
		public virtual bool IsValid
		{
			get { return true; }
		}

		public virtual void CheckIsValid()
		{
		}

		public virtual void SelectInvalid()
		{
		}

		public void Refresh(IElementPropertyInfo elementInfo)
		{
			OnRefresh(elementInfo.Element);
		}

		public void Refresh(object element)
		{
			OnRefresh(element);
			CheckIsValid();
		}

		public void Save()
		{
			OnSave();
		}

		protected override void OnLoad(EventArgs e)
		{
			// Check whether to set controls to read only.

			if ( IsReadOnly )
				readOnlyManager.SetReadOnly();
		}

		internal protected virtual void OnRefresh(object element)
		{
		}

		internal protected virtual void OnSave()
		{
		}

		internal protected virtual object OnDeselected()
		{
			return null;
		}

		internal protected virtual void OnSelected(object data)
		{
		}

		#endregion

		protected void OnElementChanged()
		{
			if ( ElementChanged != null )
				ElementChanged(this, EventArgs.Empty);

			// Let the property sheet know as well.

			ObjectPropertySheet propertySheet = m_parent as ObjectPropertySheet;
			if ( propertySheet != null )
				propertySheet.OnElementChanged();
		}

		protected void OnEnable()
		{
			ObjectPropertySheet propertySheet = m_parent as ObjectPropertySheet;
			if ( propertySheet != null )
				propertySheet.OnEnable();
		}

		protected void OnDisable()
		{
			ObjectPropertySheet propertySheet = m_parent as ObjectPropertySheet;
			if ( propertySheet != null )
				propertySheet.OnDisable();
		}

		protected void SetReadOnly(Control control)
		{
			readOnlyManager.SetReadOnly(control);
		}

		protected bool IsEditMode
		{
			get { return true; }
		}

		protected bool IsReadOnly
		{
			get
			{
				if ( m_parent != null )
					return m_parent.IsReadOnly;
				else
					return false;
			}
		}

		private void InitializeComponent()
		{
			this.readOnlyManager = new ReadOnlyManager();
			// 
			// errorProvider1
			// 
			this.readOnlyManager.Control = this;
			// 
			// ElementPropertyPage
			// 
			this.Name = "ElementPropertyPage";

		}

		private ReadOnlyManager readOnlyManager;
		private IPropertyPageParent m_parent;
	}
}
