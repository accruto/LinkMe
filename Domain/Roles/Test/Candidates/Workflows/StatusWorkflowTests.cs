using System;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Workflows
{
    [TestClass]
    public class StatusWorkflowTests
        : TestClass
    {
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidatesWorkflowCommand _candidatesWorkflowCommand = Resolve<ICandidatesWorkflowCommand>();
        private readonly ICandidatesWorkflowQuery _candidatesWorkflowQuery = Resolve<ICandidatesWorkflowQuery>();

        [TestInitialize]
        public void StatusWorkflowTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoWorkflow()
        {
            var candidate = new Candidate { Id = Guid.NewGuid(), Status = CandidateStatus.AvailableNow };
            _candidatesCommand.CreateCandidate(candidate);

            Assert.IsNull(_candidatesWorkflowCommand.GetStatusWorkflowId(candidate.Id));
            var without = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            Assert.AreEqual(1, without.Count);
            Assert.AreEqual(candidate.Id, without[0].Item1);
            Assert.AreEqual(candidate.Status, without[0].Item2);
        }

        [TestMethod]
        public void TestAddWorkflow()
        {
            var candidate = new Candidate { Id = Guid.NewGuid(), Status = CandidateStatus.AvailableNow };
            _candidatesCommand.CreateCandidate(candidate);

            var workflowId = Guid.NewGuid();
            _candidatesWorkflowCommand.AddStatusWorkflow(candidate.Id, workflowId);

            Assert.AreEqual(workflowId, _candidatesWorkflowCommand.GetStatusWorkflowId(candidate.Id));
            var without = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            Assert.AreEqual(0, without.Count);
        }

        [TestMethod]
        public void TestDeleteWorkflow()
        {
            var candidate = new Candidate { Id = Guid.NewGuid(), Status = CandidateStatus.AvailableNow };
            _candidatesCommand.CreateCandidate(candidate);

            var workflowId = Guid.NewGuid();
            _candidatesWorkflowCommand.AddStatusWorkflow(candidate.Id, workflowId);
            _candidatesWorkflowCommand.DeleteStatusWorkflow(candidate.Id);

            Assert.IsNull(_candidatesWorkflowCommand.GetStatusWorkflowId(candidate.Id));
            var without = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            Assert.AreEqual(1, without.Count);
            Assert.AreEqual(candidate.Id, without[0].Item1);
            Assert.AreEqual(candidate.Status, without[0].Item2);
        }

        [TestMethod]
        public void TestNoWorkflowWithSuggestedJobsWorkflow()
        {
            var candidate = new Candidate { Id = Guid.NewGuid(), Status = CandidateStatus.AvailableNow };
            _candidatesCommand.CreateCandidate(candidate);
            _candidatesWorkflowCommand.AddSuggestedJobsWorkflow(candidate.Id, Guid.NewGuid());

            Assert.IsNull(_candidatesWorkflowCommand.GetStatusWorkflowId(candidate.Id));
            var without = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            Assert.AreEqual(1, without.Count);
            Assert.AreEqual(candidate.Id, without[0].Item1);
            Assert.AreEqual(candidate.Status, without[0].Item2);
        }

        [TestMethod]
        public void TestAddWorkflowWithSuggestedJobsWorkflow()
        {
            var candidate = new Candidate { Id = Guid.NewGuid(), Status = CandidateStatus.AvailableNow };
            _candidatesCommand.CreateCandidate(candidate);
            _candidatesWorkflowCommand.AddSuggestedJobsWorkflow(candidate.Id, Guid.NewGuid());

            var workflowId = Guid.NewGuid();
            _candidatesWorkflowCommand.AddStatusWorkflow(candidate.Id, workflowId);

            Assert.AreEqual(workflowId, _candidatesWorkflowCommand.GetStatusWorkflowId(candidate.Id));
            var without = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            Assert.AreEqual(0, without.Count);
        }

        [TestMethod]
        public void TestDeleteWorkflowWithSuggestedJobsWorkflow()
        {
            var candidate = new Candidate { Id = Guid.NewGuid(), Status = CandidateStatus.AvailableNow };
            _candidatesCommand.CreateCandidate(candidate);
            _candidatesWorkflowCommand.AddSuggestedJobsWorkflow(candidate.Id, Guid.NewGuid());

            var workflowId = Guid.NewGuid();
            _candidatesWorkflowCommand.AddStatusWorkflow(candidate.Id, workflowId);
            _candidatesWorkflowCommand.DeleteStatusWorkflow(candidate.Id);

            Assert.IsNull(_candidatesWorkflowCommand.GetStatusWorkflowId(candidate.Id));
            var without = _candidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow();
            Assert.AreEqual(1, without.Count);
            Assert.AreEqual(candidate.Id, without[0].Item1);
            Assert.AreEqual(candidate.Status, without[0].Item2);
        }
    }
}
