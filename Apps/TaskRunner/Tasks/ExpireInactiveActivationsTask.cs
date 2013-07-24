using System;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks
{
	/// <summary>
    /// Deletes all the EmailVerification records older than "email.activation.expiry.days" setting.
	/// </summary>
	public class ExpireInactiveActivationsTask : Task
	{
        private readonly IDbConnectionFactory _connectionFactory = Container.Current.Resolve<IDbConnectionFactory>();

		public ExpireInactiveActivationsTask()
            : base(new EventSource<ExpireInactiveActivationsTask>())
		{
		}

        public override void ExecuteTask()
		{
            const string method = "ExecuteTask";

            const string sql = "DELETE FROM dbo.EmailVerification WHERE CreatedTime <= @expiryTime";

            // Use SQL here, because NHibernate LOADS all the objects to be deleted, including the attached Member
            // and all its collections, which takes a ridiculously long time. It does this even when you use Delete(hql)

            int daysToExpire = ApplicationContext.Instance.GetIntProperty(ApplicationContext.EMAIL_ACTIVATION_EXPIRY_DAYS);
            DateTime dayExpired = DateTime.Today.AddDays(daysToExpire * -1);

            int count;
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                count = DatabaseHelper.ExecuteNonQuery(connection, sql, "@expiryTime", dayExpired);
            }

            _eventSource.Raise(Event.Information, method, string.Format("{0} expired EmailVerification records were deleted.", count));
        }
	}
}
