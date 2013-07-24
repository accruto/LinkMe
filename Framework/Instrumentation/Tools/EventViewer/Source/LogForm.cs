using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using LinkMe.Framework.Tools;
using LinkMe.Framework.Utility;
using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Settings;
using LinkMe.Framework.Type;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	// This form needs to allow for multi-threaded operation. The message reader may call the HandleEventMessage
	// and HandleEventMessages methods and raise the ExistingMessagesRead event on a different thread. Be sure to
	// marshal all such calls to the main UI thread using Invoke(). Also beware of deadlocks when stopping the
	// reader: it may be waiting for HandleEventMessage() to complete while Stop() is waiting for it to complete.
    internal class LogForm : TC.Form, IMessageHandler, IMessageNotifyHandler
	{
		#region Nested types

        private delegate void HandleEventMessageIdentifierDelegate(EventMessageIdentifier eventMessageIdentifier);
        private delegate void HandleEventMessageIdentifiersDelegate(EventMessageIdentifiers eventMessageIdentifiers);
        private delegate void HandleEventMessageDelegate(EventMessage eventMessage);
		private delegate void HandleEventMessagesDelegate(EventMessages eventMessage);
		private delegate void VoidDelegate();
		private delegate void ExceptionDelegate(System.Exception ex);

        private class EventMessageDetails
        {
            private readonly EventMessageIdentifier m_identifier;
            private EventMessage m_message;

            public EventMessageDetails(EventMessageIdentifier identifier, EventMessage message)
            {
                m_identifier = identifier;
                m_message = message;
            }

            public EventMessageIdentifier Identifer
            {
                get { return m_identifier; }
            }

            public EventMessage Message
            {
                get { return m_message; }
                set { m_message = value; }
            }
        }

        private class IndexEventMessageIdentifier
            : EventMessageIdentifier
        {
            private readonly int m_index;

            public IndexEventMessageIdentifier(int index)
            {
                m_index = index;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is IndexEventMessageIdentifier))
                    return false;
                return m_index == ((IndexEventMessageIdentifier)obj).m_index;
            }

            public override int GetHashCode()
            {
                return m_index.GetHashCode();
            }

            public override string ToString()
            {
                return "Index: " + m_index;
            }
        }

		#endregion

		private const int m_imageIndexDefault = 0;
		private const int m_toolbarImageIndexAutoScrollOff = 3;
		private const int m_toolbarImageIndexAutoScrollOn = 4;
		private const string m_statusIdle = "Idle";
		private const string m_statusStopping = "Stopping...";
		private const string m_statusPaused = "Paused";
		private const string m_statusReadingExisting = "Reading messages...";
		private const string m_statusReadingExistingWithEstimate = "Reading messages (estimated total: {0})...";
		private const string m_statusMonitoringNew = "Monitoring for new messages";
		private const string m_statusError = "Reading messages failed.";

		private static readonly Hashtable m_eventIconsIndicies = new Hashtable();

		private IMessageReader m_reader = null;
        private IMessageNotifyReader m_notifyReader = null;
        private ListViewCacheManager<EventMessageDetails, EventMessage, EventMessageIdentifier> m_cache;
        private bool m_autoScrollMessages;
		private bool m_initialised = false;
        private bool m_activated = false;
        private EventViewerSettings m_settings = null;
		private bool m_paused;
		private bool m_existingRead = false;
		private string m_configurationString = null;
		private string m_stopStatus = null;
		private bool m_readSinceStarted = false;
		private System.TimeZone m_timeZone = null;
        private Interner m_interner = new Interner(true);
        private Filters m_filters;

		private LinkMe.Framework.Instrumentation.Tools.EventViewer.LogPanes panes;
		private System.Windows.Forms.StatusBar sbLog;
		private System.Windows.Forms.StatusBarPanel sbpRead;
		private System.Windows.Forms.StatusBarPanel sbpStatus;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem mnuReader;
		private System.Windows.Forms.MenuItem mnuReaderStart;
		private System.Windows.Forms.MenuItem mnuReaderStop;
		private System.Windows.Forms.MenuItem mnuReaderPause;
		private System.Windows.Forms.MenuItem mnuReaderSep1;
		private System.Windows.Forms.MenuItem mnuReaderClear;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuEditClear;
		private System.Windows.Forms.ToolBar tbLog;
		private System.Windows.Forms.ImageList imgToolbar;
		private System.Windows.Forms.ToolBarButton tbbStart;
		private System.Windows.Forms.ToolBarButton tbbPause;
		private System.Windows.Forms.ToolBarButton tbbStop;
		private System.Windows.Forms.ToolBarButton tbbSep1;
		private System.Windows.Forms.ToolBarButton tbbClearList;
		private System.Windows.Forms.ToolBarButton tbbSep2;
		private System.Windows.Forms.ToolBarButton tbbClearRepository;
		private System.Windows.Forms.MenuItem mnuReaderSep2;
		private System.Windows.Forms.MenuItem mnuReaderRemoveWhenReading;
		private System.Windows.Forms.ToolBarButton tbbSep3;
		private System.Windows.Forms.ToolBarButton tbbPrevEvent;
		private System.Windows.Forms.ToolBarButton tbbNextEvent;
		private System.Windows.Forms.ComboBox cboEvent;
		private System.Windows.Forms.ToolBarButton tbbAutoScroll;
        private ToolBarButton tbbFilters;
        private ToolBarButton tbbSep4;
		private System.ComponentModel.IContainer components = null;

	    static LogForm()
		{
			m_eventIconsIndicies.Add("CriticalError", 1);
			m_eventIconsIndicies.Add("Error", 2);
			m_eventIconsIndicies.Add("Warning", 3);
			m_eventIconsIndicies.Add("NonCriticalError", 4);
			m_eventIconsIndicies.Add("Information", 5);
			m_eventIconsIndicies.Add("Trace", 6);
			m_eventIconsIndicies.Add("MethodEnter", 7);
			m_eventIconsIndicies.Add("MethodExit", 8);
		}
		
		public LogForm()
		{
			InitializeComponent();

			Paused = false;
			SetMessagesShown(0);
            m_cache = new ListViewCacheManager<EventMessageDetails, EventMessage, EventMessageIdentifier>(
                EnsureMessageLoaded, LoadMessageRange);

            panes.lvwMessages.RetrieveVirtualItem += lvwMessages_RetrieveVirtualItem;
            panes.lvwMessages.CacheVirtualItems += lvwMessages_CacheVirtualItems;
			panes.lvwMessages.SelectedIndexChanged += lvwMessages_SelectedIndexChanged;
			panes.lvwMessages.ItemActivate += lvwMessages_ItemActivate;
			panes.lvwMessages.ColumnHeaderResized += lvwMessages_ColumnHeaderResized;
			panes.lvwMessages.KeyDown += lvwMessages_KeyDown;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}

				if (m_reader != null)
				{
					m_reader.Stop();
					m_reader.ExistingMessagesRead -= m_reader_ExistingMessagesRead;
					m_reader.ReaderThreadException -= m_reader_ReaderThreadException;

					System.IDisposable disposable = m_reader as System.IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}

					m_reader = null;
				}

                m_cache = null;

				if (m_settings != null)
				{
					m_settings.SettingChanged -= m_settings_SettingChanged;
					m_settings = null;
				}
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.sbLog = new System.Windows.Forms.StatusBar();
            this.sbpRead = new System.Windows.Forms.StatusBarPanel();
            this.sbpStatus = new System.Windows.Forms.StatusBarPanel();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEditClear = new System.Windows.Forms.MenuItem();
            this.mnuReader = new System.Windows.Forms.MenuItem();
            this.mnuReaderStart = new System.Windows.Forms.MenuItem();
            this.mnuReaderPause = new System.Windows.Forms.MenuItem();
            this.mnuReaderStop = new System.Windows.Forms.MenuItem();
            this.mnuReaderSep1 = new System.Windows.Forms.MenuItem();
            this.mnuReaderClear = new System.Windows.Forms.MenuItem();
            this.mnuReaderSep2 = new System.Windows.Forms.MenuItem();
            this.mnuReaderRemoveWhenReading = new System.Windows.Forms.MenuItem();
            this.tbLog = new System.Windows.Forms.ToolBar();
            this.tbbStart = new System.Windows.Forms.ToolBarButton();
            this.tbbPause = new System.Windows.Forms.ToolBarButton();
            this.tbbStop = new System.Windows.Forms.ToolBarButton();
            this.tbbSep1 = new System.Windows.Forms.ToolBarButton();
            this.tbbAutoScroll = new System.Windows.Forms.ToolBarButton();
            this.tbbClearList = new System.Windows.Forms.ToolBarButton();
            this.tbbSep2 = new System.Windows.Forms.ToolBarButton();
            this.tbbClearRepository = new System.Windows.Forms.ToolBarButton();
            this.tbbSep3 = new System.Windows.Forms.ToolBarButton();
            this.tbbFilters = new System.Windows.Forms.ToolBarButton();
            this.tbbSep4 = new System.Windows.Forms.ToolBarButton();
            this.tbbPrevEvent = new System.Windows.Forms.ToolBarButton();
            this.tbbNextEvent = new System.Windows.Forms.ToolBarButton();
            this.imgToolbar = new System.Windows.Forms.ImageList(this.components);
            this.cboEvent = new System.Windows.Forms.ComboBox();
            this.panes = new LinkMe.Framework.Instrumentation.Tools.EventViewer.LogPanes();
            ((System.ComponentModel.ISupportInitialize)(this.sbpRead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // sbLog
            // 
            this.sbLog.Location = new System.Drawing.Point(0, -16);
            this.sbLog.Name = "sbLog";
            this.sbLog.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpRead,
            this.sbpStatus});
            this.sbLog.ShowPanels = true;
            this.sbLog.Size = new System.Drawing.Size(784, 16);
            this.sbLog.TabIndex = 1;
            // 
            // sbpRead
            // 
            this.sbpRead.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.sbpRead.MinWidth = 100;
            this.sbpRead.Name = "sbpRead";
            // 
            // sbpStatus
            // 
            this.sbpStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.sbpStatus.Name = "sbpStatus";
            this.sbpStatus.Width = 667;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEdit,
            this.mnuReader});
            // 
            // mnuEdit
            // 
            this.mnuEdit.Enabled = false;
            this.mnuEdit.Index = 0;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEditClear});
            this.mnuEdit.MergeOrder = 1;
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditClear
            // 
            this.mnuEditClear.Index = 0;
            this.mnuEditClear.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.mnuEditClear.Text = "&Clear";
            this.mnuEditClear.Click += new System.EventHandler(this.mnuEditClear_Click);
            // 
            // mnuReader
            // 
            this.mnuReader.Enabled = false;
            this.mnuReader.Index = 1;
            this.mnuReader.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuReaderStart,
            this.mnuReaderPause,
            this.mnuReaderStop,
            this.mnuReaderSep1,
            this.mnuReaderClear,
            this.mnuReaderSep2,
            this.mnuReaderRemoveWhenReading});
            this.mnuReader.MergeOrder = 3;
            this.mnuReader.Text = "&Message Reader";
            // 
            // mnuReaderStart
            // 
            this.mnuReaderStart.Index = 0;
            this.mnuReaderStart.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.mnuReaderStart.Text = "&Start";
            this.mnuReaderStart.Click += new System.EventHandler(this.mnuReaderStart_Click);
            // 
            // mnuReaderPause
            // 
            this.mnuReaderPause.Enabled = false;
            this.mnuReaderPause.Index = 1;
            this.mnuReaderPause.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.mnuReaderPause.Text = "&Pause";
            this.mnuReaderPause.Click += new System.EventHandler(this.mnuReaderPause_Click);
            // 
            // mnuReaderStop
            // 
            this.mnuReaderStop.Enabled = false;
            this.mnuReaderStop.Index = 2;
            this.mnuReaderStop.Shortcut = System.Windows.Forms.Shortcut.ShiftF5;
            this.mnuReaderStop.Text = "S&top";
            this.mnuReaderStop.Click += new System.EventHandler(this.mnuReaderStop_Click);
            // 
            // mnuReaderSep1
            // 
            this.mnuReaderSep1.Index = 3;
            this.mnuReaderSep1.Text = "-";
            // 
            // mnuReaderClear
            // 
            this.mnuReaderClear.Enabled = false;
            this.mnuReaderClear.Index = 4;
            this.mnuReaderClear.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.mnuReaderClear.Text = "&Clear Repository";
            this.mnuReaderClear.Click += new System.EventHandler(this.mnuReaderClear_Click);
            // 
            // mnuReaderSep2
            // 
            this.mnuReaderSep2.Index = 5;
            this.mnuReaderSep2.Text = "-";
            // 
            // mnuReaderRemoveWhenReading
            // 
            this.mnuReaderRemoveWhenReading.Enabled = false;
            this.mnuReaderRemoveWhenReading.Index = 6;
            this.mnuReaderRemoveWhenReading.Text = "&Remove When Reading";
            this.mnuReaderRemoveWhenReading.Click += new System.EventHandler(this.mnuReaderRemoveWhenReading_Click);
            // 
            // tbLog
            // 
            this.tbLog.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbStart,
            this.tbbPause,
            this.tbbStop,
            this.tbbSep1,
            this.tbbAutoScroll,
            this.tbbClearList,
            this.tbbSep2,
            this.tbbClearRepository,
            this.tbbSep3,
            this.tbbFilters,
            this.tbbSep4,
            this.tbbPrevEvent,
            this.tbbNextEvent});
            this.tbLog.DropDownArrows = true;
            this.tbLog.ImageList = this.imgToolbar;
            this.tbLog.Location = new System.Drawing.Point(0, 0);
            this.tbLog.Name = "tbLog";
            this.tbLog.ShowToolTips = true;
            this.tbLog.Size = new System.Drawing.Size(784, 28);
            this.tbLog.TabIndex = 2;
            this.tbLog.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbLog_ButtonClick);
            // 
            // tbbStart
            // 
            this.tbbStart.ImageIndex = 0;
            this.tbbStart.Name = "tbbStart";
            // 
            // tbbPause
            // 
            this.tbbPause.Enabled = false;
            this.tbbPause.ImageIndex = 1;
            this.tbbPause.Name = "tbbPause";
            this.tbbPause.ToolTipText = "Stop reading messages, but remember the position of the last read message.";
            // 
            // tbbStop
            // 
            this.tbbStop.Enabled = false;
            this.tbbStop.ImageIndex = 2;
            this.tbbStop.Name = "tbbStop";
            this.tbbStop.ToolTipText = "Stop reading messages.";
            // 
            // tbbSep1
            // 
            this.tbbSep1.Name = "tbbSep1";
            this.tbbSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbAutoScroll
            // 
            this.tbbAutoScroll.ImageIndex = 3;
            this.tbbAutoScroll.Name = "tbbAutoScroll";
            this.tbbAutoScroll.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbAutoScroll.ToolTipText = "Automatically scroll to the end as new messages arrive.";
            // 
            // tbbClearList
            // 
            this.tbbClearList.ImageIndex = 5;
            this.tbbClearList.Name = "tbbClearList";
            this.tbbClearList.ToolTipText = "Clear the messages displayed in this window.";
            // 
            // tbbSep2
            // 
            this.tbbSep2.Name = "tbbSep2";
            this.tbbSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbClearRepository
            // 
            this.tbbClearRepository.ImageIndex = 6;
            this.tbbClearRepository.Name = "tbbClearRepository";
            this.tbbClearRepository.ToolTipText = "Remove all messages from the repository.";
            // 
            // tbbSep3
            // 
            this.tbbSep3.Name = "tbbSep3";
            this.tbbSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbFilters
            // 
            this.tbbFilters.ImageIndex = 9;
            this.tbbFilters.Name = "tbbFilters";
            // 
            // tbbSep4
            // 
            this.tbbSep4.Name = "tbbSep4";
            this.tbbSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbPrevEvent
            // 
            this.tbbPrevEvent.Enabled = false;
            this.tbbPrevEvent.ImageIndex = 7;
            this.tbbPrevEvent.Name = "tbbPrevEvent";
            this.tbbPrevEvent.ToolTipText = "Previous message with the selected Event (Alt-Up)";
            // 
            // tbbNextEvent
            // 
            this.tbbNextEvent.Enabled = false;
            this.tbbNextEvent.ImageIndex = 8;
            this.tbbNextEvent.Name = "tbbNextEvent";
            this.tbbNextEvent.ToolTipText = "Next message with the selected Event (Alt-Down)";
            // 
            // imgToolbar
            // 
            this.imgToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolbar.ImageStream")));
            this.imgToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgToolbar.Images.SetKeyName(0, "");
            this.imgToolbar.Images.SetKeyName(1, "");
            this.imgToolbar.Images.SetKeyName(2, "");
            this.imgToolbar.Images.SetKeyName(3, "");
            this.imgToolbar.Images.SetKeyName(4, "");
            this.imgToolbar.Images.SetKeyName(5, "");
            this.imgToolbar.Images.SetKeyName(6, "");
            this.imgToolbar.Images.SetKeyName(7, "");
            this.imgToolbar.Images.SetKeyName(8, "");
            this.imgToolbar.Images.SetKeyName(9, "Properties.ico");
            // 
            // cboEvent
            // 
            this.cboEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEvent.Enabled = false;
            this.cboEvent.Location = new System.Drawing.Point(240, 5);
            this.cboEvent.Name = "cboEvent";
            this.cboEvent.Size = new System.Drawing.Size(208, 21);
            this.cboEvent.Sorted = true;
            this.cboEvent.TabIndex = 3;
            this.cboEvent.SelectedIndexChanged += new System.EventHandler(this.cboEvent_SelectedIndexChanged);
            // 
            // panes
            // 
            this.panes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panes.Location = new System.Drawing.Point(0, 28);
            this.panes.Name = "panes";
            this.panes.Size = new System.Drawing.Size(784, 0);
            this.panes.SplitRatio = float.PositiveInfinity;
            this.panes.SplitterBorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panes.TabIndex = 0;
            this.panes.SplitRatioChanged += new System.EventHandler(this.panes_SplitRatioChanged);
            // 
            // LogForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(784, 0);
            this.Controls.Add(this.cboEvent);
            this.Controls.Add(this.panes);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.sbLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu;
            this.Name = "LogForm";
            ((System.ComponentModel.ISupportInitialize)(this.sbpRead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region IMessageHandler Members

		public IMessageHandler GetMessageHandler(string eventSource)
		{
			return this;
		}

        public void HandleEventMessage(EventMessageIdentifier eventMessageIdentifier)
        {
            if (!IsClosing && !IsDisposed)
            {
                BeginInvoke(new HandleEventMessageIdentifierDelegate(HandleEventMessageIdentifierInternal), new object[] { eventMessageIdentifier });
            }
        }

        public void HandleEventMessages(EventMessageIdentifiers eventMessageIdentifiers)
        {
            if (!IsClosing && !IsDisposed)
            {
                BeginInvoke(new HandleEventMessageIdentifiersDelegate(HandleEventMessageIdentifiersInternal), new object[] { eventMessageIdentifiers });
            }
        }

        public void HandleEventMessage(EventMessage eventMessage)
		{
			if (!IsClosing && !IsDisposed)
			{
				BeginInvoke(new HandleEventMessageDelegate(HandleEventMessageInternal), new object[] { eventMessage });
			}
		}

		public void HandleEventMessages(EventMessages eventMessages)
		{
			if (!IsClosing && !IsDisposed)
			{
				BeginInvoke(new HandleEventMessagesDelegate(HandleEventMessagesInternal), new object[] { eventMessages });
			}
		}

		public void AddNextMessageHandler(string name, IMessageHandler messageHandler)
		{
		}

		#endregion

		public IMessageReader Reader
		{
			get { return m_reader; }
		}

		public string ConfigurationString
		{
			get { return m_configurationString; }
			set { m_configurationString = value; }
		}

		private bool ShowPreviewPane
		{
			get { return panes.PaneTwoVisible; }
			set { panes.PaneTwoVisible = value; }
		}

		private bool ShowStatusBar
		{
			set { sbLog.Visible = value; }
		}

		private bool AutoScrollMessages
		{
			get { return m_autoScrollMessages; }
			set
			{
				m_autoScrollMessages = value;
				tbbAutoScroll.Pushed = value;
				tbbAutoScroll.ImageIndex = (value ? m_toolbarImageIndexAutoScrollOn : m_toolbarImageIndexAutoScrollOff);
			}
		}

		private bool Paused
		{
			set
			{
				m_paused = value;
				mnuReaderStart.Text = (value ? "&Resume" : "&Start");
				tbbStart.ToolTipText = (value ? "Resume reading messages from the position where reading was paused." :
					"Start reading messages from the repository.");
			}
		}

		internal static DateTime GetEventMessageTime(EventMessage message, System.TimeZone timeZone)
		{
			return DateTime.FromSystemDateTime(message.Time, TimeZone.UTC).ToTimeZone(timeZone);
		}

		private static int GetImageIndexForEvent(string eventName)
		{
			object index = m_eventIconsIndicies[eventName];
			return (index == null ? m_imageIndexDefault : (int)index);
		}

		public void Initialise(IMessageReader reader, string displayText)
		{
			Debug.Assert(reader != null, "reader != null");
			Debug.Assert(!m_initialised, "Initialise() called when already initialised");

			m_reader = reader;
		    m_notifyReader = reader as IMessageNotifyReader;

            panes.lvwMessages.VirtualMode = m_notifyReader != null;

			m_reader.SetMessageHandler(this);
			m_reader.ExistingMessagesRead += m_reader_ExistingMessagesRead;
			m_reader.ReaderThreadException += m_reader_ReaderThreadException;

			Text = displayText;

			// Apply settings.

			m_settings = ((MainForm)MdiParent).Settings;

			m_settings.LogWindow.ApplyToWindow(this);
			panes.SplitRatio = m_settings.LogSplitRatio;

			int[] columnWidths = m_settings.LogColumnWidths;
			ListView.ColumnHeaderCollection columns = panes.lvwMessages.Columns;
			for (int index = 0; index < columnWidths.Length && index < columns.Count - 1; index++)
			{
				columns[index].Width = columnWidths[index];
			}
			columns[columns.Count - 1].Width = Constants.Win32.LVSCW_AUTOSIZE_USEHEADER; // Auto-resize the last column.

			ShowPreviewPane = m_settings.PreviewPane;
			ShowStatusBar = m_settings.StatusBar;
			AutoScrollMessages = m_settings.AutoScrollMessages;
			m_timeZone = m_settings.DisplayTimeZone;

			m_settings.SettingChanged += m_settings_SettingChanged;

			mnuEdit.Enabled = true;
			mnuReader.Enabled = true;

			bool canRemove = (m_reader is IRemoveMessages);
			mnuReaderClear.Enabled = canRemove;
			tbbClearRepository.Enabled = canRemove;
			mnuReaderRemoveWhenReading.Enabled = canRemove;

			if (canRemove)
			{
				((IRemoveMessages)m_reader).RemoveWhenReading = m_settings.RemoveWhenReading;
				mnuReaderRemoveWhenReading.Checked = m_settings.RemoveWhenReading;
			}
			else
			{
				mnuReaderRemoveWhenReading.Checked = false;
			}

			m_initialised = true;
		}

		public void AutoStart()
		{
			using (new LongRunningMonitor(this))
			{
				// Don't auto-start if there are too many messages.

				//GetMessageCount();
                if (m_cache.Count > m_settings.MaxCountToAutoStart)
				{
                    sbpStatus.Text = "The repository has " + m_cache.Count + " messages. Click Start to read them.";
					return;
				}

				StartInternal();
			}
		}

		public void Start()
		{
			using (new LongRunningMonitor(this))
			{
				// Get the message count first, if possible.

				//GetMessageCount();
				StartInternal();
			}
		}

		public IList<EventMessage> GetAllEventMessages()
		{
            return m_cache.LoadAll();
		}

		public IList<EventMessage> GetSelectedEventMessages()
		{
			ListView.SelectedIndexCollection selection = panes.lvwMessages.SelectedIndices;
			EventMessage[] array = new EventMessage[selection.Count];

			for (int index = 0; index < selection.Count; index++)
			{
                array[index] = m_cache.GetData(selection[index]);
			}

			return array;
		}

        protected override void OnActivated(System.EventArgs e)
        {
            base.OnActivated(e);

            m_activated = true;
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

		internal void SelectNextMessage(int currentMessageIndex, NavigateDirection direction, out EventMessage nextMessage, out int nextMessageIndex)
		{
		    nextMessage = null;
		    nextMessageIndex = -1;
			if (currentMessageIndex == -1)
				return; // We don't have this message any more - the list must have been cleared.

			// Find the next or previous message.

			switch (direction)
			{
				case NavigateDirection.Previous:
					currentMessageIndex--;
					break;

				case NavigateDirection.Next:
                    currentMessageIndex++;
					break;

				default:
					Debug.Assert(false, "Unexpected value of NavigateDirection: " + direction);
					break;
			}

            if (currentMessageIndex == -1 || currentMessageIndex == m_cache.Count)
				return;

		    nextMessageIndex = currentMessageIndex;

			// Select it in the ListView and return it.

            ListViewItem item = panes.lvwMessages.Items[nextMessageIndex];
			panes.lvwMessages.SelectedIndices.Clear();
			item.Selected = true;
			item.Focused = true;
			item.EnsureVisible();

		    nextMessage = m_cache.GetData(nextMessageIndex);
		}

        internal Image GetLargeImageForSelectedMessage()
		{
            Debug.Assert(panes.lvwMessages.SelectedIndices.Count == 1, "panes.lvwMessages.SelectedIndices.Count == 1");
            return panes.lvwMessages.LargeImageList.Images[GetImageIndexForEvent(m_cache.GetData(panes.lvwMessages.SelectedIndices[0]).Event)];
		}

		internal Image GetSmallImageForSelectedMessage()
		{
            Debug.Assert(panes.lvwMessages.SelectedIndices.Count == 1, "panes.lvwMessages.SelectedIndices.Count == 1");
			return panes.lvwMessages.SmallImageList.Images[GetImageIndexForEvent(m_cache.GetData(panes.lvwMessages.SelectedIndices[0]).Event)];
		}

/*		private void GetMessageCount()
		{
			IMessageCount countReader = m_reader as IMessageCount;
			m_messageCount = (countReader != null ? countReader.GetMessageCount() : -1);

            // Update the list size.

		    panes.lvwMessages.VirtualListSize = m_messageCount == -1 ? 0 : m_messageCount;
		}
*/

		private void StartInternal()
		{
			Debug.Assert(m_reader != null, "m_reader != null");

			try
			{
				SetReadStatus(false);
				m_readSinceStarted = false;

				if (m_paused)
				{
					m_reader.Resume();
					Paused = false;
				}
				else
				{
					ClearList();
					m_reader.Start(m_filters);
				}

				EnableDisableCommands(false, true, true, false);
			}
			catch ( ThreadAbortException )
			{
				// The user may close the form while waiting for the message to be processed.

				if (!IsClosing && !IsDisposed)
					throw;
			}
		}

		private void SetMessagesShown(int count)
		{
			sbpRead.Text = "Messages shown: " + count;
		}

		private void HandleEventMessageInternal(EventMessage eventMessage)
		{
			SetReadStatus(false);
			AddEventMessage(eventMessage);
            UpdateVirtualSize();
			SelectLastMessageIfNeeded();
			SetMessagesShown(m_cache.Count);
		}

        private void HandleEventMessageIdentifierInternal(EventMessageIdentifier eventMessageIdentifier)
        {
            SetReadStatus(false);
            AddEventMessageIdentifier(eventMessageIdentifier);
            UpdateVirtualSize();
            SelectLastMessageIfNeeded();
            SetMessagesShown(m_cache.Count);
        }

        private void ExistingMessagesRead()
		{
			if (!IsClosing && !IsDisposed)
			{
				SetReadStatus(true);

				// Only select the last message if at least one message has actually been read
				// since the user clicked Start.

				if (m_readSinceStarted)
				{
					SelectLastMessageIfNeeded();
				}
			}
		}

        private void HandleEventMessageIdentifiersInternal(EventMessageIdentifiers eventMessageIdentifiers)
        {
            SetReadStatus(false);

            try
            {
                foreach (EventMessageIdentifier eventMessageIdentifier in eventMessageIdentifiers)
                {
                    AddEventMessageIdentifier(eventMessageIdentifier);
                }
            }
            finally
            {
                // Do it only once for all the message, it's expensive.
                UpdateVirtualSize();
            }

            SetMessagesShown(m_cache.Count);
            SelectLastMessageIfNeeded();
        }

        private void UpdateVirtualSize()
        {
            if (panes.lvwMessages.VirtualMode)
            {
                panes.lvwMessages.VirtualListSize = m_cache.Count;
            }
        }

        private void HandleEventMessagesInternal(EventMessages eventMessages)
		{
			SetReadStatus(false);

            if (!panes.lvwMessages.VirtualMode)
            {
                panes.lvwMessages.BeginUpdate();
            }

            try
            {
                foreach (EventMessage eventMessage in eventMessages)
                {
                    AddEventMessage(eventMessage);
                }
            }
            finally
            {
                if (panes.lvwMessages.VirtualMode)
                {
                    UpdateVirtualSize();
                }
                else
                {
                    panes.lvwMessages.EndUpdate();
                }
            }
            
			SelectLastMessageIfNeeded();
			SetMessagesShown(m_cache.Count);
		}

		private void Stop(string status)
		{
			using (new LongRunningMonitor(this))
			{
				Paused = false;
				EnableDisableCommands(false, false, false, false);
				sbpStatus.Text = m_statusStopping;

				// Create a new thread to stop the reader thread, otherwise it and this UI thread become deadlocked.

				m_stopStatus = status;
				Thread stopperThread = new Thread(StopReaderThread);
				stopperThread.Start();
			}
		}

		private void StopReaderThread()
		{
			if (!IsDisposed)
			{
			    m_reader.Stop();
			    Invoke(new MethodInvoker(UpdateGuiAfterStopping));
			}
		}

	    private void UpdateGuiAfterStopping()
	    {
	        EnableDisableCommands(true, false, false, true);
	        sbpStatus.Text = m_stopStatus;
	        panes.Cursor = Cursors.Default;
	    }

	    private void Pause()
		{
			using (new LongRunningMonitor(this))
			{
				m_reader.Pause();
				Paused = true;
				sbpStatus.Text = m_statusPaused;
				EnableDisableCommands(true, false, true, false);
			}

			panes.Cursor = Cursors.Default;
		}

		private void ClearList()
		{
            m_cache.Clear();

		    panes.lvwMessages.VirtualListSize = 0;
			panes.txtPreview.Text = string.Empty;

			panes.lvwMessages.BeginUpdate();
			try
			{
				panes.lvwMessages.Items.Clear();
			}
			finally
			{
				panes.lvwMessages.EndUpdate();
			}

			SetMessagesShown(0);

			cboEvent.Items.Clear();
			cboEvent.Enabled = false;

			EnableDisablePrevNext();
		}

		private void ClearRepository()
		{
			Debug.Assert(m_reader is IRemoveMessages, "m_reader is IRemoveMessages");

			using (new LongRunningMonitor(this))
			{
				if (MessageBox.Show(this, "Are you sure you want to remove all event messages from the repository?",
					Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
					== DialogResult.OK)
				{
					// Create a new thread to stop the reader thread, otherwise it and this UI thread become deadlocked.

					Thread clearThread = new Thread(ClearRepositoryInternal);
					clearThread.Start();
				}
			}
		}

		private void ClearRepositoryInternal()
		{
			((IRemoveMessages)m_reader).RemoveAll();
			Invoke(new VoidDelegate(ClearList));
		}

        private void FindOrCreateMessageDetailsForm(EventMessage message, int messageIndex, int imageIndex)
		{
            Debug.Assert(message != null, "message != null");
            Debug.Assert(MdiParent != null, "MdiParent != null");

			// Look for a DetailsForm that already displays this message.

			foreach (Form childForm in MdiParent.MdiChildren)
			{
				DetailsForm detailsForm = childForm as DetailsForm;
				if (detailsForm != null && detailsForm.Message == message)
				{
					childForm.BringToFront();
					return;
				}
			}

			// Not found - create a new form.

			DetailsForm form = new DetailsForm();
			form.MdiParent = MdiParent;
			form.Initialise(this);
			form.DisplayMessage(message, messageIndex, panes.lvwMessages.LargeImageList.Images[imageIndex],
				panes.lvwMessages.SmallImageList.Images[imageIndex]);

			form.Show();
		}

		private void AddEventMessage(EventMessage eventMessage)
		{
            m_interner.Intern(eventMessage);

		    int index = m_cache.Count;
		    EventMessageIdentifier identifier = new IndexEventMessageIdentifier(index);
		    EventMessageDetails details = new EventMessageDetails(identifier, eventMessage);
            m_cache.AddData(details, identifier, index);
			m_readSinceStarted = true;

            // If not using a notify reader then add the list item directly.

            if (!panes.lvwMessages.VirtualMode)
            {
                ListViewItem item = CreateItem(eventMessage);
                panes.lvwMessages.Items.Add(item);
            }
		}

        private void AddEventMessageIdentifier(EventMessageIdentifier eventMessageIdentifier)
        {
            int index = m_cache.Count;
            m_cache.AddPlaceholder(new EventMessageDetails(eventMessageIdentifier, null),
                eventMessageIdentifier, index);
            m_readSinceStarted = true;

/*            if (!cboEvent.Items.Contains(eventName))
            {
                // Add this event to the combo box.

                bool first = (cboEvent.Items.Count == 0);

                cboEvent.Items.Add(eventName);

                if (first)
                {
                    cboEvent.SelectedIndex = 0;
                    cboEvent.Enabled = true;
                }
            }
            else if (cboEvent.Text == eventName)
            {
                tbbNextEvent.Enabled = true;
            }
*/
        }

        private string GetMessageText(EventMessage eventMessage)
	    {
            try
            {
                string text = eventMessage.Message;

                try
                {
                    if (eventMessage.Exception != null)
                    {
                        text += " " + eventMessage.Exception.Message;
                    }
                }
                catch (System.Exception ex)
                {
                    // An error can occur in reading the exception, but don't let that prevent the rest of
                    // the message from being shown.
                    text += " <error: " + ex.Message + ">";
                }

                return m_interner.Intern(text);
            }
            catch (System.Exception ex)
            {
                return "<error: " + ex.Message + ">";
            }
	    }

	    private void SelectLastMessageIfNeeded()
		{
			switch (panes.lvwMessages.Items.Count)
			{
				case 0:
					break; // Nothing to select

				case 1:
					// Only one message, so may as well select it if it's not selected already.
	
					if (panes.lvwMessages.SelectedIndices.Count == 0)
					{
						SelectMessage(panes.lvwMessages.Items.Count - 1);
					}
					break;

				default:
					// Select if auto-scrolling is enabled and all the existing message have been read. We don't
					// want to auto-scroll while reading existing messages because it really slows down the reader.
					// We also don't want to auto-scroll after the user has selected a message, because this
					// is annoying.

					if (AutoScrollMessages && m_existingRead)
					{
						SelectMessage(panes.lvwMessages.Items.Count - 1);
					}
					break;
			}
		}

		private void SelectMessage(int index)
		{
			ListViewItem item = panes.lvwMessages.Items[index];
			panes.lvwMessages.SelectedIndices.Clear();
			item.Selected = true;
			item.Focused = true;
			item.EnsureVisible();
		}

		private void EnableDisableCommands(bool start, bool pause, bool stop, bool filters)
		{
			mnuReaderStart.Enabled = start;
			tbbStart.Enabled = start;

			mnuReaderPause.Enabled = pause;
			tbbPause.Enabled = pause;

			mnuReaderStop.Enabled = stop;
			tbbStop.Enabled = stop;

            tbbFilters.Enabled = filters;
		}

		private void EnableDisablePrevNext()
		{
			if (panes.lvwMessages.SelectedIndices.Count == 0)
			{
				tbbPrevEvent.Enabled = false;
				tbbNextEvent.Enabled = false;
			}
			else
			{
				// Scan messages in both directions, looking for the selected event.

			    tbbPrevEvent.Enabled = true; // (GetNextPrevMessageIndex(NavigateDirection.Previous) != -1);
			    tbbNextEvent.Enabled = true; // (GetNextPrevMessageIndex(NavigateDirection.Next) != -1);
			}
		}

		private int GetNextPrevMessageIndex(NavigateDirection direction)
		{
			string eventName = cboEvent.Text;
			int selectedIndex = panes.lvwMessages.SelectedIndices[0];

			int step;
			switch (direction)
			{
				case NavigateDirection.Next:
					step = 1;
					break;

				case NavigateDirection.Previous:
					step = -1;
					break;

				default:
					Debug.Fail("Unexpected value of direction: " + direction);
					step = 1;
					break;
			}

/*            if (selectedIndex + step >= 0 && selectedIndex + step < m_cache.Count)
                return selectedIndex + step;
            else
                return -1;
*/

			for (int index = selectedIndex + step; index >= 0 && index < m_cache.Count; index += step)
			{
                var item = panes.lvwMessages.Items[index];
                var message = item.Tag as EventMessage;
                if (message != null)
                {
                    if (message.Event == eventName)
                        return index;
                }
			}

			return -1;
        }

		private void NavigateToEvent(NavigateDirection direction)
		{
			int index = GetNextPrevMessageIndex(direction);
			//Debug.Assert(index != -1, "Failed to find the previous/next message with the selected event.");

            if (index != -1)
                SelectMessage(index);
		}

		private void SetReadStatus(bool existingRead)
		{
			m_existingRead = existingRead;

			if (m_paused)
			{
				panes.Cursor = Cursors.Default;
			}
			else
			{
				if (existingRead)
				{
					panes.Cursor = Cursors.Default;

					sbpStatus.Text = m_statusMonitoringNew;
				}
				else
				{
					panes.Cursor = Cursors.AppStarting;

                    if (m_cache.Count >= 0)
					{
                        sbpStatus.Text = string.Format(m_statusReadingExistingWithEstimate, m_cache.Count);
					}
					else
					{
						sbpStatus.Text = m_statusReadingExisting;
					}
				}
			}
		}

		private void MainThreadReaderException(System.Exception ex)
		{
			Stop(m_statusError);

			MainForm mainForm = MdiParent as MainForm;
			if (mainForm == null)
			{
				Debug.Fail("An exception has occurred on the reader thread, but MainForm is null - how did this happen?");
			}
			else
			{
				mainForm.HandleException(ex, "The following exception has occurred in the message reader:");
			}
		}

		private void RefreshMessageTimes()
		{
            if (panes.lvwMessages.VirtualMode)
            {
                panes.lvwMessages.Invalidate(true);
            }
            else
            {
                foreach (ListViewItem item in panes.lvwMessages.Items)
                {
                    item.SubItems[0].Text = GetEventMessageTime((EventMessage) item.Tag, m_timeZone).ToString();
                }
            }
		}

        private string GetPreviewText(EventMessage message)
        {
            Debug.Assert(message != null, "message != null");

            string preview = message.Message;

            try
            {
                if (message.Exception != null)
                {
                    preview += System.Environment.NewLine + System.Environment.NewLine + message.Exception;
                }
            }
            catch (System.Exception ex)
            {
                preview += "\r\n<error: " + ex.Message + ">";
            }

            // Don't add the parameters as it defeats the purpose of some lazy loading that happens.

/*
            if (message.Parameters.Count > 0)
            {
                preview += System.Environment.NewLine;

                foreach (EventParameter param in message.Parameters)
                {
                    preview += System.Environment.NewLine + param.Name + ": " + param.GetSafeDisplayString();
                }
            }
*/

            return m_interner.Intern(preview);
        }

        private void lvwMessages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = CreateItem(m_cache.GetData(e.ItemIndex));
        }

        private ListViewItem CreateItem(EventMessage message)
        {
            // The time is stored as UTC in the database. Convert it to the user-configured time zone.

            DateTime time = GetEventMessageTime(message, m_timeZone);
            string messageText = GetMessageText(message);

            string[] subItems = { time.ToString(), message.Event, message.Source, message.Type, message.Method, messageText };
            subItems = m_interner.Intern(subItems);

            ListViewItem item = new ListViewItem(subItems, GetImageIndexForEvent(message.Event));
            item.Tag = message;

            string eventName = message.Event;
            if (!cboEvent.Items.Contains(eventName))
            {
                // Add this event to the combo box.

                bool first = (cboEvent.Items.Count == 0);

                cboEvent.Items.Add(eventName);

                if (first)
                {
                    cboEvent.SelectedIndex = 0;
                    cboEvent.Enabled = true;
                }
            }
            else if (cboEvent.Text == eventName)
            {
                tbbNextEvent.Enabled = true;
            }

            return item;
        }

        private void lvwMessages_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            m_cache.CacheItemRange(e.StartIndex, e.EndIndex);
        }

	    private void lvwMessages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ListView.SelectedIndexCollection selected = panes.lvwMessages.SelectedIndices;

			if (selected.Count == 0)
			{
                panes.txtPreview.Text = string.Empty;
			}
			else
			{
                using (new LongRunningMonitor(this))
                {
                    EventMessage message = m_cache.GetData(panes.lvwMessages.SelectedIndices[0]);

                    try
                    {
                        panes.txtPreview.Text = GetPreviewText(message);
                    }
                    catch (System.Exception ex)
                    {
                        panes.txtPreview.Text = "<failed to get preview text: " + ex.Message + ">";
                    }
                }
			}

			EnableDisablePrevNext();
		}

	    private void lvwMessages_ItemActivate(object sender, System.EventArgs e)
		{
			if (panes.lvwMessages.SelectedIndices.Count <= 10)
			{
				foreach (int index in panes.lvwMessages.SelectedIndices)
				{
                    EventMessage message = m_cache.GetData(index);
                    FindOrCreateMessageDetailsForm(message, index, GetImageIndexForEvent(message.Event));
				}
			}
		}

		private void lvwMessages_ColumnHeaderResized(object sender, System.EventArgs e)
		{
			// Save the new column widths.

			int[] widths = new int[panes.lvwMessages.Columns.Count];
			for (int index = 0; index < panes.lvwMessages.Columns.Count; index++)
			{
				widths[index] = panes.lvwMessages.Columns[index].Width;
			}

			((MainForm)MdiParent).Settings.LogColumnWidths = widths;
		}

		private void panes_SplitRatioChanged(object sender, System.EventArgs e)
		{
			if (m_initialised && m_activated && ShowPreviewPane)
			{
				((MainForm)MdiParent).Settings.LogSplitRatio = panes.SplitRatio;
			}
		}

		private void m_settings_SettingChanged(object sender, SettingChangedEventArgs e)
		{
			switch (e.SettingName)
			{
				case EventViewerSettings.SettingStatusBar:
					ShowStatusBar = m_settings.StatusBar;
					break;

				case EventViewerSettings.SettingPreviewPane:
					ShowPreviewPane = m_settings.PreviewPane;
					break;

				case EventViewerSettings.SettingAutoScrollMessages:
					AutoScrollMessages = m_settings.AutoScrollMessages;
					break;

				case EventViewerSettings.SettingDisplayTimeZone:
					m_timeZone = m_settings.DisplayTimeZone;
					RefreshMessageTimes();
					break;

				default:
					// Do nothing.
					break;
			}
		}

		private void mnuReaderStart_Click(object sender, System.EventArgs e)
		{
			Start();
		}

		private void mnuReaderStop_Click(object sender, System.EventArgs e)
		{
			Stop(m_statusIdle);
		}

		private void mnuReaderPause_Click(object sender, System.EventArgs e)
		{
			Pause();
		}

		private void mnuReaderClear_Click(object sender, System.EventArgs e)
		{
			ClearRepository();
		}

		private void mnuReaderRemoveWhenReading_Click(object sender, System.EventArgs e)
		{
			Debug.Assert(m_reader is IRemoveMessages, "m_reader is IRemoveMessages");

			bool remove = !mnuReaderRemoveWhenReading.Checked;
			if (remove)
			{
				if (MessageBox.Show(this, "All event messages currently in the repository will be removed and"
				+ " future messages will be removed as they are read. Are you sure?", Application.ProductName,
					MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
					return;
			}

			((IRemoveMessages)m_reader).RemoveWhenReading = remove;
			m_settings.RemoveWhenReading = remove;
			mnuReaderRemoveWhenReading.Checked = remove;
		}

		private void mnuEditClear_Click(object sender, System.EventArgs e)
		{
			ClearList();
		}

		private void tbLog_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == tbbStart)
			{
				Start();
			}
			else if (e.Button == tbbPause)
			{
				Pause();
			}
			else if (e.Button == tbbStop)
			{
				Stop(m_statusIdle);
			}
			else if (e.Button == tbbAutoScroll)
			{
				AutoScrollMessages = !AutoScrollMessages;
			}
			else if (e.Button == tbbClearList)
			{
				ClearList();
			}
			else if (e.Button == tbbClearRepository)
			{
				ClearRepository();
			}
            else if (e.Button == tbbFilters)
            {
                GetFilters();
            }
            else if (e.Button == tbbPrevEvent)
            {
                NavigateToEvent(NavigateDirection.Previous);
            }
            else if (e.Button == tbbNextEvent)
            {
                NavigateToEvent(NavigateDirection.Next);
            }
            else
            {
                Debug.Fail("Unexpected toolbar button clicked: " + e.Button);
            }
		}

        private void GetFilters()
        {
            var form = new FiltersForm();
            form.DisplayValue(m_filters);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                m_filters = form.GetValue();
            }
        }

        // Pass these events to the main message thread.

		private void m_reader_ExistingMessagesRead(object sender, System.EventArgs e)
		{
			if (!IsClosing && IsHandleCreated)
			{
				BeginInvoke(new VoidDelegate(ExistingMessagesRead));
			}
		}

		private void m_reader_ReaderThreadException(object sender, ThreadExceptionEventArgs e)
		{
			if (!IsClosing && IsHandleCreated)
			{
				BeginInvoke(new ExceptionDelegate(MainThreadReaderException), new object[] { e.Exception });
			}
		}

		private void cboEvent_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			EnableDisablePrevNext();
		}

		private void lvwMessages_KeyDown(object sender, KeyEventArgs e)
		{
			// Select the previous/next message with the specified event on Alt-Up/Alt-Down.

			if (e.KeyData == (Keys.Alt | Keys.Up))
			{
				if (tbbPrevEvent.Enabled)
				{
					NavigateToEvent(NavigateDirection.Previous);
				}
			}
			else if (e.KeyData == (Keys.Alt | Keys.Down))
			{
				if (tbbNextEvent.Enabled)
				{
					NavigateToEvent(NavigateDirection.Next);
				}
			}
		}

        private void WindowSettingsChanged()
        {
            if (m_initialised && m_activated)
            {
                ((MainForm)MdiParent).Settings.LogWindow.ReadFromWindow(this);
            }
        }

        private EventMessage EnsureMessageLoaded(EventMessageDetails details, out EventMessageIdentifier loadedIdentifier)
        {
            loadedIdentifier = null;

            if (details.Message == null)
            {
                // Need to get it from the database.

                EventMessage message = m_notifyReader.GetMessage(details.Identifer);
                details.Message = message;
                loadedIdentifier = details.Identifer;
            }

            return details.Message;
        }

        private void LoadMessageRange(int startIndex, int endIndex)
        {
            EventMessageIdentifier startIdentifer = m_cache.GetHolder(startIndex).Identifer;
            EventMessageIdentifier endIdentifer = m_cache.GetHolder(endIndex).Identifer;

            EventMessages messages = m_notifyReader.GetMessages(startIdentifer, endIdentifer);

            foreach (EventMessage message in messages)
            {
                EventMessageIdentifier identifier = m_notifyReader.GetIdentifier(message);

                try
                {
                    EventMessageDetails details = m_cache.GetHolder(identifier);
                    details.Message = message;
                }
                catch (KeyNotFoundException)
                {
                    // Bug 9087 - although placeholders for all the identifiers SHOULD exist they sometimes
                    // don't. Not sure why this happens, but as a workaround, just add the message to the end
                    // of the list. It will be out of order, but that's better than not showing it at all.

                    int count = m_cache.Count;
                    Debug.WriteLine(string.Format("Failed to find message identifier '{0}', adding to the end"
                        + " (index {1})", identifier, count));
                    m_cache.AddData(new EventMessageDetails(identifier, message), identifier, count);
                }
            }
        }
	}
}
