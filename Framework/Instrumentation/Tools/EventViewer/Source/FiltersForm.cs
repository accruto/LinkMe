using System;
using System.Windows.Forms;
using LinkMe.Framework.Instrumentation.Message;
using TC = LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
    internal class FiltersForm : TC.Dialog
    {
        private Filters m_original = null;
        private Label label1;
        private TextBox txtEventFilter;
        private TextBox txtSourceFilter;
        private Label label2;
        private TextBox txtTypeFilter;
        private Label label3;
        private TextBox txtMethodFilter;
        private Label label4;
        private TextBox txtMessageFilter;
        private Label label5;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label7;
        private TextBox txtDetailValue;
        private Label label6;
        private TextBox txtDetailName;
        private GroupBox groupBox3;
        private Label label8;
        private TextBox txtParameterValue;
        private Label label9;
        private TextBox txtParameterName;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FiltersForm()
            : base(MessageBoxButtons.OKCancel)
        {
            InitializeComponent();

            SetButtonEnabled(DialogResult.OK, false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtEventFilter = new System.Windows.Forms.TextBox();
            this.txtSourceFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTypeFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMethodFilter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMessageFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDetailValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDetailName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtParameterValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtParameterName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Event:";
            // 
            // txtEventFilter
            // 
            this.txtEventFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEventFilter.Location = new System.Drawing.Point(89, 52);
            this.txtEventFilter.Name = "txtEventFilter";
            this.txtEventFilter.Size = new System.Drawing.Size(322, 20);
            this.txtEventFilter.TabIndex = 102;
            this.txtEventFilter.TextChanged += new System.EventHandler(this.txtEventFilter_TextChanged);
            // 
            // txtSourceFilter
            // 
            this.txtSourceFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFilter.Location = new System.Drawing.Point(89, 25);
            this.txtSourceFilter.Name = "txtSourceFilter";
            this.txtSourceFilter.Size = new System.Drawing.Size(322, 20);
            this.txtSourceFilter.TabIndex = 104;
            this.txtSourceFilter.TextChanged += new System.EventHandler(this.txtSourceFilter_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 103;
            this.label2.Text = "Source:";
            // 
            // txtTypeFilter
            // 
            this.txtTypeFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTypeFilter.Location = new System.Drawing.Point(89, 79);
            this.txtTypeFilter.Name = "txtTypeFilter";
            this.txtTypeFilter.Size = new System.Drawing.Size(322, 20);
            this.txtTypeFilter.TabIndex = 106;
            this.txtTypeFilter.TextChanged += new System.EventHandler(this.txtTypeFilter_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "Type:";
            // 
            // txtMethodFilter
            // 
            this.txtMethodFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMethodFilter.Location = new System.Drawing.Point(89, 106);
            this.txtMethodFilter.Name = "txtMethodFilter";
            this.txtMethodFilter.Size = new System.Drawing.Size(322, 20);
            this.txtMethodFilter.TabIndex = 108;
            this.txtMethodFilter.TextChanged += new System.EventHandler(this.txtMethodFilter_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Method:";
            // 
            // txtMessageFilter
            // 
            this.txtMessageFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageFilter.Location = new System.Drawing.Point(89, 133);
            this.txtMessageFilter.Name = "txtMessageFilter";
            this.txtMessageFilter.Size = new System.Drawing.Size(322, 20);
            this.txtMessageFilter.TabIndex = 110;
            this.txtMessageFilter.TextChanged += new System.EventHandler(this.txtMessageFilter_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 109;
            this.label5.Text = "Message:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtMessageFilter);
            this.groupBox1.Controls.Add(this.txtEventFilter);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMethodFilter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSourceFilter);
            this.groupBox1.Controls.Add(this.txtTypeFilter);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 166);
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Standard";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtDetailValue);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDetailName);
            this.groupBox2.Location = new System.Drawing.Point(12, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 77);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detail";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 113;
            this.label7.Text = "Value:";
            // 
            // txtDetailValue
            // 
            this.txtDetailValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetailValue.Location = new System.Drawing.Point(89, 47);
            this.txtDetailValue.Name = "txtDetailValue";
            this.txtDetailValue.Size = new System.Drawing.Size(322, 20);
            this.txtDetailValue.TabIndex = 114;
            this.txtDetailValue.TextChanged += new System.EventHandler(this.txtDetailValue_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 111;
            this.label6.Text = "Name:";
            // 
            // txtDetailName
            // 
            this.txtDetailName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetailName.Location = new System.Drawing.Point(89, 20);
            this.txtDetailName.Name = "txtDetailName";
            this.txtDetailName.Size = new System.Drawing.Size(322, 20);
            this.txtDetailName.TabIndex = 112;
            this.txtDetailName.TextChanged += new System.EventHandler(this.txtDetailName_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtParameterValue);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtParameterName);
            this.groupBox3.Location = new System.Drawing.Point(12, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(427, 77);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parameter";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 113;
            this.label8.Text = "Value:";
            // 
            // txtParameterValue
            // 
            this.txtParameterValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameterValue.Location = new System.Drawing.Point(89, 47);
            this.txtParameterValue.Name = "txtParameterValue";
            this.txtParameterValue.Size = new System.Drawing.Size(322, 20);
            this.txtParameterValue.TabIndex = 114;
            this.txtParameterValue.TextChanged += new System.EventHandler(this.txtParameterValue_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 111;
            this.label9.Text = "Name:";
            // 
            // txtParameterName
            // 
            this.txtParameterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameterName.Location = new System.Drawing.Point(89, 20);
            this.txtParameterName.Name = "txtParameterName";
            this.txtParameterName.Size = new System.Drawing.Size(322, 20);
            this.txtParameterName.TabIndex = 112;
            this.txtParameterName.TextChanged += new System.EventHandler(this.txtParameterName_TextChanged);
            // 
            // FiltersForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(451, 404);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 440);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(352, 440);
            this.Name = "FiltersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Filters";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        public void DisplayValue(Filters value)
        {
            txtEventFilter.Text = string.Empty;

            if (value == null)
                value = new Filters();

            foreach (var filter in value)
            {
                if (filter is SourceFilter)
                    txtSourceFilter.Text = filter.Pattern.Value;
                else if (filter is EventFilter)
                    txtEventFilter.Text = filter.Pattern.Value;
                else if (filter is TypeFilter)
                    txtTypeFilter.Text = filter.Pattern.Value;
                else if (filter is MethodFilter)
                    txtMethodFilter.Text = filter.Pattern.Value;
                else if (filter is MessageFilter)
                    txtMessageFilter.Text = filter.Pattern.Value;
                else if (filter is DetailFilter)
                {
                    txtDetailName.Text = filter.Name;
                    txtDetailValue.Text = filter.Pattern.Value;
                }
                else if (filter is ParameterFilter)
                {
                    txtParameterName.Text = filter.Name;
                    txtParameterValue.Text = filter.Pattern.Value;
                }
            }

            m_original = value;
            SetButtonEnabled(DialogResult.OK, false);
        }

        public Filters GetValue()
        {
            var filters = new Filters();

            if (!string.IsNullOrEmpty(txtSourceFilter.Text))
                filters.Add(new SourceFilter(PatternType.Regex, txtSourceFilter.Text));
            if (!string.IsNullOrEmpty(txtEventFilter.Text))
                filters.Add(new EventFilter(PatternType.Regex, txtEventFilter.Text));
            if (!string.IsNullOrEmpty(txtTypeFilter.Text))
                filters.Add(new TypeFilter(PatternType.Regex, txtTypeFilter.Text));
            if (!string.IsNullOrEmpty(txtMethodFilter.Text))
                filters.Add(new MethodFilter(PatternType.Regex, txtMethodFilter.Text));
            if (!string.IsNullOrEmpty(txtMessageFilter.Text))
                filters.Add(new MessageFilter(PatternType.Regex, txtMessageFilter.Text));

            if (!string.IsNullOrEmpty(txtDetailName.Text) && !string.IsNullOrEmpty(txtDetailValue.Text))
                filters.Add(new DetailFilter(txtDetailName.Text, PatternType.Regex, txtDetailValue.Text));
            if (!string.IsNullOrEmpty(txtParameterName.Text) && !string.IsNullOrEmpty(txtParameterValue.Text))
                filters.Add(new ParameterFilter(txtParameterName.Text, PatternType.Regex, txtParameterValue.Text));

            return filters;
        }

        private void Changed()
        {
            var current = GetValue();
            SetButtonEnabled(DialogResult.OK, (current != null && !current.Equals(m_original)));
        }

        private void txtEventFilter_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtSourceFilter_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtTypeFilter_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtMethodFilter_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtMessageFilter_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtDetailName_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtDetailValue_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtParameterName_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }

        private void txtParameterValue_TextChanged(object sender, EventArgs e)
        {
            Changed();
        }
    }
}
