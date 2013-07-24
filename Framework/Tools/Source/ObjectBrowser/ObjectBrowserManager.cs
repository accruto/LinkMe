using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	/// <summary>
	/// Base class for objects that can be used to create and cache object browser elements. A separate
	/// manager object should be created for each browser instance, while the settings object may be shared
	/// between instances.
	/// </summary>
	public abstract class ObjectBrowserManager : MarshalByRefObject, IDisposable
	{
		private ObjectBrowserSettings m_settings = null;

		protected ObjectBrowserManager()
		{
		}

		#region IDisposable Members

		public virtual void Dispose()
		{
			ClearCache();
			m_settings = null;
		}

		#endregion

		/// <summary>
		/// The settings object to be used by this manager. The object browser sets this value once it receives
		/// both the manager and the settings object.
		/// </summary>
		public ObjectBrowserSettings Settings
		{
			get { return m_settings; }
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>
		/// Clear any cached IElementInfo objects.
		/// </summary>
		protected internal virtual void ClearCache()
		{
		}

		/// <summary>
		/// Called by the object browser when the ShowMembers setting changes.
		/// </summary>
		protected internal virtual void OnShowMembersChanged()
		{
		}

		/// <summary>
		/// Called by the object browser when the TypeOrder setting changes.
		/// </summary>
		protected internal virtual void OnTypeOrderChanged()
		{
		}

		/// <summary>
		/// Called by the object browser when the MemberOrder setting changes.
		/// </summary>
		protected internal virtual void OnMemberOrderChanged()
		{
		}

		/// <summary>
		/// Called by the object browser when the ShowNonPublic setting changes.
		/// </summary>
		protected internal virtual void OnShowNonPublicChanged()
		{
		}

		/// <summary>
		/// Called by the object browser to re-connect to the specified repository, if supported. The base
		/// implementation simply returns the same object that was passed in.
		/// </summary>
		protected internal virtual IRepositoryBrowserInfo RefreshRepository(IRepositoryBrowserInfo repository)
		{
			return repository;
		}

		/// <summary>
		/// Called by the object browser when an object is dragger over it.
		/// </summary>
		protected internal virtual DragDropEffects OnDragOver(DragDropEffects allowedEffect, int keyState,
			IDataObject data)
		{
			return DragDropEffects.None;
		}

		/// <summary>
		/// Called by the object browser when an object is dropped onto it.
		/// </summary>
		protected internal virtual IRepositoryBrowserInfo[] OnDragDrop(DragDropEffects effect, int keyState,
			IDataObject data)
		{
			return null;
		}

		protected abstract bool SettingsObjectAcceptable(ObjectBrowserSettings settings);

		internal virtual void SetSettings(ObjectBrowserSettings value)
		{
			if (value != null && ! SettingsObjectAcceptable(value))
			{
				throw new ArgumentException(string.Format("The object browser manager and settings objects are"
					+ " incompatible. The manager object type is '{0}' and the settings object type is '{1}'.",
					GetType().FullName, value.GetType().FullName));
			}

			m_settings = value;
		}
	}
}
