using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test
{
    public static class TestHelper
    {
        private static bool _isInternetAvailable;
        private static DateTime _internetLastChecked = DateTime.MinValue;
        private static readonly Random Random = new Random();
        private static readonly Regex LocalDataSourceRegex = new Regex(@"Data\ Source\s*=\s*[\'\""]?(\(LOCAL\)|localhost|127.0.0.1)[\'\""]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);


        /// <summary>
        /// Verify we're working with DEV environment only.
        /// </summary>
        public static void VerifyDevEnvironment(IDbConnectionFactory connectionFactory)
        {
            if (RuntimeEnvironment.Environment != ApplicationEnvironment.Dev)
            {
                throw new ApplicationException("The LinkMe environment on your machine is set to '"
                    + RuntimeEnvironment.EnvironmentName + "', not 'dev'. Please fix the '"
                        + RuntimeEnvironment.EnvironmentVariable + "' system variable on your machine!");
            }

            // And to be REALLY sure it's dev check that the DB connection string points to the "(LOCAL)"
            // DB server.

            var connectionString = connectionFactory.ConnectionString;
            if (!LocalDataSourceRegex.IsMatch(connectionString))
                throw new ApplicationException("The database connection string doesn't seem to be pointing to a local database. The value is:\r\n" + connectionString);
        }

        public static bool OneInXChance(int x)
        {
            return (Random.Next(x) == 0);
        }

        /// <summary>
        /// Throws an NUnit IgnoreException if run without an Internet connection. Used for tests that access external
        /// sites, such as webmail sites.
        /// </summary>
        public static void IgnoreTestIfInternetDown()
        {
            if (!IsInternetConnectionAvailable())
                throw new AssertInconclusiveException("This test requires an Internet connection.");
        }

        public static bool IsInternetConnectionAvailable()
        {
            const int checkIntervalSeconds = 60;

            // Don't check all the time, because it's slow. Just check ever 60 seconds and cache the results.

            if (_internetLastChecked < DateTime.Now.AddSeconds(checkIntervalSeconds * -1))
            {
                _isInternetAvailable = CanConnect();
                _internetLastChecked = DateTime.Now;
            }

            return _isInternetAvailable;
        }

        private static bool CanConnect()
        {
            using (var tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect("www.google.com", 80);
                    return true;
                }
                catch (SocketException)
                {
                    return false;
                }
            }
        }
    }
}
