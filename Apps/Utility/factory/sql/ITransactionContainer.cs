
namespace LinkMe.Utility.Factory.Sql
{
	/// <summary>
	/// Summary description for ITransactionContainer.
	/// </summary>
	public interface ITransactionContainer
	{
		object GetData(string name);
		void SetData(string name, object data);
		void Remove(string name);
	}
}
