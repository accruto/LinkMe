using System;
using System.Windows.Forms;

using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueBrowser;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards
{
	/// <summary>
	/// Summary description for NewComponentWizard.
	/// </summary>
	public class NewNamespaceWizard
	{
		public NewNamespaceWizard(INamespaceParent parent)
		{
			m_parent = parent;
			m_namespace = null;
		}

		public DialogResult Show()
		{
			// Create a new Namespace.

			m_namespace = m_parent.Catalogue.CreateNamespace(m_parent, "NewNamespace");

			// Show the properties.

			NamespacePropertyForm propertyForm = new NamespacePropertyForm(m_namespace, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_namespace = propertyForm.Object as Namespace;

			return result;
		}

		public DialogResult Show(Namespace ns)
		{
			// Set the namespace.

			m_namespace = ns;

			// Show the properties.

			NamespacePropertyForm propertyForm = new NamespacePropertyForm(m_namespace, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_namespace = propertyForm.Object as Namespace;

			return result;
		}

		public Namespace Namespace
		{
			get { return m_namespace.Clone(m_parent, m_namespace.Name); }
		}

		private INamespaceParent m_parent;
		private Namespace m_namespace;
	}
}
