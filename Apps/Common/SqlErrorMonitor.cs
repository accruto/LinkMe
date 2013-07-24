using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LinkMe.Common
{
    /// <summary>
    /// Feature 6301 - short-term solution for detecting SQL timeouts.
    /// </summary>
    public static class SqlErrorMonitor
    {
        // The DB is considered to be in an error state if timeoutThresholdCount or more timeouts occur within
        // timeoutThresholdMinutes minutes.
        private const int timeoutThresholdCount = 5;
        private const int timeoutThresholdMinutes = 5;

        private static readonly Queue<DateTime> timeoutTimes = new Queue<DateTime>(timeoutThresholdCount + 1);
        // Ingore timeouts between 1 AM and 2 AM.
        private static readonly TimeSpan ignoreTimeStart = new TimeSpan(1, 0, 0);
        private static readonly TimeSpan ignoreTimeEnd = new TimeSpan(2, 00, 0);

        /// <summary>
        /// Notifies the monitor of an error that was encountered by the application.
        /// </summary>
        public static void NotifyException(Exception ex)
        {
            // Check the whole exception tree for an SqlException.

            SqlException sqlEx = GetSqlException(ex);
            if (sqlEx == null)
                return;

            // Check if we're in the "ignore DB errors" period.

            if (IsBetweenIgnoreTimes(DateTime.Now))
                return;

            if (sqlEx.Message.StartsWith("Timeout expired."))
            {
                NotifyDbTimeout();
            }
        }

        private static bool IsBetweenIgnoreTimes(DateTime time)
        {
            return (time.TimeOfDay >= ignoreTimeStart && time.TimeOfDay <= ignoreTimeEnd);
        }

        public static bool IsDatabaseTimingOut()
        {
            DateTime leastRecentTimeout;

            lock (timeoutTimes)
            {
                if (timeoutTimes.Count < timeoutThresholdCount)
                    return false;

                leastRecentTimeout = timeoutTimes.Peek();
            }

            return (leastRecentTimeout > DateTime.Now.AddMinutes(timeoutThresholdMinutes * -1));
        }

        private static void NotifyDbTimeout()
        {
            lock (timeoutTimes)
            {
                // Store the time of the last time timeout (now) and trim the list if needed.

                timeoutTimes.Enqueue(DateTime.Now);

                while (timeoutTimes.Count > timeoutThresholdCount)
                {
                    timeoutTimes.Dequeue();
                }
            }
        }

        private static SqlException GetSqlException(Exception ex)
        {
            SqlException sqlEx = ex as SqlException;
            if (sqlEx != null)
                return sqlEx;

            if (ex != null)
                return GetSqlException(ex.InnerException);

            return null;
        }
    }
}
