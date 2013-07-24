using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Users.Members.Status
{
    public enum MemberItem
    {
        Name,
        DesiredJob,
        DesiredSalary,
        Address,
        EmailAddress,
        PhoneNumber,
        Status,
        Objective,
        Industries,
        Jobs,
        Schools,
        RecentProfession,
        RecentSeniority,
        HighestEducationLevel,
        VisaStatus,
    }

    public class MemberItemStatus
    {
        public MemberItem Item { get; set; }
        public bool IsComplete { get; set; }
    }

    public class MemberStatus
    {
        private readonly IDictionary<MemberItem, MemberItemStatus> _items;

        public MemberStatus()
        {
            _items = (from i in (MemberItem[]) Enum.GetValues(typeof (MemberItem)) select new MemberItemStatus {Item = i, IsComplete = false}).ToDictionary(x => x.Item, x => x);
        }

        internal MemberStatus(IDictionary<MemberItem, MemberItemStatus> items)
        {
            _items = items;
        }

        public bool IsNameComplete
        {
            get { return _items[MemberItem.Name].IsComplete; }
            set { _items[MemberItem.Name].IsComplete = value; }
        }

        public bool IsDesiredJobComplete
        {
            get { return _items[MemberItem.DesiredJob].IsComplete; }
            set { _items[MemberItem.DesiredJob].IsComplete = value; }
        }

        public bool IsDesiredSalaryComplete
        {
            get { return _items[MemberItem.DesiredSalary].IsComplete; }
            set { _items[MemberItem.DesiredSalary].IsComplete = value; }
        }

        public bool IsAddressComplete
        {
            get { return _items[MemberItem.Address].IsComplete; }
            set { _items[MemberItem.Address].IsComplete = value; }
        }

        public bool IsEmailAddressComplete
        {
            get { return _items[MemberItem.EmailAddress].IsComplete; }
            set { _items[MemberItem.EmailAddress].IsComplete = value; }
        }

        public bool IsPhoneNumberComplete
        {
            get { return _items[MemberItem.PhoneNumber].IsComplete; }
            set { _items[MemberItem.PhoneNumber].IsComplete = value; }
        }

        public bool IsStatusComplete
        {
            get { return _items[MemberItem.Status].IsComplete; }
            set { _items[MemberItem.Status].IsComplete = value; }
        }

        public bool IsObjectiveComplete
        {
            get { return _items[MemberItem.Objective].IsComplete; }
            set { _items[MemberItem.Objective].IsComplete = value; }
        }

        public bool IsIndustriesComplete
        {
            get { return _items[MemberItem.Industries].IsComplete; }
            set { _items[MemberItem.Industries].IsComplete = value; }
        }

        public bool IsJobsComplete
        {
            get { return _items[MemberItem.Jobs].IsComplete; }
            set { _items[MemberItem.Jobs].IsComplete = value; }
        }

        public bool IsSchoolsComplete
        {
            get { return _items[MemberItem.Schools].IsComplete; }
            set { _items[MemberItem.Schools].IsComplete = value; }
        }

        public bool IsRecentProfessionComplete
        {
            get { return _items[MemberItem.RecentProfession].IsComplete; }
            set { _items[MemberItem.RecentProfession].IsComplete = value; }
        }

        public bool IsRecentSeniorityComplete
        {
            get { return _items[MemberItem.RecentSeniority].IsComplete; }
            set { _items[MemberItem.RecentSeniority].IsComplete = value; }
        }

        public bool IsHighestEducationLevelComplete
        {
            get { return _items[MemberItem.HighestEducationLevel].IsComplete; }
            set { _items[MemberItem.HighestEducationLevel].IsComplete = value; }
        }

        public bool IsVisaStatusComplete
        {
            get { return _items[MemberItem.VisaStatus].IsComplete; }
            set { _items[MemberItem.VisaStatus].IsComplete = value; }
        }

        public IEnumerator<MemberItemStatus> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        public IEnumerable<MemberItemStatus> AsEnumerable()
        {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}