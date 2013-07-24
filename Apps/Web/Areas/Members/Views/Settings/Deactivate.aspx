<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Routes"%>
<%@ Import Namespace="LinkMe.Domain.Accounts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.UI.Registered.Networkers"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Navigation"%>
<%@ Import Namespace="LinkMe.Web.Html"%>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <script type="text/javascript">
        var visiblePopupId;

        function makeVisible(clicked) {
            var newPopupId;

            if (clicked.parentNode.getElementsByTagName('div')[0] != null)
                newPopupId = clicked.parentNode.getElementsByTagName('div')[0].id;

            if (visiblePopupId != null)
                $(visiblePopupId).hide();

            if (newPopupId != null) {
                $(newPopupId).show();
            }

            visiblePopupId = newPopupId;
        }
    </script>

    <div class="page-title">
        <h1>Deactivate my account</h1>
    </div>

    <div class="forms_v2">
    
        <div class="section">
            <div class="section-content">

                <p>
                    You can deactivate your account immediately if you wish.
                    Once your account is deactivated, employers and other members
                    will not be able to find your resume or see your details.
                    You will be able to re-activate your account by logging in again with the same login name and password.
                </p>
                <p>Please let us know why you are deactivating your account.</p>

<% using (Html.RenderForm())
   { %>
                
                <p>
                    <ul id="reasons">
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.Employer, new { id = DeactivationReason.Employer, onclick = "makeVisible(this);" })%>
                            <label for="Employer">I don't want my current employer to know I'm looking for work</label>
                            <div class="popupBox" style="display:none;" id="divEmployer">
                                You can change your visibility to employers by hiding your name, phone number(s), photo and
                                current &amp; previous employers. You can also retain your resume but have it not visible at all in the employer search by clicking
                                <a href="<%=NavigationManager.GetUrlForPage<VisibilitySettingsBasic>()%>">here</a>.
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.NotLooking, new { onclick = "makeVisible(this);" })%>
                            <label for="NotLooking">I'm not looking for work anymore</label>
                            <div class="popupBox" style="display:none;" id="divNotLooking">
                                To do this, set your Work Status to 'Not looking for work' 
                                <%= Html.RouteRefLink("here", ProfilesRoutes.Profile) %>.
                                You can also retain your resume but have it not visible at all in the employer search by clicking
                                <a href="<%=NavigationManager.GetUrlForPage<VisibilitySettingsBasic>()%>">here</a>.
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.Emails, new { onclick = "makeVisible(this);" })%>
                            <label for="Emails">I receive too many emails</label>
                            <div class="popupBox" style="display:none;" id="divEmails">
                                You can manage your emails
                                <%= Html.RouteRefLink("here", SettingsRoutes.Settings, null)%>.
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.JobAlerts, new { onclick = "makeVisible(this);" })%>
                            <label for="JobAlerts">I receive too many job alert emails</label>
                            <div class="popupBox" style="display:none;" id="divJobAlerts">
                                You can delete or manage job alerts
                                <%= Html.RouteRefLink("here", SearchRoutes.SavedSearches) %>.
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.NotUseful, new { onclick = "makeVisible(this);" })%>
                            <label for="NotUseful">I don't find the website useful</label>
                            <div class="popupBox" style="display:none;" id="divNotUseful">
                                <p>You should find the site more helpful if:</p>
                                <ul>
                                    <li>you upload a resume to receive job offers <%= Html.RouteRefLink("here", ProfilesRoutes.Profile) %></li>
                                    <li>keep your resume up-dated as employers contact fresh resumes first <%= Html.RouteRefLink("here", ProfilesRoutes.Profile) %></li>
                                    <li>search and apply for jobs <%= Html.RouteRefLink("here", SearchRoutes.Search) %>, or</li>
                                    <li>invite people to your network to open other opportunities <a href="<%=NavigationManager.GetUrlForPage<LinkMe.Web.Members.Friends.InviteFriends>()%>">here</a>.</li>
                                </ul>
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.NoJobFound, new { onclick = "makeVisible(this);" })%>
                            <label for="CouldntFindJob">I couldn't find the job I was looking for</label>
                            <div class="popupBox" style="display:none;" id="divCouldntFindJob">
                                You can be notified about your perfect job by setting up job email alerts
                                <%= Html.RouteRefLink("here", SearchRoutes.SavedSearches) %>.
                                Otherwise make sure your resume presents well and is up-to-date to increase
                                your chances of employers contacting you directly <%= Html.RouteRefLink("here", ProfilesRoutes.Profile) %>.
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.DifferentLogin, new { onclick = "makeVisible(this);" })%>
                            <label for="DifferentLogin">I am going to join using different login details</label>
                            <div class="popupBox" style="display:none;" id="divDifferentLogin">
                                You can in fact retain an account but change the email address
                                <%= Html.RouteRefLink("here", SettingsRoutes.Settings, null)%>.
                            </div>
                        </li>
                        <li>
                            <%= Html.RadioButton("Reasons", DeactivationReason.Other, new { onclick = "makeVisible(this);" })%>
                            <label for="Other">Other</label>
                        </li>
                    </ul>
                </p>

                <p>
                
     <%  using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
       { %>
       
            <%= Html.MultilineTextBoxField("Comments", string.Empty).WithLabel("Any further comments?").WithLargestWidth() %>
            <%= Html.ButtonsField().Add(new DeactivateButton()).Add(new CancelButton()) %>
       
    <% }
   } %>    
   
                </p>
            </div>
        </div>
        
    </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        #reasons
        {
            list-style: none;
            line-height: 25px;
            padding: 0px;
        }
        
        #reasons label
        {
            margin-left: 5px;
        }
        
        .popupBox
        {
            background: #ECF2FF;
            text-align: left;
            color: #000;
            padding: 7px 10px;
            border: #82A1BA 1px solid;
            line-height: 16px;
        }
    </style>
    
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.HeaderAndNav) %>
    </mvc:RegisterStyleSheets>

</asp:Content>
