using System.Web;

namespace LinkMe.Utility.Factory.Sql
{

	public class HttpContextTransactionContainer : ITransactionContainer
	{
		public HttpContextTransactionContainer()
		{
		}

		public object GetData(string name)
		{
			return HttpContext.Current.Items[name];
		}

		public void SetData(string name, object data)
		{
			HttpContext.Current.Items[name] = data;
		}

		public void Remove(string name)
		{
			HttpContext.Current.Items.Remove(name);
		}

	}
}
