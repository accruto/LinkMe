/*using System.Windows.Forms;

using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards
{
	public class NewMessageHandlerTypeWizard
	{
		public NewMessageHandlerTypeWizard(Catalogue catalogue)
		{
			m_catalogue = catalogue;
			m_messageHandlerType = null;
		}

		public DialogResult Show()
		{
			// Create a new MessageHandlerType.

			m_messageHandlerType = m_catalogue.CreateMessageHandlerType(m_catalogue, "NewMessageHandlerType");

			// Show the properties.

			MessageHandlerTypePropertyForm propertyForm = new MessageHandlerTypePropertyForm(m_messageHandlerType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_messageHandlerType = propertyForm.Object as MessageHandlerType;

			return result;
		}

		public DialogResult Show(MessageHandlerType messageHandlerType)
		{
			// Set the messageHandlerType.

			m_messageHandlerType = messageHandlerType;

			// Show the properties.

			MessageHandlerTypePropertyForm propertyForm = new MessageHandlerTypePropertyForm(m_messageHandlerType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_messageHandlerType = propertyForm.Object as MessageHandlerType;

			return result;
		}

		public MessageHandlerType MessageHandlerType
		{
			get { return m_messageHandlerType.Clone(m_catalogue, m_messageHandlerType.Name); }
		}

		private Catalogue m_catalogue;
		private MessageHandlerType m_messageHandlerType;
	}
}
*/