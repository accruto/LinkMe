using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Framework.Tools.Xml;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// Displays an Exception object.
	/// </summary>
	[ToolboxBitmap(typeof(Bitmaps.Location), "ExceptionViewer.bmp")]
	public class ExceptionViewer : UserControl
	{
		private const int ImageIndexException = 0;
		private const int ImageIndexProperty = 1;
		private const string ExceptionFormatString = "<error: an exception of type '{0}' was thrown>";

		private ExceptionInfo _exception;
		private bool _xmlWritten;

		private System.Windows.Forms.TabControl tabsView;
		private System.Windows.Forms.TabPage tabTreeView;
		private System.Windows.Forms.TabPage tabText;
		private TextBox txtException;
		private System.Windows.Forms.TabPage tabXml;
		private System.Windows.Forms.RichTextBox rtfXml;
		private ExceptionPanes panes;

		public ExceptionViewer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            rtfXml.BackColor = Constants.Colors.ReadOnlyBackground;
			MinimumSize = new Size(120, 90);

			panes.tvwProperties.AfterSelect += tvwProperties_AfterSelect;
			panes.tvwProperties.Focus();
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabsView = new System.Windows.Forms.TabControl();
			this.tabTreeView = new System.Windows.Forms.TabPage();
			this.panes = new LinkMe.Framework.Tools.Controls.ExceptionPanes();
			this.tabText = new System.Windows.Forms.TabPage();
			this.txtException = new LinkMe.Framework.Tools.Controls.TextBox();
			this.tabXml = new System.Windows.Forms.TabPage();
			this.rtfXml = new System.Windows.Forms.RichTextBox();
			this.tabsView.SuspendLayout();
			this.tabTreeView.SuspendLayout();
			this.tabText.SuspendLayout();
			this.tabXml.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabsView
			// 
			this.tabsView.Controls.Add(this.tabTreeView);
			this.tabsView.Controls.Add(this.tabText);
			this.tabsView.Controls.Add(this.tabXml);
			this.tabsView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsView.Location = new System.Drawing.Point(0, 0);
			this.tabsView.Name = "tabsView";
			this.tabsView.SelectedIndex = 0;
			this.tabsView.Size = new System.Drawing.Size(616, 488);
			this.tabsView.TabIndex = 0;
			this.tabsView.SelectedIndexChanged += new System.EventHandler(this.tabsView_SelectedIndexChanged);
			// 
			// tabTreeView
			// 
			this.tabTreeView.Controls.Add(this.panes);
			this.tabTreeView.Location = new System.Drawing.Point(4, 22);
			this.tabTreeView.Name = "tabTreeView";
			this.tabTreeView.Size = new System.Drawing.Size(608, 462);
			this.tabTreeView.TabIndex = 0;
			this.tabTreeView.Text = "TreeView";
			// 
			// panes
			// 
			this.panes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panes.Location = new System.Drawing.Point(4, 4);
			this.panes.MinimumSize = new System.Drawing.Size(0, 0);
			this.panes.Name = "panes";
			this.panes.PaneOneMinSize = 60;
			this.panes.PaneOneVisible = true;
			this.panes.PaneTwoMinSize = 60;
			this.panes.PaneTwoVisible = true;
			this.panes.Size = new System.Drawing.Size(600, 459);
			this.panes.TabIndex = 5;
			// 
			// tabText
			// 
			this.tabText.Controls.Add(this.txtException);
			this.tabText.Location = new System.Drawing.Point(4, 22);
			this.tabText.Name = "tabText";
			this.tabText.Size = new System.Drawing.Size(608, 462);
			this.tabText.TabIndex = 1;
			this.tabText.Text = "Text";
			// 
			// txtException
			// 
			this.txtException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtException.Location = new System.Drawing.Point(4, 6);
			this.txtException.Multiline = true;
			this.txtException.Name = "txtException";
			this.txtException.ReadOnly = true;
			this.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtException.Size = new System.Drawing.Size(600, 452);
			this.txtException.TabIndex = 0;
			this.txtException.Text = "";
			// 
			// tabXml
			// 
			this.tabXml.Controls.Add(this.rtfXml);
			this.tabXml.Location = new System.Drawing.Point(4, 22);
			this.tabXml.Name = "tabXml";
			this.tabXml.Size = new System.Drawing.Size(608, 462);
			this.tabXml.TabIndex = 2;
			this.tabXml.Text = "XML";
			// 
			// rtfXml
			// 
			this.rtfXml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rtfXml.Location = new System.Drawing.Point(4, 6);
			this.rtfXml.Name = "rtfXml";
			this.rtfXml.ReadOnly = true;
			this.rtfXml.Size = new System.Drawing.Size(600, 452);
			this.rtfXml.TabIndex = 0;
			this.rtfXml.Text = "";
			// 
			// ExceptionViewer
			// 
			this.Controls.Add(this.tabsView);
			this.Name = "ExceptionViewer";
			this.Size = new System.Drawing.Size(616, 488);
			this.tabsView.ResumeLayout(false);
			this.tabTreeView.ResumeLayout(false);
			this.tabText.ResumeLayout(false);
			this.tabXml.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private static void AddTreeNodeForProperty(string name, object value, TreeNodeCollection parentNodes)
		{
			var newNode = new TreeNode(name, ImageIndexProperty, ImageIndexProperty) {Tag = value};
		    parentNodes.Add(newNode);
		}

		public void DisplayException(ExceptionInfo value)
		{
			try
			{
				_exception = value;
				_xmlWritten = false;

				DisplayText(value);
				DisplayInTreeView(value);
                DisplayXmlIfSelected(value);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		public void DisplayException(Exception value)
		{
			if (value == null)
			{
				DisplayException((ExceptionInfo)null);
			}
			else
			{
				DisplayException(new ExceptionInfo(value, null));
			}
		}

		public void InitialiseFocus()
		{
			// Set focus to the TreeView, so the user can easily switch
			// between exception properties with the keyboard.
			panes.Focus();
			panes.tvwProperties.Focus();
		}

		public void SaveXml(string filePath)
		{
			try
			{
				var writer = new XmlTextWriter(filePath, XmlWriteAdaptor.XmlEncoding);

				try
				{
					writer.Formatting = Formatting.Indented;
					writer.Indentation = 2;

					_exception.WriteOuterXml(writer);
				}
				finally
				{
					writer.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("The following exception occurred while trying to save the exception"
                    + " to XML file '{0}':{1}{1}{2}.", filePath, System.Environment.NewLine, ex),
					"Failed to save exception to XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void DisplayText(ExceptionInfo exception)
		{
			if (exception == null)
			{
				txtException.Text = "<no exception>";
			}
			else
			{
				try
				{
					txtException.Text = TextUtil.ReplaceLfWithCrlf(exception.ToString());
				}
				catch (Exception exEx)
				{
					txtException.Text = string.Format(ExceptionFormatString, exEx.GetType().FullName);
				}
			}
		}

        private void DisplayXmlIfSelected(IXmlSerializable exception)
        {
            if (tabsView.SelectedIndex != 2 || _xmlWritten)
                return;

            if (exception == null)
            {
                rtfXml.Text = string.Empty;
            }
            else
            {
                using (var stringWriter = new StringWriter())
                {
                    XmlTextWriter writer = new XmlRichTextWriter(stringWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 2
                    };

                    exception.WriteOuterXml(writer);

                    writer.Close();

                    rtfXml.Rtf = stringWriter.ToString();
                }
            }

            _xmlWritten = true;
        }

	    private void DisplayInTreeView(ExceptionInfo exception)
		{
			if (exception == null)
			{
				panes.txtValue.Text = string.Empty;
				panes.tvwProperties.Enabled = false;
				panes.tvwProperties.Nodes.Clear();
			}
			else
			{
				panes.tvwProperties.BeginUpdate();

				try
				{
					panes.tvwProperties.Enabled = true;
					panes.tvwProperties.Nodes.Clear();

					AddTreeNodeForException(exception, panes.tvwProperties.Nodes, string.Empty);

					panes.tvwProperties.SelectedNode = panes.tvwProperties.Nodes[0];
					panes.tvwProperties.SelectedNode.Expand();
				}
				finally
				{
					panes.tvwProperties.EndUpdate();
				}
			}
		}

		/// <summary>
		/// Add a tree node for an ExceptionInfo object.
		/// </summary>
		private static void AddTreeNodeForException(ExceptionInfo ex, TreeNodeCollection parentNodes, string prefix)
		{
            string typeName = (ex.Type == null ? "<unknown type>" : ex.Type.FullName);
			var newNode = new TreeNode(prefix + typeName, ImageIndexException, ImageIndexException) {Tag = ex};
		    parentNodes.Add(newNode);

			// Add InnerException.

			if (ex.InnerException != null)
			{
				AddTreeNodeForException(ex.InnerException, newNode.Nodes, "InnerException: ");
			}

			// Add System.Exception properties.

			AddTreeNodeForProperty("Message", ex.Message, newNode.Nodes);
			AddTreeNodeForProperty("StackTrace", ex.StackTrace, newNode.Nodes);
			AddTreeNodeForProperty("Source", ex.Source, newNode.Nodes);
			AddTreeNodeForProperty("TargetSite", ex.TargetSite, newNode.Nodes);
			AddTreeNodeForProperty("HResult", ex.HResult, newNode.Nodes);
			AddTreeNodeForProperty("HelpLink", ex.HelpLink, newNode.Nodes);

			// Add properties defined on derived exceptions.

			IDictionaryEnumerator enumerator = ex.AdditionalProperties.GetEnumerator();
			enumerator.Reset();

			while (enumerator.MoveNext())
			{
				if (enumerator.Value is IDictionary)
                    AddTreeNodeForTable((string)enumerator.Key, (IDictionary)enumerator.Value, newNode.Nodes);
				else
					AddTreeNodeForProperty((string)enumerator.Key, enumerator.Value, newNode.Nodes);
			}
		}

        private static void AddTreeNodeForTable(string name, IDictionary value, TreeNodeCollection parentNodes)
		{
			var newNode = new TreeNode(name, ImageIndexProperty, ImageIndexProperty) {Tag = value};
            parentNodes.Add(newNode);

			// Iterate over details.

			IDictionaryEnumerator enumerator =  value.GetEnumerator();
			while (enumerator.MoveNext())
			{
				// Keep iterating.

				var innerTable = enumerator.Value as IDictionary;

				if (innerTable != null)
					AddTreeNodeForTable((string)enumerator.Key, innerTable, newNode.Nodes);
				else
					AddTreeNodeForProperty((string)enumerator.Key, enumerator.Value, newNode.Nodes);
			}
		}

		private void tvwProperties_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				object nodeValue = e.Node.Tag;

				if (nodeValue is ExceptionInfo)
				{
					panes.lblValue.Text = "Message:";
					panes.txtValue.Text = ((ExceptionInfo) nodeValue).GetMessageTree();
				}
				else if (nodeValue is IDictionary)
				{
					// Clear it.

					panes.lblValue.Text = "";
					panes.txtValue.Text = "";
				}
				else
				{
					panes.lblValue.Text = "Property value:";
					panes.txtValue.Text = (nodeValue == null ? "<null>" : (string)nodeValue);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		/// <summary>
		/// Use this function to handle our own exceptions - we want to prevent an infinite loop where the
		/// caller uses this control to display any exceptions that occur.
		/// </summary>
		private void HandleException(Exception ex)
		{
			MessageBox.Show(string.Format("The following exception occurred while trying to display an exception"
				+ " in the {0} control. Please report this as a bug in the control.{1}{1}{2}",
                GetType().FullName, System.Environment.NewLine, ex),
				"Unexpected exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void tabsView_SelectedIndexChanged(object sender, EventArgs e)
		{
            DisplayXmlIfSelected(_exception);
		}
	}
}
