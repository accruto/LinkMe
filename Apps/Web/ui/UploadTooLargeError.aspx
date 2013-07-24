<%@ Page language="c#" Codebehind="UploadTooLargeError.aspx.cs" AutoEventWireup="False" Inherits="LinkMe.Web.UI.UploadTooLargeError" %>
<%@ Import namespace="LinkMe.Web.Configuration"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<HTML>
  <HEAD>
    <title>LinkMe - Uploaded File Too Large</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK media="screen" href="styles/default.css" type="text/css" rel="stylesheet">
  </HEAD>
  <BODY topMargin="0" leftMargin="0" bottomMargin="0" >
  <DIV style="TEXT-ALIGN: center">
		
<!-- Top Navigation -->
<TABLE cellpadding="0" cellspacing="0" border="0" width="770" ID="Table1">
	<TR valign="middle">
		<TD bgcolor="#474747" width="20">
			<IMG id="ucTopNavBlank_Image1" src="<%= ApplicationPath %>/ui/img/blank.gif" border="0" style="WIDTH:20px;HEIGHT:22px" >
		</TD>
		<TD bgcolor="#474747" width="100%">
		</TD>
		<TD bgcolor="#474747" width="20">
			<IMG id="ucTopNavBlank_Image2" src="<%= ApplicationPath %>/ui/img/blank.gif" border="0" style="WIDTH:20px;HEIGHT:22px" >
		</TD>
	</TR>
</TABLE>
<IMG id="ucTopNavBlank_Image3" src="<%= ApplicationPath %>/ui/img/blank.gif" border="0" style="WIDTH:1px;HEIGHT:1px" ><BR>

		<TABLE cellSpacing="0" cellPadding="0" width="770" border="0" id="Table2">
			<TR valign="middle">
				<TD height="100%">
					<TD bgcolor="#e76800" width="154">
						<A href="<%= HomeUrl %>"><IMG src="<%= ApplicationPath %>/ui/img/linkme_logo_sm.gif" alt="<%= LinkMe.Common.Constants.SLOGAN %>" border="0" style="WIDTH:180px;HEIGHT:70px" ></A>
					</TD>
					<TD bgcolor="#e76800" width="10">
						<IMG src="<%= ApplicationPath %>/ui/img/blank.gif" border="0" style="WIDTH:10px;HEIGHT:70px" >
					</TD>
				</TD>
				<TD bgcolor="#e76800" width="100%">
					<SPAN class="pageheading">Uploaded File Too Large</SPAN>
				</TD>
				<TD />
			</TR>
		</TABLE>
		<TABLE cellSpacing="0" cellPadding="0" width="770" id="Table3">
			<TR valign="top">
				<TD width="1" bgcolor="#cccccc">
					<IMG id="Image2543" src="<%= ApplicationPath %>/ui/img/blank.gif" border="0" style="WIDTH:1px;HEIGHT:1px" >
				</TD>
				<TD bgcolor="#e8eada" width="180">
					<FORM id="frmLeftName" name="frmLeftName">
<INPUT type='hidden' name='LinkMe.SecondaryForm'  value='frmLeftName' ID="Hidden1">
<INPUT name="txtPageIdentifier" type="text" value="UploadTooLargeError" id="txtPageIdentifier" style="DISPLAY: none"><TD WIDTH="180" 
BGCOLOR="#e8eada"><IMG id="Image1" border="0" alt="" src="<%= ApplicationPath %>/ui/img/side.gif" style="WIDTH:180px;HEIGHT:396px" ><BR><BR><BR></TD>
					</FORM>
				</TD>
				<TD width="100%">
<TABLE CELLSPACING=0 CELLPADDING=0 WIDTH="100%" BORDER=0 ID="Table4">
  <TR VALIGN=top>
    <TD CLASS=subhead1 WIDTH="100%" 
    background="<%= ApplicationPath %>/ui/img/bg_head.gif">Uploaded File Too Large</TD></TR>
  <TR VALIGN=top>
    <TD WIDTH="100%">
      <TABLE CELLSPACING=0 CELLPADDING=10 WIDTH="100%" BORDER=0 ID="Table5">
		<TR>
  		  <TD><BR>The file you uploaded is too large. The maximum accepted file size is
  		  <%= WebConstants.MAX_UPLOAD_FILE_SIZE_DISPLAY_TEXT %>.
  			<br><br>Please click Back to go back to the previous page and try again using a smaller file.
  		  </TD>
		</TR>
	   </TABLE>
	</TD>
  </TR>
  <TR>
	<TD align=right>
  	  <FORM name="Form1">
	    <INPUT CLASS=btntiny ID=btnBack runat="server" ONCLICK=history.back() TYPE=button VALUE=Back NAME="btnBack">&nbsp;
	  </FORM>
	 </TD>
  </TR>
</TABLE>
</TD>
	<TD width="1" bgColor="#cccccc">
		<IMG id="imgBorder" src="<%= ApplicationPath %>/ui/img/blank.gif" border="0" style="WIDTH:1px;HEIGHT:1px" >
	</TD>
  </TR>
</TABLE>
		
<TABLE cellpadding="0" cellspacing="0" border="0" width="770" ID="Table6">
	<FORM id="ucFooter_frmFooter" name="ucFooter_frmFooter">
<INPUT type='hidden' name='LinkMe.SecondaryForm'  value='ucFooter_frmFooter' ID="Hidden4"> 
  <TBODY>

  <TR>
    <TD>
<IMG id="ucFooter_Image1" src="<%= ApplicationPath %>/ui/img/block_khaki.gif" border="0" style="WIDTH:770px;HEIGHT:1px" ><BR></TD></TR>
  <TR vAlign=middle>
    <TD align=center width="100%"><IMG height=10 alt="" src="<%= ApplicationPath %>/ui/img/blank.gif" 
      width=1 border=0> <BR>© Copyright LinkMe Pty Ltd&nbsp;&nbsp;  
      <BR><IMG height=10 alt="" src="<%= ApplicationPath %>/ui/img/blank.gif" width=1 
      border=0> <BR></TD>
      </TR>
      </FORM></TBODY>	
</TABLE>
	</DIV>
	</BODY>
</HTML>
