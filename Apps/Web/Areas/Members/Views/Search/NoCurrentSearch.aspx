<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>

<asp:Content ContentPlaceHolderID="Body" runat="server">

    <script language="javascript" type="text/javascript">

        if (window.location.hash) {
            window.location = "<%= SearchRoutes.Search.GenerateUrl() %>?" + window.location.hash.substring(1);
        }
        else {
            window.location = "<%= SearchRoutes.Search.GenerateUrl() %>";
        }

    </script>    

</asp:Content>

