using System;
using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    public abstract class EmployerMemberViewEmailTests
        : EmailTests
    {
        private static readonly TimeSpan SearchResultFreshDays = new TimeSpan(28, 0, 0, 0);
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private const int MaximumLocationLength = 40;
        private const int MaximumJobFieldsLength = 40;

        protected void AppendResults(StringBuilder sb, TemplateEmail templateEmail, IEmployer employer, MemberSearchResults results, int start, int count, bool useExtraReturn, ref int resultsWritten)
        {
            if (count > 0)
            {
                sb.AppendLine("  <div class=\"alert-container\">");
                sb.AppendLine();

                for (int index = start; index < start + count; index++)
                {
                    // Get the member for the result.

                    var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, results.MemberIds[index]);
                    AppendResult(templateEmail, sb, view, index == start + 1 ? useExtraReturn : false, ref resultsWritten);
                }

                if (!useExtraReturn)
                    sb.AppendLine();
                sb.AppendLine("  </div>");
            }
        }

        private void AppendResult(TemplateEmail templateEmail, StringBuilder sb, EmployerMemberView view, bool useExtraReturn, ref int resultsWritten)
        {
            ++resultsWritten;

            var tinyUrl = GetTinyUrl(templateEmail, true, "~/employers/login", "returnUrl", new ReadOnlyApplicationUrl("~/employers/candidates", new ReadOnlyQueryString("candidateId", view.Id.ToString())).PathAndQuery);
            if (view.Resume == null || view.Resume.Jobs.IsNullOrEmpty())
            {
                sb.Append("    <div class=\"candidate\" data-id=\"").Append(view.Id).AppendLine("\">");
                sb.AppendLine("      <a href=\"" + tinyUrl + "\" class=\"alert-title\">");
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("        Details unavailable, click to view resume");
                sb.AppendLine();
                sb.AppendLine("      </a>");
                sb.AppendLine("    </div>");
                sb.AppendLine();
                sb.AppendLine();
                if (useExtraReturn)
                    sb.AppendLine();
            }
            else
            {
                // Current job.

                var currentJob = view.Resume.Jobs[0];
                sb.Append("    <div class=\"candidate\" data-id=\"").Append(view.Id).AppendLine("\">");
                sb.AppendLine("      <a href=\"" + tinyUrl + "\" class=\"alert-title\">");
                sb.AppendLine();
                sb.AppendLine();
                sb.Append("        " + TextUtil.TruncateForDisplay(currentJob.Title, MaximumJobFieldsLength));

                string location = view.Address.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                {
                    sb.AppendLine(", " + TextUtil.TruncateForDisplay(location, MaximumLocationLength));
                    sb.AppendLine();
                }
                else
                {
                    sb.AppendLine();
                }
                sb.AppendLine("      </a>");

                // Show the image if the candidate is new.

                if (view.CreatedTime.Date > (DateTime.Today - SearchResultFreshDays))
                {
                    sb.AppendLine("      <img src=\"" + InsecureRootPath + "ui/images/universal/new.png\" alt=\"New\" />");
                }

                sb.AppendLine("    </div>");
                sb.AppendLine("    <div>");
                sb.AppendLine("      <dl class=\"alert-item\">");
                sb.AppendLine("        <dt>Current Employer</dt>");
                var access = ((view.VisibilitySettings.Professional.EmploymentVisibility & ProfessionalVisibility.Resume) != 0)
                    && ((view.VisibilitySettings.Professional.EmploymentVisibility & ProfessionalVisibility.RecentEmployers) != 0);
                var currentEmployer = access && !string.IsNullOrEmpty(currentJob.Company) ? currentJob.Company : "<Employer hidden>";
                sb.AppendLine("        <dd>" + HttpUtility.HtmlEncode(TextUtil.TruncateForDisplay(currentEmployer, MaximumJobFieldsLength)) + "</dd>");

                sb.AppendLine();
                if (view.Resume.Jobs.Count > 1)
                {
                    // Previous job.

                    var previousJob = view.Resume.Jobs[1];
                    if (!string.IsNullOrEmpty(previousJob.Title))
                    {
                        sb.AppendLine("        <dt>Previous</dt>");
                        sb.AppendLine("        <dd>");
                        sb.AppendLine();
                        if (!string.IsNullOrEmpty(previousJob.Company))
                        {
                            string previousEmployer = access ? previousJob.Company : "<Employer hidden>";
                            sb.AppendLine("          " + TextUtil.TruncateForDisplay(previousJob.Title, MaximumJobFieldsLength) + ", " + HttpUtility.HtmlEncode(TextUtil.TruncateForDisplay(previousEmployer, MaximumJobFieldsLength)));
                            sb.AppendLine();
                        }
                        else
                        {
                            sb.AppendLine("          " + TextUtil.TruncateForDisplay(previousJob.Title, MaximumJobFieldsLength));
                            sb.AppendLine();
                        }
                        sb.AppendLine("        </dd>");
                        sb.AppendLine();
                    }
                }   

                // Salary.

                sb.AppendLine();
                if (view.DesiredSalary != null)
                {
                    sb.AppendLine("        <dt>Desired Annual Salary</dt>");
                    sb.AppendLine("        <dd>" + view.DesiredSalary.ToRate(SalaryRate.Year).GetDisplayText() + "</dd>");
                    sb.AppendLine();
                }

                sb.AppendLine("      </dl>");
                sb.AppendLine("    </div>");
                sb.AppendLine();
                sb.AppendLine();
            }
        }
    }
}