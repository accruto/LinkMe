/* Override any s_code.js default settings here */
s.currencyCode="AUD"
/* Populate Site Specific Variables */
s.server=window.location.host;
s.prop1="SD"
s.prop2="Vert"
s.prop3="LinkMe"
s.prop4=""
s.eVar1=s.prop1
s.eVar2=s.prop2
s.eVar3=s.prop3

var navDiv = document.getElementById('nav');
if (navDiv)
{
    var x=navDiv.getElementsByTagName("li");
    for (var i=0;i<x.length;i++)
      { 
      var y = x[i].getElementsByTagName("a");
      if (y[0].className=="selected") {
      s.channel=y[0].innerHTML;
      s.eVar4=s.channel;
      }
    }
    var pname=document.title;
    pname=pname.substr(pname.indexOf("-")+2,pname.length) + " Page";

    var b=navDiv.getElementsByTagName("a");
    var a=document.getElementById('ucHeader_EmployerNav_lnkAccount');

    if (b[1].pathname.indexOf("unregistered")!=-1)
      {
      s.prop4="Non-Member";
      }
    else if (a.pathname.indexOf("registered/employers")!=-1)
      {
      s.prop4="Employer";
      }
    else if (a.pathname.indexOf("registered/networkers")!=-1)
      {
      s.prop4="Networker";
      }
    else
      {
      s.prop4="Not Defined";
      }
    s.eVar5=s.prop4;

    s.pageName=s.prop1+":"+s.prop2+":"+s.prop3+":"+s.channel+":"+s.prop4+":"+pname;
    s.hier1=s.prop1+"|"+s.prop2+"|"+s.prop3+"|"+s.channel+"|"+s.prop4;
    s.hier2=s.prop1+"|"+s.prop2+"|"+s.prop3+"|"+s.prop4+"|"+s.channel;
}