<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc"%>

<%  switch (ViewData.GetActiveUserType())
    {
        case UserType.Employer:
            Html.RenderPartial("EmployerSubHeader");
            break;
    } %>
   