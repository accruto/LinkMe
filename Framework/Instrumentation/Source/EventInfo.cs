namespace LinkMe.Framework.Instrumentation
{
	public class EventInfo
	{
		internal EventInfo(string name, bool isEnabled, int index)
		{
			_name = name;
			IsEnabled = isEnabled;
			Index = index;
		}

		internal string Name
		{
			get { return _name; }
		}

	    internal bool IsEnabled { get; set; }
        internal int Index { get; set; }

		private readonly string _name;
	}
}
