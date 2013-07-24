using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Reports;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Apps.Agents.Reports.Employers.Commands;
using LinkMe.Apps.Agents.Reports.Employers.Queries;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Administrators.Models.Organisations;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Administrators.Controllers.Organisations
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class OrganisationReportsController
        : AdministratorsController
    {
        private readonly IOrganisationsQuery _organisationsQuery;
        private readonly IAdministratorsQuery _administratorsQuery;
        private readonly IEmployerReportsQuery _employerReportsQuery;
        private readonly IEmployerReportsCommand _employerReportsCommand;
        private readonly IExecuteEmployerReportsCommand _executeEmployerReportsCommand;

        private static readonly EmployerReport[] AllReports = new EmployerReport[] { new CandidateCareReport(), new ResumeSearchActivityReport(), new JobBoardActivityReport() };

        public OrganisationReportsController(IOrganisationsQuery organisationsQuery, IAdministratorsQuery administratorsQuery, IEmployerReportsQuery employerReportsQuery, IEmployerReportsCommand employerReportsCommand, IExecuteEmployerReportsCommand executeEmployerReportsCommand)
        {
            _organisationsQuery = organisationsQuery;
            _administratorsQuery = administratorsQuery;
            _employerReportsQuery = employerReportsQuery;
            _employerReportsCommand = employerReportsCommand;
            _executeEmployerReportsCommand = executeEmployerReportsCommand;
        }

        public ActionResult Index(Guid id)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            // Retrieve all reports using a default one if needed.

            var enabledReports = _employerReportsQuery.GetReports(organisation.Id);

            var reports = (from a in AllReports
            let r = (from e in enabledReports where e.Name == a.Name select e).SingleOrDefault()
            select r ?? a).ToList();

            return View(new ReportsModel
                            {
                                Organisation = organisation,
                                Reports = reports,
                            });
        }

        [HttpPost]
        public ActionResult Report(Guid id, string type, [Bind(Include = "IncludeChildOrganisations")] CheckBoxValue includeChildOrganisations, [Bind(Include = "IncludeDisabledUsers")] CheckBoxValue includeDisabledUsers, [Bind(Include = "PromoCode")] string promoCode, [Bind(Include = "SendToAccountManager")] CheckBoxValue sendToAccountManager, [Bind(Include = "SendToClient")] CheckBoxValue sendToClient)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var isNew = false;
            var report = _employerReportsQuery.GetReport(id, type);

            if (report == null)
            {
                report = _employerReportsCommand.CreateReportTemplate(id, type);
                isNew = true;
            }

            try
            {
                // Update thre report based on its type.

                UpdateReport(report, includeChildOrganisations, includeDisabledUsers, promoCode, sendToAccountManager, sendToClient);

                if (isNew)
                    _employerReportsCommand.CreateReport(report);
                else
                    _employerReportsCommand.UpdateReport(report);

                return RedirectToRoute(OrganisationsRoutes.Report, new { id, type });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            // Show the errors.

            return View(new ReportModel
                            {
                                Organisation = organisation,
                                AccountManager = GetAccountManager(organisation),
                                ContactDetails = GetContactDetails(organisation),
                                Report = report,
                            });
        }

        public ActionResult Report(Guid id, string type)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var report = _employerReportsQuery.GetReport(id, type)
                ?? _employerReportsCommand.CreateReportTemplate(id, type);

            return View(new ReportModel
                            {
                                Organisation = organisation,
                                AccountManager = GetAccountManager(organisation),
                                ContactDetails = GetContactDetails(organisation),
                                Report = report,
                                IncludeCredits = true,
                            });
        }

        [HttpPost, ActionName("RunReport"), ButtonClicked("Download")]
        public ActionResult RunXlsReport(Guid id, string type, CheckBoxValue includeCredits, DateTime? startDate, DateTime? endDate)
        {
            return RunFileReport(id, type, includeCredits != null && includeCredits.IsChecked, startDate, endDate, true);
        }

        [HttpPost, ActionName("RunReport"), ButtonClicked("DownloadPdf")]
        public ActionResult RunPdfReport(Guid id, string type, CheckBoxValue includeCredits, DateTime? startDate, DateTime? endDate)
        {
            return RunFileReport(id, type, includeCredits != null && includeCredits.IsChecked, startDate, endDate, false);
        }

        [HttpPost, ActionName("RunReport"), ButtonClicked("Search")]
        public ActionResult RunReport(Guid id, string type, DateTime? startDate, DateTime? endDate)
        {
            return RunTextReport(id, type, false, startDate, endDate);
        }

        private ActionResult RunFileReport(Guid id, string type, bool includeCredits, DateTime? startDate, DateTime? endDate, bool isXls)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var report = _employerReportsQuery.GetReport(id, type)
                ?? _employerReportsCommand.CreateReportTemplate(id, type);

            var accountManager = GetAccountManager(organisation);

            try
            {
                var errors = new List<ValidationError>();
                if (startDate == null)
                    errors.Add(new RequiredValidationError("StartDate"));
                if (endDate == null)
                    errors.Add(new RequiredValidationError("EndDate"));
                if (errors.Count > 0)
                    throw new ValidationErrorsException(errors);

                // Run the report.

                var output = new MemoryStream();
                var sb = new StringBuilder();

                var outcome = _executeEmployerReportsCommand.RunReport(
                    report,
                    includeCredits,
                    organisation,
                    accountManager,
                    new DateRange(startDate.Value, endDate.Value),
                    isXls ? output : null,
                    !isXls ? output : null,
                    sb);

                // Either report an error or return the file.

                switch (outcome)
                {
                    case ReportRunOutcome.InvalidParameters:
                        ModelState.AddModelError("The report parameters are invalid.");
                        break;

                    case ReportRunOutcome.NoResults:
                        ModelState.AddModelError("No results were returned for the specified criteria.");
                        break;

                    case ReportRunOutcome.FileResult:
                        output.Seek(0, SeekOrigin.Begin);
                        var fileName = FileSystem.GetValidFileName(organisation.FullName.Replace(Organisation.FullNameSeparator, '-') + " - " + report.Name) + (isXls ? ".xls" : ".pdf");
                        return new FileStreamResult(output, isXls ? MediaType.Excel : MediaType.Pdf) { FileDownloadName = fileName };
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Report", new ReportModel
                                      {
                                          Organisation = organisation,
                                          AccountManager = accountManager,
                                          ContactDetails = GetContactDetails(organisation),
                                          Report = report,
                                          IncludeCredits = includeCredits,
                                          StartDate = startDate,
                                          EndDate = endDate
                                      });
        }

        private ActionResult RunTextReport(Guid id, string type, bool includeCredits, DateTime? startDate, DateTime? endDate)
        {
            var organisation = _organisationsQuery.GetOrganisation(id);
            if (organisation == null)
                return NotFound("organisation", "id", id);

            var report = _employerReportsQuery.GetReport(id, type)
                ?? _employerReportsCommand.CreateReportTemplate(id, type);

            var accountManager = GetAccountManager(organisation);

            try
            {
                var errors = new List<ValidationError>();
                if (startDate == null)
                    errors.Add(new RequiredValidationError("StartDate"));
                if (endDate == null)
                    errors.Add(new RequiredValidationError("EndDate"));
                if (errors.Count > 0)
                    throw new ValidationErrorsException(errors);

                // Run the report.

                var sb = new StringBuilder();

                var outcome = _executeEmployerReportsCommand.RunReport(
                    report,
                    includeCredits,
                    organisation,
                    accountManager,
                    new DateRange(startDate.Value, endDate.Value),
                    null,
                    null,
                    sb);

                // Decide what to do.

                switch (outcome)
                {
                    case ReportRunOutcome.InvalidParameters:
                        ModelState.AddModelError("The report parameters are invalid.");
                        break;

                    case ReportRunOutcome.TextResultOnly:

                        // Shouldn't be an error but will do for now.

                        ModelState.AddModelError(GetResultMessage(report, organisation, sb.ToString()));
                        break;

                    case ReportRunOutcome.NoResults:
                        ModelState.AddModelError("No results were returned for the specified criteria.");
                        break;
                }
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return View("Report", new ReportModel
                                      {
                                          Organisation = organisation,
                                          AccountManager = accountManager,
                                          ContactDetails = GetContactDetails(organisation),
                                          Report = report,
                                          StartDate = startDate,
                                          EndDate = endDate
                                      });
        }

        private static string GetResultMessage(EmployerReport report, IOrganisation organisation, string result)
        {
            if (report is CandidateCareReport)
            {
                return string.IsNullOrEmpty(result)
                    ? string.Format("{0} has not referred any members during this reporting period.", HttpUtility.HtmlEncode(organisation.FullName))
                    : string.Format("{0} has referred {1} member{2} during this reporting period.", HttpUtility.HtmlEncode(organisation.FullName), result, result == "1" ? "" : "s");
            }

            return null;
        }

        private static void UpdateReport(EmployerReport report, CheckBoxValue includeChildOrganisations, CheckBoxValue includeDisabledUsers, string promoCode, CheckBoxValue sendToAccountManager, CheckBoxValue sendToClient)
        {
            if (report is ResumeSearchActivityReport)
                UpdateReport((ResumeSearchActivityReport)report, includeDisabledUsers);
            if (report is CandidateCareReport)
                UpdateReport((CandidateCareReport)report, promoCode);

            UpdateReport(report, includeChildOrganisations, sendToAccountManager, sendToClient);
        }

        private static void UpdateReport(ResumeSearchActivityReport report, CheckBoxValue includeDisabledUsers)
        {
            if (includeDisabledUsers != null)
                report.IncludeDisabledUsers = includeDisabledUsers.IsChecked;
        }

        private static void UpdateReport(CandidateCareReport report, string promoCode)
        {
            report.PromoCode = promoCode;
        }

        private static void UpdateReport(EmployerReport report, CheckBoxValue includeChildOrganisations, CheckBoxValue sendToAccountManager, CheckBoxValue sendToClient)
        {
            if (includeChildOrganisations != null)
                report.IncludeChildOrganisations = includeChildOrganisations.IsChecked;
            if (sendToAccountManager != null)
                report.SendToAccountManager = sendToAccountManager.IsChecked;
            if (sendToClient != null)
                report.SendToClient = sendToClient.IsChecked;
        }

        private IAdministrator GetAccountManager(Organisation organisation)
        {
            return organisation.IsVerified
                ? _administratorsQuery.GetAdministrator(((VerifiedOrganisation)organisation).AccountManagerId)
                : null;
        }

        private ContactDetails GetContactDetails(Organisation organisation)
        {
            if (!organisation.IsVerified)
                return new ContactDetails();

            var verifiedOrganisation = (VerifiedOrganisation)organisation;
            return verifiedOrganisation.ContactDetails
                ?? (_organisationsQuery.GetEffectiveContactDetails(organisation.Id) ?? new ContactDetails());
        }
    }
}