using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Diaries
{
    [TestClass]
    public class DiariesTests
        : TestClass
    {
        private const string TitleFormat = "This is the {0} title";
        private const string DescriptionFormat = "This is the {0} description";

        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateDiariesQuery _candidateDiariesQuery = Resolve<ICandidateDiariesQuery>();
        private readonly ICandidateDiariesCommand _candidateDiariesCommand = Resolve<ICandidateDiariesCommand>();

        [TestMethod]
        public void TestCreateDiary()
        {
            // Create a candidate.

            var candidateId = Guid.NewGuid();
            _candidatesCommand.CreateCandidate(new Candidate { Id = candidateId });

            // Create it.

            var diary = new Diary();
            _candidateDiariesCommand.CreateDiary(candidateId, diary);
            Assert.AreNotEqual(Guid.Empty, diary.Id);

            // Get it.

            Assert.AreEqual(diary, _candidateDiariesQuery.GetDiary(diary.Id));
        }

        [TestMethod]
        public void TestCreateDiaryEntries()
        {
            // Create a diary.

            var candidateId = Guid.NewGuid();
            _candidatesCommand.CreateCandidate(new Candidate { Id = candidateId });

            var diary = new Diary();
            _candidateDiariesCommand.CreateDiary(candidateId, diary);

            // Add entries.

            var entries = new List<DiaryEntry>();
            for (var index = 0; index < 5; index++)
            {
                // Entries should be returned in reverse chronological order.

                var entry = CreateDiaryEntry(index);
                _candidateDiariesCommand.CreateDiaryEntry(diary.Id, entry);
                entries.Insert(0, entry);

                AssertAreEqual(entries, _candidateDiariesQuery.GetDiaryEntries(diary.Id));
            }
        }

        [TestMethod]
        public void TestCreateDiaryEntryNoStartTime()
        {
            // Create a diary.

            var candidateId = Guid.NewGuid();
            _candidatesCommand.CreateCandidate(new Candidate { Id = candidateId });

            var diary = new Diary();
            _candidateDiariesCommand.CreateDiary(candidateId, diary);

            // Add entries.

            var entries = new List<DiaryEntry>();

            var entry = CreateDiaryEntry(2);
            _candidateDiariesCommand.CreateDiaryEntry(diary.Id, entry);
            entries.Insert(0, entry);

            // Create entry with no start time, it should come last.

            entry = CreateDiaryEntry(1);
            entry.StartTime = null;
            _candidateDiariesCommand.CreateDiaryEntry(diary.Id, entry);
            entries.Add(entry);
            AssertAreEqual(entries, _candidateDiariesQuery.GetDiaryEntries(diary.Id));

            // Create another entry.

            entry = CreateDiaryEntry(0);
            _candidateDiariesCommand.CreateDiaryEntry(diary.Id, entry);
            entries.Insert(1, entry);
            AssertAreEqual(entries, _candidateDiariesQuery.GetDiaryEntries(diary.Id));
        }

        private static DiaryEntry CreateDiaryEntry(int index)
        {
            return new DiaryEntry
                       {
                           Title = string.Format(TitleFormat, index),
                           Description = string.Format(DescriptionFormat, index),
                           StartTime = DateTime.Now.Date.AddHours(index),
                           EndTime = DateTime.Now.Date.AddHours(1 + index),
                           TotalHours = index
                       };
        }

        private static void AssertAreEqual(IList<DiaryEntry> expectedEntries, IList<DiaryEntry> entries)
        {
            Assert.AreEqual(expectedEntries.Count, entries.Count);
            for (var index = 0; index < expectedEntries.Count; ++index)
                Assert.AreEqual(expectedEntries[index], entries[index]);
        }
    }
}