using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Messaging;
using System.Windows.Forms;
using LinkMe.Framework.Tools;

namespace LinkMe.Framework.Configuration.Connection.Msmq
{
	internal class MsmqInitialiseForm
		:	LinkMe.Framework.Tools.Controls.Dialog
	{
		private System.Windows.Forms.ListBox lbxQueues;
		private System.Windows.Forms.CheckBox chkPublic;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkPrivate;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtMachine;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtQueue;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MsmqInitialiseForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsmqInitialiseForm));
            this.lbxQueues = new System.Windows.Forms.ListBox();
            this.chkPublic = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPrivate = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMachine = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQueue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbxQueues
            // 
            this.lbxQueues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxQueues.Location = new System.Drawing.Point(8, 88);
            this.lbxQueues.Name = "lbxQueues";
            this.lbxQueues.Size = new System.Drawing.Size(384, 199);
            this.lbxQueues.Sorted = true;
            this.lbxQueues.TabIndex = 0;
            this.lbxQueues.SelectedIndexChanged += new System.EventHandler(this.lbxQueues_SelectedIndexChanged);
            this.lbxQueues.DoubleClick += new System.EventHandler(this.lbxQueues_DoubleClick);
            // 
            // chkPublic
            // 
            this.chkPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPublic.Checked = true;
            this.chkPublic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPublic.Location = new System.Drawing.Point(64, 304);
            this.chkPublic.Name = "chkPublic";
            this.chkPublic.Size = new System.Drawing.Size(104, 20);
            this.chkPublic.TabIndex = 5;
            this.chkPublic.Text = "Public queues";
            this.chkPublic.CheckedChanged += new System.EventHandler(this.chkPublicPrivate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(8, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Show:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkPrivate
            // 
            this.chkPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPrivate.Location = new System.Drawing.Point(176, 304);
            this.chkPrivate.Name = "chkPrivate";
            this.chkPrivate.Size = new System.Drawing.Size(104, 20);
            this.chkPrivate.TabIndex = 6;
            this.chkPrivate.Text = "Private queues";
            this.chkPrivate.CheckedChanged += new System.EventHandler(this.chkPublicPrivate_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(296, 304);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 101;
            this.label2.Text = "Machine:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMachine
            // 
            this.txtMachine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachine.Location = new System.Drawing.Point(80, 16);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(312, 20);
            this.txtMachine.TabIndex = 102;
            this.txtMachine.TextChanged += new System.EventHandler(this.txtMachine_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 103;
            this.label3.Text = "Queue:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtQueue
            // 
            this.txtQueue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueue.Location = new System.Drawing.Point(80, 48);
            this.txtQueue.Name = "txtQueue";
            this.txtQueue.Size = new System.Drawing.Size(312, 20);
            this.txtQueue.TabIndex = 104;
            this.txtQueue.TextChanged += new System.EventHandler(this.txtQueue_TextChanged);
            // 
            // MsmqInitialiseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(400, 381);
            this.Controls.Add(this.txtQueue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMachine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chkPublic);
            this.Controls.Add(this.chkPrivate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxQueues);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(272, 184);
            this.Name = "MsmqInitialiseForm";
            this.Text = "Select MSMQ Queue";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public string GetMachineName()
		{
			return txtMachine.TextLength == 0 ? "." : txtMachine.Text;
		}

		public string GetSelectedQueueName()
		{
			return txtQueue.Text;
		}

		public void RefreshQueueList()
		{
			RefreshQueueList(txtMachine.Text);
		}

		private void RefreshQueueList(string machineName)
		{
            try
            {
                using (new LongRunningMonitor(this))
                {
                    if (machineName != null && machineName.Length != 0)
                        txtMachine.Text = machineName;
                    else
                        machineName = ".";

                    lbxQueues.Items.Clear();

                    if (chkPublic.Checked)
                    {
                        try
                        {
                            Add(MessageQueue.GetPublicQueuesByMachine(machineName));
                        }
                        catch (MessageQueueException mqex)
                        {
                            if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.UnsupportedOperation)
                            {
                                // This machine doesn't support public queues.
                                chkPublic.Enabled = false;
                            }
                            else
                                throw;
                        }
                    }

                    if (chkPrivate.Checked)
                    {
                        try
                        {
                            Add(MessageQueue.GetPrivateQueuesByMachine(machineName));
                        }
                        catch (MessageQueueException mqex)
                        {
                            if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.RemoteMachineNotAvailable)
                            {
                                MessageBox.Show(this, "Remote computer '" + machineName + "' is not available.",
                                    null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Failed to read the queue list on {0}.",
                    (machineName == "." ? "the local machine" : "'" + machineName + "'")), ex);
            }
		}

		private void Add(MessageQueue[] queues)
		{
			foreach ( MessageQueue queue in queues )
				lbxQueues.Items.Add(queue.QueueName);
		}

		private void lbxQueues_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( lbxQueues.SelectedIndex >= 0 )
				txtQueue.Text = lbxQueues.Text;
		}

		private void chkPublicPrivate_CheckedChanged(object sender, System.EventArgs e)
		{
			RefreshQueueList();
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshQueueList();
		}

		private void lbxQueues_DoubleClick(object sender, System.EventArgs e)
		{
			if ( lbxQueues.SelectedIndex >= 0 )
				txtQueue.Text = lbxQueues.Text;
			PerformClick(DialogResult.OK);
		}

		private void txtQueue_TextChanged(object sender, System.EventArgs e)
		{
			SetButtonEnabled(DialogResult.OK, txtQueue.Text != string.Empty);
		}

        private void txtMachine_TextChanged(object sender, EventArgs e)
        {
            chkPublic.Enabled = true;
        }
	}
}
