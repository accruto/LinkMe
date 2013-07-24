using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// An improved Splitter control that can maintain the ratio between the top
	/// and bottom panes and completely hide the bottom pane on demand.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.Splitter))]
	public class Splitter : System.Windows.Forms.Splitter
	{
		/// <summary>
		/// Occurs when pane is set to invisible while it is maximised. This event should
		/// really be internal, but it's made public to work with the Forms Designer.
		/// </summary>
		public event EventHandler MaximisedPaneHidden;

		internal const int DefaultMinSize = 30;
		internal const int DefaultMinExtra = 30;
		internal const BorderStyle DefaultBorderStyle = BorderStyle.None;

		private const int m_minimumPosition = 0;
		private const bool m_defaultMaintainSplitterRatio = true;
		private const bool m_defaultFirstPaneVisible = true;
		private const bool m_defaultSecondPaneVisible = true;

		private bool m_maintainSplitterRatio = m_defaultMaintainSplitterRatio;
		private float m_splitRatio = 0.5F;
		private bool m_firstPaneVisible = m_defaultFirstPaneVisible;
		private bool m_secondPaneVisible = m_defaultSecondPaneVisible;
		private bool m_firstWasVisible = m_defaultFirstPaneVisible;
		private bool m_secondWasVisible = m_defaultSecondPaneVisible;
		private int m_minSize = DefaultMinSize;
		private int m_minExtra = DefaultMinExtra;
		private BorderStyle m_borderStyle = DefaultBorderStyle;
		private Control m_parent = null;
		// True if the first pane is shown only because the second was hidden (ie. not by the user's
		// explicit choice). This matters when the second pane is made visible again -
		// the first pane should then be hidden again.
		private bool m_firstPaneForcedVisible = false;
		// As above, but for the second pane.
		private bool m_secondPaneForcedVisible = false;
		private bool m_initialised = false;
		private bool m_maximised = false;

		public Splitter()
			: base()
		{
		}

		/// <summary>
		/// The border style of the splitter. The default is None.
		/// </summary>
		[DefaultValue(DefaultBorderStyle)]
		public new BorderStyle BorderStyle
		{
			get { return m_borderStyle; }
			set
			{
				m_borderStyle = value;
				RefreshBorderStyle();
			}
		}

		/// <summary>
		/// True to automatically move the splitter to maintain the ratio between the top and bottom control
		/// when the container control is resized, false to keep the splitter in the same position relative
		/// to the control it is docked to. The default is true.
		/// </summary>
		[DefaultValue(m_defaultMaintainSplitterRatio)]
		public bool MaintainSplitterRatio
		{
			get { return m_maintainSplitterRatio; }
			set { m_maintainSplitterRatio = value; }
		}

		/// <summary>
		/// The ratio of the distance between the splitter and the side of the container control that it is
		/// docked to to the distance between the splitter and the other side of the container control.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float SplitRatio
		{
			get
			{
				if (SplitPosition == -1)
					return -1;
				else
				{
					// Return the actual ratio, not the value the user has set.
					return (Parent == null ? -1 : (float)Math.Round(((double)SplitPosition / (double)MaximumPosition), 3));
				}
			}
			set
			{
				if (value >= 0 && value <= 1 && m_firstPaneVisible && m_secondPaneVisible)
				{
					m_splitRatio = value;

					if (Parent != null)
					{
						SplitPosition = (int)(MaximumPosition * m_splitRatio);
					}
				}
			}
		}

		// Do not use these properties internally to set the value - they are for the user
		// only! Call SetFirstPaneVisible() and SetSecondPaneVisible() instead.
		// The get accessor returns whether a pane is visible when in a normal state - it
		// may not be visible currently if another pane is maximised.

		/// <summary>
		/// True to show the control that the splitter is docked to, false to hide it by moving the splitter.
		/// The default is true. At least one of FirstPaneVisible and SecondPaneVisible must be true.
		/// </summary>
		[DefaultValue(m_defaultFirstPaneVisible)]
		public bool FirstPaneVisible
		{
			get
			{
				if (m_maximised)
					return m_firstWasVisible;
				else
					return m_firstPaneVisible;
			}
			set
			{
				m_firstWasVisible = value;
				m_firstPaneForcedVisible = false;

				if (m_maximised && m_firstPaneVisible && !value)
				{
					// The user is telling is to hide the first pane while its maximised -
					// they need to do something about this.
					OnMaximisedPaneHidden(EventArgs.Empty);
				}

				if (!m_maximised)
				{
					SetFirstPaneVisible(value);
				}
			}
		}

		/// <summary>
		/// True to show the "extra" space on the side of the splitter not docked to a control, false to hide
		/// it by moving the splitter. The default is true.  At least one of FirstPaneVisible and
		/// SecondPaneVisible must be true.
		/// </summary>
		[DefaultValue(m_defaultSecondPaneVisible)]
		public bool SecondPaneVisible
		{
			get
			{
				if (m_maximised)
					return m_secondWasVisible;
				else
					return m_secondPaneVisible;
			}
			set
			{
				m_secondWasVisible = value;
				m_secondPaneForcedVisible = false;

				if (m_maximised && m_secondPaneVisible && !value)
				{
					// The user is telling is to hide the second pane while its maximised -
					// they need to do something about this.
					OnMaximisedPaneHidden(EventArgs.Empty);
				}

				if (!m_maximised)
				{
					SetSecondPaneVisible(value);
				}
			}
		}

		// MinSize and MinExtra properties are not virtual, so we have to make do by shadowing them.

		/// <summary>
		/// The minimum distance that must remain between the splitter control and the container edge that
		/// the control is docked to. The default is 30.
		/// </summary>
		[DefaultValue(DefaultMinSize)]
		public new int MinSize
		{
			get { return m_minSize; }
			set
			{
				m_minSize = value;
				base.MinSize = value;
			}
		}

		/// <summary>
		/// The minimum distance that must remain between the splitter control and the edge of the opposite
		/// side of the container (or the closest control docked to that side). The default is 30.
		/// </summary>
		[DefaultValue(DefaultMinExtra)]
		public new int MinExtra
		{
			get { return m_minExtra; }
			set
			{
				m_minExtra = value;
				base.MinExtra = value;
			}
		}

		private int MaximumPosition
		{
			get
			{
				Debug.Assert(Parent != null, "Parent != null");
				return (Dock == DockStyle.Left || Dock == DockStyle.Right ?
					Parent.Width : Parent.Height);
			}
		}

		/// <summary>
		/// The parent control should call this function after all its children have
		/// been positioned.
		/// </summary>
		public void Initialise()
		{
			if (Parent == null)
				throw new ApplicationException("Initialise() was called on the Splitter"
					+ " control when it does not have a parent.");

			if (m_firstPaneVisible ^ m_secondPaneVisible)
			{
				// Both panels are visible initially, so we need to hide one here
				if (!m_firstPaneVisible)
				{
					SetFirstPaneVisible(false);
				}
				else if (!m_secondPaneVisible)
				{
					SetSecondPaneVisible(false);
				}
			}

			SaveSplitterRatio();
			m_initialised = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (m_parent != null)
				{
					m_parent.Resize -= new EventHandler(m_parent_Resize);
					m_parent = null;
				}
			}

			base.Dispose(disposing);
		}

		protected virtual void OnMaximisedPaneHidden(EventArgs e)
		{
			if (MaximisedPaneHidden != null)
			{
				MaximisedPaneHidden(this, e);
			}
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);

			if (Parent != null)
			{
				SaveSplitterRatio();
			}

			if (m_parent != null)
			{
				m_parent.Resize -= new EventHandler(m_parent_Resize);
			}

			m_parent = Parent;

			if (m_parent != null)
			{
				m_parent.Resize += new EventHandler(m_parent_Resize);
			}
		}

		protected override void OnSplitterMoved(SplitterEventArgs sevent)
		{
			if (Parent != null)
			{
				SaveSplitterRatio();
			}

			base.OnSplitterMoved(sevent);
		}

		internal void MaximiseFirstPane()
		{
			m_firstWasVisible = FirstPaneVisible;
			m_secondWasVisible = SecondPaneVisible;
			SetSecondPaneVisible(false);
			m_maximised = true;
		}

		internal void MaximiseSecondPane()
		{
			m_firstWasVisible = FirstPaneVisible;
			m_secondWasVisible = SecondPaneVisible;
			SetFirstPaneVisible(false);
			m_maximised = true;
		}

		internal void RestorePanes()
		{
			SetFirstPaneVisible(m_firstWasVisible);
			SetSecondPaneVisible(m_secondWasVisible);
			m_maximised = false;
		}

		private void SetFirstPaneVisible(bool value)
		{
			if (Parent == null)
			{
				m_firstPaneVisible = value;
			}
			else
			{
				if (value)
				{
					MinSize = m_minSize;
					m_firstPaneVisible = true; // Set BEFORE restoring

					if (m_secondPaneForcedVisible)
					{
						// We were only showing the first pane because we had to. Now we can hide it again.
						m_secondPaneVisible = false;
					}

					RestoreSplitterRatio();
				}
				else
				{
					m_minSize = MinSize;
					MinSize = 0;
					SaveSplitterRatio();
					m_firstPaneVisible = false; // Set AFTER saving

					if (!m_secondPaneVisible)
					{
						m_secondPaneVisible = true; // One pane must always be visible
						m_secondPaneForcedVisible = true;
					}

					SplitPosition = m_minimumPosition;
				}

				Enabled = value;
				RefreshBorderStyle();
			}
		}

		private void SetSecondPaneVisible(bool value)
		{
			if (Parent == null)
			{
				m_secondPaneVisible = value;
			}
			else
			{
				if (value)
				{
					MinExtra = m_minExtra;
					m_secondPaneVisible = true; // Set BEFORE restoring

					if (m_firstPaneForcedVisible)
					{
						// We were only showing the first pane because we had to. Now we can hide it again.
						m_firstPaneVisible = false;
					}

					RestoreSplitterRatio();
				}
				else
				{
					m_minExtra = MinExtra;
					MinExtra = 0;
					SaveSplitterRatio();
					m_secondPaneVisible = false; // Set AFTER saving

					if (!m_firstPaneVisible)
					{
						m_firstPaneVisible = true; // One pane must always be visible
						m_firstPaneForcedVisible = true;
					}

					SplitPosition = MaximumPosition;
				}

				Enabled = value;
				RefreshBorderStyle();
			}
		}

		private void SaveSplitterRatio()
		{
			if (SplitPosition != -1 && m_firstPaneVisible && m_secondPaneVisible && MaximumPosition > 0)
			{
				float splitterRatio = (float)SplitPosition / (float)MaximumPosition;

				// Displaying a few pixels of a control is useless, so round the ratio a little
				if (splitterRatio < 0.05F)
				{
					splitterRatio = 0F;
				}
				else if (splitterRatio > 0.95F)
				{
					splitterRatio = 1F;
				}

				m_splitRatio = splitterRatio;
			}
		}

		private void RestoreSplitterRatio()
		{
			if (m_firstPaneVisible && m_secondPaneVisible)
			{
				if (MaintainSplitterRatio)
				{
                    int newPosition = (int)(MaximumPosition * m_splitRatio);

                    // Only set if changed, otherwise unncessary events are fired and use a lot of CPU.
                    if (SplitPosition != newPosition)
                    {
                        SplitPosition = newPosition;
                    }
				}
				else
				{
					// We're not maintaining the splitter ratio, but still need to enforce MinSize and MinExtra.

					int maxPosition = MaximumPosition - MinExtra;
					if (maxPosition >= 0 && SplitPosition > maxPosition)
					{
						if (SplitPosition >= MinSize)
						{
							SplitPosition = maxPosition;
						}
					}
					else if (SplitPosition < MinSize)
					{
						SplitPosition = MinSize;
					}
				}
			}
			else if (m_firstPaneVisible)
			{
				SplitPosition = MaximumPosition;
			}
			else if (m_secondPaneVisible)
			{
				SplitPosition = m_minimumPosition;
			}
			else
			{
				Debug.Fail("Neither pane is visible!");
			}
		}

		private void RefreshBorderStyle()
		{
			// If only one pane is visible "hide" the splitter by not showing the border.

			base.BorderStyle = (m_firstPaneVisible && m_secondPaneVisible ? m_borderStyle : BorderStyle.None);
		}

		private void m_parent_Resize(object sender, EventArgs e)
		{
			if (m_initialised && Parent != null)
			{
				RestoreSplitterRatio();
			}
		}
	}
}