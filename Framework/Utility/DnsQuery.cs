using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LinkMe.Framework.Utility
{
    public static class DnsQuery
    {
        public static IList<string> GetMxNames(string domain)
        {
            IntPtr pFirstRecord = IntPtr.Zero;
            try
            {
                var names = new List<string>();
                int error = DnsQuery_W(domain, DNS_TYPE.MX, DNS_QUERY.BYPASS_CACHE, IntPtr.Zero, out pFirstRecord, IntPtr.Zero);
                if (error == DNS_ERROR_RCODE_NAME_ERROR || error == DNS_INFO_NO_RECORDS)
                    return names;
                if (error != 0)
                    throw new Win32Exception(error);

                DNS_MX_RECORD record;
                for (var pRecord = pFirstRecord; pRecord != IntPtr.Zero; pRecord = record.pNext)
                {
                    record = (DNS_MX_RECORD)Marshal.PtrToStructure(pRecord, typeof(DNS_MX_RECORD));
                    if (record.wType == DNS_TYPE.MX)
                    {
                        var name = Marshal.PtrToStringUni(record.Data.pNameExchange);
                        names.Add(name);
                    }
                }

                return names;
            }
            finally
            {
                DnsRecordListFree(pFirstRecord, 0);
            }
        }

        #region Win32 API

        [DllImport("dnsapi.dll", EntryPoint = "DnsQuery_W", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        private static extern int DnsQuery_W(string pszName, DNS_TYPE wType, DNS_QUERY options, IntPtr pExtra, out IntPtr ppQueryResults, IntPtr pReserved);

        [DllImport("dnsapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void DnsRecordListFree(IntPtr pRecordList, int FreeType);

        #endregion

        #region Win32 Structures

        private const int DNS_ERROR_RCODE_NAME_ERROR = 9003;
        private const int DNS_INFO_NO_RECORDS = 9501;

        private enum DNS_TYPE : ushort
        {
            //  RFC 1034/1035
            A          = 0x0001,      //  1
            NS         = 0x0002,      //  2
            MD         = 0x0003,      //  3
            MF         = 0x0004,      //  4
            CNAME      = 0x0005,      //  5
            SOA        = 0x0006,      //  6
            MB         = 0x0007,      //  7
            MG         = 0x0008,      //  8
            MR         = 0x0009,      //  9
            NULL       = 0x000a,      //  10
            WKS        = 0x000b,      //  11
            PTR        = 0x000c,      //  12
            HINFO      = 0x000d,      //  13
            MINFO      = 0x000e,      //  14
            MX         = 0x000f,      //  15
            TEXT       = 0x0010,      //  16
        }

        [Flags]
        private enum DNS_QUERY : uint
        {
            STANDARD                  = 0x00000000,
            ACCEPT_TRUNCATED_RESPONSE = 0x00000001,
            USE_TCP_ONLY              = 0x00000002,
            NO_RECURSION              = 0x00000004,
            BYPASS_CACHE              = 0x00000008,

            NO_WIRE_QUERY             = 0x00000010,
            NO_LOCAL_NAME             = 0x00000020,
            NO_HOSTS_FILE             = 0x00000040,
            NO_NETBT                  = 0x00000080,

            WIRE_ONLY                 = 0x00000100,
            RETURN_MESSAGE            = 0x00000200,

            MULTICAST_ONLY            = 0x00000400,
            NO_MULTICAST              = 0x00000800,

            TREAT_AS_FQDN             = 0x00001000,
            DONT_RESET_TTL_VALUES     = 0x00100000,
            RESERVED                  = 0xff000000,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DNS_MX_RECORD
        {
            public IntPtr pNext;
            public string pName;
            public DNS_TYPE wType;
            public ushort wDataLength;
            public uint flags;
            public uint dwTtl;
            public uint dwReserved;
            public DNS_MX_DATA Data;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DNS_MX_DATA
        {
            public IntPtr pNameExchange;
            public ushort wPreference;
            public ushort Pad;
        }

        #endregion
    }
}
