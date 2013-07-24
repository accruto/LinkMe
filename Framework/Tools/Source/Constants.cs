using System.Drawing;

namespace LinkMe.Framework.Tools.Constants
{
    public static class Colors
    {
        public static readonly Color ReadOnlyBackground = SystemColors.InactiveCaptionText;
    }

	internal static class Clipboard
	{
		internal const int MaxCopiedDataLength = 5000000;
	}

	public static class OleDb
	{
        public const string MsSqlProviderProgID = "SQLOLEDB";
        public const string MsSqlProviderDisplayName = "Microsoft OLE DB Provider for SQL Server";
	}

	internal static class Bitmap
	{
		internal const string Button = "LinkMe.Framework.Tools.Source.Controls.Bitmaps.Button.bmp";
		internal const string ButtonPressed = "LinkMe.Framework.Tools.Source.Controls.Bitmaps.ButtonPressed.bmp";
	}

    internal static class Win32
    {
        internal const int GW_CHILD = LinkMe.Framework.Utility.Constants.Win32.GW_CHILD;
        internal const int CFM_LINK = LinkMe.Framework.Utility.Constants.Win32.CFM_LINK;
        internal const int CFE_LINK = LinkMe.Framework.Utility.Constants.Win32.CFE_LINK;
        internal const int SCF_SELECTION = LinkMe.Framework.Utility.Constants.Win32.SCF_SELECTION;
        internal const int LVSCW_AUTOSIZE_USEHEADER = LinkMe.Framework.Utility.Constants.Win32.LVSCW_AUTOSIZE_USEHEADER;

        internal static class Messages
        {
            internal const int WM_CLOSE = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_CLOSE;
            internal const int WM_REFLECT = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_REFLECT;
            internal const int WM_NOTIFY = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_NOTIFY;
            internal const int WM_KEYUP = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_KEYUP;
            internal const int WM_LBUTTONDBLCLK = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_LBUTTONDBLCLK;
            internal const int WM_SETCURSOR = LinkMe.Framework.Utility.Constants.Win32.Messages.WM_SETCURSOR;
            internal const int EM_SETCHARFORMAT = LinkMe.Framework.Utility.Constants.Win32.Messages.EM_SETCHARFORMAT;
            internal const int EM_SETREADONLY = LinkMe.Framework.Utility.Constants.Win32.Messages.EM_SETREADONLY;
            internal const int EM_EMPTYUNDOBUFFER = LinkMe.Framework.Utility.Constants.Win32.Messages.EM_EMPTYUNDOBUFFER;
            internal const int TVM_DELETEITEM = LinkMe.Framework.Utility.Constants.Win32.Messages.TVM_DELETEITEM;
            internal const int TCM_HITTEST = LinkMe.Framework.Utility.Constants.Win32.Messages.TCM_HITTEST;
            internal const int LVM_GETHEADER = LinkMe.Framework.Utility.Constants.Win32.Messages.LVM_GETHEADER;
            internal const int LVM_SETCOLUMNWIDTH = LinkMe.Framework.Utility.Constants.Win32.Messages.LVM_SETCOLUMNWIDTH;
            internal const int CB_SHOWDROPDOWN = LinkMe.Framework.Utility.Constants.Win32.Messages.CB_SHOWDROPDOWN;
            internal const int CB_DELETESTRING = LinkMe.Framework.Utility.Constants.Win32.Messages.CB_DELETESTRING;
            internal const int CB_RESETCONTENT = LinkMe.Framework.Utility.Constants.Win32.Messages.CB_RESETCONTENT;
        }

        internal static class Notifications
        {
            internal const int TCN_SELCHANGING = LinkMe.Framework.Utility.Constants.Win32.Notifications.TCN_SELCHANGING;
            internal static readonly int TVN_SELCHANGING = LinkMe.Framework.Utility.Constants.Win32.Notifications.TVN_SELCHANGING;
            internal const int HDN_ENDTRACKA = LinkMe.Framework.Utility.Constants.Win32.Notifications.HDN_ENDTRACKA;
            internal const int HDN_ENDTRACKW = LinkMe.Framework.Utility.Constants.Win32.Notifications.HDN_ENDTRACKW;
            internal const int HDN_DIVIDERDBLCLICKA = LinkMe.Framework.Utility.Constants.Win32.Notifications.HDN_DIVIDERDBLCLICKA;
            internal const int HDN_DIVIDERDBLCLICKW = LinkMe.Framework.Utility.Constants.Win32.Notifications.HDN_DIVIDERDBLCLICKW;
        }
    }
}

