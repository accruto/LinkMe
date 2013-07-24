using System.Collections.Generic;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain;
using LinkMe.Framework.Utility;
using org.apache.lucene.document;
using org.apache.lucene.search;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine
{
    internal class JobTypesFieldHandler
    {
        private static readonly JobTypes[] AllJobTypes = new[]
        {
            JobTypes.FullTime,
            JobTypes.PartTime,
            JobTypes.Contract,
            JobTypes.Temp,
            JobTypes.JobShare
        };

        private static readonly JobTypes[] DefinedJobTypesOrder = new[]
        {
            JobTypes.FullTime,
            JobTypes.PartTime,
            JobTypes.Contract,
            JobTypes.Temp,
            JobTypes.JobShare
        };


        private readonly string _fieldName;
        private readonly IBooster _booster;
        private readonly string _sortFieldName;
        private readonly bool _supportSorting;

        protected JobTypesFieldHandler(string fieldName, string sortFieldName, IBooster booster)
        {
            _fieldName = fieldName;
            _booster = booster;

            if (!string.IsNullOrEmpty(sortFieldName))
            {
                _sortFieldName = sortFieldName;
                _supportSorting = true;
            }
        }

        protected void AddContent(Document document, JobTypes jobTypes)
        {
            // Add each value for search/filter purposes
            foreach (var jobType in Split(jobTypes))
            {
                var value = Encode(jobType);
                var field = new Field(_fieldName, value, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                _booster.SetBoost(field);
                document.add(field);
            }

            if (_supportSorting)
            {
                //Now add the "highest" one for sort purposes

                var sortValue = 0;

                if (jobTypes == JobTypes.None)
                {
                    //special case for none - put them at the bottom
                    sortValue = int.MaxValue;
                } else if (jobTypes != JobTypes.All)
                {
                    //special case of all job types; default to whatever's first in the list (i.e. 0)
                    sortValue = DefinedJobTypesOrder.Select((jobType, index) => new { JobType = jobType, Index = index }).Where(j => jobTypes.IsFlagSet(j.JobType)).Min(j => j.Index);
                }

                document.add(new NumericField(_sortFieldName).setIntValue(sortValue));
            }
       }

        protected BrowseSelection GetSelection(JobTypes? jobTypes)
        {
            if (!jobTypes.HasValue || jobTypes.Value == JobTypes.None || jobTypes.Value == JobTypes.All)
                return null;

            var selection = new BrowseSelection(_fieldName);
            selection.setSelectionOperation(BrowseSelection.ValueOperation.ValueOperationOr);

            foreach (var jobType in Split(jobTypes.Value))
                selection.addValue(Encode(jobType));

            return selection;
        }

        protected Sort GetSort(bool reverse)
        {
            return new Sort(new[] 
            {
                new SortField(_sortFieldName, SortField.INT, reverse),
                SortField.FIELD_SCORE
            });
        }

        private static string Encode(JobTypes status)
        {
            return NumericUtils.intToPrefixCoded((int)status);
        }

        public static JobTypes Decode(string status)
        {
            return (JobTypes)NumericUtils.prefixCodedToInt(status);
        }

        private static IEnumerable<JobTypes> Split(JobTypes flags)
        {
            return AllJobTypes.Where(jobType => (flags & jobType) != 0);
        }
    }
}