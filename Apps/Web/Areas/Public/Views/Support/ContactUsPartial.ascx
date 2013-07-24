<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ContactUsModel>" %>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Web.Areas.Public.Models.Support"%>

<div class="overlay contactus">
    <div class="title">Contact LinkMe</div>
    <div class="section byemail">
        <div class="titlebar">
            <div class="icon"></div>
            <div class="title">By email</div>
        </div>
		<div class="validation-success">
		    <div class="icon"></div>
		    <div class="prompt"></div>
		</div>
	    <div class="validation-errors">
	        <div>
		        <div class="icon"></div>
		        <div class="prompt">There are some errors, please correct them below.</div>
	        </div>
	        <ul>
	        </ul>
		</div>
        <div class="fields">
            <%= Html.TextBoxField(Model.EmailUs, d => d.Name).WithIsRequired() %>
            <%= Html.TextBoxField(Model.EmailUs, d => d.From).WithLabel("Email address").WithIsRequired()%>
            <%= Html.TextBoxField(Model.EmailUs, d => d.PhoneNumber).WithLabel("Phone").WithCssPrefix("phone")%>
            <%= Html.DropDownListField(Model.EmailUs, d => d.UserType, Model.UserTypes).WithText(s => s == UserType.Member ? "a candidate" : "an employer").WithLabel("I am...").WithIsRequired().WithAttribute("size", "2")%>
            <%= Html.DropDownListField(Model.EmailUs, d => d.SubcategoryId, new List<Guid?> { null }.Concat(Model.MemberSubCategories.Select(s => (Guid?)s.Id)))
                    .WithText(i => i == null ? "Report a site issue" : Model.MemberSubCategories.Single(s => s.Id == i).Name).WithId("MemberSubcategoryId").WithCssPrefix("formember").WithLabel("Type of enquiry").WithIsRequired().WithAttribute("size", "10") %>
            <%= Html.DropDownListField(Model.EmailUs, d => d.SubcategoryId, new List<Guid?> { null }.Concat(Model.EmployerSubCategories.Select(s => (Guid?)s.Id)))
                    .WithText(i => i == null ? "Report a site issue" : Model.EmployerSubCategories.Single(s => s.Id == i).Name).WithId("EmployerSubcategoryId").WithCssPrefix("foremployer").WithLabel("Type of enquiry").WithIsRequired().WithAttribute("size", "9") %>
            <%= Html.MultilineTextBoxField(Model.EmailUs, d => d.Message).WithIsRequired() %>
        </div>
        <div class="buttons">
            <div class="button send"></div>
            <div class="button cancel eighty"></div>
        </div>
        <div class="divider"></div>
        <div class="divider light"></div>
    </div>
    <div class="section bymail">
        <div class="titlebar">
            <div class="icon"></div>
            <div class="title">By mail</div>
        </div>
        <div class="detail">
            <div class="title">Send all inquiries by post to:</div>
            <div class="desc">
                <span>LinkMe Pty Ltd</span><br />
                <span>PO Box 408</span><br />
                <span>NEUTRAL BAY NSW 2089</span><br />
                <span>AUSTRALIA</span><br />
            </div>
        </div>
    </div>
    <div class="section byphone">
        <div class="titlebar">
            <div class="icon"></div>
            <div class="title">By phone</div>
        </div>
        <div class="detail">
            <div class="title">For all enquiries:</div>
            <div class="desc block">
                <span>Toll-free (within Australia)</span><br />
                <span><%= LinkMe.Apps.Agents.Constants.PhoneNumbers.FreecallHtml %></span><br />
            </div>
            <div class="desc block">
                <span>International</span><br />
                <span><%= LinkMe.Apps.Agents.Constants.PhoneNumbers.InternationalHtml %></span><br />
            </div>
        </div>
    </div>
</div>