using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using Win32 = LinkMe.Framework.Utility.Win32;
using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	/// Implements IDataObject interface.  Responsible for mapping IDataObject 
	/// pointers that MMC uses to our managed Node objects.
	/// </summary>
	internal class DataObject
		:	IDataObject
	{
		#region Constructors

		public DataObject(SnapinNode node)
		{
			m_node = node;
		}

		static DataObject()
		{
			// Get the Clipboard Format Numbers for these various items.
			// MMC should have already registered these Clipboard Formats,
			// so this call just gives us the id assigned for each format.

			s_nodeType = SafeNativeMethods.RegisterClipboardFormat("CCF_NODETYPE");
			s_SZNodeType = SafeNativeMethods.RegisterClipboardFormat("CCF_SZNODETYPE");
			s_snapinClsid = SafeNativeMethods.RegisterClipboardFormat("CCF_SNAPIN_CLASSID");
			s_displayName = SafeNativeMethods.RegisterClipboardFormat("CCF_DISPLAY_NAME");
			s_snapins   = SafeNativeMethods.RegisterClipboardFormat("CCF_MULTI_SELECT_SNAPINS");
			s_cfMultiSelect = SafeNativeMethods.RegisterClipboardFormat("CCF_OBJECT_TYPES_IN_MULTI_SELECT");
			s_isMsObj   = SafeNativeMethods.RegisterClipboardFormat("CCF_MMC_MULTISELECT_DATAOBJECT");
			s_cookies   = SafeNativeMethods.RegisterClipboardFormat("CCF_MULTISELECT_COOKIES");
		}
    
		#endregion

		/// <summary>
		/// The node associated with the IDataObject
		/// </summary>
		public SnapinNode Node
		{
			get { return m_node; }
		}

		#region IDataObject Implementation

		/// <summary>
		/// Send data to MMC in a stream depending on what is requested 
		/// </summary>
		public void GetDataHere(ref FormatEtc pFormatEtc, ref StgMedium pMedium)
		{
			const char unknownString = '\0'; // Send this value if we don't know what string to send.

			try
			{
				ushort cf = (ushort)pFormatEtc.Format;

				BinaryWriter writer = CreateBinaryWriter();

				if (cf == s_displayName)
				{
					writer.Write(m_node.DisplayName.ToCharArray());
				}
				else if (cf == s_SZNodeType)
				{
					writer.Write(unknownString);
				}
				else if (cf == s_nodeType)
				{
					writer.Write(m_node.Guid.ToByteArray());
				}
				else if (cf == s_snapinClsid)
				{
					writer.Write(m_node.Snapin.Guid.ToByteArray());
				}

				// Write the memory stream to the HGlobal memory.

				WriteMemoryStreamToHGlobal(writer, pMedium.Global);
			}
			catch ( System.Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred in the snap-in:").ShowDialog(m_node.Snapin);
			}
		}

		public int GetData(ref FormatEtc pFormatEtc, ref StgMedium pMedium)
		{
			ushort cf = (ushort)pFormatEtc.Format;

			if (cf == s_cfMultiSelect)
			{
				BinaryWriter writer = CreateBinaryWriter();

				// MMC expects a SMMCObjectTypes struct. Don't bother creating the struct and serializing it,
				// just write the fields:
				// 1) count of GUIDs.
				// 2) array of node GUIDs.

				writer.Write(1);
				writer.Write(m_node.Guid.ToByteArray());

				// Allocate HGlobal memory.
 
				pMedium.Global = Marshal.AllocHGlobal(new IntPtr(writer.BaseStream.Length));
				Debug.Assert(pMedium.Global != IntPtr.Zero, "pMedium.Global != IntPtr.Zero");

				// Write the memory stream to the HGlobal memory.

				try
				{
					WriteMemoryStreamToHGlobal(writer, pMedium.Global);
				}
				catch
				{
					// An error occurred, so free the memory we allocated.

					Marshal.FreeHGlobal(pMedium.Global);
					throw;
				}

				return Constants.Win32.HResults.S_OK;
			}
			else
				return Constants.Win32.HResults.E_NOTIMPL;
		}

		public int QueryGetData(IntPtr a)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}

		public int GetCanonicalFormatEtc(IntPtr a, IntPtr b)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}

		public int SetData(IntPtr a, IntPtr b, int c)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}
    
		public int EnumFormatEtc(uint a, IntPtr b)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}
    
		public int DAdvise(IntPtr a, uint b, IntPtr c, ref uint d)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}

		public int DUnadvise(uint a)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}

		public int EnumDAdvise(IntPtr a)
		{
			return Constants.Win32.HResults.E_NOTIMPL;
		}

		#endregion

		private static BinaryWriter CreateBinaryWriter()
		{
			// Make a memory stream to handle streaming data to a buffer.

			MemoryStream stream = new MemoryStream(128);
			return new BinaryWriter(stream, new UnicodeEncoding());
		}

		private static void WriteMemoryStreamToHGlobal(BinaryWriter writer, IntPtr hGlobal)
		{
			MemoryStream stream = (MemoryStream)writer.BaseStream;

			IStream pStream;
			if (UnsafeNativeMethods.CreateStreamOnHGlobal(hGlobal, false, out pStream) != 0)
				throw new System.ComponentModel.Win32Exception();
			if (pStream == null)
				throw new ApplicationException("Failed to CreateStreamOnHGlobal()");

			writer.Flush();

			// Write to the HGlobal stream.

			IntPtr dataSentPtr = Marshal.AllocCoTaskMem(IntPtr.Size);
			pStream.Write(stream.GetBuffer(), (int)stream.Length, dataSentPtr);

			// Check how much data was written.

			int dataSent = Marshal.ReadInt32(dataSentPtr);
			Marshal.FreeCoTaskMem(dataSentPtr);

			if (dataSent != stream.Length)
			{
				throw new ApplicationException(string.Format("Tried to write {0} bytes to an HGlobal memory"
					+ " stream, but only {1} bytes were actually written.", stream.Length, dataSent));
			}
		}

		private static ushort s_displayName; 
		private static ushort s_nodeType;    
		private static ushort s_SZNodeType;  
		private static ushort s_snapinClsid;
		private static ushort s_snapins;
		private static ushort s_cfMultiSelect;
		private static ushort s_isMsObj;
		private static ushort s_cookies;

		private SnapinNode m_node;

		// flags
		public static readonly int NULL = 0;
		public static readonly int CUSTOMOCX = -1;
		public static readonly int CUSTOMWEB = -2;
	}
}
