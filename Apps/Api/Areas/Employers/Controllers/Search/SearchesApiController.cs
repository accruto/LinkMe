using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Api.Areas.Employers.Models.Search;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Devices.Apple;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Api.Areas.Employers.Controllers.Search
{
    public class SearchesApiController
        : ApiController
    {
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery;
        private readonly IAppleDevicesQuery _appleDevicesQuery;

        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand;
        private readonly IAppleDevicesCommand _appleDevicesCommand;

        private readonly JavaScriptConverter[] _converters = new[] { new MemberSearchModelJavaScriptConverter() };

        public SearchesApiController(IMemberSearchAlertsQuery memberSearchAlertsQuery, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IAppleDevicesQuery appleDevicesQuery, IMemberSearchAlertsCommand memberSearchAlertsCommand, IAppleDevicesCommand appleDevicesCommand)
        {
            _memberSearchAlertsQuery = memberSearchAlertsQuery;
            _appleDevicesQuery = appleDevicesQuery;

            _memberSearchAlertsCommand = memberSearchAlertsCommand;
            _appleDevicesCommand = appleDevicesCommand;

            _converters = new JavaScriptConverter[] { new MemberSearchCriteriaJavaScriptConverter(locationQuery, industriesQuery) };
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpGet]
        public ActionResult Searches()
        {
            try
            {
                var employer = CurrentEmployer;
                var searches = _memberSearchAlertsQuery.GetMemberSearches(employer.Id);
                var alerts = _memberSearchAlertsQuery.GetMemberSearchAlerts(from s in searches select s.Id, AlertType.AppleDevice);

                var model = new MemberSearchesResponseModel
                {
                    TotalSearches = searches.Count,
                    MemberSearches = (from s in searches
                                      orderby s.CreatedTime descending
                                      select new MemberSearchModel
                                      {
                                          Id = s.Id,
                                          Name = s.Name,
                                          IsAlert = alerts.Any(a => a.MemberSearchId == s.Id),
                                          Criteria = s.Criteria,
                                      }).ToList()
                                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel(), JsonRequestBehavior.AllowGet);
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), ActionName("Searches"), HttpPost]
        public ActionResult NewSearch(string name, bool isAlert, MemberSearchCriteria criteria, string deviceToken)
        {
            try
            {
                var search = new MemberSearch { Name = name, Criteria = criteria.Clone() };
                if (isAlert)
                {
                    _memberSearchAlertsCommand.CreateMemberSearchAlert(CurrentEmployer, search, AlertType.AppleDevice);
                    RegisterDevice(CurrentEmployer.Id, deviceToken);
                }
                else
                    _memberSearchAlertsCommand.CreateMemberSearch(CurrentEmployer, search);

                return Json(new JsonConfirmationModel { Id = search.Id });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), HttpPut]
        public ActionResult EditSearch(Guid id, string name, bool isAlert, MemberSearchCriteria criteria, string deviceToken)
        {
            try
            {
                var search = _memberSearchAlertsQuery.GetMemberSearch(id);
                if (search == null)
                    return JsonNotFound("search");

                search.Name = name;
                search.Criteria = criteria;
                _memberSearchAlertsCommand.UpdateMemberSearch(CurrentEmployer, search, new Tuple<AlertType, bool>(AlertType.AppleDevice, isAlert));

                RegisterDevice(CurrentEmployer.Id, deviceToken);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        [EnsureHttps, ApiEnsureAuthorized(UserType.Employer), ActionName("EditSearch"), HttpDelete]
        public ActionResult DeleteSearch(Guid id)
        {
            try
            {
                var search = _memberSearchAlertsQuery.GetMemberSearch(id);
                if (search == null)
                    return JsonNotFound("search");

                _memberSearchAlertsCommand.DeleteMemberSearch(CurrentEmployer, search);

                return Json(new JsonResponseModel());
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }

        protected override JavaScriptConverter[] GetConverters()
        {
            return _converters;
        }

        private void RegisterDevice(Guid employerId, string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
                return;

            // strip spaces and brackets
            var deviceTokenToStore = deviceToken.Replace(" ", string.Empty).Replace("<", string.Empty).Replace(">",
                string.Empty);

            var devices = _appleDevicesQuery.GetDevices(employerId).Select(s => s.DeviceToken);

            if (devices.Contains(deviceTokenToStore))
                return;
            
            var newDevice = new AppleDevice
                                {
                                    OwnerId = employerId,
                                    Active = true,
                                    DeviceToken = deviceTokenToStore,
                                };

            _appleDevicesCommand.CreateDevice(newDevice);
        }
    }
}