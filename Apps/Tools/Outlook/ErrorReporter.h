#pragma once


template <const CLSID* pclsid, const IID* piid>
class ErrorReporter
{
protected:
	HRESULT ReportError(const _com_error& e)
	{
		IErrorInfoPtr pInfo(e.ErrorInfo(), false);
		::SetErrorInfo(0, pInfo);
		return e.Error();
	}

	HRESULT ReportError(LPCOLESTR lpszDesc, HRESULT hRes)
	{
		return AtlReportError(GetCLSID(), lpszDesc, GetIID(), hRes);
	}

private:
	static const CLSID& GetCLSID() { return *pclsid; }
	static const IID& GetIID() { return *piid; }
};