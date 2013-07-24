using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.ObjectProperties;

namespace LinkMe.Framework.Tools.Mmc
{
	internal enum ResultDataItemMask
	{
		Str		= 0x2,
		Image	= 0x4,
		State	= 0x8,
		Param	= 0x10,
		Index	= 0x20,
		Indent	= 0x40,
	}

	internal class SubTexts
	{
		public SubTexts(string[] subTexts)
		{
			Reset(subTexts);
		}

		public void Reset(string[] subTexts)
		{
			m_subTexts = (string[]) subTexts.Clone();
		}

		public string this[int index]
		{
			get
			{
				if ( index >= 0 && index < m_subTexts.Length )
					return m_subTexts[index];
				else
					return string.Empty;
			}
		}

		private string[] m_subTexts;
	}

	internal class Result
	{
		public Result(IntPtr id, int smallImageIndex, int largeImageIndex, object data, string name, string text, string[] subtexts, int param)
		{
			m_id = id;
			m_smallImageIndex = smallImageIndex;
			m_largeImageIndex = largeImageIndex;
			m_data = data;
			m_name = name;
			m_text = text;
			m_subtexts = new SubTexts(subtexts);
			m_param = param;
		}

        public IntPtr ID
		{
			get { return m_id; }
		}

		public int SmallImageIndex
		{
			get { return m_smallImageIndex; }
			set { m_smallImageIndex = value; }
		}

		public int LargeImageIndex
		{
			get { return m_largeImageIndex; }
			set { m_largeImageIndex = value; }
		}

		public object Data
		{
			get { return m_data; }
			set { m_data = value; }
		}

		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		public string Text
		{
			get { return m_text; }
			set { m_text = value; }
		}

		public SubTexts SubTexts
		{
			get { return m_subtexts; }
		}

		public int Param
		{
			get { return m_param; }
		}

		public ObjectPropertyForm PropertyForm
		{
			get { return m_propertyForm; }
			set { m_propertyForm = value; }
		}

        private IntPtr m_id;
		private int m_smallImageIndex;
		private int m_largeImageIndex;
		private object m_data;
		private string m_name;
		private string m_text;
		private SubTexts m_subtexts;
		private int m_param;
		private ObjectPropertyForm m_propertyForm;
	}

	internal class Results
	{
		public Results()
		{
		}

		public int Count
		{
			get { return m_results.Count; }
		}

		public Result this[string name]
		{
			get
			{
				foreach (Result result in m_results)
				{
					if (result.Name == name)
						return result;
				}

				return null;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return m_results.GetEnumerator();
		}

		public void Add(Result result)
		{
			Debug.Assert(result.Param == m_results.Count, "result.Param == m_results.Count");
			m_results.Add(result);
		}

		public Result GetByParam(int param)
		{
			return (Result)m_results[param];
		}

		private ArrayList m_results = new ArrayList();
	}

	/// <summary>
	/// This is a MMC.NET treenode for the MMC scope view.
	/// The result view displays as a list view.
	/// </summary>
	public abstract class ResultNode
		:	SnapinNode
	{
		#region Constructors

		protected ResultNode(Snapin snapin)
			:	base(snapin)
		{
			m_resultSelected = false;
		}

		#endregion

		#region ResultView

		protected void AddColumn(string name, ListViewColumnFormat format, int width)
		{
			IHeaderCtrl2 header = Snapin.ResultViewConsole as IHeaderCtrl2;
			header.InsertColumn(m_currentColumns++, name, (int) format, width);
		}

		protected void AddColumn(string name, ListViewColumnFormat format)
		{
			AddColumn(name, format, -1);
		}

		protected void AddColumn(string name, int width)
		{
			AddColumn(name, ListViewColumnFormat.Left, width);
		}

		protected void AddColumn(string name)
		{
			AddColumn(name, -1);
		}

		protected void AddColumn(int width)
		{
			AddColumn(" ", width);
		}

		protected void AddColumn()
		{
			AddColumn(-1);
		}

		protected void AddResult(string text)
		{
			AddResult(null, text, text, new string[] {});
		}

		protected void AddResult(string text, string[] subTexts)
		{
			AddResult(null, text, text, subTexts);
		}

		protected void AddResult(object data, string text)
		{
			AddResult(data, text, text, new string[] {});
		}

		protected void AddResult(object data, string text, string[] subTexts)
		{
			AddResult(data, text, text, subTexts);
		}

		protected void AddResult(object data, string name, string text, string[] subTexts)
		{
			// Add the item.

			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			ResultDataItem rdi = new ResultDataItem();
			rdi.Mask = (uint) ResultDataItemMask.Str | (uint) ResultDataItemMask.Image | (uint) ResultDataItemMask.Param;
			rdi.Image = -1;
			rdi.Str = (IntPtr)(-1);
			rdi.Col = 0;
			int resultParam = m_results.Count;
			rdi.Param = GetItemParamFromResultParam(resultParam);
			resultData.InsertItem(ref rdi);

			// Add the result details.

			m_results.Add(new Result(rdi.ItemId, -1, -1, data, name, text, subTexts, resultParam));
		}

		protected void AddResult(string smallImage, string largeImage, string text)
		{
			AddResult(smallImage, largeImage, text, new string[] {});
		}

		protected void AddResult(string smallImage, string largeImage, string text, string[] subTexts)
		{
			AddResult(smallImage, largeImage, null, text, text, subTexts);
		}

		protected void AddResult(string smallImage, string largeImage, object data, string text)
		{
			AddResult(smallImage, largeImage, data, text, text, new string[] {});
		}

		protected void AddResult(string smallImage, string largeImage, object data, string text, string[] subTexts)
		{
			AddResult(smallImage, largeImage, data, text, text, subTexts);
		}

		protected void AddResult(string smallImage, string largeImage, object data, string name, string text, string[] subTexts)
		{
			// Add the item.

			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			ResultDataItem rdi = new ResultDataItem();
			rdi.Mask = (uint) ResultDataItemMask.Str | (uint) ResultDataItemMask.Image | (uint) ResultDataItemMask.Param;
			rdi.Image = -1;
			rdi.Str = (IntPtr)(-1);
			rdi.Col = 0;
			int resultParam = m_results.Count;
			rdi.Param = GetItemParamFromResultParam(resultParam);
			resultData.InsertItem(ref rdi);

			// Add the result details.

			int smallImageIndex = AddResultImage(smallImage);
			int largeImageIndex = AddResultImage(largeImage);
			m_results.Add(new Result(rdi.ItemId, smallImageIndex, largeImageIndex, data, name, text, subTexts, resultParam));
		}

		protected void RefreshResult(string name)
		{
			if ( name != null && name != string.Empty )
			{
				Result result = m_results[name];
				if ( result != null )
				{
					string smallImage = string.Empty;
					string largeImage = string.Empty;
					string text = string.Empty;
					string[] subTexts = null;
					if ( RefreshResult(result.Name, result.Data, ref smallImage, ref largeImage, ref text, ref subTexts) )
						RefreshResult(result, smallImage, largeImage, text, subTexts);
				}
			}
		}

		protected virtual bool RefreshResult(string name, object data, ref string smallImage, ref string largeImage, ref string text, ref string[] subTexts)
		{
			return false;
		}

		private void RefreshResult(Result result, string smallImage, string largeImage, string text, string[] subTexts)
		{
			// Update the result.

			int newSmallIndex = AddResultImage(smallImage);
			int newLargeIndex = AddResultImage(largeImage);
			result.SmallImageIndex = newSmallIndex;
			result.LargeImageIndex = newLargeIndex;
			result.Text = text;
			result.SubTexts.Reset(subTexts);

			// Update the image.

			ResultDataItem rdi = new ResultDataItem();
			rdi.ItemId = result.ID;
			rdi.Mask = (uint) ResultDataItemMask.Image;
			rdi.Image = -1;

			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			resultData.SetItem(ref rdi);

			// Redraw.

			resultData.UpdateItem(result.ID);
		}

		protected void SelectResult(string name)
		{
			Result result = m_results[name];
			if (result != null)
			{
				SelectResult(result);
			}
		}

		private void SelectResult(Result result)
		{
			Debug.Assert(result != null, "result != null");

			// Set it to be selected and focused.

			IResultData resultData = (IResultData)Snapin.ResultViewConsole;
			resultData.ModifyItemState(0, result.ID, (uint) (ListViewItemState.Focused | ListViewItemState.Selected), 0);
		}

		protected virtual string GetLargeResultImage(string name)
		{
			return GetSmallResultImage(name); // By default use the small image in place of the large one.
		}

		/// <summary>
		/// Unlike SnapinNode.GetImage() this method does not have to be overriden in the derived class.
		/// By default the result icons do not change from their original values (set in AddResult()).
		/// If the result icons need to change then the derived class should override this method to return
		/// the current icon for the result with the specified name. Return null to keep the icon unchanged.
		/// </summary>
		protected virtual string GetSmallResultImage(string name)
		{
			return null;
		}

		public void UpdateResultImages(string name)
		{
			if (m_results == null)
				return;

			Result result = m_results[name];
			if (result == null)
				return;

			string smallImage = GetSmallResultImage(name);
			if (smallImage == null)
				return;

			string largeImage = GetLargeResultImage(name);
			if (largeImage == null)
				return;

			SetResultImage(result, smallImage, largeImage);
		}

		protected void UpdateSelectedResultImages()
		{
			Result[] selected = GetSelectedResults();
			foreach (Result result in selected)
			{
				UpdateResultImages(result);
			}
		}

		internal void UpdateResultImages()
		{
			if (m_results == null)
				return;

			foreach (Result result in m_results)
			{
				UpdateResultImages(result);
			}
		}

		private void UpdateResultImages(Result result)
		{
			string smallImage = GetSmallResultImage(result.Name);
			if (smallImage == null)
				return;

			string largeImage = GetLargeResultImage(result.Name);
			if (largeImage == null)
				return;

			SetResultImage(result, smallImage, largeImage);
		}

		private void SetResultImage(Result result, string smallImage, string largeImage)
		{
			int newSmallIndex = AddResultImage(smallImage);
			int newLargeIndex = AddResultImage(largeImage);

			if (newSmallIndex == result.SmallImageIndex && newLargeIndex == result.LargeImageIndex)
				return; // No change.

			result.SmallImageIndex = newSmallIndex;
			result.LargeImageIndex = newLargeIndex;

			// Update the image.

			ResultDataItem rdi = new ResultDataItem();
			rdi.ItemId = result.ID;
			rdi.Mask = (uint) ResultDataItemMask.Image;
			rdi.Image = -1;

			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			resultData.SetItem(ref rdi);

			// Redraw.

			resultData.UpdateItem(result.ID);
		}

		protected void SetResultData(string name, object data)
		{
			Result result = m_results[name];
			if ( result != null )
				result.Data = data;
		}

		private void SetResultViewMode()
		{
			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			resultData.SetViewMode((int) GetResultViewMode());
		}
		
		#endregion

		#region Handlers

		protected override void Show()
		{
			// Let the derived class add columns.

			m_currentColumns = 0;
			AddColumns();

			// Let the derived class add results.

			m_results = new Results();
			AddResults();

			SetResultViewMode();
		}

		protected override void SelectResult()
		{
			m_resultSelected = true;

			// Give the node a chance to enable verbs.

			EnableResultVerbs();
		}

		protected override void DeselectResult()
		{
			m_resultSelected = false;
		}

		protected override void DeleteResults()
		{
			// Get the selected result.

			Result[] selectedResults = GetSelectedResults();
			if ( selectedResults.Length == 0 )
				return;

			try
			{
				// Check first.

				bool delete;
				if ( selectedResults.Length == 1 )
					delete = PromptForDeleteResult(selectedResults[0]);
				else
					delete = PromptForDeleteResults();

				if ( !delete )
					return;

				using ( new LongRunningMonitor(Snapin) )
				{
					// Keep track of what to select after.

					Result nextSelection = GetNextResultAfterDeleting(selectedResults);

					foreach ( Result result in selectedResults )
					{
						// Let the derived class deal with this result being deleted.

						DeleteResult(result.Data);

						// Delete the result from the result view.

						DeleteResultItem(result.ID);

						// Let the derived class decide whether it needs to delete a node as well.

						DeleteResultNode(result.Data);
					}

 					// Select the next item worked out above.

					if ( nextSelection != null )
						SelectResult(nextSelection.Name);
				}
			}
			catch ( Exception e )
			{
				new ExceptionDialog(e, "Cannot delete the elements.").ShowDialog(Snapin);
			}
		}

		private Result GetNextResultAfterDeleting(Result[] selectedResults)
		{
			// Work out which result should be after the deletion. This follows the same rules as the
			// normal ListView. Try to select the item that follows the last deleted item. If the last
			// item was deleted then select the last remaining item.

			Result[] results = GetResults();
			if ( results.Length == selectedResults.Length )
				return null;

			Result lastSelectedResult = selectedResults[selectedResults.Length - 1];
			if ( lastSelectedResult.Name == results[results.Length - 1].Name )
			{
				// Look for the last unselected result.

				int lastSelectedIndex = selectedResults.Length - 1;
				for ( int index = results.Length - 1; index >= 0; --index, --lastSelectedIndex )
				{
					if ( lastSelectedIndex < 0 || results[index].Name != selectedResults[lastSelectedIndex].Name )
						return results[index];
				}
			}
			else
			{
				// There is at least one result after the last selected result.

				for ( int index = results.Length - 1; index >= 0; --index )
				{
					if ( results[index].Name == lastSelectedResult.Name )
						return results[index + 1];
				}
			}

			return null;
		}

		protected override bool RenameResult(string newName)
		{
			Result[] results = GetSelectedResults();
			if ( results.Length != 1 )
				return false;
			Result result = results[0];

			object data = result.Data;
			string oldName = result.Text;

			if ( RenameResult(ref data, newName) )
			{
				// Reset the result.

				result.Data = data;
				result.Text = newName;

				// Give the node a chance to update the corresponding node.

				RenameResultNode(oldName, data);

				// Update the display.

				Refresh(false);
				SelectResult(newName);
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool PromptForDeleteResult(Result result)
		{
			return Snapin.MessageBox(GetDeleteResultPromptText(result.Text), "Confirm deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
		}

		private bool PromptForDeleteResults()
		{
			return Snapin.MessageBox(GetDeleteResultsPromptText(), "Confirm deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
		}

		#endregion

		#region Virtual Methods

		protected virtual void AddColumns()
		{
		}

		protected virtual void AddResults()
		{
		}

		protected virtual ListViewMode GetResultViewMode()
		{
			return ListViewMode.Report;
		}

		protected virtual string GetDeleteResultPromptText(string name)
		{
			return "Are you sure you want to delete '" + name + "'?";
		}

		protected virtual string GetDeleteResultsPromptText()
		{
			return "Are you sure you want to delete these elements?";
		}

		protected virtual void DeleteResult(object data)
		{
			Debug.Fail("DeleteResult was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");
		}

		protected virtual void DeleteResultNode(object data)
		{
		}

		protected virtual bool RenameResult(ref object data, string newName)
		{
			Debug.Fail("RenameResult was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");

			return false;
		}

		protected virtual void RenameResultNode(string oldName, object data)
		{
		}

		protected virtual ObjectPropertyForm CreateResultPropertyForm(object data)
		{
			Debug.Fail("CreateResultPropertyForm was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");

			return null;
		}

		protected virtual void ApplyResultProperties(object obj)
		{
			Debug.Fail("ApplyResultProperties was called, but is not overridden in the derived class, '"
				+ GetType().FullName + "'.");
		}

		protected virtual void EnableResultVerbs()
		{
		}

		#endregion

		#region Display

		protected override bool IsSelected
		{
			get
			{
				// Would like to use m_nodeSelected but there are times
				// when both m_nodeSelected and m_resultSelected can both
				// be false, whilst m_resultSelected seems to be OK.

				return !m_resultSelected;
			}
		}

		protected internal override bool IsResultSelected
		{
			get { return m_resultSelected; }
		}

		protected virtual bool EnableMultiSelect
		{
			get { return false; }
		}

		protected object GetResultData(string name)
		{
			Result result = m_results[name];
			return (result == null ? null : result.Data);
		}

		protected string GetResultName(object data)
		{
			foreach ( Result result in m_results )
			{
				if ( object.Equals(result.Data, data) )
					return result.Name;
			}

			return null;
		}

		protected object GetSelectedResultData()
		{
			return GetSelectedResultDatas()[0];
		}

		protected object[] GetSelectedResultDatas()
		{
			Result[] results = GetSelectedResults();
			object[] datas = new object[results.Length];
			for ( int index = 0; index < results.Length; ++index )
				datas[index] = results[index].Data;
			return datas;
		}

		protected string GetSelectedResultName()
		{
			return GetSelectedResults()[0].Name;
		}

		/// <summary>
		/// Populates the ResultDataItem with the appropriate information. In this case,
		/// it is the information from the ListView.
		/// </summary>
		/// <param name="resultDataItem"></param>
		internal override void GetDisplayInfo(ref ResultDataItem resultDataItem)
		{
			bool bCallbase = true;

			if ( (resultDataItem.Mask & (uint) ResultDataItemMask.Str) > 0 )
			{
				if ( m_results != null )
				{
					// Determine which result.

					int resultParam = GetResultParamFromItemParam(resultDataItem.Param);
					if ( resultParam >= 0 && resultParam < m_results.Count )
					{
						// Determine which column.

						if ( resultDataItem.Col == 0 )
							resultDataItem.Str = Marshal.StringToCoTaskMemUni(m_results.GetByParam(resultParam).Text);
						else
							resultDataItem.Str = Marshal.StringToCoTaskMemUni(m_results.GetByParam(resultParam).SubTexts[resultDataItem.Col - 1]);
						bCallbase = false;
					}
				}
			}

			// Use the correct item image.

			if ( (resultDataItem.Mask & (uint) ResultDataItemMask.Image) > 0 )
			{
				if ( m_results != null )
				{
					// Determine which result.

					int resultParam = GetResultParamFromItemParam(resultDataItem.Param);
					if ( resultParam >= 0 && resultParam < m_results.Count )
					{
						Result result = m_results.GetByParam(resultParam);
						resultDataItem.Image = IsUseSmallIcons() ? result.SmallImageIndex : result.LargeImageIndex;
						bCallbase = false;
					}
				}
			}

			if ( bCallbase )
				base.GetDisplayInfo(ref resultDataItem);
		}		

		private Result[] GetSelectedResults()
		{
			ArrayList results = new ArrayList();

			ResultDataItem rdi = GetSelectedResultItem(-1);
			while ( rdi.Index != -1 )
			{
				results.Add(m_results.GetByParam(GetResultParamFromItemParam(rdi.Param)));
				rdi = GetSelectedResultItem(rdi.Index);
			}

			return (Result[]) results.ToArray(typeof(Result));
		}

		private ResultDataItem GetSelectedResultItem(int index) 
		{
			ResultDataItem rdi = new ResultDataItem();
			rdi.Mask = (uint) ResultDataItemMask.State;
			rdi.Col	= 0;
			rdi.Index = index;
			rdi.State = (uint) ListViewItemState.Selected;
			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			resultData.GetNextItem(ref rdi);
			return rdi;
		}

		private Result[] GetResults()
		{
			ArrayList results = new ArrayList();

			ResultDataItem rdi = GetNextResultItem(-1);
			while ( rdi.Index != -1 )
			{
				results.Add(m_results.GetByParam(GetResultParamFromItemParam(rdi.Param)));
				rdi = GetNextResultItem(rdi.Index);
			}

			return (Result[]) results.ToArray(typeof(Result));
		}

		private ResultDataItem GetNextResultItem(int index) 
		{
			ResultDataItem rdi = new ResultDataItem();
			rdi.Mask = (uint) ResultDataItemMask.State;
			rdi.Col	= 0;
			rdi.Index = index;
			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			resultData.GetNextItem(ref rdi);
			return rdi;
		}

        private void DeleteResultItem(IntPtr id)
		{
			IResultData resultData = Snapin.ResultViewConsole as IResultData;
			resultData.DeleteItem(id, 0);
		}

		private IntPtr GetItemParamFromResultParam(int resultparam)
		{
			return (IntPtr)((int)Cookie | (resultparam << 16));
		}

		private int GetResultParamFromItemParam(IntPtr itemparam)
		{
			return (int)itemparam >> 16;
		}

		internal override string GetResultViewType(ref int viewOptions)
		{
			// Stop tree nodes from showing up in the result pane.

			viewOptions = (int) MmcViewOptions.ExcludeScopeItemsFromList;
			if (EnableMultiSelect)
			{
				viewOptions |= (int) MmcViewOptions.MultiSelect;
			}

			return string.Empty;		
		}

		protected int AddResultImage(string image)
		{
			return Snapin.AddImage(image);
		}

		protected internal override void OnRefresh()
		{
			base.OnRefresh();

			if ( IsResultSelected )
			{
				Result[] results = GetSelectedResults();
				if ( results.Length == 1 )
				{
					Result result = results[0];
					string smallImage = string.Empty;
					string largeImage = string.Empty;
					string text = string.Empty;
					string[] subTexts = null;
					if ( RefreshResult(result.Name, result.Data, ref smallImage, ref largeImage, ref text, ref subTexts) )
						RefreshResult(result, smallImage, largeImage, text, subTexts);
				}
			}
		}

		#endregion

		#region Properties

		internal override void CreatePropertyForm(IPropertySheetCallback lpProvider, IntPtr handle)
		{
			if ( IsSelected )
			{
				// The node is selected so delegate to the base.

				base.CreatePropertyForm(lpProvider, handle);
			}
			else if ( IsResultSelected )
			{
				// Determine which result is selected.

				Result result = GetSelectedResults()[0];
				if ( result != null )
				{
					// Create the property sheet on a separate thread if it doesn't already exist.

					if ( result.PropertyForm == null )
					{
						ResultPropertyFormData data = new ResultPropertyFormData(result, this);
						Thread thread = new Thread(data.CreateThreadResultPropertyForm);
						thread.SetApartmentState(ApartmentState.STA);
						thread.Start();
					}
					else
					{
						result.PropertyForm.Activate();
					}
				}
			}
		}

		private void CreateThreadResultPropertyForm(Result result)
		{
			Application.ThreadException += Snapin.Application_ThreadException;

			result.PropertyForm = CreateResultPropertyForm(result.Data);
			if ( result.PropertyForm != null )
			{
				result.PropertyForm.Apply += ApplyResultHandler;
				result.PropertyForm.ShowDialog();
				result.PropertyForm = null;
			}
		}

		private void ApplyResultHandler(object sender, ApplyEventArgs args)
		{
			try
			{
				ApplyResultProperties(args.Object);
			}
			catch ( Exception ex )
			{
				new ExceptionDialog(ex, "The following exception has occurred:").ShowDialog(Snapin);
			}

			// Refresh the display.

			try
			{
				InvokeRefreshResults();
			}
			catch ( Exception )
			{
			}
		}

		#endregion

		private class ResultPropertyFormData
		{
			public ResultPropertyFormData(Result result, ResultNode node)
			{
				m_result = result;
				m_node = node;
			}

			public Result Result
			{
				get { return m_result; }
			}

			public void CreateThreadResultPropertyForm()
			{
				m_node.CreateThreadResultPropertyForm(m_result);
			}

			private Result m_result;
			private ResultNode m_node;
		}

		private int m_currentColumns;
		private Results m_results;
		private bool m_resultSelected;
	}
}
