using LinkMe.Apps.Agents.Users;
using LinkMe.Apps.Agents.Users.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Members.Visitors
{
    [TestClass]
    public class VisitorStatusTests
        : TestClass
    {
        private readonly IVisitorStatusQuery _visitorStatusQuery = Resolve<IVisitorStatusQuery>();
        private readonly int[] _occasionalPrompts = new[] { 3, 10, 25 };
        private readonly int[] _casualPrompts = new[] { 3, 10 };

        [TestMethod]
        public void TestSearches()
        {
            var search = 0;

            // Less than the first prompt is a new visitor.
            
            for (; search < _occasionalPrompts[0]; ++search)
                AssertStatus(VisitorFrequency.New, false, _visitorStatusQuery.GetVisitorStatus(search, 0, 0, 0));

            // Prompt on the next search and frequency changes.

            AssertStatus(VisitorFrequency.Occasional, true, _visitorStatusQuery.GetVisitorStatus(search, 0, 0, 0));

            // Look for next prompts.

            var prompt = 1;
            for (; prompt < _occasionalPrompts.Length; ++prompt)
            {
                // No prompt.

                ++search;
                for (; search < _occasionalPrompts[prompt]; ++search)
                    AssertStatus(VisitorFrequency.Occasional, false, _visitorStatusQuery.GetVisitorStatus(search, 0, prompt, 0));

                // Prompt.

                AssertStatus(VisitorFrequency.Occasional, true, _visitorStatusQuery.GetVisitorStatus(search, 0, prompt, 0));
            }

            // After that no prompt.

            ++search;
            for (; search < 100; ++search)
                AssertStatus(VisitorFrequency.Occasional, false, _visitorStatusQuery.GetVisitorStatus(search, 0, prompt, 0));
        }

        [TestMethod]
        public void TestApplications()
        {
            var applications = 0;

            // Less than the first prompt is a new visitor.
            
            for (; applications < _casualPrompts[0]; ++applications)
                AssertStatus(VisitorFrequency.New, false, _visitorStatusQuery.GetVisitorStatus(0, applications, 0, 0));

            // Prompt on the next search and frequency changes.

            AssertStatus(VisitorFrequency.Casual, true, _visitorStatusQuery.GetVisitorStatus(0, applications, 0, 0));

            // Look for next prompts.

            var prompt = 1;
            for (; prompt < _casualPrompts.Length; ++prompt)
            {
                // No prompt.

                ++applications;
                for (; applications < _casualPrompts[prompt]; ++applications)
                    AssertStatus(VisitorFrequency.Casual, false, _visitorStatusQuery.GetVisitorStatus(0, applications, 0, prompt));

                // Prompt.

                AssertStatus(VisitorFrequency.Casual, true, _visitorStatusQuery.GetVisitorStatus(0, applications, 0, prompt));
            }

            // After that no prompt.

            ++applications;
            for (; applications < 100; ++applications)
                AssertStatus(VisitorFrequency.Casual, false, _visitorStatusQuery.GetVisitorStatus(0, applications, 0, prompt));
        }

        [TestMethod]
        public void TestApplicationsOverSearches()
        {
            // First occasional prompt.

            var search = 0;
            for (; search < _occasionalPrompts[0]; ++search)
                AssertStatus(VisitorFrequency.New, false, _visitorStatusQuery.GetVisitorStatus(search, 0, 0, 0));
            AssertStatus(VisitorFrequency.Occasional, true, _visitorStatusQuery.GetVisitorStatus(search, 0, 0, 0));

            // First casual prompt.

            var applications = 0;
            for (; applications < _casualPrompts[0]; ++applications)
                AssertStatus(VisitorFrequency.Occasional, false, _visitorStatusQuery.GetVisitorStatus(0, applications, 1, 0));
            AssertStatus(VisitorFrequency.Casual, true, _visitorStatusQuery.GetVisitorStatus(0, applications, 1, 0));

            // Look for next occasional prompts which don't come.

            var prompt = 1;
            for (; prompt < _occasionalPrompts.Length; ++prompt)
            {
                ++search;
                for (; search < _occasionalPrompts[prompt]; ++search)
                    AssertStatus(VisitorFrequency.Casual, false, _visitorStatusQuery.GetVisitorStatus(search, 0, prompt, 1));
                AssertStatus(VisitorFrequency.Casual, false, _visitorStatusQuery.GetVisitorStatus(search, 0, prompt, 1));
            }
        }

        private static void AssertStatus(VisitorFrequency expectedFrequency, bool expectedShouldPrompt, VisitorStatus status)
        {
            Assert.AreEqual(expectedFrequency, status.Frequency);
            Assert.AreEqual(expectedShouldPrompt, status.ShouldPrompt);
        }
    }
}