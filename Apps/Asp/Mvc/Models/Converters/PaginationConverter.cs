using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Asp.Mvc.Models.Converters
{
    public class PaginationConverter
        : Converter<Pagination>
    {
        private readonly int? _defaultItems;

        public PaginationConverter(int? defaultItems)
        {
            _defaultItems = defaultItems;
        }

        public override void Convert(Pagination pagination, ISetValues values)
        {
            if (pagination == null)
                return;
            if (pagination.Page != null && pagination.Page.Value != 1)
                values.SetValue(PaginationKeys.Page, pagination.Page);

            if (pagination.Items != null)
            {
                if (_defaultItems == null || pagination.Items.Value != _defaultItems.Value)
                    values.SetValue(PaginationKeys.Items, pagination.Items);
            }
        }

        public override Pagination Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var page = values.GetIntValue(PaginationKeys.Page);
            var items = values.GetIntValue(PaginationKeys.Items);

            if (page != null)
            {
                if (page.Value <= 0)
                    page = null;
            }

            if (items != null)
            {
                if (_defaultItems != null && items.Value == _defaultItems.Value)
                    items = null;
            }

            if (page == null && items == null)
                return null;
            return new Pagination { Page = page, Items = items };
        }
    }
}