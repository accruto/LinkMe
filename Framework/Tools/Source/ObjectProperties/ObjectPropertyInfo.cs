using System;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	public abstract class ObjectPropertyInfo
		:	ElementPropertyInfo,
			IObjectPropertyInfo
	{
		private bool m_isEditMode = true;
		private ObjectPropertySettings m_settings;

		protected ObjectPropertyInfo(string imageResource, object element)
			:	base(null, imageResource, element)
		{
		}

		#region IObjectPropertyInfo Members

		public object Object
		{
			get { return Element; }
		}

		public abstract object CloneObject();

		public bool IsEditMode
		{
			get { return m_isEditMode; }
			set { m_isEditMode = value; }
		}

		public ObjectPropertySettings Settings
		{
			get { return m_settings; }
			set { m_settings = value; }
		}

		#endregion
	}
}
