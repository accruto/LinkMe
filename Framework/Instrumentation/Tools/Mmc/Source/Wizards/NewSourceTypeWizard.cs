/*using System.Windows.Forms;

using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards
{
	public class NewSourceTypeWizard
	{
		public NewSourceTypeWizard(Catalogue catalogue)
		{
			m_catalogue = catalogue;
			m_sourceType = null;
		}

		public DialogResult Show()
		{
			// Create a new SourceType.

			m_sourceType = m_catalogue.CreateSourceType(m_catalogue, "NewSourceType");

			// Show the properties.

			SourceTypePropertyForm propertyForm = new SourceTypePropertyForm(m_sourceType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_sourceType = propertyForm.Object as SourceType;

			return result;
		}

		public DialogResult Show(SourceType sourceType)
		{
			// Set the sourceType.

			m_sourceType = sourceType;

			// Show the properties.

			SourceTypePropertyForm propertyForm = new SourceTypePropertyForm(m_sourceType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_sourceType = propertyForm.Object as SourceType;

			return result;
		}

		public SourceType SourceType
		{
			get { return m_sourceType.Clone(m_catalogue, m_sourceType.Name); }
		}

		private Catalogue m_catalogue;
		private SourceType m_sourceType;
	}
}
*/