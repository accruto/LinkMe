using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace LinkMe.Workflow.Design.ActivationEmailWorkflow
{
    partial class ActivationEmailWorkflow
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
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding4 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding5 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding6 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding7 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            this.StopSending = new System.Workflow.Activities.HandleExternalEventActivity();
            this.GetNextDelay = new System.Workflow.Activities.CallExternalMethodActivity();
            this.IncrementSeqNo = new System.Workflow.Activities.CodeActivity();
            this.SendEmail = new System.Workflow.Activities.CallExternalMethodActivity();
            this.Timeout = new System.Workflow.Activities.DelayActivity();
            this.StopSendingEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.TimeoutEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.Wait = new System.Workflow.Activities.ListenActivity();
            this.CompleteWorkflow = new System.Workflow.Activities.CallExternalMethodActivity();
            this.WhileSendingEmail = new System.Workflow.Activities.WhileActivity();
            this.GetFirstDelay = new System.Workflow.Activities.CallExternalMethodActivity();
            // 
            // StopSending
            // 
            this.StopSending.EventName = "StopSending";
            this.StopSending.InterfaceType = typeof(LinkMe.Workflow.Design.ActivationEmailWorkflow.IDataExchange);
            this.StopSending.Name = "StopSending";
            this.StopSending.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.StopSending_Invoked);
            // 
            // GetNextDelay
            // 
            this.GetNextDelay.InterfaceType = typeof(LinkMe.Workflow.Design.ActivationEmailWorkflow.IDataExchange);
            this.GetNextDelay.MethodName = "GetNextDelay";
            this.GetNextDelay.Name = "GetNextDelay";
            activitybind1.Name = "ActivationEmailWorkflow";
            activitybind1.Path = "EmailSeqNo";
            workflowparameterbinding1.ParameterName = "emailSeqNo";
            workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            activitybind2.Name = "ActivationEmailWorkflow";
            activitybind2.Path = "Delay";
            workflowparameterbinding2.ParameterName = "(ReturnValue)";
            workflowparameterbinding2.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.GetNextDelay.ParameterBindings.Add(workflowparameterbinding1);
            this.GetNextDelay.ParameterBindings.Add(workflowparameterbinding2);
            // 
            // IncrementSeqNo
            // 
            this.IncrementSeqNo.Name = "IncrementSeqNo";
            this.IncrementSeqNo.ExecuteCode += new System.EventHandler(this.IncrementSeqNo_ExecuteCode);
            // 
            // SendEmail
            // 
            this.SendEmail.InterfaceType = typeof(LinkMe.Workflow.Design.ActivationEmailWorkflow.IDataExchange);
            this.SendEmail.MethodName = "SendEmail";
            this.SendEmail.Name = "SendEmail";
            activitybind3.Name = "ActivationEmailWorkflow";
            activitybind3.Path = "UserId";
            workflowparameterbinding3.ParameterName = "userId";
            workflowparameterbinding3.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            activitybind4.Name = "ActivationEmailWorkflow";
            activitybind4.Path = "EmailSeqNo";
            workflowparameterbinding4.ParameterName = "emailSeqNo";
            workflowparameterbinding4.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.SendEmail.ParameterBindings.Add(workflowparameterbinding3);
            this.SendEmail.ParameterBindings.Add(workflowparameterbinding4);
            // 
            // Timeout
            // 
            this.Timeout.Name = "Timeout";
            activitybind5.Name = "ActivationEmailWorkflow";
            activitybind5.Path = "Delay";
            this.Timeout.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            // 
            // StopSendingEvent
            // 
            this.StopSendingEvent.Activities.Add(this.StopSending);
            this.StopSendingEvent.Name = "StopSendingEvent";
            // 
            // TimeoutEvent
            // 
            this.TimeoutEvent.Activities.Add(this.Timeout);
            this.TimeoutEvent.Activities.Add(this.SendEmail);
            this.TimeoutEvent.Activities.Add(this.IncrementSeqNo);
            this.TimeoutEvent.Activities.Add(this.GetNextDelay);
            this.TimeoutEvent.Name = "TimeoutEvent";
            // 
            // Wait
            // 
            this.Wait.Activities.Add(this.TimeoutEvent);
            this.Wait.Activities.Add(this.StopSendingEvent);
            this.Wait.Name = "Wait";
            // 
            // CompleteWorkflow
            // 
            this.CompleteWorkflow.InterfaceType = typeof(LinkMe.Workflow.Design.ActivationEmailWorkflow.IDataExchange);
            this.CompleteWorkflow.MethodName = "CompleteWorkflow";
            this.CompleteWorkflow.Name = "CompleteWorkflow";
            activitybind6.Name = "ActivationEmailWorkflow";
            activitybind6.Path = "UserId";
            workflowparameterbinding5.ParameterName = "userId";
            workflowparameterbinding5.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.CompleteWorkflow.ParameterBindings.Add(workflowparameterbinding5);
            // 
            // WhileSendingEmail
            // 
            this.WhileSendingEmail.Activities.Add(this.Wait);
            ruleconditionreference1.ConditionName = "SendingEmail";
            this.WhileSendingEmail.Condition = ruleconditionreference1;
            this.WhileSendingEmail.Name = "WhileSendingEmail";
            // 
            // GetFirstDelay
            // 
            this.GetFirstDelay.InterfaceType = typeof(LinkMe.Workflow.Design.ActivationEmailWorkflow.IDataExchange);
            this.GetFirstDelay.MethodName = "GetNextDelay";
            this.GetFirstDelay.Name = "GetFirstDelay";
            workflowparameterbinding6.ParameterName = "emailSeqNo";
            workflowparameterbinding6.Value = 0;
            activitybind7.Name = "ActivationEmailWorkflow";
            activitybind7.Path = "Delay";
            workflowparameterbinding7.ParameterName = "(ReturnValue)";
            workflowparameterbinding7.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.GetFirstDelay.ParameterBindings.Add(workflowparameterbinding6);
            this.GetFirstDelay.ParameterBindings.Add(workflowparameterbinding7);
            // 
            // ActivationEmailWorkflow
            // 
            this.Activities.Add(this.GetFirstDelay);
            this.Activities.Add(this.WhileSendingEmail);
            this.Activities.Add(this.CompleteWorkflow);
            this.Name = "ActivationEmailWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private CallExternalMethodActivity GetFirstDelay;
        private EventDrivenActivity StopSendingEvent;
        private EventDrivenActivity TimeoutEvent;
        private ListenActivity Wait;
        private DelayActivity Timeout;
        private CallExternalMethodActivity SendEmail;
        private CodeActivity IncrementSeqNo;
        private CallExternalMethodActivity GetNextDelay;
        private HandleExternalEventActivity StopSending;
        private CallExternalMethodActivity CompleteWorkflow;
        private WhileActivity WhileSendingEmail;






























    }
}
