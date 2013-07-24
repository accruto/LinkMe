using System;
using System.Collections.Generic;
using System.Threading;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Workflow.Test.PeriodicWorkflow
{
    [TestClass]
    public class WorkflowTests
    {
        private const double Delay = 1000;

        private WorkflowRuntime _workflowRuntime;
        private ManualWorkflowSchedulerService _scheduler;
        private MockDataExchange _dataExchange;

        [TestCleanup]
        public void TestCleanup()
        {
            _workflowRuntime.StopRuntime();
            _workflowRuntime.Dispose();
        }

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

        [TestMethod]
        public void TestKeepsSendingEmail()
        {
            var userId = Guid.NewGuid();
            var lastSentTime = DateTime.MinValue;
            var workflow = CreateWorkflow(userId);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run for next delay confirming previous run.

            RunWorkflow(workflow);
            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.IsFalse(_dataExchange.IsComplete);
            Assert.AreEqual(lastSentTime, _dataExchange.LastSentTime);
            lastSentTime = _dataExchange.LastSentTime;
            Sleep(2 * Delay);

            // Run for next delay confirming previous run.

            RunWorkflow(workflow);
            Assert.AreEqual(2, _dataExchange.Emails);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.IsFalse(_dataExchange.IsComplete);
            Assert.IsTrue(lastSentTime < _dataExchange.LastSentTime);
            lastSentTime = _dataExchange.LastSentTime;
            Sleep(2 * Delay);

            // Run for next delay confirming previous run.

            RunWorkflow(workflow);
            Assert.AreEqual(3, _dataExchange.Emails);
            Assert.AreEqual(userId, _dataExchange.UserId);
            Assert.IsFalse(_dataExchange.IsComplete);
            Assert.IsTrue(lastSentTime < _dataExchange.LastSentTime);
        }

        [TestMethod]
        public void TestCanChangeDelay()
        {
            var userId = Guid.NewGuid();
            var workflow = CreateWorkflow(userId);
            
            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run for next delay.

            RunWorkflow(workflow);
            Assert.AreEqual(1, _dataExchange.Emails);

            // Change delay.

            _dataExchange.RaiseDelayChanged(workflow.InstanceId, TimeSpan.FromMilliseconds(3 * Delay));
            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // No new emails.

            RunWorkflow(workflow);
            Assert.AreEqual(1, _dataExchange.Emails);
            Sleep(2 * Delay);

            // Now get email after delay.

            RunWorkflow(workflow);
            Assert.AreEqual(2, _dataExchange.Emails);
        }

        [TestMethod]
        public void TestCanCompleteWorkflow()
        {
            var userId = Guid.NewGuid();
            var workflow = CreateWorkflow(userId);

            // Run for first delay.

            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // Run for the next delay.

            RunWorkflow(workflow);
            Assert.AreEqual(1, _dataExchange.Emails);

            // Interrupt it.

            _dataExchange.RaiseDelayChanged(workflow.InstanceId, TimeSpan.MaxValue);
            RunWorkflow(workflow);
            Sleep(2 * Delay);

            // No new emails.

            Assert.AreEqual(1, _dataExchange.Emails);
            Assert.IsTrue(_dataExchange.IsComplete);
            Assert.AreEqual(userId, _dataExchange.UserId);
        }

        private WorkflowInstance CreateWorkflow(Guid userId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Delay", TimeSpan.FromMilliseconds(Delay) },
                { "UserId", userId },
                { "LastRunTime", DateTime.MinValue },
            };

            var workflow = _workflowRuntime.CreateWorkflow(typeof(LinkMe.Workflow.Design.PeriodicWorkflow.PeriodicWorkflow), parameters);
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
    }
}