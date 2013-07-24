using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using LinkMe.Apps.Services.External.Dewr.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Reports.Roles.Integration;
using LinkMe.Query.Reports.Roles.Integration.Commands;

namespace LinkMe.Apps.Services.External.JobSearch
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobAdExporter
        : IJobAdExporter
    {
        #region Security Configuration

        private readonly SecurityUsernameToken _usernameToken = new SecurityUsernameToken
        {
            ValueType = "AD",
            Username = new Username { Value = "SV_UUDH" },
            Password = new Password { Value = "r6RvZ868" }
        };

        private const string OrgCode = "UUDH";
        private const string SiteCode = "M310";
        private const long EmployerId = 43781208;

        #endregion

        private static readonly EventSource Logger = new EventSource<JobAdExporter>();
        private readonly IChannelManager<IPublicVacancy> _channelManager;
        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdExportCommand _exportCommand;
        private readonly IIntegrationQuery _integrationQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IJobAdIntegrationReportsCommand _jobAdIntegrationReportsCommand;
        private readonly JobAdMapper _mapper;
        private readonly IntegratorUser _integratorUser;

        private class ControlNumbers
        {
            public int integrityControlNumber { get; set; }
            public int detailsIntegrityControlNumber { get; set; }
            public int agentIntegrityControlNumber { get; set; }
            public int contactIntegrityControlNumber { get; set; }
            public int contactAddressIntegrityControlNumber { get; set; }
        }

        private Guid[] _excludedIntegratorIds = new Guid[0];
        public string[] ExcludedIntegrators
        {
            set { _excludedIntegratorIds = Array.ConvertAll(value, GetIntegratorId); }
        }

        public JobAdExporter(IChannelManager<IPublicVacancy> channelManager, IDewrQuery dewrQuery, IJobAdsQuery jobAdsQuery, IJobAdExportCommand exportCommand, IIndustriesQuery industriesQuery, IEmployersQuery employersQuery, IIntegrationQuery integrationQuery, IJobAdIntegrationReportsCommand jobAdIntegrationReportsCommand)
        {
            _channelManager = channelManager;
            _jobAdsQuery = jobAdsQuery;
            _integrationQuery = integrationQuery;
            _exportCommand = exportCommand;
            _employersQuery = employersQuery;
            _jobAdIntegrationReportsCommand = jobAdIntegrationReportsCommand;
            _mapper = new JobAdMapper(industriesQuery);
            _integratorUser = dewrQuery.GetIntegratorUser();
        }

        #region Implementation of IJobAdExporter

        void IJobAdExporter.Add(Guid jobAdId)
        {
            const string method = "Add";

            try
            {
                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
                if (jobAd == null)
                    return;

                if (!IsExcluded(jobAd))
                    Add(jobAd);
            }
            catch (Exception e)
            {
                #region Log
                Logger.Raise(Event.Error, method, e, null, Event.Arg("jobAdId", jobAdId));
                #endregion
            }
        }

        void IJobAdExporter.Update(Guid jobAdId)
        {
            const string method = "Update";

            try
            {
                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
                if (jobAd == null)
                    return;

                // Dont't bother with bulk updates from integrators.

                if (jobAd.Integration.IntegratorUserId != null)
                    return;

                // Decide whether to add or update.

                var vacancyId = _exportCommand.GetJobSearchId(jobAd.Id);
                if (vacancyId == null)
                {
                    if (!IsExcluded(jobAd))
                        Add(jobAd);
                }
                else
                {
                    Update(vacancyId.Value, jobAd);
                }
            }
            catch (Exception e)
            {
                #region Log
                Logger.Raise(Event.Error, method, e, null, Event.Arg("jobAdId", jobAdId));
                #endregion
            }

            #region Log
            Logger.Raise(Event.Flow, method, "Job ad has been updated.", Event.Arg("jobAdId", jobAdId));
            #endregion
        }

        void IJobAdExporter.Delete(Guid jobAdId)
        {
            const string method = "Delete";

            try
            {
                long? vacancyId = _exportCommand.GetJobSearchId(jobAdId);
                if (vacancyId == null)
                {
                    _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportCloseEvent { Success = false, IntegratorUserId = _integratorUser.Id, NotFound = 1, JobAds = 1 });

                    #region Log
                    Logger.Raise(Event.Trace, method, "Job ad was never posted.", Event.Arg("jobAdId", jobAdId));
                    #endregion
                }
                else
                {
                    Delete(vacancyId.Value);
                    _exportCommand.DeleteJobSearchId(jobAdId);
                }
            }
            catch (Exception e)
            {
                #region Log
                Logger.Raise(Event.Error, method, e, null, Event.Arg("jobAdId", jobAdId));
                #endregion
            }

            #region Log
            Logger.Raise(Event.Flow, method, "Job has been deleted.", Event.Arg("jobAdId", jobAdId));
            #endregion
        }

        #endregion

        private void Add(JobAd jobAd)
        {
            const string method = "Add";

            var request = new AddVacancyRequestMessage { Body = _mapper.CreateAddRequestBody(jobAd) };
            SetSecurity(request);

            if (request.Body.daysToExpiry <= 2)
            {
                #region Log
                Logger.Raise(Event.Flow, method, "Job ad will not be published as it is about to expire.",
                    Event.Arg("jobAdId", jobAd.Id), Event.Arg("daysToExpiry", request.Body.daysToExpiry));
                #endregion

                return;
            }

            var channel = _channelManager.Create();
            AddVacancyResponseMessage response;
            try
            {
                response = channel.AddVacancy(request);
            }
            catch (Exception)
            {
                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = 1, JobAds = 1 });
                _channelManager.Abort(channel);
                throw;
            }
            _channelManager.Close(channel);

            if (response.Header.executionStatus == EsiExecutionStatus.Failed)
            {
                var ser = new System.Xml.Serialization.XmlSerializer(request.GetType());
                var requestStream = new StringWriter();
                ser.Serialize(requestStream, request);

                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = 1, JobAds = 1 });

                #region Log
                Logger.Raise(Event.Error, method, response.Body.ErrorString,
                    Event.Arg("jobAdId", jobAd.Id),
                    Event.Arg("messages", Array.ConvertAll(response.Header.Messages, m => m.text)),
                    Event.Arg("request", requestStream));
                #endregion
                return;
            }

            _exportCommand.CreateJobSearchId(jobAd.Id, response.Body.vacancyID);

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = true, IntegratorUserId = _integratorUser.Id, Posted = 1, JobAds = 1 });

            #region Log
            Logger.Raise(Event.Flow, method, "Job ad has been published.", Event.Arg("jobAdId", jobAd.Id));
            #endregion
        }

        private void Update(long vacancyId, JobAd jobAd)
        {
            const string method = "Update";

            // Update.

            var channel = _channelManager.Create();

            var request = new UpdateVacancyRequestMessage {Body = _mapper.CreateUpdateRequestBody(jobAd)};
            request.Body.vacancyID = vacancyId;

            var controlNumbers = GetControlNumbers(channel, vacancyId);
            request.Body.integrityControlNumber = controlNumbers.integrityControlNumber;
            request.Body.contactIntegrityControlNumber = controlNumbers.contactIntegrityControlNumber;
            request.Body.contactAddressIntegrityControlNumber = controlNumbers.contactAddressIntegrityControlNumber;
            request.Body.detailsIntegrityControlNumber = controlNumbers.detailsIntegrityControlNumber;
            request.Body.agentIntegrityControlNumber = controlNumbers.agentIntegrityControlNumber;

            SetSecurity(request);

            UpdateVacancyResponseMessage response;
            try
            {
                response = channel.UpdateVacancy(request);
            }
            catch (Exception)
            {
                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = 1, JobAds = 1 });
                _channelManager.Abort(channel);
                throw;
            }

            _channelManager.Close(channel);

            if (response.Header.executionStatus == EsiExecutionStatus.Failed)
            {
                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = 1, JobAds = 1 });

                #region Log

                Logger.Raise(Event.Error, method, response.Body.ErrorString,
                    Event.Arg("jobAdId", jobAd.Id),
                    Event.Arg("messages", Array.ConvertAll(response.Header.Messages, m => m.text)));

                #endregion
            }

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportPostEvent { Success = true, IntegratorUserId = _integratorUser.Id, Updated = 1, JobAds = 1 });
        }

        private void Delete(long vacancyId)
        {
            const string method = "Delete";

            var channel = _channelManager.Create();

            var request = new DeleteVacancyRequestMessage
            {
                Body = new DeleteVacancyRequestBody {vacancyID = vacancyId}
            };

            var controlNumbers = GetControlNumbers(channel, vacancyId);
            request.Body.integrityControlNumber = controlNumbers.integrityControlNumber;

            SetSecurity(request);

            DeleteVacancyResponseMessage response;
            try
            {
                response = channel.DeleteVacancy(request);
            }
            catch (Exception)
            {
                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportCloseEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = 1, JobAds = 1 });
                _channelManager.Abort(channel);
                throw;
            }

            _channelManager.Close(channel);

            if (response.Header.executionStatus == EsiExecutionStatus.Failed)
            {
                _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportCloseEvent { Success = false, IntegratorUserId = _integratorUser.Id, Failed = 1, JobAds = 1 });

                #region Log

                Logger.Raise(Event.Error, method, "A call to the DeleteVacancy service failed.",
                    Event.Arg("vacancyId", vacancyId),
                    Event.Arg("messages", Array.ConvertAll(response.Header.Messages, m => m.text)));

                #endregion
            }

            _jobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(new JobAdExportCloseEvent { Success = true, IntegratorUserId = _integratorUser.Id, Closed = 1, JobAds = 1 });
        }

        private void SetSecurity(AddVacancyRequestMessage request)
        {
            request.Security = new Security { UsernameToken = _usernameToken };
            request.Body.orgCode = OrgCode;
            request.Body.siteCode = SiteCode;
            request.Body.employerID = EmployerId;
        }

        private void SetSecurity(UpdateVacancyRequestMessage request)
        {
            request.Security = new Security { UsernameToken = _usernameToken };
            request.Body.orgCode = OrgCode;
            request.Body.siteCode = SiteCode;
            request.Body.employerID = EmployerId;
        }

        private void SetSecurity(DeleteVacancyRequestMessage request)
        {
            request.Security = new Security { UsernameToken = _usernameToken };
        }

        private void SetSecurity(GetVacancyDetailsRequestMessage request)
        {
            request.Security = new Security { UsernameToken = _usernameToken };
        }

        private Guid GetIntegratorId(string integratorName)
        {
            var integrator = _integrationQuery.GetIntegratorUser(integratorName);
            if (integrator == null)
            {
                throw new ApplicationException("There is no IntegratorUser with username '"
                    + integratorName + "'.");
            }

            return integrator.Id;
        }

        private bool IsExcluded(JobAdEntry jobAd)
        {
            if (jobAd.Status != JobAdStatus.Open)
                return true;

            if (jobAd.Integration == null || jobAd.Integration.IntegratorUserId == null)
                return false;

            if (_excludedIntegratorIds.Contains(jobAd.Integration.IntegratorUserId.Value))
                return true;

            // Do not export community job ads.

            var employer = _employersQuery.GetEmployer(jobAd.PosterId);
            if (employer == null)
                return false;

            return ((IEmployer)employer).AffiliateId != null;
        }

        private ControlNumbers GetControlNumbers(IPublicVacancy channel, long vacancyId)
        {
            const string method = "GetControlNumbers";

            var request = new GetVacancyDetailsRequestMessage
            {
                Body = new GetVacancyDetailsRequestBody { vacancyID = vacancyId }
            };
            SetSecurity(request);

            GetVacancyDetailsResponseMessage response;
            try
            {
                response = channel.GetVacancyDetails(request);
            }
            catch (Exception)
            {
                _channelManager.Abort(channel);
                throw;
            }

            if (response.Header.executionStatus == EsiExecutionStatus.Failed)
            {
                #region Log
                Logger.Raise(Event.Error, method, "Unable to get the control numbers.",
                    Event.Arg("vacancyId", vacancyId),
                    Event.Arg("messages", Array.ConvertAll(response.Header.Messages, m => m.text)));
                #endregion
                _channelManager.Close(channel);
                throw new InvalidOperationException("Unable to get the control numbers.");
            }

            return new ControlNumbers
            {
                agentIntegrityControlNumber = response.Body.agentIntegrityControlNumber,
                contactAddressIntegrityControlNumber = response.Body.contactAddressIntegrityControlNumber,
                contactIntegrityControlNumber = response.Body.contactIntegrityControlNumber,
                detailsIntegrityControlNumber = response.Body.detailsIntegrityControlNumber,
                integrityControlNumber = response.Body.integrityControlNumber,
            };
        }
    }
}
