using System;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility.Linq;

namespace LinkMe.Domain.Users.Employers.Views
{
    public class EmployerMemberView
        : ProfessionalView, ICandidate
    {
        private class EmployerJobView
            : IJob
        {
            private readonly EmployerMemberView _memberView;
            private readonly Job _job;
            private readonly bool _isRecent;

            public EmployerJobView(EmployerMemberView memberView, Job job, bool isRecent)
            {
                _memberView = memberView;
                _job = job;
                _isRecent = isRecent;
            }

            public Guid Id
            {
                get { return _job.Id; }
            }

            public string Title
            {
                get { return _job.Title; }
            }

            public string Description
            {
                get { return _job.Description; }
            }

            public string Company
            {
                get
                {
                    if (_memberView.CanAccess(ProfessionalVisibility.RecentEmployers))
                        return _job.Company;

                    // Only hide recent jobs.

                    return !_isRecent ? _job.Company : null;
                }
            }

            public PartialDateRange Dates
            {
                get { return _job.Dates; }
            }

            public bool IsCurrent
            {
                get { return _job.IsCurrent; }
            }
        }

        private class EmployerResumeView
            : IResume
        {
            private readonly EmployerMemberView _memberView;
            private readonly Resume _resume;

            public EmployerResumeView(EmployerMemberView memberView, Resume resume)
            {
                _memberView = memberView;
                _resume = resume;
            }

            public Guid Id
            {
                get { return _resume.Id; }
            }

            public bool IsEmpty
            {
                get { return _resume.IsEmpty; }
            }

            public DateTime CreatedTime
            {
                get { return _resume.CreatedTime; }
            }

            public DateTime LastUpdatedTime
            {
                get { return _resume.LastUpdatedTime; }
            }

            public string Skills
            {
                get { return _resume.Skills; }
            }

            public string Objective
            {
                get { return _resume.Objective; }
            }

            public string Summary
            {
                get { return _resume.Summary; }
            }

            public string Other
            {
                get { return _resume.Other; }
            }

            public string Citizenship
            {
                get { return _resume.Citizenship; }
            }

            public string Affiliations
            {
                get { return _resume.Affiliations; }
            }

            public string Professional
            {
                get { return _resume.Professional; }
            }

            public string Interests
            {
                get { return _resume.Interests; }
            }

            public string Referees
            {
                get
                {
                    return _memberView.CanAccess(ProfessionalVisibility.Referees)
                        ? _resume.Referees
                        : null;
                }
            }

            public ReadOnlyCollection<ISchool> Schools
            {
                get
                {
                    var schools = _resume.Schools;
                    return schools == null
                        ? null
                        : schools.Cast<ISchool>().ToReadOnlyCollection();
                }
            }

            public ReadOnlyCollection<string> Courses
            {
                get { return ((IResume)_resume).Courses; }
            }

            public ReadOnlyCollection<string> Awards
            {
                get { return ((IResume)_resume).Awards; }
            }

            public ReadOnlyCollection<IJob> Jobs
            {
                get
                {
                    var jobs = _resume.Jobs;
                    if (jobs == null)
                        return null;

                    // Split into current jobs, previous job and then the rest.

                    var currentJobs = (from j in jobs where j.IsCurrent select j).ToArray();
                    var previousJob = jobs.Skip(currentJobs.Length).Take(1);
                    var otherJobs = jobs.Skip(currentJobs.Length + 1);

                    return (from j in currentJobs select (IJob)new EmployerJobView(_memberView, j, true))
                        .Concat(from j in previousJob select (IJob)new EmployerJobView(_memberView, j, true))
                        .Concat(from j in otherJobs select (IJob)new EmployerJobView(_memberView, j, false)).ToReadOnlyCollection();
                }
            }

            public ReadOnlyCollection<IJob> CurrentJobs
            {
                get
                {
                    var currentJobs = _resume.CurrentJobs;
                    return currentJobs != null
                        ? (from j in currentJobs select (IJob)new EmployerJobView(_memberView, j, true)).ToReadOnlyCollection()
                        : null;
                }
            }

            public IJob PreviousJob
            {
                get
                {
                    var previousJob = _resume.PreviousJob;
                    return previousJob != null
                        ? new EmployerJobView(_memberView, _resume.PreviousJob, true)
                        : null;
                }
            }
        }

        public bool HasBeenViewed { get; private set; }
        public bool IsFlagged { get; private set; }
        public bool IsInMobileFolder { get; private set; }
        public int Folders { get; private set; }
        public int Notes { get; private set; }
        private Candidate _candidate;
        private Resume _resume;
        private bool _resumeViewSet;
        private EmployerResumeView _resumeView;

        public EmployerMemberView()
        {
            _candidate = new Candidate();
        }

        public EmployerMemberView(Member member, int? contactCredits, ProfessionalContactDegree effectiveContactDegree, bool hasBeenAccessed, bool isRepresented)
            : base(member, contactCredits, effectiveContactDegree, hasBeenAccessed, isRepresented)
        {
        }

        public EmployerMemberView(Member member, Candidate candidate, Resume resume, int? contactCredits, ProfessionalContactDegree effectiveContactDegree, bool hasBeenAccessed, bool isRepresented)
            : base(member, contactCredits, effectiveContactDegree, hasBeenAccessed, isRepresented)
        {
            _candidate = candidate;
            _resume = resume;
        }

        internal void Set(Candidate candidate, Resume resume, bool hasBeenViewed, bool isFlagged, bool isInMobileFolder, int folders, int notes)
        {
            _candidate = candidate;
            _resume = resume;
            HasBeenViewed = hasBeenViewed;
            Folders = folders;
            IsFlagged = isFlagged;
            IsInMobileFolder = isInMobileFolder;
            Notes = notes;
        }

        DateTime ICandidate.LastUpdatedTime
        {
            get { return _candidate.LastUpdatedTime; }
        }

        public CandidateStatus Status
        {
            get { return _candidate.Status; }
        }

        public string DesiredJobTitle
        {
            get { return _candidate.DesiredJobTitle; }
        }

        public JobTypes DesiredJobTypes
        {
            get { return _candidate.DesiredJobTypes; }
        }

        public Salary DesiredSalary
        {
            get { return CanAccess(ProfessionalVisibility.Salary) ? _candidate.DesiredSalary : null; }
        }

        public ReadOnlyCollection<Industry> Industries
        {
            get { return ((ICandidate)_candidate).Industries; }
        }

        public ReadOnlyCollection<LocationReference> RelocationLocations
        {
            get { return ((ICandidate)_candidate).RelocationLocations; }
        }

        public RelocationPreference RelocationPreference
        {
            get { return _candidate.RelocationPreference; }
        }

        public EducationLevel? HighestEducationLevel
        {
            get { return _candidate.HighestEducationLevel; }
        }

        public Seniority? RecentSeniority
        {
            get { return _candidate.RecentSeniority; }
        }

        public Profession? RecentProfession
        {
            get { return _candidate.RecentProfession; }
        }

        public VisaStatus? VisaStatus
        {
            get { return _candidate.VisaStatus; }
        }

        Guid? ICandidate.ResumeId
        {
            get { return _candidate.ResumeId; }
        }

        public IResume Resume
        {
            get
            {
                if (_resumeViewSet)
                    return _resumeView;

                _resumeView = _resume != null && CanAccess(ProfessionalVisibility.Resume)
                    ? new EmployerResumeView(this, _resume)
                    : null;

                _resumeViewSet = true;
                return _resumeView;
            }
        }

        public CanContactStatus CanAccessResume()
        {
            return _resume == null ? CanContactStatus.No : CanContact();
        }
    }

    public class EmployerMemberViews
        : ProfessionalViewCollection<EmployerMemberView>
    {
    }
}