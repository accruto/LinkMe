using System.Workflow.Activities;
using LinkMe.Workflow.Design.PeriodicWorkflow;

namespace LinkMe.Workflow.Design.PeriodicWorkflow
{
    partial class PeriodicWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding2 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            this.DelayChanged = new System.Workflow.Activities.HandleExternalEventActivity();
            this.SetLastRunTime = new System.Workflow.Activities.CodeActivity();
            this.Run = new System.Workflow.Activities.CallExternalMethodActivity();
            this.Timeout = new System.Workflow.Activities.DelayActivity();
            this.DelayChangedEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.TimeoutEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.cancellationHandlerActivity1 = new System.Workflow.ComponentModel.CancellationHandlerActivity();
            this.Wait = new System.Workflow.Activities.ListenActivity();
            this.Complete = new System.Workflow.Activities.CallExternalMethodActivity();
            this.whileWantEmail = new System.Workflow.Activities.WhileActivity();
            // 
            // DelayChanged
            // 
            this.DelayChanged.EventName = "DelayChanged";
            this.DelayChanged.InterfaceType = typeof(LinkMe.Workflow.Design.PeriodicWorkflow.IDataExchange);
            this.DelayChanged.Name = "DelayChanged";
            this.DelayChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.DelayChanged_Invoked);
            // 
            // SetLastRunTime
            // 
            this.SetLastRunTime.Name = "SetLastRunTime";
            this.SetLastRunTime.ExecuteCode += new System.EventHandler(this.SetLastRunTime_ExecuteCode);
            // 
            // Run
            // 
            this.Run.InterfaceType = typeof(LinkMe.Workflow.Design.PeriodicWorkflow.IDataExchange);
            this.Run.MethodName = "Run";
            this.Run.Name = "Run";
            activitybind1.Name = "PeriodicWorkflow";
            activitybind1.Path = "UserId";
            workflowparameterbinding1.ParameterName = "userId";
            workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            activitybind2.Name = "PeriodicWorkflow";
            activitybind2.Path = "LastRunTime";
            workflowparameterbinding2.ParameterName = "lastRunTime";
            workflowparameterbinding2.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.Run.ParameterBindings.Add(workflowparameterbinding1);
            this.Run.ParameterBindings.Add(workflowparameterbinding2);
            // 
            // Timeout
            // 
            this.Timeout.Name = "Timeout";
            activitybind3.Name = "PeriodicWorkflow";
            activitybind3.Path = "Delay";
            this.Timeout.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            // 
            // DelayChangedEvent
            // 
            this.DelayChangedEvent.Activities.Add(this.DelayChanged);
            this.DelayChangedEvent.Name = "DelayChangedEvent";
            // 
            // TimeoutEvent
            // 
            this.TimeoutEvent.Activities.Add(this.Timeout);
            this.TimeoutEvent.Activities.Add(this.Run);
            this.TimeoutEvent.Activities.Add(this.SetLastRunTime);
            this.TimeoutEvent.Name = "TimeoutEvent";
            // 
            // cancellationHandlerActivity1
            // 
            this.cancellationHandlerActivity1.Name = "cancellationHandlerActivity1";
            // 
            // Wait
            // 
            this.Wait.Activities.Add(this.TimeoutEvent);
            this.Wait.Activities.Add(this.DelayChangedEvent);
            this.Wait.Name = "Wait";
            // 
            // Complete
            // 
            this.Complete.InterfaceType = typeof(LinkMe.Workflow.Design.PeriodicWorkflow.IDataExchange);
            this.Complete.MethodName = "CompleteWorkflow";
            this.Complete.Name = "Complete";
            activitybind4.Name = "PeriodicWorkflow";
            activitybind4.Path = "UserId";
            workflowparameterbinding3.ParameterName = "userId";
            workflowparameterbinding3.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.Complete.ParameterBindings.Add(workflowparameterbinding3);
            // 
            // whileWantEmail
            // 
            this.whileWantEmail.Activities.Add(this.Wait);
            this.whileWantEmail.Activities.Add(this.cancellationHandlerActivity1);
            ruleconditionreference1.ConditionName = "WantEmail";
            this.whileWantEmail.Condition = ruleconditionreference1;
            this.whileWantEmail.Name = "whileWantEmail";
            // 
            // PeriodicWorkflow
            // 
            this.Activities.Add(this.whileWantEmail);
            this.Activities.Add(this.Complete);
            this.Name = "PeriodicWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity SetLastRunTime;
        private EventDrivenActivity DelayChangedEvent;
        private EventDrivenActivity TimeoutEvent;
        private ListenActivity Wait;
        private DelayActivity Timeout;
        private HandleExternalEventActivity DelayChanged;
        private CallExternalMethodActivity Run;
        private CallExternalMethodActivity Complete;
        private System.Workflow.ComponentModel.CancellationHandlerActivity cancellationHandlerActivity1;
        private WhileActivity whileWantEmail;



















































    }
}