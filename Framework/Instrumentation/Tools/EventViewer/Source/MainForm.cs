using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using LinkMe.Framework.Tools;
using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Editors;
using LinkMe.Framework.Tools.Settings;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	/// <summary>
	/// Instrumentation Event Viewer MDI container form.
	/// </summary>
	public class MainForm : TC.Form
	{
		public const string ModulesXmlFileName = "LinkMe.Framework.Instrumentation.Tools.EventViewer.Modules.config";

		private const int RecentlyUsedMenuIndex = 3;

		private RecentlyUsedList _recentlyUsed;
		private readonly EventViewerSettings _settings = new EventViewerSettings();
		private Modules _modules;

#if DEBUG
		private readonly ArrayList _wrForms = new ArrayList(); // Weak references to closed MDI child forms.
#endif

		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuFileConnect;
		private System.Windows.Forms.MenuItem mnuView;
		private System.Windows.Forms.MenuItem mnuViewPreviewPane;
		private System.Windows.Forms.MenuItem mnuViewAutoScroll;
		private System.Windows.Forms.MenuItem mnuViewStatusBar;
		private System.Windows.Forms.MenuItem mnuDebug;
		private System.Windows.Forms.MenuItem mnuDebugAttach;
		private System.Windows.Forms.MenuItem mnuDebugMemoryLeaks;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.MenuItem mnuFileCopyAll;
		private System.Windows.Forms.MenuItem mnuFileCopySelected;
		private System.Windows.Forms.MenuItem mnuViewSep1;
		private System.Windows.Forms.MenuItem mnuViewTimeZone;
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
		    _modules = null;
		    InitializeComponent();

			Text = EventViewerSettings.ProductName;
			mnuHelpAbout.Text = "About " + EventViewerSettings.ProductName;

#if DEBUG
			mnuDebug.Enabled = true;
			mnuDebug.Visible = true;
			AddWindowMenu(mainMenu, 3, 5);
#else
			AddWindowMenu(mainMenu, 3, 4);
#endif
		}

	    public override sealed string Text
	    {
	        get { return base.Text; }
	        set { base.Text = value; }
	    }

	    /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuFileConnect = new System.Windows.Forms.MenuItem();
			this.mnuFileCopyAll = new System.Windows.Forms.MenuItem();
			this.mnuFileCopySelected = new System.Windows.Forms.MenuItem();
			this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
			this.mnuFileExit = new System.Windows.Forms.MenuItem();
			this.mnuView = new System.Windows.Forms.MenuItem();
			this.mnuViewPreviewPane = new System.Windows.Forms.MenuItem();
			this.mnuViewStatusBar = new System.Windows.Forms.MenuItem();
			this.mnuViewAutoScroll = new System.Windows.Forms.MenuItem();
			this.mnuDebug = new System.Windows.Forms.MenuItem();
			this.mnuDebugAttach = new System.Windows.Forms.MenuItem();
			this.mnuDebugMemoryLeaks = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
			this.mnuViewSep1 = new System.Windows.Forms.MenuItem();
			this.mnuViewTimeZone = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mnuFile,
																					 this.mnuView,
																					 this.mnuDebug,
																					 this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFileConnect,
																					this.mnuFileCopyAll,
																					this.mnuFileCopySelected,
																					this.mnuFileSep1,
																					this.mnuFileExit});
			this.mnuFile.Text = "&File";
			this.mnuFile.Popup += new System.EventHandler(this.mnuFile_Popup);
			// 
			// mnuFileConnect
			// 
			this.mnuFileConnect.Index = 0;
			this.mnuFileConnect.Text = "&Connect To";
			// 
			// mnuFileCopyAll
			// 
			this.mnuFileCopyAll.Enabled = false;
			this.mnuFileCopyAll.Index = 1;
			this.mnuFileCopyAll.Text = "Copy &All To";
			// 
			// mnuFileCopySelected
			// 
			this.mnuFileCopySelected.Enabled = false;
			this.mnuFileCopySelected.Index = 2;
			this.mnuFileCopySelected.Text = "Copy &Selected To";
			// 
			// mnuFileSep1
			// 
			this.mnuFileSep1.Index = 3;
			this.mnuFileSep1.Text = "-";
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Index = 4;
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
			// 
			// mnuView
			// 
			this.mnuView.Index = 1;
			this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuViewPreviewPane,
																					this.mnuViewStatusBar,
																					this.mnuViewAutoScroll,
																					this.mnuViewSep1,
																					this.mnuViewTimeZone});
			this.mnuView.MergeOrder = 2;
			this.mnuView.Text = "&View";
			// 
			// mnuViewPreviewPane
			// 
			this.mnuViewPreviewPane.Index = 0;
			this.mnuViewPreviewPane.Text = "&Preview Pane";
			this.mnuViewPreviewPane.Click += new System.EventHandler(this.mnuViewPreviewPane_Click);
			// 
			// mnuViewStatusBar
			// 
			this.mnuViewStatusBar.Index = 1;
			this.mnuViewStatusBar.Text = "&Status Bar";
			this.mnuViewStatusBar.Click += new System.EventHandler(this.mnuViewStatusBar_Click);
			// 
			// mnuViewAutoScroll
			// 
			this.mnuViewAutoScroll.Index = 2;
			this.mnuViewAutoScroll.Text = "&Auto Scroll";
			this.mnuViewAutoScroll.Click += new System.EventHandler(this.mnuViewAutoScroll_Click);
			// 
			// mnuDebug
			// 
			this.mnuDebug.Enabled = false;
			this.mnuDebug.Index = 2;
			this.mnuDebug.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mnuDebugAttach,
																					 this.mnuDebugMemoryLeaks});
			this.mnuDebug.MergeOrder = 4;
			this.mnuDebug.Text = "&Debug";
			this.mnuDebug.Visible = false;
			// 
			// mnuDebugAttach
			// 
			this.mnuDebugAttach.Index = 0;
			this.mnuDebugAttach.Text = "Attach &Debugger";
			this.mnuDebugAttach.Click += new System.EventHandler(this.mnuDebugAttach_Click);
			// 
			// mnuDebugMemoryLeaks
			// 
			this.mnuDebugMemoryLeaks.Index = 1;
			this.mnuDebugMemoryLeaks.Text = "Detect &Memory Leaks";
			this.mnuDebugMemoryLeaks.Click += new System.EventHandler(this.mnuDebugMemoryLeaks_Click);
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 3;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuHelpAbout});
			this.mnuHelp.MergeOrder = 6;
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHelpAbout
			// 
			this.mnuHelpAbout.Index = 0;
			this.mnuHelpAbout.Text = "&About";
			this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
			// 
			// mnuViewSep1
			// 
			this.mnuViewSep1.Index = 3;
			this.mnuViewSep1.Text = "-";
			// 
			// mnuViewTimeZone
			// 
			this.mnuViewTimeZone.Index = 4;
			this.mnuViewTimeZone.Text = "Time &Zone...";
			this.mnuViewTimeZone.Click += new System.EventHandler(this.mnuViewTimeZone_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(752, 405);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu;
			this.Name = "MainForm";

		}
		#endregion

		internal EditorManager EditorManager
		{
			get { return _modules.EditorManager; }
		}

		internal EventViewerSettings Settings
		{
			get { return _settings; }
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main() 
		{
			var form = new MainForm();
			Application.ThreadException += form.Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += form.CurrentDomain_UnhandledException;
			Application.Run(form);
		}

		protected override void OnLoad(EventArgs e)
		{
			try
			{
				LoadSettings();

				string modulesFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
					+ Path.DirectorySeparatorChar + ModulesXmlFileName;
				_modules = new Modules(modulesFile);

				foreach (MessageReaderInfo readerInfo in _modules.MessageReaders)
				{
					mnuFileConnect.MenuItems.Add(new ConnectMenuItem(readerInfo.Name, readerInfo.DisplayName, connectMenuItem_Click));
				}

				foreach (MessageHandlerInfo handlerInfo in _modules.MessageHandlers)
				{
					mnuFileCopyAll.MenuItems.Add(new CopyMenuItem(handlerInfo.Name, false, handlerInfo.DisplayName, copyMenuItem_Click));
					mnuFileCopySelected.MenuItems.Add(new CopyMenuItem(handlerInfo.Name, true, handlerInfo.DisplayName, copyMenuItem_Click));
				}

				// Open the log form that was active when the application was last closed.

				if (_settings.LastActive != null)
				{
					ConnectToLog(_settings.LastActive);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);

				if (_modules == null)
				{
					Application.Exit(); // No point in running if we couldn't load the message readers.
				}
			}
			finally
			{
				base.OnLoad(e);
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			try
			{
				SaveSettings();
			}
			finally
			{
				base.OnClosing(e);
			}
		}

		internal void HandleException(Exception ex)
		{
			HandleException(ex, "The following exception has occurred:");
		}

		internal void HandleException(Exception ex, string heading)
		{
			if (IsDisposed)
				return; // An exception occurred as the program is exiting - ignore it.

			var dialog = new TC.ExceptionDialog(ex, heading);

			if (_settings != null)
			{
				_settings.ExceptionWindow.ApplyToWindow(dialog);
			}

			dialog.ShowDialog(this);

			if (_settings != null)
			{
				_settings.ExceptionWindow.ReadFromWindow(dialog);
			}
		}

		private void LoadSettings()
		{
			// Create the recently used list and pass it to the settings object

			_recentlyUsed = new RecentlyUsedList(mnuFile, RecentlyUsedMenuIndex);
			_recentlyUsed.Click += m_recentlyUsed_Click;
			_settings.RecentlyUsedList = _recentlyUsed;

			try
			{
				_settings.LoadXml();
			}
			catch (Exception ex)
			{
				// Handle the exception here, so we can continue running even if settings fail to load.
				HandleException(ex);
			}

			_settings.MainWindow.ApplyToWindow(this);
			Refresh();

			mnuViewPreviewPane.Checked = _settings.PreviewPane;
			mnuViewStatusBar.Checked = _settings.StatusBar;
			mnuViewAutoScroll.Checked = _settings.AutoScrollMessages;
			_recentlyUsed.Size = _settings.MaxRecentlyUsed;
		}

		private void SaveSettings()
		{
			try
			{
				_settings.MainWindow.ReadFromWindow(this);

				// Save the configuration string for the active Log Form, if any.

				var activeLogForm = ActiveMdiChild as LogForm;
				if (activeLogForm == null)
				{
					var detailsForm = ActiveMdiChild as DetailsForm;
					if (detailsForm != null)
					{
						activeLogForm = detailsForm.ParentLogForm;
					}
				}
				_settings.LastActive = (activeLogForm == null ? null : activeLogForm.ConfigurationString);

				_settings.SaveXml();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private void ConnectToLog(string fullConfigurationString)
		{
			// Format: readerName|displayName|configurationString

			int index = fullConfigurationString.IndexOf('|');
			if (index == -1)
				throw new ApplicationException("Invalid configuration string: '" + fullConfigurationString + "'.");

			string readerName = fullConfigurationString.Substring(0, index);
			index = fullConfigurationString.IndexOf('|', index + 1);
			if (index == -1)
				throw new ApplicationException("Invalid configuarion item: '" + fullConfigurationString + "'.");

			ConnectToLog(readerName, fullConfigurationString.Substring(index + 1));
		}

		private void ConnectToLog(string readerName, string initialisationString)
		{
            string displayString;
            IMessageReader reader;

			try
			{
                if (initialisationString == null)
                    initialisationString = _modules.GetInitialisationString(readerName);
                displayString = initialisationString ?? readerName;

			    // Create the message reader and set its initialisation string, prompting the user if necessary.

                reader = _modules.CreateMessageReader(readerName, initialisationString);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("Failed to initialise a '{0}' message reader"
					+ " using configuration string '{1}'.", readerName, initialisationString), ex);
			}

			// Not found - create and show a new log form for the message reader.

			var form = new LogForm {MdiParent = this};
#if DEBUG
			form.Closed += logForm_Closed;
#endif
			string displayText = displayString + " (" + _modules.GetDisplayName(readerName) + ")";

            string fullConfigurationString = readerName + "|" + displayString + "|" + initialisationString;
			form.ConfigurationString = fullConfigurationString;

			form.Initialise(reader, displayText);
			form.Show();

			_recentlyUsed.ItemUsed(fullConfigurationString, displayText);

			using (new LongRunningMonitor(this))
			{
				form.AutoStart(); // Now that the form is visible populate it with messages.
			}
		}

		private void CopyToLog(string handlerName, bool selectedOnly)
		{
			Debug.Assert(ActiveMdiChild is LogForm, "ActiveMdiChild is LogForm");

			// Create the message handler and set its initialisation string, prompting the user if necessary.

			IMessageHandler handler = _modules.CreateMessageHandler(handlerName);

            /*
			IMessageComponentInitialise initialise = handler as IMessageComponentInitialise;
			if ( initialise != null )
			{
				IConfigurationHandler configurationHandler = _modules.CreateMessageHandlerConfiguration(handlerName);
				if ( !initialise.Prompt(this, configurationHandler) )
					return;
			}
            */

			// Copy the selected messages.

			using (new LongRunningMonitor(this))
			{
				var childForm = (LogForm)ActiveMdiChild;
				IList<EventMessage> list = (selectedOnly ? childForm.GetSelectedEventMessages() : childForm.GetAllEventMessages());

				var messages = new EventMessages();
				foreach ( EventMessage message in list )
					messages.Add(message);

				handler.HandleEventMessages(messages);
			}
		}

		private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			HandleException(e.Exception);
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.ExceptionObject is Exception)
			{
				// Ignore ThreadAbortException - the message reader can throw these.

				if (!(e.ExceptionObject is ThreadAbortException))
				{
					HandleException((Exception)e.ExceptionObject,
						"The following unhandled exception has occurred:");
				}
			}
			else
			{
				MessageBox.Show(this, "An unhandled exception of type '" + e.ExceptionObject.GetType().FullName
					+ "' (which is not derived from System.Exception) has occurred in the default domain.",
					"Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		private void mnuFile_Popup(object sender, EventArgs e)
		{
			mnuFileCopyAll.Enabled = (ActiveMdiChild is LogForm);
			mnuFileCopySelected.Enabled = (ActiveMdiChild is LogForm);
		}

		private void connectMenuItem_Click(object sender, EventArgs e)
		{
			Debug.Assert(sender is ConnectMenuItem, "sender is ConnectMenuItem");
			ConnectToLog(((ConnectMenuItem)sender).MessageReaderName, null);
		}

		private void copyMenuItem_Click(object sender, EventArgs e)
		{
			Debug.Assert(sender is CopyMenuItem, "sender is CopyMenuItem");
			var menuItem = (CopyMenuItem)sender;
			CopyToLog(menuItem.MessageHandlerName, menuItem.SelectedOnly);
		}

		private void m_recentlyUsed_Click(object sender, RecentlyUsedClickEventArgs e)
		{
			ConnectToLog(e.ItemName);
		}

		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void mnuViewPreviewPane_Click(object sender, EventArgs e)
		{
			mnuViewPreviewPane.Checked = !mnuViewPreviewPane.Checked;
			_settings.PreviewPane = mnuViewPreviewPane.Checked;
		}

		private void mnuViewStatusBar_Click(object sender, EventArgs e)
		{
			mnuViewStatusBar.Checked = !mnuViewStatusBar.Checked;
			_settings.StatusBar = mnuViewStatusBar.Checked;
		}

		private void mnuViewAutoScroll_Click(object sender, EventArgs e)
		{
			mnuViewAutoScroll.Checked = !mnuViewAutoScroll.Checked;
			_settings.AutoScrollMessages = mnuViewAutoScroll.Checked;
		}

		private void mnuViewTimeZone_Click(object sender, EventArgs e)
		{
			var form = new TimeZoneForm();
			form.DisplayValue(_settings.DisplayTimeZone);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_settings.DisplayTimeZone = form.GetValue();
			}
		}

		private void mnuHelpAbout_Click(object sender, EventArgs e)
		{
			using (var form = new AboutForm())
			{
				form.ShowDialog(this);
			}
		}

		private void mnuDebugAttach_Click(object sender, EventArgs e)
		{
#if DEBUG
			Debugger.Launch();
#endif
		}

		private void mnuDebugMemoryLeaks_Click(object sender, EventArgs e)
		{
#if DEBUG
			// Force a garbage collection and wait for it to complete.

			GC.Collect();
			long totalMemory = GC.GetTotalMemory(true);

			// Check that all child MDI forms that are no longer open have been garbage collected.

			int index = 0;
			while (index < _wrForms.Count)
			{
				var wr = (WeakReference)_wrForms[index];

				if (wr.IsAlive)
				{
					Debug.Fail("Memory leak: '" + wr.Target
						+ "' has been closed, but was not collected by the garbage collection.");
					index++;
				}
				else
				{
					_wrForms.RemoveAt(index);
				}
			}

			MessageBox.Show(this, "Memory leak detection completed. Total memory: " + totalMemory.ToString("n0")
				+ " bytes.", "Memory leak detection", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
		}

#if DEBUG
		private void logForm_Closed(object sender, EventArgs e)
		{
			// Add a weak reference to the form being closed, so that we can check later whether it
			// really gets garbage collected as it should.
			_wrForms.Add(new WeakReference(sender, true));
		}
#endif
	}
}
