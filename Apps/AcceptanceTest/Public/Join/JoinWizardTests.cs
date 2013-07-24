using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class JoinWizardTests
        : JoinTests
    {
        private class Step
        {
            public string Name;
            public bool IsCurrent;
            public bool IsEnabled;
            public bool IsManual;
        }

        [TestMethod]
        public void TestManualSteps()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            UpdateMember(member, Gender, DateOfBirth);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var resume = CreateResume();

            // When first starting the process the default is to upload your resume.

            var joinStep = new Step { Name = "Join", IsManual = false, IsEnabled = true };
            var personalDetailsStep = new Step { Name = "PersonalDetails", IsManual = false, IsEnabled = false };
            var jobDetailsStep = new Step { Name = "JobDetails", IsManual = false, IsEnabled = false };
            var activateStep = new Step { Name = "Activate", IsManual = false, IsEnabled = false };

            // Join.

            Get(GetJoinUrl());

            AssertUrl(GetJoinUrl());
            joinStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Personal details.

            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();

            // Now that the user has selected to create their resume manually it flips.

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            joinStep.IsManual = true;
            personalDetailsStep.IsManual = true;
            jobDetailsStep.IsManual = true;
            activateStep.IsManual = true;
            joinStep.IsCurrent = false;
            personalDetailsStep.IsCurrent = true;
            personalDetailsStep.IsEnabled = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Job details.

            SubmitPersonalDetails(instanceId, member, candidate, Password);
            
            AssertUrl(GetJobDetailsUrl(instanceId));
            personalDetailsStep.IsCurrent = false;
            jobDetailsStep.IsCurrent = true;
            jobDetailsStep.IsEnabled = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Activate.

            SubmitJobDetails(instanceId, member, candidate, resume, true, null, false);

            AssertUrl(GetActivateUrl(instanceId));
            jobDetailsStep.IsCurrent = false;
            activateStep.IsCurrent = true;
            activateStep.IsEnabled = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Go back.

            Get(GetJoinUrl(instanceId));
            AssertUrl(GetJoinUrl(instanceId));
            activateStep.IsCurrent = false;
            joinStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            Get(GetJobDetailsUrl(instanceId));
            AssertUrl(GetJobDetailsUrl(instanceId));
            joinStep.IsCurrent = false;
            jobDetailsStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            Get(GetPersonalDetailsUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));
            jobDetailsStep.IsCurrent = false;
            personalDetailsStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            Get(GetActivateUrl(instanceId));
            AssertUrl(GetActivateUrl(instanceId));
            personalDetailsStep.IsCurrent = false;
            activateStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);
        }

        [TestMethod]
        public void TestUploadSteps()
        {
            var testResume = TestResume.Complete;
            var parsedResume = testResume.GetParsedResume();

            var member = CreateMember(parsedResume.FirstName, parsedResume.LastName, parsedResume.EmailAddresses[0].Address);
            UpdateMember(member, parsedResume.PhoneNumbers[0].Number, parsedResume.PhoneNumbers[0].Type, parsedResume.Address.Location);
            UpdateMember(member, Gender, parsedResume.DateOfBirth);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            var resume = parsedResume.Resume;

            // When first starting the process the default is to upload your resume.

            var joinStep = new Step { Name = "Join", IsManual = false, IsEnabled = true };
            var personalDetailsStep = new Step { Name = "PersonalDetails", IsManual = false, IsEnabled = false };
            var jobDetailsStep = new Step { Name = "JobDetails", IsManual = false, IsEnabled = false };
            var activateStep = new Step { Name = "Activate", IsManual = false, IsEnabled = false };

            // Join.

            Get(GetJoinUrl());

            AssertUrl(GetJoinUrl());
            joinStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Personal details.

            UploadResume(testResume);
            var instanceId = GetInstanceId();
            joinStep.IsManual = false;

            AssertUrl(GetPersonalDetailsUrl(instanceId));
            joinStep.IsCurrent = false;
            personalDetailsStep.IsCurrent = true;
            personalDetailsStep.IsEnabled = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Job details.

            SubmitPersonalDetails(instanceId, member, candidate, Password);

            AssertUrl(GetJobDetailsUrl(instanceId));
            personalDetailsStep.IsCurrent = false;
            jobDetailsStep.IsCurrent = true;
            jobDetailsStep.IsEnabled = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Activate.

            SubmitJobDetails(instanceId, member, candidate, resume, true, null, true);

            AssertUrl(GetActivateUrl(instanceId));
            jobDetailsStep.IsCurrent = false;
            activateStep.IsCurrent = true;
            activateStep.IsEnabled = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            // Go back.

            Get(GetJoinUrl(instanceId));
            AssertUrl(GetJoinUrl(instanceId));
            activateStep.IsCurrent = false;
            joinStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            Get(GetJobDetailsUrl(instanceId));
            AssertUrl(GetJobDetailsUrl(instanceId));
            joinStep.IsCurrent = false;
            jobDetailsStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            Get(GetPersonalDetailsUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));
            jobDetailsStep.IsCurrent = false;
            personalDetailsStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);

            Get(GetActivateUrl(instanceId));
            AssertUrl(GetActivateUrl(instanceId));
            personalDetailsStep.IsCurrent = false;
            activateStep.IsCurrent = true;
            AssertSteps(joinStep, personalDetailsStep, jobDetailsStep, activateStep);
        }

        [TestMethod]
        public void TestJoinStep()
        {
            // Try to go to other than join page.

            Get(GetPersonalDetailsUrl(null));
            var instanceId = GetInstanceId();
            AssertUrl(GetJoinUrl(instanceId));

            Get(GetJobDetailsUrl(instanceId));
            AssertUrl(GetJoinUrl(instanceId));

            Get(GetActivateUrl(instanceId));
            AssertUrl(GetJoinUrl(instanceId));

            Get(GetJoinUrl(instanceId));
            AssertUrl(GetJoinUrl(instanceId));
        }

        [TestMethod]
        public void TestPersonalDetailsStep()
        {
            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            // Try to go to other than personal details page.

            Get(GetJobDetailsUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            Get(GetActivateUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            // Go to previous pages.

            Get(GetJoinUrl());
            AssertUrl(GetJoinUrl());

            Get(GetPersonalDetailsUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));
        }

        [TestMethod]
        public void TestJobDetailsStep()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();
            SubmitPersonalDetails(instanceId, member, candidate, Password);
            AssertUrl(GetJobDetailsUrl(instanceId));

            // Try to go to other than job details page.

            Get(GetActivateUrl(instanceId));
            AssertUrl(GetJobDetailsUrl(instanceId));

            // Go to previous pages.

            Get(GetJoinUrl());
            AssertUrl(GetJoinUrl());

            Get(GetPersonalDetailsUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            Get(GetJobDetailsUrl(instanceId));
            AssertUrl(GetJobDetailsUrl(instanceId));
        }

        [TestMethod]
        public void TestActivateStep()
        {
            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            UpdateMember(member, Gender, DateOfBirth);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);

            Get(GetJoinUrl());
            Browser.Submit(_joinFormId);
            var instanceId = GetInstanceId();
            SubmitPersonalDetails(instanceId, member, candidate, Password);
            SubmitJobDetails(instanceId, member, candidate, CreateResume(), true, null, false);
            AssertUrl(GetActivateUrl(instanceId));

            // Go to previous pages.

            Get(GetJoinUrl());
            AssertUrl(GetJoinUrl());

            Get(GetPersonalDetailsUrl(instanceId));
            AssertUrl(GetPersonalDetailsUrl(instanceId));

            Get(GetJobDetailsUrl(instanceId));
            AssertUrl(GetJobDetailsUrl(instanceId));

            Get(GetActivateUrl(instanceId));
            AssertUrl(GetActivateUrl(instanceId));
        }

        private void AssertSteps(params Step[] expectedSteps)
        {
            var steps = GetSteps();
            Assert.AreEqual(expectedSteps.Length, steps.Count);
            for (var index = 0; index < expectedSteps.Length; ++index)
            {
                Assert.AreEqual(expectedSteps[index].Name, steps[index].Name, "Expected name '" + expectedSteps[index].Name + "' for step " + index + " but got '" + steps[index].Name + "'.");
                Assert.AreEqual(expectedSteps[index].IsCurrent, steps[index].IsCurrent, "Expected current '" + expectedSteps[index].IsCurrent + "' for step " + index + " but got '" + steps[index].IsCurrent + "'.");
                Assert.AreEqual(expectedSteps[index].IsEnabled, steps[index].IsEnabled, "Expected enabled '" + expectedSteps[index].IsEnabled + "' for step " + index + " but got '" + steps[index].IsEnabled + "'.");
                Assert.AreEqual(expectedSteps[index].IsManual, steps[index].IsManual, "Expected manual '" + expectedSteps[index].IsManual + "' for step " + index + " but got '" + steps[index].IsManual + "'.");
            }
        }

        private IList<Step> GetSteps()
        {
            var steps = new List<Step>();

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='wizard-steps']/div");
            Assert.IsNotNull(nodes);
            foreach (var node in nodes)
            {
                var isCurrent = false;
                var isEnabled = true;
                var isManual = false;

                var cssClasses = node.Attributes["class"].Value.Split(' ');
                Assert.AreEqual("step", cssClasses[0]);
                var name = cssClasses[1];

                for (var index = 2; index < cssClasses.Length; ++index)
                {
                    switch (cssClasses[index])
                    {
                        case "current":
                            isCurrent = true;
                            break;

                        case "disabled":
                            isEnabled = false;
                            break;

                        case "manually":
                            isManual = true;
                            break;
                    }
                }

                steps.Add(new Step { Name = name, IsCurrent = isCurrent, IsEnabled = isEnabled, IsManual = isManual });
            }

            return steps;
        }
    }
}
