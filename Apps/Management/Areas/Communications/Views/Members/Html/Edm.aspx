<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Edm" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications.Models.Members"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="LMEP_OK" content="LMEP_OK" />
        <title>Edm</title>
    </head>
    <body bgcolor="#FFFFFF">
		<!-- Save for Web Slices (EDM600.psd) -->
		<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td align="center">
		<table id="Table_01" width="600" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td><img border="0" id="EDM600_01" src="<%= Html.ImageUrl("Edm600/EDM600_01.png")%>" width="600" height="165" alt="linkmejobs SPECIAL EDITION March 2012" /></td>
			</tr>
			<tr>
				<td><img border="0" id="EDM600_02" src="<%= Html.ImageUrl("Edm600/EDM600_02.png")%>" width="600" height="85" alt="Finding talent has never been so easy!" /></td>
			</tr>
			<tr>
				<td><img border="0" id="EDM600_03" src="<%= Html.ImageUrl("Edm600/EDM600_03.png")%>" width="600" height="8" alt="" /></td>
			</tr>
			<tr><td>
				<table id="main-4" width="600" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td><img border="0" id="EDM600_04" src="<%= Html.ImageUrl("Edm600/EDM600_04.png")%>" width="10" height="17" alt="" /></td>
						<td width="580" height="17" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 16px Arial; color:#333333; line-height:17px;">Hi <%= Model.Member.FirstName %>,</td>
						<td><img border="0" id="EDM600_06" src="<%= Html.ImageUrl("Edm600/EDM600_06.png")%>" width="10" height="17" alt="" /></td>
					</tr>
				</table>
			</td></tr>
			<tr>
				<td><img border="0" id="EDM600_07" src="<%= Html.ImageUrl("Edm600/EDM600_07.png")%>" width="600" height="15" alt="" /></td>
			</tr>
			<tr><td>
				<table id="main-6" width="600" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td><img border="0" id="EDM600_08" src="<%= Html.ImageUrl("Edm600/EDM600_08.png")%>" width="10" height="159" alt="" /></td>
						<td width="580" height="159" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;"><p>We hope you've had a great start to 2012.</p><p>As our LinkMe candidate database continues to grow, we thought this would be a good time to remind you that you too can use our database to source talented candidates for your own team.</p><p>With over 600,000 Australian job seekers available, we have now introduced an even easier way for employers to search our database - by using 'Candidate Connect', our new LinkMe iPhone app. Read more about LinkMe, the iPhone app and our Autumn Special below.</p></td>
						<td><img border="0" id="EDM600_10" src="<%= Html.ImageUrl("Edm600/EDM600_10.png")%>" width="10" height="159" alt="" /></td>
					</tr>
				</table>
			</td></tr>
			<tr><td>
				<table id="main-7" width="600" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table id="main-7-1" width="300" border="0" cellpadding="0" cellspacing="0">
								<tr><td><img border="0" id="EDM600_11" src="<%= Html.ImageUrl("Edm600/EDM600_11.png")%>" width="300" height="60" alt="ACCESS OVER 600,000 CANDIDATES" /></td></tr>
								<tr><td>
									<table id="main-7-1-2 width="300" border="0" cellpadding="0" cellspacing="0">
										<tr>
										    <td><a href="<%= GetLogoutUrl(Html, true, "~/employers") %>" target="_blank"><img border="0" id="EDM600_13" src="<%= Html.ImageUrl("Edm600/EDM600_13.png")%>" width="288" height="155" alt="" /></a></td>
										    <td><img border="0" id="EDM600_14" src="<%= Html.ImageUrl("Edm600/EDM600_14.png")%>" width="12" height="155" alt="" /></td>
										</tr>
									</table>
								</td></tr>
								<tr><td><img border="0" id="EDM600_27" src="<%= Html.ImageUrl("Edm600/EDM600_27.png")%>" width="300" height="25" alt="" /></td></tr>
								<tr><td>
								    <table id="main-7-1-4" width="300" border="0" cellpadding="0" cellspacing="0">
								        <tr>
								            <td><img border="0" id="EDM600_31" src="<%= Html.ImageUrl("Edm600/EDM600_31.png")%>" width="10" height="63" alt="" /></td>
								            <td width="278" height="63" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;">Search our database of over <span style="color:#F73451;">600,000</span> candidates and then contact your potential employees directly. It’s as easy as that!</td>
								            <td><img border="0" id="EDM600_33" src="<%= Html.ImageUrl("Edm600/EDM600_33.png")%>" width="12" height="63" alt="" /></td>
								        </tr>
								    </table>
								</td></tr>
								<tr><td>
								    <table id="main-7-1-5" width="300" border="0" cellpadding="0" cellspacing="0">
								        <tr>
								            <td>
								                <table id="main-7-1-5-1" width="147" border="0" cellpadding="0" cellspacing="0">
								                    <tr><td><img border="0" id="EDM600_34" src="<%= Html.ImageUrl("Edm600/EDM600_34.png")%>" width="147" height="9" alt="" /></td></tr>
								                    <tr><td>
								                        <table id="main-7-1-5-1-2" width="147" border="0" cellpadding="0" cellspacing="0">
								                            <tr>
								                                <td><img border="0" id="EDM600_40" src="<%= Html.ImageUrl("Edm600/EDM600_40.png")%>" width="10" height="39" alt="" /></td>
								                                <td width="137" height="39" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;">Search now to find your next team member</td>
								                            </tr>
								                        </table>
								                    </td></tr>
								                </table>
								            </td>
								            <td><a href="<%= GetLogoutUrl(Html, true, "~/employers") %>" target="_blank"><img border="0" id="EDM600_35" src="<%= Html.ImageUrl("Edm600/EDM600_35.png")%>" width="133" height="48" alt="" /></a></td>
								            <td><img border="0" id="EDM600_36" src="<%= Html.ImageUrl("Edm600/EDM600_36.png")%>" width="20" height="48" alt="" /></td>
								        </tr>
								    </table>
								</td></tr>
								<tr><td>
									<table width="300" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td><img border="0" id="EDM600_34" src="<%= Html.ImageUrl("Edm600/EDM600_34.png")%>" width="147" height="70" alt="" /></td>
											<td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="133" height="70" alt="" /></td>
											<td><img border="0" id="EDM600_36" src="<%= Html.ImageUrl("Edm600/EDM600_36.png")%>" width="20" height="70" alt="" /></td>
										</tr>
									</table>
								</td></tr>
								<tr><td><img border="0" id="EDM600_44" src="<%= Html.ImageUrl("Edm600/EDM600_44.png")%>" width="300" height="25" alt="" /></td></tr>
							</table>
						</td>
						<td>
						    <table id="main-7-2" width="300" border="0" cellpadding="0" cellspacing="0">
						        <tr><td><img border="0" id="EDM600_12" src="<%= Html.ImageUrl("Edm600/EDM600_12.png")%>" width="300" height="67" alt="AUTUMN SPECIAL: 20% OFF" /></td></tr>
						        <tr><td>
						            <table id="main-7-2-2" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_15" src="<%= Html.ImageUrl("Edm600/EDM600_15.png")%>" width="2" height="22" alt="" /></td>
						                    <td width="184" height="22" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 14px Arial; color:#333333; line-height:14px;">Take advantage of our</td>
						                    <td><img border="0" id="EDM600_17" src="<%= Html.ImageUrl("Edm600/EDM600_17.png")%>" width="114" height="22" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
						            <table id="main-7-2-3" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_18" src="<%= Html.ImageUrl("Edm600/EDM600_18.png")%>" width="2" height="19" alt="" /></td>
						                    <td width="198" height="19" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 14px Arial; color:#333333; line-height:14px;">exclusive autumn special:</td>
						                    <td><img border="0" id="EDM600_20" src="<%= Html.ImageUrl("Edm600/EDM600_20.png")%>" width="100" height="19" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
						            <table id="main-7-2-4" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_21" src="<%= Html.ImageUrl("Edm600/EDM600_21.png")%>" width="2" height="33" alt="" /></td>
						                    <td width="296" height="33" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 14px Arial; color:#333333; line-height:18px;">Contact 5 candidates for only $144.<span style="color:#F73451;font:Bold 12px Arial;">Save $36</span></td>
						                    <td><img border="0" id="EDM600_23" src="<%= Html.ImageUrl("Edm600/EDM600_23.png")%>" width="2" height="33" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
						            <table id="main-7-2-5" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_24" src="<%= Html.ImageUrl("Edm600/EDM600_24.png")%>" width="6" height="90" alt="" /></td>
						                    <td width="292" height="90" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;">That’s cheaper than posting an ad on an online job board. You don’t have to wade through hundreds of resumes, and you get to speak to your potential employees immediately - you don’t have to wait!</td>
						                    <td><img border="0" id="EDM600_23" src="<%= Html.ImageUrl("Edm600/EDM600_23.png")%>" width="2" height="90" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
						            <table id="main-7-2-6" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="6" height="57" alt="" /></td>
						                    <td width="292" height="57" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;"><b>Searching is free</b> – you only pay if you find a potential employee who you would liketo contact.</td>
						                    <td><img border="0" id="EDM600_30" src="<%= Html.ImageUrl("Edm600/EDM600_30.png")%>" width="2" height="57" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
						            <table id="main-7-2-6" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="6" height="50" alt="" /></td>
						                    <td width="286" height="50" valign="center" align="center" bgcolor="#FCF4CA" style="font:12px Arial; color:#333333; line-height:18px;">Use coupon code "<b>LMASD01</b>" in the<br/>Purchase flow to redeem this offer</td>
						                    <td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="6" height="50" alt="" /></td>
						                    <td><img border="0" id="EDM600_30" src="<%= Html.ImageUrl("Edm600/EDM600_30.png")%>" width="2" height="50" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
						            <table id="main-7-2-6" width="300" border="0" cellpadding="0" cellspacing="0">
						                <tr>
						                    <td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="298" height="25" alt="" /></td>
						                    <td><img border="0" id="EDM600_30" src="<%= Html.ImageUrl("Edm600/EDM600_30.png")%>" width="2" height="25" alt="" /></td>
						                </tr>
						            </table>
						        </td></tr>
						        <tr><td>
								    <table id="main-7-1-7" width="300" border="0" cellpadding="0" cellspacing="0">
								        <tr>
								            <td>
								                <table id="main-7-1-7-1" width="138" border="0" cellpadding="0" cellspacing="0">
								                    <tr>
														<td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="23" height="58" alt="" /></td>
														<td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/Share.png")%>" width="91" height="58" alt="" /></td>
														<td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="24" height="58" alt="" /></td>
													</tr>
								                </table>
								            </td>
								            <td>
								                <table id="main-7-1-7-1" width="138" border="0" cellpadding="0" cellspacing="0">
								                    <tr><td><img border="0" id="EDM600_28" src="<%= Html.ImageUrl("Edm600/EDM600_28.png")%>" width="144" height="10" alt="" /></td></tr>
								                    <tr><td><a href="<%= GetLogoutUrl(Html, true, "~/employers/products/choose", "couponCode", "LMASD01") %>" target="_blank"><img border="0" id="EDM600_38" src="<%= Html.ImageUrl("Edm600/EDM600_38.png")%>" width="144" height="48" alt="" /></a></td></tr>
								                </table>											
											</td>
								            <td><img border="0" id="EDM600_39" src="<%= Html.ImageUrl("Edm600/EDM600_39.png")%>" width="18" height="58" alt="" /></td>
								        </tr>
								    </table>
						        </td></tr>
						        <tr><td><img border="0" id="EDM600_45" src="<%= Html.ImageUrl("Edm600/EDM600_45.png")%>" width="300" height="25" alt="" /></td></tr>
						    </table>
						</td>
					</tr>
				</table>
			</td></tr>
			<tr><td>
			    <table id="main-8" width="600" border="0" cellpadding="0" cellspacing="0">
			        <tr>
			            <td>
			                <table id="main-8-1" width="300" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><img border="0" id="EDM600_46" src="<%= Html.ImageUrl("Edm600/EDM600_46.png")%>" width="300" height="67" alt="ALL INDUSTRIES ACROSS AUSTRALIA" /></td></tr>
			                    <tr><td>
			                        <table id="main-8-1-2" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_48" src="<%= Html.ImageUrl("Edm600/EDM600_48.png")%>" width="11" height="87" alt="" /></td>
			                                <td width="270" height="87" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;">We have thousands of candidates immediately available in professions ranging from retail staff to senior professionals, all across Australia.</td>
			                                <td><img border="0" id="EDM600_50" src="<%= Html.ImageUrl("Edm600/EDM600_50.png")%>" width="19" height="87" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td>
			                        <table id="main-8-1-3" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_54" src="<%= Html.ImageUrl("Edm600/EDM600_54.png")%>" width="77" height="258" alt="" /></td>
			                                <td>
			                                    <table id="main-8-1-3-2" width="204" border="0" cellpadding="0" cellspacing="0">
			                                        <tr><td><img border="0" id="EDM600_55" src="<%= Html.ImageUrl("Edm600/EDM600_55.png")%>" width="204" height="17" alt="" /></td></tr>
			                                        <tr><td width="204" height="70" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Over 32,000 Engineers</td></tr>
			                                        <tr><td width="204" height="70" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Over 35,000 Receptionists</td></tr>
			                                        <tr><td width="204" height="101" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Over 63,000 Sales managers</td></tr>
			                                    </table>
			                                </td>
			                                <td><img border="0" id="EDM600_56" src="<%= Html.ImageUrl("Edm600/EDM600_56.png")%>" width="19" height="258" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                </table>
			            </td>
			            <td>
			                <table id="main-8-2" width="300" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><img border="0" id="EDM600_47" src="<%= Html.ImageUrl("Edm600/EDM600_47.png")%>" width="300" height="67" alt="HOW DOES LINKME WORK?" /></td></tr>
			                    <tr><td>
			                        <table id="main-8-2-2" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_51" src="<%= Html.ImageUrl("Edm600/EDM600_51.png")%>" width="7" height="87" alt="" /></td>
			                                <td width="291" height="87" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;"><b>LinkMe</b> is a revolutionary new way to match job seekers with employers who are looking for staff. Our job seekers are waiting for your call. To see how the site works, watch this short video.</td>
			                                <td><img border="0" id="EDM600_53" src="<%= Html.ImageUrl("Edm600/EDM600_53.png")%>" width="2" height="87" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td>
			                        <table id="main-8-2-3" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_57" src="<%= Html.ImageUrl("Edm600/EDM600_57.png")%>" width="7" height="249" alt="" /></td>
			                                <td width="284" height="249"><video controls="controls" width="284" height="249" class="video-stream" poster="http://i.ytimg.com/vi/7H8RrYPDpJo/hqdefault.jpg" x-webkit-airplay="allow" data-youtube-id="N9oxmRT2YWw" src="http://o-o.preferred.aapt-syd1.v22.lscache4.c.youtube.com/videoplayback?sparams=id%2Cexpire%2Cip%2Cipbits%2Citag%2Csource%2Cratebypass%2Ccp&amp;fexp=905228%2C913601%2C911201&amp;itag=43&amp;ip=202.0.0.0&amp;signature=9FBE2F9E6D32D604A701667C44005A16662AA5DE.5F1553A8636379191CD228F97C6B7EF6742DB8CD&amp;sver=3&amp;ratebypass=yes&amp;source=youtube&amp;expire=1330410999&amp;key=yt1&amp;ipbits=8&amp;cp=U0hSRVRMTl9OU0NOMl9MRlJKOlQtZERwTTEyNGlS&amp;id=ec7f11ad83c3a49a"><a href="http://www.youtube.com/watch?v=7H8RrYPDpJo" target="_blank"><img border="0" src="http://i.ytimg.com/vi/7H8RrYPDpJo/hqdefault.jpg" alt="What is LinkMe (Watch this video in a new window)" width="284" height="249" /></a></video></td>
		                                    <td><img border="0" id="EDM600_59" src="<%= Html.ImageUrl("Edm600/EDM600_59.png")%>" width="9" height="249" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td><img border="0" id="EDM600_63" src="<%= Html.ImageUrl("Edm600/EDM600_63.png")%>" width="300" height="9" alt="" /></td></tr>
			                </table>
			            </td>
			        </tr>
			    </table>
			</td></tr>
			<tr><td>
			    <table id="main-9" width="600" border="0" cellpadding="0" cellspacing="0">
			        <tr>
			            <td>
			                <table id="main-9-1" width="300" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><img border="0" id="EDM600_64" src="<%= Html.ImageUrl("Edm600/EDM600_64.png")%>" width="300" height="65" alt="SIGN UP AS AN EMPLOYER TODAY" /></td></tr>
			                    <tr><td>
			                        <table id="main-9-1-2" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_67" src="<%= Html.ImageUrl("Edm600/EDM600_67.png")%>" width="13" height="57" alt="" /></td>
			                                <td width="284" height="57" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Create an employer account* and search, shortlist and contact candidates for your role.</td>
			                                <td><img border="0" id="EDM600_69" src="<%= Html.ImageUrl("Edm600/EDM600_69.png")%>" width="3" height="57" alt="" /></td>
			                            </tr>
			                        </table>
                                </td></tr>
			                    <tr><td>
			                        <table id="main-9-1-3" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_70" src="<%= Html.ImageUrl("Edm600/EDM600_70.png")%>" width="78" height="45" alt="" /></td>
			                                <td><a href="<%= GetLogoutUrl(Html, true, "~/employers/join") %>" target="_blank"><img border="0" id="EDM600_71" src="<%= Html.ImageUrl("Edm600/EDM600_71.png")%>" width="142" height="45" alt="" /></a></td>
			                                <td><img border="0" id="EDM600_72" src="<%= Html.ImageUrl("Edm600/EDM600_72.png")%>" width="80" height="45" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td><img border="0" id="EDM600_73" src="<%= Html.ImageUrl("Edm600/EDM600_73.png")%>" width="300" height="100" alt="" /></td></tr>
			                    <tr><td>
			                        <table id="main-9-1-5" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_74" src="<%= Html.ImageUrl("Edm600/EDM600_74.png")%>" width="13" height="88" alt="" /></td>
			                                <td width="70" height="88" valign="top" align="center" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Search for candidates</td>
			                                <td><img border="0" id="EDM600_51" src="<%= Html.ImageUrl("Edm600/EDM600_51.png")%>" width="22" height="88" alt="" /></td>
			                                <td width="70" height="88" valign="top" align="center" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Contact candidates instantly</td>
			                                <td><img border="0" id="EDM600_51" src="<%= Html.ImageUrl("Edm600/EDM600_51.png")%>" width="30" height="88" alt="" /></td>
			                                <td width="70" height="88" valign="top" align="center" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">Place/Employ candidates</td>
			                                <td><img border="0" id="EDM600_51" src="<%= Html.ImageUrl("Edm600/EDM600_51.png")%>" width="22" height="88" alt="" /></td>
			                                <td><img border="0" id="EDM600_78" src="<%= Html.ImageUrl("Edm600/EDM600_78.png")%>" width="3" height="88" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td>
			                        <table id="main-9-1-5" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_79" src="<%= Html.ImageUrl("Edm600/EDM600_79.png")%>" width="13" height="43" alt="" /></td>
			                                <td width="284" height="43" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:12px;">*You will not be able to use your LinkMe candidate login</td>
			                                <td><img border="0" id="EDM600_81" src="<%= Html.ImageUrl("Edm600/EDM600_81.png")%>" width="3" height="43" alt="" /></td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td><img border="0" id="EDM600_89" src="<%= Html.ImageUrl("Edm600/EDM600_89.png")%>" width="300" height="84" alt="" /></td></tr>
			                </table>
			            </td>
			            <td>
			                <table id="main-9-2" width="300" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><img border="0" id="EDM600_65" src="<%= Html.ImageUrl("Edm600/EDM600_65.png")%>" width="300" height="64" alt="Talent at your fingertips" /></td></tr>
			                    <tr><td><img border="0" id="EDM600_66" src="<%= Html.ImageUrl("Edm600/EDM600_66.png")%>" width="300" height="291" alt="Candidate connect powered by linkmejobs" /></td></tr>
			                    <tr><td>
			                        <table id="main-9-2-3" width="300" border="0" cellpadding="0" cellspacing="0">
			                            <tr>
			                                <td><img border="0" id="EDM600_82" src="<%= Html.ImageUrl("Edm600/EDM600_82.png")%>" width="10" height="60" alt="" /></td>
			                                <td><a target="_blank" href="http://itunes.apple.com/us/app/candidate-connect/id490840013?ls=1&mt=8"><img border="0" id="EDM600_83" src="<%= Html.ImageUrl("Edm600/EDM600_83.png")%>" width="171" height="60" alt="Available on the App Store" /></a></td>
			                                <td>
			                                    <table id="main-9-2-3-3" width="119" border="0" cellpadding="0" cellspacing="0">
			                                        <tr><td><img border="0" id="EDM600_84" src="<%= Html.ImageUrl("Edm600/EDM600_84.png")%>" width="119" height="26" alt="" /></td></tr>
			                                        <tr><td>
			                                            <table id="main-9-2-3-3-2" width="119" border="0" cellpadding="0" cellspacing="0">
			                                                <tr>
			                                                    <td><img border="0" id="EDM600_85" src="<%= Html.ImageUrl("Edm600/EDM600_85.png")%>" width="74" height="34" alt="" /></td>
			                                                    <td>
			                                                        <table id="main-0-2-3-3-2-2" width="29" border="0" cellpadding="0" cellspacing="0">
			                                                            <tr><td><a target="_blank" href="<%= GetLogoutUrl(Html, false, "~/employers/candidateconnect") %>"><img border="0" id="EDM600_86" src="<%= Html.ImageUrl("Edm600/EDM600_86.png")%>" width="29" height="13" alt="find out more" /></a></td></tr>
			                                                            <tr><td><img border="0" id="EDM600_88" src="<%= Html.ImageUrl("Edm600/EDM600_88.png")%>" width="29" height="21" alt="" /></td></tr>
			                                                        </table>
			                                                    </td>
			                                                    <td><img border="0" id="EDM600_87" src="<%= Html.ImageUrl("Edm600/EDM600_87.png")%>" width="16" height="34" alt="" /></td>
			                                                </tr>
			                                            </table>
			                                        </td></tr>
			                                    </table>
			                                </td>
			                            </tr>
			                        </table>
			                    </td></tr>
			                    <tr><td><img border="0" id="EDM600_90" src="<%= Html.ImageUrl("Edm600/EDM600_90.png")%>" width="300" height="67" alt="" /></td></tr>
			                </table>
			            </td>
			        </tr>
			    </table>
			</td></tr>
			<tr><td><img border="0" id="EDM600_91" src="<%= Html.ImageUrl("Edm600/EDM600_91.png")%>" width="600" height="29" alt="" /></td></tr>
			<tr><td>
			    <table id="main-11" width="600" border="0" cellpadding="0" cellspacing="0">
			        <tr>
			            <td><img border="0" id="EDM600_92" src="<%= Html.ImageUrl("Edm600/EDM600_92.png")%>" width="427" height="15" alt="" /></td>
			            <td>
			                <table id="main-11-2" width="83" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><a target="_blank" href="<%= Html.HomeUrl() %>"><img border="0" id="EDM600_93" src="<%= Html.ImageUrl("Edm600/EDM600_93.png")%>" width="83" height="12" alt="LinkMe.com.au" /></a></td></tr>
			                    <tr><td><img border="0" id="EDM600_95" src="<%= Html.ImageUrl("Edm600/EDM600_95.png")%>" width="83" height="3" alt="" /></td></tr>
			                </table>
			            </td>
			            <td><img border="0" id="EDM600_94" src="<%= Html.ImageUrl("Edm600/EDM600_94.png")%>" width="90" height="15" alt="" /></td>
			        </tr>
			    </table>
			</td></tr>
			<tr><td>
			    <table id="main-12" width="600" border="0" cellpadding="0" cellspacing="0">
			        <tr>
			            <td><img border="0" id="EDM600_96" src="<%= Html.ImageUrl("Edm600/EDM600_96.png")%>" width="145" height="52" alt="" /></td>
			            <td>
			                <table id="main-12-2" width="92" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><a target="_blank" href="<%= GetLogoutLoginUrl(Html, "~/employers/login", "~/employers/settings") %>"><img border="0" id="EDM600_97" src="<%= Html.ImageUrl("Edm600/EDM600_97.png")%>" width="92" height="12" alt="edit your settings" /></a></td></tr>
			                    <tr><td><img border="0" id="EDM600_101" src="<%= Html.ImageUrl("Edm600/EDM600_101.png")%>" width="92" height="40" alt="" /></td></tr>
			                </table>
			            </td>
			            <td><img border="0" id="EDM600_98" src="<%= Html.ImageUrl("Edm600/EDM600_98.png")%>" width="209" height="52" alt="" /></td>
			            <td>
			                <table id="main-12-4" width="66" border="0" cellpadding="0" cellspacing="0">
			                    <tr><td><a target="_blank" href="<%= GetLogoutUrl(Html, false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category) %>"><img border="0" id="EDM600_99" src="<%= Html.ImageUrl("Edm600/EDM600_99.png")%>" width="66" height="12" alt="unsubscribe" /></a></td></tr>
			                    <tr><td><img border="0" id="EDM600_102" src="<%= Html.ImageUrl("Edm600/EDM600_102.png")%>" width="66" height="40" alt="" /></td></tr>
			                </table>
			            </td>
			            <td><img border="0" id="EDM600_100" src="<%= Html.ImageUrl("Edm600/EDM600_100.png")%>" width="88" height="52" alt="" /></td>			            			            
			        </tr>
			    </table>
			</td></tr>
		</table>
		</td></tr></table>
		<!-- End Save for Web Slices -->
    </body>
</html>
