using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Base class for a field or property wrapper obtained from a GenericWrapper.
	/// </summary>
	[Serializable]
	public class MemberWrappers : CollectionBase, IBindingList
	{
		public MemberWrappers()
		{
		}

		#region IBindingList Members

		public event ListChangedEventHandler ListChanged;

		public bool AllowNew
		{
			get { return false; }
		}

		public bool AllowEdit
		{
			get { return true; }
		}

		public bool AllowRemove
		{
			get { return false; }
		}

		public PropertyDescriptor SortProperty
		{
			get { return null; }
		}

		public bool SupportsSorting
		{
			get { return false; }
		}

		public bool IsSorted
		{
			get { return false; }
		}

		public bool SupportsSearching
		{
			get { return false; }
		}

		public ListSortDirection SortDirection
		{
			get { return new ListSortDirection(); }
		}

		public bool SupportsChangeNotification
		{
			get { return true; }
		}

		public object AddNew()
		{
			throw new NotSupportedException();
		}

		public void AddIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		public void RemoveIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		public void RemoveSort()
		{
			throw new NotSupportedException();
		}

		public int Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		#endregion

		public MemberWrapper this[int index]
		{
			get { return (MemberWrapper)List[index]; }
			set { List[index] = value; }
		}

		public void Add(MemberWrapper value)
		{
			List.Add(value);
		}

		public bool Contains(MemberWrapper value)
		{
			return List.Contains(value);
		}

		public void CopyTo(MemberWrapper[] array, int index)
		{
			List.CopyTo(array, index);
		}

		public int IndexOf(MemberWrapper value)
		{
			return List.IndexOf(value);
		}

		public void Insert(int index, MemberWrapper value)
		{
			List.Insert(index, value);
		}

		public void Remove(MemberWrapper value)
		{
			List.Remove(value);
		}

		public void RemoveRange(int index, int count)
		{
			InnerList.RemoveRange(index, count);

			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		protected internal virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (ListChanged != null)
			{
				ListChanged(this, e);
			}
		}

		protected override void OnClearComplete()
		{
			base.OnClearComplete();

			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);

			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);

			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		protected override void OnSetComplete(int index, object oldValue, object newValue)
		{
			base.OnSetComplete(index, oldValue, newValue);

			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
		}
	}
}
