namespace LinkMe.Web.UI.Controls.Common
{
	public class WorkflowScreenItem
	{
		public enum ItemState
		{
			Selected, 
			NotSelected,
			Link
		}

		private int number;
		private string text;
		private string hint;
		private string url;
		private ItemState state;

		public int Number
		{
			get { return number; }
			set { number = value; }
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public string Hint
		{
			get { return hint; }
			set { hint = value; }
		}

		public string Url
		{
			get { return url; }
			set { url = value; }
		}

		public ItemState State
		{
			get { return state; }
			set { state = value; }
		}

		public WorkflowScreenItem(int number, string text, string hint, string url, ItemState state)
		{
			this.number = number;
			this.text = text;
			this.hint = hint;
			this.url = url;
			this.state = state;			
		}
	}
}
