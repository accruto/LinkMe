using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;

namespace LinkMe.Web.Models.Binders
{
    public class CandidateSalaryBandBinder
        : BaseModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            IGetValues values = new ModelBinderValues(bindingContext);
            var salaryBand = values.GetStringValue(bindingContext.ModelName);
            if (string.IsNullOrEmpty(salaryBand))
                return null;

            decimal? lowerBand = null;
            decimal? upperBand = null;

            try
            {
                salaryBand = salaryBand.Replace("-candidates", string.Empty);

                if (salaryBand.StartsWith("up-to-", StringComparison.InvariantCultureIgnoreCase))
                    upperBand = ParseSalaryBand(salaryBand.Replace("up-to-", string.Empty));
                else if (salaryBand.EndsWith("-and-above", StringComparison.InvariantCultureIgnoreCase))
                    lowerBand = ParseSalaryBand(salaryBand.Replace("-and-above", string.Empty));
                else
                {
                    var bands = salaryBand.Split('-');

                    if (bands.Length == 2)
                    {
                        lowerBand = ParseSalaryBand(bands[0]);
                        upperBand = ParseSalaryBand(bands[1]);
                    }
                }
                
                return new Salary {LowerBound = lowerBand, UpperBound = upperBand};
            }
            catch (Exception)
            {
            }
            
            return null;
        }

        private static decimal ParseSalaryBand(string band)
        {
            band = band.Replace("k", string.Empty);

            return decimal.Parse(band) * 1000;
        }
    }
}
