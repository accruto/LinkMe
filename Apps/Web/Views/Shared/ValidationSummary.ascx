<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Models"%>

<script type="text/javascript">
    function showErrors(errors) {
        $("#error-summary").show();
        var messages = $("#error-message").find("ul");
        messages.empty();
        for (var i = 0; i < errors.length; i++) {
            messages.append("<li>" + errors[i] + "</li>");
        }
    }

    function clearErrors() {
        $("#error-summary").hide();
        var messages = $("#error-message").find("ul");
        messages.empty();
    }
</script>

<%  if (!ViewData.ModelState.IsValid && ViewData.ModelState.GetErrorMessages().Length > 0)
    { %>
    <div id="error-summary" style="border-width: 0px; color: Red;">
        <div id="error-message">
            <ul>
                <% foreach (var message in ViewData.ModelState.GetErrorMessages())
                   { %>
                    <li><%= message%></li>
                <% } %>
            </ul>
        </div>
    </div>
<%  }
    else
    { %>
    <div id="error-summary" style="border-width: 0px; color: Red; display:none;">
        <div id="error-message">
            <ul>
            </ul>
        </div>
    </div>
<%  } %>

<%  if (ViewData.ModelState.ContainsKey(ModelStateKeys.Confirmation) || ViewData.ModelState.ContainsKey(ModelStateKeys.Information))
    { %>
    <div style="border-width: 0px; color: Red;">
<%      if (ViewData.ModelState.ContainsKey(ModelStateKeys.Confirmation))
        { %>
        <div id="confirm-message">
            <ul>
<%          foreach (var error in ViewData.ModelState[ModelStateKeys.Confirmation].Errors)
            { %>            
                <li><%= error.ErrorMessage %></li>
<%          } %>
            </ul>
        </div>
<%      } %>        
<%      if (ViewData.ModelState.ContainsKey(ModelStateKeys.Information))
        { %>
        <div id="info-message">
            <ul>
<%          foreach (var error in ViewData.ModelState[ModelStateKeys.Information].Errors)
            { %>            
                <li><%= error.ErrorMessage %></li>
<%          } %>
            </ul>
        </div>
<%      } %>        
    </div>
<%  } %>