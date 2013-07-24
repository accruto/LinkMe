using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Configuration;
using LinkMe.TaskRunner.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks
{
	[TestClass]
	public class ExpireInactiveActivationsTaskTest : TaskTests
	{
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();
        private readonly IEmailVerificationsQuery _emailVerificationsQuery = Resolve<IEmailVerificationsQuery>();

		[TestMethod]
		public void TestTask()
		{
			int daysToExpire = ApplicationContext.Instance.GetIntProperty(
                ApplicationContext.EMAIL_ACTIVATION_EXPIRY_DAYS);
			
			DateTime dayExpired = DateTime.Now.Date.Subtract(TimeSpan.FromDays(daysToExpire));
			
            var random = new Random();
			string activationCode1 = random.Next().ToString();
            string activationCode2 = random.Next().ToString();
            string activationCode3 = random.Next().ToString();

			Member networker1 = _memberAccountsCommand.CreateTestMember("linkme50@test.linkme.net.au");
            Member networker2 = _memberAccountsCommand.CreateTestMember("linkme51@test.linkme.net.au");
            Member networker3 = _memberAccountsCommand.CreateTestMember("linkme52@test.linkme.net.au");
            networker1.IsActivated = false;
            networker2.IsActivated = false;
            networker3.IsActivated = false;
            _memberAccountsCommand.UpdateMember(networker1);
            _memberAccountsCommand.UpdateMember(networker2);
            _memberAccountsCommand.UpdateMember(networker3);

            var expiredActivationMinusOneDay = new EmailVerification { UserId = networker1.Id, VerificationCode = activationCode1, CreatedTime = dayExpired.AddDays(-1), EmailAddress = networker1.GetBestEmailAddress().Address };
            var expiredActivation = new EmailVerification { UserId = networker2.Id, VerificationCode = activationCode2, CreatedTime = dayExpired, EmailAddress = networker2.GetBestEmailAddress().Address };
            var expiredActivationPlusOneDay = new EmailVerification { UserId = networker3.Id, VerificationCode = activationCode3, CreatedTime = dayExpired.AddDays(1), EmailAddress = networker3.GetBestEmailAddress().Address };

			_emailVerificationsCommand.CreateEmailVerification(expiredActivationMinusOneDay);
            _emailVerificationsCommand.CreateEmailVerification(expiredActivation);
            _emailVerificationsCommand.CreateEmailVerification(expiredActivationPlusOneDay);
			
			var expiryTask = new ExpireInactiveActivationsTask();
			expiryTask.ExecuteTask();

            var expiredActivation1 = _emailVerificationsQuery.GetEmailVerification(networker1.Id, networker1.GetBestEmailAddress().Address);
            var expiredActivation2 = _emailVerificationsQuery.GetEmailVerification(networker2.Id, networker2.GetBestEmailAddress().Address);
            var expiredActivation3 = _emailVerificationsQuery.GetEmailVerification(networker3.Id, networker3.GetBestEmailAddress().Address);

			Assert.IsNull(expiredActivation1);
			Assert.IsNull(expiredActivation2);
			Assert.IsNotNull(expiredActivation3);
		}
	}
}
