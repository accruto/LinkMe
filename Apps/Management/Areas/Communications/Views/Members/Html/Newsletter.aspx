<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Newsletter" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="-webkit-text-size-adjust:none;">
	<head id="Head1" runat="server">
        <meta name="LMEP_OK" content="LMEP_OK" />
		<meta name="viewport" content="width = 600, user-scalable = no, initial-scale = 1.0" />
		<title>LinkMe <%= DateTime.Now.ToString("MMMM") %> Newsletter</title>
	</head>
	<body bgcolor="#FFFFFF" style="-webkit-text-size-adjust:none;">
		<!-- Save for Web Slices (Reactivation600.psd) -->
		<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td align="center">
		<table id="Table_01" width="600" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td style="line-height:0px"><img border="0" id="Reactivation600_01" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_01.png")%>" width="600" height="182" alt="linkmejobs NEWS <%= DateTime.Now.ToString("MMMM yyyy") %>" /></td>
			</tr>
			<tr>
				<td style="line-height:0px">
					<table id="main-0" width="600" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td style="line-height:0px"><img border="0" id="Reactivation600_02-1" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_02.png")%>" width="100" height="24" alt="" /></td>
							<td width="400" height="24" valign="middle" align="center" bgcolor="#F6D517" style="font:17px Arial; color:#204363; line-height:24px;">More than <b><%= (Model.TotalJobAds / 1000) + ",000" %></b> jobs added this month</td>
							<td style="line-height:0px"><img border="0" id="Reactivation600_02-2" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_04.png")%>" width="100" height="24" alt="" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_02-3" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_05.png")%>" width="600" height="41" alt="" /></td></tr>
			<tr>
				<td style="line-height:0px">
					<table id="main" width="600" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td style="line-height:0px">
								<table id="main-1" width="300" border="0" cellpadding="0" cellspacing="0">
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_06.png")%>" width="21" height="315" alt="" /></td>
												<td style="line-height:0px">
													<table width="270" border="0" cellpadding="0" cellspacing="0">
														<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="270" height="9" alt="" /></td></tr>
														<tr><td width="270" height="32" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 16px Arial;color:#333333;line-height:20px">Hi <%= Model.Member.FirstName %></td></tr>
														<tr><td width="270" height="50" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:20px">Welcome to the September edition of LinkMe.com.au’s newsletter.</td></tr>
														<tr><td width="270" height="130" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:20px">This month we consider how <a href="<%= Html.TinyUrl(false, "~/members/resources/article/Career-management-Manage-your-career-Social-Media-Your-Online-Footprint/5844bc0b-e8e8-4f2d-bebd-8d455dd3d077") %>" target="_blank">your online profile can affect your chances of finding a job</a>. Paul Jury, MD Recruitment, Australia & New Zealand, at <a href="<%= Html.TinyUrl(false, "http://www.talent2.com") %>" target="_blank">Talent2</a> gives us his perspective and has some great tips on how to avoid the pitfalls of social media.</td></tr>
														<tr><td width="270" height="94" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:20px">As a reminder, your LinkMe profile is your passport to a new career. We encourage you to review and <a href="<%= Html.TinyUrl(true, "~/members/profile") %>" target="_blank">update your profile</a> on a regular basis.</td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="7" height="315" alt="" /></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_08.png")%>" width="2" height="315" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_14.png")%>" width="300" height="68" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px">
													<table width="94" border="0" cellpadding="0" cellspacing="0">
														<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_23.png")%>" width="94" height="41" alt="" /></td></tr>
														<tr><td style="line-height:0px">
															<table width="94" border="0" cellpadding="0" cellspacing="0">
																<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_26.png")%>" width="93" height="18" alt="" /></td><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="1" height="18" alt="" /></td></tr>
															</table>
														</td></tr>
														<tr><td style="line-height:0px">
															<table width="94" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_29.png")%>" width="21" height="11" alt="" /></td>
																	<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="73" height="11" alt="" /></td>
																</tr>
															</table>
														</td></tr>
													</table>
												</td>
												<td width="204" height="70" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 14px Arial;color:#383737;line-height:20px">Does your resume Pass or Fail?</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_25.png")%>" width="2" height="70" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px">
													<table width="94" border="0" cellpadding="0" cellspacing="0">
														<tr>
															<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_29.png")%>" width="21" height="18" alt="" /></td>
															<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="73" height="18" alt="" /></td>
														</tr>
													</table>
												</td>
												<td width="204" height="18" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:18px">Does your professional resume</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_28.png")%>" width="2" height="18" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_29.png")%>" width="21" height="63" alt="" /></td>
												<td width="277" height="63" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:18px">stand out from the competition? Will your resume meet the expectations of employers or hiring managers or will it end up in the... <a href="<%= Html.TinyUrl(false, "~/members/resources/answeredQuestion/Resume-writing-Resume-writing-tips-Does-your-resume-Pass-or-Fail/c531330b-f8ed-4998-bd1a-af59bffcd7e4") %>" target="_blank">Read more</a></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_31.png")%>" width="2" height="63" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_32.png")%>" width="21" height="15" alt="" /></td>
												<td width="277" height="15" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 12px Arial;color:#333333;line-height:15px">Need help with your career?</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_34.png")%>" width="2" height="15" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_35.png")%>" width="21" height="31" alt="" /></td>
												<td width="277" height="31" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:15px"><a href="<%= Html.TinyUrl(false, "~/members/resources") %>" target="_blank">Ask us and receive expert advice</a></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_37.png")%>" width="2" height="31" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_38.png")%>" width="300" height="66" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_39.png")%>" width="94" height="59" alt="" /></td>
												<td width="270" height="59" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 12px Arial;color:#333333;line-height:15px">Social media: Your online footprint</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="34" height="59" alt="" /></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_41.png")%>" width="2" height="59" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_46.png")%>" width="24" height="32" alt="" /></td>
												<td width="274" height="32" valign="top" align="left" bgcolor="#FFFFFF" style="font:Italic 10px Arial;color:#333333;line-height:12px">by Paul Jury, MD Recruitment, Australia & New Zealand, at <a href="<%= Html.TinyUrl(false, "http://www.talent2.com") %>" target="_blank">Talent2</a></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_45.png")%>" width="2" height="32" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_46.png")%>" width="24" height="66" alt="" /></td>
												<td width="274" height="66" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:15px">We’ve all done it… made a controversial 'wall' comment, written a cheeky blog post or suffered as friends tag questionable photos of a big night out. It's a bit of harmless fun... <a href="<%= Html.TinyUrl(false, "~/members/resources/article/Career-management-Manage-your-career-Social-Media-Your-Online-Footprint/5844bc0b-e8e8-4f2d-bebd-8d455dd3d077") %>" target="_blank">Read more</a></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_48.png")%>" width="2" height="66" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_53.png")%>" width="300" height="69" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px">
													<table width="93" border="0" cellpadding="0" cellspacing="0">
														<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_57.png")%>" width="93" height="26" alt="" /></td></tr>
														<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_60.png")%>" width="93" height="36" alt="" /></td></tr>
													</table>
												</td>
												<td style="line-height:0px">
													<table width="205" border="0" cellpadding="0" cellspacing="0">
														<tr><td width="205" height="47" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 14px Arial;color:#333333;line-height:15px">LinkMe launches mobile site</td></tr>
														<tr><td width="205" height="15" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:15px">You can now search and apply for</td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_59.png")%>" width="2" height="62" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_63.png")%>" width="21" height="198" alt="" /></td>
												<td style="line-height:0px">
													<table width="267" border="0" cellpadding="0" cellspacing="0">
														<tr><td width="267" height="100" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial;color:#333333;line-height:15px">jobs on LinkMe.com.au direct from your mobile device. Visit <a href="<%= Html.TinyUrl(false, "~/") %>" target="_blank">www.linkme.com.au</a> today using your smart phone and see how easy it is.</td></tr>
														<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="267" height="98" alt="" /></td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_07.png")%>" width="10" height="198" alt="" /></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_65.png")%>" width="2" height="198" alt="" /></td>
											</tr>
										</table>
									</td></tr>									
								</table>
							</td>
							<td style="line-height:0px">
								<table id="main-2" width="300" border="0" cellpadding="0" cellspacing="0">
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_04" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04.png")%>" width="300" height="55" alt="YOUR PROFILE SUMMARY" /></td></tr>
									<tr><td style="line-height:0px">
										<table id="main-2-2" width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px">
													<table id="main-2-2-1" width="282" border="0" cellpadding="0" cellspacing="0">
														<tr><td style="line-height:0px">
															<table id="main-2-2-1-1" width="282" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_04009" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-09.png")%>" width="23" height="98" alt="" /></td>
																	<td style="line-height:0px">
																		<table id="main-2-2-1-1-2" width="12" border="0" cellpadding="0" cellspacing="0">
																			<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-22.png")%>" width="13" height="5" alt="" style="display:block;" /></td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_10" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_10-" + Model.Candidate.Status + ".png")%>" width="13" height="13" alt="" /></td></tr>
																			<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-22.png")%>" width="13" height="5" alt="" style="display:block;" /></td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_04014" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-14.png")%>" width="13" height="75" alt="" /></td></tr>
																		</table>
																	</td>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_04011" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-11.png")%>" width="9" height="98" alt="" /></td>
																	<td style="line-height:0px">
																		<table id="main-2-2-1-1-4" width="237" border="0" cellpadding="0" cellspacing="0">
																			<tr><td width="237" height="57" valign="top" align="left" bgcolor="#FBF1DC" style="font:12px Arial; color:#333333; line-height:19px;"><b>Work status:</b> <%= Model.Candidate.Status.GetDisplayText() %><br /><b>Desired job:</b> <span title="<%= Model.Candidate.DesiredJobTitle %>"><%= (!string.IsNullOrEmpty(Model.Candidate.DesiredJobTitle) && Model.Candidate.DesiredJobTitle.Length > 25) ? (Model.Candidate.DesiredJobTitle.Substring(0, 25) + "...") : Model.Candidate.DesiredJobTitle %></span><br /><b>Primary phone:</b> <%= Model.Member.GetPrimaryPhoneNumber() != null ? Model.Member.GetPrimaryPhoneNumber().Number : "" %></td></tr>
																			<tr><td width="237" height="41" valign="top" align="left" bgcolor="#FBF1DC" style="font:12px Arial; color:#333333; line-height:19px;">Your profile has been viewed <b><%= Model.TotalViewed %></b> times by employers and recruiters this month</td></tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td></tr>
														<tr><td style="line-height:0px">
															<table id="main-2-2-1-2" width="282" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_04019" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_13-16.png")%>" width="13" height="147" alt="" /></td>
																	<td style="line-height:0px">
																		<table id="main-2-2-1-2-2" width="269" border="0" cellpadding="0" cellspacing="0">
																			<tr><td style="line-height:0px">
																				<table id="main-2-2-1-2-2-1" width="269" border="0" cellpadding="0" cellspacing="0">
																					<tr>
																						<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-22.png")%>" width="32" height="14" alt="" /></td>
																						<td width="135" height="14" align="left" bgcolor="#6C6962" style="line-height:0px;"><img border="0" src="<%= Html.ImageUrl("Reactivation600/ProgressBarFg.png")%>" width="<%= 135 * Model.ProfilePercentComplete / 100 %>" height="14" alt="" /></td>
																						<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-22.png")%>" width="3" height="14" alt="" /></td>
																						<td width="32" height="14" bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#555960; line-height:10px;" align="center"><%= Model.ProfilePercentComplete %>%</td>
																						<td style="line-height:0px"><img border="0" id="Reactivation600_04022" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-22.png")%>" width="67" height="14" alt="" /></td>
																					</tr>
																				</table>
																			</td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_24" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_24.png")%>" width="269" height="8" alt="" /></td></tr>
																			<tr><td style="line-height:0px">
																				<table id="main-2-2-1-2-4" width="269" border="0" cellpadding="0" cellspacing="0">
																					<tr>
																						<td style="line-height:0px"><img border="0" id="Reactivation600_04025" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_14-16.png")%>" width="32" height="125" alt="" /></td>
																						<td style="line-height:0px">
																							<table id="main-2-2-1-2-4-2" width="232" border="0" cellpadding="0" cellspacing="0">
																								<tr><td width="237" height="16" bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#<%= Model.ProfilePercentComplete == 100 ? "0C6E18" : "D42042"%>; line-height:10px;">Your profile is <%= Model.ProfilePercentComplete == 100 ? "" : "in"%>complete.</td></tr>
																								<% if (Model.ProfilePercentComplete == 100) { %>
																								<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_18.png")%>" width="237" height="7" alt="" /></td></tr>
																								<tr><td width="237" height="16"  bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#373737; line-height:10px;">Changed your contact details recently?</td></tr>
																								<tr><td width="237" height="16"  bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#373737; line-height:10px;"> Started a new job or completed a course?</td></tr>
																								<tr><td width="237" height="45"  bgcolor="#FBF1DC" valign="top" align="left" style="font:11px Arial; color:#373737; line-height:14px;"><a href="<%= Html.TinyUrl(true, "~/members/profile") %>" target="_blank">Update your profile</a> to ensure employers will contact you directly</td></tr>
																								<% } else { %>
																								<tr><td width="237" height="84"  bgcolor="#FBF1DC" valign="top" align="left" style="font:12px Arial; color:#373737; line-height:14px;">Please <a href="<%= Html.TinyUrl(true, "~/members/profile") %>" target="_blank">review your profile</a> to increase your chances of being found by employers.</td></tr>
																								<% } %>
																								<tr><td style="line-height:0px"><img border="0" id="Reactivation600_04033" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_18-21.png")%>" width="237" height="25" alt="" /></td></tr>
																							</table>
																						</td>
																					</tr>
																				</table>
																			</td></tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" id="Reactivation600_04013" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_13.png")%>" width="18" height="245" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_34" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_34.png")%>" width="300" height="63" alt="YOUR PERSONALISED JOBS" /></td></tr>
									<% var index = 0;
									foreach (var j in Model.SuggestedJobs) {
								        index++;
                                        if (index > 3) break;
										var title = j.Title.Trim();
									if (j.Features.IsFlagSet(JobAdFeatures.Highlight)) { %>
									<tr><td style="line-height:0px">
										<table id="main-2-7-2" width="300" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_40" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40.png")%>" width="300" height="21" alt="" /></td></tr>
											<tr><td style="line-height:0px">
												<table id="main-2-7-2-2" width="300" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td style="line-height:0px"><img border="0" id="Reactivation600_40040" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40-40.png")%>" width="18" height="59" alt="" /></td>
														<td style="line-height:0px">
															<table id="main-2-7-2-2-2" width="42" border="0" cellpadding="0" cellspacing="0">
																<tr><td style="line-height:0px"><img border="0" id="Reactivation600_41" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_41-" + j.Description.JobTypes + ".png")%>" width="42" height="37" alt="" /></td></tr>
																<tr><td style="line-height:0px"><img border="0" id="Reactivation600_40048" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40-48.png")%>" width="42" height="22" alt="" /></td></tr>
															</table>
														</td>
														<td style="line-height:0px"><img border="0" id="Reactivation600_40042" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40-42.png")%>" width="10" height="59" alt="" /></td>
														<td style="line-height:0px">
															<table id="main-2-7-2-2-4" width="212" border="0" cellpadding="0" cellspacing="0">
																<tr><td width="212" height="16" valign="top" align="left" bgcolor="#FBF1DC"><a style="font:Bold 12px Arial; color:#000000; line-height:16px;" title="<%= title %>" target="_blank" href="<%= Html.TinyUrl(true, j.GenerateUrl()) %>"><%= title.Length > 30 ? (title.Substring(0, 30) + "...") : title %></a></td></tr>
																<tr><td style="line-height:0px">
																	<table id="main-2-7-2-2-4-2" width="212" border="0" cellpadding="0" cellspacing="0">
																		<tr>
																			<td style="line-height:0px">
																				<table id="main-2-7-2-2-4-2-1" width="154" border="0" cellpadding="0" cellspacing="0">
																				<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
																					if (string.IsNullOrEmpty(location) && j.Description.Location != null)
																						location = j.Description.Location.Country.Name; %>
																					<tr><td width="154" height="14" valign="top" align="left" bgcolor="#FBF1DC" style="font:Bold 11px Arial; color:#547694; line-height:14px;"><%= location.Length > 30 ? (location.Substring(0, 30) + "...") : location %></td></tr>
																					<tr><td width="154" height="18" valign="top" align="left" bgcolor="#FBF1DC" style="font:Bold 11px Arial; color:#809CAD; line-height:18px;">Posted <%= j.CreatedTime.ToString("dd/MM/yyyy")%></td></tr>
																					<tr><td style="line-height:0px"><img border="0" id="Reactivation600_40049" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40-49.png")%>" width="154" height="11" alt="" /></td></tr>
																				</table>
																			</td>
																			<td style="line-height:0px"><img border="0" id="Reactivation600_40046" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40-46.png")%>" width="58" height="43" alt="" /></td>
																		</tr>
																	</table>
																</td></tr>
															</table>
														</td>
														<td style="line-height:0px"><img border="0" id="Reactivation600_40044" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_40-44.png")%>" width="18" height="59" alt="" /></td>
													</tr>
												</table>
											</td></tr>
										</table>
									</td></tr>
									<% } else { %>
									<tr><td style="line-height:0px">
										<table id="main-2-3" width="300" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_42" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_42.png")%>" width="300" height="21" alt="" /></td></tr>
											<tr><td style="line-height:0px">
												<table width="300" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td style="line-height:0px"><img border="0" id="Reactivation600_42051" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_42-51.png")%>" width="18" height="59" alt="" /></td>
														<td style="line-height:0px">
															<table width="42" border="0" cellpadding="0" cellspacing="0">
																<tr><td style="line-height:0px"><img border="0" id="Reactivation600_41" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_41-" + j.Description.JobTypes + ".png")%>" width="42" height="37" alt="" /></td></tr>
																<tr><td style="line-height:0px"><img border="0" id="Reactivation600_42062" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_42-62.png")%>" width="42" height="22" alt="" /></td></tr>
															</table>
														</td>
														<td style="line-height:0px"><img border="0" id="Reactivation600_42053" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_42-53.png")%>" width="9" height="59" alt="" /></td>
														<td style="line-height:0px">
															<table id="main-2-7-2-2-4-2-1" width="213" border="0" cellpadding="0" cellspacing="0">
																<tr><td width="213" height="16" valign="top" align="left" bgcolor="#FBF1DC"><a style="font:Bold 11px Arial; color:#000000; line-height:14px;" title="<%= title %>" target="_blank" href="<%= Html.TinyUrl(true, j.GenerateUrl()) %>"><%= title.Length > 30 ? (title.Substring(0, 30) + "...") : title %></a></td></tr>
																<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
																	if (string.IsNullOrEmpty(location) && j.Description.Location != null)
																		location = j.Description.Location.Country.Name; %>
																<tr><td width="213" height="16" bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#547694; line-height:14px;"><%= location.Length > 30 ? (location.Substring(0, 30) + "...") : location %></td></tr>
																<tr><td width="213" height="16" bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#809CAD; line-height:14px;">Posted <%= j.CreatedTime.ToString("dd/MM/yyyy")%></td></tr>
																<tr><td style="line-height:0px"><img border="0" id="Reactivation600_61002" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_61_02.png")%>" width="213" height="11" alt="" /></td></tr>
															</table>
														</td>
														<td style="line-height:0px"><img border="0" id="Reactivation600_42055" src="<%=Html.ImageUrl("Reactivation600/Reactivation600_42-55.png")%>" width="18" height="59" alt="" /></td>
													</tr>
												</table>
											</td></tr>
										</table>
									</td></tr>
									<% } %>
									<% } %>
									<tr><td style="line-height:0px">
										<table id="main-2-7" width="300" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_48" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_48.png")%>" width="300" height="13" alt="" /></td></tr>
											<tr><td style="line-height:0px">
												<table id="main-2-7-2" width="300" border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td style="line-height:0px"><img border="0" id="Reactivation600_48067" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_48-67.png")%>" width="21" height="43" alt="" /></td>
														<td width="261" height="43" bgcolor="#F1EEE1" valign="top" align="left" style="font:12px Arial; color:#333333; line-height:18px;"><a href="<%= Html.TinyUrl(true, "~/members/profile") %>" target="_blank">Edit your resume</a> to improve the quality of suggested jobs.</td>
														<td style="line-height:0px"><img border="0" id="Reactivation600_48069" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_48-69.png")%>" width="18" height="43" alt="" /></td>
													</tr>
												</table>
											</td></tr>
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_71" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_71.png")%>" width="300" height="24" alt="" /></td></tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_42.png")%>" width="300" height="76" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_49.png")%>" width="23" height="14" alt="" /></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_50.png")%>" width="198" height="14" alt="" /></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_51.png")%>" width="79" height="14" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_52.png")%>" width="300" height="13" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_54.png")%>" width="37" height="115" alt="" /></td>
												<td style="line-height:0px">
													<table width="245" border="0" cellpadding="0" cellspacing="0">
														<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_18.png")%>" width="245" height="10" alt="" /></td></tr>
														<tr><td width="245" height="105" bgcolor="FBF1DC" valign="top" align="left" style="font:12px Arial;color:#333333;line-height:18px;">Your <a href="<%= Html.TinyUrl(true, "~/members/resources/answeredQuestions/all") %>" target="_blank">questions</a> answered by career experts Advice about <a href="<%= Html.TinyUrl(true, "~/members/resources/articles/Job-search?categoryId=8637cdba-6807-4fcf-b30e-2a303449f956") %>" target="_blank">job hunting</a> and <a href="<%= Html.TinyUrl(true, "~/members/resources/articles/Job-interviewing?categoryId=19ad7ec3-658c-4dfc-842e-c66beee26d9b") %>" target="_blank">job interviews</a><br /><a href="<%= Html.TinyUrl(true, "~/members/resources/articles/Resume-writing?categoryId=c25671a7-f05f-4e16-9f16-d216ecd6d875") %>" target="_blank">Resume and cover letter</a> hints and tips Insight about the <a href="<%= Html.TinyUrl(true, "~/members/resources/articles/Australian-job-market?categoryId=be2e6396-2797-48ec-9611-0403f6c88b70") %>" target="_blank">Australian job market</a><br />Advice specific to <a href="<%= Html.TinyUrl(true, "~/members/resources/articles/Students-and-grads?categoryId=76c7365b-b82a-40b9-a897-020e6bdf4e5c") %>" target="_blank">students and grads</a></td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_56.png")%>" width="18" height="115" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_62.png")%>" width="300" height="22" alt="" /></td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_66.png")%>" width="300" height="79" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_68.png")%>" width="78" height="52" alt="" /></td>
												<td width="50" height="52" bgcolor="FBF1DC" valign="top" align="left" style="font:Bold 12px Arial;line-height:18px;"><a href="<%= Html.TinyUrl(true, "http://www.facebook.com/LinkMeAus") %>" target="_blank">Like LinkMe</a></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_70.png")%>" width="80" height="52" alt="" /></td>
												<td width="50" height="52" bgcolor="FBF1DC" valign="top" align="left" style="font:Bold 12px Arial;line-height:18px;"><a href="<%= Html.TinyUrl(true, "http://www.twitter.com/LinkMeJobs") %>" target="_blank">Follow LinkMe</a></td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_72.png")%>" width="42" height="52" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_74.png")%>" width="23" height="64" alt="" /></td>
												<td width="235" height="64" bgcolor="FBF1DC" valign="top" align="left" style="font:12px Arial;color:#333333;line-height:18px;">Connect to LinkMe for the latest news, advice and hints on Australian jobs and managing your career.</td>
												<td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_76.png")%>" width="42" height="64" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_77.png")%>" width="300" height="14" alt="" /></td></tr>
									<% for (var i = 3; i > Model.SuggestedJobs.Count; i-- ) { %>
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_51" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_51.png")%>" width="300" height="80" alt="" /></td></tr>
									<% } %>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr><td style="line-height:0px"><img border="0" src="<%= Html.ImageUrl("NewsLetterApril/NewsLetterApril_78.png")%>" width="600" height="39" alt="" /></td></tr>
			<tr>
				<td style="line-height:0px">
					<table id="footer" width="600" border="0" cellpadding="0" cellspacing="0">
						<tr><td style="line-height:0px"><img border="0" id="Reactivation600_81" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_81.png")%>" width="600" height="28" alt="" /></td></tr>
						<tr><td style="line-height:0px">
							<table id="footer2" width="600" border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td style="line-height:0px"><img border="0" id="Reactivation600_82" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_82.png")%>" width="422" height="15" alt="" /></td>
									<td style="line-height:0px">
										<table id="footer2-2" width="83" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><a target="_blank" href="<%= Html.TinyUrl(false, "~/") %>" target="_blank"><img border="0" id="Reactivation600_83" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_83.png")%>" width="83" height="12" alt="LinkMe.com.au" /></a></td></tr>
											<tr><td height="3" style="line-height:0px;display:block;"><img border="0" id="Reactivation600_85" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_85.png")%>" width="83" height="3" alt="" style="display:block;" /></td></tr>
										</table>
									</td>
									<td style="line-height:0px"><img border="0" id="Reactivation600_84" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_84.png")%>" width="95" height="15" alt="" /></td>
								</tr>
							</table>
						</td></tr>
						<tr><td style="line-height:0px">
							<table id="footer3" width="600" border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td style="line-height:0px"><img border="0" id="Reactivation600_86" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_86.png")%>" width="140" height="51" alt="" /></td>
									<td style="line-height:0px">
										<table id="footer3-2" width="92" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><a target="_blank" href="<%= Html.TinyUrl(true, "~/members/settings") %>" target="_blank"><img border="0" id="Reactivation600_87" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_87.png")%>" width="92" height="12" alt="edit your settings" /></a></td></tr>
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_91" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_91.png")%>" width="92" height="39" alt="" /></td></tr>
										</table>
									</td>
									<td style="line-height:0px"><img border="0" id="Reactivation600_88" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_88.png")%>" width="209" height="51" alt="" /></td>
									<td style="line-height:0px">
										<table id="footer3-4" width="69" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><a target="_blank" href="<%= Html.TinyUrl(false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category) %>" target="_blank"><img border="0" id="Reactivation600_89" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_89.png")%>" width="69" height="12" alt="unsubscribe" /></a></td></tr>
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_92" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_92.png")%>" width="69" height="39" alt="" /></td></tr>
										</table>
									</td>
									<td style="line-height:0px"><img border="0" id="Reactivation600_90" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_90.png")%>" width="90" height="51" alt="" /></td>
								</tr>
							</table>
						</td></tr>
					</table>
				</td>
			</tr>
		</table>
		</td></tr></table>
		<!-- End Save for Web Slices -->
</body>
</html>