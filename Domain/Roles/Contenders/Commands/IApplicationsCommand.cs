using System;

namespace LinkMe.Domain.Roles.Contenders.Commands
{
    public interface IApplicationsCommand
    {
        void CreateApplication<TApplication>(TApplication application) where TApplication : Application;
        void UpdateApplication<TApplication>(TApplication application) where TApplication : Application;
        void DeleteApplication<TApplication>(Guid id) where TApplication : Application;
    }
}