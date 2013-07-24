using System;
using System.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
    [TestClass]
    public class SoundExTest
        : TestClass
    {
        private static readonly Random Random = new Random();

        [TestMethod]
        public void TestStatic()
        {
            Assert.AreEqual("T522", SoundEx.GenerateSoundEx("Tymczak"));
            Assert.AreEqual("A226", SoundEx.GenerateSoundEx("Ashcraft"));
            Assert.AreEqual("P123", SoundEx.GenerateSoundEx("Pfister"));
            Assert.AreEqual("J250", SoundEx.GenerateSoundEx("Jackson"));
            Assert.AreEqual("G362", SoundEx.GenerateSoundEx("Gutierrez"));
            Assert.AreEqual("V532", SoundEx.GenerateSoundEx("VanDeusen"));
            Assert.AreEqual("D250", SoundEx.GenerateSoundEx("Deusen"));
        }

        [TestMethod]
        public void TestStaticAgainstSqlServer()
        {
            using (IDbConnection conn = GetDbConnection())
            {
                conn.Open();

                using (IDbCommand command = conn.CreateCommand())
                {
                    InitialiseSqlCommand(command);

                    CheckAgainstSqlServer("This has @unhandled chars!", command);
                    CheckAgainstSqlServer("[starts with unhandled", command);
                }
            }
        }

        [TestMethod]
        public void TestRandomAgainstSqlServer()
        { 
            using (IDbConnection conn = GetDbConnection())
            {
                conn.Open();

                using (IDbCommand command = conn.CreateCommand())
                {
                    InitialiseSqlCommand(command);

                    for (int count = 0; count < 10000; count++)
                    {
                        CheckAgainstSqlServer(GetRandomString(), command);
                    }
                }
            }
        }

        private static IDbConnection GetDbConnection()
        {
            return Resolve<IDbConnectionFactory>().CreateConnection();
        }

        private static void CheckAgainstSqlServer(string text, IDbCommand command)
        {
            string codeSoundEx = SoundEx.GenerateSoundEx(text);

            ((IDbDataParameter)command.Parameters["@s"]).Value = text;
            var sqlSoundEx = (string)command.ExecuteScalar();

            Assert.AreEqual(sqlSoundEx, codeSoundEx, "SoundEx comparison failed for string"
                + " '{0}': generated value was '{1}', but SQL server returned '{2}'.",
                text, codeSoundEx, sqlSoundEx);
        }

        private static void InitialiseSqlCommand(IDbCommand command)
        {
            command.CommandText = "SELECT SOUNDEX(@s)";
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@s";
            parameter.DbType = DbType.String;
            command.Parameters.Add(parameter);
        }

        private static string GetRandomString()
        {
            const int minLength = 1;
            const int maxLength = 100;

            int length = Random.Next(minLength, maxLength + 1);
            var chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)Random.Next(0x40, 0x80);
            }

            return new string(chars);
        }
    }
}
