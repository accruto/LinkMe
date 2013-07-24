using System.Data.SqlClient;


namespace LinkMe.Utility.Factory.Sql
{
	public interface ISqlConnectionService
	{
		SqlConnection SqlConnection { set; get; }
		SqlTransaction Transaction {get;set;}
	}
}
