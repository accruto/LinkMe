<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Block.Page.Master" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewPage<LinkMe.Web.Areas.Employers.Models.JobAds.EditJobAdModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls" %>
<%@ Import Namespace="LinkMe.Domain" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>
<%@ Import Namespace="LinkMe.Web.Areas.Api.Routes" %>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.StyleSheet(StyleSheets.Block.LeftSideBar) %>
        <%= Html.RenderStyles(StyleBundles.Block.Forms) %>
        <%= Html.RenderStyles(StyleBundles.Block.Employers.JobAds.JobAd) %>
        <%= Html.StyleSheet(StyleSheets.JQueryCustom) %>
        <%= Html.StyleSheet(StyleSheets.JQueryAutocomplete) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiCore) %>
        <%= Html.JavaScript(JavaScripts.JQueryUiDatePicker) %>
        <%= Html.JavaScript(JavaScripts.Autocomplete) %>
        <%= Html.RenderScripts(ScriptBundles.Employers.JobAds.JobAd) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="BreadCrumbs" runat="server">
    <ul class="breadcrumbs">
        <li>Employer site</li>
        <li class="current-breadcrumb"><%= Model.IsNew ? "New" : "Edit" %> job ad</li>
    </ul>
</asp:Content>

<asp:Content ContentPlaceHolderID="LeftSidebarContent" runat="server">
    <% Html.RenderPartial("Help"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1><%= Model.IsNew ? "New job ad" : HtmlUtil.HtmlToText(Model.JobAd.Title) %></h1>
    </div>
    
    <div class="form">
<%  using (Html.RenderForm(Context.GetClientUrl(), true))
    {
        using (Html.RenderFieldSet(FieldSetOptions.LabelsOnLeft))
        { %>
        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Job details</h2>
                </div>
            
                <div class="section-content">
                    <%= Html.TextBoxField(Model, m => m.JobAd.Title)
                        .WithLabel("Job ad title").WithIsRequired().WithLargestWidth() %>
                    <%= Html.TextBoxField(Model, m => m.JobAd.PositionTitle)
                        .WithLabel("Position title").WithLargestWidth() %>
                    <%= Html.DateField(Model, m => m.JobAd.ExpiryTime)
                        .WithLabel("Job ad closes on") %>
                    <%= Html.TextBoxField(Model, m => m.JobAd.ExternalReferenceId)
                        .WithLabel("Reference #") %>
            
                    <% Html.RenderPartial("JobAdLogo", Model); %>

                    <%= Html.TextBoxField(Model, "BulletPoint1", m => m.JobAd.BulletPoints != null && m.JobAd.BulletPoints.Count > 0 ? m.JobAd.BulletPoints[0] : null)
                        .WithLabel("Key bullet points").WithLargerWidth() %>
                    <%= Html.TextBoxField(Model, "BulletPoint2", m => m.JobAd.BulletPoints != null && m.JobAd.BulletPoints.Count > 1 ? m.JobAd.BulletPoints[1] : null)
                        .WithLabel("").WithLargerWidth() %>
                    <%= Html.TextBoxField(Model, "BulletPoint3", m => m.JobAd.BulletPoints != null && m.JobAd.BulletPoints.Count > 2 ? m.JobAd.BulletPoints[2] : null)
                        .WithLabel("").WithLargerWidth() %>

                    <%= Html.MultilineTextBoxField(Model, m => m.JobAd.Summary).WithSize(3, 50)
                        .WithLabel("Job summary").WithLargestWidth() %>
                    <%= Html.MultilineTextBoxField(Model, m => m.JobAd.Content).WithSize(10, 50)
                        .WithLabel("Job content").WithLargestWidth().WithIsRequired() %>
                    
                    <%= Html.CheckBoxField(Model, m => m.JobAd.ResidencyRequired)
                        .WithLabelOnRight("Position open only to people with Australian / New Zealand non-restricted working visas.") %>

                    <%= Html.TextBoxField(Model, m => m.JobAd.Package)
                        .WithLabel("Package")
                        .WithLargestWidth()
                        .WithExampleText("e.g. 9% Super, car, mobile phone, tools of the trade") %>

                    <%= Html.TextBoxField(Model, m => m.JobAd.CompanyName)
                        .WithLabel("Company").WithLargerWidth() %>
                    <%= Html.CheckBoxField(Model, m => m.JobAd.HideCompany)
                        .WithLabelOnRight("Hide company from job applicants") %>
                </div>            
            </div>
            <div class="section-foot"></div>
        </div>

        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Contact details</h2>
                </div>
        
                <div class="section-content">
                    <%= Html.NameTextBoxField(Model, m => m.JobAd.ContactDetails.FirstName)
                        .WithLabel("First name").WithLargerWidth() %>
                    <%= Html.NameTextBoxField(Model, m => m.JobAd.ContactDetails.LastName)
                        .WithLabel("Last name").WithLargerWidth() %>
                    <%= Html.EmailAddressTextBoxField(Model, m => m.JobAd.ContactDetails.EmailAddress)
                        .WithLabel("Email address").WithLargerWidth().WithIsRequired() %>
                    <%= Html.TextBoxField(Model, m => m.JobAd.ContactDetails.SecondaryEmailAddresses)
                        .WithLabel("Secondary email addresses")
                        .WithLargerWidth()
                        .WithExampleText("use commas to separate multiple emails") %>
                    <%= Html.TextBoxField(Model, m => m.JobAd.ContactDetails.PhoneNumber)
                        .WithLabel("Phone number") %>
                    <%= Html.TextBoxField(Model, m => m.JobAd.ContactDetails.FaxNumber)
                        .WithLabel("Fax number") %>

                    <%= Html.CheckBoxField(Model, m => m.JobAd.HideContactDetails)
                        .WithLabelOnRight("Hide contact details from job applicants") %>

                </div>
            </div>
            <div class="section-foot"></div>
        </div>

        <div class="shadowed-section section">
            <div class="section-head"></div>
            <div class="section-body">
                <div class="section-title">
                    <h2>Job category</h2>
                </div>
        
                <div class="section-content">
                    <%= Html.IndustriesField(Model, m => m.JobAd.IndustryIds, Model.Reference.Industries).WithSize(10)
                        .WithLabel("Industry").WithIsRequired() %>
                    <%= Html.CountryField(Model, "CountryId", m => m.JobAd.Location == null ? Model.Reference.DefaultCountry.Id : m.JobAd.Location.Country.Id, Model.Reference.Countries)
                        .WithLabel("Country") %>
                    <%= Html.TextBoxField(Model, "Location", m => m.JobAd.Location == null ? "" : m.JobAd.Location.ToString())
                        .WithLabel("Suburb / State / Postcode")
                        .WithIsRequired()
                        .WithLargestWidth()
                        .WithExampleText("Start typing the city, state, or postcode and we'll complete the details for you.") %>
            
                    <%= Html.ControlsField()
                        .WithLabel("Annual salary range ($)")
                        .Add(Html.TextBoxField(Model, "SalaryLowerBound", m => m.JobAd.Salary != null && m.JobAd.Salary.LowerBound != null ? (int)m.JobAd.Salary.LowerBound : (int?)null)
                            .WithLabel("to")
                            .WithShorterWidth())
                        .Add(Html.TextBoxField(Model, "SalaryUpperBound", m => m.JobAd.Salary != null && m.JobAd.Salary.UpperBound != null ? (int)m.JobAd.Salary.UpperBound : (int?)null)
                            .WithLabel("")
                            .WithShorterWidth()) %>
                    
                    <%= Html.CheckBoxesField(Model, m => m.JobAd.JobTypes).Without(JobTypes.None).Without(JobTypes.All)
                        .WithLabel("Job type")
                        .WithLabel(JobTypes.FullTime, "Full time")
                        .WithLabel(JobTypes.PartTime, "Part time")
                        .WithLabel(JobTypes.JobShare, "Job share")
                        .WithIsRequired() %>

                </div>
            </div>
            <div class="section-foot"></div>
        </div>
<%      } %>

        <div class="right-buttons-section section">
            <div class="section-body">

<%      if (CurrentEmployer != null)
        { %>
            <%= Html.ButtonsField(new PreviewButton(), new SaveButton())%>
<%      }
        else
        { %>
            <%= Html.ButtonsField(new PreviewButton()) %>
<%      } %>
            
            </div>
        </div>
                
<%  } %>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            linkme.employers.jobads.jobad.ready({
                urls: {
                    apiLocationPartialMatchesUrl: '<%= Html.RouteRefUrl(LocationRoutes.PartialMatches) %>?countryId=1&location='
                },
            });
        });
    </script>

</asp:Content>
