using System;
using System.Collections;
using System.Diagnostics;

using LinkMe.Framework.Tools.Mmc;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.ObjectProperties;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Tools.CatalogueProperties;
using LinkMe.Framework.Instrumentation.Tools.Controls;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc.Nodes
{
 	public class SourceNode
		:	ControlNode
	{
		public SourceNode(Snapin snapin, Source source)
			:	base(snapin, typeof(EventStatusEditor).GUID)
		{
			// Properties

			DisplayName = source.RelativeQualifiedReference;
			m_source = source;

			// Images.

			AddImage(IconManager.GetResourceName(Icons.Source));
			AddImage(IconManager.GetResourceName(Icons.Source, IconMask.ReadOnly));
		}

		public void Reset(Source source)
		{
			m_source = source;
		}

		#region Overrides

		protected override string GetImage()
		{
			return IconManager.GetResourceName(Icons.Source, m_source.IsReadOnly);
		}

		protected override bool HasChildren()
		{
			return false;
		}

		protected override void AddMenuItems()
		{
			// Create the Export menu.

			AddExportMenuItems(m_source, m_source.Catalogue);
		}

		protected override void EnableVerbs()
		{
			if ( !m_source.IsReadOnly )
			{
				EnableDelete();
				EnableRename();
			}

			EnableProperties();
		}

		protected override void Delete()
		{
			// Delete the source.

			m_source.Parent.Remove(m_source, true);
			try
			{
				m_source.Parent.Commit();
			}
			catch ( System.Exception )
			{
				// If there is a problem then put it back.

				m_source.Parent.Add(m_source, true);
				throw;
			}
		}

		protected override bool Rename(string newName)
		{
			try
			{
				// Extract the name and version.

				CatalogueName catalogueName = new CatalogueName(newName);

				// Create the new source.

				Source newSource = m_source.Clone(m_source.Parent, catalogueName.Name, catalogueName.Version);
				// Don't update parent state in this case, since only the name is being changed.
				m_source.Parent.Replace(m_source, newSource, false); 
				m_source.Parent.Commit();

				m_source = newSource;
				return true;
			}
			catch ( System.Exception e )
			{
				new ExceptionDialog(e, "Cannot rename the source to '" + newName + "'.").ShowDialog();
				return false;
			}
		}

		public override bool IsReadOnly
		{
			get { return m_source.IsReadOnly; }
		}

		protected override ObjectPropertyForm CreatePropertyForm()
		{
			return new SourcePropertyForm(m_source, IsReadOnly);
		}

		protected override void ApplyProperties(object element)
		{
			ApplyProperties(m_source, element as Source);
		}

		protected override void Show()
		{
			Debug.Assert(Control != null, "The OCX control for the node is null.");
			Debug.Assert(Control is EventStatusEditor, "The OCX control is not an EventStatusEditor.");

			using (new LongRunningMonitor(Snapin))
			{
				EventStatusEditor editor = (EventStatusEditor)Control;
				editor.ReadOnly = IsReadOnly;
				editor.DisplayEventStatus(m_source);
			}
		}

		public override string GetStatusText()
		{
			return "Source: " + m_source.FullyQualifiedReference;
		}

		#endregion

		private Source m_source;
	}
}
