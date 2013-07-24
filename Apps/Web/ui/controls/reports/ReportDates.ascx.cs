using System;
using LinkMe.Domain;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Reports
{
    public partial class ReportDates : LinkMeUserControl
    {
        private const string Format = "yyyy-MM-dd";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.ReportDates);

            btnStartCalendar.OnClientClick = "return showCalendar('" + txtStart.ClientID + "', '" + Format + "');";
            btnEndCalendar.OnClientClick = "return showCalendar('" + txtEnd.ClientID + "', '" + Format + "');";
        }

        public bool Required
        {
            set
            {
                reqValStart.Enabled = value;
                reqValEnd.Enabled = value;
            }
        }

        public DateTimeRange GetPeriod()
        {
            DateTime? startDate = TryParse(txtStart.Text, "start date");
            DateTime? endDate = TryParse(txtEnd.Text, "end date");

            if (startDate == null || endDate == null)
                return null;

            if (endDate.Value < startDate.Value)
                throw new UserException("The end date you entered is before the start date.");

            return new DateTimeRange(startDate.Value, endDate.Value);
        }

        private static DateTime? TryParse(string text, string description)
        {
            if (TextUtil.TrimAndCheckEmpty(ref text))
                return null;

            try
            {
                return DateTime.Parse(text);
            }
            catch (Exception ex)
            {
                throw new UserException("The specified " + description + " is not a valid date.", ex);
            }
        }
    }
}