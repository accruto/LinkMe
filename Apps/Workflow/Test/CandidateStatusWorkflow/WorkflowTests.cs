using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;
using LinkMe.Workflow.Design.CandidateStatusWorkflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Workflow.Test.CandidateStatusWorkflow
{
    [TestClass]
    public class WorkflowTests
    {
        private const double Delay = 1000;

        private WorkflowRuntime _workflowRuntime;
        private ManualWorkflowSchedulerService _scheduler;
        private PropertyTrackingService _tracking;
        private MockDataExchange _dataExchange;

        [TestInitialize]
        public void TestInitialize()
        {
            _workflowRuntime = new WorkflowRuntime();
            _scheduler = new ManualWorkflowSchedulerService();
            _tracking = new PropertyTrackingService();

            _workflowRuntime.AddService(_scheduler);
            _workflowRuntime.AddService(_tracking);
            _workflowRuntime.AddService(new ExternalDataExchangeService());

            _dataExchange = new MockDataExchange(Delay, 2 * Delay + Delay);
            _workflowRuntime.GetService<ExternalDataExchangeService>().AddService(_dataExchange);

            _workflowRuntime.StartRuntime();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _workflowRuntime.StopRuntime();
            _workflowRuntime.Dispose();
        }

        #region No Response Tests

        [TestMethod]
        public void TestPassiveRemainsPassive()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.Passive, false);

            // Running from passive means it runs to completion with no emails.

            RunWorkflow(workflow);

            Assert.AreEqual(State.Passive, _dataExchange.State);
            Assert.AreEqual(0, _dataExchange.Emails.Count);
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestLookingDegradesToPassive()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last());
            Sleep(2 * Delay);

            // No reply means becomes passive with notification.

            RunWorkflow(workflow);
            Assert.AreEqual(State.Passive, _dataExchange.State);
            Assert.AreEqual(2, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.PassiveNotification, _dataExchange.Emails.Last());
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableDegradesToPassive()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last());
            Sleep(2 * Delay);

            // Degrade to actively looking with notification.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(2, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.LookingNotification, _dataExchange.Emails.Last());
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(3, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last());
            Sleep(2 * Delay);

            // No reply means becomes passive with notification.

            RunWorkflow(workflow);
            Assert.AreEqual(State.Passive, _dataExchange.State);
            Assert.AreEqual(4, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.PassiveNotification, _dataExchange.Emails.Last());
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        #endregion

        #region External Change Tests

        [TestMethod]
        public void TestLookingChangesToPassive()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);

            // Interrupt that delay.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.Passive);
            Sleep(2 * Delay);

            // Run to completion with no emails.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(0, _dataExchange.Emails.Count);
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestLookingChangesToAvailable()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);

            // Interrupt that delay.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.AvailableNow); // external change
            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(0, _dataExchange.Emails.Count); // no emails
            Assert.AreEqual(State.AvailableNow, GetWorkflowProperties(workflow)["State"]); // workflow is in the available state
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableChangesToPassive()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);

            // Interrupt that delay which completes the workflow.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.Passive);
            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(0, _dataExchange.Emails.Count); // no emails
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableChangesToLooking()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);

            // Interrupt that delay.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.ActivelyLooking); // external change
            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(0, _dataExchange.Emails.Count); // no emails
            Assert.AreEqual(State.ActivelyLooking, GetWorkflowProperties(workflow)["State"]); // workflow is in the available state
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestLookingChangesToPassive2()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last());

            // Interrupt which should complete the workflow.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.Passive);
            RunWorkflow(workflow);

            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestLookingChangesToAvailable2()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last());

            // Interrupt it up to available now.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.AvailableNow); // external change
            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            Assert.AreEqual(State.AvailableNow, GetWorkflowProperties(workflow)["State"]); // workflow is in the available state
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableChangesToPassive2()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last());

            // Interrupt it to passive which should complete the workflow.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.Passive); // external change
            RunWorkflow(workflow);

            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableChangesToLooking2()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last());

            // Interrupt it down to actively looking.

            _dataExchange.RaiseStatusChanged(workflow.InstanceId, State.ActivelyLooking); // external change
            RunWorkflow(workflow);

            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            Assert.AreEqual(State.ActivelyLooking, GetWorkflowProperties(workflow)["State"]); // workflow is in the available state
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        #endregion

        #region Confirmation Tests

        [TestMethod]
        public void TestLookingClicksLooking()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last());

            // Confirm looking.

            _dataExchange.OnActivelyLookingConfirmed(workflow.InstanceId); // clicked email link
            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            Assert.AreEqual(State.ActivelyLooking, GetWorkflowProperties(workflow)["State"]); // workflow is in the looking state
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // still looking
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last()); // asking to confirm
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestLookingWithZeroTimeoutClicksLooking()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, true);

            // Run for first delay.

            RunWorkflow(workflow);

            // Interrupt that delay.

            _dataExchange.OnActivelyLookingConfirmed(workflow.InstanceId); // clicked email link
            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            Assert.AreEqual(State.ActivelyLooking, GetWorkflowProperties(workflow)["State"]); // workflow is in the looking state
            Sleep(2 * Delay);

            // Now should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // still looking
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last()); // asking to confirm
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestLookingClicksAvailable()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.ActivelyLooking, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.ActivelyLooking, _dataExchange.State); // still looking
            Assert.AreEqual(1, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmLookingRequest, _dataExchange.Emails.Last()); // asking to confirm

            _dataExchange.OnActivelyLookingUpgraded(workflow.InstanceId); // clicked email link
            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            var properties = GetWorkflowProperties(workflow);
            Assert.AreEqual(State.AvailableNow, properties["State"]); // workflow is in the looking state

            Sleep(2 * Delay);
            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // still available
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last()); // asking to confirm

            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableClicksAvailableShortReminder()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State);
            Assert.AreEqual(1, _dataExchange.Emails.Count);
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last());

            // Confirm with short reminder.

            _dataExchange.OnAvailableNowConfirmedWithShortReminder(workflow.InstanceId); // clicked email link
            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            var properties = GetWorkflowProperties(workflow);
            Assert.AreEqual(State.AvailableNow, properties["State"]); // workflow is in the available state
            Assert.IsFalse((bool)properties["UseAvailableNowLongTimeout"]);
            Sleep(2 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // still looking
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last()); // asking to confirm
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableWithZeroTimeoutClicksAvailableShortReminder()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, true);

            // Run for first delay.

            RunWorkflow(workflow);
            _dataExchange.OnAvailableNowConfirmedWithShortReminder(workflow.InstanceId); // clicked email link

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            var properties = GetWorkflowProperties(workflow);
            Assert.AreEqual(State.AvailableNow, properties["State"]); // workflow is in the available state
            Assert.IsFalse((bool)properties["UseAvailableNowLongTimeout"]);
            Assert.IsFalse((bool)properties["IgnoreTimeoutOnce"]); // no more ignored timeouts
            Sleep(2 * Delay);

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // still looking
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last()); // asking to confirm
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableClicksAvailableLongReminder()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, false);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);
            
            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // still available
            Assert.AreEqual(1, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last()); // asking to confirm

            // Confirm.

            _dataExchange.OnAvailableNowConfirmedWithLongReminder(workflow.InstanceId); // clicked email link
            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            var properties = GetWorkflowProperties(workflow);
            Assert.AreEqual(State.AvailableNow, properties["State"]); // workflow is in the available state
            Assert.IsTrue((bool)properties["UseAvailableNowLongTimeout"]);
            Sleep(3 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // still available
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last()); // asking to confirm
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestAvailableWithZeroTimeoutClicksAvailableLongReminder()
        {
            var candidateId = Guid.NewGuid();
            var workflow = CreateWorkflow(candidateId, State.AvailableNow, true);

            // Run for first delay.

            RunWorkflow(workflow);
            _dataExchange.OnAvailableNowConfirmedWithLongReminder(workflow.InstanceId); // clicked email link

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // not changed by the workflow
            Assert.AreEqual(1, _dataExchange.Emails.Count); // no new emails
            var properties = GetWorkflowProperties(workflow);
            Assert.AreEqual(State.AvailableNow, properties["State"]); // workflow is in the available state
            Assert.IsTrue((bool)properties["UseAvailableNowLongTimeout"]);
            Assert.IsFalse((bool)properties["IgnoreTimeoutOnce"]); // no more ignored timeouts
            Sleep(3 * Delay);

            // Should get reminder email.

            RunWorkflow(workflow);
            Assert.AreEqual(State.AvailableNow, _dataExchange.State); // still available
            Assert.AreEqual(2, _dataExchange.Emails.Count); // got email
            Assert.AreEqual(MockEmail.ConfirmAvailableRequest, _dataExchange.Emails.Last()); // asking to confirm
            Assert.IsFalse(_dataExchange.IsComplete);
        }

        #endregion

        #region Private Methods

        private WorkflowInstance CreateWorkflow(Guid candidateId, State state, bool ignoreTimeoutOnce)
        {
            _dataExchange.Reset(state);

            var parameters = new Dictionary<string, object>
            {
                { "CandidateId", candidateId },
                { "State", state },
            };
            if (ignoreTimeoutOnce)
                parameters.Add("IgnoreTimeoutOnce", true);

            var workflow = _workflowRuntime.CreateWorkflow(typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.Workflow), parameters);
            workflow.Start();
            return workflow;
        }

        private void RunWorkflow(WorkflowInstance workflow)
        {
            _scheduler.RunWorkflow(workflow.InstanceId);
        }

        private static void Sleep(double interval)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(interval));
        }

        private Dictionary<string , object> GetWorkflowProperties(WorkflowInstance workflow)
        {
            return _tracking.GetWorkflowProperties(workflow.InstanceId);
        }

        #endregion
    }
}
