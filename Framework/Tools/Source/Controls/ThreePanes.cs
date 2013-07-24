using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// Encapsulates three panels and a splitter and enables the panels to be hidden, maximised/restored and resized
	/// automatically as the this control is resized. Inherit from this control to use the panels.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "ThreePanes.bmp")]
	public class ThreePanes : UserControl
	{
		public event DisplayModeChangedEventHandler DisplayModeChanged;
		public event EventHandler SplitRatioTopChanged;
		public event EventHandler SplitRatioBottomChanged;

		private const bool m_defaultPaneOneVisible = true;
		private const bool m_defaultPaneTwoVisible = true;
		private const bool m_defaultPaneThreeVisible = true;

		protected Panel PaneOne;
		protected Panel PaneTwo;
		protected Panel PaneThree;

		// The splitter must have one of the panes visible at all times (when we hide the first pane it
		// automatically shows the second one and vice versa), so this control must also keep track of which
		// panes are visible - it cannot rely on Splitter.FirstPaneVisible and Splitter.SecondPaneVisible.

		private bool m_paneOneVisible = m_defaultPaneOneVisible;
		private bool m_paneTwoVisible = m_defaultPaneTwoVisible;
		private bool m_paneThreeVisible = m_defaultPaneThreeVisible;
		private int m_maximisedPane = 0;
		private IList m_changeModeControls = new ArrayList();

		private Panel panTopTwo;
		private Splitter splitTop;
		private Splitter splitBottom;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ThreePanes()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			splitBottom.Initialise();
			splitTop.Initialise();
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
			this.panTopTwo = new System.Windows.Forms.Panel();
			this.PaneTwo = new System.Windows.Forms.Panel();
			this.splitTop = new LinkMe.Framework.Tools.Controls.Splitter();
			this.PaneOne = new System.Windows.Forms.Panel();
			this.splitBottom = new LinkMe.Framework.Tools.Controls.Splitter();
			this.PaneThree = new System.Windows.Forms.Panel();
			this.panTopTwo.SuspendLayout();
			this.SuspendLayout();
			// 
			// panTopTwo
			// 
			this.panTopTwo.Controls.Add(this.PaneTwo);
			this.panTopTwo.Controls.Add(this.splitTop);
			this.panTopTwo.Controls.Add(this.PaneOne);
			this.panTopTwo.Dock = System.Windows.Forms.DockStyle.Top;
			this.panTopTwo.Location = new System.Drawing.Point(0, 0);
			this.panTopTwo.Name = "panTopTwo";
			this.panTopTwo.Size = new System.Drawing.Size(704, 206);
			this.panTopTwo.TabIndex = 1;
			// 
			// PaneTwo
			// 
			this.PaneTwo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PaneTwo.Location = new System.Drawing.Point(0, 106);
			this.PaneTwo.Name = "PaneTwo";
			this.PaneTwo.Size = new System.Drawing.Size(704, 100);
			this.PaneTwo.TabIndex = 4;
			// 
			// splitTop
			// 
			this.splitTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitTop.FirstPaneVisible = true;
			this.splitTop.Location = new System.Drawing.Point(0, 100);
			this.splitTop.MaintainSplitterRatio = true;
			this.splitTop.MinExtra = 30;
			this.splitTop.MinSize = 30;
			this.splitTop.Name = "splitTop";
			this.splitTop.SecondPaneVisible = true;
			this.splitTop.Size = new System.Drawing.Size(704, 6);
			this.splitTop.TabIndex = 3;
			this.splitTop.TabStop = false;
			this.splitTop.MaximisedPaneHidden += new System.EventHandler(this.split_MaximisedPaneHidden);
			this.splitTop.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitTop_SplitterMoved);
			// 
			// PaneOne
			// 
			this.PaneOne.Dock = System.Windows.Forms.DockStyle.Top;
			this.PaneOne.Location = new System.Drawing.Point(0, 0);
			this.PaneOne.Name = "PaneOne";
			this.PaneOne.Size = new System.Drawing.Size(704, 100);
			this.PaneOne.TabIndex = 0;
			// 
			// splitBottom
			// 
			this.splitBottom.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitBottom.FirstPaneVisible = true;
			this.splitBottom.Location = new System.Drawing.Point(0, 206);
			this.splitBottom.MaintainSplitterRatio = true;
			this.splitBottom.MinExtra = 30;
			this.splitBottom.MinSize = 30;
			this.splitBottom.Name = "splitBottom";
			this.splitBottom.SecondPaneVisible = true;
			this.splitBottom.Size = new System.Drawing.Size(704, 6);
			this.splitBottom.TabIndex = 2;
			this.splitBottom.TabStop = false;
			this.splitBottom.MaximisedPaneHidden += new System.EventHandler(this.split_MaximisedPaneHidden);
			this.splitBottom.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitBottom_SplitterMoved);
			// 
			// PaneThree
			// 
			this.PaneThree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PaneThree.Location = new System.Drawing.Point(0, 212);
			this.PaneThree.Name = "PaneThree";
			this.PaneThree.Size = new System.Drawing.Size(704, 100);
			this.PaneThree.TabIndex = 3;
			// 
			// ThreePanes
			// 
			this.Controls.Add(this.PaneThree);
			this.Controls.Add(this.splitBottom);
			this.Controls.Add(this.panTopTwo);
			this.Name = "ThreePanes";
			this.Size = new System.Drawing.Size(704, 312);
			this.panTopTwo.ResumeLayout(false);
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
				if (value != PaneOneVisible)
				{
					m_paneOneVisible = value;
					splitBottom.FirstPaneVisible = (value || PaneTwoVisible);
					splitTop.FirstPaneVisible = value;
					PaneOne.Visible = value;
					AdjustBottomSplitter();
				}
			}
		}

		/// <summary>
		/// True to show the second (middle) pane, false to hide it. The default is true.
		/// </summary>
		[DefaultValue(m_defaultPaneTwoVisible)]
		public bool PaneTwoVisible
		{
			get { return m_paneTwoVisible; }
			set
			{
				if (value != PaneTwoVisible)
				{
					m_paneTwoVisible = value;
					splitBottom.FirstPaneVisible = (PaneOneVisible || value);
					splitTop.SecondPaneVisible = value;
					PaneTwo.Visible = value;
					AdjustBottomSplitter();
				}
			}
		}

		/// <summary>
		/// True to show the third (bottom) pane, false to hide it. The default is true.
		/// </summary>
		[DefaultValue(m_defaultPaneThreeVisible)]
		public bool PaneThreeVisible
		{
			get { return m_paneThreeVisible; }
			set
			{
				m_paneThreeVisible = value;
				splitBottom.SecondPaneVisible = value;
				PaneThree.Visible = value;
				AdjustBottomSplitter();
			}
		}

		/// <summary>
		/// The minimum size of the first (top) pane. The default is 30.
		/// </summary>
		public int PaneOneMinSize
		{
			get { return splitTop.MinSize; }
			set
			{
				splitTop.MinSize = value;
				splitBottom.MinSize = value + splitTop.Height + PaneTwoMinSize;
			}
		}

		/// <summary>
		/// The minimum size of the second (middle) pane. The default is 30.
		/// </summary>
		[DefaultValue(Splitter.DefaultMinExtra)]
		public int PaneTwoMinSize
		{
			get { return splitTop.MinExtra; }
			set
			{
				splitTop.MinExtra = value;
				splitBottom.MinSize = PaneOneMinSize + splitTop.Height + value;
			}
		}

		/// <summary>
		/// The minimum size of the third (bottom) pane. The default is 30.
		/// </summary>
		[DefaultValue(Splitter.DefaultMinExtra)]
		public int PaneThreeMinSize
		{
			get { return splitBottom.MinExtra; }
			set { splitBottom.MinExtra = value; }
		}

		/// <summary>
		/// The border style of the splitters between the panes.
		/// </summary>
		[DefaultValue(Splitter.DefaultBorderStyle)]
		public BorderStyle SplitterBorderStyle
		{
			get { return splitTop.BorderStyle; }
			set
			{
				splitTop.BorderStyle = value;
				splitBottom.BorderStyle = value; 
			}
		}

		/// <summary>
		/// The ratio of the size of the first pane to the size of the second pane.
		/// </summary>
		public float SplitRatioTop
		{
			get { return (m_paneOneVisible && (m_paneTwoVisible || m_paneThreeVisible) ? splitTop.SplitRatio : -1); }
			set
			{
				if (m_paneOneVisible && (m_paneTwoVisible || m_paneThreeVisible))
				{
					splitTop.SplitRatio = value;
				}
			}
		}

		/// <summary>
		/// The ratio of the combined size of the first and second pane (including the splitter between them)
		/// to the size of the third pane.
		/// </summary>
		public float SplitRatioBottom
		{
			get { return ((m_paneOneVisible || m_paneTwoVisible) && m_paneThreeVisible ? splitBottom.SplitRatio : -1); }
			set
			{
				if ((m_paneOneVisible || m_paneTwoVisible) && m_paneThreeVisible)
				{
					splitBottom.SplitRatio = value;
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
			InitialisePane(PaneThree);
		}

		public void MaximisePaneOne()
		{
			if (!PaneOneVisible)
				throw new InvalidOperationException("Pane one cannot be maximised,"
					+ " because it is not visible.");
			if (m_maximisedPane == 1)
				throw new InvalidOperationException("Pane one cannot be maximised,"
					+ " because it is already maximised.");

			splitBottom.MaximiseFirstPane();
			splitTop.MaximiseFirstPane();
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

			splitBottom.MaximiseFirstPane();
			splitTop.MaximiseSecondPane();
			m_maximisedPane = 2;
		}
	
		public void MaximisePaneThree()
		{
			if (!PaneThreeVisible)
				throw new InvalidOperationException("Pane three cannot be maximised,"
					+ " because it is not visible.");
			if (m_maximisedPane == 3)
				throw new InvalidOperationException("Pane three cannot be maximised,"
					+ " because it is already maximised.");

			splitBottom.MaximiseSecondPane();
			m_maximisedPane = 3;
		}

		public void RestorePanes()
		{
			splitBottom.RestorePanes();
			splitTop.RestorePanes();
			m_maximisedPane = 0;
		}

		protected virtual void OnDisplayModeChanged(DisplayModeChangedEventArgs e)
		{
			if (DisplayModeChanged != null)
			{
				DisplayModeChanged(this, e);
			}
		}

		protected virtual void OnSplitRatioTopChanged(EventArgs e)
		{
			if (SplitRatioTopChanged != null)
			{
				SplitRatioTopChanged(this, e);
			}
		}

		protected virtual void OnSplitRatioBottomChanged(EventArgs e)
		{
			if (SplitRatioBottomChanged != null)
			{
				SplitRatioBottomChanged(this, e);
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
	
		private void paneThree_DisplayModeChanged(object sender, DisplayModeChangedEventArgs e)
		{
			if (e.FullSize)
			{
				MaximisePaneThree();
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
			if (pane == PaneThree)
				return new DisplayModeChangedEventHandler(paneThree_DisplayModeChanged);

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

		private void AdjustBottomSplitter()
		{
			// If either the top or middle panel is not visible we want the bottom splitter to be in the middle of
			// the control, not at two thirds, so that first and third panels get equal space (until the user moves
			// the splitter).

			if (splitTop.FirstPaneVisible ^ splitTop.SecondPaneVisible)
			{
				splitBottom.SplitRatio = 0.5F;
			}
			else if (splitTop.FirstPaneVisible && splitTop.SecondPaneVisible)
			{
				splitBottom.SplitRatio = 0.667F;
			}
		}

		private void splitTop_SplitterMoved(object sender, SplitterEventArgs e)
		{
			OnSplitRatioTopChanged(EventArgs.Empty);
		}

		private void splitBottom_SplitterMoved(object sender, SplitterEventArgs e)
		{
			OnSplitRatioBottomChanged(EventArgs.Empty);
		}
	}
}
