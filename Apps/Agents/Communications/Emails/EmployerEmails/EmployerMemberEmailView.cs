using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class EmployerMemberEmailView
    {
        private const int MaximumLocationLength = 40;
        private const int MaximumJobFieldsLength = 40;

        private string _location;
        private string _currentJobTitle;
        private string _currentEmployer;
        private string _previousJobTitle;
        private string _previousEmployer;
        private string _idealSalary;

        public Guid CandidateId { get; set; }
        public bool IsNew { get; set; }

        public string Location
        {
            get { return TextUtil.TruncateForDisplay(_location, MaximumLocationLength) ?? string.Empty; }
            set { _location = value; }
        }

        public string CurrentJobTitle
        {
            get { return TextUtil.TruncateForDisplay(_currentJobTitle, MaximumJobFieldsLength) ?? string.Empty; }
            set { _currentJobTitle = value; }
        }

        public string CurrentEmployer
        {
            get { return TextUtil.TruncateForDisplay(_currentEmployer, MaximumJobFieldsLength) ?? string.Empty; }
            set { _currentEmployer = value; }
        }

        public string PreviousJobTitle
        {
            get { return TextUtil.TruncateForDisplay(_previousJobTitle, MaximumJobFieldsLength) ?? string.Empty; }
            set { _previousJobTitle = value; }
        }

        public string PreviousEmployer
        {
            get { return TextUtil.TruncateForDisplay(_previousEmployer, MaximumJobFieldsLength) ?? string.Empty; }
            set { _previousEmployer = value; }
        }

        public string IdealSalary
        {
            get { return _idealSalary ?? string.Empty; }
            set { _idealSalary = value; }
        }
    }

    public class EmployerMemberEmailViews
        : Dictionary<Guid, EmployerMemberEmailView>
    {
    }
}