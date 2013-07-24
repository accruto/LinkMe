using System.Web.Mvc;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public class ModelBinderErrors
        : IDeconverterErrors
    {
        private readonly ModelBindingContext _bindingContext;
        private readonly IErrorHandler _errorHandler;

        public ModelBinderErrors(ModelBindingContext bindingContext, IErrorHandler errorHandler)
        {
            _bindingContext = bindingContext;
            _errorHandler = errorHandler;
        }

        void IDeconverterErrors.AddError(UserException exception)
        {
            _bindingContext.ModelState.AddModelError(exception, _errorHandler);
        }

        void IDeconverterErrors.AddError(ValidationError error)
        {
            _bindingContext.ModelState.AddModelError(error, _errorHandler);
        }
    }
}
