using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Settings;
using LinkMeType = LinkMe.Framework.Type;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class EventViewerSettings : SettingsBase
	{
		internal const string ProductName = "LinkMe Event Viewer";
		internal const string EventViewerXmlPrefix = "ev";
		internal const string EventViewerXmlNamespace = "http://xmlns.linkme.com.au/Framework/Instrumentation/EventViewer";

		internal const string SectionMessageReader = "messageReader";
		internal const string SettingRemoveWhenReading = "removeWhenReading";
		internal const string SettingMaxCountToAutoStart = "maxCountToAutoStart";

		internal const string SectionVisual = "visual";
		internal const string SettingMainWindow = "mainWindow";
		internal const string SettingLogWindow = "logWindow";
		internal const string SettingDetailsWindow = "detailsWindow";
		internal const string SettingExceptionWindow = "exceptionWindow";
		internal const string SettingGenericEditorWindow = "genericEditorWindow";
		internal const string SettingLogColumnWidths = "logColumnWidths";
		internal const string SettingLogSplitterRatio = "logSplitterRatio";
		internal const string SettingDetailsSplitterRatioTop = "detailsSplitterRatioTop";
		internal const string SettingDetailsSplitterRatioBottom = "detailsSplitterRatioBottom";
		internal const string SettingDetailColumnWidths = "detailColumnWidths";
		internal const string SettingParameterColumnWidths = "parameterColumnWidths";

		internal const string SectionView = "view";
		internal const string SettingPreviewPane = "previewPane";
		internal const string SettingStatusBar = "statusBar";
		internal const string SettingAutoScrollMessages = "autoScrollMessages";
		internal const string SettingDisplayTimeZone = "displayTimeZone";

		internal const string SectionRecentlyUsed = "recentlyUsed";
		internal const string SettingRecentlyUsedList = "recentlyUsedList";
		internal const string SettingMaxRecentlyUsed = "maxRecentlyUsed";
		internal const string SettingLastActive = "lastActive";

		private const bool DefaultRemoveWhenReading = false;
		private const int DefaultMaxCountToAutoStart = 10000;
		private const float DefaultLogSplitterRatio = 0.8F;
		private const float DefaultDetailsSplitterRatioTop = 0.5F;
		private const float DefaultDetailsSplitterRatioBottom = 0.667F;
		private const bool DefaultPreviewPane = true;
		private const bool DefaultStatusBar = true;
		private const bool DefaultAutoScrollMessage = true;
		private const int DefaultMaxRecentlyUsed = 6;
		private const string CurrentTimeZoneName = "(CurrentTimeZone)";

		private static readonly Version m_version = new Version(1, 0, 0, 0);
		private static readonly int[] DefaultLogColumnWidths = new int[] { 150, 90, 150, 100, 100 };
		private static readonly System.TimeZone DefaultDisplayTimeZone = System.TimeZone.CurrentTimeZone;

		public EventViewerSettings()
		{
		}

		#region Constant properties

		public override Version HighestSupportedVersion
		{
			get { return m_version; }
		}

		public override Version LowestSupportedVersion
		{
			get { return m_version; }
		}

		public override string SettingsXmlPrefix
		{
			get { return EventViewerXmlPrefix; }
		}

		public override string SettingsXmlNamespace
		{
			get { return EventViewerXmlNamespace; }
		}

		protected override string ObjectDisplayType
		{
			get { return "Event Viewer settings"; }
		}

		protected override string SettingsXmlName
		{
			get { return "EventViewerSettings"; }
		}

		#endregion

		public bool RemoveWhenReading
		{
			get { return (bool)this[SettingRemoveWhenReading]; }
			set { this[SettingRemoveWhenReading] = value; }
		}

		public int MaxCountToAutoStart
		{
			get { return (int)this[SettingMaxCountToAutoStart]; }
			set { this[SettingMaxCountToAutoStart] = value; }
		}

		public WindowSettings MainWindow
		{
			get { return (WindowSettings)this[SettingMainWindow]; }
		}

		public WindowSettings LogWindow
		{
			get { return (WindowSettings)this[SettingLogWindow]; }
		}

		public WindowSettings DetailsWindow
		{
			get { return (WindowSettings)this[SettingDetailsWindow]; }
		}

		public WindowSettings ExceptionWindow
		{
			get { return (WindowSettings)this[SettingExceptionWindow]; }
		}

		public WindowSettings GenericEditorWindow
		{
			get { return (WindowSettings)this[SettingGenericEditorWindow]; }
		}

		public int[] LogColumnWidths
		{
			get { return (int[])this[SettingLogColumnWidths]; }
			set { this[SettingLogColumnWidths] = value; }
		}

		public float LogSplitRatio
		{
			get { return (float)this[SettingLogSplitterRatio]; }
			set { this[SettingLogSplitterRatio] = value; }
		}

		public float DetailsSplitterRatioTop
		{
			get { return (float)this[SettingDetailsSplitterRatioTop]; }
			set { this[SettingDetailsSplitterRatioTop] = value; }
		}

		public float DetailsSplitterRatioBottom
		{
			get { return (float)this[SettingDetailsSplitterRatioBottom]; }
			set { this[SettingDetailsSplitterRatioBottom] = value; }
		}

		public TC.DataGrid.ColumnWidths DetailColumnWidths
		{
			get { return (TC.DataGrid.ColumnWidths)this[SettingDetailColumnWidths]; }
			set { this[SettingDetailColumnWidths] = value; }
		}

		public TC.DataGrid.ColumnWidths ParameterColumnWidths
		{
			get { return (TC.DataGrid.ColumnWidths)this[SettingParameterColumnWidths]; }
			set { this[SettingParameterColumnWidths] = value; }
		}

		public bool PreviewPane
		{
			get { return (bool)this[SettingPreviewPane]; }
			set { this[SettingPreviewPane] = value; }
		}

		public bool StatusBar
		{
			get { return (bool)this[SettingStatusBar]; }
			set { this[SettingStatusBar] = value; }
		}

		public bool AutoScrollMessages
		{
			get { return (bool)this[SettingAutoScrollMessages]; }
			set { this[SettingAutoScrollMessages] = value; }
		}

		public System.TimeZone DisplayTimeZone
		{
			get { return (System.TimeZone)this[SettingDisplayTimeZone]; }
			set { this[SettingDisplayTimeZone] = value; }
		}

		public int MaxRecentlyUsed
		{
			get { return (int)this[SettingMaxRecentlyUsed]; }
			set { this[SettingMaxRecentlyUsed] = value; }
		}

		public ISettingsObject RecentlyUsedList
		{
			get { return (ISettingsObject)this[SettingRecentlyUsedList]; }
			set { this[SettingRecentlyUsedList] = value; }
		}

		public string LastActive
		{
			get { return (string)this[SettingLastActive]; }
			set { this[SettingLastActive] = value; }
		}

		public override string DefaultSettingsFileName
		{
			get { return "LinkMe.Framework.Instrumentation.Tools.EventViewer.Settings.config"; }
		}

		protected override void AddSettingNames(SettingNames names)
		{
			names.Add(SettingRemoveWhenReading, SettingMaxCountToAutoStart);

			names.Add(SettingMainWindow, SettingLogWindow, SettingDetailsWindow,
				SettingExceptionWindow, SettingGenericEditorWindow, SettingLogColumnWidths,
				SettingLogSplitterRatio, SettingDetailsSplitterRatioTop, SettingDetailsSplitterRatioBottom,
				SettingDetailColumnWidths, SettingParameterColumnWidths);

			names.Add(SettingPreviewPane, SettingStatusBar, SettingAutoScrollMessages, SettingDisplayTimeZone);

			names.Add(SettingMaxRecentlyUsed, SettingRecentlyUsedList, SettingLastActive);
		}

		protected override void AddInitialValues(SettingValues values)
		{
			values.Add(DefaultRemoveWhenReading, DefaultMaxCountToAutoStart);

			values.Add(new WindowSettings(FormWindowState.Maximized), new WindowSettings(), new WindowSettings(),
				new WindowSettings(), new WindowSettings(), DefaultLogColumnWidths,
				DefaultLogSplitterRatio, DefaultDetailsSplitterRatioTop, DefaultDetailsSplitterRatioBottom,
				null, null);

			values.Add(DefaultPreviewPane, DefaultStatusBar, DefaultAutoScrollMessage, DefaultDisplayTimeZone);

			values.Add(DefaultMaxRecentlyUsed, new RecentlyUsedList(), null);
		}

		protected override void ReadAllSettings(XmlNode xmlSettings, XmlNamespaceManager xmlNsManager)
		{
			// Message Reader

			XmlNode xmlSection = GetSection(xmlSettings, xmlNsManager, SectionMessageReader, false);
			if (xmlSection != null)
			{
				RemoveWhenReading = ReadSettingBoolean(xmlSection, xmlNsManager, SettingRemoveWhenReading,
					DefaultRemoveWhenReading);
				MaxCountToAutoStart = ReadSettingInt32(xmlSection, xmlNsManager, SettingMaxCountToAutoStart,
					DefaultMaxCountToAutoStart);
			}

			// Visual

			xmlSection = GetSection(xmlSettings, xmlNsManager, SectionVisual, false);
			if (xmlSection != null)
			{
				ReadSettingIntoObject(xmlSection, xmlNsManager, SettingMainWindow, MainWindow);
				ReadSettingIntoObject(xmlSection, xmlNsManager, SettingLogWindow, LogWindow);
				ReadSettingIntoObject(xmlSection, xmlNsManager, SettingDetailsWindow, DetailsWindow);
				ReadSettingIntoObject(xmlSection, xmlNsManager, SettingExceptionWindow, ExceptionWindow);
				ReadSettingIntoObject(xmlSection, xmlNsManager, SettingGenericEditorWindow, GenericEditorWindow);
				LogColumnWidths = ReadSettingInt32Array(xmlSection, xmlNsManager, SettingLogColumnWidths,
					DefaultLogColumnWidths);
				LogSplitRatio = ReadSettingSingle(xmlSection, xmlNsManager, SettingLogSplitterRatio,
					DefaultLogSplitterRatio);
				DetailsSplitterRatioTop = ReadSettingSingle(xmlSection, xmlNsManager, SettingDetailsSplitterRatioTop,
					DefaultDetailsSplitterRatioTop);
				DetailsSplitterRatioBottom = ReadSettingSingle(xmlSection, xmlNsManager,
					SettingDetailsSplitterRatioBottom, DefaultDetailsSplitterRatioBottom);

				TC.DataGrid.ColumnWidths widths = new TC.DataGrid.ColumnWidths();
				if (ReadSettingIntoObject(xmlSection, xmlNsManager, SettingDetailColumnWidths, widths))
				{
					DetailColumnWidths = widths;
				}

				widths = new TC.DataGrid.ColumnWidths();
				if (ReadSettingIntoObject(xmlSection, xmlNsManager, SettingParameterColumnWidths, widths))
				{
					ParameterColumnWidths = widths;
				}
			}

			// View

			xmlSection = GetSection(xmlSettings, xmlNsManager, SectionView, false);
			if (xmlSection != null)
			{
				PreviewPane = ReadSettingBoolean(xmlSection, xmlNsManager, SettingPreviewPane,
					DefaultPreviewPane);
				StatusBar = ReadSettingBoolean(xmlSection, xmlNsManager, SettingStatusBar,
					DefaultStatusBar);
				AutoScrollMessages = ReadSettingBoolean(xmlSection, xmlNsManager, SettingAutoScrollMessages,
					DefaultAutoScrollMessage);

				// Read the standard name of the time zone and create a TimeZone object.

				string tzName = ReadSettingString(xmlSection, xmlNsManager, SettingDisplayTimeZone, null);
				if (tzName == null || tzName == CurrentTimeZoneName)
				{
					DisplayTimeZone = System.TimeZone.CurrentTimeZone;
				}
				else
				{
					try
					{
						DisplayTimeZone = new LinkMeType.TimeZone(tzName);
					}
					catch (System.Exception ex)
					{
						// If the time zone is not found don't prevent the rest of the settings from loading.

						new TC.ExceptionDialog(ex, "An error occurred in reading the Display Time Zone from"
							+ " the settings. Local time will be used instead.",
							MessageBoxButtons.OK, MessageBoxIcon.Warning).ShowDialog();
						DisplayTimeZone = System.TimeZone.CurrentTimeZone;
					}
				}
			}

			// Recently used

			xmlSection = GetSection(xmlSettings, xmlNsManager, SectionRecentlyUsed, false);
			if (xmlSection != null)
			{
				MaxRecentlyUsed = ReadSettingInt32(xmlSection, xmlNsManager, SettingMaxRecentlyUsed,
					DefaultMaxRecentlyUsed);
				ReadSettingIntoObject(xmlSection, xmlNsManager,  SettingRecentlyUsedList, RecentlyUsedList);
				LastActive = ReadSettingString(xmlSection, xmlNsManager, SettingLastActive, null);
			}
		}

		protected override void WriteAllSettings(XmlWriter writer)
		{
			// Message Reader

			StartSection(writer, SectionMessageReader);
			WriteSetting(writer, SettingRemoveWhenReading, XmlConvert.ToString(RemoveWhenReading));
			WriteSetting(writer, SettingMaxCountToAutoStart, XmlConvert.ToString(MaxCountToAutoStart));
			EndSection(writer);

			// Visual

			StartSection(writer, SectionVisual);
			WriteSetting(writer, SettingMainWindow, MainWindow);
			WriteSetting(writer, SettingLogWindow, LogWindow);
			WriteSetting(writer, SettingDetailsWindow, DetailsWindow);
			WriteSetting(writer, SettingExceptionWindow, ExceptionWindow);
			WriteSetting(writer, SettingGenericEditorWindow, GenericEditorWindow);
			WriteSetting(writer, SettingLogColumnWidths, LogColumnWidths);
			WriteSetting(writer, SettingLogSplitterRatio, XmlConvert.ToString(LogSplitRatio));
			WriteSetting(writer, SettingDetailsSplitterRatioTop, XmlConvert.ToString(DetailsSplitterRatioTop));
			WriteSetting(writer, SettingDetailsSplitterRatioBottom, XmlConvert.ToString(DetailsSplitterRatioBottom));
			WriteSetting(writer, SettingDetailColumnWidths, DetailColumnWidths);
			WriteSetting(writer, SettingParameterColumnWidths, ParameterColumnWidths);
			EndSection(writer);

			// View

			StartSection(writer, SectionView);
			WriteSetting(writer, SettingPreviewPane, XmlConvert.ToString(PreviewPane));
			WriteSetting(writer, SettingStatusBar, XmlConvert.ToString(StatusBar));
			WriteSetting(writer, SettingAutoScrollMessages, XmlConvert.ToString(AutoScrollMessages));

			// Write the standard name of the time zone or "(CurrentTimeZone)" for the current time zone.

			System.TimeZone timeZone = DisplayTimeZone;
			if (timeZone == null || timeZone == System.TimeZone.CurrentTimeZone)
			{
				WriteSetting(writer, SettingDisplayTimeZone, CurrentTimeZoneName);
			}
			else
			{
				Debug.Assert(timeZone is LinkMeType.TimeZone, "Unexpected type of TimeZone: " + timeZone.GetType().FullName);
				WriteSetting(writer, SettingDisplayTimeZone, ((LinkMeType.TimeZone)timeZone).StandardName);
			}

			EndSection(writer);

			// Recently used

			StartSection(writer, SectionRecentlyUsed);
			WriteSetting(writer, SettingMaxRecentlyUsed, XmlConvert.ToString(MaxRecentlyUsed));
			WriteSetting(writer, SettingRecentlyUsedList, RecentlyUsedList);
			WriteSetting(writer, SettingLastActive, LastActive);
			EndSection(writer);
		}
	}
}
