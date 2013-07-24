// SearchEvent.h : Declaration of the CSearchEvent

#pragma once
#include "resource.h"       // main symbols
#include "EditControl.h"
#include "ErrorReporter.h"


// CSearchEvent

class ATL_NO_VTABLE CSearchEvent :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CSearchEvent, &CLSID_SearchEvent>,
	public IDispatchImpl<_CommandBarButtonEvents, &__uuidof(_CommandBarButtonEvents), &__uuidof(__Office), 2, 3>,
	private ErrorReporter<&CLSID_SearchEvent, &__uuidof(_CommandBarButtonEvents)>
{
public:
	CSearchEvent();

	void Initialise(EditControl* pJobTitle);

BEGIN_COM_MAP(CSearchEvent)
	COM_INTERFACE_ENTRY(IDispatch)
	COM_INTERFACE_ENTRY(_CommandBarButtonEvents)
END_COM_MAP()

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct();
	void FinalRelease();

public:
	STDMETHOD(Click(_CommandBarButton* pButton, VARIANT_BOOL* cancelDefault));

private:
	EditControl* m_pJobTitle;
};

