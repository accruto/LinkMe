<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl<StepsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>

<div id="wizardsteps">
    <ol>

<%  for (var index = 0; index < Model.Steps.Count; ++index)
    { %>
        <li id="item_<%=index %>" class="<%= index == Model.CurrentStepIndex ? "selected" : ""%><%= index < Model.CurrentStepIndex ? " done" : ""%><%= index == 0 ? " first" + (index < Model.CurrentStepIndex ? " first-done" : "") + (index == Model.CurrentStepIndex ? " first-selected" : "") : "" %><%= index == Model.Steps.Count - 1 ? " last" + (index == Model.CurrentStepIndex ? " last-selected" : "") : ""%>">
<%      var step = Model.Steps[index];
        switch (step.Name)
        {
            case "Choose": %>
                <%= "Choose credits" %>
<%              break;
                
            case "Account": %>
                <%= "Log in / Join" %>
<%              break;                

            default: %>
                <%= step.Name %>
<%              break;
        } %>
        </li>
<%  } %>            
    </ol>
</div>
