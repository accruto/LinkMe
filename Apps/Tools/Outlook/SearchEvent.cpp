#include "StdAfx.h"
#include "Outlook.h"
#include "SearchEvent.h"


CSearchEvent::CSearchEvent()
{
}

void CSearchEvent::Initialise(EditControl* pJobTitle)
{
	m_pJobTitle = pJobTitle;
}

HRESULT CSearchEvent::FinalConstruct()
{
	return S_OK;
}

void CSearchEvent::FinalRelease()
{
}

STDMETHODIMP CSearchEvent::Click(_CommandBarButton* /*pButton*/, VARIANT_BOOL* /*cancelDefault*/)
{
	try
	{
		// Construct the url.

		_bstr_t url = Constants::AdvancedSearchUrl;
		_bstr_t jobTitle = m_pJobTitle->GetText();
		if (jobTitle.length() != 0)
		{
			url += L"?";
			url += Constants::JobTitleParameter;
			url += L"=";

			// Need to escape the job title in the url.

			DWORD size = 256;
			wchar_t* buffer = new wchar_t[size];
			HRESULT hr = ::UrlEscape(jobTitle, buffer, &size, URL_ESCAPE_SEGMENT_ONLY);
			if (hr == E_POINTER)
			{
				// Buffer too small.

				delete[] buffer;
				buffer = new wchar_t[size];
				hr = ::UrlEscape(jobTitle, buffer, &size, URL_ESCAPE_SEGMENT_ONLY);
			}

			if (SUCCEEDED(hr))
				url += buffer;
			else
				url += jobTitle;

			delete[] buffer;
		}

		// Navigate.

		::ShellExecute(NULL, L"open", url, NULL, NULL, SW_SHOWNORMAL);
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

