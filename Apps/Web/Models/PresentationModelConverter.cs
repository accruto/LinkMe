using LinkMe.Apps.Asp.Mvc.Models.Converters;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Web.Models
{
    public abstract class PresentationModelConverter<TPresentationModel>
        : Converter<TPresentationModel>
        where TPresentationModel : PresentationModel, new()
    {
        public override void Convert(TPresentationModel presentation, ISetValues values)
        {
            if (presentation == null)
                return;
            new PaginationConverter(presentation.DefaultItemsPerPage).Convert(presentation.Pagination, values);
        }

        public override TPresentationModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var pagination = new PaginationConverter(null).Deconvert(values, errors);
            if (pagination == null)
                return null;

            return new TPresentationModel
            {
                Pagination = pagination,
            };
        }
    }
}
