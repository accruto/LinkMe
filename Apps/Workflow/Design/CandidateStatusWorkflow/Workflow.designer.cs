using System.Workflow.Activities;

namespace LinkMe.Workflow.Design.CandidateStatusWorkflow
{
    partial class Workflow
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
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding4 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding5 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding6 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding7 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding8 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding9 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding10 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding11 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding12 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding13 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding14 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding15 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding16 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference2 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference3 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference4 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference5 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference6 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding17 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference7 = new System.Workflow.Activities.Rules.RuleConditionReference();
            this.SetAvailableNowShortTimeout = new System.Workflow.Activities.CodeActivity();
            this.AvailableNowConfirmedWithShortReminder = new System.Workflow.Activities.HandleExternalEventActivity();
            this.SetAvailableNowLongTimeout = new System.Workflow.Activities.CodeActivity();
            this.AvailableNowConfirmedWithLongReminder = new System.Workflow.Activities.HandleExternalEventActivity();
            this.CreateActivelyLookingNotification = new System.Workflow.Activities.CallExternalMethodActivity();
            this.UpdateStatusActivelyLooking = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetStateActivelyLooking = new System.Workflow.Activities.CodeActivity();
            this.AvailableNowResponseDelay = new System.Workflow.Activities.DelayActivity();
            this.StatusChanged5 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.SetAvailableNowResponseShortTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetAvailableNowResponseLongTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.UpdateStatusAvailableNow = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetStateAvailableNow = new System.Workflow.Activities.CodeActivity();
            this.ActivelyLookingUpgraded = new System.Workflow.Activities.HandleExternalEventActivity();
            this.ActivelyLookingConfirmed = new System.Workflow.Activities.HandleExternalEventActivity();
            this.CreatePassiveNotification = new System.Workflow.Activities.CallExternalMethodActivity();
            this.UpdateStatusPassive = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetStatePassive = new System.Workflow.Activities.CodeActivity();
            this.ActivelyLookingResponseDelay = new System.Workflow.Activities.DelayActivity();
            this.StatusChanged3 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.ConfirmAvailableNowShort = new System.Workflow.Activities.EventDrivenActivity();
            this.ConfirmAvailableNowLong = new System.Workflow.Activities.EventDrivenActivity();
            this.DowngradeAvailableNow = new System.Workflow.Activities.EventDrivenActivity();
            this.ChangeAvailableNow = new System.Workflow.Activities.EventDrivenActivity();
            this.GetAvailableNowResponseShortTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.GetAvailableNowResponseLongTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.UpgradeActivelyLooking = new System.Workflow.Activities.EventDrivenActivity();
            this.ConfirmActivelyLooking = new System.Workflow.Activities.EventDrivenActivity();
            this.DowngradeActivelyLooking = new System.Workflow.Activities.EventDrivenActivity();
            this.ChangeActivelyLooking = new System.Workflow.Activities.EventDrivenActivity();
            this.StatusChanged4 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.AvailableNowResponse = new System.Workflow.Activities.ListenActivity();
            this.SetAvailableNowResponseTimeout = new System.Workflow.Activities.IfElseActivity();
            this.CreateAvailableNowConfirmationRequest = new System.Workflow.Activities.CallExternalMethodActivity();
            this.AvailableNowConfirmationDelay = new System.Workflow.Activities.DelayActivity();
            this.SetAvailableNowConfirmationShortTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetAvailableNowConfirmationLongTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetAvailableNowTimeoutZero = new System.Workflow.Activities.CodeActivity();
            this.ActivelyLookingResponse = new System.Workflow.Activities.ListenActivity();
            this.SetActivelyLookingResponseTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.CreateActivelyLookingConfirmationRequest = new System.Workflow.Activities.CallExternalMethodActivity();
            this.ActivelyLookingConfirmationDelay = new System.Workflow.Activities.DelayActivity();
            this.StatusChanged2 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.SetActivelyLookingConfirmationTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.SetActivelyLookingTimeoutZero = new System.Workflow.Activities.CodeActivity();
            this.AvailableNowStatusChanged = new System.Workflow.Activities.EventDrivenActivity();
            this.AvailableNowConfirmation = new System.Workflow.Activities.EventDrivenActivity();
            this.GetAvailableNowShortTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.GetAvailableNowLongTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.IgnoreAvailableNowTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.ActivelyLookingConfirmation = new System.Workflow.Activities.EventDrivenActivity();
            this.ActivelyLookingStatusChanged = new System.Workflow.Activities.EventDrivenActivity();
            this.GetActivelyLookingTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.IgnoreActivelyLookingTimeout = new System.Workflow.Activities.IfElseBranchActivity();
            this.Available_Reminder = new System.Workflow.Activities.ListenActivity();
            this.SetAvailableNowTimeout = new System.Workflow.Activities.IfElseActivity();
            this.ActivelyLookingReminder = new System.Workflow.Activities.ListenActivity();
            this.SetActivelyLookingTimeout = new System.Workflow.Activities.IfElseActivity();
            this.AvailableNow = new System.Workflow.Activities.IfElseBranchActivity();
            this.ActivelyLooking = new System.Workflow.Activities.IfElseBranchActivity();
            this.SwitchState = new System.Workflow.Activities.IfElseActivity();
            this.Complete = new System.Workflow.Activities.CallExternalMethodActivity();
            this.WhileNotPassive = new System.Workflow.Activities.WhileActivity();
            // 
            // SetAvailableNowShortTimeout
            // 
            this.SetAvailableNowShortTimeout.Name = "SetAvailableNowShortTimeout";
            this.SetAvailableNowShortTimeout.ExecuteCode += new System.EventHandler(this.ExecuteSetAvailableNowShortTimeout);
            // 
            // AvailableNowConfirmedWithShortReminder
            // 
            this.AvailableNowConfirmedWithShortReminder.EventName = "AvailableNowConfirmedWithShortReminder";
            this.AvailableNowConfirmedWithShortReminder.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.AvailableNowConfirmedWithShortReminder.Name = "AvailableNowConfirmedWithShortReminder";
            // 
            // SetAvailableNowLongTimeout
            // 
            this.SetAvailableNowLongTimeout.Name = "SetAvailableNowLongTimeout";
            this.SetAvailableNowLongTimeout.ExecuteCode += new System.EventHandler(this.ExecuteSetAvailableNowLongTimeout);
            // 
            // AvailableNowConfirmedWithLongReminder
            // 
            this.AvailableNowConfirmedWithLongReminder.EventName = "AvailableNowConfirmedWithLongReminder";
            this.AvailableNowConfirmedWithLongReminder.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.AvailableNowConfirmedWithLongReminder.Name = "AvailableNowConfirmedWithLongReminder";
            // 
            // CreateActivelyLookingNotification
            // 
            this.CreateActivelyLookingNotification.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.CreateActivelyLookingNotification.MethodName = "CreateActivelyLookingNotification";
            this.CreateActivelyLookingNotification.Name = "CreateActivelyLookingNotification";
            activitybind1.Name = "Workflow";
            activitybind1.Path = "CandidateId";
            workflowparameterbinding1.ParameterName = "candidateId";
            workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.CreateActivelyLookingNotification.ParameterBindings.Add(workflowparameterbinding1);
            // 
            // UpdateStatusActivelyLooking
            // 
            this.UpdateStatusActivelyLooking.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.UpdateStatusActivelyLooking.MethodName = "UpdateStatus";
            this.UpdateStatusActivelyLooking.Name = "UpdateStatusActivelyLooking";
            activitybind2.Name = "Workflow";
            activitybind2.Path = "CandidateId";
            workflowparameterbinding2.ParameterName = "candidateId";
            workflowparameterbinding2.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            activitybind3.Name = "Workflow";
            activitybind3.Path = "State";
            workflowparameterbinding3.ParameterName = "state";
            workflowparameterbinding3.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.UpdateStatusActivelyLooking.ParameterBindings.Add(workflowparameterbinding2);
            this.UpdateStatusActivelyLooking.ParameterBindings.Add(workflowparameterbinding3);
            // 
            // SetStateActivelyLooking
            // 
            this.SetStateActivelyLooking.Name = "SetStateActivelyLooking";
            this.SetStateActivelyLooking.ExecuteCode += new System.EventHandler(this.ExecuteSetStateActivelyLooking);
            // 
            // AvailableNowResponseDelay
            // 
            this.AvailableNowResponseDelay.Name = "AvailableNowResponseDelay";
            activitybind4.Name = "Workflow";
            activitybind4.Path = "AvailableNowResponseTimeout";
            this.AvailableNowResponseDelay.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // StatusChanged5
            // 
            this.StatusChanged5.EventName = "StatusChanged";
            this.StatusChanged5.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.StatusChanged5.Name = "StatusChanged5";
            this.StatusChanged5.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.StatusChanged_Invoked);
            // 
            // SetAvailableNowResponseShortTimeout
            // 
            this.SetAvailableNowResponseShortTimeout.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.SetAvailableNowResponseShortTimeout.MethodName = "GetAvailableNowResponseShortTimeout";
            this.SetAvailableNowResponseShortTimeout.Name = "SetAvailableNowResponseShortTimeout";
            activitybind5.Name = "Workflow";
            activitybind5.Path = "AvailableNowResponseTimeout";
            workflowparameterbinding4.ParameterName = "(ReturnValue)";
            workflowparameterbinding4.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.SetAvailableNowResponseShortTimeout.ParameterBindings.Add(workflowparameterbinding4);
            // 
            // SetAvailableNowResponseLongTimeout
            // 
            this.SetAvailableNowResponseLongTimeout.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.SetAvailableNowResponseLongTimeout.MethodName = "GetAvailableNowResponseLongTimeout";
            this.SetAvailableNowResponseLongTimeout.Name = "SetAvailableNowResponseLongTimeout";
            activitybind6.Name = "Workflow";
            activitybind6.Path = "AvailableNowResponseTimeout";
            workflowparameterbinding5.ParameterName = "(ReturnValue)";
            workflowparameterbinding5.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.SetAvailableNowResponseLongTimeout.ParameterBindings.Add(workflowparameterbinding5);
            // 
            // UpdateStatusAvailableNow
            // 
            this.UpdateStatusAvailableNow.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.UpdateStatusAvailableNow.MethodName = "UpdateStatus";
            this.UpdateStatusAvailableNow.Name = "UpdateStatusAvailableNow";
            activitybind7.Name = "Workflow";
            activitybind7.Path = "CandidateId";
            workflowparameterbinding6.ParameterName = "candidateId";
            workflowparameterbinding6.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            activitybind8.Name = "Workflow";
            activitybind8.Path = "State";
            workflowparameterbinding7.ParameterName = "state";
            workflowparameterbinding7.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.UpdateStatusAvailableNow.ParameterBindings.Add(workflowparameterbinding6);
            this.UpdateStatusAvailableNow.ParameterBindings.Add(workflowparameterbinding7);
            // 
            // SetStateAvailableNow
            // 
            this.SetStateAvailableNow.Name = "SetStateAvailableNow";
            this.SetStateAvailableNow.ExecuteCode += new System.EventHandler(this.ExecuteSetStateAvailableNow);
            // 
            // ActivelyLookingUpgraded
            // 
            this.ActivelyLookingUpgraded.EventName = "ActivelyLookingUpgraded";
            this.ActivelyLookingUpgraded.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.ActivelyLookingUpgraded.Name = "ActivelyLookingUpgraded";
            // 
            // ActivelyLookingConfirmed
            // 
            this.ActivelyLookingConfirmed.EventName = "ActivelyLookingConfirmed";
            this.ActivelyLookingConfirmed.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.ActivelyLookingConfirmed.Name = "ActivelyLookingConfirmed";
            // 
            // CreatePassiveNotification
            // 
            this.CreatePassiveNotification.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.CreatePassiveNotification.MethodName = "CreatePassiveNotification";
            this.CreatePassiveNotification.Name = "CreatePassiveNotification";
            activitybind9.Name = "Workflow";
            activitybind9.Path = "CandidateId";
            workflowparameterbinding8.ParameterName = "candidateId";
            workflowparameterbinding8.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.CreatePassiveNotification.ParameterBindings.Add(workflowparameterbinding8);
            // 
            // UpdateStatusPassive
            // 
            this.UpdateStatusPassive.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.UpdateStatusPassive.MethodName = "UpdateStatus";
            this.UpdateStatusPassive.Name = "UpdateStatusPassive";
            activitybind10.Name = "Workflow";
            activitybind10.Path = "CandidateId";
            workflowparameterbinding9.ParameterName = "candidateId";
            workflowparameterbinding9.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            activitybind11.Name = "Workflow";
            activitybind11.Path = "State";
            workflowparameterbinding10.ParameterName = "state";
            workflowparameterbinding10.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.UpdateStatusPassive.ParameterBindings.Add(workflowparameterbinding9);
            this.UpdateStatusPassive.ParameterBindings.Add(workflowparameterbinding10);
            // 
            // SetStatePassive
            // 
            this.SetStatePassive.Name = "SetStatePassive";
            this.SetStatePassive.ExecuteCode += new System.EventHandler(this.ExecuteSetStatePassive);
            // 
            // ActivelyLookingResponseDelay
            // 
            this.ActivelyLookingResponseDelay.Name = "ActivelyLookingResponseDelay";
            activitybind12.Name = "Workflow";
            activitybind12.Path = "ActivelyLookingResponseTimeout";
            this.ActivelyLookingResponseDelay.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // StatusChanged3
            // 
            this.StatusChanged3.EventName = "StatusChanged";
            this.StatusChanged3.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.StatusChanged3.Name = "StatusChanged3";
            this.StatusChanged3.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.StatusChanged_Invoked);
            // 
            // ConfirmAvailableNowShort
            // 
            this.ConfirmAvailableNowShort.Activities.Add(this.AvailableNowConfirmedWithShortReminder);
            this.ConfirmAvailableNowShort.Activities.Add(this.SetAvailableNowShortTimeout);
            this.ConfirmAvailableNowShort.Name = "ConfirmAvailableNowShort";
            // 
            // ConfirmAvailableNowLong
            // 
            this.ConfirmAvailableNowLong.Activities.Add(this.AvailableNowConfirmedWithLongReminder);
            this.ConfirmAvailableNowLong.Activities.Add(this.SetAvailableNowLongTimeout);
            this.ConfirmAvailableNowLong.Name = "ConfirmAvailableNowLong";
            // 
            // DowngradeAvailableNow
            // 
            this.DowngradeAvailableNow.Activities.Add(this.AvailableNowResponseDelay);
            this.DowngradeAvailableNow.Activities.Add(this.SetStateActivelyLooking);
            this.DowngradeAvailableNow.Activities.Add(this.UpdateStatusActivelyLooking);
            this.DowngradeAvailableNow.Activities.Add(this.CreateActivelyLookingNotification);
            this.DowngradeAvailableNow.Name = "DowngradeAvailableNow";
            // 
            // ChangeAvailableNow
            // 
            this.ChangeAvailableNow.Activities.Add(this.StatusChanged5);
            this.ChangeAvailableNow.Name = "ChangeAvailableNow";
            // 
            // GetAvailableNowResponseShortTimeout
            // 
            this.GetAvailableNowResponseShortTimeout.Activities.Add(this.SetAvailableNowResponseShortTimeout);
            this.GetAvailableNowResponseShortTimeout.Name = "GetAvailableNowResponseShortTimeout";
            // 
            // GetAvailableNowResponseLongTimeout
            // 
            this.GetAvailableNowResponseLongTimeout.Activities.Add(this.SetAvailableNowResponseLongTimeout);
            ruleconditionreference1.ConditionName = "IfUseAvailableNowResponseLongTimeout";
            this.GetAvailableNowResponseLongTimeout.Condition = ruleconditionreference1;
            this.GetAvailableNowResponseLongTimeout.Name = "GetAvailableNowResponseLongTimeout";
            // 
            // UpgradeActivelyLooking
            // 
            this.UpgradeActivelyLooking.Activities.Add(this.ActivelyLookingUpgraded);
            this.UpgradeActivelyLooking.Activities.Add(this.SetStateAvailableNow);
            this.UpgradeActivelyLooking.Activities.Add(this.UpdateStatusAvailableNow);
            this.UpgradeActivelyLooking.Name = "UpgradeActivelyLooking";
            // 
            // ConfirmActivelyLooking
            // 
            this.ConfirmActivelyLooking.Activities.Add(this.ActivelyLookingConfirmed);
            this.ConfirmActivelyLooking.Name = "ConfirmActivelyLooking";
            // 
            // DowngradeActivelyLooking
            // 
            this.DowngradeActivelyLooking.Activities.Add(this.ActivelyLookingResponseDelay);
            this.DowngradeActivelyLooking.Activities.Add(this.SetStatePassive);
            this.DowngradeActivelyLooking.Activities.Add(this.UpdateStatusPassive);
            this.DowngradeActivelyLooking.Activities.Add(this.CreatePassiveNotification);
            this.DowngradeActivelyLooking.Name = "DowngradeActivelyLooking";
            // 
            // ChangeActivelyLooking
            // 
            this.ChangeActivelyLooking.Activities.Add(this.StatusChanged3);
            this.ChangeActivelyLooking.Name = "ChangeActivelyLooking";
            // 
            // StatusChanged4
            // 
            this.StatusChanged4.EventName = "StatusChanged";
            this.StatusChanged4.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.StatusChanged4.Name = "StatusChanged4";
            this.StatusChanged4.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.StatusChanged_Invoked);
            // 
            // AvailableNowResponse
            // 
            this.AvailableNowResponse.Activities.Add(this.ChangeAvailableNow);
            this.AvailableNowResponse.Activities.Add(this.DowngradeAvailableNow);
            this.AvailableNowResponse.Activities.Add(this.ConfirmAvailableNowLong);
            this.AvailableNowResponse.Activities.Add(this.ConfirmAvailableNowShort);
            this.AvailableNowResponse.Name = "AvailableNowResponse";
            // 
            // SetAvailableNowResponseTimeout
            // 
            this.SetAvailableNowResponseTimeout.Activities.Add(this.GetAvailableNowResponseLongTimeout);
            this.SetAvailableNowResponseTimeout.Activities.Add(this.GetAvailableNowResponseShortTimeout);
            this.SetAvailableNowResponseTimeout.Name = "SetAvailableNowResponseTimeout";
            // 
            // CreateAvailableNowConfirmationRequest
            // 
            this.CreateAvailableNowConfirmationRequest.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.CreateAvailableNowConfirmationRequest.MethodName = "CreateAvailableNowConfirmationRequest";
            this.CreateAvailableNowConfirmationRequest.Name = "CreateAvailableNowConfirmationRequest";
            activitybind13.Name = "Workflow";
            activitybind13.Path = "CandidateId";
            workflowparameterbinding11.ParameterName = "candidateId";
            workflowparameterbinding11.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.CreateAvailableNowConfirmationRequest.ParameterBindings.Add(workflowparameterbinding11);
            // 
            // AvailableNowConfirmationDelay
            // 
            this.AvailableNowConfirmationDelay.Name = "AvailableNowConfirmationDelay";
            activitybind14.Name = "Workflow";
            activitybind14.Path = "AvailableNowConfirmationTimeout";
            this.AvailableNowConfirmationDelay.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // SetAvailableNowConfirmationShortTimeout
            // 
            this.SetAvailableNowConfirmationShortTimeout.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.SetAvailableNowConfirmationShortTimeout.MethodName = "GetAvailableNowConfirmationShortTimeout";
            this.SetAvailableNowConfirmationShortTimeout.Name = "SetAvailableNowConfirmationShortTimeout";
            activitybind15.Name = "Workflow";
            activitybind15.Path = "AvailableNowConfirmationTimeout";
            workflowparameterbinding12.ParameterName = "(ReturnValue)";
            workflowparameterbinding12.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.SetAvailableNowConfirmationShortTimeout.ParameterBindings.Add(workflowparameterbinding12);
            // 
            // SetAvailableNowConfirmationLongTimeout
            // 
            this.SetAvailableNowConfirmationLongTimeout.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.SetAvailableNowConfirmationLongTimeout.MethodName = "GetAvailableNowConfirmationLongTimeout";
            this.SetAvailableNowConfirmationLongTimeout.Name = "SetAvailableNowConfirmationLongTimeout";
            activitybind16.Name = "Workflow";
            activitybind16.Path = "AvailableNowConfirmationTimeout";
            workflowparameterbinding13.ParameterName = "(ReturnValue)";
            workflowparameterbinding13.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.SetAvailableNowConfirmationLongTimeout.ParameterBindings.Add(workflowparameterbinding13);
            // 
            // SetAvailableNowTimeoutZero
            // 
            this.SetAvailableNowTimeoutZero.Name = "SetAvailableNowTimeoutZero";
            this.SetAvailableNowTimeoutZero.ExecuteCode += new System.EventHandler(this.ExecuteSetAvailableNowTimeoutZero);
            // 
            // ActivelyLookingResponse
            // 
            this.ActivelyLookingResponse.Activities.Add(this.ChangeActivelyLooking);
            this.ActivelyLookingResponse.Activities.Add(this.DowngradeActivelyLooking);
            this.ActivelyLookingResponse.Activities.Add(this.ConfirmActivelyLooking);
            this.ActivelyLookingResponse.Activities.Add(this.UpgradeActivelyLooking);
            this.ActivelyLookingResponse.Name = "ActivelyLookingResponse";
            // 
            // SetActivelyLookingResponseTimeout
            // 
            this.SetActivelyLookingResponseTimeout.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.SetActivelyLookingResponseTimeout.MethodName = "GetActivelyLookingResponseTimeout";
            this.SetActivelyLookingResponseTimeout.Name = "SetActivelyLookingResponseTimeout";
            activitybind17.Name = "Workflow";
            activitybind17.Path = "ActivelyLookingResponseTimeout";
            workflowparameterbinding14.ParameterName = "(ReturnValue)";
            workflowparameterbinding14.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.SetActivelyLookingResponseTimeout.ParameterBindings.Add(workflowparameterbinding14);
            // 
            // CreateActivelyLookingConfirmationRequest
            // 
            this.CreateActivelyLookingConfirmationRequest.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.CreateActivelyLookingConfirmationRequest.MethodName = "CreateActivelyLookingConfirmationRequest";
            this.CreateActivelyLookingConfirmationRequest.Name = "CreateActivelyLookingConfirmationRequest";
            activitybind18.Name = "Workflow";
            activitybind18.Path = "CandidateId";
            workflowparameterbinding15.ParameterName = "candidateId";
            workflowparameterbinding15.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            this.CreateActivelyLookingConfirmationRequest.ParameterBindings.Add(workflowparameterbinding15);
            // 
            // ActivelyLookingConfirmationDelay
            // 
            this.ActivelyLookingConfirmationDelay.Name = "ActivelyLookingConfirmationDelay";
            activitybind19.Name = "Workflow";
            activitybind19.Path = "ActivelyLookingConfirmationTimeout";
            this.ActivelyLookingConfirmationDelay.SetBinding(System.Workflow.Activities.DelayActivity.TimeoutDurationProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            // 
            // StatusChanged2
            // 
            this.StatusChanged2.EventName = "StatusChanged";
            this.StatusChanged2.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.StatusChanged2.Name = "StatusChanged2";
            this.StatusChanged2.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.StatusChanged_Invoked);
            // 
            // SetActivelyLookingConfirmationTimeout
            // 
            this.SetActivelyLookingConfirmationTimeout.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.SetActivelyLookingConfirmationTimeout.MethodName = "GetActivelyLookingConfirmationTimeout";
            this.SetActivelyLookingConfirmationTimeout.Name = "SetActivelyLookingConfirmationTimeout";
            activitybind20.Name = "Workflow";
            activitybind20.Path = "ActivelyLookingConfirmationTimeout";
            workflowparameterbinding16.ParameterName = "(ReturnValue)";
            workflowparameterbinding16.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            this.SetActivelyLookingConfirmationTimeout.ParameterBindings.Add(workflowparameterbinding16);
            // 
            // SetActivelyLookingTimeoutZero
            // 
            this.SetActivelyLookingTimeoutZero.Name = "SetActivelyLookingTimeoutZero";
            this.SetActivelyLookingTimeoutZero.ExecuteCode += new System.EventHandler(this.ExecuteSetActivelyLookingTimeoutZero);
            // 
            // AvailableNowStatusChanged
            // 
            this.AvailableNowStatusChanged.Activities.Add(this.StatusChanged4);
            this.AvailableNowStatusChanged.Name = "AvailableNowStatusChanged";
            // 
            // AvailableNowConfirmation
            // 
            this.AvailableNowConfirmation.Activities.Add(this.AvailableNowConfirmationDelay);
            this.AvailableNowConfirmation.Activities.Add(this.CreateAvailableNowConfirmationRequest);
            this.AvailableNowConfirmation.Activities.Add(this.SetAvailableNowResponseTimeout);
            this.AvailableNowConfirmation.Activities.Add(this.AvailableNowResponse);
            this.AvailableNowConfirmation.Name = "AvailableNowConfirmation";
            // 
            // GetAvailableNowShortTimeout
            // 
            this.GetAvailableNowShortTimeout.Activities.Add(this.SetAvailableNowConfirmationShortTimeout);
            this.GetAvailableNowShortTimeout.Name = "GetAvailableNowShortTimeout";
            // 
            // GetAvailableNowLongTimeout
            // 
            this.GetAvailableNowLongTimeout.Activities.Add(this.SetAvailableNowConfirmationLongTimeout);
            ruleconditionreference2.ConditionName = "IfUseAvailableNowConfirmationLongTimeout";
            this.GetAvailableNowLongTimeout.Condition = ruleconditionreference2;
            this.GetAvailableNowLongTimeout.Name = "GetAvailableNowLongTimeout";
            // 
            // IgnoreAvailableNowTimeout
            // 
            this.IgnoreAvailableNowTimeout.Activities.Add(this.SetAvailableNowTimeoutZero);
            ruleconditionreference3.ConditionName = "IgnoreTimeoutOnce";
            this.IgnoreAvailableNowTimeout.Condition = ruleconditionreference3;
            this.IgnoreAvailableNowTimeout.Name = "IgnoreAvailableNowTimeout";
            // 
            // ActivelyLookingConfirmation
            // 
            this.ActivelyLookingConfirmation.Activities.Add(this.ActivelyLookingConfirmationDelay);
            this.ActivelyLookingConfirmation.Activities.Add(this.CreateActivelyLookingConfirmationRequest);
            this.ActivelyLookingConfirmation.Activities.Add(this.SetActivelyLookingResponseTimeout);
            this.ActivelyLookingConfirmation.Activities.Add(this.ActivelyLookingResponse);
            this.ActivelyLookingConfirmation.Name = "ActivelyLookingConfirmation";
            // 
            // ActivelyLookingStatusChanged
            // 
            this.ActivelyLookingStatusChanged.Activities.Add(this.StatusChanged2);
            this.ActivelyLookingStatusChanged.Name = "ActivelyLookingStatusChanged";
            // 
            // GetActivelyLookingTimeout
            // 
            this.GetActivelyLookingTimeout.Activities.Add(this.SetActivelyLookingConfirmationTimeout);
            this.GetActivelyLookingTimeout.Name = "GetActivelyLookingTimeout";
            // 
            // IgnoreActivelyLookingTimeout
            // 
            this.IgnoreActivelyLookingTimeout.Activities.Add(this.SetActivelyLookingTimeoutZero);
            ruleconditionreference4.ConditionName = "IgnoreTimeout";
            this.IgnoreActivelyLookingTimeout.Condition = ruleconditionreference4;
            this.IgnoreActivelyLookingTimeout.Name = "IgnoreActivelyLookingTimeout";
            // 
            // Available_Reminder
            // 
            this.Available_Reminder.Activities.Add(this.AvailableNowConfirmation);
            this.Available_Reminder.Activities.Add(this.AvailableNowStatusChanged);
            this.Available_Reminder.Name = "Available_Reminder";
            // 
            // SetAvailableNowTimeout
            // 
            this.SetAvailableNowTimeout.Activities.Add(this.IgnoreAvailableNowTimeout);
            this.SetAvailableNowTimeout.Activities.Add(this.GetAvailableNowLongTimeout);
            this.SetAvailableNowTimeout.Activities.Add(this.GetAvailableNowShortTimeout);
            this.SetAvailableNowTimeout.Name = "SetAvailableNowTimeout";
            // 
            // ActivelyLookingReminder
            // 
            this.ActivelyLookingReminder.Activities.Add(this.ActivelyLookingStatusChanged);
            this.ActivelyLookingReminder.Activities.Add(this.ActivelyLookingConfirmation);
            this.ActivelyLookingReminder.Name = "ActivelyLookingReminder";
            // 
            // SetActivelyLookingTimeout
            // 
            this.SetActivelyLookingTimeout.Activities.Add(this.IgnoreActivelyLookingTimeout);
            this.SetActivelyLookingTimeout.Activities.Add(this.GetActivelyLookingTimeout);
            this.SetActivelyLookingTimeout.Name = "SetActivelyLookingTimeout";
            // 
            // AvailableNow
            // 
            this.AvailableNow.Activities.Add(this.SetAvailableNowTimeout);
            this.AvailableNow.Activities.Add(this.Available_Reminder);
            ruleconditionreference5.ConditionName = "IsAvailableNow";
            this.AvailableNow.Condition = ruleconditionreference5;
            this.AvailableNow.Name = "AvailableNow";
            // 
            // ActivelyLooking
            // 
            this.ActivelyLooking.Activities.Add(this.SetActivelyLookingTimeout);
            this.ActivelyLooking.Activities.Add(this.ActivelyLookingReminder);
            ruleconditionreference6.ConditionName = "IsActivelyLooking";
            this.ActivelyLooking.Condition = ruleconditionreference6;
            this.ActivelyLooking.Name = "ActivelyLooking";
            // 
            // SwitchState
            // 
            this.SwitchState.Activities.Add(this.ActivelyLooking);
            this.SwitchState.Activities.Add(this.AvailableNow);
            this.SwitchState.Name = "SwitchState";
            // 
            // Complete
            // 
            this.Complete.InterfaceType = typeof(LinkMe.Workflow.Design.CandidateStatusWorkflow.IDataExchange);
            this.Complete.MethodName = "CompleteWorkflow";
            this.Complete.Name = "Complete";
            activitybind21.Name = "Workflow";
            activitybind21.Path = "CandidateId";
            workflowparameterbinding17.ParameterName = "candidateId";
            workflowparameterbinding17.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.Complete.ParameterBindings.Add(workflowparameterbinding17);
            // 
            // WhileNotPassive
            // 
            this.WhileNotPassive.Activities.Add(this.SwitchState);
            ruleconditionreference7.ConditionName = "IsNotPassive";
            this.WhileNotPassive.Condition = ruleconditionreference7;
            this.WhileNotPassive.Name = "WhileNotPassive";
            // 
            // Workflow
            // 
            this.Activities.Add(this.WhileNotPassive);
            this.Activities.Add(this.Complete);
            this.Name = "Workflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private IfElseBranchActivity ActivelyLooking;

        private IfElseActivity SwitchState;

        private IfElseBranchActivity AvailableNow;

        private EventDrivenActivity ActivelyLookingConfirmation;

        private EventDrivenActivity ActivelyLookingStatusChanged;

        private ListenActivity ActivelyLookingReminder;

        private HandleExternalEventActivity StatusChanged2;

        private DelayActivity ActivelyLookingConfirmationDelay;

        private EventDrivenActivity DowngradeActivelyLooking;

        private EventDrivenActivity ChangeActivelyLooking;

        private ListenActivity ActivelyLookingResponse;

        private CallExternalMethodActivity CreateActivelyLookingConfirmationRequest;

        private HandleExternalEventActivity StatusChanged3;

        private DelayActivity ActivelyLookingResponseDelay;

        private CallExternalMethodActivity UpdateStatusPassive;

        private CallExternalMethodActivity CreatePassiveNotification;

        private CodeActivity SetStatePassive;

        private HandleExternalEventActivity ActivelyLookingConfirmed;

        private EventDrivenActivity ConfirmActivelyLooking;

        private CallExternalMethodActivity UpdateStatusAvailableNow;

        private CodeActivity SetStateAvailableNow;

        private HandleExternalEventActivity ActivelyLookingUpgraded;

        private EventDrivenActivity UpgradeActivelyLooking;

        private EventDrivenActivity AvailableNowConfirmation;

        private EventDrivenActivity AvailableNowStatusChanged;

        private ListenActivity Available_Reminder;

        private HandleExternalEventActivity StatusChanged4;

        private CallExternalMethodActivity CreateAvailableNowConfirmationRequest;

        private DelayActivity AvailableNowConfirmationDelay;

        private CodeActivity SetAvailableNowShortTimeout;

        private HandleExternalEventActivity AvailableNowConfirmedWithShortReminder;

        private CodeActivity SetAvailableNowLongTimeout;

        private HandleExternalEventActivity AvailableNowConfirmedWithLongReminder;

        private CallExternalMethodActivity CreateActivelyLookingNotification;

        private CallExternalMethodActivity UpdateStatusActivelyLooking;

        private CodeActivity SetStateActivelyLooking;

        private DelayActivity AvailableNowResponseDelay;

        private HandleExternalEventActivity StatusChanged5;

        private EventDrivenActivity ConfirmAvailableNowShort;

        private EventDrivenActivity ConfirmAvailableNowLong;

        private EventDrivenActivity DowngradeAvailableNow;

        private EventDrivenActivity ChangeAvailableNow;

        private ListenActivity AvailableNowResponse;

        private CallExternalMethodActivity Complete;

        private IfElseBranchActivity GetActivelyLookingTimeout;

        private IfElseBranchActivity IgnoreActivelyLookingTimeout;

        private IfElseActivity SetActivelyLookingTimeout;

        private CodeActivity SetActivelyLookingTimeoutZero;

        private CallExternalMethodActivity SetActivelyLookingConfirmationTimeout;

        private CallExternalMethodActivity SetActivelyLookingResponseTimeout;

        private IfElseBranchActivity GetAvailableNowLongTimeout;

        private IfElseBranchActivity IgnoreAvailableNowTimeout;

        private IfElseActivity SetAvailableNowTimeout;

        private CodeActivity SetAvailableNowTimeoutZero;

        private IfElseBranchActivity GetAvailableNowShortTimeout;

        private CallExternalMethodActivity SetAvailableNowConfirmationLongTimeout;

        private CallExternalMethodActivity SetAvailableNowConfirmationShortTimeout;

        private CallExternalMethodActivity SetAvailableNowResponseShortTimeout;

        private CallExternalMethodActivity SetAvailableNowResponseLongTimeout;

        private IfElseBranchActivity GetAvailableNowResponseShortTimeout;

        private IfElseBranchActivity GetAvailableNowResponseLongTimeout;

        private IfElseActivity SetAvailableNowResponseTimeout;

        private WhileActivity WhileNotPassive;






























































































































































































































































    }
}