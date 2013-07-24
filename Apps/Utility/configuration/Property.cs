using System;

namespace LinkMe.Utility.Configuration
{
    // TODO: Remvoe this class and just reading using XmlReader instead of XmlSerializer.
	[Serializable]
	public class Property
	{
		private string name;
		private string value;

		public Property()
		{
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Value
		{
			get { return value; }
			set { this.value = value; }
		}
	}
}