using System.Diagnostics;
using System.Windows.Forms;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.Controls;
using LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Mmc;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	public class NamespacesNode
		:	ControlNode
	{
		public const string Name = "Namespaces";

		public NamespacesNode(Snapin snapin, Catalogue catalogue)
			:	base(snapin, typeof(EventStatusEditor).GUID)
		{
			// Properties

			DisplayName = Name;
			m_catalogue = catalogue;
		}

		#region Overrides

		protected override void AddMenuItems()
		{
			// Add appropriate items.

			AddNewMenuItem(new ContextMenuItem("Namespace", "New Namespace", new MenuCommandHandler(MenuNewNamespaceHandler)));

			// Create the Export menu.

			AddExportMenuItems(m_catalogue.Namespaces, m_catalogue);
		}

		protected override bool HasChildren()
		{
			using (new LongRunningMonitor(Snapin))
			{
				return m_catalogue.Namespaces.Count > 0;
			}
		}

		protected override SnapinNode[] GetChildNodes()
		{
			// Iterate through namespaces.

			SnapinNode[] nodes = new SnapinNode[m_catalogue.Namespaces.Count];
			int index = 0;

			foreach ( Namespace ns in m_catalogue.Namespaces )
			{
				nodes[index++] = new NamespaceNode(Snapin, ns);
			}

			return nodes;
		}

		public override bool IsReadOnly
		{
			get { return m_catalogue.IsReadOnly; }
		}

		protected override void Show()
		{
			Debug.Assert(Control != null, "The OCX control for the node is null.");

			EventStatusEditor editor = Control as EventStatusEditor;
			if (editor == null)
			{
				throw new System.ApplicationException("The OCX control is not a '"
					+ typeof(EventStatusEditor).FullName + "' control. This may happen when the '"
					+ typeof(EventStatusEditor).Assembly.FullName + "' assembly fails to register for COM interop.");
			}

			using (new LongRunningMonitor(Snapin))
			{
				editor.ReadOnly = IsReadOnly;
				editor.DisplayEventStatus(m_catalogue);
			}
		}

		#endregion

		#region Handlers

		protected void MenuNewNamespaceHandler(object sender, SnapinNode node)
		{
			// Show the form.

			NewNamespaceWizard wizard = new NewNamespaceWizard(m_catalogue);
			if ( wizard.Show() != DialogResult.OK )
				return;

			try
			{
				// Add the element.

				Namespace newNamespace = null;
				bool cont = true;
				while ( cont )
				{
					newNamespace = wizard.Namespace;
					try
					{
						m_catalogue.Add(newNamespace);
					}
					catch ( System.Exception e )
					{
						new ExceptionDialog(e, "Cannot add the namespace.").ShowDialog();

						// Show it again.

						if ( wizard.Show(newNamespace) != DialogResult.OK )
							return;
						cont = true;
						continue;
					}

					m_catalogue.Commit();
					cont = false;
				}

				// Update the display.

				NamespaceNode newNode = new NamespaceNode(Snapin, newNamespace);
				InsertChildNode(newNode);
				newNode.Select();
				newNode.Refresh(false);
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The following exception has occurred:").ShowDialog();
			}
		}

		#endregion

		private Catalogue m_catalogue;
	}
}
