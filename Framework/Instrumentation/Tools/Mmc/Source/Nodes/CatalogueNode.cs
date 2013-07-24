using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.ObjectProperties;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

using Catalogue = LinkMe.Framework.Instrumentation.Management.Catalogue;
using ConfigurationIcons = LinkMe.Framework.Configuration.Tools.Icons;
using ConfigurationIconManager = LinkMe.Framework.Configuration.Tools.IconManager;
using ConfigurationIconMask = LinkMe.Framework.Configuration.Tools.IconMask;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	/// <summary>
	/// Summary description for CatalogueNode
	/// </summary>
	public class CatalogueNode
		:	Node
	{
		public const string Name = "Catalogue";

		public CatalogueNode(Snapin snapin, Catalogue catalogue)
			:	base(snapin)
		{
			// Properties

			DisplayName = Name;
			_catalogue = catalogue;

			// Images.

			AddImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Catalogue));

			AddResultImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Folder));
			AddResultImage(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Folder, ConfigurationIconMask.Large));
		}

		#region Overrides

		protected override string GetImage()
		{
			return ConfigurationIconManager.GetResourceName(ConfigurationIcons.Catalogue);
		}

		protected override void AddMenuItems()
		{
			// Tasks.

			if ( !_catalogue.IsReadOnly )
				AddImportMenuItems(_catalogue);
			AddExportMenuItems(_catalogue, _catalogue);
			AddTaskSeparatorMenuItem();
			AddTaskMenuItem(new ContextMenuItem("Manage Types...", "Manage the module types.", new MenuCommandHandler(MenuManageTypesHandler)));
		}

		protected override void AddResultMenuItems()
		{
			// Create the Export menu.

			AddExportMenuItems(GetSelectedResultDatas(), _catalogue);
		}

		protected override void AddColumns()
		{
			AddColumn("Name", 150);
		}

		protected override void AddResults()
		{
			AddResult(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Folder), ConfigurationIconManager.GetResourceName(ConfigurationIcons.Folder, ConfigurationIconMask.Large), _catalogue.Namespaces, NamespacesNode.Name);
			AddResult(ConfigurationIconManager.GetResourceName(ConfigurationIcons.Folder), ConfigurationIconManager.GetResourceName(ConfigurationIcons.Folder, ConfigurationIconMask.Large), _catalogue.EventTypes, EventTypesNode.Name);
		}

		protected override SnapinNode[] GetChildNodes()
		{
			return new SnapinNode[] {
				new NamespacesNode(Snapin, _catalogue),
				new EventTypesNode(Snapin, _catalogue) };
		}

		protected override ObjectPropertyForm CreateManageTypesForm()
		{
			return new CatalogueTypesPropertyForm(_catalogue.RepositoryLink.Name, _catalogue, IsReadOnly);
		}

		protected override void ApplyManageTypes(object obj)
		{
			ApplyManageTypes(_catalogue, obj as Catalogue);
		}

		#endregion

		#region Handlers

		private void MenuManageTypesHandler(object sender, SnapinNode arg)
		{
			ManageTypes();
		}

		#endregion

		private readonly Catalogue _catalogue;
	}
}
