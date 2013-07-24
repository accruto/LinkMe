using System.Runtime.InteropServices;

using ELEMDESC = System.Runtime.InteropServices.ComTypes.ELEMDESC;
using SYSKIND = System.Runtime.InteropServices.ComTypes.SYSKIND;

namespace LinkMe.Framework.Utility.Win32
{
	// Win32 API structures.

	public struct CHARFORMAT2W
	{
		public static CHARFORMAT2W Create()
		{
			CHARFORMAT2W s = new CHARFORMAT2W();
			s.cbSize = Marshal.SizeOf(typeof(CHARFORMAT2W));
			return s;
		}

		public int cbSize;
		public int dwMask;
		public int dwEffects;
		public int yHeight;
		public int yOffset;
		public int crTextColor;
		public byte bCharSet;
		public byte bPitchAndFamily;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public byte[] szFaceName;
		public short wWeight;
		public short sSpacing;
		public int crBackColor;
		public uint lcid;
		public int dwReserved;
		public short wKerning;
		public byte bUnderlineType;
		public byte bAnimation;
		public byte bRevAuthor;
		public byte bReserved1;
	}

	/// <summary>
	/// The .NET framework defines this struct (System.Runtime.InteropServices.VARDESC), but that definition
	/// is incorrect - it's missing the union member (descUnion).
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	public struct VARDESC
	{
		[StructLayout(LayoutKind.Explicit)]
			public struct DESCUNION
		{
			[FieldOffset(0)]
			public System.IntPtr lpvarValue;
			[FieldOffset(0)]
			public int oInst;
		}

		public int memid;
		public string lpstrSchema;
		public DESCUNION descUnion;
		public ELEMDESC elemdescVar;
		public short wVarFlags;
		public VARKIND varkind;
	}

	/// <summary>
	/// The .NET framework defines this struct (System.Runtime.InteropServices.TYPELIBATTR), but that definition
	/// has short members instead of unsigned short, so it fails to work with methods such as QueryPathOfRegTypeLib.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
	public struct TLIBATTR
	{
		public System.Guid guid;	// globally unique library ID.
		public uint lcid;			// Locale of the TypeLibrary.
		public SYSKIND syskind;		// Target hardware platform.
		public ushort wMajorVerNum;	// Major version number.
		public ushort wMinorVerNum;	// Minor version number.
		public ushort wLibFlags;	// Library flags.

		public TLIBATTR(System.Guid guid, uint lcid, SYSKIND syskind, ushort wMajorVerNum, ushort wMinorVerNum,
			ushort wLibFlags)
		{
			this.guid = guid;
			this.lcid = lcid;
			this.syskind = syskind;
			this.wMajorVerNum = wMajorVerNum;
			this.wMinorVerNum = wMinorVerNum;
			this.wLibFlags = wLibFlags;
		}
	};

	/* VARIANT STRUCTURE
	 *
	 *  VARTYPE vt;
	 *  WORD wReserved1;
	 *  WORD wReserved2;
	 *  WORD wReserved3;
	 *  union {
	 *    LONGLONG       VT_I8
	 *    LONG           VT_I4
	 *    BYTE           VT_UI1
	 *    SHORT          VT_I2
	 *    FLOAT          VT_R4
	 *    DOUBLE         VT_R8
	 *    VARIANT_BOOL   VT_BOOL
	 *    SCODE          VT_ERROR
	 *    CY             VT_CY
	 *    DATE           VT_DATE
	 *    BSTR           VT_BSTR
	 *    IUnknown *     VT_UNKNOWN
	 *    IDispatch *    VT_DISPATCH
	 *    SAFEARRAY *    VT_ARRAY
	 *    BYTE *         VT_BYREF|VT_UI1
	 *    SHORT *        VT_BYREF|VT_I2
	 *    LONG *         VT_BYREF|VT_I4
	 *    LONGLONG *     VT_BYREF|VT_I8
	 *    FLOAT *        VT_BYREF|VT_R4
	 *    DOUBLE *       VT_BYREF|VT_R8
	 *    VARIANT_BOOL * VT_BYREF|VT_BOOL
	 *    SCODE *        VT_BYREF|VT_ERROR
	 *    CY *           VT_BYREF|VT_CY
	 *    DATE *         VT_BYREF|VT_DATE
	 *    BSTR *         VT_BYREF|VT_BSTR
	 *    IUnknown **    VT_BYREF|VT_UNKNOWN
	 *    IDispatch **   VT_BYREF|VT_DISPATCH
	 *    SAFEARRAY **   VT_BYREF|VT_ARRAY
	 *    VARIANT *      VT_BYREF|VT_VARIANT
	 *    PVOID          VT_BYREF (Generic ByRef)
	 *    CHAR           VT_I1
	 *    USHORT         VT_UI2
	 *    ULONG          VT_UI4
	 *    ULONGLONG      VT_UI8
	 *    INT            VT_INT
	 *    UINT           VT_UINT
	 *    DECIMAL *      VT_BYREF|VT_DECIMAL
	 *    CHAR *         VT_BYREF|VT_I1
	 *    USHORT *       VT_BYREF|VT_UI2
	 *    ULONG *        VT_BYREF|VT_UI4
	 *    ULONGLONG *    VT_BYREF|VT_UI8
	 *    INT *          VT_BYREF|VT_INT
	 *    UINT *         VT_BYREF|VT_UINT
	 *  }
	 */

	[StructLayout(LayoutKind.Explicit, Size=16)]
	public struct VARIANT
	{
		[FieldOffset(0)]
		public ushort vt;
		[FieldOffset(2)]
		public ushort wReserved1;
		[FieldOffset(4)]
		public ushort wReserved2;
		[FieldOffset(6)]
		public ushort wReserved3;
		[FieldOffset(8)]
		public long llVal;
		[FieldOffset(8)]
		public int lVal;
		[FieldOffset(8)]
		public byte bVal;
		[FieldOffset(8)]
		public short iVal;
		[FieldOffset(8)]
		public float fltVal;
		[FieldOffset(8)]
		public double dblVal;
		[FieldOffset(8), MarshalAs(UnmanagedType.VariantBool)]
		public bool boolVal;
		[FieldOffset(8)]
		public int scode;
//		[FieldOffset(8), MarshalAs(UnmanagedType.Currency)]
//		public Decimal cyVal;
		[FieldOffset(8)]
		public double date;
//		[FieldOffset(8), MarshalAs(UnmanagedType.BStr)]
//		public string bstrVal;
//		[FieldOffset(8), MarshalAs(UnmanagedType.IUnknown)]
//		public IntPtr punkVal;
//		[FieldOffset(8), MarshalAs(UnmanagedType.IDispatch)]
//		public IntPtr pdispVal;
//		[FieldOffset(8), MarshalAs(UnmanagedType.SafeArray)]
//		public IntPtr parray;
//		[FieldOffset(8)]
//		public unsafe byte* pbVal;
//		[FieldOffset(8)]
//		public unsafe short* piVal;
//		[FieldOffset(8)]
//		public unsafe int* plVal;
//		[FieldOffset(8)]
//		public unsafe long* pllVal;
//		[FieldOffset(8)]
//		public unsafe float* pfltVal;
//		[FieldOffset(8)]
//		public unsafe double* pdblVal;
//		[FieldOffset(8)]
//		public unsafe short* pboolVal;
//		[FieldOffset(8)]
//		public unsafe int* pscode;
//		[FieldOffset(8)]
//		public unsafe CY* pcyVal;
//		[FieldOffset(8)]
//		public unsafe double* pdate;
//		[FieldOffset(8)]
//		public unsafe ushort** pbstrVal;
//		[FieldOffset(8)]
//		public unsafe IUnknown** ppunkVal;
//		[FieldOffset(8)]
//		public unsafe IDispatch** ppdispVal;
//		[FieldOffset(8)]
//		public unsafe SAFEARRAY** pparray;
//		[FieldOffset(8), MarshalAs(UnmanagedType.LPStruct)]
//		public IntPtr pvarVal;
//		[FieldOffset(8)]
//		public unsafe void* byref;
		[FieldOffset(8)]
		public sbyte cVal;
		[FieldOffset(8)]
		public ushort uiVal;
		[FieldOffset(8)]
		public uint ulVal;
		[FieldOffset(8)]
		public ulong ullVal;
		[FieldOffset(8)]
		public int intVal;
		[FieldOffset(8)]
		public uint uintVal;
		//[FieldOffset(8)]
		//public unsafe DECIMAL* pdecVal;
//		[FieldOffset(8)]
//		public unsafe sbyte* pcVal;
//		[FieldOffset(8)]
//		public unsafe ushort* puiVal;
//		[FieldOffset(8)]
//		public unsafe uint* pulVal;
//		[FieldOffset(8)]
//		public unsafe ulong* pullVal;
//		[FieldOffset(8)]
//		public unsafe int* pintVal;
//		[FieldOffset(8)]
//		public unsafe uint* puintVal;
//		[FieldOffset(8)]
//		public unsafe void* pvRecord;
//		[FieldOffset(12)]
//		public unsafe IRecordInfo* pRecInfo;
//		[FieldOffset(0)]
//		public Decimal decVal;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct NMHDR
	{
		public System.IntPtr hwndFrom;
		public int idFrom;
		public int code;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct NMTREEVIEW
	{
		public NMHDR nmhdr;
		public int action;
		public TV_ITEM itemOld;
		public TV_ITEM itemNew;
		public int ptDrag_X;
		public int ptDrag_Y;
	}
 
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=1)]
	public struct TV_ITEM
	{
		public int mask;
		public System.IntPtr hItem;
		public int state;
		public int stateMask;
		public System.IntPtr pszText;
		public int cchTextMax;
		public int iImage;
		public int iSelectedImage;
		public int cChildren;
		public System.IntPtr lParam;
	}


	[StructLayout(LayoutKind.Sequential)]
	public struct TCHITTESTINFO
	{
		public System.Drawing.Point pt;
		public int flags;

		public TCHITTESTINFO(System.Drawing.Point point)
		{
			pt = point;
			flags = 0;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Auto)]
	public struct BITMAP
	{
		public int				Type;
		public int				Width;
		public int				Height;
		public int				WidthBytes;
		public ushort			Planes;
		public ushort			BitsPixel;
		public System.IntPtr	Bits;
	}

	[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Auto)]
	public struct BITMAPINFOHEADER
	{
		public int		Size;
		public int		Width;
		public int		Height;
		public ushort	Planes;
		public ushort	BitCount;
		public uint		Compression;
		public uint		SizeImage;
		public int		XPelsPerMeter;
		public int		YPelsPerMeter;
		public uint		ClrUsed;
		public uint		ClrImportant;
	}

	[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Auto)]
	public struct BITMAPINFO
	{
		public BITMAPINFOHEADER	Header;
		public long				Colors0;
		public long				Colors1;
		public long				Colors2;
		public long				Colors3;
	}

	[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Auto)]
	public struct DIBSECTION
	{
		public BITMAP			Bm;
		public BITMAPINFOHEADER	Bmih;
		public uint				Bitfields0;
		public uint				Bitfields1;
		public uint				Bitfields2;
		public System.IntPtr	Section;
		public uint				Offset;
	}

	[StructLayout(LayoutKind.Sequential, Pack=4)]
	public struct WBEM_COMPILE_STATUS_INFO
	{
		public int lPhaseError;	// 0 = no error, 1 = argument error,  2 = parsing, 3 error occurred while storing the data
		[MarshalAs(UnmanagedType.Error)]
		public int hRes;		// Actual error code
		public int ObjectNum;	// object which is at fault
		public int FirstLine;	// first line number of the object
		public int LastLine;	// last line number of the object
		public uint dwOutFlags;	// reserved
	};

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=1)]
	public struct LVITEM
	{
		public int mask;
		public int iItem;
		public int iSubItem;
		public int state;
		public int stateMask;
		public string pszText;
		public int cchTextMax;
		public int iImage;
		public System.IntPtr lParam;
		public int iIndent;
	}
}
