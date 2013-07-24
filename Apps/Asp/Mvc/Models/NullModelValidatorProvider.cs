using System.Collections.Generic;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Models
{
    public class NullModelValidatorProvider
        : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelValidator[0];
        }
    }
}
