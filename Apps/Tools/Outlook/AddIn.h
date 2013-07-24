#pragma once
#include "resource.h"
#include "EditManager.h"
#include "ErrorReporter.h"


class ATL_NO_VTABLE CAddIn : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CAddIn, &CLSID_AddIn>,
	public IDispatchImpl<AddInDesignerObjects::_IDTExtensibility2, &AddInDesignerObjects::IID__IDTExtensibility2, &AddInDesignerObjects::LIBID_AddInDesignerObjects, 1, 0>,
	private ErrorReporter<&CLSID_AddIn, &AddInDesignerObjects::IID__IDTExtensibility2>
{
public:
	CAddIn();

DECLARE_REGISTRY_RESOURCEID(IDR_ADDIN)
DECLARE_NOT_AGGREGATABLE(CAddIn)

BEGIN_COM_MAP(CAddIn)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(AddInDesignerObjects::IDTExtensibility2)
END_COM_MAP()

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct();
	void FinalRelease();

public:
	// IDTExtensibility2

	STDMETHOD(OnConnection)(IDispatch* pApplication, AddInDesignerObjects::ext_ConnectMode connectMode, IDispatch* pAddInInstance, SAFEARRAY** custom);
	STDMETHOD(OnDisconnection)(AddInDesignerObjects::ext_DisconnectMode disconnectMode, SAFEARRAY** custom);
	STDMETHOD(OnAddInsUpdate)(SAFEARRAY** custom);
	STDMETHOD(OnStartupComplete)(SAFEARRAY** custom);
	STDMETHOD(OnBeginShutdown)(SAFEARRAY** custom);

private:
	_ApplicationPtr m_pApplication;
	_CommandBarComboBoxPtr m_pJobTitle;
	_CommandBarButtonPtr m_pSearch;
	DWORD m_searchEventCookie;

	void CreateCandidateSearchCommandBar(const _CommandBarsPtr& pCommandBars);
	void CreateCandidateSearchControls(const CommandBarPtr& pCommandBar);
	void ShutdownCandidateSearchCommandBar();

	CommandBarPtr FindOrCreateCommandBar(const _CommandBarsPtr& pCommandBars, const wchar_t* commandBarName);
	CommandBarControlPtr CreateControl(const CommandBarPtr& pCommandBar, MsoControlType type, const wchar_t* caption, int width, const wchar_t* onAction);

	template < class T >
	DWORD CreateCommandBarButtonEvent(const _CommandBarButtonPtr& pButton, EditControl* pEditControl);
	void ShutdownCommandBarButtonEvent(const _CommandBarButtonPtr& pButton, DWORD cookie);

	IPictureDispPtr GetPicture(int resource);
};


OBJECT_ENTRY_AUTO(__uuidof(AddIn), CAddIn)


template < class Event >
DWORD CAddIn::CreateCommandBarButtonEvent(const _CommandBarButtonPtr& pControl, EditControl* pEditControl)
{
	// Create an instance of the event.

	CComObject<Event>* pEvent;
	HRESULT hr = CComObject<Event>::CreateInstance(&pEvent);
	if (FAILED(hr))
		return 0;

	// Initialise them.

	pEvent->Initialise(pEditControl);

	// Advise.

	DWORD cookie = 0;
	hr = AtlAdvise(pControl, pEvent, __uuidof(_CommandBarButtonEvents), &cookie);
	if (FAILED(hr))
	{
		delete pEvent;
		return 0;
	}
	else
	{
		return cookie;
	}
}