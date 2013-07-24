<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="DiaryPreview.aspx.cs" Inherits="LinkMe.Web.UI.Registered.Networkers.DiaryPreview" MasterPageFile="~/master/StandardMasterPage.master" %>
<%@ MasterType VirtualPath="~/master/StandardMasterPage.master" %>
<%@ Register TagPrefix="uc" TagName="DiaryTextSections" Src="~/ui/controls/common/ResumeEditor/DiaryTextSections.ascx" %>

<asp:Content ContentPlaceHolderID="StyleSheet" runat="server">
    <style type="text/css">
        body { background: none; }
        #container { background:none; }
        #body-container { background:none; }
        #community-header { display:none; }
        #community-brought { display:none; }
        #community-footer { display:none; }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="resume_noprint preview-actions">
        <a href="javascript:window.close()">Close this window</a>
    </div>
    
    <% if (PrintMode) { %>
        <div class="resume_noprint printing-advice">
            <strong>For best results, ensure printing of backgrounds is switched on for your browser.</strong>
            <ul>
                <li><strong>Internet Explorer</strong>: Go to Tools &gt; Internet Options, Advanced tab. Scroll
                down the list until you see &quot;Printing&quot;. Check &quot;Print background colors and 
                images&quot;, then click OK. Click Print on the toolbar to finish.</li>
                <li><strong>Firefox</strong>: Cancel this Print dialog. Go to File &gt; Page Setup, and check
                &quot;Print Background (colors and images)&quot;. Go to File &gt; Print and click OK.</li>
                <li><strong>Safari (Mac)</strong>: In the Print dialog, choose &quot;Safari&quot; from the
                pulldown which defaults to &quot;Copies & Pages&quot;. Check &quot;Print backgrounds&quot;, and
                click Print.</li>
            </ul>
            <br />
            This tip will not be printed.
        </div>
    <% } %>

    <asp:PlaceHolder id="phContent" runat="server" visible="false">
        <div class="resume read-only_resume">
               
	        <div class="header">
                <div class="bg_l"><div class="bg_r"><div class="bg_tl"><div class="bg_bl"><div class="bg_tr"><div class="bg_br">
                    <div class="name-title">
                        Continuing Professional Development Diary
                    </div>
                    
                    <br class="clearer" />                    
                </div></div></div></div></div></div>
            </div>
			
            <uc:DiaryTextSections id="ucDiaryTextSections" runat="server" />
        </div>

        <% if (PrintMode) { %>
            <script>
                window.onload = window.print();
            </script>
        <% } %>
    </asp:PlaceHolder>
</asp:Content>
