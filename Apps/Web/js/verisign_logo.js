<!--
dn="WWW.LINKME.COM.AU";
lang="en";
aff="eSignAustraliaLimitedGlobalServer";
tpt="transparent";
vrsn_style="WW";
splash_url="https://seal.verisign.com";
seal_url="https://seal.verisign.com";
u1=splash_url+"/splash?form_file=fdf/splash.fdf&dn="+dn+"&lang="+lang;
u2=seal_url+"/getseal?at=0&&sealid=1&dn="+dn+"&aff="+aff+"&lang="+lang;
u3=seal_url+"/getseal?at=1&&sealid=1&dn="+dn+"&aff="+aff+"&lang="+lang;
function vrsn_splash() {
tbar = "location=yes,status=yes,resizable=yes,scrollbars=yes,width=560,height=500";
sw = window.open(u1,'VRSN_Splash',tbar);
//sw.focus();
}
var MM_cVer = 5;
var plugin = (navigator.mimeTypes && navigator.mimeTypes["application/x-shockwave-flash"]) ? navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin : 0;
if ( plugin ) { var words = navigator.plugins["Shockwave Flash"].description.split(" ");
  for (var i = 0; i < words.length; ++i) { if (isNaN(parseInt(words[i]))) continue; var MM_pVer = words[i]; 
  }
  var MM_play = MM_pVer >= MM_cVer;
}
else if (navigator.userAgent && navigator.userAgent.indexOf("MSIE")>=0 && (navigator.appVersion.indexOf("Win") != -1)) {
	document.write('<SCR' + 'IPT LANGUAGE=VBScript\> \n');
	document.write('on error resume next \n');
	document.write('MM_play = ( IsObject(CreateObject("ShockwaveFlash.ShockwaveFlash." & MM_cVer)))\n');
	document.write('</SCR' + 'IPT\> \n');
}
if ( MM_play ) {
	document.write('<OBJECT classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"');
	document.write('  codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0" ');
	document.write(' ID="s_m" WIDTH="115" HEIGHT="82" ALIGN="">');
	document.write(' <PARAM NAME=movie VALUE="'+u3+'"> <PARAM NAME=loop VALUE=false> <PARAM NAME=menu VALUE=false> <PARAM NAME=quality VALUE=best> <PARAM NAME=wmode VALUE='+tpt+'> '); 
	document.write(' <EMBED src="'+u3+'" loop=false menu=false quality=best wmode='+tpt);
	document.write(' swLiveConnect=FALSE WIDTH="115" HEIGHT="82" NAME="s_m" ALIGN=""');
	document.write(' TYPE="application/x-shockwave-flash" PLUGINSPAGE="https://www.macromedia.com/go/getflashplayer">');
	document.write(' </EMBED>');
	document.write(' </OBJECT>');
} else {
v_ua=navigator.userAgent.toLowerCase();
v_oie=(v_ua.indexOf("msie")!=-1);
if(v_oie) v_oie=(v_ua.indexOf("msie 5")==-1 && v_ua.indexOf("msie 6")==-1);
function v_mact(e){
 if (document.addEventListener) {
  var s=(e.target.name=="seal");
   if (s) { vrsn_splash(); return false; }
 }else if(document.captureEvents) {
  var tgt=e.target.toString(); var s=(tgt.indexOf("splash")!=-1);
  if (s){ vrsn_splash(); return false; }
 }
 return true;
}
function v_mDown() {
if (event.button==1){
  if (v_oie) { return true; } else { vrsn_splash(); return false; }
} else if (event.button==2) { vrsn_splash(); return false; }
}
document.write("<a HREF=\""+u1+"\" tabindex=\"-1\" onmousedown=\"return v_mDown();\" target=\"VRSN_Splash\"><IMG NAME=\"seal\" BORDER=\"true\" SRC=\""+u2+"\" oncontextmenu=\"return false;\" alt=\"This Web site has chosen one or more VeriSign SSL Certificate or online payment solutions to improve the security of e-commerce and other confidential communication\"></A>");
if (document.addEventListener){ document.addEventListener('mouseup', v_mact, true); } 
else {
  if (document.layers){
    document.captureEvents(Event.MOUSEDOWN); document.onmousedown=v_mact;
  }
}
function v_resized(){
  if(pageWidth!=innerWidth || pageHeight!=innerHeight){
    self.history.go(0);
  }
}
if(document.layers){
  pageWidth=innerWidth; pageHeight=innerHeight; window.onresize=v_resized;
}
}
// -->
