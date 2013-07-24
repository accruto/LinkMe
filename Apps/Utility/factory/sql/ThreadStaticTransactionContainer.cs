using System;
using System.Collections;

namespace LinkMe.Utility.Factory.Sql
{
	
	public class ThreadStaticTransactionContainer : ITransactionContainer
	{
		[ThreadStatic] private static Hashtable sessionData =  null; 

		public ThreadStaticTransactionContainer()
		{
		}

		private static Hashtable SessionData
		{
			get
			{
				if(sessionData == null)
				{
					sessionData = new Hashtable();
				}
				return sessionData;
			}
		}

		public object GetData(string name)
		{
			if(SessionData.ContainsKey(name))
			{
				return SessionData[name];
			}
			else
			{
				return null;
			}
		}

		public void SetData(string name, object data)
		{
			SessionData[name] = data;
		}

		public void Remove(string name)
		{
			SessionData.Remove(name);
		}

	}
}
