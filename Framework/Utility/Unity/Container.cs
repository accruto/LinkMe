using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace LinkMe.Framework.Utility.Unity
{
    public static class Container
    {
        private static IUnityContainer _current = new UnityContainer();
        private static readonly Stack<IUnityContainer> ContainerStack = new Stack<IUnityContainer>();

        public static IUnityContainer Current
        {
            get { return _current; }
        }

        public static IUnityContainer Push()
        {
            ContainerStack.Push(_current);
            _current = _current.CreateChildContainer();
            return _current;
        }

        public static IUnityContainer Pop()
        {
            _current = ContainerStack.Pop();
            return _current;
        }
    }
}
