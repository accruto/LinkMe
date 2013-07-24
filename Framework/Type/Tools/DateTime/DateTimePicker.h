#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;

const int wmReflectNotify = 0x2000 /* WM_REFLECT */ + 0x4e /* WM_NOTIFY */;

namespace LinkMeDateTime
{
	public __gc __interface IDateTimePickerOwner
	{
		System::String* GetFormatText(System::String* format);
		bool KeyDown(System::String* format, System::Windows::Forms::Keys key);
	};

	/// <summary> 
	/// Summary for DateTimePicker
	/// </summary>
	public __gc class DateTimePicker : public System::Windows::Forms::DateTimePicker
	{
	public: 
		DateTimePicker(void)
		{
			InitializeComponent();
		}

		void SetOwner(IDateTimePickerOwner* owner)
		{
			m_owner = owner;
		}

		bool GetIgnoreUpKey()
		{
			return m_ignoreUpKey;
		}

        void SetIgnoreUpKey(bool value)
		{
			m_ignoreUpKey = value;
		}

	protected: 
		void Dispose(Boolean disposing)
		{
			if (disposing && components)
			{
				components->Dispose();
			}
			__super::Dispose(disposing);
		} 
		       
		void WndProc(Message* message)
		{
			if ( message->Msg == WM_KEYUP && m_ignoreUpKey )
				return;

			if ( message->Msg == wmReflectNotify )
			{
				NMHDR* nmhdr = (NMHDR*) (int) message->LParam;
				switch ( nmhdr->code )
				{
				case DTN_FORMATQUERY:
					WmFormatQuery(message);
					break;

				case DTN_FORMAT:
					WmFormat(message);
					break;

				case DTN_WMKEYDOWN:
					WmKeyDown(message);
					break;

				case DTN_USERSTRING:
					WmUserString(message);
					break;
				}
			}

			__super::WndProc(message);
		}

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container* components;
		IDateTimePickerOwner* m_owner;
		bool m_ignoreUpKey;	// This is used by the DataGridPrimitiveValueColumn.
 
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>		
		void InitializeComponent(void)
		{
			LONG style = ::GetWindowLong((HWND) (void*) Handle, GWL_STYLE);
			style |= DTS_APPCANPARSE;
			::SetWindowLong((HWND) (void*) Handle, GWL_STYLE, style);
		}
		
		void WmFormatQuery(Message* message)
		{
			if ( m_owner != NULL )
			{
				NMDATETIMEFORMATQUERY* formatQuery = (NMDATETIMEFORMATQUERY*) (void*) message->LParam;

				// Get the text.

				System::String* text = m_owner->GetFormatText(formatQuery->pszFormat);
				LPCWSTR pszText = (LPCWSTR) (void*) System::Runtime::InteropServices::Marshal::StringToHGlobalUni(text);

				// Set the size.

				HWND hWnd = (HWND) (void*) this->Handle;
				HDC hdc = ::GetDC(hWnd);
				HFONT hFont = (HFONT)::SendMessage(hWnd, WM_GETFONT, 0, 0);
				HFONT hOrigFont = (HFONT) ::SelectObject(hdc, (HGDIOBJ) hFont);
				::GetTextExtentPoint32W(hdc, pszText, text->Length, &formatQuery->szMax);
				System::Runtime::InteropServices::Marshal::FreeHGlobal((void*) pszText);

				// Release.

				::SelectObject(hdc, (HGDIOBJ) hOrigFont);
				::ReleaseDC(hWnd, hdc);
			}
		}

		void WmFormat(Message* message)
		{
			if ( m_owner != NULL )
			{
				NMDATETIMEFORMAT* format = (NMDATETIMEFORMAT*) (void*) message->LParam;

				// Get the text.

				System::String* text = m_owner->GetFormatText(format->pszFormat);

				// Set it.

				int index;
				for ( index = 0; index < min(text->Length, 63); ++index )
					format->pszDisplay[index] = text->Chars[index];
				format->pszDisplay[index] = L'\0';
			}
		}

		void WmKeyDown(Message* message)
		{
			if ( m_owner != NULL )
			{
				NMDATETIMEWMKEYDOWN* keyDown = (NMDATETIMEWMKEYDOWN*) (void*) message->LParam;

				if ( m_owner->KeyDown(keyDown->pszFormat, (System::Windows::Forms::Keys) keyDown->nVirtKey) )
				{
					// Fire events and refresh the display.

					OnTextChanged(System::EventArgs::Empty);
					OnValueChanged(System::EventArgs::Empty);
					Refresh();
				}
			}
		}

		void WmUserString(Message* message)
		{
		}
	};
}