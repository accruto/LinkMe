using System.Text;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Html
{
    public class MemberSortOrderField
        : EnumDropDownListField<MemberSearchSortCriteria, MemberSortOrder>
    {
        private readonly bool _isReversed;

        public MemberSortOrderField(HtmlHelper htmlHelper, MemberSearchSortCriteria criteria)
            : base(htmlHelper, criteria, m => m.SortOrder)
        {
            _isReversed = criteria.ReverseSortOrder;
            SetText(GetText);
        }

        private static string GetText(MemberSortOrder order)
        {
            switch (order)
            {
                case MemberSortOrder.DateUpdated:
                    return "Date updated";

                case MemberSortOrder.FirstName:
                    return "First name";

                default:
                    return order.ToString();
            }
        }

        protected override void BuildPostInnerHtml(StringBuilder sb)
        {
            base.BuildPostInnerHtml(sb);

            // Add radio buttons for ascending/descending.

            sb.Append("<div class=\"ascending\">");
            sb.Append(RadioButton(Name + "Direction", Name + "IsAscending", _isReversed, new { id = Name + "IsAscending" }));
            sb.Append("</div>");
            sb.Append("<div class=\"descending\">");
            sb.Append(RadioButton(Name + "Direction", Name + "IsDescending", !_isReversed, new { id = Name + "IsDescending" }));
            sb.Append("</div>");
        }
    }

    public static class MemberSortOrderFieldExtensions
    {
        public static MemberSortOrderField MemberSortOrderField(this HtmlHelper htmlHelper, MemberSearchSortCriteria criteria)
        {
            return new MemberSortOrderField(htmlHelper, criteria);
        }
    }
}
