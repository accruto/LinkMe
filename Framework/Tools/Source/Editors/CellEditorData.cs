using System;

using LinkMe.Framework.Tools;
using LinkMe.Framework.Tools.Net;

namespace LinkMe.Framework.Tools.Editors
{
	internal class CellEditorData : MarshalByRefObject
	{
		private MemberWrapper m_wrapper;
		private bool m_isExpanded = false;
		// If m_children is null then we haven't tried to get the children. If it's a collection with 0 items then
		// we tried to get them and there are none.
		private MemberWrappers m_children = null;
		private int m_level = 0;
		private CellEditorData m_parent = null;
		private bool m_isLastInLevel = false;
		private Type m_editorType = null;
		private bool m_editorCached = false;

		internal CellEditorData(MemberWrapper wrapper)
		{
			m_wrapper = wrapper;
		}

		internal CellEditorData(MemberWrapper wrapper, int level, CellEditorData parent)
		{
			m_wrapper = wrapper;
			m_level = level;
			m_parent = parent;
		}

		public bool IsExpanded
		{
			get { return m_isExpanded; }
			set { m_isExpanded = value; }
		}

		public MemberWrapper Wrapper
		{
			get { return m_wrapper; }
		}

		public MemberWrappers Children
		{
			get { return m_children; }
			set { m_children = value; }
		}

		public bool HasNoChildren
		{
			get { return (m_children != null && m_children.Count == 0); }
		}

		public int Level
		{
			get { return m_level; }
		}

		public CellEditorData Parent
		{
			get { return m_parent; }
		}

		public bool IsLastInLevel
		{
			get { return m_isLastInLevel; }
			set { m_isLastInLevel = value; }
		}

		public int TotalExpandedRows
		{
			get
			{
				if (Children == null)
					return 0;
				else
				{
					int count = Children.Count;

					foreach (MemberWrapper child in Children)
					{
						CellEditorData childData = (CellEditorData)child.Tag;

						if (childData != null && childData.IsExpanded)
						{
							count += childData.TotalExpandedRows;
						}
					}

					return count;
				}
			}
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public Type GetEditorType(EditorManager manager)
		{
			if (manager == null)
				return null;

			if (!m_editorCached)
			{
				try
				{
					Type editAsType = m_wrapper.ValueIsDerivedFromAny(manager.GetAvailableTypes());
					m_editorType = (editAsType == null ? null : manager.GetEditorTypeForValueType(editAsType, false));
				}
				catch (System.Exception)
				{
					m_editorType = null;
				}

				m_editorCached = true;
			}

			return m_editorType;
		}
	}
}
