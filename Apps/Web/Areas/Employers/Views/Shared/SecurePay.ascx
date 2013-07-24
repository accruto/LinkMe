<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="LinkMe.Web.Content" %>

<div class="section hint-section">
    <div class="section-body">
        <p>We use SecurePay to ensure all payments are securely processed.</p>
        <img src="<%= Images.Block.SecurePay %>" style="display: block; margin: 0 auto; padding-top: 5px;" alt="Payment Gateway by SecurePay" />
    </div>
</div>
