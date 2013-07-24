using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.ObjectProperties;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Configuration.Extension;
using LinkMe.Framework.Configuration.Management;
using LinkMe.Framework.Configuration.Tools.CatalogueProperties;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

using Catalogue = LinkMe.Framework.Instrumentation.Management.Catalogue;
using ConfigurationNode = LinkMe.Framework.Configuration.Tools.Mmc.Nodes.Node;
using ConfigurationIcons = LinkMe.Framework.Configuration.Tools.Icons;
using ConfigurationIconManager = LinkMe.Framework.Configuration.Tools.IconManager;
using ConfigurationIconMask = LinkMe.Framework.Configuration.Tools.IconMask;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	public class RepositoryNode
		:	Node
	{
		public RepositoryNode(Snapin snapin, Repository repository, IMmcRepositoryExtensionCallback callback)
			:	base(snapin)
		{
			// Properties

			DisplayName = repository.Name;
			m_repository = repository;
			m_callback = callback;

			// Images.

			AddImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Repository));
			AddImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Repository, ConfigurationIconMask.ReadOnly));
			AddImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Repository, ConfigurationIconMask.Disconnected));
			AddImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Repository, ConfigurationIconMask.Disconnected | ConfigurationIconMask.ReadOnly));

			AddResultImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Catalogue));
			AddResultImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Catalogue, ConfigurationIconMask.Large));
		}

		public void Update(Repository repository)
		{
			m_repository = repository;
		}

		public bool IsConnected
		{
			get { return m_catalogue != null; }
		}

		public bool CanConnect
		{
			get { return m_canConnect; }
			set { m_canConnect = value; }
		}

		public void Connect()
		{
			try
			{
				using (new LongRunningMonitor(Snapin))
				{
					ConnectInternal();

					// Update the display.

					Refresh(true);
				}
			}
			catch ( System.Exception e )
			{
				new ExceptionDialog(e, "Cannot connect to the '" + m_repository.Name + "' repository:").ShowDialog();
				Disconnect();
			}
		}

		public void Disconnect()
		{
			// Throw away the information.

			m_repositoryLink = null;
			m_catalogue = null;

			// Update the display.

			Refresh(true);
		}

		public void CreateRepository()
		{
			EnsureRepositoryLink();
			Debug.Assert(!m_repository.IsReadOnly, "!m_repository.IsReadOnly");

			IRepositoryBuilder builder = m_repositoryLink as IRepositoryBuilder;
			if (builder == null)
				throw new System.NotSupportedException();

			((Configuration.Tools.Mmc.Snapin) Snapin).CreateRepository(builder, Instrumentation.Management.Constants.Module.DisplayName);
		}

		public void DeleteRepository()
		{
			EnsureRepositoryLink();
			Debug.Assert(!m_repository.IsReadOnly, "!m_repository.IsReadOnly");

			IRepositoryBuilder builder = m_repositoryLink as IRepositoryBuilder;
			if (builder == null)
				throw new System.NotSupportedException();

			((Configuration.Tools.Mmc.Snapin) Snapin).DeleteRepository(builder, Instrumentation.Management.Constants.Module.DisplayName);
		}

		#region Overrides

		protected override string GetImage()
		{
			if ( m_catalogue == null )
				return ConfigurationIconManager.GetResourceName(ConfigurationIcons.Repository, IsReadOnly, ConfigurationIconMask.Disconnected);
			else
				return ConfigurationIconManager.GetResourceName(ConfigurationIcons.Repository, IsReadOnly);
		}

		protected override void EnableVerbs()
		{
			if ( m_catalogue == null )
			{
				EnableDelete();
				EnableRename();
			}

			EnableProperties();
		}

		protected override void AddMenuItems()
		{
			AddTaskMenuItemAndChildren(ConfigurationNode.GetExportToSubMenu(Snapin, new object[] { m_repository }, m_repository.Catalogue));

			// Determine whether or not this repository has been connected to.

			if ( m_catalogue == null )
			{
				AddTopMenuItem(new ContextMenuItem("Connect", "Connects to the repository.", new MenuCommandHandler(MenuConnectHandler)));

				// Add Create and Delete commands for the repository, if supported.

				if (IsRepositoryBuilder)
				{
					AddTaskSeparatorMenuItem();
					AddTaskMenuItem(new ContextMenuItem("Create Repository", "Creates a new repository and populates it with default framework configuration elements.", new MenuCommandHandler(MenuCreateRepositoryHandler)));
					AddTaskMenuItem(new ContextMenuItem("Delete Repository", "Deletes the repository. All repository contents are lost.", new MenuCommandHandler(MenuDeleteRepositoryHandler)));
				}
			}
			else
			{
				AddTopMenuItem(new ContextMenuItem("Disconnect", "Disconnects from the repository.", new MenuCommandHandler(MenuDisconnectHandler)));
			}
		}

		protected override void AddResultMenuItems()
		{
			// Determine whether or not this repository has been connected to.

			if ( m_catalogue != null )
			{
				// Create the Export menu.

				AddExportMenuItems(GetSelectedResultDatas(), m_catalogue);
			}
		}

		protected override void AddColumns()
		{
			if ( m_catalogue != null )
				AddColumn("Name", 150);
		}

		protected override void AddResults()
		{
			if ( m_catalogue != null )
				AddResult(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Catalogue), ConfigurationIconManager.GetResourceName(ConfigurationIcons.Catalogue, ConfigurationIconMask.Large), CatalogueNode.Name);
		}

		protected override SnapinNode[] GetChildNodes()
		{
			return (m_catalogue == null ? null : new SnapinNode[] { new CatalogueNode(Snapin, m_catalogue) });
		}

		protected override void Delete()
		{
			m_callback.Delete();
		}

		protected override bool Rename(string newName)
		{
			return m_callback.Rename(newName);
		}

		protected override ObjectPropertyForm CreatePropertyForm()
		{
			return m_callback.CreatePropertyForm(m_catalogue != null);
		}

		protected override void ApplyProperties(object obj)
		{
			m_callback.Apply(this, obj as Repository);
		}

		protected override void Expand(bool duringStartup)
		{
			// If the node is being expanded before the configuration is connected then connect.

			if ( m_canConnect && m_repositoryLink == null && Control.ModifierKeys != Keys.Shift )
			{
				// Only show the form during start up.

				bool doConnect = true;
				if ( duringStartup )
				{
					RepositoryPropertyForm form = new RepositoryPropertyForm("Connect to Instrumentation Repository", m_repository);
					if ( form.ShowPrompt() != DialogResult.OK )
						doConnect = false;
				}

				if ( doConnect )
					ConnectInternal();
			}
		}

		public override bool IsReadOnly
		{
			get { return m_repository.IsReadOnly; }
		}

		public override string GetStatusText()
		{
			return "Repository: " + m_repository.Name;
		}

		#endregion

		public bool IsRepositoryBuilder
		{
			get
			{
				EnsureRepositoryLink();
				return (!m_repository.IsReadOnly && m_repositoryLink is IRepositoryBuilder);
			}
		}

		#region Handlers

		private void MenuConnectHandler(object sender, SnapinNode node)
		{
			Connect();
		}

		private void MenuDisconnectHandler(object sender, SnapinNode node)
		{
			Disconnect();
		}

		private void MenuCreateRepositoryHandler(object sender, SnapinNode node)
		{
			CreateRepository();
		}

		private void MenuDeleteRepositoryHandler(object sender, SnapinNode node)
		{
			DeleteRepository();
		}

		#endregion

		private void EnsureRepositoryLink()
		{
			// Get the catalogue for this repository.

			m_repositoryLink = m_repository.CreateRepositoryLink();
		}

		private void ConnectInternal()
		{
			EnsureRepositoryLink();

            IRepositoryReader reader = m_repositoryLink.GetConnection<IRepositoryReader>();
            if (reader != null)
            {
                using (ConnectionState state = new ConnectionState())
                {
                    m_catalogue = reader.Read(state);
                }
            }
		}

		private Repository m_repository;
		private IRepositoryLink m_repositoryLink;
		private Catalogue m_catalogue;
		private IMmcRepositoryExtensionCallback m_callback;
		private bool m_canConnect = true;
	}
}
