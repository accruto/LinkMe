using System.Windows.Forms;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards
{
	public class NewEventTypeWizard
	{
		public NewEventTypeWizard(Catalogue catalogue)
		{
			m_catalogue = catalogue;
			m_eventType = null;
		}

		public DialogResult Show()
		{
			// Create a new Event.

			m_eventType = m_catalogue.CreateEventType(m_catalogue, "NewEvent");

			// Show the properties.

			EventTypePropertyForm propertyForm = new EventTypePropertyForm(m_eventType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_eventType = propertyForm.Object as EventType;

			return result;
		}

		public DialogResult Show(EventType eventType)
		{
			// Set the event.

			m_eventType = eventType;

			// Show the properties.

			EventTypePropertyForm propertyForm = new EventTypePropertyForm(m_eventType, false);
			DialogResult result = propertyForm.ShowNew();
			if ( result == DialogResult.OK )
				m_eventType = propertyForm.Object as EventType;

			return result;
		}

		public EventType EventType
		{
			get { return m_eventType.Clone(m_catalogue, m_eventType.Name); }
		}

		private Catalogue m_catalogue;
		private EventType m_eventType;
	}
}
