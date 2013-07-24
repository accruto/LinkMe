using System.Windows.Forms;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;
using LinkMe.Framework.Instrumentation.Tools.Mmc.Wizards;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.ObjectProperties;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
	/// <summary>
	/// Summary description for EventNode
	/// </summary>
	public class EventTypesNode
		:	Node
	{
		public const string Name = "Events";

		public EventTypesNode(Snapin snapin, Catalogue catalogue)
			:	base(snapin)
		{
			// Properties

			DisplayName = Name;
			m_catalogue = catalogue;

			// Result images.

			AddResultImage(IconManager.GetResourceName(Icons.EventEnabled));
			AddResultImage(IconManager.GetResourceName(Icons.EventEnabled, IconMask.ReadOnly));
			AddResultImage(IconManager.GetResourceName(Icons.EventDisabled));
			AddResultImage(IconManager.GetResourceName(Icons.EventDisabled, IconMask.ReadOnly));
		}

		#region Overrides

		protected override string GetSmallResultImage(string name)
		{
			return GetSmallResultImage(m_catalogue.EventTypes[name]);
		}

		private string GetSmallResultImage(EventType eventType)
		{
			if ( eventType.IsEnabled )
				return IconManager.GetResourceName(Icons.EventEnabled, eventType.IsReadOnly);
			else
				return IconManager.GetResourceName(Icons.EventDisabled, eventType.IsReadOnly);
		}

		protected override void AddMenuItems()
		{
			// New menu items.

            AddNewMenuItem(new ContextMenuItem("Event", "New Event", new MenuCommandHandler(MenuNewEventTypeHandler)));

            // Create the Export menu.

            AddExportMenuItems(m_catalogue.EventTypes, m_catalogue);
		}

		protected override bool HasChildren()
		{
			return false;
		}

		protected override void AddColumns()
		{
			AddColumn("Name", 250);
		}

		protected override void AddResults()
		{
			// Iterate through events.

			foreach ( EventType eventType in m_catalogue.EventTypes )
			{
                string image = GetSmallResultImage(eventType);
				AddResult(image, image, eventType, eventType.Name);
			}
		}

		protected override void AddResultMenuItems()
		{
			bool enable = false;
			bool disable = false;
			object[] datas = GetSelectedResultDatas();

			foreach (EventType eventType in datas)
			{
				if ( !eventType.IsReadOnly )
				{
					if ( eventType.IsEnabled )
					{
						disable = true;
						if (enable)
							break;
					}
					else
					{
						enable = true;
						if (disable)
							break;
					}
				}
			}

			if (enable)
			{
				AddTopMenuItem(new ContextMenuItem("Enable", "Enable", new MenuCommandHandler(MenuEnableEventHandler)));
			}
			if (disable)
			{
				AddTopMenuItem(new ContextMenuItem("Disable", "Disable", new MenuCommandHandler(MenuDisableEventHandler)));
			}

			// Create the Export menu.

			AddExportMenuItems(datas, m_catalogue);
		}

		protected override void EnableResultVerbs()
		{
			object[] datas = GetSelectedResultDatas();

			// Enable properties when a single Event is selected. Enable Rename when a single event is selected
			// and it's not read-only.

			if (datas.Length == 1)
			{
				EnableProperties();

				EventType eventType = datas[0] as EventType;
				if (eventType != null && !eventType.IsReadOnly)
				{
					EnableRename();
				}
			}

			// Enable delete if ALL of the selected events can be deleted. This is contrary to the
			// documented MMC standard, but it's more intuitive (Windows Explorer behaves this way).

			bool delete = true;
			foreach (object data in datas)
			{
				EventType eventType = data as EventType;
				if (eventType.IsReadOnly)
				{
					delete = false;
					break;
				}
			}

			if (delete)
			{
				EnableDelete();
			}
		}

		protected override void DeleteResult(object data)
		{
			// Delete a child.

			EventType eventType = (EventType) data;
			m_catalogue.Remove(eventType);
			try
			{
				m_catalogue.Commit();
			}
			catch ( System.Exception )
			{
				// If there is a problem then put it back.

				m_catalogue.Add(eventType);
				throw;
			}
		}

		protected override bool RenameResult(ref object data, string newName)
		{
			try
			{
				EventType eventType = data as EventType;
				if ( eventType != null )
				{
					// Create the new event group.

					EventType newEventType = eventType.Clone(m_catalogue, newName);
					m_catalogue.Replace(eventType, newEventType);
					m_catalogue.Commit();
					data = newEventType;
					return true;
				}

				return false;
			}
			catch ( System.Exception e )
			{
				new ExceptionDialog(e, "Cannot rename the element to '" + newName + "'.").ShowDialog();
				return false;
			}
		}

		protected override ObjectPropertyForm CreateResultPropertyForm(object data)
		{
			EventType eventType = (EventType) data;
			return new EventTypePropertyForm(eventType, eventType.IsReadOnly);
		}

		protected override void ApplyResultProperties(object element)
		{
			EventType eventType = element as EventType;
			ApplyProperties(GetResultData(eventType.Name) as EventType, eventType);
		}

		protected override bool EnableMultiSelect
		{
			get { return true; }
		}

		public override bool IsReadOnly
		{
			get { return m_catalogue.IsReadOnly; }
		}

		#endregion

		#region Handlers

		protected void MenuNewEventTypeHandler(object sender, SnapinNode node)
		{
			// Show the form.

			NewEventTypeWizard wizard = new NewEventTypeWizard(m_catalogue);
			if ( wizard.Show() != DialogResult.OK )
				return;

			try
			{
				// Add the element.

				EventType newEventType = null;
				bool cont = true;
				while ( cont )
				{
					newEventType = wizard.EventType;
					try
					{
						m_catalogue.Add(newEventType);
					}
					catch ( System.Exception e )
					{
						new ExceptionDialog(e, "Cannot add the event group.").ShowDialog();

						// Show it again.

						if ( wizard.Show(newEventType) != DialogResult.OK )
							return;
						cont = true;
						continue;
					}

					m_catalogue.Commit();
					cont = false;
				}

				// Update the display.

				Refresh(false);
			}
			catch ( System.Exception ex )
			{
				new ExceptionDialog(ex, "The following exception has occurred:").ShowDialog();
			}
		}

		protected void MenuEnableEventHandler(object sender, SnapinNode node)
		{
			SetIsEnabled(true);
		}

		protected void MenuDisableEventHandler(object sender, SnapinNode node)
		{
			SetIsEnabled(false);
		}

		private void SetIsEnabled(bool isEnabled)
		{
			using ( new LongRunningMonitor(Snapin) )
			{
				foreach (object data in GetSelectedResultDatas())
				{
					EventType eventType = data as EventType;
					if (eventType != null)
					{
						// Update.

						eventType.IsEnabled = isEnabled;
						eventType.Commit();
					}
				}

				// Refresh the display.

				UpdateSelectedResultImages();
			}
		}

		#endregion

		private Catalogue m_catalogue;
	}
}
