// AddIn.cpp : Implementation of DLL Exports.

#include "stdafx.h"
#include "resource.h"
#include "Outlook.h"

#include "Outlook_i.c"


COutlookModule _AtlModule;


const wchar_t* Constants::CandidateSearchCommandBarName = L"LinkMe Candidate Search";
const wchar_t* Constants::JobTitleCaption = L"Search for candidates with this job title";
const wchar_t* Constants::JobTitleOnAction = L"=btnSearch_Click()";
const wchar_t* Constants::JobTitlePrompt = L"Type a job title to search by";
const wchar_t* Constants::SearchCaption = L"Search LinkMe";
const wchar_t* Constants::SearchOnAction = L"!<LinkMe.Tools.Outlook.Addin>";
const wchar_t* Constants::AdvancedSearchUrl = L"http://www.linkme.com.au/search/resumes/advancedsearch.aspx";
const wchar_t* Constants::JobTitleParameter = L"JobTitle";


// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	_AtlModule.SetResourceInstance(hInstance);
	return _AtlModule.DllMain(dwReason, lpReserved); 
}


// Used to determine whether the DLL can be unloaded by OLE
STDAPI DllCanUnloadNow(void)
{
	return _AtlModule.DllCanUnloadNow();
}


// Returns a class factory to create an object of the requested type
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}


// DllRegisterServer - Adds entries to the system registry
STDAPI DllRegisterServer(void)
{
	// registers object, typelib and all interfaces in typelib
	HRESULT hr = _AtlModule.DllRegisterServer();
	return hr;
}


// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
	return hr;
}
