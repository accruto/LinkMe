#include "StdAfx.h"
#include "EditManager.h"


HWND EditManager::m_hWindow = NULL;
HHOOK EditManager::m_hHook = NULL;
EditManager::EditControlList EditManager::m_editControls;


EditManager::EditManager()
{
	// Create the edit window and install the hook.

	CreateEditWindow();
	InstallHook();
}

void EditManager::Shutdown()
{
	// Delete all control states.

	for (EditControlList::iterator it = m_editControls.begin(); it != m_editControls.end(); ++it)
		delete (*it);
	m_editControls.clear();

	// Uninstall the hook and destroy the edit window.

	UninstallHook();
	DestroyEditWindow();
}

void EditManager::Add(EditControl* pEditControl)
{
	m_editControls.push_back(pEditControl);
}

EditControl* EditManager::GetEditControl(HWND hWnd)
{
	// Iterate until found.

	for (EditControlList::iterator it = m_editControls.begin(); it != m_editControls.end(); ++it)
	{
		if ((*it)->GetWindowHandle() == hWnd)
			return *it;
	}

	return NULL;
}

void EditManager::CreateEditWindow()
{
	// Register a new class.

	const wchar_t className[] = L"LinkMeEditWindow";

	WNDCLASSEX wndclass;
	wndclass.cbSize			= sizeof(wndclass);
	wndclass.style			= 0;
	wndclass.lpfnWndProc	= &EditWndProc;
	wndclass.cbClsExtra		= 0;
	wndclass.cbWndExtra		= 0;
	wndclass.hInstance		= _AtlModule.GetResourceInstance();
	wndclass.hIcon			= NULL;
	wndclass.hCursor		= NULL;
	wndclass.hbrBackground	= (HBRUSH) GetStockObject(WHITE_BRUSH);
	wndclass.lpszMenuName	= NULL;
	wndclass.lpszClassName	= className;
	wndclass.hIconSm		= NULL;
	::RegisterClassEx(&wndclass);

	// Create a window from that class.

	m_hWindow = ::CreateWindow(className, L"LinkMeAddin", WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 400, 200, NULL, NULL, _AtlModule.GetResourceInstance(), NULL);
}

void EditManager::DestroyEditWindow()
{
	if (m_hWindow != NULL)
	{
		::DestroyWindow(m_hWindow);
		m_hWindow = NULL;
	}
}

void EditManager::InstallHook()
{
	m_hHook = ::SetWindowsHookEx(WH_CBT, (HOOKPROC) HookProc, 0, GetCurrentThreadId());
}

void EditManager::UninstallHook()
{
	if (m_hHook != NULL)
	{
		::UnhookWindowsHookEx(m_hHook);
		m_hHook = NULL;
	}
}

LRESULT CALLBACK EditManager::HookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	// Look for the creation of controls.

	if (nCode == HCBT_CREATEWND)
	{
		HWND hWnd = (HWND) wParam;

		// Grab the class name.

		LPCBT_CREATEWND cw = (LPCBT_CREATEWND) lParam;
		LPCREATESTRUCT cs = (LPCREATESTRUCT) cw->lpcs;
		LPCWSTR className = cs->lpszClass;
		WCHAR temp[1024];

		if (!HIWORD(cs->lpszClass))
		{
			if (::GetClassName(hWnd, temp, sizeof(temp) / sizeof(WCHAR) -1 ) == 0)
				className = NULL;
			else
				className = temp;
		}

		if (className != NULL)
		{
			// Interested in RichEdit20W controls.

			if (wcscmp(className, L"RichEdit20W") == 0)
			{
				// Call the next handler.

				LRESULT result = ::CallNextHookEx(m_hHook, nCode, wParam, lParam);

				// Post the message to the edit state window.

				::PostMessage(m_hWindow, RICHEDIT20W_CREATEWND, (WPARAM) hWnd, 0);
				return result;
			}
		}
	}

	// Call the next handler.

	return ::CallNextHookEx(m_hHook, nCode, wParam, lParam);
}

LRESULT CALLBACK EditManager::EditWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	// Look for the create of the edit control.

	switch (uMsg)
	{
	case RICHEDIT20W_CREATEWND:
		{
			NewRichEdit20W((HWND) wParam);
			return 0;
		}
	}

	return ::DefWindowProc(hWnd, uMsg, wParam, lParam);
}

void EditManager::NewRichEdit20W(HWND hWnd)
{
	// Check if it belongs to one of the edit controls.

	for (EditControlList::iterator it = m_editControls.begin(); it != m_editControls.end(); ++it)
	{
		EditControl* pEditControl = *it;

		// If it belongs to any of the controls then hook it.

		if (pEditControl->ContainsWindow(hWnd))
		{
			pEditControl->HookWindow(hWnd);
			break;
		}	
	}
}

