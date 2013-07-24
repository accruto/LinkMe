using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Query.Search.Engine.Test
{
    public abstract class TestClass
    {
        protected static T Resolve<T>(string name)
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>(name);
        }

        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }
    }
}
