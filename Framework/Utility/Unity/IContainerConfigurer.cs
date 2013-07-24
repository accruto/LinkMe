using System.Collections.Generic;
using LinkMe.Framework.Utility.PublishedEvents.Unity;
using Microsoft.Practices.Unity;

namespace LinkMe.Framework.Utility.Unity
{
    public interface IContainerConfigurer
    {
        void RegisterTypes(IUnityContainer container);
    }

    public interface IContainerInstanceConfigurer
    {
        void RegisterInstances(IUnityContainer container);
    }

    internal class SectionConfigurer
        : IContainerConfigurer
    {
        private readonly string _sectionName;

        public SectionConfigurer(string sectionName)
        {
            _sectionName = sectionName;
        }

        public void RegisterTypes(IUnityContainer container)
        {
            container.AddConfiguration(_sectionName);
        }
    }

    internal class TypeConfigurer<TFrom, TTo>
        : IContainerConfigurer
        where TTo : TFrom
    {
        private readonly string _name;
        private readonly LifetimeManager _lifetimeManager;
        private readonly InjectionMember[] _injectionMembers;

        public TypeConfigurer(string name, LifetimeManager lifetimeManager, InjectionMember[] injectionMembers)
        {
            _name = name;
            _lifetimeManager = lifetimeManager;
            _injectionMembers = injectionMembers;
        }

        public void RegisterTypes(IUnityContainer container)
        {
            if (!string.IsNullOrEmpty(_name))
            {
                if (_lifetimeManager != null)
                    container.RegisterType<TFrom, TTo>(_name, _lifetimeManager, _injectionMembers);
                else
                    container.RegisterType<TFrom, TTo>(_name, _injectionMembers);
            }
            else
            {
                if (_lifetimeManager != null)
                    container.RegisterType<TFrom, TTo>(_lifetimeManager, _injectionMembers);
                else
                    container.RegisterType<TFrom, TTo>(_injectionMembers);
            }
        }
    }

    internal class TypeConfigurer<TFrom>
        : IContainerConfigurer
    {
        private readonly string _name;
        private readonly LifetimeManager _lifetimeManager;
        private readonly InjectionMember[] _injectionMembers;

        public TypeConfigurer(string name, LifetimeManager lifetimeManager, InjectionMember[] injectionMembers)
        {
            _name = name;
            _lifetimeManager = lifetimeManager;
            _injectionMembers = injectionMembers;
        }

        public void RegisterTypes(IUnityContainer container)
        {
            if (!string.IsNullOrEmpty(_name))
            {
                if (_lifetimeManager != null)
                    container.RegisterType<TFrom>(_name, _lifetimeManager, _injectionMembers);
                else
                    container.RegisterType<TFrom>(_name, _injectionMembers);
            }
            else
            {
                if (_lifetimeManager != null)
                    container.RegisterType<TFrom>(_lifetimeManager, _injectionMembers);
                else
                    container.RegisterType<TFrom>(_injectionMembers);
            }
        }
    }

    public class ContainerConfigurer
    {
        private readonly IList<IContainerConfigurer> _configurers = new List<IContainerConfigurer>();

        public ContainerConfigurer Add(IContainerConfigurer configurer)
        {
            _configurers.Add(configurer);
            return this;
        }

        public ContainerConfigurer Add(string sectionName)
        {
            _configurers.Add(new SectionConfigurer(sectionName));
            return this;
        }

        public ContainerConfigurer RegisterType<TFrom, TTo>(params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            _configurers.Add(new TypeConfigurer<TFrom, TTo>(null, null, injectionMembers));
            return this;
        }

        public ContainerConfigurer RegisterType<TFrom, TTo>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            _configurers.Add(new TypeConfigurer<TFrom, TTo>(null, lifetimeManager, injectionMembers));
            return this;
        }

        public ContainerConfigurer RegisterType<TFrom, TTo>(string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            _configurers.Add(new TypeConfigurer<TFrom, TTo>(name, lifetimeManager, injectionMembers));
            return this;
        }

        public ContainerConfigurer RegisterType<TFrom>(params InjectionMember[] injectionMembers)
        {
            _configurers.Add(new TypeConfigurer<TFrom>(null, null, injectionMembers));
            return this;
        }

        public ContainerConfigurer RegisterType<TFrom>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            _configurers.Add(new TypeConfigurer<TFrom>(null, lifetimeManager, injectionMembers));
            return this;
        }

        public ContainerConfigurer RegisterType<TFrom>(string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            _configurers.Add(new TypeConfigurer<TFrom>(name, lifetimeManager, injectionMembers));
            return this;
        }

        public void Configure(IUnityContainer container, IContainerEventSource eventSource)
        {
            container.AddExtension(new EventBrokerExtension(eventSource));

            foreach (var configurer in _configurers)
                configurer.RegisterTypes(container);

            foreach (var configurer in _configurers)
            {
                if (configurer is IContainerInstanceConfigurer)
                    ((IContainerInstanceConfigurer)configurer).RegisterInstances(container);
            }
        }
    }
}
