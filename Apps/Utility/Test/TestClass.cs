namespace LinkMe.Apps.Utility.Test
{
    public abstract class TestClass
    {
        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Framework.Utility.Unity.Container.Current.Resolve<T>();
        }

        protected static T Resolve<T>(string name)
        {
            TestAssembly.InitialiseContainer();
            return Framework.Utility.Unity.Container.Current.Resolve<T>(name);
        }
    }
}
