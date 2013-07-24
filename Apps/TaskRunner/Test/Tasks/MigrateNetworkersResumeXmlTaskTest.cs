namespace LinkMe.TaskRunner.Test.Tasks
{
    /* TODO: case 3740 - update for the new schema.
	[TestClass]
	public class MigrateNetworkersResumeXmlTaskTest : BaseTaskTest
	{
		[TestMethod]
		public void TestExecute()
		{
			IUserProfileBroker userProfileBroker = Resolve<IUserProfileBroker>();

			Member one = GetNetworkerProfile("maireadstephensotd");
			userProfileBroker.SaveNewUserProfile(one);
			Member two = GetNetworkerProfile("linkme1");
			userProfileBroker.SaveNewUserProfile(two);
			Member three = GetNetworkerProfile("safdarfaghaniotd");
			userProfileBroker.SaveNewUserProfile(three);

			Assert.AreEqual(null, one.NetworkerResumeData);
			Assert.AreEqual(null, two.NetworkerResumeData);
			Assert.AreEqual(null, three.NetworkerResumeData);

			MigrateNetworkerResumeXMLTask migrateNetworkerResumeXMLTask = new MigrateNetworkerResumeXMLTask();
			migrateNetworkerResumeXMLTask.Execute();

			Member oneUpdated = TestObjectMother.GetMemberByLoginId(one.LoginId);
			Member twoUpdated = TestObjectMother.GetMemberByLoginId(two.LoginId);
			Member threeUpdated = TestObjectMother.GetMemberByLoginId(three.LoginId);

			Assert.IsNotNull(oneUpdated.NetworkerResumeData);
			Assert.IsNotNull(twoUpdated.NetworkerResumeData);
			Assert.IsNotNull(threeUpdated.NetworkerResumeData);

			Assert.IsNotNull(oneUpdated.Resume);
			Assert.IsNotNull(twoUpdated.Resume);
			Assert.IsNotNull(threeUpdated.Resume);
		}
	}*/
}