using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using TC = LinkMe.Framework.Tools.Controls;
using LinkMeType = LinkMe.Framework.Type;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class TimeZoneForm : TC.Dialog
	{
		private System.TimeZone m_original = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radCurrent;
		private System.Windows.Forms.RadioButton radUtc;
		private System.Windows.Forms.RadioButton radSpecified;
		private System.Windows.Forms.ComboBox cboTimeZone;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TimeZoneForm()
			: base(MessageBoxButtons.OKCancel)
		{
			InitializeComponent();

			SetButtonEnabled(DialogResult.OK, false);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.radCurrent = new System.Windows.Forms.RadioButton();
			this.radUtc = new System.Windows.Forms.RadioButton();
			this.radSpecified = new System.Windows.Forms.RadioButton();
			this.cboTimeZone = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(336, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select the time zone in which message times should be displayed.";
			// 
			// radCurrent
			// 
			this.radCurrent.Checked = true;
			this.radCurrent.Location = new System.Drawing.Point(8, 32);
			this.radCurrent.Name = "radCurrent";
			this.radCurrent.Size = new System.Drawing.Size(176, 24);
			this.radCurrent.TabIndex = 1;
			this.radCurrent.TabStop = true;
			this.radCurrent.Text = "The current (local) time zone";
			this.radCurrent.CheckedChanged += new System.EventHandler(this.radTimeZone_CheckedChanged);
			// 
			// radUtc
			// 
			this.radUtc.Location = new System.Drawing.Point(8, 56);
			this.radUtc.Name = "radUtc";
			this.radUtc.Size = new System.Drawing.Size(192, 24);
			this.radUtc.TabIndex = 2;
			this.radUtc.Text = "Universal Time Coordinate (UTC)";
			this.radUtc.CheckedChanged += new System.EventHandler(this.radTimeZone_CheckedChanged);
			// 
			// radSpecified
			// 
			this.radSpecified.Location = new System.Drawing.Point(8, 80);
			this.radSpecified.Name = "radSpecified";
			this.radSpecified.Size = new System.Drawing.Size(144, 24);
			this.radSpecified.TabIndex = 3;
			this.radSpecified.Text = "The following time zone:";
			this.radSpecified.CheckedChanged += new System.EventHandler(this.radTimeZone_CheckedChanged);
			// 
			// cboTimeZone
			// 
			this.cboTimeZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTimeZone.Enabled = false;
			this.cboTimeZone.Location = new System.Drawing.Point(24, 104);
			this.cboTimeZone.Name = "cboTimeZone";
			this.cboTimeZone.Size = new System.Drawing.Size(312, 21);
			this.cboTimeZone.Sorted = true;
			this.cboTimeZone.TabIndex = 4;
			this.cboTimeZone.SelectedIndexChanged += new System.EventHandler(this.cboTimeZone_SelectedIndexChanged);
			// 
			// TimeZoneForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 181);
			this.Controls.Add(this.cboTimeZone);
			this.Controls.Add(this.radSpecified);
			this.Controls.Add(this.radUtc);
			this.Controls.Add(this.radCurrent);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 208);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(352, 208);
			this.Name = "TimeZoneForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Time Zone";
			this.ResumeLayout(false);

		}
		#endregion

		public void DisplayValue(System.TimeZone value)
		{
			EnsureInitialised();

			if (value == null)
				throw new ArgumentNullException("value");

			if (value == System.TimeZone.CurrentTimeZone)
			{
				radCurrent.Checked = true;
			}
			else
			{
				LinkMeType.TimeZone typeTz = value as LinkMeType.TimeZone;
				if (typeTz == null)
				{
					throw new ArgumentException("Unsupported type of TimeZone object: "
						+ value.GetType().FullName, "value");
				}

				if (typeTz.Equals(LinkMeType.TimeZone.UTC))
				{
					radUtc.Checked = true;
				}
				else
				{
					radSpecified.Checked = true;
					
					bool found = false;
					for (int index = 0; index < cboTimeZone.Items.Count; index++)
					{
						if (typeTz.StandardName == (string)cboTimeZone.Items[index])
						{
							found = true;
							cboTimeZone.SelectedIndex = index;
							break;
						}
					}

					if (!found)
					{
						cboTimeZone.Items.Insert(0, value.StandardName);
						cboTimeZone.SelectedIndex = 0;
					}
				}
			}

			m_original = value;
			SetButtonEnabled(DialogResult.OK, false);
		}

		public System.TimeZone GetValue()
		{
			if (radCurrent.Checked)
				return System.TimeZone.CurrentTimeZone;
			else if (radUtc.Checked)
				return LinkMeType.TimeZone.UTC;
			else if (radSpecified.Checked)
			{
				string tzName = (string)cboTimeZone.SelectedItem;
				return (tzName == null ? null : new LinkMeType.TimeZone(tzName));
			}
			else
			{
				Debug.Fail("None of the expected radio buttons are checked.");
				return null;
			}
		}

		private void EnsureInitialised()
		{
			if (cboTimeZone.Items.Count == 0)
			{
				foreach (LinkMeType.TimeZone timeZone in LinkMeType.TimeZone.TimeZones)
				{
					cboTimeZone.Items.Add(timeZone.StandardName);
				}
			}
		}

		private void radTimeZone_CheckedChanged(object sender, System.EventArgs e)
		{
			cboTimeZone.Enabled = radSpecified.Checked;

			System.TimeZone current = GetValue();
			SetButtonEnabled(DialogResult.OK, (current != null && !current.Equals(m_original)));
		}

		private void cboTimeZone_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboTimeZone.SelectedIndex == -1)
			{
				SetButtonEnabled(DialogResult.OK, false);
			}
			else
			{
				System.TimeZone current = GetValue();
				SetButtonEnabled(DialogResult.OK, (current != null && !current.Equals(m_original)));
			}
		}
	}
}
