using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TC = LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Editors;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class DetailPanes : TC.ThreePanes
	{
		internal TC.TextBox txtMessage;
		internal GenericEditorGrid gridParameters;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		internal LinkMe.Framework.Tools.Editors.GenericEditorGrid gridDetails;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DetailPanes()
		{
			InitializeComponent();
			Initialise();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtMessage = new LinkMe.Framework.Tools.Controls.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.gridParameters = new LinkMe.Framework.Tools.Editors.GenericEditorGrid();
			this.gridDetails = new LinkMe.Framework.Tools.Editors.GenericEditorGrid();
			this.PaneOne.SuspendLayout();
			this.PaneTwo.SuspendLayout();
			this.PaneThree.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridParameters)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
			this.SuspendLayout();
			// 
			// PaneOne
			// 
			this.PaneOne.Controls.Add(this.txtMessage);
			this.PaneOne.Controls.Add(this.label1);
			this.PaneOne.Name = "PaneOne";
			this.PaneOne.Size = new System.Drawing.Size(704, 137);
			// 
			// PaneTwo
			// 
			this.PaneTwo.Controls.Add(this.gridDetails);
			this.PaneTwo.Controls.Add(this.label2);
			this.PaneTwo.Location = new System.Drawing.Point(0, 143);
			this.PaneTwo.Name = "PaneTwo";
			this.PaneTwo.Size = new System.Drawing.Size(704, 131);
			// 
			// PaneThree
			// 
			this.PaneThree.Controls.Add(this.gridParameters);
			this.PaneThree.Controls.Add(this.label3);
			this.PaneThree.Location = new System.Drawing.Point(0, 280);
			this.PaneThree.Name = "PaneThree";
			this.PaneThree.Size = new System.Drawing.Size(704, 136);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Message:";
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(0, 16);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(704, 114);
			this.txtMessage.TabIndex = 0;
			this.txtMessage.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 19);
			this.label2.TabIndex = 0;
			this.label2.Text = "Details:";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 19);
			this.label3.TabIndex = 0;
			this.label3.Text = "Parameters:";
			// 
			// gridParameters
			// 
			this.gridParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridParameters.Location = new System.Drawing.Point(0, 16);
			this.gridParameters.MinimumSize = new System.Drawing.Size(0, 0);
			this.gridParameters.Name = "gridParameters";
			this.gridParameters.NameColumnHeaderText = "Parameter";
			this.gridParameters.ReadOnly = true;
			this.gridParameters.ShowMemberTypeColumn = false;
			this.gridParameters.Size = new System.Drawing.Size(704, 120);
			this.gridParameters.TabIndex = 1;
			this.gridParameters.ValueFormat = LinkMe.Framework.Tools.Editors.EditorValueFormat.PlainString;
			// 
			// gridDetails
			// 
			this.gridDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridDetails.Location = new System.Drawing.Point(0, 16);
			this.gridDetails.MinimumSize = new System.Drawing.Size(0, 0);
			this.gridDetails.Name = "gridDetails";
			this.gridDetails.NameColumnHeaderText = "Detail";
			this.gridDetails.ReadOnly = true;
			this.gridDetails.ShowMemberTypeColumn = false;
			this.gridDetails.Size = new System.Drawing.Size(704, 110);
			this.gridDetails.TabIndex = 2;
			this.gridDetails.ValueFormat = LinkMe.Framework.Tools.Editors.EditorValueFormat.PlainString;
			// 
			// DetailPanes
			// 
			this.Name = "DetailPanes";
			this.Size = new System.Drawing.Size(704, 416);
			this.SplitRatioBottom = -1F;
			this.SplitterBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PaneOne.ResumeLayout(false);
			this.PaneTwo.ResumeLayout(false);
			this.PaneThree.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridParameters)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
