/*using System.Windows.Forms;

using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards
{
	public class NewMessageReaderTypeWizard
	{
		public NewMessageReaderTypeWizard(Catalogue catalogue)
		{
			m_catalogue = catalogue;
			m_messageReaderType = null;
		}

		public DialogResult Show()
		{
			// Create a new MessageReaderType.

			m_messageReaderType = m_catalogue.CreateMessageReaderType(m_catalogue, "NewMessageReaderType");

			// Show the properties.

			MessageReaderTypePropertyForm propertyForm = new MessageReaderTypePropertyForm(m_messageReaderType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_messageReaderType = propertyForm.Object as MessageReaderType;

			return result;
		}

		public DialogResult Show(MessageReaderType messageReaderType)
		{
			// Set the messageReaderType.

			m_messageReaderType = messageReaderType;

			// Show the properties.

			MessageReaderTypePropertyForm propertyForm = new MessageReaderTypePropertyForm(m_messageReaderType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_messageReaderType = propertyForm.Object as MessageReaderType;

			return result;
		}

		public MessageReaderType MessageReaderType
		{
			get { return m_messageReaderType.Clone(m_catalogue, m_messageReaderType.Name); }
		}

		private Catalogue m_catalogue;
		private MessageReaderType m_messageReaderType;
	}
}
*/