// Connect.cpp : Implementation of CAddIn
#include "stdafx.h"
#include "Outlook.h"
#include "AddIn.h"
#include "SearchEvent.h"
#include "EditControl.h"


extern COutlookModule _AtlModule;
EditManager _EditManager;


CAddIn::CAddIn()
{
}

HRESULT CAddIn::FinalConstruct()
{
	return S_OK;
}

void CAddIn::FinalRelease() 
{
}

STDMETHODIMP CAddIn::OnConnection(IDispatch* pApplication, AddInDesignerObjects::ext_ConnectMode connectMode, IDispatch* pAddInInstance, SAFEARRAY ** custom)
{
	pApplication->QueryInterface(__uuidof(IDispatch), (LPVOID*)&m_pApplication);

	if (connectMode != AddInDesignerObjects::ext_cm_Startup)
		return OnStartupComplete(custom);
	else
		return S_OK;
}

STDMETHODIMP CAddIn::OnDisconnection(AddInDesignerObjects::ext_DisconnectMode disconnectMode, SAFEARRAY ** custom)
{
	HRESULT hr = S_OK;
	if (disconnectMode != AddInDesignerObjects::ext_dm_HostShutdown)
		hr = OnBeginShutdown(custom);

	m_pApplication = NULL;
	return hr;
}

STDMETHODIMP CAddIn::OnAddInsUpdate(SAFEARRAY** /*custom*/)
{
	return S_OK;
}

STDMETHODIMP CAddIn::OnStartupComplete(SAFEARRAY** /*custom*/)
{
	try
	{
		// Create the candidate search toolbar.

		_CommandBarsPtr pCommandBars = m_pApplication->ActiveExplorer()->CommandBars;
		CreateCandidateSearchCommandBar(pCommandBars);
	}
	catch (const _com_error& e)
	{
		return ReportError(e);
	}
	catch (...)
	{
		return ReportError(L"Unexpected exception.", E_FAIL);
	}

	return S_OK;
}

STDMETHODIMP CAddIn::OnBeginShutdown(SAFEARRAY** /*custom*/)
{
	try
	{
		ShutdownCandidateSearchCommandBar();
	}
	catch (const _com_error& e)
	{
		return ReportError(e);
	}
	catch (...)
	{
		return ReportError(L"Unexpected exception.", E_FAIL);
	}

	return S_OK;
}

void CAddIn::CreateCandidateSearchCommandBar(const _CommandBarsPtr& pCommandBars)
{
	// Get the command bar.

	CommandBarPtr pCommandBar = FindOrCreateCommandBar(pCommandBars, Constants::CandidateSearchCommandBarName);

	// Create the controls on that command bar.

	CreateCandidateSearchControls(pCommandBar);
}

void CAddIn::CreateCandidateSearchControls(const CommandBarPtr& pCommandBar)
{
    // Create the job title control.

    m_pJobTitle = CreateControl(pCommandBar, msoControlEdit, Constants::JobTitleCaption, Constants::JobTitleWidth, Constants::JobTitleOnAction);

    // Add it to the manager.

	EditControl* pJobTitleEditControl = new EditControl(m_pJobTitle, Constants::JobTitlePrompt);
    _EditManager.Add(pJobTitleEditControl);

    // Create the search button.

    m_pSearch = CreateControl(pCommandBar, msoControlButton, Constants::SearchCaption, 0, Constants::SearchOnAction);
    m_pSearch->Style = msoButtonIconAndCaption;
	m_searchEventCookie = CreateCommandBarButtonEvent<CSearchEvent>(m_pSearch, pJobTitleEditControl);
	m_pSearch->PutPicture(GetPicture(IDB_SEARCH));
}

void CAddIn::ShutdownCandidateSearchCommandBar()
{
	_EditManager.Shutdown();
	ShutdownCommandBarButtonEvent(m_pSearch, m_searchEventCookie);
	m_pSearch = NULL;
	m_pJobTitle = NULL;
}

CommandBarPtr CAddIn::FindOrCreateCommandBar(const _CommandBarsPtr& pCommandBars, const wchar_t* commandBarName)
{
	CommandBarPtr pCommandBar;

	try
    {
		// Check whether it is already there or not.

		pCommandBar = pCommandBars->Item[Constants::CandidateSearchCommandBarName];

		// Remove all previous controls.

		int count = pCommandBar->Controls->Count;
		while (count > 0)
		{
			CommandBarControlPtr pCommandBarControl = pCommandBar->Controls->Item[1];
			pCommandBarControl->Delete();
			count = pCommandBar->Controls->Count;
		}
    }
    catch (_com_error e)
    {
        // Create a top level command bar.

		pCommandBar = pCommandBars->Add(Constants::CandidateSearchCommandBarName, msoBarTop, false, false);
		pCommandBar->Visible = VARIANT_TRUE;
    }

	return pCommandBar;
}

CommandBarControlPtr CAddIn::CreateControl(const CommandBarPtr& pCommandBar, MsoControlType type, const wchar_t* caption, int width, const wchar_t* onAction)
{
    // Add the control.

    CommandBarControlPtr pControl = pCommandBar->Controls->Add(type);

    // Set other attributes.

    pControl->Caption = caption;
    pControl->Tag = caption;
    pControl->Visible = true;

    if (type == msoControlEdit)
        pControl->Width = width;
    if (onAction != NULL && *onAction != '\0')
        pControl->OnAction = onAction;

    return pControl;
}

void CAddIn::ShutdownCommandBarButtonEvent(const _CommandBarButtonPtr& pButton, DWORD cookie)
{
	AtlUnadvise(pButton, __uuidof(_CommandBarButtonEvents), cookie);
}

IPictureDispPtr CAddIn::GetPicture(int resource)
{
	// Load the bitmap.

	PICTDESC* pd = new PICTDESC();
	pd->cbSizeofstruct = sizeof(PICTDESC);
	pd->picType = PICTYPE_BITMAP;
	pd->bmp.hbitmap = ::LoadBitmap(_AtlModule.GetResourceInstance(), MAKEINTRESOURCE(resource));
	pd->bmp.hpal = 0;

	// Create the picture.

	IPictureDispPtr pPictureDisp;
	::OleCreatePictureIndirect(pd, IID_IPictureDisp, FALSE, (void**)(&pPictureDisp));
	delete pd;
	return pPictureDisp;
}

