<%@ Control Language="C#" Inherits="LinkMe.Web.Areas.Employers.Views.Candidates.ResumeSummary" %>
<%@ Import Namespace="LinkMe.Framework.Utility"%>

<div class="self-summary_section resume_section" id="self-summary">
    <div class="resume-heading">Self-summary</div>
    <div class="resume-content">
        <%= Model.View.Resume == null || Model.View.Resume.Summary == null ? null : HtmlUtil.LineBreaksToHtml(HighlightKeywords(Model.View.Resume.Summary)) %>
    </div>
</div>
