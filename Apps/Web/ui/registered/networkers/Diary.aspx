<%@ Page language="c#" Codebehind="Diary.aspx.cs" AutoEventWireup="False" ValidateRequest="false" Inherits="LinkMe.Web.UI.Registered.Networkers.Diary" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Validation"%>
<%@ Import namespace="LinkMe.Utility.Validation"%>
<%@ Import namespace="LinkMe.Web.UI.Registered.Networkers"%>
<%@ Import namespace="LinkMe.Web.UI.Controls.Common.ResumeEditor"%>
<%@ Register TagPrefix="cc" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<%@ Register TagPrefix="uc" TagName="DiaryTextSections" Src="~/ui/controls/common/ResumeEditor/DiaryTextSections.ascx" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
	<style type="text/css">
        #secondary-nav {
	        margin-bottom: 0;
        }
	</style>
</asp:Content>

<asp:Content ContentPlaceHolderID="JavaScript" runat="server">

    <script type="text/javascript">		
		LinkMeUI.JSLoadHelper.LoadValidationLibrary();
		LinkMeUI.JSLoadHelper.LoadLocateHelper();
		LinkMeUI.JSLoadHelper.LoadStringUtils();
    </script>
	<script type="text/javascript">
		function GetErrorSummaryHtml() {
		    <%--
		        /* 
		        AS: 21.12.2007: Do not use $$ (Prototype CSS selector here, because they
		        perform poorly in IE.
		        
		        If they got faster in 1.6, then possibly use them
		        */
		    --%> 
			var errorElements = LinkMeUI.LocateHelper.GetElementsByClassName(document.body, 'error');					
			errorElements = errorElements.concat(LinkMeUI.LocateHelper.GetElementsByClassName(document.body, 'resume_error'));
			for (i = 0; i < errorElements.length; i++)	{
			/*
			AS: TODO 3.0.1: Talk to whoever created this function and ask - why 
			body-property class is needed
			*/
				if (errorElements[i].innerHTML != '' && errorElements[i].parentNode.className == 'body-property') {
					return "<p><strong>Sorry, we couldn't understand parts of your diary.</strong></p><p>Please review it below:</p>";
				}
			}
			return '';
		}

		function DisplayErrors() {
			var errorBox = $('error_summary');
			var errors = GetErrorSummaryHtml();

			if (errors == '') {
				errorBox.hide();
			} else {
				errorBox.show();
			}
			errorBox.update(errors);
		}

		var SalaryPattern = /<%= LinkMe.Domain.RegularExpressions.SalaryPattern %>/;
		var PHONE_REGEX = /^<%= LinkMe.Domain.RegularExpressions.CompletePhoneNumberPattern %>$/;
	</script>
				
	<%--
		AS: Do not merge together these two script sections. (one above and one below)
		For the reason why see Javascript.ascx file, 'Important comment on browser's behaviour'
	--%>

	<script type="text/javascript" language="javascript">
		LinkMeUI.JSLoadHelper.LoadSectionsEditor();
	</script>

</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <style type="text/css">
        .update_div {
            background:#FFFFDD none repeat scroll 0 0;
            border:1px solid #DDDD88;
            margin-top:10px;
            margin-left: 15px;
            margin-bottom:20px;
            padding:7px;
            width:50em;
        }
    </style>

    <%-- //Uncomment button below to run performance tests on Prototype's .select() --%>
    <%-- <input type="button" onclick="javascript:LinkMeUI.Editor.Stats.TempTest(10);" value="click" /> --%>
	<%--
	17.12.2007 AS: This iframe has been moved to the top of the page
	in order to allow early editing of the resume parts, which require
	it for browser' history management purposes.

	Potentially will be removed at all when proper history library is used.
	--%>
	<iframe id="iframeAjaxHistory" style="display:none;" src="javascript:false;"></iframe>
	<!-- main resume -->
	<cc:Loader id="ccDiaryLoader" runat="server" ControlToHide="mainctrl" Message="Loading your diary, please wait..."> 
	    <div id="mainctrl" runat="server">
	        <div id="error_summary"></div>
	        
	        <div class="ribbon">
	            <div class="ribbon_inner">
	                <ul class="plain_horizontal_action-list horizontal_action-list action-list">
		                <li><a class="preview-resume-action" href="<%= GetUrlForPage<DiaryPreview>() %>" target="_blank">Preview diary</a></li>
		                <li><a class="print-resume-action" href="<%= GetUrlForPage<DiaryPreview>(DiaryPreview.PrintModeParam, bool.TrueString) %>" target="_blank">Print</a></li>
	                </ul>
	            </div>
	        </div>
            
		    <div class="resume editable_resume">
                <div class="bg_l"><div class="bg_r"><div class="bg_tl"><div class="bg_bl"><div class="bg_tr"><div class="bg_br">
                
		            <div class="header">
                        <div class="bg_l"><div class="bg_r"><div class="bg_tl"><div class="bg_bl"><div class="bg_tr"><div class="bg_br">
                        
			            <div>
			            
			                <div class="name-title">Continuing Professional Development Diary</div>
            		        <br class="clearer" />

			            </div>
			            
			            <br class="clearer" />
		                </div></div></div></div></div></div>
		            </div>

		            <uc:DiaryTextSections id="ucDiaryTextSections" runat="server" />
		            
		        </div></div></div></div></div></div>
		    </div>
	    </div>
    </cc:Loader>

</asp:Content>

<asp:Content ContentPlaceHolderID="NonFormContent" runat="server">
    <script type="text/javascript">
        Event.observe(window, 'load', function() {DisplayErrors();});        
    </script>
</asp:Content>

