using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace LinkMe.Framework.Tools.Mmc
{
	#region Persist Interfaces

	[
		ComImport,
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		Guid("0000010c-0000-0000-C000-000000000046")
	]
	internal interface IPersist 
	{
		void GetClassID(out Guid pClassID);
	};
    
	[
		ComImport,
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
		Guid("00000109-0000-0000-C000-000000000046")
	]	
	internal interface IPersistStream : IPersist 
	{
		new void GetClassID(out Guid pClassID);

		[PreserveSig] int IsDirty();
		void Load([In] IStream pStm);
		void Save([In] IStream pStm, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);
		void GetSizeMax(out long pcbSize);
	};

	#endregion

	#region MMC Interfaces

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("955AB28A-5218-11D0-A985-00C04FD8D565")
	]
	internal interface IComponentData
	{
		void Initialize([MarshalAs(UnmanagedType.Interface)] object pUnknown);
		void CreateComponent(out IComponent ppComponent);
		[PreserveSig] int Notify(IDataObject lpDataObject, uint aevent, IntPtr arg, IntPtr param);
		void Destroy();
		void QueryDataObject(IntPtr cookie, uint type, out IDataObject ppDataObject);
		void GetDisplayInfo(ref ScopeDataItem ResultDataItem);
		[PreserveSig] int CompareObjects(IDataObject lpDataObjectA, IDataObject lpDataObjectB);
	}

	[
		ComImport,
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("43136EB2-D36C-11CF-ADBC-00AA00A80033")
	]
	internal interface IComponent
	{
		void Initialize([MarshalAs(UnmanagedType.Interface)]object lpConsole);
		[PreserveSig] int Notify(IntPtr lpDataObject, uint aevent, IntPtr arg, IntPtr param);
		void Destroy(IntPtr cookie);
        void QueryDataObject(IntPtr cookie, uint type, out IDataObject ppDataObject);
		[PreserveSig] int GetResultViewType(IntPtr cookie, out IntPtr ppViewType, out int pViewOptions);
		void GetDisplayInfo(ref ResultDataItem ResultDataItem);
		[PreserveSig] int CompareObjects(IDataObject lpDataObjectA, IDataObject lpDataObjectB);
	}

	[   
		ComImport,
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("1245208C-A151-11D0-A7D7-00C04FD909DD")
	]
	internal interface ISnapinAbout
	{
		void GetSnapinDescription(out IntPtr lpDescription);
		void GetProvider(out IntPtr pName);
		void GetSnapinVersion(out IntPtr lpVersion);
		void GetSnapinImage(out IntPtr hAppIcon);
		void GetStaticFolderImage(out IntPtr hSmallImage, out IntPtr hSmallImageOpen, out IntPtr hLargeImage, out uint cMask);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("0000010e-0000-0000-C000-000000000046")
	]
	internal interface IDataObject
	{
		[PreserveSig] int GetData(ref FormatEtc pFormatEtc, ref StgMedium b);
		void GetDataHere(ref FormatEtc pFormatEtc, ref StgMedium b);
		[PreserveSig] int QueryGetData(IntPtr a);
		[PreserveSig] int GetCanonicalFormatEtc(IntPtr a, IntPtr b);
		[PreserveSig] int SetData(IntPtr a, IntPtr b, int c);
		[PreserveSig] int EnumFormatEtc(uint a, IntPtr b);
		[PreserveSig] int DAdvise(IntPtr a, uint b, IntPtr c, ref uint d);
		[PreserveSig] int DUnadvise(uint a);
		[PreserveSig] int EnumDAdvise(IntPtr a);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("4861A010-20F9-11d2-A510-00C04FB6DD2C")
	]
	internal interface ISnapinHelp2
	{
		[PreserveSig] int GetHelpTopic(out IntPtr lpCompiledHelpFile);
		[PreserveSig] int GetLinkedTopics(out IntPtr lpCompiledHelpFiles);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("cc593830-b926-11d1-8063-0000f875a9ce")
	]
	internal interface IDisplayHelp
	{
		void ShowTopic([MarshalAs(UnmanagedType.LPWStr)] string pszHelpTopic);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("4F3B7A4F-CFAC-11CF-B8E3-00C04FD8D5B0")
	]
	internal interface IExtendContextMenu
	{
		void AddMenuItems(IntPtr piDataObject, IContextMenuCallback piCallback, ref int pInsertionAllowed);
		void Command(int lCommandID, IntPtr piDataObject);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("49506520-6F40-11D0-A98B-00C04FD8D565")
	]
	internal interface IExtendControlbar
	{
		void SetControlbar(IControlbar pControlbar);
		[PreserveSig] int ControlbarNotify(uint aevent, IntPtr arg, IntPtr param);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("85DE64DC-EF21-11cf-A285-00C04FD8DBE6")
	]
	internal interface IExtendPropertySheet
	{
		[PreserveSig] int CreatePropertyPages(IPropertySheetCallback lpProvider, IntPtr handle, IDataObject lpIDataObject);
		[PreserveSig] int QueryPagesFor(IDataObject lpDataObject);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("85DE64DE-EF21-11cf-A285-00C04FD8DBE6")
	]
	internal interface IPropertySheetProvider
	{
		void CreatePropertySheet([MarshalAs(UnmanagedType.LPWStr)] string title, int type, IntPtr cookie, IDataObject pIDataObjectm, uint dwOptions);
        void FindPropertySheet(IntPtr hItem, IComponent lpComponent, IDataObject lpDataObject);
        void AddPrimaryPages([MarshalAs(UnmanagedType.Interface)]object lpUnknown, int bCreateHandle, IntPtr hNotifyWindow, int bScopePane);
		void AddExtensionPages();
		void Show(int window, int page);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("85DE64DD-EF21-11cf-A285-00C04FD8DBE6")
	]
	internal interface IPropertySheetCallback
	{
		void AddPage(IntPtr hPage);
		void RemovePage(IntPtr hPage);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("255F18CC-65DB-11D1-A7DC-00C04FD8D565")
	]
	internal interface IConsoleNameSpace2
	{
		void InsertItem(ref ScopeDataItem a);
		void DeleteItem(IntPtr hItem, int fDeleteThis);
		void SetItem(ref ScopeDataItem a);
		void GetItem(ref ScopeDataItem a);
		void GetChildItem(IntPtr hItem, out IntPtr pItemChild, out IntPtr pCookie);
        void GetNextItem(IntPtr hItem, out IntPtr pItemNext, out IntPtr pCookie);
        void GetParentItem(IntPtr hItem, out IntPtr pItemParent, out IntPtr pCookie);
		void Expand(IntPtr hItem);
		void AddExtension(ref ScopeDataItem hItem, CLSID lpClsid); 
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("9757abb8-1b32-11d1-a7ce-00c04fd8d565")
	]
	internal interface IHeaderCtrl2
	{
		void InsertColumn(int nCol, [MarshalAs(UnmanagedType.LPWStr)] string title, int nFormat, int nWidth);
		void DeleteColumn(int nCol);
		void SetColumnText(int nCol, [MarshalAs(UnmanagedType.LPWStr)] string title);
		void GetColumnText(int nCol, out int pText);
		void SetColumnWidth(int nCol, int nWidth);
		void GetColumnWidth(int nCol, out int pWidth);
		void SetChangeTimeOut( uint uTimeout);
		void SetColumnFilter(uint nColumn, uint dwType, IntPtr pFilterData);
		int GetColumnFilter(uint nColumn, ref uint pdwType, ref MmcFilterData data);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("31DA5FA0-E0EB-11cf-9F21-00AA003CA9F6")
	]
	internal interface IResultData 
	{
		void InsertItem(ref ResultDataItem item);
		void DeleteItem(IntPtr itemID, int nCol);
        void FindItemByLParam(IntPtr lParam, out IntPtr pItemID);
		void DeleteAllRsltItems();
		void SetItem(ref ResultDataItem item);
		void GetItem(ref ResultDataItem item);
		void GetNextItem(ref ResultDataItem item);
        void ModifyItemState(int nIndex, IntPtr itemID, uint uAdd, uint uRemove);
		void ModifyViewStyle(int add, int remove);
		void SetViewMode(int lViewMode);
		void GetViewMode(out int lViewMode);
        void UpdateItem(IntPtr itemID);
        void Sort(int nColumn, uint dwSortOptions, IntPtr lUserParam);
		void SetDescBarText([MarshalAs(UnmanagedType.LPWStr)] ref string DescText);
		void SetItemCount(int nItemCount, uint dwOptions);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("43136EB8-D36C-11CF-ADBC-00AA00A80033")
	]
	internal interface IImageList
	{
		void ImageListSetIcon(IntPtr pIcon, int nLoc);
        void ImageListSetStrip(IntPtr pBMapSm, IntPtr pBMapLg, int nStartLoc, int cMask);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("103D842A-AA63-11D1-A7E1-00C04FD8D565")
	]
	internal interface IConsole2
	{
		void SetHeader(ref IHeaderCtrl2 pHeader);
		void SetToolbar([MarshalAs(UnmanagedType.Interface)] ref object pToolbar);
		void QueryResultView([MarshalAs(UnmanagedType.Interface)] out object pUnknown);
		void QueryScopeImageList(out IImageList ppImageList);
		void QueryResultImageList(out IImageList ppImageList);
        void UpdateAllViews(IDataObject lpDataObject, IntPtr data, IntPtr hint);
		void MessageBox([MarshalAs(UnmanagedType.LPWStr)] string lpszText,
			[MarshalAs(UnmanagedType.LPWStr)] string lpszTitle, uint fuStyle, ref int piRetval);
		void QueryConsoleVerb(out IConsoleVerb ppConsoleVerb);
		void SelectScopeItem(IntPtr hScopeItem);
        void GetMainWindow(ref IntPtr phwnd);
        void NewWindow(IntPtr hScopeItem, uint lOptions);
		void Expand(IntPtr hItem, int bExpand);
		void IsTaskpadViewPreferred();
		void SetStatusText([MarshalAs(UnmanagedType.LPWStr)]string pszStatusText);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("43136EB7-D36C-11CF-ADBC-00AA00A80033")
	]
	internal interface IContextMenuCallback
	{
		void AddItem(ref MmcContextMenuItem pItem);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("69FB811E-6C1C-11D0-A2CB-00C04FD909DD")
	]
	internal interface IControlbar
	{
		void Create(MmcControlType nType, IExtendControlbar pExtendControlbar, [MarshalAs(UnmanagedType.Interface)] out object ppUnknown);
		void Attach(MmcControlType nType, [MarshalAs(UnmanagedType.Interface)] object lpUnknown);
		void Detach([MarshalAs(UnmanagedType.Interface)] object lpUnknown);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("43136EB9-D36C-11CF-ADBC-00AA00A80033")
	]
	internal interface IToolbar
	{
		void AddBitmap(int nImages, IntPtr hbmp, int cxSize, int cySize, int crMask);
		void AddButtons(int nButtons,  ref MmcButton lpButtons);
		void InsertButton(int nIndex, ref MmcButton lpButton);
		void DeleteButton(int nIndex);
		void GetButtonState(int idCommand, MmcButtonState nState, out int pState);
		void SetButtonState(int idCommand, MmcButtonState nState, int bState);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("E49F7A60-74AF-11D0-A286-00C04FD8FE93")
	]
	internal interface IConsoleVerb 
	{
		void GetVerbState(MmcConsoleVerb eCmdID, MmcButtonState nState, ref int pState);
		void SetVerbState(MmcConsoleVerb eCmdID, MmcButtonState nState, int bState);
		void SetDefaultVerb(MmcConsoleVerb eCmdID);
		void GetDefaultVerb(ref MmcConsoleVerb peCmdID);
	}

	[
		ComImport, 
		InterfaceType(ComInterfaceType.InterfaceIsIUnknown), 
		Guid("DCA43F48-C74C-4c3e-94DD-84EFC2827F46")
	]
	internal interface IMmcFormsShim
	{
		IntPtr HostUserControl(string strAssembly, string strClass); 
		IntPtr RehostUserControl(object pControlObject);
	}

	[Guid("320879B0-AFD9-4277-9543-788AB588EDF0")]
	internal interface ISnapinLink
	{
		SnapinNode ContextNode { get; set; }
	}

	#endregion

	#region Enumerations

	internal enum CookieType
	{
		MultiSelectCookie			= -2
	}

	public enum ListViewItemState
	{
		Focused						= 0x0001,	// LVIS_FOCUSED
		Selected					= 0x0002	// LVIS_SELECTED
	}

	public enum ListViewColumnFormat
	{
		Left						= 0x0000,	// LVCFMT_LEFT
		Right						= 0x0001,	// LVCFMT_RIGHT
		Center						= 0x0002,	// LVCFMT_CENTER
	}

	public enum ListViewColumnWidth
	{
		Auto						= -1,		// MMCLV_AUTO
		HideColumn					= -4		// HIDE_COLUMN
	}

	public enum ListViewMode
	{
		Icon        = 0,
		Report      = 1,
		SmallIcon   = 2,
		List        = 3,
		Filtered    = 4,
	}

	public enum ToolbarButtonState
	{
		Checked						= 0x01,		// TBSTATE_CHECKED
		Pressed						= 0x02,		// TBSTATE_PRESSED
		Enabled						= 0x04,		// TBSTATE_ENABLED
		Hidden						= 0x08,		// TBSTATE_HIDDEN
		Indeterminate				= 0x10,		// TBSTATE_INDETERMINATE
		Wrap						= 0x20,		// TBSTATE_WRAP
		Ellipses					= 0x40,		// TBSTATE_ELLIPSES
		Marked						= 0x80,		// TBSTATE_MARKED
	}

	public enum ToolbarStyle
	{
		Button						= 0x0000,	// TBSTYLE_BUTTON
		Sep							= 0x0001,	// TBSTYLE_SEP
		Check						= 0x0002,	// TBSTYLE_CHECK
		Group						= 0x0004,	// TBSTYLE_GROUP
		CheckGroup					= Group | Check,	// TBSTYLE_CHECKGROUP
		Dropdown					= 0x0008,	// TBSTYLE_DROPDOWN
		Autosize					= 0x0010,	// TBSTYLE_AUTOSIZE
		NoPrefix					= 0x0020,	// TBSTYLE_NOPREFIX
	}

	internal enum MmcFilterChangeCode
	{
		Disable						= 0,		// MFCC_DISABLE
		Enable						= 1,		// MFCC_ENABLE
		ValueChange					= 2			// MFCC_VALUE_CHANGE
	}

	public enum MmcButtonState
	{
		Enabled					= 0x1,		// ENABLED
		Checked					= 0x2,		// CHECKED
		Hidden					= 0x4,		// HIDDEN
		Indeterminate			= 0x8,		// INDETERMINATE
		ButtonPressed			= 0x10		// BUTTONPRESSED
	}

	internal enum MmcConsoleVerb
	{
		None			= 0x0000,		// NONE
		Open			= 0x8000,		// OPEN
		Copy			= 0x8001,		// COPY
		Paste			= 0x8002,		// PASTE
		Delete			= 0x8003,		// DELETE
		Properties		= 0x8004,		// PROPERTIES
		Rename			= 0x8005,		// RENAME
		Refresh			= 0x8006,		// REFRESH
		Print			= 0x8007,		// PRINT
		Cut				= 0x8008,		// CUT
		Max				= 0x8009,		// MAX
		First			= Open,			// FIRST
		Last			= Max - 1		// LAST
	}

	internal enum MmcControlType
	{
		Toolbar					= 0,				// TOOLBAR
		MenuButton				= Toolbar + 1,		// MENUBUTTON
		ComboBoxBar				= MenuButton + 1	// COMBOBOXBAR
	}

 	public enum MmcViewOptions
	{
		None						= 0,			// NONE
		CreateNew					= 0x00000010,	// CREATENEW
		ExcludeScopeItemsFromList	= 0x00000040,	// EXCLUDE_SCOPE_ITEMS_FROM_LIST
		Filtered					= 0x00000008,	// FILTERED
		LexicalSort					= 0x00000080,	// LEXICAL_SORT
		NoListViews					= 0x00000001,	// NOLISTVIEWS
		MultiSelect					= 0x00000002,	// MULTISELECT
		OwnerDataList				= 0x00000004,	// OWNERDATALIST
		UseFontLinking				= 0x00000020,	// USEFONTLINKING
		Activate					= 0x00000006,	// ACTIVATE
	}

	internal enum MmcNotify
	{
		Activate			= 0x8001,	// MMCN_ACTIVATE
		AddImages			= 0x8002,	// MMCN_ADD_IMAGES
		BtnClick			= 0x8003,	// MMCN_BTN_CLICK
		Click				= 0x8004,	// MMCN_CLICK
		ColumnClick			= 0x8005,	// MMCN_COLUMN_CLICK
		ContextMenu			= 0x8006,	// MMCN_CONTEXTMENU
		CutOrMove			= 0x8007,	// MMCN_CUTORMOVE
		DblClick			= 0x8008,	// MMCN_DBLCLICK
		Delete				= 0x8009,	// MMCN_DELETE
		DeselectAll			= 0x800A,	// MMCN_DESELECT_ALL
		Expand				= 0x800B,	// MMCN_EXPAND
		Help				= 0x800C,	// MMCN_HELP
		MenuBtnClick		= 0x800D,	// MENU_BTNCLICK
		Minimized			= 0x800E,	// MMCN_MINIMIZED
		Paste				= 0x800F,	// MMCN_PASTE
		PropertyChange		= 0x8010,	// MMCN_PROPERTY_CHANGE
		QueryPaste			= 0x8011,	// MMCN_QUERY_PASTE
		Refresh				= 0x8012,	// MMCN_MMCN_REFRESH
		RemoveChildren		= 0x8013,	// MMCN_REMOVE_CHILDREN
		Rename				= 0x8014,	// MMCN_RENAME
		Select				= 0x8015,	// MMCN_SELECT
		Show				= 0x8016,	// MMCN_SHOW
		ViewChange			= 0x8017,	// MMCN_VIEW_CHANGE
		SnapinHelp			= 0x8018,	// MMCN_SNAPINHELP
		ContextHelp			= 0x8019,	// MMCN_CONTEXTHELP
		InitOcx				= 0x801A,	// MMCN_INITOCX
		FilterChange		= 0x801B,	// MMCN_FILTER_CHANGE
		GetFilterMenu		= 0x801C,	// MMCN_GET_FILTER_MENU
		FilterOperator		= 0x801D,	// MMCN_FILTER_OPERATOR
		Print				= 0x801E,	// MMCN_PRINT
		Preload				= 0x801F,	// MMCN_PRELOAD
		ListPad				= 0x8020,	// MMCN_LISTPAD
		ExpandSync			= 0x8021,	// MMCN_EXPANDSYNC
		ColumnsChanged		= 0x8022,	// MMCN_COLUMNS_CHANGED
		CanPasteOutOfProc	= 0x8023,	// MMCN_CANPASTE_OUTOFPROC
	}

 	internal enum ScopeDataItemFlags
	{
		Str					= 0x2,			// SDI_STR
		Image				= 0x4,			// SDI_IMAGE
		OpenImage			= 0x8,			// SDI_OPENIMAGE
		State				= 0x10,			// SDI_STATE
		Param				= 0x20,			// SDI_PARAM
		Children			= 0x40,			// SDI_CHILDREN
		Parent				= 0,			// SDI_PARENT
		Previous			= 0x10000000,	// SDI_PREVIOUS
		Next				= 0x20000000,	// SDI_NEXT
		First				= 0x8000000,	// SDI_FIRST
	}

	internal enum ResultDataItemFlags
	{
		Str					= 0x2,		// RDI_STR
		Image				= 0x4,		// RDI_IMAGE
		State				= 0x8,		// RDI_STATE
		Param				= 0x10,		// RDI_PARAM
		Index				= 0x20,		// RDI_INDEX
		Indent				= 0x40,		// RDI_INDENT
	}

	internal class ActivationContext
	{
		public const uint ProcessorArchitectureValid	= 0x001;	// ACTCTX_FLAG_PROCESSOR_ARCHITECTURE_VALID
		public const uint LangIdValid					= 0x002;	// ACTCTX_FLAG_LANGID_VALID
		public const uint AssemblyDirectoryValid		= 0x004;	// ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID
		public const uint ResourceNameValid				= 0x008;	// ACTCTX_FLAG_RESOURCE_NAME_VALID
		public const uint SetProcessDefault				= 0x010;	// ACTCTX_FLAG_SET_PROCESS_DEFAULT
		public const uint ApplicationNameValid			= 0x020;	// ACTCTX_FLAG_APPLICATION_NAME_VALID
		public const uint HModuleValid					= 0x080;	// ACTCTX_FLAG_HMODULE_VALID
	}

	internal enum ConsoleVerb
	{
		None		= 0x0000,                       
		Open		= 0x8000,                       
		Copy		= 0x8001,                       
		Paste		= 0x8002,                       
		Delete		= 0x8003,                       
		Properties	= 0x8004,                       
		Rename		= 0x8005,                       
		Refresh		= 0x8006,                       
		Print		= 0x8007,                       
		Cut			= 0x8008,
	}

	public enum ButtonState
	{
		Enabled			= 0x1,
		Checked			= 0x2,
		Hidden			= 0x4,
		Indeterminate	= 0x8,
		ButtonPressed	= 0x10
	}

	[Flags]
	public enum ContextMenuFlags
		:	uint
	{
		InsertionAllowedTop = 1,
		InsertionAllowedNew = 2,
		InsertionAllowedTask = 4,
		InsertionAllowedView = 8,

		SpecialDefaultItem = 4,

		Enabled = 0x00000000,
		Grayed = 0x00000001,
		Disabled = 0x00000002,
		Popup = 0x00000010,
		Separator = 0x00000800,
		Checked = 0x00000008,
		Unchecked = 0x00000000
	}

	[Flags]
	internal enum InsertionPointId
		:	uint
	{
		PrimaryTop = 0xa0000000,
		PrimaryNew = 0xa0000001,
		PrimaryTask = 0xa0000002,
		PrimaryView = 0xa0000003,
		ThirdPartyNew = 0x90000001,
		ThirdPartyTask = 0x90000002,
		RootMenu = 0x80000000,
	}

	#endregion

	#region Structs

 	internal struct MmcFilterData
	{  
		public IntPtr Text;
		public int TextMax;
		public int Value;

		internal MmcFilterData(IntPtr text, int textMax, int value)
		{
			Text = text;
			TextMax = textMax;
			Value = value;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MenuButtonData
	{
		public int CommandId;
		public int X;
		public int Y;
	} 

	[StructLayout(LayoutKind.Sequential)]
	internal struct MmcButton
	{
		public int Bitmap;
		public int CommandId;
		public byte State;
		public byte Type;
		[MarshalAs(UnmanagedType.LPWStr)] public string ButtonText;
		[MarshalAs(UnmanagedType.LPWStr)] public string TooltipText;
		
		public MmcButton(int bitmap, int commandId, string buttonText, string tooltipText)
		{
			Bitmap = bitmap;
			CommandId = commandId;
			State = (byte) ToolbarButtonState.Enabled;
			Type = (byte) ToolbarStyle.Group;
			ButtonText = buttonText;
			TooltipText = tooltipText;
		}

		public MmcButton(int bitmap, int commandId, ToolbarButtonState state, ToolbarStyle type, string buttonText, string tooltipText)
		{
			Bitmap = bitmap;
			CommandId = commandId;
			State = (byte) state;
			Type = (byte) type;
			ButtonText = buttonText;
			TooltipText = tooltipText;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MmcContextMenuItem
	{
		[MarshalAs(UnmanagedType.LPWStr)] public string Name;
		[MarshalAs(UnmanagedType.LPWStr)] public string StatusBarText;
		public int CommandId;
		public uint InsertionPointId;
		public int Flags;
		public int SpecialFlags;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ScopeDataItem
	{
		public uint Mask;
		public IntPtr DisplayName;
        public int Image; // Should really be short, but we're doing some weird << 16 thing with this!
		public int OpenImage;
		public uint State;
		public int Children;
        public IntPtr Param;
        public IntPtr RelativeId;
		public IntPtr Id;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ResultDataItem
	{
		public uint Mask;
		public int bScopeItem;
		public IntPtr ItemId;
		public int Index;
		public int Col;
		public IntPtr Str;
		public int Image; // Should really be short, but we're doing some weird << 16 thing with this!
		public uint State;
		public IntPtr Param;
		public int Indent;
	}

 	[StructLayout(LayoutKind.Sequential)]
	internal struct CLSID
	{
		public uint x;
		public ushort s1;
		public ushort s2;
		public byte[] c;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct FormatEtc
	{
		public int Format;
		public int Ptd;
		public uint Aspect;
		public int Index;
		public uint Tymed;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct StgMedium
	{
		public uint Tymed;
		public IntPtr Global;
		public object pUnkForRelease;
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	internal struct ActCtx
	{  
		public uint Size;  
		public uint Flags;  
		public string Source;  
		public ushort ProcessorArchitecture;  
		public ushort LangId;
		public string AssemblyDirectory;  
		public string ResourceName;  
		public string ApplicationName;  
		public IntPtr Module;
	}

	#endregion

	#region Delegates

	internal delegate bool DialogProc(IntPtr hwndDlg, uint uMsg, IntPtr wParam, IntPtr lParam); 
	internal delegate uint PropSheetPageProc(IntPtr hwnd, uint uMsg, IntPtr lParam);

	#endregion
}
