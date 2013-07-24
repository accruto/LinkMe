using System;
using System.Windows.Forms;

using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueBrowser;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards
{
	public class NewSourceWizard
	{
		public NewSourceWizard(ISourceParent parent)
		{
			m_parent = parent;
			m_source = null;
		}

		public DialogResult Show()
		{
			// Create a new Source.

			m_source = m_parent.Catalogue.CreateSource(m_parent, "NewSource", null);

			// Show the properties.

			SourcePropertyForm propertyForm = new SourcePropertyForm(m_source, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_source = propertyForm.Object as Source;

			return result;
		}

		public DialogResult Show(Source source)
		{
			// Set the source.

			m_source = source;

			// Show the properties.

			SourcePropertyForm propertyForm = new SourcePropertyForm(m_source, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_source = propertyForm.Object as Source;

			return result;
		}

		public Source Source
		{
			get { return m_source.Clone(m_parent, m_source.Name, m_source.Version); }
		}

		private ISourceParent m_parent;
		private Source m_source;
	}
}
