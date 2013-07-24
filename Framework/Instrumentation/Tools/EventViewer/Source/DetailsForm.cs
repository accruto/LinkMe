using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Tools;
using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Editors;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Tools.Settings;
using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class DetailsForm : TC.Form
	{
		private EventMessage m_eventMessage = null;
	    private int m_eventMessageIndex = -1;
		private LogForm m_parentForm = null;
		private bool m_initialised = false;
		private GenericEditorGrid.RowTree m_expandedDetails = null;
		private EventViewerSettings m_settings = null;
		private System.TimeZone m_timeZone = null;
		private ConstantMemberWrapper m_timePropertyWrapper = null;

		private LinkMe.Framework.Instrumentation.Tools.EventViewer.DetailPanes panes;
		private System.Windows.Forms.Button btnPrevious;
		private System.Windows.Forms.ImageList imgDetails;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.PictureBox picEventIcon;
		private System.Windows.Forms.ToolTip tipLog;
		private System.ComponentModel.IContainer components;

		public DetailsForm()
		{
			InitializeComponent();
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

				if (m_settings != null)
				{
					m_settings.SettingChanged -= m_settings_SettingChanged;
					m_settings = null;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DetailsForm));
			this.panes = new LinkMe.Framework.Instrumentation.Tools.EventViewer.DetailPanes();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.imgDetails = new System.Windows.Forms.ImageList(this.components);
			this.btnNext = new System.Windows.Forms.Button();
			this.picEventIcon = new System.Windows.Forms.PictureBox();
			this.tipLog = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// panes
			// 
			this.panes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panes.Location = new System.Drawing.Point(8, 8);
			this.panes.MinimumSize = new System.Drawing.Size(0, 0);
			this.panes.Name = "panes";
			this.panes.PaneOneMinSize = 30;
			this.panes.Size = new System.Drawing.Size(680, 344);
			this.panes.SplitRatioBottom = 0.666F;
			this.panes.SplitRatioTop = 0.498F;
			this.panes.SplitterBorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panes.TabIndex = 0;
			this.panes.SplitRatioTopChanged += new System.EventHandler(this.panes_SplitRatioTopChanged);
			this.panes.SplitRatioBottomChanged += new System.EventHandler(this.panes_SplitRatioBottomChanged);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.ImageIndex = 0;
			this.btnPrevious.ImageList = this.imgDetails;
			this.btnPrevious.Location = new System.Drawing.Point(696, 96);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(32, 32);
			this.btnPrevious.TabIndex = 1;
			this.tipLog.SetToolTip(this.btnPrevious, "Previous event message (Alt-Up)");
			this.btnPrevious.Click += new System.EventHandler(this.btnNavigate_Click);
			// 
			// imgDetails
			// 
			this.imgDetails.ImageSize = new System.Drawing.Size(16, 16);
			this.imgDetails.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgDetails.ImageStream")));
			this.imgDetails.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.ImageIndex = 1;
			this.btnNext.ImageList = this.imgDetails;
			this.btnNext.Location = new System.Drawing.Point(696, 144);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(32, 32);
			this.btnNext.TabIndex = 2;
			this.tipLog.SetToolTip(this.btnNext, "Next event message (Alt-Down)");
			this.btnNext.Click += new System.EventHandler(this.btnNavigate_Click);
			// 
			// picEventIcon
			// 
			this.picEventIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picEventIcon.Location = new System.Drawing.Point(696, 24);
			this.picEventIcon.Name = "picEventIcon";
			this.picEventIcon.Size = new System.Drawing.Size(32, 32);
			this.picEventIcon.TabIndex = 3;
			this.picEventIcon.TabStop = false;
			// 
			// DetailsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 357);
			this.Controls.Add(this.picEventIcon);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnPrevious);
			this.Controls.Add(this.panes);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(400, 200);
			this.Name = "DetailsForm";
			this.Text = "Event Message Details";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailsForm_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion

		#region Static methods

		private static Icon ImageToIcon(Image image)
		{
			Debug.Assert(image != null, "image != null");
			return Icon.FromHandle(new Bitmap(image).GetHicon());
		}

		#endregion

		public EventMessage Message
		{
			get { return m_eventMessage; }
		}

		internal LogForm ParentLogForm
		{
			get { return m_parentForm; }
		}

		public void DisplayMessage(EventMessage eventMessage, int eventMessageIndex, Image largeIcon, Image smallIcon)
		{
			const int MaxTitleBarMessageLength = 200;

			Debug.Assert(eventMessage != null && largeIcon != null && smallIcon != null,
				"eventMessage != null && largeIcon != null && smallIcon != null");
			Debug.Assert(MdiParent != null, "MdiParent != null");

			m_eventMessage = eventMessage;
		    m_eventMessageIndex = eventMessageIndex;

			Icon = ImageToIcon(smallIcon);
			picEventIcon.Image = largeIcon;
			tipLog.SetToolTip(picEventIcon, eventMessage.Event);

			using (new LongRunningMonitor(this))
			{
				panes.txtMessage.Text = eventMessage.Message;
				Text = "\"" + TextUtil.TruncateText(eventMessage.Message, MaxTitleBarMessageLength) + "\" (Event Message)";

				// Save the currently expanded rows in the Details grid.

				panes.gridDetails.SaveExpandedRows(ref m_expandedDetails);

				EditorManager editorManager = ((MainForm)MdiParent).EditorManager;
				panes.gridDetails.EditorManager = editorManager;
				panes.gridParameters.EditorManager = editorManager;

				panes.gridDetails.DisplayValue(GetMessageProperties(eventMessage));
				panes.gridParameters.DisplayValue(GetMessageParameters(eventMessage));

				// Restore the grid column widths in the settings object. This must be done after displaying values.

				EventViewerSettings settings = ((MainForm)MdiParent).Settings;

				panes.gridDetails.RestoreColumnWidths(settings.DetailColumnWidths);
				panes.gridParameters.RestoreColumnWidths(settings.ParameterColumnWidths);

				// Restore the expanded rows in the Details grid, so that if the message being displayed
				// has the rows with the same names as the previous message it appears to the user that only the
				// values have changed.

				panes.gridDetails.RestoreExpandedRows(m_expandedDetails);
			}
		}

		public void Initialise(LogForm parentForm)
		{
			// Apply settings.

			m_settings = ((MainForm)MdiParent).Settings;

			m_settings.DetailsWindow.ApplyToWindow(this);
			panes.SplitRatioTop = m_settings.DetailsSplitterRatioTop;
			panes.SplitRatioBottom = m_settings.DetailsSplitterRatioBottom;
			panes.gridDetails.EditorDialogSettings = m_settings.GenericEditorWindow;
			panes.gridParameters.EditorDialogSettings = m_settings.GenericEditorWindow;
			m_timeZone = m_settings.DisplayTimeZone;

			m_parentForm = parentForm;

			m_settings.SettingChanged += m_settings_SettingChanged;

			m_initialised = true;
		}

		protected override void OnResize(System.EventArgs e)
		{
		    base.OnResize(e);

		    WindowSettingsChanged();
		}

        protected override void OnMove(System.EventArgs e)
        {
            base.OnMove(e);

            WindowSettingsChanged();
        }

	    protected override void OnClosing(CancelEventArgs e)
		{
			// Save grid column widths.

			EventViewerSettings settings = ((MainForm)MdiParent).Settings;

			settings.DetailColumnWidths = panes.gridDetails.SaveColumnWidths();
			settings.ParameterColumnWidths = panes.gridParameters.SaveColumnWidths();

			base.OnClosing(e);
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (base.ProcessDialogKey(keyData))
				return true;

			// Close the form when the user presses Escape, even though it's not a dialog.

			if (keyData == Keys.Escape)
			{
				Close();
				return true;
			}
			else
				return false;
		}

		private MemberWrappers GetMessageProperties(EventMessage eventMessage)
		{
			MemberWrappers properties = new MemberWrappers();

            // Handle exceptions from individual members to give the user a chance to see the rest of the details.

            try
            {
                // Convert the time to the user-configured time zone.

			    DateTime time = LogForm.GetEventMessageTime(eventMessage, m_timeZone);

			    m_timePropertyWrapper = new ConstantMemberWrapper("Time", time);
			    properties.Add(m_timePropertyWrapper);
			    properties.Add(new ConstantMemberWrapper("Event", eventMessage.Event));
			    properties.Add(new ConstantMemberWrapper("Source", eventMessage.Source));
			    properties.Add(new ConstantMemberWrapper("Type", eventMessage.Type));
			    properties.Add(new ConstantMemberWrapper("Method", eventMessage.Method));

                try
                {
                    properties.Add(new ExceptionWrapper(eventMessage.Exception));
                }
                catch (System.Exception ex)
                {
                    ((MainForm)MdiParent).HandleException(ex);
                }

                foreach (IEventDetail detail in eventMessage.Details)
				{
                    try
                    {
                        properties.Add(new EventDetailMemberWrapper(detail));
                    }
                    catch (System.Exception ex)
                    {
                        ((MainForm)MdiParent).HandleException(ex);
                    }
                }
			}
			catch (System.Exception ex)
			{
				((MainForm)MdiParent).HandleException(ex);
			}

			return properties;
		}

        private MemberWrappers GetMessageParameters(EventMessage eventMessage)
        {
            MemberWrappers wrappers = new MemberWrappers();

            try
            {
                foreach (EventParameter parameter in eventMessage.Parameters)
                {
                    wrappers.Add(new ParameterWrapper(parameter));
                }
            }
            catch (System.Exception ex)
            {
                ((MainForm)MdiParent).HandleException(ex);
            }

            return wrappers;
        }

		private void Navigate(NavigateDirection direction)
		{
			using (new LongRunningMonitor(this))
			{
				EventMessage nextMessage;
                int nextMessageIndex;

                m_parentForm.SelectNextMessage(m_eventMessageIndex, direction, out nextMessage, out nextMessageIndex);
                if (nextMessage != null)
				{
                    DisplayMessage(nextMessage, nextMessageIndex, m_parentForm.GetLargeImageForSelectedMessage(), m_parentForm.GetSmallImageForSelectedMessage());
				}
			}
		}

		private void panes_SplitRatioTopChanged(object sender, System.EventArgs e)
		{
			if (m_initialised)
			{
				((MainForm)MdiParent).Settings.DetailsSplitterRatioTop = panes.SplitRatioTop;
			}
		}

		private void panes_SplitRatioBottomChanged(object sender, System.EventArgs e)
		{
			if (m_initialised)
			{
				((MainForm)MdiParent).Settings.DetailsSplitterRatioBottom = panes.SplitRatioBottom;
			}
		}

		private void btnNavigate_Click(object sender, System.EventArgs e)
		{
			Debug.Assert(m_parentForm != null && m_eventMessage != null, "m_parentForm != null && m_eventMessage != null");

			NavigateDirection direction;
			if (sender == btnNext)
			{
				direction = NavigateDirection.Next;
			}
			else if (sender == btnPrevious)
			{
				direction = NavigateDirection.Previous;
			}
			else
			{
				Debug.Fail("Unexpected sender for btnNavigate_Click: " + sender);
				direction = 0;
			}

			Navigate(direction);
		}

		private void DetailsForm_KeyDown(object sender, KeyEventArgs e)
		{
			// Select the previous/next message on Alt-Up/Alt-Down.

			if (e.KeyData == (Keys.Alt | Keys.Up))
			{
				if (btnPrevious.Enabled)
				{
					Navigate(NavigateDirection.Previous);
				}
			}
			else if (e.KeyData == (Keys.Alt | Keys.Down))
			{
				if (btnNext.Enabled)
				{
					Navigate(NavigateDirection.Next);
				}
			}
		}

		private void m_settings_SettingChanged(object sender, SettingChangedEventArgs e)
		{
			if (e.SettingName == EventViewerSettings.SettingDisplayTimeZone)
			{
				m_timeZone = m_settings.DisplayTimeZone;

				if (m_timePropertyWrapper != null)
				{
					m_timePropertyWrapper.SetValue(LogForm.GetEventMessageTime(m_eventMessage, m_timeZone));
					panes.gridDetails.Invalidate(true); // Must be called to refresh the grid immediately.
				}
			}
		}

        private void WindowSettingsChanged()
        {
            if (m_initialised)
            {
                ((MainForm)MdiParent).Settings.DetailsWindow.ReadFromWindow(this);
            }
        }
    }
}
