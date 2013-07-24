using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A form that displays buttons specified by the MessageBoxButtons enumeration and returns
	/// a DialogResult. The default is to display only an OK button. Inherit from this form to
	/// add your own controls.
	/// </summary>
	public class Dialog : Form
	{
		internal const int ButtonWidth = 72;
		internal const int ButtonHeight = 24;
		internal const int PaddingBetween = 16;
		internal const int PaddingBottom = 16;

		private class Buttons
		{
			public Buttons()
			{
				m_buttons = new ArrayList();
			}

			public IEnumerator GetEnumerator()
			{
				return m_buttons.GetEnumerator();
			}

			public void Add(Button button)
			{
				m_buttons.Add(button);
			}

			public Button this[int index]
			{
				get { return (Button) m_buttons[index]; }
			}

			public int Count
			{
				get { return m_buttons.Count; }
			}

			public void Clear()
			{
				m_buttons.Clear();
			}

			private ArrayList m_buttons;
		}

		private Buttons m_buttons;
		private HorizontalAlignment m_buttonAlignment;

		public Dialog(HorizontalAlignment buttonAlignment, MessageBoxButtons buttons, params string[] extraButtons)
		{
			ResetButtons(buttonAlignment, buttons, extraButtons);
		}

		public Dialog(MessageBoxButtons buttons, params string[] extraButtons)
			:	this(HorizontalAlignment.Center, buttons, extraButtons)
		{
		}

		public Dialog(HorizontalAlignment buttonAlignment, MessageBoxButtons buttons)
			:	this(buttonAlignment, buttons, null)
		{
		}

		public Dialog(MessageBoxButtons buttons)
			:	this(HorizontalAlignment.Center, buttons, null)
		{
		}

		public Dialog(HorizontalAlignment buttonAlignment)
			:	this(buttonAlignment, MessageBoxButtons.OK, null)
		{
		}

		public Dialog()
			:	this(HorizontalAlignment.Center, MessageBoxButtons.OK, null)
		{
		}

		protected void SetButtonEnabled(DialogResult button, bool enabled)
		{
			foreach (Button b in m_buttons)
			{
				if (b.DialogResult == button)
				{
					b.Enabled = enabled;
					return;
				}
			}

			throw new ApplicationException("There is no button with the dialog result '" + button.ToString() + "'.");
		}

		protected void SetButtonEnabled(string name, bool enabled)
		{
			foreach (Button b in m_buttons)
			{
				if (b.Name == name)
				{
					b.Enabled = enabled;
					return;
				}
			}
		}

		protected Button GetButton(DialogResult button)
		{
			foreach (Button b in m_buttons)
			{
				if (b.DialogResult == button)
					return b;
			}

			return null;
		}

		protected void PerformClick(DialogResult button)
		{
			foreach (Button b in m_buttons)
			{
				if (b.DialogResult == button)
				{
					b.PerformClick();
					return;
				}
			}

			throw new ApplicationException("There is no button with the dialog result '" + button.ToString() + "'.");
		}

		protected virtual bool OnClick(DialogResult button)
		{
			return true;
		}

		protected virtual bool OnClick(string name)
		{
			return true;
		}

		protected override void OnResize(EventArgs e)
		{
			SuspendLayout();
			LayoutButtons();
			ResumeLayout();

			base.OnResize(e);
		}

		protected void ResetButtons(HorizontalAlignment buttonAlignment, MessageBoxButtons buttons, params string[] extraButtons)
		{
			SuspendLayout();

			// Remove all existing buttons.

			if (m_buttons != null)
			{
				foreach (Button button in m_buttons)
				{
					if (Controls.Contains(button))
					{
						Controls.Remove(button);
					}
				}

				m_buttons.Clear();
			}

			m_buttonAlignment = buttonAlignment;
			InitializeButtons(buttons, extraButtons);

			AutoScaleBaseSize = new Size(5, 13);

			foreach (Button button in m_buttons)
			{
				Controls.Add(button);
			}

			Name = "Dialog";

			LayoutButtons();

			ResumeLayout(false);
		}

		private void InitializeButtons(MessageBoxButtons buttons, string[] extraButtons)
		{
			int extraButtonsCount = extraButtons == null ? 0 : extraButtons.Length;
			switch (buttons)
			{
				case MessageBoxButtons.OK:
					CreateButtons(extraButtonsCount + 1);
					m_buttons[0].Text = "OK";
					m_buttons[0].DialogResult = DialogResult.OK;
					AcceptButton = m_buttons[0];
					CancelButton = m_buttons[0];
					break;

				case MessageBoxButtons.OKCancel:
					CreateButtons(extraButtonsCount + 2);
					m_buttons[0].Text = "OK";
					m_buttons[0].DialogResult = DialogResult.OK;
					m_buttons[1].Text = "Cancel";
					m_buttons[1].DialogResult = DialogResult.Cancel;
					AcceptButton = m_buttons[0];
					CancelButton = m_buttons[1];
					break;

				case MessageBoxButtons.RetryCancel:
					CreateButtons(extraButtonsCount + 2);
					m_buttons[0].Text = "&Retry";
					m_buttons[0].DialogResult = DialogResult.Retry;
					m_buttons[1].Text = "Cancel";
					m_buttons[1].DialogResult = DialogResult.Cancel;
					CancelButton = m_buttons[1];
					break;

				case MessageBoxButtons.YesNo:
					CreateButtons(extraButtonsCount + 2);
					m_buttons[0].Text = "&Yes";
					m_buttons[0].DialogResult = DialogResult.Yes;
					m_buttons[1].Text = "&No";
					m_buttons[1].DialogResult = DialogResult.No;
					break;

				case MessageBoxButtons.YesNoCancel:
					CreateButtons(extraButtonsCount + 3);
					m_buttons[0].Text = "&Yes";
					m_buttons[0].DialogResult = DialogResult.Yes;
					m_buttons[1].Text = "&No";
					m_buttons[1].DialogResult = DialogResult.No;
					m_buttons[2].Text = "Cancel";
					m_buttons[2].DialogResult = DialogResult.Cancel;
					CancelButton = m_buttons[2];
					break;

				case MessageBoxButtons.AbortRetryIgnore:
					CreateButtons(extraButtonsCount + 3);
					m_buttons[0].Text = "&Abort";
					m_buttons[0].DialogResult = DialogResult.Abort;
					m_buttons[1].Text = "&Retry";
					m_buttons[1].DialogResult = DialogResult.Retry;
					m_buttons[2].Text = "&Ignore";
					m_buttons[2].DialogResult = DialogResult.Ignore;
					break;

				default:
					throw new InvalidEnumArgumentException("buttons", (int)buttons, typeof(MessageBoxButtons));
			}

			if ( extraButtonsCount > 0 )
				InitializeExtraButtons(extraButtons);
		}

		private void InitializeExtraButtons(string[] extraButtons)
		{
			// Fill in the button definitions at the end.

			for ( int index = 0; index < extraButtons.Length; ++index )
			{
				m_buttons[m_buttons.Count - index - 1].Text = extraButtons[extraButtons.Length - index - 1];
				m_buttons[m_buttons.Count - index - 1].DialogResult = DialogResult.None;
				m_buttons[m_buttons.Count - index - 1].Name = extraButtons[extraButtons.Length - index - 1].Replace("&", string.Empty);
			}
		}

		private void LayoutButtons()
		{
			int top = ClientSize.Height - ButtonHeight - PaddingBottom;
			int left;

			switch ( m_buttonAlignment )
			{
				case HorizontalAlignment.Left:
					left = PaddingBetween / 2;
					break;
					
				case HorizontalAlignment.Right:
					left = ClientSize.Width - (ButtonWidth * m_buttons.Count + PaddingBetween * (m_buttons.Count - 1) + PaddingBetween / 2);
					break;

				case HorizontalAlignment.Center:
				default:
					left = (ClientSize.Width - ButtonWidth * m_buttons.Count - PaddingBetween * (m_buttons.Count - 1)) / 2;
					break;
			}

			for (int index = 0; index < m_buttons.Count; index++)
			{
				m_buttons[index].Location = new Point(left, top);
				left += ButtonWidth + PaddingBetween;
			}
		}

		private void CreateButtons(int number)
		{
			m_buttons = new Buttons();

			for (int index = 0; index < number; index++)
			{
				m_buttons.Add(CreateButton(index));
			}
		}

		private Button CreateButton(int index)
		{
			Button button = new Button();

			button.Name = "dialogButton" + index.ToString();
			button.Size = new Size(ButtonWidth, ButtonHeight);
			// The buttons should at the bottom of the dialog, so put them at the bottom of the tab order as well.
			button.TabIndex = 100 + index;
			button.Click += new EventHandler(button_Click);

			return button;
		}

		private void button_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;

			if ( button.DialogResult != DialogResult.None ? OnClick(button.DialogResult) : OnClick(button.Name) )
			{
				// In case the caller is not showing this form as a dialog (ie. they called Show() instead)
				// manually close the form when the accept or cancel button is clicked.

				if (AcceptButton == sender || CancelButton == sender)
				{
					Close();
				}
			}
		}
	}
}
