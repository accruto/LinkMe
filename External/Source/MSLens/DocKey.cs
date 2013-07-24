using System;

namespace com.bgt.lens
{
	/// <summary>
	/// Wrapper class to store a resume/posting document key.
	/// </summary>
	public class DocKey
	{
		private ulong mKey;

		public DocKey(): this( 0 )
		{

		}

		public DocKey(ulong inKey) 
		{
			this.mKey = inKey;
		}

		public void SetKey(ulong inKey)
		{
			mKey = inKey;
		}

		public ulong GetKey()
		{
			return mKey;
		}

	}
}
