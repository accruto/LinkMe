<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LinkMe.Web.Areas.Employers.Models.PaymentModel>" %>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields" %>
<%@ Import Namespace="LinkMe.Web.Html" %>

<div class="shadowed-section shadowed_section section">
    <div class="section-head"></div>
    <div class="section-body">
        <div class="section-content">
<%  using (Html.BeginFieldSet())
    { %>
            <%= Html.CheckBoxField(Model, "UseDiscount", m => m.UseDiscount)
                .WithLabelOnRight("Please confirm your organisation is a member of VECCI to receive a 20% member discount") %>
<%  } %>
        </div>
    </div>
    <div class="section-foot"></div>
</div>
