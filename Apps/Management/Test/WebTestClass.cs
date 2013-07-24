using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Management.Test
{
    public class WebTestClass
        : Asp.Test.WebTestClass
    {
        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }
    }
}
