using System;
using System.Web.Mvc;
using LinkMe.Domain.Products;

namespace LinkMe.Apps.Asp.Mvc.Models.Binders
{
    public class CreditCardBinder
        : BaseModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var creditCard = new CreditCard();

            // Grab all values.

            string value;
            if (TryBind(controllerContext, bindingContext, "CardNumber", out value))
                creditCard.CardNumber = value;
            if (TryBind(controllerContext, bindingContext, "Cvv", out value))
                creditCard.Cvv = value;
            if (TryBind(controllerContext, bindingContext, "CardHolderName", out value))
                creditCard.CardHolderName = value;

            CreditCardType cardType;
            if (TryBindEnum(controllerContext, bindingContext, "CardType", out cardType))
                creditCard.CardType = cardType;

            int month;
            int year;
            if (TryBind(controllerContext, bindingContext, "ExpiryMonth", out month) && TryBind(controllerContext, bindingContext, "ExpiryYear", out year))
                creditCard.ExpiryDate = new ExpiryDate(year < 100 ? 2000 + year : year, month);

            return creditCard;
        }

        private bool TryBind<T>(ControllerContext controllerContext, ModelBindingContext bindingContext, string name, out T t)
        {
            t = default(T);
            var result = bindingContext.ValueProvider.GetValue(name);
            if (result == null)
                return false;

            // Put in model to cause control repopulation.

            AddToModel(controllerContext, name, result);

            // Get the value.

            t = (T)result.ConvertTo(typeof(T));
            return true;
        }

        private bool TryBindEnum<T>(ControllerContext controllerContext, ModelBindingContext bindingContext, string name, out T t)
        {
            t = default(T);
            var result = bindingContext.ValueProvider.GetValue(name);
            if (result == null)
                return false;

            // Put in model to cause control repopulation.

            AddToModel(controllerContext, name, result);

            // Get the value.

            if (result.RawValue is string)
                return TryBindEnum((string) result.RawValue, out t);
            if (result.RawValue is string[])
            {
                var array = (string[]) result.RawValue;
                if (array.Length == 1)
                    return TryBindEnum(array[0], out t);
            }
            return true;
        }

        private static bool TryBindEnum<T>(string value, out T t)
        {
            try
            {
                t = (T) Enum.Parse(typeof(T), value);
                return true;
            }
            catch (Exception)
            {
                t = default(T);
                return false;
            }
        }
    }
}
