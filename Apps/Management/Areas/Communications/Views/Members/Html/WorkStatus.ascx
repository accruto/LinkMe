<%@ Control Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.WorkStatus" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>
<%@ Import Namespace="LinkMe.Domain"%>

<%
    var status = Model.Status;
    var desiredJobTitle = Model.DesiredJobTitle;

    switch (status)
    {
        case CandidateStatus.ActivelyLooking:
%>
            <%=Html.ActivelyLookingDescription(desiredJobTitle, Html.TinyUrl(true, "~/members/profile"))%>
<%
            break;

        case CandidateStatus.NotLooking:
%>
            <%=Html.NotLookingDescription()%>
<%
            break;

        case CandidateStatus.OpenToOffers:
%>
            <%=Html.OpenToOffersDescription(desiredJobTitle, Html.TinyUrl(true, "~/members/profile"))%>
<%
            break;

        default:
%>
            <%= Html.UnspecifiedDescription(Html.TinyUrl(true, "~/members/profile"))%>
<%
            break;
    }
%>