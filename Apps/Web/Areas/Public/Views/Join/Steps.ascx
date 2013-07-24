<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<JoinModel>" %>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Join"%>

<div class="wizard-steps">
<%  for (var index = 0; index < Model.Steps.Steps.Count; ++index)
    {
        var step = Model.Steps.Steps[index]; %>
        <div class="step <%= step.Name %><%= index == Model.Steps.CurrentStepIndex ? " current" : "" %><%= step.CanBeMovedTo ? "" : " disabled" %><%= !Model.IsUploadingResume ? " manually" : "" %>"></div>
<%  } %>
</div>