using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Location;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members
{
    public class MemberSearchSortCriteria
    {
        public MemberSortOrder SortOrder { get; set; }
        public bool ReverseSortOrder { get; set; }
    }

    public class MemberSearchCriteria
        : SearchCriteria, ISearchCriteriaLocation, ISearchCriteriaIndustries, ICanBeEmpty
    {
        public const MemberSortOrder DefaultSortOrder = MemberSortOrder.Relevance;
        public const int DefaultDistance = 50;
        public const int DefaultRegionalDistance = 0;
        public const JobsToSearch DefaultJobTitlesToSearch = JobsToSearch.RecentJobs;
        public const bool DefaultIncludeSynonyms = true;
        public const bool DefaultHasResume = true;
        public const bool DefaultIsActivated = true;
        public const bool DefaultIsContactable = true;
        public const bool DefaultExcludeNoSalary = false;

        public const int DefaultRecency = 183;
        public const int MaxRecency = 731;
        public const int MinSalary = 0;
        public const int MaxSalary = 250000;
        public const int StepSalary = 5000;

        public const JobTypes DefaultJobTypes = JobTypes.All;
        public const bool DefaultIncludeRelocating = false;
        public const bool DefaultIncludeInternational = false;
        public const JobsToSearch DefaultCompaniesToSearch = JobsToSearch.AllJobs;
        public const bool DefaultIncludeSimilarNames = false;

        private const string JobTitleName = "JobTitle";
        private const string CommunityIdName = "Community";
        private const string SalaryLowerBoundName = "SalaryLowerBound";
        private const string SalaryUpperBoundName = "SalaryUpperBound";
        private const string ExcludeNoSalaryName = "ExcludeNoSalary";
        private const string IndustryIdsName = "IndustryIds";
        private const string IndustriesName = "Industries";
        private const string IndustryIdName = "IndustryId";
        private const string SortOrderName = "SortOrder";
        private const string ReverseSortOrderName = "ReverseSortOrder";
        private const string IncludeSynonymsName = "IncludeSynonyms";
        private const string HasResumeName = "HasResume";
        private const string IsActivatedName = "IsActivated";
        private const string IsContactableName = "IsContactable";
        private const string CountryName = "Country";
        private const string LocationName = "Location";
        private const string AreaName = "Area";
        private const string PostcodeName = "Postcode";
        private const string StateName = "State";
        private const string RecencyName = "Recency";
        private const string IncludeRelocatingName = "IncludeRelocating";
        private const string IncludeInternationalName = "IncludeInternational";
        private const string DistanceName = "Distance";
        private const string JobTitlesToSearchName = "JobsToSearch";
        private const string CompanyKeywordsName = "CompanyKeywords";
        private const string CompaniesToSearchName = "CompaniesToSearch";
        private const string EducationKeywordsName = "EducationKeywords";
        private const string DesiredJobTitleName = "DesiredJobTitle";
        private const string JobTypesName = "IdealJobTypes";
        private const string CandidateFlagsName = "CandidateFlags";
        private const string EthnicStatusName = "EthnicFlags";
        private const string VisaStatusFlagsName = "VisaStatusFlags";
        private const string KeywordsName = "Keywords";
        private const string InFolderName = "InFolder";
        private const string IsFlaggedName = "IsFlagged";
        private const string HasNotesName = "HasNotes";
        private const string HasViewedName = "HasViewed";
        private const string IsUnlockedName = "IsUnlocked";
        private const string IncludeSimilarNamesName = "IncludeSimilarNames";
        private const string NameName = "Name";

        private LocationReference _locationReference;
        private string _allKeywords;
        private string _exactPhrase;
        private string _anyKeywords;
        private string _withoutKeywords;

        private static readonly IDictionary<string, CriteriaDescription> Descriptions = new Dictionary<string, CriteriaDescription>
        {
            {JobTitleName, new CriteriaValueDescription<string>(null)},
            {CommunityIdName, new CriteriaValueDescription<Guid?>(null, new GuidValuePersister())},
            {SalaryLowerBoundName, new CriteriaValueDescription<decimal?>(null, new DecimalValuePersister())},
            {SalaryUpperBoundName, new CriteriaValueDescription<decimal?>(null, new DecimalValuePersister())},
            {ExcludeNoSalaryName, new CriteriaValueDescription<bool>(DefaultExcludeNoSalary, new BoolValuePersister())},
            {IndustryIdsName, new CriteriaListDescription<Guid>(g => g.ToString(), s => new Guid(s))},
            {IndustriesName, new CriteriaValueDescription<string>(null)},
            {IndustryIdName, new CriteriaValueDescription<Guid?>(null, new GuidValuePersister())},
            {SortOrderName, new CriteriaValueDescription<MemberSortOrder>(DefaultSortOrder, new EnumValuePersister<MemberSortOrder>())},
            {ReverseSortOrderName, new CriteriaValueDescription<bool>(false, new BoolValuePersister())},
            {IncludeSynonymsName, new CriteriaValueDescription<bool>(DefaultIncludeSynonyms, new BoolValuePersister())},
            {HasResumeName, new CriteriaValueDescription<bool?>(DefaultHasResume, new BoolValuePersister())},
            {IsActivatedName, new CriteriaValueDescription<bool?>(DefaultIsActivated, new BoolValuePersister())},
            {IsContactableName, new CriteriaValueDescription<bool?>(DefaultIsContactable, new BoolValuePersister())},
            {CountryName, new CriteriaValueDescription<int?>(null, new IntValuePersister())},
            {LocationName, new CriteriaValueDescription<string>(null)},
            {AreaName, new CriteriaValueDescription<int?>(null, new IntValuePersister())},
            {PostcodeName, new CriteriaValueDescription<string>(null)},
            {StateName, new CriteriaValueDescription<string>(null)},
            {RecencyName, new CriteriaValueDescription<TimeSpan?>(null, new TimeSpanValuePersister())},
            {IncludeRelocatingName, new CriteriaValueDescription<bool>(DefaultIncludeRelocating, new BoolValuePersister())},
            {IncludeInternationalName, new CriteriaValueDescription<bool>(DefaultIncludeInternational, new BoolValuePersister())},
            {DistanceName, new CriteriaValueDescription<int?>(null, new IntValuePersister())},
            {JobTitlesToSearchName, new CriteriaValueDescription<JobsToSearch>(DefaultJobTitlesToSearch, new EnumValuePersister<JobsToSearch>())},
            {CompanyKeywordsName, new CriteriaValueDescription<string>(null)},
            {CompaniesToSearchName, new CriteriaValueDescription<JobsToSearch>(DefaultCompaniesToSearch, new EnumValuePersister<JobsToSearch>())},
            {EducationKeywordsName, new CriteriaValueDescription<string>(null)},
            {DesiredJobTitleName, new CriteriaValueDescription<string>(null)},
            {JobTypesName, new CriteriaValueDescription<JobTypes>(DefaultJobTypes, new EnumValuePersister<JobTypes>())},
            {CandidateFlagsName, new CriteriaValueDescription<CandidateStatusFlags?>(null, new EnumValuePersister<CandidateStatusFlags>())},
            {EthnicStatusName, new CriteriaValueDescription<EthnicStatus?>(null, new EnumValuePersister<EthnicStatus>())},
            {VisaStatusFlagsName, new CriteriaValueDescription<VisaStatusFlags?>(null, new EnumValuePersister<VisaStatusFlags>())},
            {KeywordsName, new CriteriaValueDescription<string>(null)},
            {InFolderName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {IsFlaggedName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {HasNotesName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {HasViewedName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {IsUnlockedName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {IncludeSimilarNamesName, new CriteriaValueDescription<bool>(DefaultIncludeSimilarNames, new BoolValuePersister())},
            {NameName, new CriteriaValueDescription<string>(null)},
        };

        private static readonly IDictionary<string, CriteriaDescription> CombinedDescriptions = Combine(Descriptions);

        public MemberSearchCriteria()
            : base(CombinedDescriptions)
        {
            SortCriteria = new MemberSearchSortCriteria {SortOrder = DefaultSortOrder, ReverseSortOrder = false};
        }

        public IExpression KeywordsExpression { get; protected set; }

        public string JobTitle
        {
            get
            {
                return GetValue<string>(JobTitleName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(JobTitleName, value);
                JobTitleExpression = Expression.Parse(value);
            }
        }

        public IExpression JobTitleExpression { get; private set; }

        int? ISearchCriteriaLocation.CountryId
        {
            get { return GetValue<int?>(CountryName); }
        }

        string ISearchCriteriaLocation.LocationText
        {
            get { return GetValue<string>(LocationName); }
        }

        int? ISearchCriteriaLocation.Area
        {
            get { return GetValue<int?>(AreaName); }
        }

        string ISearchCriteriaLocation.Postcode
        {
            get { return GetValue<string>(PostcodeName); }
        }

        string ISearchCriteriaLocation.State
        {
            get { return GetValue<string>(StateName); }
        }

        public LocationReference Location
        {
            get
            {
                return _locationReference;
            }
            set
            {
                _locationReference = value;

                if (value == null)
                {
                    SetValue(CountryName, null);
                    SetValue(LocationName, null);
                }
                else
                {
                    SetValue(CountryName, value.Country.Id);
                    var location = value.ToString();
                    SetValue(LocationName, !string.IsNullOrEmpty(location) ? location : null);
                }

                SetValue(AreaName, null);
                SetValue(PostcodeName, null);
                SetValue(StateName, null);
            }
        }

        public Guid? CommunityId
        {
            get { return GetValue<Guid?>(CommunityIdName); }
            set { SetValue(CommunityIdName, value); }
        }

        public Salary Salary
        {
            get
            {
                var lowerBound = GetValue<decimal?>(SalaryLowerBoundName);
                var upperBound = GetValue<decimal?>(SalaryUpperBoundName);

                if (lowerBound == null && upperBound == null)
                    return null;

                return new Salary
                           {
                               LowerBound = lowerBound,
                               UpperBound = upperBound,
                               Rate = SalaryRate.Year,
                               Currency = Currency.AUD,
                           };
            }
            set
            {
                SetValue(SalaryLowerBoundName, value == null ? null : value.LowerBound);
                SetValue(SalaryUpperBoundName, value == null ? null : value.UpperBound);
            }
        }

        public bool ExcludeNoSalary
        {
            get { return GetValue<bool>(ExcludeNoSalaryName); }
            set { SetValue(ExcludeNoSalaryName, value); }
        }

        public IList<Guid> IndustryIds
        {
            get { return GetValue<IList<Guid>>(IndustryIdsName); }
            set { SetValue(IndustryIdsName, value); }
        }

        public TimeSpan? Recency
        {
            get { return GetValue<TimeSpan?>(RecencyName); }
            set { SetValue(RecencyName, value); }
        }

        string ISearchCriteriaIndustries.Industries
        {
            get { return GetValue<string>(IndustriesName); }
        }

        Guid? ISearchCriteriaIndustries.IndustryId
        {
            get { return GetValue<Guid?>(IndustryIdName); }
        }

        public MemberSearchSortCriteria SortCriteria
        {
            get
            {
                return new MemberSearchSortCriteria
                {
                    SortOrder = GetValue<MemberSortOrder>(SortOrderName),
                    ReverseSortOrder = GetValue<bool>(ReverseSortOrderName),
                };
            }
            set
            {
                if (value == null)
                {
                    SetValue(SortOrderName, DefaultSortOrder);
                    SetValue(ReverseSortOrderName, false);
                }
                else
                {
                    SetValue(SortOrderName, value.SortOrder);
                    SetValue(ReverseSortOrderName, value.ReverseSortOrder);
                }
            }
        }

        public bool IncludeSynonyms
        {
            get { return GetValue<bool>(IncludeSynonymsName); }
            set { SetValue(IncludeSynonymsName, value); }
        }

        public bool? HasResume
        {
            get { return GetValue<bool?>(HasResumeName); }
            set { SetValue(HasResumeName, value); }
        }

        public bool? IsActivated
        {
            get { return GetValue<bool?>(IsActivatedName); }
            set { SetValue(IsActivatedName, value); }
        }

        public bool? IsContactable
        {
            get { return GetValue<bool?>(IsContactableName); }
            set { SetValue(IsContactableName, value); }
        }

        public bool IncludeRelocating
        {
            get { return GetValue<bool>(IncludeRelocatingName); }
            set { SetValue(IncludeRelocatingName, value); }
        }

        public bool IncludeInternational
        {
            get { return GetValue<bool>(IncludeInternationalName); }
            set { SetValue(IncludeInternationalName, value); }
        }

        public int? Distance
        {
            get { return GetValue<int?>(DistanceName); }
            set { SetValue(DistanceName, value); }
        }

        public bool IncludeSimilarNames
        {
            get { return GetValue<bool>(IncludeSimilarNamesName); }
            set { SetValue(IncludeSimilarNamesName, value); }
        }

        public string Name
        {
            get { return GetValue<string>(NameName); }
            set { SetValue(NameName, value); }
        }

        public JobsToSearch JobTitlesToSearch
        {
            get
            {
                var value = GetValue<JobsToSearch?>(JobTitlesToSearchName);
                return value == null ? DefaultJobTitlesToSearch : value.Value;
            }
            set
            {
                SetValue(JobTitlesToSearchName, value);
            }
        }

        public string CompanyKeywords
        {
            get
            {
                return GetValue<string>(CompanyKeywordsName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(CompanyKeywordsName, value);
                CompanyKeywordsExpression = Expression.Parse(value);
            }
        }

        public IExpression CompanyKeywordsExpression { get; private set; }

        public JobsToSearch CompaniesToSearch
        {
            get
            {
                var value = GetValue<JobsToSearch?>(CompaniesToSearchName);
                return value == null ? DefaultCompaniesToSearch : value.Value;
            }
            set
            {
                SetValue(CompaniesToSearchName, value);
            }
        }

        public string EducationKeywords
        {
            get
            {
                return GetValue<string>(EducationKeywordsName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(EducationKeywordsName, value);
                EducationKeywordsExpression = Expression.Parse(value);
            }
        }

        public IExpression EducationKeywordsExpression { get; private set; }

        public string DesiredJobTitle
        {
            get
            {
                return GetValue<string>(DesiredJobTitleName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(DesiredJobTitleName, value);
                DesiredJobTitleExpression = Expression.Parse(value);
            }
        }

        public IExpression DesiredJobTitleExpression { get; private set; }

        public JobTypes JobTypes
        {
            get
            {
                return GetValue<JobTypes>(JobTypesName);
            }
            set
            {
                value = value == JobTypes.None ? JobTypes.All : value;
                SetValue(JobTypesName, value);
            }
        }

        public CandidateStatusFlags? CandidateStatusFlags
        {
            get
            {
                return GetValue<CandidateStatusFlags?>(CandidateFlagsName);
            }
            set
            {
                value = value == Domain.CandidateStatusFlags.All || value == default(CandidateStatusFlags) ? null : value;
                SetValue(CandidateFlagsName, value);
            }
        }

        public EthnicStatus? EthnicStatus
        {
            get
            {
                return GetValue<EthnicStatus?>(EthnicStatusName);
            }
            set
            {
                value = value == default(EthnicStatus) ? null : value;
                SetValue(EthnicStatusName, value);
            }
        }

        public VisaStatusFlags? VisaStatusFlags
        {
            get
            {
                return GetValue<VisaStatusFlags?>(VisaStatusFlagsName);
            }
            set
            {
                value = value == Domain.VisaStatusFlags.All || value == default(VisaStatusFlags) ? null : value;
                SetValue(VisaStatusFlagsName, value);
            }
        }

        public string AllKeywords
        {
            get { return _allKeywords; }
        }

        public string ExactPhrase
        {
            get { return _exactPhrase; }
        }

        public string AnyKeywords
        {
            get { return _anyKeywords; }
        }

        public string WithoutKeywords
        {
            get { return _withoutKeywords; }
        }

        internal string Keywords
        {
            get { return GetValue<string>(KeywordsName); }
            set { SetValue(KeywordsName, value); }
        }

        public void SetKeywords(string value)
        {
            value = value.NullIfEmpty();
            KeywordsExpression = SplitKeywords(false, value, out _allKeywords, out _exactPhrase, out _anyKeywords, out _withoutKeywords);
        }

        public void SetKeywords(string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords)
        {
            _allKeywords = allKeywords = allKeywords.NullIfEmpty();
            _exactPhrase = exactPhrase = exactPhrase.NullIfEmpty();
            _anyKeywords = anyKeywords = anyKeywords.NullIfEmpty();
            _withoutKeywords = withoutKeywords = withoutKeywords.NullIfEmpty();
            KeywordsExpression = CombineKeywords(false, allKeywords, exactPhrase, anyKeywords, withoutKeywords);
        }

        public string GetKeywords()
        {
            return KeywordsExpression == null ? null : CombineKeywords(false, _allKeywords, _exactPhrase, _anyKeywords, _withoutKeywords).GetUserExpression();
        }

        public bool? InFolder
        {
            get { return GetValue<bool?>(InFolderName); }
            set { SetValue(InFolderName, value); }
        }

        public bool? IsFlagged
        {
            get { return GetValue<bool?>(IsFlaggedName); }
            set { SetValue(IsFlaggedName, value); }
        }

        public bool? HasNotes
        {
            get { return GetValue<bool?>(HasNotesName); }
            set { SetValue(HasNotesName, value); }
        }

        public bool? HasViewed
        {
            get { return GetValue<bool?>(HasViewedName); }
            set { SetValue(HasViewedName, value); }
        }

        public bool? IsUnlocked
        {
            get { return GetValue<bool?>(IsUnlockedName); }
            set { SetValue(IsUnlockedName, value); }
        }

        public int EffectiveDistance
        {
            get
            {
                return Location == null
                    ? DefaultDistance
                    : Distance == null
                        ? (Location.GeographicalArea != null && (Location.GeographicalArea is CountrySubdivision || Location.GeographicalArea is Region))
                            ? DefaultRegionalDistance
                            : DefaultDistance
                        : Distance.Value;
            }
        }

        public JobsToSearch EffectiveJobTitlesToSearch
        {
            get { return JobTitlesToSearch; }
        }

        public bool IsEmpty
        {
            get
            {
                return KeywordsExpression == null
                       && JobTitleExpression == null
                       && Location == null
                       && CommunityId == null
                       && Salary == null
                       && (IndustryIds == null || IndustryIds.Count == 0)
                       && Recency == null
                       && IncludeSynonyms == DefaultIncludeSynonyms
                       && HasResume == DefaultHasResume
                       && IsActivated == DefaultIsActivated
                       && IsContactable == DefaultIsContactable
                       && IncludeRelocating == DefaultIncludeRelocating
                       && IncludeInternational == DefaultIncludeInternational
                       && (Distance == null || Distance == DefaultDistance)
                       && JobTitlesToSearch == DefaultJobTitlesToSearch
                       && CompanyKeywordsExpression == null
                       && CompaniesToSearch == DefaultCompaniesToSearch
                       && EducationKeywordsExpression == null
                       && DesiredJobTitleExpression == null
                       && JobTypes == DefaultJobTypes
                       && CandidateStatusFlags == null
                       && EthnicStatus == null
                       && VisaStatusFlags == null
                       && InFolder == null
                       && IsFlagged == null
                       && HasNotes == null
                       && HasViewed == null
                       && IsUnlocked == null
                       && IncludeSimilarNames == DefaultIncludeSimilarNames
                       && Name == null;
            }
        }

        protected override void OnCloning()
        {
            base.OnCloning();

            var expression = CombineKeywords(false, _allKeywords, _exactPhrase, _anyKeywords, _withoutKeywords);
            var keywords = expression == null ? null : expression.GetUserExpression();
            SetValue(KeywordsName, keywords);
        }

        protected override void OnCloned()
        {
            base.OnCloned();

            var keywords = GetValue<string>(KeywordsName);
            SetKeywords(keywords);

            CompanyKeywordsExpression = Expression.Parse(CompanyKeywords);
            EducationKeywordsExpression = Expression.Parse(EducationKeywords);
            DesiredJobTitleExpression = Expression.Parse(DesiredJobTitle);

            _locationReference = _locationReference == null ? null : _locationReference.Clone();
            JobTitleExpression = Expression.Parse(JobTitle);
        }

        public override bool Equals(object obj)
        {
            var other = obj as MemberSearchCriteria;
            if (other == null)
                return false;

            return GetType() == other.GetType()
                   && Equals(KeywordsExpression, other.KeywordsExpression)
                   && Equals(JobTitleExpression, other.JobTitleExpression)
                   && Equals(Location, other.Location)
                   && CommunityId == other.CommunityId
                   && Equals(Salary, other.Salary)
                   && ExcludeNoSalary == other.ExcludeNoSalary
                   && IndustryIds.NullableCollectionEqual(other.IndustryIds)
                   && SortCriteria.SortOrder == other.SortCriteria.SortOrder
                   && SortCriteria.ReverseSortOrder == other.SortCriteria.ReverseSortOrder
                   && IncludeSynonyms == other.IncludeSynonyms
                   && HasResume == other.HasResume
                   && IsActivated == other.IsActivated
                   && IsContactable == other.IsContactable
                   && Equals(Recency, other.Recency)
                   && IncludeRelocating == other.IncludeRelocating
                   && IncludeInternational == other.IncludeInternational
                   && Equals(Distance, other.Distance)
                   && Equals(JobTitlesToSearch, other.JobTitlesToSearch)
                   && Equals(CompanyKeywordsExpression, other.CompanyKeywordsExpression)
                   && Equals(CompaniesToSearch, other.CompaniesToSearch)
                   && Equals(EducationKeywordsExpression, other.EducationKeywordsExpression)
                   && Equals(DesiredJobTitleExpression, other.DesiredJobTitleExpression)
                   && JobTypes == other.JobTypes
                   && CandidateStatusFlags == other.CandidateStatusFlags
                   && EthnicStatus == other.EthnicStatus
                   && VisaStatusFlags == other.VisaStatusFlags
                   && Equals(InFolder, other.InFolder)
                   && Equals(IsFlagged, other.IsFlagged)
                   && Equals(HasViewed, other.HasViewed)
                   && Equals(HasNotes, other.HasNotes)
                   && Equals(IsUnlocked, other.IsUnlocked)
                   && Equals(IncludeSimilarNames, other.IncludeSimilarNames)
                   && Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return new object[] { KeywordsExpression, JobTitleExpression, Location, Salary, CommunityId, Recency, HasResume, IsActivated, IsContactable, CompanyKeywordsExpression, EducationKeywords, DesiredJobTitleExpression, InFolder, IsFlagged, HasViewed, HasNotes, IsUnlocked, Name }.GetCollectionHashCode()
                ^ IndustryIds.GetCollectionHashCode()
                ^ ExcludeNoSalary.GetHashCode()
                ^ SortCriteria.SortOrder.GetHashCode()
                ^ SortCriteria.ReverseSortOrder.GetHashCode()
                ^ IncludeSynonyms.GetHashCode()
                ^ IncludeRelocating.GetHashCode()
                ^ IncludeInternational.GetHashCode()
                ^ Distance.GetHashCode()
                ^ JobTitlesToSearch.GetHashCode()
                ^ CompaniesToSearch.GetHashCode()
                ^ JobTypes.GetHashCode()
                ^ CandidateStatusFlags.GetHashCode()
                ^ EthnicStatus.GetHashCode()
                ^ VisaStatusFlags.GetHashCode()
                ^ IncludeSimilarNames.GetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            ToString(sb);
            return sb.ToString();
        }

        protected virtual void ToString(StringBuilder sb)
        {
            sb.AppendFormat(
                "{0}: job title: '{1}', job title expression: '{2}', location: '{3}', salary: {4}, exclude no salary: {5}, community id: '{6}', sort order: {7}, reverse sort order: {8}, include synonyms: {9}, has resume: {10}, is activated: {11}, is contactable {12}, keywords expression: '{13}', industry ids: {14}, recency: {15}, keywords: '{16}', company keywords expression: '{17}', company keywords: '{18}', education keywords expression: {19}, education keywords: {20}, jobs to search: {21}, companies to search: {22}, include relocating: {23}, include international: {24}, ideal job types: '{25}', candidate flags: '{26}', ethnic flags: '{27}', visa status flags: '{28}', desired job title: {29}, in folder: {30}, is flagged: {31}, has notes: {32}, has viewed: {33}, is unlocked: {34}, include similiar names: {35}, name: {36}.",
                GetType().Name,
                JobTitle,
                JobTitleExpression == null ? null : JobTitleExpression.GetRawExpression(),
                Location,
                Salary,
                ExcludeNoSalary,
                CommunityId == null ? string.Empty : CommunityId.ToString(),
                SortCriteria.SortOrder,
                SortCriteria.ReverseSortOrder,
                IncludeSynonyms,
                HasResume,
                IsActivated,
                IsContactable,
                KeywordsExpression == null ? null : KeywordsExpression.GetRawExpression(),
                IndustryIds == null ? null : string.Join(", ", (from i in IndustryIds select i.ToString()).ToArray()),
                Recency,
                GetKeywords(),
                CompanyKeywordsExpression == null ? null : CompanyKeywordsExpression.GetRawExpression(),
                CompanyKeywords,
                EducationKeywordsExpression == null ? null : EducationKeywordsExpression.GetUserExpression(),
                EducationKeywords,
                JobTitlesToSearch,
                CompaniesToSearch,
                IncludeRelocating,
                IncludeInternational,
                JobTypes,
                CandidateStatusFlags,
                EthnicStatus,
                VisaStatusFlags,
                DesiredJobTitleExpression == null ? null : DesiredJobTitleExpression.GetUserExpression(),
                InFolder,
                IsFlagged,
                HasNotes,
                HasViewed,
                IsUnlocked,
                IncludeSimilarNames,
                Name);
        }

        public MemberSearchQuery GetSearchQuery(Range range)
        {
            return new MemberSearchQuery
            {
                SortOrder = SortCriteria.SortOrder,
                ReverseSortOrder = SortCriteria.ReverseSortOrder,

                Skip = range == null ? 0 : range.Skip,
                Take = range == null ? null : range.Take,

                Distance = EffectiveDistance,
                JobTitlesToSearch = EffectiveJobTitlesToSearch,
                JobTitle = JobTitleExpression,
                Keywords = KeywordsExpression,
                CommunityId = CommunityId,
                Location = Location,
                IndustryIds = IndustryIds,
                Salary = Salary,
                Recency = Recency,
                ExcludeNoSalary = ExcludeNoSalary,
                IncludeSynonyms = IncludeSynonyms,
                HasResume = HasResume,
                IsActivated = IsActivated,
                IsContactable = IsContactable,
                CompanyKeywords = CompanyKeywordsExpression,
                CompaniesToSearch = CompaniesToSearch,
                EducationKeywords = EducationKeywordsExpression,
                DesiredJobTitle = DesiredJobTitleExpression,
                IncludeRelocating = IncludeRelocating,
                IncludeInternational = IncludeInternational,
                DesiredJobTypes = JobTypes,
                CandidateStatusList = GetCandidateStatusList(CandidateStatusFlags),
                EthnicStatus = EthnicStatus,
                VisaStatusList = GetVisaStatusList(VisaStatusFlags),
                InFolder = InFolder,
                IsFlagged = IsFlagged,
                HasNotes = HasNotes,
                HasViewed = HasViewed,
                IsUnlocked = IsUnlocked,
                IncludeSimilarNames = IncludeSimilarNames,
                Name = Name,
            };
        }

        private static IList<CandidateStatus> GetCandidateStatusList(CandidateStatusFlags? flags)
        {
            if (flags == null)
                return null;

            var list = new List<CandidateStatus>();

            if ((flags & Domain.CandidateStatusFlags.AvailableNow) != 0)
                list.Add(CandidateStatus.AvailableNow);

            if ((flags & Domain.CandidateStatusFlags.ActivelyLooking) != 0)
                list.Add(CandidateStatus.ActivelyLooking);

            if ((flags & Domain.CandidateStatusFlags.OpenToOffers) != 0)
                list.Add(CandidateStatus.OpenToOffers);

            if ((flags & Domain.CandidateStatusFlags.NotLooking) != 0)
                list.Add(CandidateStatus.NotLooking);

            if ((flags & Domain.CandidateStatusFlags.Unspecified) != 0)
                list.Add(CandidateStatus.Unspecified);

            return list;
        }

        private static IList<VisaStatus> GetVisaStatusList(VisaStatusFlags? flags)
        {
            if (flags == null)
                return null;

            var list = new List<VisaStatus>();

            if ((flags & Domain.VisaStatusFlags.Citizen) != 0)
                list.Add(VisaStatus.Citizen);

            if ((flags & Domain.VisaStatusFlags.NoWorkVisa) != 0)
                list.Add(VisaStatus.NoWorkVisa);

            if ((flags & Domain.VisaStatusFlags.RestrictedWorkVisa) != 0)
                list.Add(VisaStatus.RestrictedWorkVisa);

            if ((flags & Domain.VisaStatusFlags.UnrestrictedWorkVisa) != 0)
                list.Add(VisaStatus.UnrestrictedWorkVisa);

            return list;
        }
    }
}