using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Domain.Test
{
    public abstract class TestClass
    {
        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }
    }
}
