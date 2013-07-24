<%@ Import namespace="LinkMe.WebControls"%>
<%@ Control Language="c#" AutoEventWireup="False" Codebehind="MultiHtmlFileInput.ascx.cs" Inherits="LinkMe.Web.UI.Controls.Common.MultiHtmlFileInput" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="LinkMe.WebControls" Assembly="LinkMe.WebControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3b0227c645ed34d6" %>
<script type="text/javascript">
	var nr_files= 5;

	// Strings
	var L1='<a href="javascript: void(0);" onkeypress="morefiles()" onclick="morefiles()">Attach another file</a>&nbsp;';
	var L2='<a href="javascript: void(0);" onkeypress="morefiles()" onclick="morefiles()"><img src="<%= ApplicationPath %>/ui/img/paperclip.gif" border="0" />Attach a file</a>';
	var L3='<a href="javascript: void(0);" onkeypress="lessfiles()" onclick="lessfiles()">Remove</a>';

	// privates
	var _current = 0;
	var _fileatts = new Array();

	function morefiles()
	{
		if (_current < nr_files)
		{
		    var el = document.getElementById(_fileatts[_current]);
		    el.style.display = "";

		    _current++;
		    getFileInput(_current).disabled = '';

		    if (_current < nr_files ) 
		    {
			    document.getElementById("morefiles").innerHTML = L1;
			}
		    else
		    {
			    document.getElementById("morefiles").innerHTML = "";
			}

		    document.getElementById("lessfiles").innerHTML = L3;
		}
	}

	function lessfiles()
	{
		getFileInput(_current).disabled = 'disabled';

		var el = document.getElementById(_fileatts[_current-1]);
		el.style.display="none";

		_current--;

		if (_current==0)
		{
		    document.getElementById("morefiles").innerHTML=L2;
		    document.getElementById("lessfiles").innerHTML="";
		}
		else
		{
		    document.getElementById("morefiles").innerHTML=L1;
		}
	}
	
	function getFileInput(i)
	{
	    return document.getElementById('<%= ClientID %>_fileattachment' + (i - 1));
	}

	for (i = 0; i < nr_files; i++)
	{
	    _fileatts[i] = "fileatt" + i;
	}

	init = function()
	{
	    document.getElementById("morefiles").innerHTML = L2;
	}

	window.onload = init;
</script>
<style>
	#layout-attach-file input {
		margin-top:5px;
	}
</style>
<div id="layout-attach-file">
<span id="fileatt0" style="DISPLAY:none">
	<CC1:LinkMeHtmlInputFile class="fielddefault" id="fileattachment0" runat="server" size="60" />
	<br>
</span><span id="fileatt1" style="DISPLAY:none">
	<CC1:LinkMeHtmlInputFile class="fielddefault" id="fileattachment1" runat="server" size="60" />
	<br>
</span><span id="fileatt2" style="DISPLAY:none">
	<CC1:LinkMeHtmlInputFile class="fielddefault" id="fileattachment2" runat="server" size="60" />
	<br>
</span><span id="fileatt3" style="DISPLAY:none">
	<CC1:LinkMeHtmlInputFile class="fielddefault" id="fileattachment3" runat="server" size="60" />
	<br>
</span><span id="fileatt4" style="DISPLAY:none">
	<CC1:LinkMeHtmlInputFile class="fielddefault" id="fileattachment4" runat="server" size="60" />
	<br>
</span>
</div>
<noscript>
	<input type="file" name="fileattachment" size="35">
</noscript>
<span id="morefiles"></span>
<span id="lessfiles"></span>

