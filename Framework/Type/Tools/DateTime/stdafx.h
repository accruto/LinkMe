// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//
#pragma once

#include <windows.h>

const UINT DTN_FORMATQUERY	= -742;
const UINT DTN_FORMAT		= -743;
const UINT DTN_WMKEYDOWN	= -744;
const UINT DTN_USERSTRING	= -745;
const UINT DTS_APPCANPARSE	= 0x0010;

typedef struct tagNMDATETIMEFORMATQUERY
{
    NMHDR nmhdr;
    const wchar_t* pszFormat;  // format substring
    SIZE szMax;        // max bounding rectangle app will use for this format string
} NMDATETIMEFORMATQUERY, *LPNMDATETIMEFORMATQUERY;

typedef struct tagNMDATETIMEFORMAT
{
    NMHDR nmhdr;
    const wchar_t* pszFormat;   // format substring
    SYSTEMTIME st;       // current systemtime
    wchar_t* pszDisplay;   // string to display
    wchar_t szDisplay[64];  // buffer pszDisplay originally points at
} NMDATETIMEFORMAT, *LPNMDATETIMEFORMAT;

typedef struct tagNMDATETIMEWMKEYDOWN
{
    NMHDR      nmhdr;
    int        nVirtKey;  // virtual key code of WM_KEYDOWN which MODIFIES an X field
    const wchar_t* pszFormat; // format substring
    SYSTEMTIME st;        // current systemtime, app should modify based on key
} NMDATETIMEWMKEYDOWN, *LPNMDATETIMEWMKEYDOWN;

typedef struct tagNMDATETIMESTRING
{
    NMHDR      nmhdr;
    const wchar_t*     pszUserString;  // string user entered
    SYSTEMTIME st;             // app fills this in
    DWORD      dwFlags;        // GDT_VALID or GDT_NONE
} NMDATETIMESTRING, *LPNMDATETIMESTRING;
