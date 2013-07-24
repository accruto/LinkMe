using LinkMe.Apps.Presentation.Converters;
using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Employers.Models
{
    public class CandidatesPresentationModelConverter
        : PresentationModelConverter<CandidatesPresentationModel>
    {
        private const DetailLevel DefaultDetailLevel = DetailLevel.Expanded;

        public override void Convert(CandidatesPresentationModel presentation, ISetValues values)
        {
            base.Convert(presentation, values);
            if (presentation == null)
                return;
            if (presentation.DetailLevel != default(DetailLevel))
                values.SetValue(PresentationKeys.DetailLevel, presentation.DetailLevel);
        }

        public override CandidatesPresentationModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var presentation = base.Deconvert(values, errors);
            var detailLevel = values.GetValue<DetailLevel>(PresentationKeys.DetailLevel);

            if (presentation == null && detailLevel == null)
                return null;

            presentation = presentation ?? new CandidatesPresentationModel();
            presentation.DetailLevel = detailLevel ?? DefaultDetailLevel;
            return presentation;
        }
    }
}
