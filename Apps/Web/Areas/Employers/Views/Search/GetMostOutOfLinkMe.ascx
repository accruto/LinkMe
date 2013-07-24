<%@ Control Language="C#" Inherits="LinkMe.Apps.Asp.Mvc.Views.ViewUserControl" %>
<%@ Import Namespace="LinkMe.Framework.Utility.Urls"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Fields"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Mvc.Html"%>
<%@ Import Namespace="LinkMe.Apps.Asp.Routing"%>
<%@ Import Namespace="LinkMe.Web.Html"%>
<%@ Import Namespace="LinkMe.Domain"%>

<div class="columns">
    <div class="column">
        <div class="advanced-search_icon36-section icon36-section">
            <div class="get-most-header">Candidate Database</div>
        </div>
        <ol class="get-most_section js_gm_collapsible">
            <li class="section-title"><span class="title down-icon">Conduct regular searches for all your roles</span>
                <div class="section-content">
                    <div>Our database is continually updated as hundreds of new candidates join LinkMe every day and candidates update their resume on a regular basis.</div>
                    <div>Regular searches mean you can proactively contact suitable candidates without having to wait until candidates contact you.</div>
                    <div>We 've assembled a range of tips to help you improve your search results, which you can download here [ <a href="<%= new ReadOnlyApplicationUrl("~/resources/employer/search/SearchTips.pdf") %>">Hints and tips</a> ]</div>
                </div>
            </li>                            
            <li class="section-title"><span class="title down-icon">Contact passive candidates</span>
                <div class="section-content">
                    <div>Sometimes the best candidates aren't actively looking for another role, but are interested in hearing about other opportunities.</div>
                    <div>You can identify these candidates quickly with LinkMe Resume Search and approach them directly.</div>
                    <div>Passive candidates are often flattered to receive a call and can be a great way to ensure you are finding the best candidate in the market, not just the best that applied for your role.</div>
                </div>
            </li>
            <li class="section-title"><span class="title down-icon">Set up email alerts</span>
                <div class="section-content">
                    <div>Email alerts are similar to a saved search, but LinkMe will automatically let you know if any candidates are a match to your search terms every day.</div>
                    <div>You can set up a variety of email alerts to allow you sit back and let LinkMe do all the hard work for you.</div>
                </div>
            </li> 
            <li class="section-title"><span class="title down-icon">Save your favourite searches</span>
                <div class="section-content">
                    <div>Once you 've found a set of search terms and refinements that work for a particular role or set of candidates, you can save your search criteria.</div>
                    <div>This gives you one-click access to this search at any time, allowing you to quickly check the database for any new candidates.</div>
                </div>
            </li> 
            <li class="section-title"><span class="title down-icon">Save suitable candidates in Folders and add notes</span>
                <div class="section-content">
                    <div>You can create folders for particular roles or candidates with particular skills and save candidates into folders as you search.</div>
                    <div>Candidates in this folders will never get stale as their profiles are automatically updated as candidates edit their resumes.</div>
                    <div>You can create private candidate folders only viewable to you, or you can share a folder with other people in your organisation.</div>
                    <div>Add, edit and share notes about candidates with your colleagues to keep track of communications and progress.</div>
                </div>
            </li> 
        </ol>
    </div>
    <div class="column">
        <div class="job-ads_icon36-section icon36-section">
            <div class="get-most-header">Job Board</div>
        </div>
        <ol class="get-most_section js_gm_collapsible">
            <li class="section-title"><span class="title down-icon">Quick and easy online job posting</span>
                <div class="section-content">
                    <div>Our job advertisements are very competitively priced, with additional volume discounts available. Each advertisement is displayed for 30 days, with the option to refresh at any time.</div>
                    <div>You'll receive immediate notification of applications to your inbox. All ads receive automatic promotion across the LinkMe network of job sites.</div>
                </div>
            </li>
            <li class="section-title"><span class="title down-icon">The more information you include on your job advertisement, the better we can help you find suitable candidates</span>
                <div class="section-content">
                    <div>Put as much information on your job advertisements as possible, including a salary level where possible, to encourage relevant applications.</div>
                    <div>LinkMe matches your adverstisement against our candidate database and sends the details to ideal candidates via "Job Alerts" and "Suggested Jobs" emails.</div>
                    <div>Our experience shows that this approach often attracts passive candidates who may not be actively searching on job boards.</div>
                    <!-- <div>Click here for more information on writing effective advertisements.</div> -->
                </div>
            </li>
            <li class="section-title"><span class="title down-icon">Review the "Suggested Candidates" recommended by LinkMe, who match your requirements</span>
                <div class="section-content">
                    <div>LinkMe will provide you with a list of "Suggested Candidates" (when suitable matches are found) when you upload a job advertisement, giving you a head start on sourcing the right candidate.</div>
                    <div>Review these to see who matches your requirements, then make sure you use the LinkMe resume database for more matches.</div>
                    <div>Note that you may need to purchse candidate database credits in order to view full candidate records and/or download their resumes.</div>
                </div>
            </li>
            <li class="section-title"><span class="title down-icon">Manage and keep track of the applications you receive</span>
                <div class="section-content">
                    <div>All applications received for a particular job advertisement are tracked online when you log in to your LinkMe account.</div>
                    <div>You can short-list any suitable candidates by saving them to a folder, or</div>
                    <div>You can reject unsuitable applicants using a pre-formatted rejection email.</div>
                </div>
            </li>
            <li class="section-title"><span class="title down-icon">Integrate LinkMe with your Applicant Tracking System or job ad multi-poster</span>
                <div class="section-content">
                    <div>Integration with your ATS or multi-poster will make uploading your advertisements easy and will capture your applications in your system.</div>
                    <div>At this time, integration is only availabale to customers on a LinkMe contract - check with your Account Manager for more details, or call 1 800 LINKME</div>
                </div>
            </li>
         </ol>
    </div>
    <script language="javascript">
        (function($){
	        /* Initialization for collapsible sections */
            $(".js_gm_collapsible").makeGetMostSubSectionsCollapsible();
        })(jQuery);
    </script>
</div>