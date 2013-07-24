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
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSearchSortCriteria
    {
        public JobAdSortOrder SortOrder { get; set; }
        public bool ReverseSortOrder { get; set; }
    }

    public class JobAdSearchCriteria
        : SearchCriteria, ISearchCriteriaLocation, ISearchCriteriaIndustries, ICanBeEmpty
    {
        public const int DefaultDistance = 50;
        public const int DefaultRegionalDistance = 0;
        public const JobAdSortOrder DefaultSortOrder = JobAdSortOrder.Relevance;
        public const int MaxRecency = 60;
        public const bool DefaultIncludeSynonyms = true;

        public const int DefaultRecency = 30;
        public const int MinSalary = 0;
        public const int MaxSalary = 250000;
        public const int StepSalary = 5000;
        public const int MinHourlySalary = 0;
        public const int MaxHourlySalary = 125;
        public const int StepHourlySalary = 5;

        public const bool DefaultExcludeNoSalary = false;
        public const SalaryRate DefaultSalaryRate = SalaryRate.Year;
        public const JobTypes DefaultJobTypes = JobTypes.All;

        private const string AdTitleName = "AdTitle";
        private const string SortOrderName = "SortOrder";
        private const string ReverseSortOrderName = "ReverseSortOrder";
        private const string IncludeSynonymsName = "IncludeSynonyms";
        private const string CountryName = "Country";
        private const string LocationName = "Location";
        private const string AreaName = "Area";
        private const string PostcodeName = "Postcode";
        private const string StateName = "State";
        private const string CommunityIdName = "CommunityId";
        private const string CommunityOnlyName = "CommunityOnly";
        private const string RecencyName = "Recency";
        private const string AdvertiserNameName = "Advertiser";
        private const string SalaryLowerBoundName = "MinSalary";
        private const string SalaryUpperBoundName = "MaxSalary";
        private const string SalaryRateName = "SalaryRate";
        private const string ExcludeNoSalaryName = "ExcludeNoSalary";
        private const string DistanceName = "Distance";
        private const string JobTypesName = "JobTypes";
        private const string IndustryIdsName = "IndustryIds";
        private const string IndustriesName = "Industries";
        private const string IndustryIdName = "IndustryId";
        private const string KeywordsName = "Keywords";
        private const string IsFlaggedName = "IsFlagged";
        private const string HasNotesName = "HasNotes";
        private const string HasViewedName = "HasViewed";
        private const string HasAppliedName = "HasApplied";

        private LocationReference _locationReference;
        private string _allKeywords;
        private string _exactPhrase;
        private string _anyKeywords;
        private string _withoutKeywords;

        private static readonly IDictionary<string, CriteriaDescription> Descriptions = new Dictionary<string, CriteriaDescription>
        {
            {AdTitleName, new CriteriaValueDescription<string>(null)},
            {SortOrderName, new CriteriaValueDescription<JobAdSortOrder>(DefaultSortOrder, new EnumValuePersister<JobAdSortOrder>())},
            {ReverseSortOrderName, new CriteriaValueDescription<bool>(false, new BoolValuePersister())},
            {IncludeSynonymsName, new CriteriaValueDescription<bool>(DefaultIncludeSynonyms, new BoolValuePersister())},
            {CountryName, new CriteriaValueDescription<int?>(null, new IntValuePersister())},
            {LocationName, new CriteriaValueDescription<string>(null)},
            {AreaName, new CriteriaValueDescription<int?>(null, new IntValuePersister())},
            {PostcodeName, new CriteriaValueDescription<string>(null)},
            {StateName, new CriteriaValueDescription<string>(null)},
            {CommunityIdName, new CriteriaValueDescription<Guid?>(null, new GuidValuePersister())},
            {CommunityOnlyName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {RecencyName, new CriteriaValueDescription<TimeSpan?>(null, new TimeSpanValuePersister())},
            {AdvertiserNameName, new CriteriaValueDescription<string>(null)},
            {SalaryLowerBoundName, new CriteriaValueDescription<decimal?>(null, new DecimalValuePersister())},
            {SalaryUpperBoundName, new CriteriaValueDescription<decimal?>(null, new DecimalValuePersister())},
            {SalaryRateName, new CriteriaValueDescription<SalaryRate>(DefaultSalaryRate, new EnumValuePersister<SalaryRate>())},
            {ExcludeNoSalaryName, new CriteriaValueDescription<bool>(DefaultExcludeNoSalary, new BoolValuePersister())},
            {DistanceName, new CriteriaValueDescription<int?>(null, new IntValuePersister())},
            {JobTypesName, new CriteriaValueDescription<JobTypes>(JobTypes.All, new EnumValuePersister<JobTypes>())},
            {IndustryIdsName, new CriteriaListDescription<Guid>(g => g.ToString(), s => new Guid(s))},
            {IndustriesName, new CriteriaValueDescription<string>(null)},
            {IndustryIdName, new CriteriaValueDescription<Guid?>(null, new GuidValuePersister())},
            {KeywordsName, new CriteriaValueDescription<string>(null)},
            {IsFlaggedName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {HasNotesName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {HasViewedName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
            {HasAppliedName, new CriteriaValueDescription<bool?>(null, new BoolValuePersister())},
        };

        private static readonly IDictionary<string, CriteriaDescription> CombinedDescriptions = SearchCriteria.Combine(Descriptions);

        protected new static IDictionary<string, CriteriaDescription> Combine(IDictionary<string, CriteriaDescription> descriptions)
        {
            return Combine(CombinedDescriptions, descriptions);
        }

        public JobAdSearchCriteria()
            : base(CombinedDescriptions)
        {
            SortCriteria = new JobAdSearchSortCriteria { SortOrder = DefaultSortOrder, ReverseSortOrder = false };
            JobTypes = DefaultJobTypes;
        }

        public string AdTitle
        {
            get
            {
                return GetValue<string>(AdTitleName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(AdTitleName, value);
                AdTitleExpression = Expression.Parse(value, ModificationFlags.AllowShingling);
            }
        }

        public IExpression AdTitleExpression { get; private set; }

        public IExpression KeywordsExpression { get; protected set; }

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
                    SetValue(LocationName, value.ToString());
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

        public bool? CommunityOnly
        {
            get { return GetValue<bool?>(CommunityOnlyName); }
            set { SetValue(CommunityOnlyName, value); }
        }

        public TimeSpan? Recency
        {
            get { return GetValue<TimeSpan?>(RecencyName); }
            set { SetValue(RecencyName, value); }
        }

        public JobAdSearchSortCriteria SortCriteria
        {
            get
            {
                return new JobAdSearchSortCriteria
                {
                    SortOrder = GetValue<JobAdSortOrder>(SortOrderName),
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

        public IExpression AdvertiserNameExpression { get; private set; }

        public string AdvertiserName
        {
            get
            {
                return GetValue<string>(AdvertiserNameName);
            }
            set
            {
                value = value.NullIfEmpty();
                SetValue(AdvertiserNameName, value);
                AdvertiserNameExpression = Expression.Parse(value);
            }
        }

        public Salary Salary
        {
            get
            {
                var lowerBound = GetValue<decimal?>(SalaryLowerBoundName);
                var upperBound = GetValue<decimal?>(SalaryUpperBoundName);
                var rate = GetValue<SalaryRate>(SalaryRateName);

                if (lowerBound == null && upperBound == null)
                    return null;

                return new Salary
                {
                    LowerBound = lowerBound,
                    UpperBound = upperBound,
                    Rate = rate,
                    Currency = Currency.AUD,
                };
            }
            set
            {
                SetValue(SalaryLowerBoundName, value == null ? null : value.LowerBound);
                SetValue(SalaryUpperBoundName, value == null ? null : value.UpperBound);
                SetValue(SalaryRateName, value == null ? DefaultSalaryRate : value.Rate);
            }
        }

        public bool ExcludeNoSalary
        {
            get { return GetValue<bool>(ExcludeNoSalaryName); }
            set { SetValue(ExcludeNoSalaryName, value); }
        }

        public int? Distance
        {
            get { return GetValue<int?>(DistanceName); }
            set { SetValue(DistanceName, value); }
        }

        public JobTypes JobTypes
        {
            get { return GetValue<JobTypes>(JobTypesName); }
            set { SetValue(JobTypesName, value); }
        }

        public IList<Guid> IndustryIds
        {
            get { return GetValue<IList<Guid>>(IndustryIdsName); }
            set { SetValue(IndustryIdsName, value); }
        }

        string ISearchCriteriaIndustries.Industries
        {
            get { return GetValue<string>(IndustriesName); }
        }

        Guid? ISearchCriteriaIndustries.IndustryId
        {
            get { return GetValue<Guid?>(IndustryIdName); }
        }

        public IList<LocationReference> Relocations { get; set; }

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
            KeywordsExpression = SplitKeywords(true, value, out _allKeywords, out _exactPhrase, out _anyKeywords, out _withoutKeywords);
        }

        public void SetKeywords(string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords)
        {
            _allKeywords = allKeywords = allKeywords.NullIfEmpty();
            _exactPhrase = exactPhrase = exactPhrase.NullIfEmpty();
            _anyKeywords = anyKeywords = anyKeywords.NullIfEmpty();
            _withoutKeywords = withoutKeywords = withoutKeywords.NullIfEmpty();
            KeywordsExpression = CombineKeywords(true, allKeywords, exactPhrase, anyKeywords, withoutKeywords);
        }

        public string GetKeywords()
        {
            if (KeywordsExpression == null)
                return null;

            // Build the expression again from the original keywords as the keywords may have been modified,
            // eg. by inserting synonyms.

            var originalKeywordsExpression = CombineKeywords(true, _allKeywords, _exactPhrase, _anyKeywords, _withoutKeywords);
            return originalKeywordsExpression == null ? null : originalKeywordsExpression.GetUserExpression();
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

        public bool? HasApplied
        {
            get { return GetValue<bool?>(HasAppliedName); }
            set { SetValue(HasAppliedName, value); }
        }

        public bool IsEmpty
        {
            get
            {
                return AdTitleExpression == null
                       && KeywordsExpression == null
                       && Location == null
                       && !CommunityId.HasValue
                       && IncludeSynonyms == DefaultIncludeSynonyms
                       && !Recency.HasValue
                       && AdvertiserNameExpression == null
                       && Salary == null
                       && (Distance == null || Distance == DefaultDistance)
                       && JobTypes == DefaultJobTypes
                       && (IndustryIds == null || IndustryIds.Count == 0)
                       && (Relocations == null || Relocations.Count == 0)
                       && IsFlagged == null
                       && HasNotes == null
                       && HasViewed == null
                       && HasApplied == null;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as JobAdSearchCriteria;
            if (other == null)
                return false;

            return base.Equals(obj)
                   && Equals(AdTitleExpression, other.AdTitleExpression)
                   && Equals(KeywordsExpression, other.KeywordsExpression)
                   && Equals(Location, other.Location)
                   && Equals(CommunityId, other.CommunityId)
                   && Equals(CommunityOnly, other.CommunityOnly)
                   && IncludeSynonyms == other.IncludeSynonyms
                   && Equals(Recency, other.Recency)
                   && SortCriteria.SortOrder == other.SortCriteria.SortOrder
                   && SortCriteria.ReverseSortOrder == other.SortCriteria.ReverseSortOrder
                   && Equals(AdvertiserNameExpression, other.AdvertiserNameExpression)
                   && Equals(Salary, other.Salary)
                   && ExcludeNoSalary == other.ExcludeNoSalary
                   && Equals(Distance, other.Distance)
                   && JobTypes == other.JobTypes
                   && IndustryIds.NullableCollectionEqual(other.IndustryIds)
                   && Relocations.NullableCollectionEqual(other.Relocations)
                   && Equals(IsFlagged, other.IsFlagged)
                   && Equals(HasViewed, other.HasViewed)
                   && Equals(HasNotes, other.HasNotes)
                   && Equals(HasApplied, other.HasApplied);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ new object[] { AdTitleExpression, KeywordsExpression, Location, CommunityId, CommunityOnly, Recency, AdvertiserNameExpression, Salary, Distance, IsFlagged, HasViewed, HasNotes, HasApplied }.GetCollectionHashCode()
                ^ SortCriteria.SortOrder.GetHashCode()
                ^ SortCriteria.ReverseSortOrder.GetHashCode()
                ^ IncludeSynonyms.GetHashCode()
                ^ JobTypes.GetHashCode()
                ^ IndustryIds.GetCollectionHashCode()
                ^ ExcludeNoSalary.GetHashCode()
                ^ Relocations.GetCollectionHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            ToString(sb);
            return sb.ToString();
        }

        protected void ToString(StringBuilder sb)
        {
            sb.AppendFormat(
                "{0}: ad title: '{1}', location: '{2}', community id: {3}, community only: {4}, include synonyms: {5}, recency: {6}, sort order: {7}, reverse sort order: {8}, keywords: '{9}', advertiser: '{10}', salary range: {11}, job types: {12}, distance: {13}, industry ids: {14}, is flagged: {15}, has viewed: {16}, has notes: {17}, has applied: {18}",
                GetType().Name,
                AdTitle,
                Location == null ? null : (Location.IsCountry ? Location.Country.Name : Location.ToString()),
                CommunityId.HasValue ? CommunityId.ToString() : String.Empty,
                CommunityOnly.HasValue ? CommunityOnly.Value.ToString() : String.Empty,
                IncludeSynonyms,
                Recency,
                SortCriteria.SortOrder,
                SortCriteria.ReverseSortOrder,
                GetKeywords(),
                AdvertiserNameExpression == null ? "" : AdvertiserNameExpression.GetUserExpression(),
                Salary == null ? "(none)" : Salary.ToString(),
                JobTypes,
                Distance,
                IndustryIds == null ? null : string.Join(", ", (from i in IndustryIds select i.ToString()).ToArray()),
                IsFlagged,
                HasViewed,
                HasNotes,
                HasApplied);
        }

        /// <summary>
        /// Returns true if the object contains criteria that can be used to meaningfully rank the results
        /// by "relevance", otherwise false.
        /// </summary>
        public bool CanSortByRelevance
        {
            get { return AdTitleExpression != null || KeywordsExpression != null; }
        }

        /// <summary>
        /// Returns true if the object contains any criteria that filter (restrict) the result set, false
        /// if it only specifies a sort order (ie. search with the criteria would return all job ads).
        /// </summary>
        public bool HasFilters
        {
            get
            {
                return AdTitleExpression != null
                    || KeywordsExpression != null
                    || (Location != null && Location.GeographicalArea != null)
                    || (CommunityOnly.HasValue && CommunityOnly.Value)
                    || (CommunityId.HasValue)
                    || (Recency.HasValue && Recency.Value.Days != MaxRecency)
                    || AdvertiserNameExpression != null
                    || Salary != null
                    || JobTypes != JobTypes.All
                    || IndustryIds != null
                    || IsFlagged != null
                    || HasViewed != null
                    || HasNotes != null;
            }
        }

        protected override void OnCloning()
        {
            base.OnCloning();

            var expression = CombineKeywords(true, _allKeywords, _exactPhrase, _anyKeywords, _withoutKeywords);
            var keywords = expression == null ? null : expression.GetUserExpression();
            SetValue(KeywordsName, keywords);
        }

        protected override void OnCloned()
        {
            base.OnCloned();
            _locationReference = _locationReference == null ? null : _locationReference.Clone();
            AdTitleExpression = Expression.Parse(AdTitle);

            var keywords = GetValue<string>(KeywordsName);
            SetKeywords(keywords);
            AdvertiserNameExpression = Expression.Parse(AdvertiserName);

            var relocations = Relocations;
            if (relocations != null)
            {
                Relocations = new List<LocationReference>();
                foreach (var relocation in relocations)
                    Relocations.Add(relocation.Clone());
            }
        }

        public JobAdSearchQuery GetSearchQuery(Range range)
        {
            return new JobAdSearchQuery
            {
                SortOrder = SortCriteria.SortOrder,
                ReverseSortOrder = SortCriteria.ReverseSortOrder,

                Skip = range == null ? 0 : range.Skip,
                Take = range == null ? null : range.Take,

                IncludeSynonyms = IncludeSynonyms,
                AdTitle = AdTitleExpression,
                Location = Location,
                Distance = EffectiveDistance,
                Keywords = KeywordsExpression,
                CommunityId = CommunityId,
                CommunityOnly = CommunityOnly,
                Recency = Recency,
                IndustryIds = IndustryIds,
                AdvertiserName = AdvertiserNameExpression,
                Salary = Salary,
                ExcludeNoSalary = ExcludeNoSalary,
                JobTypes = JobTypes,
                Relocations = Relocations,
                IsFlagged = IsFlagged,
                HasNotes = HasNotes,
                HasViewed = HasViewed,
                HasApplied = HasApplied,
            };
        }
    }
}