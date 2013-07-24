<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="JoinToApplySteps.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Networkers.JoinToApplySteps" %>

<asp:PlaceHolder ID="phJoinToApplyStepsWrapper" runat="server">
    <style type="text/css">
    .join-steps_ascx {
        text-align: center;
        padding: 10px 0;
        margin-bottom: 10px;
        word-spacing: -1ex;
    }
        .join-steps_ascx .step-image,
        .join-steps_ascx .step-label {
            display: inline;
            display: -moz-inline-box;
            display: inline-block;
            zoom: 1;
            vertical-align: middle;
            word-spacing: normal;
        }
        
        .join-steps_ascx .step-label {
            padding: 0; margin: 0;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: bold;
        }
        
        .join-steps_ascx .step-image {
            padding-left: 10px;
        }
        .join-steps_ascx .step-label {
            padding-left: 5px;
            padding-right: 10px;
        }
    </style>

    <asp:PlaceHolder ID="phHighlightOne" Visible="false" runat="server">
        <style type="text/css">
            .join-steps_ascx #join-steps-one {
                color:#FF6600;
            }
        </style>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phHighlightTwo" Visible="false" runat="server">
        <style type="text/css">
            .join-steps_ascx #join-steps-two {
                color:#FF6600;
            }
        </style>
    </asp:PlaceHolder>

    <div class="join-steps_ascx">
        <asp:Image ID="imgStepOne" CssClass="step-image" runat="server" />
        <span id="join-steps-one" class="step-label">Sign in</span>
        <asp:Image ID="imgStepTwo" CssClass="step-image" runat="server" />
        <span id="join-steps-two" class="step-label">Create your application</span>
    </div>
    <div style="clear:both;"></div>
</asp:PlaceHolder>