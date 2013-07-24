#pragma once


class EditControl
{
public:
	EditControl(const _CommandBarComboBoxPtr& pComboBox, const wchar_t* prompt);

	bool ContainsWindow(HWND hWnd);
	void HookWindow(HWND hWnd);

	void SetText(const wchar_t* text);
	_bstr_t GetText() const;
	_bstr_t GetPromptText() const;

	WNDPROC GetOldWndProc();
	HWND GetWindowHandle();

private:
	static LRESULT CALLBACK EditControlWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);
	static _bstr_t GetText(HWND hWnd);

	_CommandBarComboBoxPtr m_pComboBox;
	_bstr_t m_promptText;
	WNDPROC m_oldWndProc;
	HWND m_hWnd;
};
