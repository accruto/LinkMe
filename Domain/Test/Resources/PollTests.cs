using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Test.Resources
{
    [TestClass]
    public class PollTests
        : TestClass
    {
        private readonly IPollsCommand _pollsCommand = Resolve<IPollsCommand>();
        private readonly IPollsQuery _pollsQuery = Resolve<IPollsQuery>();

        private const string PollNameFormat = "Poll{0}";
        private const string PollQuestionFormat = "What is the {0} question?";
        private const string AnswerTextFormat = "This is the {1} answer for poll {0}.";

        [TestInitialize]
        public void PollTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreatePoll()
        {
            var poll0 = CreatePoll(0, 3, true);
            var poll1 = CreatePoll(1, 4, false);

            AssertPoll(poll0, _pollsQuery.GetPoll(poll0.Name));
            AssertPoll(poll0, _pollsQuery.GetActivePoll());
            AssertPoll(poll1, _pollsQuery.GetPoll(poll1.Name));
        }

        [TestMethod]
        public void TestVotes()
        {
            var poll = CreatePoll(0, 4, true);

            var userId1 = Guid.NewGuid();
            var userId2 = Guid.NewGuid();
            var userId3 = Guid.NewGuid();

            AssertVotes(poll, 0, 0, 0, 0);

            _pollsCommand.CreatePollAnswerVote(new PollAnswerVote { AnswerId = poll.Answers[0].Id, UserId = userId1 });
            AssertVotes(poll, 1, 0, 0, 0);

            _pollsCommand.CreatePollAnswerVote(new PollAnswerVote { AnswerId = poll.Answers[0].Id, UserId = userId1 });
            AssertVotes(poll, 1, 0, 0, 0);

            _pollsCommand.CreatePollAnswerVote(new PollAnswerVote { AnswerId = poll.Answers[1].Id, UserId = userId1 });
            AssertVotes(poll, 1, 0, 0, 0);

            _pollsCommand.CreatePollAnswerVote(new PollAnswerVote { AnswerId = poll.Answers[2].Id, UserId = userId2 });
            AssertVotes(poll, 1, 0, 1, 0);

            _pollsCommand.CreatePollAnswerVote(new PollAnswerVote { AnswerId = poll.Answers[2].Id, UserId = userId3 });
            AssertVotes(poll, 1, 0, 2, 0);
        }

        private void AssertVotes(Poll poll, params int[] expectedVotes)
        {
            var votes = _pollsQuery.GetPollAnswerVotes(poll.Id);
            Assert.AreEqual(expectedVotes.Length, votes.Count);
            for (var index = 0; index < expectedVotes.Length; ++index)
                Assert.AreEqual(expectedVotes[index], votes[poll.Answers[index].Id]);
        }

        private static void AssertPoll(Poll expectedPoll, Poll poll)
        {
            Assert.AreEqual(expectedPoll.Id, poll.Id);
            Assert.AreEqual(expectedPoll.IsActive, poll.IsActive);
            Assert.AreEqual(expectedPoll.Name, poll.Name);
            Assert.AreEqual(expectedPoll.Question, poll.Question);

            Assert.AreEqual(expectedPoll.Answers.Count, poll.Answers.Count);
            for (var index = 0; index < expectedPoll.Answers.Count; ++index)
                AssertAnswer(expectedPoll.Answers[index], poll.Answers[index]);
        }

        private static void AssertAnswer(PollAnswer expectedAnswer, PollAnswer answer)
        {
            Assert.AreEqual(expectedAnswer.Id, answer.Id);
            Assert.AreEqual(expectedAnswer.Order, answer.Order);
            Assert.AreEqual(expectedAnswer.Text, answer.Text);
        }

        private Poll CreatePoll(int index, int answers, bool isActive)
        {
            var poll = new Poll
            {
                Name = string.Format(PollNameFormat, index),
                Question = string.Format(PollQuestionFormat, index),
                Answers = CreatePollAnswers(index, answers),
                IsActive = isActive,
            };
            _pollsCommand.CreatePoll(poll);
            return poll;
        }

        private static IList<PollAnswer> CreatePollAnswers(int poll, int count)
        {
            return (from i in Enumerable.Range(0, count)
                    select new PollAnswer
                    {
                        Order = i,
                        Text = string.Format(AnswerTextFormat, poll, i),
                    }).ToList();
        }
        /*
        private void AssertVotes(string pollName, params Tuple<Guid, int>[] expectedVotes)
        {
            var answers = _pollsQuery.GetPollAnswers2(pollName);

            foreach (var expectedVote in expectedVotes)
            {
                var answerId = expectedVote.Item1;
                var votes = _pollsQuery.GetPollAnswerVotes2(answerId);

                Assert.AreEqual(expectedVote.Item2, answers.First(a => a.Id == answerId).VoteCount);
                Assert.AreEqual(expectedVote.Item2, votes);
            }
        }
         * */
    }
}
