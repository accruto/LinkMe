using System.Web;

namespace LinkMe.Utility.Factory.Sql
{
	
	public class SqlTransactionContainerFactory
	{
		private static ITransactionContainer transactionContainer;
		private static readonly SqlTransactionContainerFactory instance = new SqlTransactionContainerFactory();
		
		private object lockObject = new object();

		private SqlTransactionContainerFactory()
		{
		}

		public static SqlTransactionContainerFactory Instance
		{
			get { return instance; }
		}

		public object GetData(string name)
		{
			return TransactionContainer.GetData(name);
		}

		public void SetData(string name, object data)
		{
			this.TransactionContainer.SetData(name, data);
		}

		public void Remove(string name)
		{
			this.TransactionContainer.Remove(name);
		}

		private ITransactionContainer TransactionContainer
		{
			get
			{
				if(transactionContainer == null)
				{
					lock(lockObject)
					{
						if(transactionContainer == null) //striclty speaking this is an anti-pattern.
						{
							if(HttpContext.Current !=null)
							{
								transactionContainer = new HttpContextTransactionContainer();
							}
							else
							{
								transactionContainer = new ThreadStaticTransactionContainer();
							}
						}
					}
				}
				return transactionContainer;
			}
		}
	}
}
