using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.ObjectBrowser;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Configuration.Management;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Instrumentation.Tools.CatalogueBrowser;

using CatalogueElements = LinkMe.Framework.Instrumentation.Management.CatalogueElements;
using Catalogue = LinkMe.Framework.Instrumentation.Management.Catalogue;
using Namespace = LinkMe.Framework.Instrumentation.Management.Namespace;
using Namespaces = LinkMe.Framework.Instrumentation.Management.Namespaces;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc
{
	public class ExportRepositoryHandler
	{
		public ExportRepositoryHandler(RepositoryType repositoryType, object[] elements, Catalogue catalogue, IWin32Window parent)
		{
			_repositoryType = repositoryType;
			_elements = elements;
			_catalogue = catalogue;
			_parent = parent;
		}

		public void Export(object item, SnapinNode node)
		{
			try
			{
				// Show the form to select what goes in.

				var form = new CatalogueBrowserForm();
				form.DisplayCatalogue(_catalogue, true, CatalogueElements.All);
				form.Text = "Select Elements";

				// Select the appropriate element.

				SelectElements(form.Browser);

				// Show the dialog.

				if ( form.ShowDialog() == DialogResult.OK )
					WriteElements(form.Browser);
			}
			catch ( System.Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred while trying to write the information:").ShowDialog();
			}
		}

		private void SelectElements(ObjectBrowser browser)
		{
			var manager = (CatalogueBrowserManager) browser.Manager;

			foreach ( object element in _elements )
			{
				// Select the item based on the type of the element.

				if ( element is Catalogue )
				{
					// Catalogue is already the select type so make sure it is checked.

					browser.SelectedTypeChecked = true;
				}
				else if ( element is Namespaces )
				{
					// Need to select and check all the namespaces below this.

					foreach ( Namespace ns in (Namespaces) element )
					{
						browser.SetTypeElementChecked(manager.GetNamespaceInfo(ns), true);
					}
				}
				else if ( element is EventTypes )
				{
					// Need to select and check all the events below this.

					foreach ( EventType eventType in (EventTypes) element )
					{
						browser.SetTypeElementChecked(manager.GetEventTypeInfo(eventType), true);
					}
				}
				else
				{
					// Select the appropriate element.

					if ( element is Namespace )
						browser.SelectedType = manager.GetNamespaceInfo((Namespace) element);
					else if ( element is Source )
						browser.SelectedType = manager.GetSourceInfo((Source) element);
					else if ( element is EventType )
						browser.SelectedType = manager.GetEventTypeInfo((EventType) element);
					else
						Debug.Fail("Unexpected type of selected element: " + element.GetType().FullName);

					browser.SelectedTypeChecked = true;
				}
			}
		}

		private void WriteElements(ObjectBrowser browser)
		{
			// Create a writer from the repository type.

            var writer = _repositoryType.GetRepositoryConnection<IRepositoryWriter>(true, _parent);

			if ( writer != null )
			{
				var elements = new ArrayList();

				// Get the selected elements.

				foreach ( IElementBrowserInfo elementInfo in browser.GetCheckedElements() )
				{
					if ( elementInfo is NamespaceBrowserInfo )
						elements.Add(((NamespaceBrowserInfo) elementInfo).Namespace);
					else if ( elementInfo is SourceBrowserInfo )
						elements.Add(((SourceBrowserInfo) elementInfo).Source);
					else if ( elementInfo is EventTypeBrowserInfo )
						elements.Add(((EventTypeBrowserInfo) elementInfo).EventType);
				}

				// Run it.

				var runner = new ExportRepositoryResultsRunner(writer, elements);
				runner.Run();
			}
		}

		private readonly RepositoryType _repositoryType;
		private readonly object[] _elements;
		private readonly Catalogue _catalogue;
		private readonly IWin32Window _parent;
	}
}
