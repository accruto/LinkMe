using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Resumes
{
    public class Resume
        : IResume, ICloneable
    {
        private JobList _jobs;
        private SchoolList _schools;
        private IList<string> _courses;
        private IList<string> _awards;

        [DefaultNewGuid]
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public string Skills { get; set; }
        public string Objective { get; set; }
        public string Summary { get; set; }
        public string Other { get; set; }
        public string Citizenship { get; set; }
        public string Affiliations { get; set; }
        public string Professional { get; set; }
        public string Interests { get; set; }
        public string Referees { get; set; }

        public IList<string> Courses
        {
            get
            {
                return _courses;
            }
            set
            {
                if (value == null)
                {
                    _courses = null;
                }
                else
                {
                    var courses = (from c in value
                                   where c != null && !string.IsNullOrEmpty(c.Trim())
                                   select c).ToList();
                    _courses = courses.Count == 0 ? null : courses;
                }
            }
        }

        ReadOnlyCollection<string> IResume.Courses { get { return Courses == null ? null : Courses.ToReadOnlyCollection(); } }

        public IList<string> Awards
        {
            get
            {
                return _awards;
            }
            set
            {
                if (value == null)
                {
                    _awards = null;
                }
                else
                {
                    var awards = (from a in value
                                  where a != null && !string.IsNullOrEmpty(a.Trim())
                                  select a).ToList();
                    _awards = awards.Count == 0 ? null : awards;
                }
            }
        }

        ReadOnlyCollection<string> IResume.Awards { get { return Awards == null ? null : Awards.ToReadOnlyCollection(); } }

        [Prepare, Validate]
        public IList<School> Schools
        {
            get
            {
                return _schools;
            }
            set
            {
                if (value == null)
                {
                    _schools = null;
                }
                else
                {
                    var schools = new SchoolList(value);
                    _schools = schools.Count == 0 ? null : schools;
                }
            }
        }

        [Prepare, Validate]
        public IList<Job> Jobs
        {
            get
            {
                return _jobs;
            }
            set
            {
                if (value == null)
                {
                    _jobs = null;
                }
                else
                {
                    var jobs = new JobList(value);
                    _jobs = jobs.Count == 0 ? null : jobs;
                }
            }
        }

        public IList<Job> CurrentJobs
        {
            get
            {
                var jobs = Jobs;
                return jobs == null
                           ? null
                           : jobs.TakeWhile(j => j.IsCurrent).ToList();
            }
        }

        public Job PreviousJob
        {
            get
            {
                var jobs = Jobs;
                return jobs == null
                    ? null
                    : jobs.SkipWhile(j => j.IsCurrent).FirstOrDefault(j => !j.IsCurrent);
            }
        }

        ReadOnlyCollection<ISchool> IResume.Schools
        {
            get
            {
                var schools = Schools;
                return schools == null
                    ? null
                    : schools.Cast<ISchool>().ToReadOnlyCollection();
            }
        }

        ReadOnlyCollection<IJob> IResume.Jobs
        {
            get
            {
                var jobs = Jobs;
                return jobs == null
                    ? null
                    : jobs.Cast<IJob>().ToReadOnlyCollection();
            }
        }

        ReadOnlyCollection<IJob> IResume.CurrentJobs
        {
            get
            {
                var currentJobs = CurrentJobs;
                return currentJobs == null
                    ? null
                    : currentJobs.Cast<IJob>().ToReadOnlyCollection();
            }
        }

        IJob IResume.PreviousJob
        {
            get { return PreviousJob; }
        }

        public bool IsEmpty
        {
            get
            {
                return _courses == null
                       && _awards == null
                       && string.IsNullOrEmpty(Skills)
                       && string.IsNullOrEmpty(Objective)
                       && string.IsNullOrEmpty(Summary)
                       && string.IsNullOrEmpty(Other)
                       && string.IsNullOrEmpty(Citizenship)
                       && string.IsNullOrEmpty(Affiliations)
                       && string.IsNullOrEmpty(Professional)
                       && string.IsNullOrEmpty(Interests)
                       && string.IsNullOrEmpty(Referees)
                       && _schools == null
                       && _jobs == null;
            }
        }

        public Resume Clone()
        {
            return new Resume
            {
                Skills = Skills,
                Objective = Objective,
                Summary = Summary,
                Other = Other,
                Citizenship = Citizenship,
                Affiliations = Affiliations,
                Professional  = Professional,
                Interests = Interests,
                Referees = Referees,
                Courses = Courses == null ? null : Courses.ToList(),
                Awards = Awards == null ? null : Awards.ToList(),
                Jobs = Jobs == null ? null : (from j in Jobs select j.Clone()).ToList(),
                Schools = Schools == null ? null : (from s in Schools select s.Clone()).ToList(),
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}