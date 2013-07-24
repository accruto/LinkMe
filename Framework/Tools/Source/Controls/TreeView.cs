using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// An enhanced TreeView control.
	/// </summary>
	[ToolboxBitmap(typeof(System.Windows.Forms.TreeView))]
	public class TreeView : System.Windows.Forms.TreeView
	{
		#region Nested Types

		/// <summary>
		/// A collection of the TreeNode objects that are currently selected in a TreeView control. The
		/// collection is sorted so that nodes in it are always in the same order as they appear in the tree.
		/// </summary>
		public class SelectedTreeNodeCollection : IList
		{
			private TreeView m_owner = null;
			private ArrayList m_list = new ArrayList();

			internal SelectedTreeNodeCollection(TreeView owner)
			{
				Debug.Assert(owner != null, "owner != null");
				m_owner = owner;
			}

			#region IList Members

			public bool IsReadOnly
			{
				get { return false; }
			}

			object IList.this[int index]
			{
				get { return m_list[index]; }
				set { throw new NotSupportedException(); }
			}

			public void RemoveAt(int index)
			{
				if (index < 0 || index >= Count)
					throw new ArgumentOutOfRangeException("index");

				RemoveNode(index, this[index]);
			}

			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			void IList.Remove(object value)
			{
				if (!(value is TreeNode))
					throw new ArgumentException("The value must be a TreeNode.", "value");

				Remove((TreeNode)value);
			}

			bool IList.Contains(object value)
			{
				return m_list.Contains(value);
			}

			public void Clear()
			{
				// Raise BeforeSelect without a node, since the selection is changing.

				TreeViewCancelEventArgs args = new TreeViewCancelEventArgs(null, false, TreeViewAction.Unknown);
				m_owner.RaiseBeforeSelect(args);
				if (args.Cancel)
					return;

				m_owner.m_selectionPivot = null;
				InternalClear();
				m_owner.RealSelectedNode = null;
				m_owner.OnSelectionsChanged(EventArgs.Empty);
			}

			int IList.IndexOf(object value)
			{
				return m_list.IndexOf(value);
			}

			int IList.Add(object value)
			{
				if (!(value is TreeNode))
					throw new ArgumentException("The value must be a TreeNode.", "value");

				return Add((TreeNode)value);
			}

			public bool IsFixedSize
			{
				get { return false; }
			}

			#endregion

			#region ICollection Members

			public bool IsSynchronized
			{
				get { return m_list.IsSynchronized; }
			}

			public int Count
			{
				get { return m_list.Count; }
			}

			void ICollection.CopyTo(Array array, int index)
			{
				m_list.CopyTo(array, index);
			}

			public object SyncRoot
			{
				get { return m_list.SyncRoot; }
			}

			#endregion

			#region IEnumerable Members

			public IEnumerator GetEnumerator()
			{
				return m_list.GetEnumerator();
			}

			#endregion

			public TreeNode this[int index]
			{
				get { return (TreeNode)m_list[index]; }
			}

			public int Add(TreeNode node)
			{
				int index = AddWithCheck(node);

				if (m_owner.RealSelectedNode == null)
				{
					m_owner.RealSelectedNode = node;
				}

				m_owner.OnSelectionsChanged(EventArgs.Empty);

				return index;
			}

			public void AddRange(TreeNode[] nodes)
			{
				AddRangeCore(nodes);
			}

			public void AddRange(TreeNodeCollection nodes)
			{
				AddRangeCore(nodes);
			}

			public bool Contains(TreeNode node)
			{
				return m_list.Contains(node);
			}

			public void CopyTo(TreeNode[] array, int index)
			{
				m_list.CopyTo(array, index);
			}

			public int IndexOf(TreeNode node)
			{
				return m_list.IndexOf(node);
			}

			public void Remove(TreeNode node)
			{
				if (node == null)
					throw new ArgumentNullException("node");

				int index = m_list.IndexOf(node);
				if (index == -1)
				{
					throw new ArgumentException("The supplied TreeNode ('" + node.Text + "') cannot be removed"
						+ "  from the selection, because it is not selected.", "node");
				}

				RemoveNode(index, node);
			}

			public void Remove(TreeNode[] nodes)
			{
				if (nodes == null)
					throw new ArgumentNullException("nodes");

				TreeNode changeRealSelectionTo = null;

				foreach (TreeNode node in nodes)
				{
					if (!m_list.Contains(node))
					{
						throw new ArgumentException("One of the supplied TreeNodes ('" + node.Text
							+ "') cannot be removed from the selection, because it is not selected.", "nodes");
					}

					InternalRemove(node);
					m_owner.DeselectNode(node);

					if (m_owner.RealSelectedNode == node || changeRealSelectionTo == node)
					{
						changeRealSelectionTo = (node.PrevVisibleNode != null ? node.PrevVisibleNode
							: node.NextVisibleNode);
					}
				}

				if (changeRealSelectionTo != null && Contains(changeRealSelectionTo))
				{
					m_owner.ChangeRealSelection(changeRealSelectionTo);
				}

				m_owner.OnSelectionsChanged(EventArgs.Empty);
			}

			/// <summary>
			/// Replaces the selection with the supplied nodes, only raising BeforeSelect, AfterSelect
			/// and SelectionsChanged events once.
			/// </summary>
			public void ReplaceWithRange(TreeNode[] nodes)
			{
				ReplaceWithRangeCore(nodes);
			}

			/// <summary>
			/// Replaces the selection with the supplied nodes, only raising BeforeSelect, AfterSelect
			/// and SelectionsChanged events once.
			/// </summary>
			public void ReplaceWithRange(TreeNodeCollection nodes)
			{
				ReplaceWithRangeCore(nodes);
			}

			internal void AddSortedRange(ArrayList collection, TreeNode nodeToFocus)
			{
				Debug.Assert(collection != null && nodeToFocus != null, "collection != null && nodeToFocus != null");
				Debug.Assert(collection.Count > 0, "collection.Count > 0");
				Debug.Assert(collection.Contains(nodeToFocus), "collection.Contains(nodeToFocus)");

				int searchResult = m_list.BinarySearch(collection[0], new TreeNodeComparer());
				Debug.Assert(searchResult < 0, "BinarySearch returned " + searchResult.ToString()
					+ " in AddSortedRange - must be re-adding already selected nodes.");

				// Raise BeforeSelect and give the user a chance to cancel.

				TreeViewCancelEventArgs args = new TreeViewCancelEventArgs(nodeToFocus, false,
					TreeViewAction.Unknown);
				m_owner.RaiseBeforeSelect(args);
				if (args.Cancel)
					return;

				// The items in collection are contiguous and already sorted, so just insert the whole range
				// into the position where the first node would go.

				int index = ~searchResult;
				m_list.InsertRange(index, collection);

				UpdateTreeViewForAddRange(collection, nodeToFocus);
			}

			internal void InternalClear()
			{
				m_owner.RemovePaintFromNodes();
				m_list.Clear();
			}

			internal int InternalAdd(TreeNode node)
			{
				// Insert this node into its correct sorted position (the rest of the list should be sorted).

				int searchResult = m_list.BinarySearch(node, new TreeNodeComparer());
				if (searchResult >= 0)
				{
					throw new ArgumentException(string.Format("TreeNode '{0}' cannot be added to the"
						+ " selection, because it is already selected.", node.Text), "node");
				}

				int index = ~searchResult;
				m_list.Insert(index, node);

				m_owner.PaintSelectedNode(node);

				return index;
			}

			internal void InternalRemove(TreeNode node)
			{
				m_list.Remove(node);
			}

			internal int IndexOfHandle(IntPtr handle)
			{
				Debug.Assert(handle != IntPtr.Zero, "handle != IntPtr.Zero");

				for (int index = 0; index < Count; index++)
				{
					if (this[index].Handle == handle)
						return index;
				}

				return -1;
			}

			private void RemoveNode(int index, TreeNode node)
			{
				TreeNode nextSelection = null;

				if (m_owner.RealSelectedNode == node)
				{
					// The "real" selected node has been unselected. Set the focus to the previous selected node,
					// if any, or else the next selected node. Make sure to raise events in the right order.

					if (index == 0)
					{
						nextSelection = (Count > 1 ? this[1] : null);
					}
					else
					{
						nextSelection = this[index - 1];
					}

					TreeViewCancelEventArgs args = new TreeViewCancelEventArgs(nextSelection, false,
						TreeViewAction.Unknown);
					m_owner.RaiseBeforeSelect(args);
					if (args.Cancel)
						return;
				}

				if (nextSelection != null)
				{
					m_owner.ChangeRealSelection(nextSelection);
				}

				m_list.RemoveAt(index);
				m_owner.DeselectNode(node);

				if (nextSelection != null)
				{
					m_owner.RaiseAfterSelect(new TreeViewEventArgs(nextSelection));
				}

				m_owner.OnSelectionsChanged(EventArgs.Empty);
			}

			private void AddRangeCore(IList nodes)
			{
				if (nodes == null)
					throw new ArgumentNullException("nodes");
				if (nodes.Count == 0)
					return;

				// Raise BeforeSelect for the first node in the list (it probably won't match the one 
				// in AfterSelect, but we have to put in something).

				TreeViewCancelEventArgs args = new TreeViewCancelEventArgs((TreeNode)nodes[0], false,
					TreeViewAction.Unknown);
				m_owner.RaiseBeforeSelect(args);
				if (args.Cancel)
					return;

				foreach (TreeNode node in nodes)
				{
					AddWithCheck(node);
				}

				// Update the TreeView, which should set the "real" selected node and cause AfterSelect to be
				// raised for it. Do not raise BeforeSelect, however, because it would be too late to cancel it
				// now (which is why it was raised manually before).

				UpdateTreeViewForAddRange(nodes, (TreeNode)nodes[0]);
			}

			/// <summary>
			/// Combine Clear() and AddRange(), but only raise BeforeSelect, AfterSelect and SelectionsChangede
			/// events once.
			/// </summary>
			private void ReplaceWithRangeCore(IList nodes)
			{
				if (nodes == null)
					throw new ArgumentNullException("nodes");

				// Raise BeforeSelect without a node, since the selection is changing.

				TreeViewCancelEventArgs args = new TreeViewCancelEventArgs(null, false, TreeViewAction.Unknown);
				m_owner.RaiseBeforeSelect(args);
				if (args.Cancel)
					return;

				// Clear

				m_owner.m_selectionPivot = null;
				InternalClear();

				// Add

				foreach (TreeNode node in nodes)
				{
					AddWithCheck(node);
				}

				// Update the TreeView, which should set the "real" selected node and cause AfterSelect to be
				// raised for it, if nodes is not empty. If it is empty AfterSelect should not be raised,
				// but SelectionsChanged should be.

				UpdateTreeViewForAddRange(nodes, (TreeNode)nodes[0]);
			}

			private int AddWithCheck(TreeNode node)
			{
				if (node == null)
					throw new ArgumentNullException("node");
				if (node.TreeView != m_owner)
				{
					throw new ArgumentException(string.Format("TreeNode '{0}' does not belong to this TreeView.",
						node.Text), "node");
				}

				return InternalAdd(node);
			}

			private void UpdateTreeViewForAddRange(IList collection, TreeNode nodeToFocus)
			{
				m_owner.BeginUpdate();

				try
				{
					foreach (TreeNode node in collection)
					{
						m_owner.PaintSelectedNode(node);
					}
				}
				finally
				{
					m_owner.EndUpdate();
				}

				if (collection.Count > 0)
				{
					if (m_owner.RealSelectedNode != nodeToFocus)
					{
						m_owner.m_selectionProcessing =	SelectionProcessing.IgnoreOnceNoBeforeSelectEvent;
						m_owner.RealSelectedNode = nodeToFocus;
					}
				}
				else if (m_owner.RealSelectedNode != null)
				{
					m_owner.RealSelectedNode = null;
				}

				m_owner.OnSelectionsChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// A read-only collection of TreeNode objects.
		/// </summary>
		public class ReadOnlyTreeNodeCollection : IList
		{
			private ArrayList m_list = new ArrayList();

			internal ReadOnlyTreeNodeCollection(TreeNode node)
			{
				if (node != null)
				{
					m_list.Add(node);
				}
			}

			internal ReadOnlyTreeNodeCollection(ICollection collection)
			{
				m_list.AddRange(collection);
			}

			#region IList Members

			public bool IsReadOnly
			{
				get { return true; }
			}

			object IList.this[int index]
			{
				get { return m_list[index]; }
				set { throw new NotSupportedException(); }
			}

			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			bool IList.Contains(object value)
			{
				return m_list.Contains(value);
			}

			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			int IList.IndexOf(object value)
			{
				return m_list.IndexOf(value);
			}

			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			public bool IsFixedSize
			{
				get { return false; }
			}

			#endregion

			#region ICollection Members

			public bool IsSynchronized
			{
				get { return m_list.IsSynchronized; }
			}

			public int Count
			{
				get { return m_list.Count; }
			}

			void ICollection.CopyTo(Array array, int index)
			{
				m_list.CopyTo(array, index);
			}

			public object SyncRoot
			{
				get { return m_list.SyncRoot; }
			}

			#endregion

			#region IEnumerable Members

			public IEnumerator GetEnumerator()
			{
				return m_list.GetEnumerator();
			}

			#endregion

			public TreeNode this[int index]
			{
				get { return (TreeNode)m_list[index]; }
			}

			public bool Contains(TreeNode node)
			{
				return m_list.Contains(node);
			}

			public void CopyTo(TreeNode[] array, int index)
			{
				m_list.CopyTo(array, index);
			}

			public int IndexOf(TreeNode node)
			{
				return m_list.IndexOf(node);
			}
		}

		private class TreeNodeComparer : IComparer
		{
			internal TreeNodeComparer()
			{
			}

			#region IComparer Members

			public int Compare(object x, object y)
			{
				if (!(x is TreeNode && y is TreeNode))
					throw new ArgumentException("Both arguments must be TreeNode objects.");

				return TreeNodeComparer.Compare((TreeNode)x, (TreeNode)y);
			}

			#endregion

			/// <summary>
			/// Compare two TreeNodes by their order in the TreeView.
			/// </summary>
			internal static int Compare(TreeNode x, TreeNode y)
			{
				if (x.TreeView != y.TreeView)
				{
					throw new ArgumentException("Unable to compare the TreeNode objects, because they are"
						+ " not from the same TreeView.");
				}

				if (x.Parent == y.Parent)
					return x.Index - y.Index; // Shortcut in case the nodes are siblings or the same node.

				// Get arrays of node indices from the root node down for both of the supplied nodes and
				// compare them.

				int[] indicesX = GetIndexArray(x);
				int[] indicesY = GetIndexArray(y);
				int result = 0;

				for (int index = 0; index < indicesX.Length && index < indicesY.Length && result == 0; index++)
				{
					result = (int)indicesX[index] - (int)indicesY[index];
				}

				if (result == 0)
				{
					// The indices are the same up to the end of the shorter array, so one node is a descendent
					// of the other. The descendent is displayed lower down in the tree, so it's considered
					// "greater".

					Debug.Assert(indicesX.Length != indicesY.Length, "Nodes are not the same, but arrays of indices are.");
					return indicesX.Length - indicesY.Length;
				}
				else
					return result;
			}

			/// <summary>
			/// Returns the array of node indices for specified node, starting for the root node.
			/// </summary>
			private static int[] GetIndexArray(TreeNode node)
			{
				Debug.Assert(node != null, "node != null");

				ArrayList list = new ArrayList();

				list.Add(node.Index);
				while (node.Parent != null)
				{
					list.Add(node.Parent.Index);
					node = node.Parent;
				}

				list.Reverse();

				return (int[])list.ToArray(typeof(int));
			}
		}

		private enum SelectionProcessing
		{
			Process, // Process the selection as normal.
			IgnoreOnce, // Don't do our own processing next time.
			IgnoreThenCancel, // Don't do our own processing next time and cancel the one after that.
			IgnoreOnceNoBeforeSelectEvent, // Don't do our own processing next time and don't raise BeforeSelect.
			Cancel, // Cancel the selection.
			SetToThis // Set our selection (the m_selected collection) to be this selection.
		}

		#endregion

		public event EventHandler ContextNodesChanged;
		public event TreeViewEventHandler NodeActivate;
		public event EventHandler SelectionsChanged;

		private const bool m_defaultMultiSelect = false;

		private TreeNode m_clickedNode = null;
		private bool m_multiSelect = m_defaultMultiSelect;
		private SelectedTreeNodeCollection m_selected;
		private TreeNode m_selectionPivot = null; // The "pivot" node about which contiguous selections are processed.
		private SelectionProcessing m_selectionProcessing = SelectionProcessing.Process;
		private bool m_hidingSelection = false;
		private bool m_suppressContextNodesChanged = false;

		public TreeView()
		{
			m_selected = new SelectedTreeNodeCollection(this);
		}

		/// <summary>
		/// The node for which context menu commands should be executed.
		/// </summary>
		/// <remarks>This property returns the the clicked node, unless the user clicks on something other than a
		/// node. In that case it returns the selected node. If MultiSelect is true this property returns only
		/// the first of the context node - you should access the ContextNodes property instead to retrieve
		/// all the context nodes.</remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeNode ContextNode
		{
			get
			{
				// Check the parent in case the clicked node has been deleted since it was stored.

				if (m_clickedNode != null && m_clickedNode.TreeView == this)
					return m_clickedNode;
				else if (MultiSelect)
					return (SelectedNodes.Count == 0 ? null : SelectedNodes[0]);
				else
					return RealSelectedNode;
			}
		}

		/// <summary>
		/// The node for which context menu commands should be executed.
		/// </summary>
		/// <remarks>This property returns the the clicked node, unless the user clicks on something other than a
		/// node. In that case it returns the selected node.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ReadOnlyTreeNodeCollection ContextNodes
		{
			get
			{
				// Check the parent in case the clicked node has been deleted since it was stored.

				if (m_clickedNode != null && m_clickedNode.TreeView == this)
					return new ReadOnlyTreeNodeCollection(m_clickedNode);
				else if (MultiSelect)
					return new ReadOnlyTreeNodeCollection(SelectedNodes);
				else
					return new ReadOnlyTreeNodeCollection(RealSelectedNode);
			}
		}

		/// <summary>
		/// True to allow the user to select multiple tree nodes, false to only only single selection
		/// (standard TreeView behaviour). The default is false.
		/// </summary>
		[DefaultValue(m_defaultMultiSelect)]
		public bool MultiSelect
		{
			get { return m_multiSelect; }
			set
			{
				if (m_multiSelect != value)
				{
					m_multiSelect = value;

					if (value)
					{
						Debug.Assert(m_selected.Count == 0, "m_selected.Count == 0");

						if (RealSelectedNode != null)
						{
							m_selected.Add(RealSelectedNode);
						}
					}
					else
					{
						m_selected.Clear();
					}
				}
			}
		}

		// SelectedNode is not virtual, so we cannot override it, but do our best to make this property
		// behave correctly when MultiSelect is true.
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TreeNode SelectedNode
		{
			get { return (MultiSelect && m_selected.Count == 0 ? null : base.SelectedNode); }
			set
			{
				if (MultiSelect)
				{
					// When the user sets this property we want the events to fire in this order:
					// BeforeSelect, SelectionChanged, AfterSelect.
					// We also want SelectedNode to return the old node while BeforeSelect fires and give the caller the
					// opportunity to cancel it, so the actual change must be done in OnBeforeSelect().

					m_selectionProcessing = SelectionProcessing.SetToThis;
					RealSelectedNode = value;
				}
				else
				{
					base.SelectedNode = value;
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SelectedTreeNodeCollection SelectedNodes
		{
			get
			{
				return (MultiSelect ? m_selected : null);
			}
		}

		private TreeNode RealSelectedNode
		{
			get { return base.SelectedNode; }
			set { base.SelectedNode = value; }
		}

		#region Static methods

		/// <summary>
		/// Sets the tags of all nodes in the specified collection to null, recursively.
		/// </summary>
		public static void ClearTagsRecursive(TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
			{
				node.Tag = null;
				ClearTagsRecursive(node.Nodes);
			}
		}

		/// <summary>
		/// Returns the node that should be selected after deleting the specified node to emulate ListView
		/// behaviour. The node after the specified node is returned, if there is one. If the last node was
		/// deleted then the previous node is returned. If there is no previous node (ie. the last node at
		/// that level has been deleted) then the parent node is returned.
		/// </summary>
		public static TreeNode GetNodeToSelectAfterDeleting(TreeNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			// Try the next node after the deleted one.

			if (node.NextNode != null)
				return node.NextNode;

			// Try the previous node.

			if (node.PrevNode != null)
				return node.PrevNode;

			// The last node at this level is being deleted, so return the parent node or null if there is
			// no parent (ie. all nodes in the TreeView are being deleted).

			return node.Parent;
		}

		/// <summary>
		/// Returns the node that should be selected after deleting the specified nodes to emulate ListView
		/// behaviour. If all the specified nodes are siblings the node after the last (lowest)
		/// of these nodes is returned. If the last node was deleted then the last node that still exists
		/// is returned. If all nodes at this level were deleted then the parent node is returned. If the
		/// specified nodes are not all siblings null is returned.
		/// </summary>
		public static TreeNode GetNodeToSelectAfterDeleting(TreeNode[] nodes)
		{
			if (nodes == null)
				throw new ArgumentNullException("nodes");
			if (nodes.Length == 0)
				throw new ArgumentException("The array of nodes to be deleted is empty.", "nodes");

			TreeNode lastDeleted = nodes[0];
			TreeNode parent = lastDeleted.Parent; // Note that parent may be null.

			for (int index = 1; index < nodes.Length; index++)
			{
				TreeNode node = nodes[index];

				if (node.Parent != parent)
					return null; // Nodes being deleted are not all siblings.

				if (node.Index > lastDeleted.Index)
				{
					lastDeleted = node;
				}
			}

			// Try the next node after the last deleted one.

			if (lastDeleted.NextNode != null)
				return lastDeleted.NextNode;

			// Find the last node in the parent that is not being deleted.

			TreeNode lastNonDeleted = lastDeleted.PrevNode;
			while (lastNonDeleted != null && Array.IndexOf(nodes, lastNonDeleted) != -1)
			{
				lastNonDeleted = lastNonDeleted.PrevNode;
			}

			// Return the last node that is not being deleted, the parent node if there is none or null if
			// there is no parent (ie. all nodes in the TreeView are being deleted).

			return (lastNonDeleted != null ? lastNonDeleted : parent);
		}

		private static void PaintSelectedNode(TreeNode node, bool focused)
		{
			node.BackColor = (focused ? SystemColors.Highlight : SystemColors.InactiveBorder);
			node.ForeColor = (focused ? SystemColors.HighlightText : SystemColors.WindowText);
		}

		#endregion

		/// <summary>
		/// Finds the node with the specified path starting from the root of the TreeView. This requires
		/// the node text to be unique amongst siblings at each level. If the node cannot be found and approximate
		/// is true this method returns the closest parent node of the requested node, otherwise it returns null.
		/// Note that even if approximate is true the method may return null.
		/// </summary>
		public TreeNode FindNodeByPath(string path, bool approximate)
		{
			return FindNodeByPath(Nodes, path, approximate);
		}

		public TreeNode FindNodeByPath(TreeNodeCollection nodes, string path, bool approximate)
		{
			return FindNodeByPath(nodes, path, approximate, false);
		}

		/// <summary>
		/// Finds the node with the specified path starting from the supplied node collection. This requires
		/// the node text to be unique amongst siblings at each level. If the node cannot be found and approximate
		/// is true this method returns the closest parent node of the requested node, otherwise it returns null.
		/// Note that even if approximate is true the method may return null.
		/// </summary>
		public TreeNode FindNodeByPath(TreeNodeCollection nodes, string path, bool approximate, bool expand)
		{
			if (nodes == null)
				throw new ArgumentNullException("nodes");
			if (path == null)
				throw new ArgumentNullException("path");
			if (PathSeparator == null)
				throw new InvalidOperationException("Unable to search by node path, because the"
					+ " PathSeparator property returned null.");
			if (PathSeparator.Length > 1)
				throw new InvalidOperationException("Unable to search by node path, because the"
					+ " PathSeparator property returned a string longer than 1 character ('"
					+ PathSeparator + "').");

			string[] nodeNames = path.Split(PathSeparator[0]);
			if (nodeNames.Length == 0)
				throw new ArgumentException("The path ('" + path + "') is not a valid node path.");

			TreeNode foundNode = null;

			for (int index = 0; index < nodeNames.Length; index++)
			{
				string name = nodeNames[index];
				bool childFound = false;

				foreach (TreeNode childNode in nodes)
				{
					// Check the node text.

					if (childNode.Text == name)
					{
						foundNode = childNode;
						nodes = childNode.Nodes;
						childFound = true;
						break;
					}
				}

				if (!childFound)
					return (approximate ? foundNode : null);

				// Expand the node before traversing its children, if the caller wants us to, except for the
				// last (bottom) one.

				if (expand && index != nodeNames.Length - 1)
				{
					foundNode.Expand();
				}
			}

			return foundNode;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// Need to set the context nodes here, because the user may right-click on a node without
				// selecting it (left-clicking).

				TreeNode clickedNode = GetNodeAt(e.X, e.Y);

				if (clickedNode != null && MultiSelect)
				{
					if (SelectedNodes.Contains(clickedNode))
					{
						// The clicked node is one of the selected nodes, so the selected nodes remain the
						// context nodes - clear the clicked node.

						SetClickedNode(null);

						// Set the node that was right-clicked to be the real selected node. Without this
						// it looks deselected to the user while the mouse button is held down.

						if (RealSelectedNode != clickedNode)
						{
							m_selectionProcessing = SelectionProcessing.IgnoreOnce;
							RealSelectedNode = clickedNode;
						}
					}
					else
					{
						// Set the clicked node and visually deselect the selected nodes to make it clear
						// to the user that the clicked node is the one on which context menu commands will
						// be executed. The selected nodes are painted again in OnMouseUp.

						BeginUpdate();
						try
						{
							RemovePaintFromNodes();
							PaintSelectedNode(clickedNode);
						}
						finally
						{
							EndUpdate();
						}

						SetClickedNode(clickedNode);
					}
				}
				else
				{
					SetClickedNode(clickedNode);
				}
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			bool processed = false;

			if (MultiSelect)
			{
				if (e.Button == MouseButtons.Right)
				{
					// The selected nodes may have had highlight paint removed in OnMouseDown, so paint them again.

					BeginUpdate();

					try
					{
						PaintSelectedNodes();

						if (m_clickedNode != null && !m_selected.Contains(m_clickedNode))
						{
							RemovePaintFromNode(m_clickedNode);
						}
					}
					finally
					{
						EndUpdate();
					}
				}
				else if (e.Button == MouseButtons.Left)
				{
					if (Control.ModifierKeys == Keys.None || Control.ModifierKeys == Keys.Control)
					{
						TreeNode node = GetNodeAt(PointToClient(MousePosition));

						if (node == RealSelectedNode && m_selected.Count > 1)
						{
							// The user clicked on the real selected node, so we want to either make it the
							// only selected node or deselect it, depending on whether they held down Control.

							Debug.Assert(m_selected.Contains(node), "m_selected.Contains(node)");

							if (Control.ModifierKeys == Keys.Control)
							{
								m_selected.Remove(node);
							}
							else
							{
								m_selected.InternalClear();
								m_selectionPivot = node;
								m_selected.Add(node);
							}

							processed = true;
						}
					}
				}
			}

			if (!processed)
			{
				base.OnMouseUp(e);
			}
		}


		protected override void OnDoubleClick(EventArgs e)
		{
			TreeNode activated = GetNodeAt(PointToClient(MousePosition));

			base.OnDoubleClick(e);

			// The user can double-click on an empty area next to node text. GetNodeAt() returns the node
			// in that case, but we only want to activate it if it's the selected node (ie. the user clicked
			// on the node itself, not empty space).

			if (activated != null && activated == RealSelectedNode)
			{
				OnNodeActivate(new TreeViewEventArgs(activated, TreeViewAction.ByMouse));
			}
		}

		protected virtual void OnContextNodesChanged(EventArgs e)
		{
			if (ContextNodesChanged != null)
			{
				ContextNodesChanged(this, e);
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			TreeNode activated = null;

			if (e.KeyChar == 13)
			{
				activated = RealSelectedNode;
				e.Handled = true;
			}

			base.OnKeyPress(e);

			if (activated != null)
			{
				OnNodeActivate(new TreeViewEventArgs(activated, TreeViewAction.ByKeyboard));
			}
		}

		protected virtual void OnNodeActivate(TreeViewEventArgs e)
		{
			if (NodeActivate != null)
			{
				NodeActivate(this, e);
			}
		}

		protected virtual void OnSelectionsChanged(EventArgs e)
		{
			if (SelectionsChanged != null)
			{
				SelectionsChanged(this, e);
			}

			// If the user has not right-clicked on a node then the selected nodes are also the context nodes.

			if (!m_suppressContextNodesChanged && (m_clickedNode == null || m_clickedNode.TreeView != this))
			{
				OnContextNodesChanged(e);
			}
		}

		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			bool changeSelection = false;
			bool raiseEvent = true;

			if (MultiSelect)
			{
				switch (m_selectionProcessing)
				{
					case SelectionProcessing.Process:
						if (ModifierKeys != Keys.Shift)
						{
							m_selectionPivot = e.Node;
						}

						Debug.Assert(!(ModifierKeys == Keys.Control && m_selected.Contains(e.Node)
							&& m_selected.Count > 1), "OnBeforeSelect when Control-clicking a node to deselect it,"
							+ " this should have been handled in WmNotify.");
						break;

					case SelectionProcessing.Cancel:
						Debug.Fail("OnBeforeSelect called when m_selectionProcessing == Cancel, this should"
							+ " have been handled in WmNotify.");
						break;

					case SelectionProcessing.IgnoreOnce:
					case SelectionProcessing.IgnoreThenCancel:
						// Do nothing - these cases are handled in OnAfterSelect().
						break;

					case SelectionProcessing.IgnoreOnceNoBeforeSelectEvent:
						raiseEvent = false; // Don't do anything, don't even raise the event.
						break;

					case SelectionProcessing.SetToThis:
						// Change the real selection here and don't do anything in AfterSelect.
						changeSelection = true;
						m_selectionProcessing = SelectionProcessing.IgnoreOnce;
						break;

					default:
						Debug.Fail("Unexpected value of m_selectionProcessing:" + m_selectionProcessing.ToString());
						break;
				}
			}

			if (raiseEvent)
			{
				base.OnBeforeSelect(e);
			}

			if (changeSelection && !e.Cancel)
			{
				m_selected.InternalClear();
				m_selectionPivot = e.Node;

				if (e.Node != null)
				{
					m_selected.InternalAdd(e.Node);
				}

				OnSelectionsChanged(EventArgs.Empty);
			}
		}

		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			if (MultiSelect)
			{
				switch (m_selectionProcessing)
				{
					case SelectionProcessing.Process:	   
						BeginUpdate();
						try
						{
							m_suppressContextNodesChanged = true;
							ProcessMultipleSelection(e.Node);
						}
						finally
						{
							m_suppressContextNodesChanged = false;
							EndUpdate();
						}
						break;

					case SelectionProcessing.IgnoreOnce:
					case SelectionProcessing.IgnoreOnceNoBeforeSelectEvent:
						// Resume normal processing.
						m_selectionProcessing = SelectionProcessing.Process;
						break;

					case SelectionProcessing.IgnoreThenCancel:
						// The next selection is the one we don't want - cancel it.
						m_selectionProcessing = SelectionProcessing.Cancel;
						break;

					default:
						Debug.Fail("Unexpected value of m_selectionProcessing:" + m_selectionProcessing.ToString());
						break;
				}
			}

			// Need to clear the clicked node here and raise the ContextNodesChangedEvent, because the context
			// nodes are now the selected nodes until the user right-clicks on another node (handled in
			// OnMouseDown). Need to raise the event even if m_clickedNode is already null, since the selection
			// (and therefore the context nodes) have changed. Raise AfterSelect first, then ContextNodesChanged.

			m_clickedNode = null;

			base.OnAfterSelect(e);

			OnContextNodesChanged(EventArgs.Empty);
		}

		protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
		{
			// If there are any selected nodes in the node being collapsed we want to unselect them. If one
			// of them is the real selected node then we want to set some other node to be selected, otherwise
			// the TreeView will do it, which will confuse us.

			if (MultiSelect)
			{
				BeginUpdate();

				try
				{
					bool changeRealSelection = false;
					bool changed = UnselectNodesUnder(e.Node.Nodes, out changeRealSelection);

					if (changeRealSelection)
					{
						// If all the selected nodes were under the node being collapsed then select the node
						// being collapsed, otherwise set the first of the remaining selected nodes to be the
						// real selection.

						m_selectionProcessing = SelectionProcessing.IgnoreOnce;
						if (m_selected.Count == 0)
						{
							m_selected.InternalAdd(e.Node);
							RealSelectedNode = e.Node;
							changed = true;
						}

						RealSelectedNode = m_selected[0];
					}

					if (changed)
					{
						OnSelectionsChanged(EventArgs.Empty);
					}
				}
				finally
				{
					EndUpdate();
				}
			}

			base.OnBeforeCollapse(e);
		}

		protected override void OnEnter(EventArgs e)
		{
			// At this stage the Focused property still returns false, so pass true to PaintSelectedNodes to
			// paint them as if the control has the focus. We could do this in OnGotFocus instead (Focused is
			// true by then), but that would result in a lot of unncessary painting as the user switches between
			// windows.

			PaintSelectedNodes(true);

			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			m_hidingSelection = HideSelection;
			PaintNodesForHideSelection();

			base.OnLeave(e);
		}

		protected override void OnStyleChanged(EventArgs e)
		{
			if (!Focused && HideSelection != m_hidingSelection)
			{
				m_hidingSelection = HideSelection;
				PaintNodesForHideSelection();
			}

			base.OnStyleChanged(e);
		}

		protected override void WndProc(ref Message m)
		{
			const int wmReflectNotify = Constants.Win32.Messages.WM_REFLECT + Constants.Win32.Messages.WM_NOTIFY;

			bool selectionChanged = false;
			bool processed = false;

			switch (m.Msg)
			{
				case wmReflectNotify:
					processed = WmNotify(ref m);
					break;

				case Constants.Win32.Messages.TVM_DELETEITEM:
					selectionChanged = TvmDeleteItem(m.LParam);
					break;
			}

			if (!processed)
			{
				base.WndProc(ref m);
			}

			if (selectionChanged)
			{
				OnSelectionsChanged(EventArgs.Empty);
			}
		}

		private bool WmNotify(ref Message m)
		{
			if (!MultiSelect)
				return false;
				
			switch (m_selectionProcessing)
			{
				case SelectionProcessing.Cancel:
					// Selection processing is set to Cancel, so cancel it here in the TvnSelchanging notification
					// before it even gets to OnBeforeSelect(), because we cannot cancel it in OnBeforeSelect
					// without raising the BeforeSelect event (which would then have Cancel already set to true).

					Win32.NMHDR header1 = (Win32.NMHDR)m.GetLParam(typeof(Win32.NMHDR));

					if (header1.code == Constants.Win32.Notifications.TVN_SELCHANGING)
					{
						m.Result = new IntPtr(1); // Cancel this selection.
						m_selectionProcessing = SelectionProcessing.Process; // Resume normal processing.
						return true;
					}
					else
						return false;

				case SelectionProcessing.Process:
					if (!(ModifierKeys == Keys.Control && m_selected.Count > 1))
						return false;

					Win32.NMHDR header2 = (Win32.NMHDR)m.GetLParam(typeof(Win32.NMHDR));

					if (header2.code == Constants.Win32.Notifications.TVN_SELCHANGING)
					{
						// If the node the user clicked on (while holding Control) is already selected and
						// it's not the only one then deselect it.

						Win32.NMTREEVIEW treeHeader = (Win32.NMTREEVIEW)Marshal.PtrToStructure(m.LParam,
							typeof(Win32.NMTREEVIEW));

						if (treeHeader.itemNew.hItem == IntPtr.Zero)
							return false; // Not sure why this would happen, but handle it since .NET does.

						int index = m_selected.IndexOfHandle(treeHeader.itemNew.hItem);
						if (index == -1)
							return false; // Not in the list of selected nodes.

						m_selected.RemoveAt(index); // Deselect.
						m.Result = new IntPtr(1); // Cancel this selection.

						return true;
					}
					else
						return false;

				default:
					return false;
			}
		}

		private bool TvmDeleteItem(IntPtr deletedNodeHandle)
		{
			bool selectionChanged = false;

			if (MultiSelect)
			{
				// Check whether the node being deleted is in our list of selected nodes and if so -
				// remove it from the selection and paint it normal again.

				foreach (TreeNode node in m_selected)
				{
					if (node.Handle == deletedNodeHandle)
					{
						selectionChanged = true;
						RemovePaintFromNode(node);
						m_selected.InternalRemove(node);
						break;
					}
				}
			}

			return selectionChanged;
		}

		private void PaintNodesForHideSelection()
		{
			if (m_hidingSelection)
			{
				RemovePaintFromNodes();
			}
			else
			{
				PaintSelectedNodes();
			}
		}

		/// <summary>
		/// Recursively remove any selected nodes in the supplied collection from the selection and return
		/// a flag indicating whether any were removed. Also return a flag indicating whether the real
		/// selected node needs to be changed.
		/// </summary>
		private bool UnselectNodesUnder(TreeNodeCollection nodes, out bool changeRealSelection)
		{
			bool changed = false;
			changeRealSelection = false;

			foreach (TreeNode node in nodes)
			{
				if (RealSelectedNode == node)
				{
					changeRealSelection = true;
				}

				if (m_selected.Contains(node))
				{
					m_selected.InternalRemove(node);
					RemovePaintFromNode(node);
					changed = true;
				}

				bool childChangeReal;
				bool childChanged = UnselectNodesUnder(node.Nodes, out childChangeReal);
				changed |= childChanged;
				changeRealSelection |= childChangeReal;
			}

			return changed;
		}

		private void ProcessMultipleSelection(TreeNode selectedNode)
		{
			if (ModifierKeys == Keys.Control)
			{
				if (m_selected.Contains(selectedNode))
				{
					if (m_selected.Count > 1)
					{
						m_selected.Remove(selectedNode);
					}
				}
				else
				{
					m_selected.Add(selectedNode);
				}
			}
			else if (ModifierKeys == Keys.Shift)
			{
				if (m_selectionPivot == null)
				{
					// Shift was held, but there was no selection pivot, so just select this node as if it was
					// a normal click (no shift).

					m_selected.InternalClear();
					m_selected.Add(selectedNode);
					m_selectionPivot = selectedNode;
				}
				else
				{
					ProcessContiguousSelection(selectedNode);
				}
			}
			else
			{
				// No Control or Shift modifier pressed - just select this node.

				m_selected.InternalClear();
				m_selected.Add(selectedNode);
			}
		}

		private void ProcessContiguousSelection(TreeNode selectedNode)
		{
			TreeNode upperNode = null;
			TreeNode lowerNode = null;

			// We have two nodes that define the edges of the selection - the "pivot node" and the one that
			// the user selected just now. Work out which of them is higher and which is lower in the tree.

			int comparison = TreeNodeComparer.Compare(m_selectionPivot, selectedNode);
			if (comparison < 0)
			{
				upperNode = m_selectionPivot;
				lowerNode = selectedNode;
			}
			else if (comparison > 0)
			{
				upperNode = selectedNode;
				lowerNode = m_selectionPivot;
			}
			else
			{
				Debug.Assert(m_selectionPivot == selectedNode, "TreeNodeComparer returned 0 for nodes that"
					+ " are not the same.");

				// It's possible for this method to be called when only a single node is selected:
				// 1) Select a single node.
				// 2) Hold shift while pressing down to select the next node.
				// 3) Hold shift while pressing up to deselect this node again.

				lowerNode = selectedNode;
			}

			// Select all the visible nodes from the upper one down to the lower one. Note that some of the
			// nodes may already be selected, because the user may hold shift and click twice while the
			// pivot node remains the same. This matches the behaviour of the VS.NET Solution Explorer.

			ArrayList tempList = new ArrayList();
			if (upperNode != null)
			{
				for (TreeNode node = upperNode; node != null && node != lowerNode; node = node.NextVisibleNode)
				{
					tempList.Add(node);
				}
			}
			tempList.Add(lowerNode);

			m_selected.InternalClear(); // A contigous selection clears any previous selection.
			m_selected.AddSortedRange(tempList, selectedNode);
		}

		private void PaintSelectedNode(TreeNode node)
		{
			PaintSelectedNode(node, Focused);
		}

		private void PaintSelectedNodes()
		{
			PaintSelectedNodes(Focused);
		}

		private void PaintSelectedNodes(bool focused)
		{
			foreach (TreeNode node in m_selected)
			{
				PaintSelectedNode(node, focused);
			}
		}

		private void RemovePaintFromNode(TreeNode node)
		{
			node.BackColor = BackColor;
			node.ForeColor = ForeColor;
		}

		private void RemovePaintFromNodes()
		{
			BeginUpdate();

			try
			{
				foreach (TreeNode node in m_selected)
				{
					RemovePaintFromNode(node);
				}
			}
			finally
			{
				EndUpdate();
			}
		}

		private void DeselectNode(TreeNode node)
		{
			Debug.Assert(node != null, "node != null");

			RemovePaintFromNode(node);

			if (m_selectionPivot == node)
			{
				m_selectionPivot = null;
			}
		}

		private void ChangeRealSelection(TreeNode changeToNode)
		{
			m_selectionProcessing = SelectionProcessing.IgnoreThenCancel;
			RealSelectedNode = changeToNode;
		}

		private void SetClickedNode(TreeNode node)
		{
			// Note that node may be null.

			if (m_clickedNode != node)
			{
				m_clickedNode = node;

				if (!m_suppressContextNodesChanged)
				{
					OnContextNodesChanged(EventArgs.Empty);
				}
			}
		}

		private void RaiseBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect(e);
		}

		private void RaiseAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);
		}
	}
}
