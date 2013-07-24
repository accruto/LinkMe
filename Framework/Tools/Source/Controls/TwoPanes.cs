using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// Encapsulates two panels and a splitter and enables the panels to be hidden, maximised/restored and resized
	/// automatically as the this control is resized. Inherit from this control to use the panels.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "TwoPanes.bmp")]
	public class TwoPanes : UserControl
	{
		public event DisplayModeChangedEventHandler DisplayModeChanged;
		public event EventHandler SplitRatioChanged;

		private const bool m_defaultPaneOneVisible = true;
		private const bool m_defaultPaneTwoVisible = true;

		// The splitter must have one of the panes visible at all times (when we hide the first pane it
		// automatically shows the second one and vice versa), so this control must also keep track of which
		// panes are visible - it cannot rely on Splitter.FirstPaneVisible and Splitter.SecondPaneVisible.

		private bool m_paneOneVisible = m_defaultPaneOneVisible;
		private bool m_paneTwoVisible = m_defaultPaneTwoVisible;
		private int m_maximisedPane = 0;
		private IList m_changeModeControls = new ArrayList();

		protected System.Windows.Forms.Panel PaneOne;
		protected System.Windows.Forms.Panel PaneTwo;

		private LinkMe.Framework.Tools.Controls.Splitter split;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TwoPanes()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			split.Initialise();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PaneOne = new System.Windows.Forms.Panel();
			this.split = new LinkMe.Framework.Tools.Controls.Splitter();
			this.PaneTwo = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// PaneOne
			// 
			this.PaneOne.Dock = System.Windows.Forms.DockStyle.Top;
			this.PaneOne.Location = new System.Drawing.Point(0, 0);
			this.PaneOne.Name = "PaneOne";
			this.PaneOne.Size = new System.Drawing.Size(728, 173);
			this.PaneOne.TabIndex = 1;
			// 
			// split
			// 
			this.split.Dock = System.Windows.Forms.DockStyle.Top;
			this.split.FirstPaneVisible = true;
			this.split.Location = new System.Drawing.Point(0, 173);
			this.split.MaintainSplitterRatio = true;
			this.split.MinExtra = 30;
			this.split.MinSize = 30;
			this.split.Name = "split";
			this.split.SecondPaneVisible = true;
			this.split.Size = new System.Drawing.Size(728, 6);
			this.split.TabIndex = 4;
			this.split.TabStop = false;
			this.split.MaximisedPaneHidden += new System.EventHandler(this.split_MaximisedPaneHidden);
			this.split.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.split_SplitterMoved);
			// 
			// PaneTwo
			// 
			this.PaneTwo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PaneTwo.Location = new System.Drawing.Point(0, 179);
			this.PaneTwo.Name = "PaneTwo";
			this.PaneTwo.Size = new System.Drawing.Size(728, 173);
			this.PaneTwo.TabIndex = 5;
			// 
			// TwoPanes
			// 
			this.Controls.Add(this.PaneTwo);
			this.Controls.Add(this.split);
			this.Controls.Add(this.PaneOne);
			this.Name = "TwoPanes";
			this.Size = new System.Drawing.Size(728, 352);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// True to show the first (top) pane, false to hide it. The default is true.
		/// </summary>
		[DefaultValue(m_defaultPaneOneVisible)]
		public bool PaneOneVisible
		{
			get { return m_paneOneVisible; }
			set
			{
				m_paneOneVisible = value;
				split.FirstPaneVisible = value;
				PaneOne.Visible = value;
			}
		}

		/// <summary>
		/// True to show the second (bottom) pane, false to hide it. The default is true.
		/// </summary>
		[DefaultValue(m_defaultPaneTwoVisible)]
		public bool PaneTwoVisible
		{
			get { return m_paneTwoVisible; }
			set
			{
				m_paneTwoVisible = value;
				split.SecondPaneVisible = value;
				PaneTwo.Visible = value;
			}
		}

		/// <summary>
		/// The minimum size of the first (top) pane. The default is 30.
		/// </summary>
		[DefaultValue(Splitter.DefaultMinSize)]
		public int PaneOneMinSize
		{
			get { return split.MinSize; }
			set { split.MinSize = value; }
		}

		/// <summary>
		/// The minimum size of the second (bottom) pane. The default is 30.
		/// </summary>
		[DefaultValue(Splitter.DefaultMinExtra)]
		public int PaneTwoMinSize
		{
			get { return split.MinExtra; }
			set { split.MinExtra = value; }
		}

		/// <summary>
		/// The border style of the splitter between the panes.
		/// </summary>
		[DefaultValue(Splitter.DefaultBorderStyle)]
		public BorderStyle SplitterBorderStyle
		{
			get { return split.BorderStyle; }
			set { split.BorderStyle = value; }
		}

		/// <summary>
		/// The ratio of the size of the first pane to the size of the second pane.
		/// </summary>
		public float SplitRatio
		{
			get { return (m_paneOneVisible && m_paneTwoVisible ? split.SplitRatio : -1); }
			set
			{
				if (m_paneOneVisible && m_paneTwoVisible)
				{
					split.SplitRatio = value;
				}
			}
		}

		/// <summary>
		/// The parent control should call this function after all its children (and their
		/// children) have been added.
		/// </summary>
		public void Initialise()
		{
			InitialisePane(PaneOne);
			InitialisePane(PaneTwo);
		}

		public void MaximisePaneOne()
		{
			if (!PaneOneVisible)
				throw new InvalidOperationException("Pane one cannot be maximised,"
					+ " because it is not visible.");
			if (m_maximisedPane == 1)
				throw new InvalidOperationException("Pane one cannot be maximised,"
					+ " because it is already maximised.");

			split.MaximiseFirstPane();
			m_maximisedPane = 1;
		}

		public void MaximisePaneTwo()
		{
			if (!PaneTwoVisible)
				throw new InvalidOperationException("Pane two cannot be maximised,"
					+ " because it is not visible.");
			if (m_maximisedPane == 2)
				throw new InvalidOperationException("Pane two cannot be maximised,"
					+ " because it is already maximised.");

			split.MaximiseSecondPane();
			m_maximisedPane = 2;
		}
	
		public void RestorePanes()
		{
			split.RestorePanes();
			m_maximisedPane = 0;
		}

		protected virtual void OnDisplayModeChanged(DisplayModeChangedEventArgs e)
		{
			if (DisplayModeChanged != null)
			{
				DisplayModeChanged(this, e);
			}
		}

		protected virtual void OnSplitRatioChanged(EventArgs e)
		{
			if (SplitRatioChanged != null)
			{
				SplitRatioChanged(this, e);
			}
		}

		private void InitialisePane(Panel pane)
		{
			foreach (Control control in pane.Controls)
			{
				AddContainedControlEventHandler(control, GetHandlerForPane(pane));
			}

			pane.ControlAdded += new ControlEventHandler(Pane_ControlAdded);
			pane.ControlRemoved += new ControlEventHandler(Pane_ControlRemoved);
		}

		private void AddContainedControlEventHandler(Control control,
			DisplayModeChangedEventHandler handler)
		{
			IChangeDisplayMode changeModeControl = control as IChangeDisplayMode;
			if (changeModeControl != null)
			{
				changeModeControl.DisplayModeChanged += new DisplayModeChangedEventHandler(handler);
				m_changeModeControls.Add(changeModeControl);
			}

			foreach (Control child in control.Controls)
			{
				AddContainedControlEventHandler(child, handler);
			}
		}

		private void RemoveContainedControlEventHandler(Control control,
			DisplayModeChangedEventHandler handler)
		{
			IChangeDisplayMode changeModeControl = control as IChangeDisplayMode;
			if (changeModeControl != null)
			{
				changeModeControl.DisplayModeChanged -= new DisplayModeChangedEventHandler(handler);
				m_changeModeControls.Remove(changeModeControl);
			}

			foreach (Control child in control.Controls)
			{
				RemoveContainedControlEventHandler(child, handler);
			}
		}

		private void Pane_ControlAdded(object sender, ControlEventArgs e)
		{
			AddContainedControlEventHandler(e.Control, GetHandlerForPane(sender));
		}

		private void Pane_ControlRemoved(object sender, ControlEventArgs e)
		{
			RemoveContainedControlEventHandler(e.Control, GetHandlerForPane(sender));
		}

		private void paneOne_DisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
		{
			if (e.FullSize)
			{
				MaximisePaneOne();
			}
			else
			{
				RestorePanes();
			}

			OnDisplayModeChanged(e);
		}

		private void paneTwo_DisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
		{
			if (e.FullSize)
			{
				MaximisePaneTwo();
			}
			else
			{
				RestorePanes();
			}

			OnDisplayModeChanged(e);
		}
	
		private DisplayModeChangedEventHandler GetHandlerForPane(object pane)
		{
			if (pane == PaneOne)
				return new DisplayModeChangedEventHandler(paneOne_DisplayModeChanged);
			if (pane == PaneTwo)
				return new DisplayModeChangedEventHandler(paneTwo_DisplayModeChanged);

			throw new ArgumentException("Unexpected value passed to GetHandlerForPane(): '"
				+ pane.ToString() + "'.");
		}

		private void split_MaximisedPaneHidden(object sender, System.EventArgs e)
		{
			// Force a restore and let all our controls know

			RestorePanes();

			foreach (IChangeDisplayMode changeModeControl in m_changeModeControls)
			{
				changeModeControl.OnForcedRestore();
			}
		}

		private void split_SplitterMoved(object sender, SplitterEventArgs e)
		{
			OnSplitRatioChanged(EventArgs.Empty);
		}
	}
}
