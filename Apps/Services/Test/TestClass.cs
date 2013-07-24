using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Services.Test
{
    public abstract class TestClass
    {
        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }

        protected static T Resolve<T>(string name)
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>(name);
        }
    }
}