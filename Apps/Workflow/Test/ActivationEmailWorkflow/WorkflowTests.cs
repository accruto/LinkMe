using System;
using System.Collections.Generic;
using System.Threading;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Workflow.Test.ActivationEmailWorkflow
{
    [TestClass]
    public class WorkflowTests
    {
        private const double Delay = 1000;

        private WorkflowRuntime _workflowRuntime;
        private ManualWorkflowSchedulerService _scheduler;
        private MockDataExchange _dataExchange;

        #region SetUp

        [TestInitialize]
        public void TestInitialize()
        {
            _workflowRuntime = new WorkflowRuntime();
            _scheduler = new ManualWorkflowSchedulerService();

            _workflowRuntime.AddService(_scheduler);
            _workflowRuntime.AddService(new ExternalDataExchangeService());

            _dataExchange = new MockDataExchange();
            _dataExchange.Reset();
            _workflowRuntime.GetService<ExternalDataExchangeService>().AddService(_dataExchange);

            _workflowRuntime.StartRuntime();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _workflowRuntime.StopRuntime();
            _workflowRuntime.Dispose();

            _workflowRuntime = null;
            _scheduler = null;
            _dataExchange = null;
        }

        #endregion

        #region Test Cases

        [TestMethod]
        public void TestOneEmail()
        {
            var userId = Guid.NewGuid();
            _dataExchange.SetDelays(Delay);
            var workflow = CreateWorkflow(userId);

            // Run workflow for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run to completion.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestTwoEmails()
        {
            var userId = Guid.NewGuid();
            _dataExchange.SetDelays(Delay, Delay);
            var workflow = CreateWorkflow(userId);

            // Run workflow for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run workflow for the second delay, confirming the results of the first run.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.IsFalse(_dataExchange.IsComplete);
            Sleep(2 * Delay);

            // Run to completion.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(2, _dataExchange.Emails);
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestThreeEmails()
        {
            var userId = Guid.NewGuid();
            _dataExchange.SetDelays(Delay, Delay, 3 * Delay);
            var workflow = CreateWorkflow(userId);

            // Run workflow for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run workflow for the second delay, confirming the results of the first run.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.IsFalse(_dataExchange.IsComplete);
            Sleep(2 * Delay);

            // Run workflow for the third delay, confirming the results of the second run.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(2, _dataExchange.Emails);
            Assert.IsFalse(_dataExchange.IsComplete);
            Sleep(Delay);

            // No new emails so far.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(2, _dataExchange.Emails);
            Assert.IsFalse(_dataExchange.IsComplete);
            Sleep(2 * Delay);

            // Run to completion.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(3, _dataExchange.Emails);
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        [TestMethod]
        public void TestStopSending()
        {
            var userId = Guid.NewGuid();
            _dataExchange.SetDelays(Delay, Delay);
            var workflow = CreateWorkflow(userId);

            // Run workflow for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run workflow for the second delay, confirming the results of the first run.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.IsFalse(_dataExchange.IsComplete);

            // Interrupt it.

            _dataExchange.RaiseStopSending(workflow.InstanceId);
            Sleep(2 * Delay);

            // Run to completion with no new emails.

            RunWorkflow(workflow);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.IsTrue(_dataExchange.IsComplete);
        }

        #endregion

        #region Private Methods

        private WorkflowInstance CreateWorkflow(Guid userId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "UserId", userId },
            };

            var workflow = _workflowRuntime.CreateWorkflow(typeof(LinkMe.Workflow.Design.ActivationEmailWorkflow.ActivationEmailWorkflow), parameters);
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

        #endregion
    }
}
