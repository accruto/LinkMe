<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>

<% switch (ViewData.GetActiveUserType())
   {
       case UserType.Member:
           Html.RenderPartial("MemberHeader");
           break;
           
       case UserType.Employer:
           Html.RenderPartial("EmployerHeader");
           break;
           
        case UserType.Administrator:
           Html.RenderPartial("AdministratorHeader");
           break;

        case UserType.Custodian:
           Html.RenderPartial("CustodianHeader");
           break;
   } %>
   