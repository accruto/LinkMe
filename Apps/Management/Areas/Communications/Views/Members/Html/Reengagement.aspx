<%@ Page Language="C#" Inherits="LinkMe.Apps.Management.Areas.Communications.Views.Members.Html.Reengagement" %>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain.Roles.JobAds"%>
<%@ Import Namespace="LinkMe.Domain.Location"%>
<%@ Import Namespace="LinkMe.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Contacts"%>
<%@ Import Namespace="LinkMe.Apps.Presentation.Domain"%>
<%@ Import Namespace="LinkMe.Domain.Roles.JobAds" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications" %>
<%@ Import Namespace="LinkMe.Apps.Management.Areas.Communications.Models.Members" %>
<%@ Import Namespace="LinkMe.Framework.Utility" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="-webkit-text-size-adjust:none;">
	<head runat="server">
        <meta name="LMEP_OK" content="LMEP_OK" />
		<meta name="viewport" content="width = 600, user-scalable = no, initial-scale = 1.0" />
		<title><%= Model.Member.FirstName %>, LinkMe misses you</title>
	</head>
	<body bgcolor="#FFFFFF" style="-webkit-text-size-adjust:none;">
		<!-- Save for Web Slices (Reactivation600.psd) -->
		<table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td align="center">
		<table id="Table_01" width="600" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td style="line-height:0px"><img border="0" id="Reactivation600_01" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_01.png")%>" width="600" height="160" alt="linkmejobs SPECIAL EDITION March 2012" /></td>
			</tr>
			<tr>
				<td style="line-height:0px"><img border="0" id="Reactivation600_02" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_02.png")%>" width="600" height="18" alt="" /></td>
			</tr>
			<tr>
				<td style="line-height:0px">
					<table id="main-0" width="600" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td style="line-height:0px"><img border="0" id="Reactivation600_02-1" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_02-1.png")%>" width="60" height="24" alt="" /></td>
							<td width="480" height="24" valign="top" align="center" bgcolor="#F4D417" style="font:17px Arial; color:#204363; line-height:24px;">Over <b><%= (Model.TotalContactsLastMonth / 1000) + ",000" %></b> candidates were contacted about jobs last month</td>
							<td style="line-height:0px"><img border="0" id="Reactivation600_02-2" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_02-2.png")%>" width="60" height="24" alt="" /></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td style="line-height:0px"><img border="0" id="Reactivation600_02-3" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_02-3.png")%>" width="600" height="45" alt="" /></td>
			</tr>
			<tr>
				<td style="line-height:0px">
					<table id="main" width="600" border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td style="line-height:0px">
								<table id="main-1" width="300" border="0" cellpadding="0" cellspacing="0">
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_03" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_03.png")%>" width="300" height="9" alt="" /></td></tr>
									<tr><td style="line-height:0px">
										<table id="main-1-2" width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" id="Reactivation600_05" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_05.png")%>" width="10" height="478" alt="" /></td>
												<td style="line-height:0px">
													<table id="main-1-2-2" width="280" border="0" cellpadding="0" cellspacing="0">
														<tr><td width="280" height="43" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 16px Arial; color:#333333;">Hi <%= Model.Member.FirstName %>,</td></tr>
														<tr><td width="280" height="100" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;">You joined <a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, false, "~/") %>">LinkMe.com.au</a> a while back. It's been too long since we've heard from you, <% if (Model.JobSearch != null) { %>and we wanted to let you know that there are now <span style="font-weight:bold; color:#F73451;"><%= Model.JobSearch.TotalMatches.ToString("N0")%></span> jobs matching <span style="font-weight:bold;" title="<%= Model.JobSearch.Description%>">"<%= (!string.IsNullOrEmpty(Model.JobSearch.Description) && Model.JobSearch.Description.Length > 70) ? (Model.JobSearch.Description.Substring(0, 70) + "...") : Model.JobSearch.Description %>"</span><% } else { %>so we thought we'd remind you that we're still here helping people find their perfect job. How about we help you too?<% } %></td></tr>
														<tr><td style="line-height:0px">
															<table id="main-1-2-2-3" width="280" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px">
																		<table id="main-1-2-2-3-1" width="87" border="0" cellpadding="0" cellspacing="0">
																			<tr><td style="line-height:0px"><a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, false, "~/search/jobs") %>"><img border="0" id="Reactivation600_16" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_16.png")%>" width="87" height="29" alt="find jobs" /></a></td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_27" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_27.png")%>" width="87" height="54" alt="" /></td></tr>
																			<tr><td style="line-height:0px"><a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/login") %>"><img border="0" id="Reactivation600_30" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_30.png")%>" width="87" height="29" alt="login to your account" /></a></td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_32" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_32.png")%>" width="87" height="82" alt="" /></td></tr>
																		</table>
																	</td>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_17" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_17.png")%>" width="9" height="194" alt="" /></td>
																	<td style="line-height:0px">
																		<table id="main-1-2-2-3-3" width="184" border="0" cellpadding="0" cellspacing="0">
																			<tr><td width="184" height="52" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:21px;"> over 30,000 jobs advertisements available on our job board today</td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_29" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_29.png")%>" width="184" height="32" alt="" /></td></tr>
																			<tr><td width="184" height="110" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:21px;">to update your LinkMe profile (which you created when you joined) and let employers search for you</td></tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td></tr>
														<tr><td style="line-height:0px"><img border="0" id="Reactivation600_35" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_35.png")%>" width="280" height="11" alt="" /></td></tr>
														<tr><td style="line-height:0px">
															<table id="main-1-2-2-5" width="280" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_36" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_36.png")%>" width="13" height="109" alt="" /></td>
																	<td width="245" height="109" valign="top" align="left" bgcolor="#F1EEE1" style="font:12px Arial; color:#333333; line-height:21px;">Even if you're not actively looking for a job, you can <a  target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile") %>">update your availability status</a> and let employers know that you're open to discussing suitable job opportunities. You never know what you might be missing.</td>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_38" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_38.png")%>" width="22" height="109" alt="" /></td>
																</tr>
															</table>
														</td></tr>
														<tr><td style="line-height:0px"><img border="0" id="Reactivation600_56" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_56.png")%>" width="280" height="21" alt="" /></td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" id="Reactivation600_07" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_07.png")%>" width="10" height="478" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px">
										<table id="main-1-3" width="300" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="line-height:0px"><img border="0" id="Reactivation600_58" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_58.png")%>" width="10" height="383" alt="" /></td>
												<td style="line-height:0px">
													<table id="main-1-3-2" width="284" border="0" cellpadding="0" cellspacing="0">
														<tr><td style="line-height:0px"><img border="0" id="Reactivation600_59" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_59.png")%>" width="284" height="72" alt="BE FOUND BY EMPLOYERS TODAY" /></td></tr>
														<tr><td style="line-height:0px">
															<table id="main-1-3-2-2" width="284" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_64" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_64.png")%>" width="70" height="82" alt="" /></td>
																	<td width="214" height="82" valign="top" align="left" bgcolor="#FFFFFF" style="font:Bold 14px Arial; color:#383737; line-height:21px;">Did you know that employers contacted <span style="color:#F73451;"><%= Model.TotalContactsLastWeek.ToString("N0") %></span> LinkMe candidates last week?</td>
																</tr>
															</table>
														</td></tr>
														<tr><td width="284" height="62" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:20px;">You have already set up a basic LinkMe profile, so why not <a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile") %>">update your profile</a> and add your name to the list.</td></tr>
														<tr><td style="line-height:0px"><img border="0" id="Reactivation600_73" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_73.png")%>" width="284" height="19" alt="" /></td></tr>
														<tr><td width="284" height="18" valign="top" align="left" bgcolor="#FFFFFF" style="font:12px Arial; color:#333333; line-height:18px;">Good luck with your job search,</td></tr>
														<tr><td style="line-height:0px">
															<table id="main-1-3-2-6" width="284" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td style="line-height:0px">
																		<table id="main-1-3-2-6-1" width="130" border="0" cellpadding="0" cellspacing="0">
																			<tr><td style="line-height:0px"><a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, false, "~/") %>"><img border="0" id="Reactivation600_75" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_75.png")%>" width="130" height="45" alt="LinkMeJobs" /></a></td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_78" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_78.png")%>" width="130" height="5" alt="" style="display:block;" /></td></tr>
																			<tr><td width="130" height="33" valign="top" align="left" style="font:Bold 12px Arial; color:#333333; line-height:18px;">The LinkMe Team</td></tr>
																			<tr><td style="line-height:0px"><img border="0" id="Reactivation600_80" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_80.png")%>" width="130" height="47" alt="" /></td></tr>
																		</table>
																	</td>
																	<td style="line-height:0px"><img border="0" id="Reactivation600_76" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_76.png")%>" width="154" height="130" alt="" /></td>
																</tr>
															</table>
														</td></tr>
													</table>
												</td>
												<td style="line-height:0px"><img border="0" id="Reactivation600_60" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_60.png")%>" width="6" height="383" alt="" /></td>
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
																	<td style="line-height:0px"><img border="0" id="Reactivation600_04019" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-19.png")%>" width="13" height="207" alt="" /></td>
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
																						<td style="line-height:0px"><img border="0" id="Reactivation600_04025" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-25.png")%>" width="32" height="185" alt="" /></td>
																						<td style="line-height:0px">
																							<table id="main-2-2-1-2-4-2" width="232" border="0" cellpadding="0" cellspacing="0">
																								<tr><td width="237" height="16" bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#<%= Model.ProfilePercentComplete == 100 ? "0C6E18" : "D42042"%>; line-height:10px;">Your profile is <%= Model.ProfilePercentComplete == 100 ? "" : "in"%>complete.</td></tr>
																								<tr><td width="237" height="135"  bgcolor="#FBF1DC" valign="top" align="left" style="font:12px Arial; color:#373737; line-height:14px;">Get expert advice on improving your resume: <a href="<%= GetActivationUrl(Html, Model.ActivationCode, false, "~/members/resources") %>" target="_blank">Ask us</a> today<br/><br/><% if (Model.ProfilePercentComplete == 100) { %><b>Changed your contact details recently? Started a new job or completed a course?</b><br /><br /><a href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile") %>">Update your profile</a> to ensure employers will contact you directly<% } else { %>Please <a href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile") %>">review your profile</a> to increase your chances of being found by employers.<% } %></td></tr>
																								<tr><td style="line-height:0px"><img border="0" id="Reactivation600_04033" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-33.png")%>" width="237" height="34" alt="" /></td></tr>
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
												<td style="line-height:0px"><img border="0" id="Reactivation600_04013" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_04-13.png")%>" width="18" height="305" alt="" /></td>
											</tr>
										</table>
									</td></tr>
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_34" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_34.png")%>" width="300" height="63" alt="YOUR PERSONALISED JOBS" /></td></tr>
									<% var index = 0;
								    foreach (var j in Model.SuggestedJobs) {
								        index++;
                                        if (index > 3) break;
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
																<tr><td width="212" height="16" valign="top" align="left" bgcolor="#FBF1DC"><a style="font:Bold 12px Arial; color:#000000; line-height:16px;" title="<%= j.Title %>" target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, true, j.GenerateUrl()) %>"><%= j.Title.Length > 30 ? (j.Title.Substring(0, 30) + "...") : j.Title %></a></td></tr>
																<tr><td style="line-height:0px">
																	<table id="main-2-7-2-2-4-2" width="212" border="0" cellpadding="0" cellspacing="0">
																		<tr>
																			<td style="line-height:0px">
																				<table id="main-2-7-2-2-4-2-1" width="154" border="0" cellpadding="0" cellspacing="0">
																				<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
																					if (string.IsNullOrEmpty(location) && j.Description.Location != null)
																						location = j.Description.Location.Country.Name; %>
																					<tr><td width="154" height="14" valign="top" align="left" bgcolor="#FBF1DC" style="font:Bold 11px Arial; color:#547694; line-height:14px;"><%= location %></td></tr>
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
																<tr><td width="213" height="16" valign="top" align="left" bgcolor="#FBF1DC"><a style="font:Bold 11px Arial; color:#000000; line-height:14px;" title="<%= j.Title %>" target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, true, j.GenerateUrl()) %>"><%= j.Title.Length > 30 ? (j.Title.Substring(0, 30) + "...") : j.Title %></a></td></tr>
																<%  var location = j.Description.Location == null ? null : j.Description.Location.ToString();
																	if (string.IsNullOrEmpty(location) && j.Description.Location != null)
																		location = j.Description.Location.Country.Name; %>
																<tr><td width="213" height="16" bgcolor="#FBF1DC" valign="top" align="left" style="font:Bold 11px Arial; color:#547694; line-height:14px;"><%= location %></td></tr>
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
														<td width="261" height="43" bgcolor="#F1EEE1" valign="top" align="left" style="font:12px Arial; color:#333333; line-height:18px;"><a href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/profile") %>">Edit your resume</a> to improve the quality of suggested jobs.</td>
														<td style="line-height:0px"><img border="0" id="Reactivation600_48069" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_48-69.png")%>" width="18" height="43" alt="" /></td>
													</tr>
												</table>
											</td></tr>
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_71" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_71.png")%>" width="300" height="24" alt="" /></td></tr>
										</table>
									</td></tr>
									<% for (var i = 3; i > Model.SuggestedJobs.Count; i-- ) { %>
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_51" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_51.png")%>" width="300" height="80" alt="" /></td></tr>
									<% } %>
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_51" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_51.png")%>" width="300" height="20" alt="" /></td></tr>
									<tr><td style="line-height:0px"><img border="0" id="Reactivation600_77" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_77.png")%>" width="300" height="107" alt="" /></td></tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
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
											<tr><td style="line-height:0px"><a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, false, "~/") %>"><img border="0" id="Reactivation600_83" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_83.png")%>" width="83" height="12" alt="LinkMe.com.au" /></a></td></tr>
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
											<tr><td style="line-height:0px"><a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, true, "~/members/settings") %>"><img border="0" id="Reactivation600_87" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_87.png")%>" width="92" height="12" alt="edit your settings" /></a></td></tr>
											<tr><td style="line-height:0px"><img border="0" id="Reactivation600_91" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_91.png")%>" width="92" height="39" alt="" /></td></tr>
										</table>
									</td>
									<td style="line-height:0px"><img border="0" id="Reactivation600_88" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_88.png")%>" width="209" height="51" alt="" /></td>
									<td style="line-height:0px">
										<table id="footer3-4" width="69" border="0" cellpadding="0" cellspacing="0">
											<tr><td style="line-height:0px"><a target="_blank" href="<%= GetActivationUrl(Html, Model.ActivationCode, false, "~/accounts/settings/unsubscribe", "userId", Model.UserId.ToString(), "category", Model.Category) %>"><img border="0" id="Reactivation600_89" src="<%= Html.ImageUrl("Reactivation600/Reactivation600_89.png")%>" width="69" height="12" alt="unsubscribe" /></a></td></tr>
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