using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Credits
{
    public class ExercisedCredit
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
        public Guid CreditId { get; set; }
        public Guid? AllocationId { get; set; }
        public bool AdjustedAllocation { get; set; }
        public Guid ExercisedById { get; set; }
        public Guid? ExercisedOnId { get; set; }
        public Guid? ReferenceId { get; set; }
    }
}
