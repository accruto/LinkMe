<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="System.Web.Mvc.ViewPage<LinkMe.Web.Areas.Employers.Models.Settings.SettingsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing" %>
<%@ Import Namespace="LinkMe.Web.Areas.Employers.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content" %>
<%@ Import Namespace="LinkMe.Web.Html" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.Page) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    
    <% Html.RenderPartial("LinkedInAuth"); %>

    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb">Settings</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <% Html.RenderPartial("LeftSideBar"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1>Account settings</h1>
    </div>

    <div class="form">

<%  using (Html.RenderRouteForm(SettingsRoutes.Settings))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>

        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Contact details</h2>
                </div>
            
                <%= Html.NameTextBoxField(Model, m => m.FirstName)
                        .WithLabel("First name").WithIsRequired() %>
                <%= Html.NameTextBoxField(Model, m => m.LastName)
                        .WithLabel("Last name").WithIsRequired() %>

                <p>
                    <br/>
                    The email address that you specify will be used to contact you and notify you regarding your account.
                    We will not release this email address outside of LinkMe.
                </p>
                <%= Html.TextBoxField(Model, m => m.EmailAddress).WithLargerWidth()
                        .WithLabel("Email address").WithIsRequired().WithIsReadOnly(true) %>
                <%= Html.TextBoxField(Model, m => m.PhoneNumber)
                        .WithLabel("Phone number").WithIsRequired() %>

        	</div>
            <div class="section-foot"></div>
        </div>

        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Credits</h2>
                </div>
        
                <p>
                    You currently have the following active credits:
                </p>
                
                <div class="control">
                    <ul>
<%  foreach (var allocation in Model.Allocations)
    {
        var quantity = allocation.Value == null
            ? "none remaining"
            : allocation.Value.IsUnlimited
                ? "unlimited"
                : allocation.Value.RemainingQuantity.Value == 0
                    ? "none remaining"
                    : allocation.Value.RemainingQuantity.Value + " remaining";
        var expiry = allocation.Value.NeverExpires
            ? "never expiring"
            : allocation.Value.ExpiryDate.Value != DateTime.MinValue
                ? "expiring on " + allocation.Value.ExpiryDate.Value.ToShortDateString()
                : ""; %>

            		    <li>
            		        <%= allocation.Key.ShortDescription %>: <%= quantity %><%= !string.IsNullOrEmpty(expiry) ? ", " + expiry : ""%>
            		    </li>
<%  } %>
                    </ul>
                </div>

                <ul class="actions corner-actions">
                    <li><a href="<%= ProductsRoutes.Credits.GenerateUrl() %>">View all credit details</a></li>
                    <li><a href="<%= ProductsRoutes.NewOrder.GenerateUrl() %>">Purchase credits</a></li>
                </ul>
        	</div>
            <div class="section-foot"></div>
        </div>

        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Username</h2>
                </div>
                
                <p>
                    The username is your unique identifier that you will use to login to LinkMe.
                    You may choose to specify your email address as your username or something else entirely.
                </p>
            
                <%= Html.TextBoxField(Model, m => m.LoginId)
                        .WithLabel("Username").WithIsRequired(Model.HasLoginCredentials) %>
                        
<%  if (!Model.HasLoginCredentials)
    { %>
                <%= Html.PasswordField(Model, m => m.Password)
                        .WithLabel("Password") %>
                <%= Html.PasswordField(Model, m => m.ConfirmPassword)
                        .WithLabel("Confirm password") %>
<%  } %>

<%  if (Model.HasLoginCredentials)
    { %>
                <ul class="actions corner-actions">
                    <li><%= Html.RouteRefLink("Change my password", LinkMe.Web.Areas.Accounts.Routes.AccountsRoutes.ChangePassword, null)%></li>
                </ul>
<%  } %>

        	</div>
            <div class="section-foot"></div>
        </div>

<%  if (Model.HasLoginCredentials)
    { %>
        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>LinkedIn profile&nbsp;<img class="linkedin" src="<%= Images.LinkedIn16 %>"/></h2>
                </div>

<%      if (Model.UseLinkedInProfile)
        { %>
                <%= Html.CheckBoxField(Model, m => m.UseLinkedInProfile).WithLabelOnRight("Use my LinkedIn profile to log into LinkMe") %>
<%      }
        else
        { %>
                <p>
                    To make it easier to log into our site you can use your LinkedIn username and password.
                </p>
                <div class="control">
                    <script type="in/Login"></script>
                </div>
<%      } %>

        	</div>
            <div class="section-foot"></div>
        </div>
<%  } %>

        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Company details</h2>
                </div>
            
                <%= Html.TextBoxField(Model, m => m.OrganisationName).WithLargerWidth()
                        .WithLabel("Company").WithIsRequired().WithIsReadOnly(!Model.CanEditOrganisationName) %>
                <%= Html.IndustriesField(Model, m => m.IndustryIds, Model.Industries).WithLabel("Industries") %>
                <%= Html.TextBoxField(Model, m => m.JobTitle).WithLabel("Job title").WithLargerWidth() %>
                <%= Html.RadioButtonsField(Model, m => m.Role) %>

        	</div>
            <div class="section-foot"></div>
        </div>

        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Suggested candidates</h2>
                </div>
                
                <div class="control">
                    When I post a job ad:
                </div>
                
                <%= Html.CheckBoxField(Model, m => m.ShowSuggestedCandidates).WithLabelOnRight("Show me suggested candidates") %>
                <%= Html.CheckBoxField(Model, m => m.SendSuggestedCandidates).WithLabelOnRight("Email suggested candidates to the contact person") %>

                <div class="control">
                    <br/>
                    When I am the contact person for a job ad:
                </div>
                <%= Html.CheckBoxField(Model, m => m.ReceiveSuggestedCandidates).WithLabelOnRight("Email suggested candidates to me")%>
                <div class="control">
                    <br/>
                    Note that you will only receive an email if the job poster has also checked
                    &quot;Email Suggested Candidates to the contact person&quot;.
                </div>
        	</div>
            <div class="section-foot"></div>
        </div>
    
        <div class="shadowed-section shadowed_section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Communications</h2>
                </div>

                <%= Html.CheckBoxField(Model, m => m.EmailEmployerUpdate).WithLabelOnRight("Email the monthly performance update to me") %>
                <%= Html.CheckBoxField(Model, m => m.EmailCampaign).WithLabelOnRight("Email news and general notices to me") %>
        	</div>
            <div class="section-foot"></div>
        </div>

<%      } %>

        <div class="right-buttons-section section">
            <div class="section-body">
                <%= Html.ButtonsField(new SaveButton(), new CancelButton()) %>
            </div>
        </div>

<%  } %>
        
    </div>

</asp:Content>

