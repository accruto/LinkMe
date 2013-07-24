using System.Data.Linq;

namespace LinkMe.Framework.Utility.Data.Linq
{
    public static class DataContextExtensions
    {
        public static T AsReadOnly<T>(this T dc)
            where T : DataContext
        {
            dc.ObjectTrackingEnabled = false;
            return dc;
        }
    }
}
