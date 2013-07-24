using System;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class CareerObjectivesMemberModelConverter
        : Converter<CareerObjectivesMemberModel>
    {
        public override void Convert(CareerObjectivesMemberModel obj, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override CareerObjectivesMemberModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new CareerObjectivesMemberModel
            {
                Objective = values.GetStringValue(CareerObjectivesKeys.Objective),
                Summary = values.GetStringValue(CareerObjectivesKeys.Summary),
                Skills = values.GetStringValue(CareerObjectivesKeys.Skills),
            };
        }
    }
}