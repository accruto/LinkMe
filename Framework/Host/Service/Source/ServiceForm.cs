using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Host.Service
{
	/// <summary>
	/// Summary description for ServiceForm.
	/// </summary>
	public class ServiceForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtRootFolder;
		private System.Windows.Forms.TextBox txtConfigurationFile;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Button btnExit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private readonly System.ComponentModel.Container components = null;
		private ServiceManager _manager;
		private readonly ServiceParameters _parameters;

		public ServiceForm(ServiceParameters parameters)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_parameters = parameters;
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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.txtConfigurationFile = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(216, 80);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Root folder:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Configuration file:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootFolder.Location = new System.Drawing.Point(114, 16);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.ReadOnly = true;
            this.txtRootFolder.Size = new System.Drawing.Size(264, 20);
            this.txtRootFolder.TabIndex = 3;
            // 
            // txtConfigurationFile
            // 
            this.txtConfigurationFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfigurationFile.Location = new System.Drawing.Point(114, 48);
            this.txtConfigurationFile.Name = "txtConfigurationFile";
            this.txtConfigurationFile.ReadOnly = true;
            this.txtConfigurationFile.Size = new System.Drawing.Size(264, 20);
            this.txtConfigurationFile.TabIndex = 4;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(304, 80);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(216, 112);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 6;
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(304, 112);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Status:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(88, 96);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(100, 20);
            this.txtStatus.TabIndex = 9;
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnExit.Location = new System.Drawing.Point(256, 152);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "&Exit";
            // 
            // ServiceForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(402, 191);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.txtConfigurationFile);
            this.Controls.Add(this.txtRootFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ServiceForm";
            this.Text = "LinkMe Host Service";
            this.Load += new System.EventHandler(this.ServiceForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ServiceForm_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ServiceForm_Load(object sender, System.EventArgs e)
		{
			// Create a new service manager.

			_manager = new ServiceManager(_parameters);
			txtRootFolder.Text = _parameters.ApplicationRootFolder;
			txtConfigurationFile.Text = _parameters.ConfigurationFile;

			// Start it.

			btnStart_Click(sender, e);
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			try
			{
				_manager.Start();
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The container could not be started:").ShowDialog();
			}

			TryUpdateDisplay();
		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			try
			{
				_manager.Pause();
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The container could not be paused:").ShowDialog();
			}

			TryUpdateDisplay();
		}

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			try
			{
				_manager.Continue();
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The container could not be continued:").ShowDialog();
			}

			TryUpdateDisplay();
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			try
			{
				_manager.Stop();
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The container could not be stopped:").ShowDialog();
			}
		
			TryUpdateDisplay();
		}

		private void ServiceForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				_manager.Stop();
			}
			catch ( System.Exception )
			{
			}
		}

        private void TryUpdateDisplay()
        {
            try
            {
                UpdateDisplay();
            }
            catch (System.Exception ex)
            {
                new ExceptionDialog(ex, "Failed to update display:").ShowDialog();
            }
        }

	    private void UpdateDisplay()
		{
			txtStatus.Text = _manager.Status.ToString();

			// Update the buttons based on the current status.

			btnStart.Enabled = false;
			btnPause.Enabled = false;
			btnContinue.Enabled = false;
			btnStop.Enabled = false;

			switch ( _manager.Status )
			{
				case ChannelStatus.Paused:
					btnStop.Enabled = true;
					btnContinue.Enabled = true;
					break;

                case ChannelStatus.Running:
					btnStop.Enabled = true;
					btnPause.Enabled = true;
					break;

                case ChannelStatus.Stopped:
					btnStart.Enabled = true;
					break;
			}
		}
	}
}
