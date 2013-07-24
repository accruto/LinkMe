using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
    [TestClass]
    public class DatabaseHelperTest
        : TestClass
    {
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();

        [TestMethod]
        public void InlineParametersSqlInjection()
        {
            const string commandText = "SELECT displayName FROM dbo.NamedLocation WHERE displayName = @displayName";
            const string normalValue = "Tasmania";
            const string hackedSentinel = "SQL injection!";
            const string sqlInjectionValue = "'; INSERT INTO dbo.Sequence([Name], NextNumber) VALUES ('"
                + hackedSentinel + "', 0); --";
            const string checkCommandText = "SELECT [Name] FROM dbo.Sequence WHERE [Name] = '" + hackedSentinel + "'";

            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                // Clean the results of a previous failed test, if any.

                DatabaseHelper.ExecuteNonQuery(connection, "DELETE FROM dbo.Sequence WHERE [Name] = '" + hackedSentinel + "'");

                IDbCommand command = connection.CreateCommand();
                command.CommandText = commandText;
                DatabaseHelper.AddParameter(command, "@displayName", DbType.String, normalValue);

                // Check that the regular command works as expected.

                Assert.AreEqual(normalValue, command.ExecuteScalar());

                // Inline the parameters and check that it still works.

                DatabaseHelper.InlineParameters(command);
                Assert.AreEqual(normalValue, command.ExecuteScalar());

                // Now try some SQL injection.

                command.CommandText = commandText;
                DatabaseHelper.AddParameter(command, "@displayName", DbType.String, sqlInjectionValue);
                Assert.AreEqual(null, command.ExecuteScalar());
                Assert.AreEqual(null, DatabaseHelper.GetScalar(connection, checkCommandText));

                // Now try the same with inlining.

                DatabaseHelper.InlineParameters(command);
                Assert.AreEqual(null, command.ExecuteScalar());
                Assert.AreEqual(null, DatabaseHelper.GetScalar(connection, checkCommandText));
            }
        }

        [TestMethod]
        public void TestGetDisplayCommand()
        {
            // No parameters.

            var sqlCommand = new SqlCommand("SELECT * FROM table");

            Assert.AreEqual("SELECT * FROM table", DatabaseHelper.GetDisplayCommand(sqlCommand));

            // Guid, string.

            sqlCommand = new SqlCommand("SELECT * FROM table WHERE id = @id AND name = @name");
            sqlCommand.Parameters.AddWithValue("@id", new Guid("10DEC456-A741-41F0-982E-25919DA9590B"));
            sqlCommand.Parameters.AddWithValue("@name", "some'thing");

            Assert.AreEqual("SELECT * FROM table WHERE id = '10DEC456-A741-41F0-982E-25919DA9590B'"
                + " AND name = N'some''thing'", DatabaseHelper.GetDisplayCommand(sqlCommand));

            // Tricky parameter names, quotes, DBNULL.

            sqlCommand.CommandText = "SELECT '@id', 'name ''and @name' FROM table name = @name1 OR name = @name";
            sqlCommand.Parameters.AddWithValue("@name1", DBNull.Value);
            Assert.AreEqual("SELECT '@id', 'name ''and @name' FROM table name = NULL OR name = N'some''thing'",
                DatabaseHelper.GetDisplayCommand(sqlCommand));

            // Boolean, date/time, binary.

            var oleDbCommand = new OleDbCommand("INSERT INTO table VALUES (@bit, @date, @dateTime, @time, @binary");
            oleDbCommand.Parameters.AddWithValue("@bit", true);
            var dateTime = new DateTime(2006, 10, 3, 15, 20, 30, 40);
            DatabaseHelper.AddParameter(oleDbCommand, "@date", DbType.Date, dateTime);
            DatabaseHelper.AddParameter(oleDbCommand, "@dateTime", DbType.DateTime, dateTime);
            DatabaseHelper.AddParameter(oleDbCommand, "@time", DbType.Time, dateTime);
            oleDbCommand.Parameters.AddWithValue("@binary", new byte[] { 1, 32, 255 });

            Assert.AreEqual("INSERT INTO table VALUES (1, '2006-10-03', '2006-10-03 15:20:30.040',"
                + " '15:20:30.040', 0x0120FF", DatabaseHelper.GetDisplayCommand(oleDbCommand));
        }
    }
}