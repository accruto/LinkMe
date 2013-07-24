#include "StdAfx.h"
#include "EditControl.h"
#include "EditManager.h"


EditControl::EditControl(const _CommandBarComboBoxPtr& pComboBox, const wchar_t* prompt)
{
	m_oldWndProc = NULL;
	m_hWnd = NULL;

	// Keep the combo box.

	m_pComboBox = pComboBox;

	// Set the text in the control to the prompt text.

	m_pComboBox->Text = prompt;
	m_promptText = prompt;
}

bool EditControl::ContainsWindow(HWND hWnd)
{
	if (m_hWnd != NULL)
		return false;

	// Get the handle's rectangle, and check if the control is inside it.

	try
	{
		RECT rect;
		::GetWindowRect(hWnd, &rect);

		POINT point;
		point.x = m_pComboBox->Left + m_pComboBox->Width / 2;
		point.y = m_pComboBox->Top + m_pComboBox->Height / 2;

		return (point.x < rect.right && point.x > rect.left
			&& point.y < rect.bottom && point.y > rect.top);
	}
	catch (...)
	{
		// The control has not been drawn yet and this seems to be the only way to determine that.
		// Ignore the error.

		return false;
	}
}

void EditControl::HookWindow(HWND hWnd)
{
	// Set a new WndProc.

	m_hWnd = hWnd;
	m_oldWndProc = (WNDPROC) ::GetWindowLongPtr(hWnd, GWLP_WNDPROC);
	::SetWindowLongPtr(hWnd, GWLP_WNDPROC, (LONG_PTR) EditControlWndProc);
}

WNDPROC EditControl::GetOldWndProc()
{
	return m_oldWndProc;
}

HWND EditControl::GetWindowHandle()
{
	return m_hWnd;
}

void EditControl::SetText(const wchar_t* text)
{
	m_pComboBox->Text = text;
}

_bstr_t EditControl::GetText() const
{
	// Only return the text if it is different from the prompt text.

	_bstr_t text = m_pComboBox->Text;
	return text == m_promptText ? L"" : text;
}

_bstr_t EditControl::GetPromptText() const
{
	return m_promptText;
}

LRESULT CALLBACK EditControl::EditControlWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	// Grab the control state corresponding to the window handle.

	EditControl* pEditControl = _EditManager.GetEditControl(hWnd);
	if (pEditControl == NULL)
		return 0;

	switch (uMsg)
	{
		case WM_KILLFOCUS:
			{
				// This ensures that the text is not lost when the control loses focus without pressing enter or tab.

				_bstr_t text = GetText(hWnd);
				if (text != pEditControl->GetPromptText())
					pEditControl->SetText(text);
				break;
			}

		case WM_SETFOCUS:
			{
				// If the prompt text is in place then remove it.

				_bstr_t text = GetText(hWnd);
				if (text == pEditControl->GetPromptText())
					pEditControl->SetText(L"");
				break;
			}
	}

	// Call the old WndProc.

	return ::CallWindowProc(pEditControl->GetOldWndProc(), hWnd, uMsg, wParam, lParam);
}

_bstr_t EditControl::GetText(HWND hWnd)
{
	// Allocate buffer.

	int length = static_cast<int>(::SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0));
	wchar_t* buffer = new wchar_t[length + 1];

	// Get the text.

	::SendMessage(hWnd, WM_GETTEXT, length + 1, (LPARAM) buffer);
	_bstr_t text = buffer;
	delete [] buffer;
	return text;
}

