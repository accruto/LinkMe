using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using LinkMe.Framework.Utility;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A form that implements IFormIsClosing to enable users to query whether it's currently being closed and
	/// provides a simple Window menu with a list of MDI children.
	/// </summary>
	public class Form : System.Windows.Forms.Form, IFormIsClosing
	{
		private bool m_closing = false;
		private MainMenu m_mainMenu = null;
		private int m_windowMenuIndex = -1;
		private int m_mergeOrder = -1;
		private MenuItem m_mnuWindow = null;
		private MenuItem m_mnuWindowCloseAll = null;
		private MenuItem m_mnuWindowCascade = null;
		private MenuItem m_mnuWindowTileHorizontal = null;
		private MenuItem m_mnuWindowTileVertical = null;


		public Form()
		{
		}

		#region IFormIsClosing Members

		[Browsable(false)]
		public bool IsClosing
		{
			get
			{
				if (m_closing)
					return true;
				else
				{
					IFormIsClosing parent = ParentForm as IFormIsClosing;
					return (parent == null ? false : parent.IsClosing);
				}
			}
		}

		#endregion

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Constants.Win32.Messages.WM_CLOSE)
			{
				m_closing = true;
			}

			base.WndProc(ref m);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			m_closing = !e.Cancel;
		}

		protected virtual void EnforceUserControlMinimumSize()
		{
			int minimumWidth = MinimumSize.Width;
			int minimumHeight = MinimumSize.Height;

			bool minimumSizeFound = false;
			foreach (Control childControl in Controls)
			{
				minimumSizeFound = UserControl.GetMinimumSizeRecursive(childControl, Size,
					ref minimumWidth, ref minimumHeight) || minimumSizeFound;
			}

			MinimumSize = (minimumSizeFound ? new Size(minimumWidth, minimumHeight) : Size.Empty);
		}

		protected override void OnMdiChildActivate(EventArgs e)
		{
			// The Window menu which contains a list of MDI child forms needs special treatment. It's destroyed
			// and re-created whenever an MDI child form is closed to work around a bug in WinForms that keeps a
			// reference to the form. For more information see
			// http://lab.msdn.microsoft.com/productfeedback/viewfeedback.aspx?feedbackid=de299fd0-9030-4d0a-8d44-49ae5ded64ce

			if (m_windowMenuIndex != -1)
			{
				// Without the line below a NullReferenceException occurs in Unknown Module when debugging.
				Debug.Write(string.Empty);

				DeleteWindowMenu();
				CreateWindowMenu();
			}

			base.OnMdiChildActivate(e);
		}

		protected void AddWindowMenu(MainMenu mainMenu, int index)
		{
			AddWindowMenu(mainMenu, index, 0);
		}

		protected void AddWindowMenu(MainMenu mainMenu, int index, int mergeOrder)
		{
			if (mainMenu == null)
				throw new ArgumentNullException("mainMenu");
			if (m_windowMenuIndex != -1)
				throw new InvalidOperationException("A Window menu has already been added.");

			m_mainMenu = mainMenu;
			m_windowMenuIndex = index;
			m_mergeOrder = mergeOrder;

			CreateWindowMenu();
		}

		protected virtual bool IsCloseAllEnabled()
		{
			return true;
		}

		private void DeleteWindowMenu()
		{
			if (m_mnuWindow != null)
			{
				Debug.Assert(m_mainMenu != null, "m_mainMenu != null");

				m_mnuWindow.Dispose();

				m_mainMenu.MenuItems.Remove(m_mnuWindow);

				m_mnuWindow = null;
				m_mnuWindowCloseAll = null;
				m_mnuWindowCascade = null;
				m_mnuWindowTileHorizontal = null;
				m_mnuWindowTileVertical = null;
			}
		}

		private void CreateWindowMenu()
		{
			Debug.Assert(m_mainMenu != null, "m_mainMenu != null");

			m_mnuWindow = new MenuItem();
			m_mnuWindowCloseAll = new MenuItem();
			m_mnuWindowCascade = new MenuItem();
			m_mnuWindowTileHorizontal = new MenuItem();
			m_mnuWindowTileVertical = new MenuItem();

			m_mainMenu.MenuItems.Add(m_windowMenuIndex, m_mnuWindow);

			m_mnuWindow.Index = m_windowMenuIndex;
			m_mnuWindow.MdiList = true;
			m_mnuWindow.MergeOrder = m_mergeOrder;
			m_mnuWindow.MenuItems.AddRange(new MenuItem[] { m_mnuWindowCloseAll, m_mnuWindowCascade,
				m_mnuWindowTileHorizontal, m_mnuWindowTileVertical });
			m_mnuWindow.Text = "&Window";
			m_mnuWindow.Popup += new EventHandler(mnuWindow_Popup);

			m_mnuWindowCloseAll.Index = 0;
			m_mnuWindowCloseAll.Text = "C&lose All";
			m_mnuWindowCloseAll.Click += new EventHandler(mnuWindowCloseAll_Click);

			m_mnuWindowCascade.Index = 1;
			m_mnuWindowCascade.Text = "&Cascade";
			m_mnuWindowCascade.Click += new EventHandler(m_mnuWindowCascade_Click);

			m_mnuWindowTileHorizontal.Index = 2;
			m_mnuWindowTileHorizontal.Text = "Tile &Horizontal";
			m_mnuWindowTileHorizontal.Click += new EventHandler(m_mnuWindowTileHorizontal_Click);

			m_mnuWindowTileVertical.Index = 3;
			m_mnuWindowTileVertical.Text = "Tile &Vertical";
			m_mnuWindowTileVertical.Click += new EventHandler(m_mnuWindowTileVertical_Click);
		}

		private void mnuWindow_Popup(object sender, EventArgs e)
		{
			bool enable = (MdiChildren.Length > 0);

			m_mnuWindowCloseAll.Enabled = enable && IsCloseAllEnabled();
			m_mnuWindowCascade.Enabled = enable;
			m_mnuWindowTileHorizontal.Enabled = enable;
			m_mnuWindowTileVertical.Enabled = enable;
		}

		private void mnuWindowCloseAll_Click(object sender, EventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}

		private void m_mnuWindowCascade_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.Cascade);
		}

		private void m_mnuWindowTileHorizontal_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileHorizontal);
		}
	
		private void m_mnuWindowTileVertical_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileVertical);
		}
	}
}
