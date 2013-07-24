using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.ObjectProperties;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;
using LinkMe.Framework.Instrumentation.Tools.Controls;
using LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards;

using ConfigurationIcons = LinkMe.Framework.Configuration.Tools.Icons;
using ConfigurationIconManager = LinkMe.Framework.Configuration.Tools.IconManager;
using ConfigurationIconMask = LinkMe.Framework.Configuration.Tools.IconMask;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	public class NamespaceNode
		:	ControlNode
	{
		public NamespaceNode(Snapin snapin, Namespace ns)
			:	base(snapin, typeof(EventStatusEditor).GUID)
		{
			// Properties

			DisplayName = ns.Name;
			m_namespace = ns;

			// Images.

			AddImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Namespace));
		}

		#region Overrides

		protected override string GetImage()
		{
			return ConfigurationIconManager.GetResourceName(ConfigurationIcons.Namespace);
		}

		protected override bool HasChildren()
		{
			using (new LongRunningMonitor(Snapin))
			{
				return (m_namespace.Namespaces.Count > 0 || m_namespace.Sources.Count > 0);
			}
		}

		protected override SnapinNode[] GetChildNodes()
		{
			// Iterate.

			SnapinNode[] nodes = new SnapinNode[m_namespace.Namespaces.Count + m_namespace.Sources.Count];
			int index = 0;

			foreach ( Namespace ns in m_namespace.Namespaces )
			{
				nodes[index++] = new NamespaceNode(Snapin, ns);
			}

			foreach ( Source source in m_namespace.Sources )
			{
				nodes[index++] = new SourceNode(Snapin, source);
			}

			return nodes;
		}

		protected override void AddMenuItems()
		{
			// Add appropriate items.

			AddNewMenuItem(new ContextMenuItem("Namespace", "New Namespace", new MenuCommandHandler(MenuNewNamespaceHandler)));
			AddNewMenuItem(new ContextMenuItem("Source", "New Source", new MenuCommandHandler(MenuNewSourceHandler)));

			// Create the Export menu.

			AddExportMenuItems(m_namespace, m_namespace.Catalogue);
		}

		protected override void EnableVerbs()
		{
			if ( !m_namespace.Catalogue.IsReadOnly )
			{
				EnableDelete();
			}

			EnableProperties();
		}

		protected override void Delete()
		{
			// Delete the namespace.

			m_namespace.Parent.Remove(m_namespace, true);
			try
			{
				m_namespace.Parent.Commit();
			}
			catch ( System.Exception )
			{
				// If there is a problem then put it back.

				m_namespace.Parent.Add(m_namespace, true);
				throw;
			}
		}

		public override bool IsReadOnly
		{
			get { return m_namespace.Catalogue.IsReadOnly; }
		}

		protected override ObjectPropertyForm CreatePropertyForm()
		{
			return new NamespacePropertyForm(m_namespace, IsReadOnly);
		}

		protected override void ApplyProperties(object obj)
		{
			ApplyProperties(m_namespace, obj as Namespace);
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
				editor.DisplayEventStatus(m_namespace);
			}
		}

		public override string GetStatusText()
		{
			return "Namespace: " + m_namespace.FullName;
		}

		#endregion

		#region Handlers

		protected void MenuNewNamespaceHandler(object sender, SnapinNode node)
		{
			// Show the form.

			NewNamespaceWizard wizard = new NewNamespaceWizard(m_namespace);
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
						m_namespace.Add(newNamespace, true);
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

					m_namespace.Commit();
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

		protected void MenuNewSourceHandler(object sender, SnapinNode node)
		{
			// Show the wizard.

			NewSourceWizard wizard = new NewSourceWizard(m_namespace);
			if ( wizard.Show() != DialogResult.OK )
				return;

			try
			{
				// Add the element.

				Source newSource = null;
				bool cont = true;
				while ( cont )
				{
					newSource = wizard.Source;
					try
					{
						m_namespace.Add(newSource, true);
					}
					catch ( System.Exception e )
					{
						new ExceptionDialog(e, "Cannot add the source.").ShowDialog();

						// Show it again.

						if ( wizard.Show(newSource) != DialogResult.OK )
							return;
						cont = true;
						continue;
					}

					m_namespace.Commit();
					cont = false;
				}

				// Update the display.

				SourceNode newNode = new SourceNode(Snapin, newSource);
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

		private const string m_stateKeyNamespacesCount = "InstrumentationNamespacesCount";

		private Namespace m_namespace;
	}
}
