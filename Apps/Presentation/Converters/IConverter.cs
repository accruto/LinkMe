using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Presentation.Converters
{
    public interface IDeconverterErrors
    {
        void AddError(UserException exception);
        void AddError(ValidationError error);
    }

    public interface IDeconverter
    {
        object Deconvert(IGetValues values, IDeconverterErrors errors);
    }

    public interface IConverter
    {
        void Convert(object obj, ISetValues values);
    }

    public interface IConverter<T>
        : IDeconverter, IConverter
    {
        void Convert(T obj, ISetValues values);
        new T Deconvert(IGetValues values, IDeconverterErrors errors);
    }

    public abstract class Converter<T>
        : IConverter<T>
        where T : class
    {
        public abstract void Convert(T obj, ISetValues values);
        public abstract T Deconvert(IGetValues values, IDeconverterErrors errors);

        void IConverter.Convert(object obj, ISetValues values)
        {
            Convert(obj as T, values);
        }

        object IDeconverter.Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return Deconvert(values, errors);
        }
    }
}