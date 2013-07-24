#pragma once

#include <list>
#include "EditControl.h"


class EditManager
{
public:
	EditManager();

	static void Shutdown();

	static void Add(EditControl* pEditControl);
	static EditControl* GetEditControl(HWND hWnd);

private:
	static void CreateEditWindow();
	static void DestroyEditWindow();

	static void InstallHook();
	static void UninstallHook();

	static void NewRichEdit20W(HWND hWnd);

	static LRESULT CALLBACK HookProc(int nCode, WPARAM wParam, LPARAM lParam);
	static LRESULT CALLBACK EditWndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

	static HWND m_hWindow;
	static HHOOK m_hHook;

	typedef std::list<EditControl*> EditControlList;
	static EditControlList m_editControls;

	const static int RICHEDIT20W_CREATEWND = (WM_USER + 0x668);
};

extern EditManager _EditManager;
