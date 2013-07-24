<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPages/Page.Master" Inherits="System.Web.Mvc.ViewPage<JobAdQuestionsModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Content" %>
<%@ Import Namespace="LinkMe.Web.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Content"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Web.Areas.Members.Models.JobAds"%>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <mvc:RegisterStyleSheets runat="server">
        <%= Html.RenderStyles(StyleBundles.HeaderAndNav) %>
        <%= Html.RenderStyles(StyleBundles.JobAds) %>
    </mvc:RegisterStyleSheets>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">
    <mvc:RegisterJavaScripts runat="server">
        <%= Html.JavaScript(JavaScripts.JQuery162) %>
        <%= Html.JavaScript(JavaScripts.JQueryCustom)%>
        <%= Html.JavaScript(JavaScripts.JobAd) %>
    </mvc:RegisterJavaScripts>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("ValidationSummary"); %>

    <div class="page-title">
        <h1><%= Html.Encode(Model.JobAd.Title) %></h1>
    </div>
    
    <div class="jobadquestions">
    
<%  using (Html.RenderForm(Context.GetClientUrl()))
    {
        using (Html.BeginFieldSet(FieldSetOptions.LabelsOnLeft))
        {
            if (Model.JobAd.Application.IncludeCoverLetter && string.IsNullOrEmpty(Model.Application.CoverLetterText))
            { %>
        	    <%= Html.MultilineTextBoxField("CoverLetter", Model.Member != null ? Model.JobAd.GetDefaultCoverLetter(Model.Member) : Model.JobAd.GetDefaultCoverLetter(Model.AnonymousContact))
        	        .WithLabel("Cover letter")
                    .WithIsRequired()   
        	        .WithAttribute("maxlength", "1000") %>
<%          }
            
            if (Model.JobAd.Integration.ApplicationRequirements != null && Model.JobAd.Integration.ApplicationRequirements.Questions != null)
            {
                foreach (var question in Model.JobAd.Integration.ApplicationRequirements.Questions.OrderBy(q => q.Id))
                {
                    var multipleChoiceQuestion = question as MultipleChoiceQuestion;
                    if (multipleChoiceQuestion != null)
                    { %>
                        <%= Html.DropDownListField(multipleChoiceQuestion, "Question" + multipleChoiceQuestion.Id, q => q.Answers[0].Value, (from a in multipleChoiceQuestion.Answers select a.Value))
                            .WithText(v => (from a in multipleChoiceQuestion.Answers where a.Value == v select a.Text).Single())
                            .WithLabel(question.Text).WithAttribute("size", "5") %>
<%                  }
                    else
                    { %>
                        <%= Html.TextBoxField("Question" + question.Id, string.Empty)
                            .WithLargerWidth()
                            .WithLabel(question.Text)
                            .WithIsRequired(question.IsRequired) %>
<%                  }
                } %>
                
                <%= Html.ButtonsField().Add(new SendButton()) %>
<%          }
        }
    } %> 
    
    </div>       

</asp:Content>

