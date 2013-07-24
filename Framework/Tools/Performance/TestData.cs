using System.Data;
using System.IO;
using System.Reflection;

namespace LinkMe.Framework.Tools.Performance
{
    public class TestData
    {
        protected static IDbConnection CreateConnection(string connectionString)
        {
            return null; // SqlCommandUtil.CreateConnection(connectionString);
        }

        protected static void Execute(IDbConnection connection, string sql, params object[] parameters)
        {
            // Put in a long wait time because these jobs may be doing all sorts of things like deleting large number of users etc.

            //SqlCommandUtil.Execute(connection, 600, sql, parameters);
        }

        protected static IDataReader ExecuteReader(IDbConnection connection, string sql, params object[] parameters)
        {
            // Put in a long wait time because these jobs may be doing all sorts of things like deleting large number of users etc.

            return null; // SqlCommandUtil.ExecuteReader(connection, 600, sql, parameters);
        }

        protected static string LoadSql(Assembly assembly, string resource)
        {
            var stream = assembly.GetManifestResourceStream(resource);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
