using System;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Accounts.Data;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Channels.Data;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Data;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Data;
using LinkMe.Domain.Devices.Apple;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Devices.Apple.Data;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Donations.Commands;
using LinkMe.Domain.Donations.Data;
using LinkMe.Domain.Donations.Queries;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Data;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Images.Commands;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.PhoneNumbers.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Commands;
using LinkMe.Domain.Products.Data;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Data;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Spam;
using LinkMe.Domain.Spam.Commands;
using LinkMe.Domain.Spam.Data;
using LinkMe.Domain.Spam.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;

namespace LinkMe.Domain.Unity
{
    public class DomainConfigurator
        : IContainerConfigurer
    {
        void IContainerConfigurer.RegisterTypes(IUnityContainer container)
        {
            // Database.

            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("database.connection.string")));
            container.RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                "database.tracking.connection.factory",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("database.tracking.connection.string")));

            container.RegisterType<IDataContextFactory, DataContextFactory>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<IDbConnectionFactory>(),
                    new ResolvedParameter<TimeSpan>("database.command.timeout")));

            // Industries.

            container.RegisterType<IIndustriesRepository, IndustriesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IIndustriesQuery, IndustriesQuery>(new ContainerControlledLifetimeManager());

            // Location.

            container.RegisterType<ILocationRepository, LocationRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILocationQuery, LocationQuery>(new ContainerControlledLifetimeManager());

            // PhoneNumbers.

            container.RegisterType<IPhoneNumbersQuery, PhoneNumbersQuery>(new ContainerControlledLifetimeManager());

            // Credits.

            container.RegisterType<ICreditsRepository, CreditsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICreditsQuery, CreditsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAllocationsCommand, AllocationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAllocationsQuery, AllocationsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExercisedCreditsCommand, ExercisedCreditsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IExercisedCreditsQuery, ExercisedCreditsQuery>(new ContainerControlledLifetimeManager());

            // Products.

            container.RegisterType<IProductsRepository, ProductsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductsCommand, ProductsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductsQuery, ProductsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICreditCardsQuery, CreditCardsQuery>(new ContainerControlledLifetimeManager());

            // Channels.

            container.RegisterType<IChannelsRepository, ChannelsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IChannelsQuery, ChannelsQuery>(new ContainerControlledLifetimeManager());

            // Resources.

            container.RegisterType<IResourcesRepository, ResourcesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResourcesCommand, ResourcesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IResourcesQuery, ResourcesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFaqsCommand, FaqsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFaqsQuery, FaqsQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPollsCommand, PollsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPollsQuery, PollsQuery>(new ContainerControlledLifetimeManager());

            // Files.

            container.RegisterType<IFilesRepository, FilesRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFilesCommand, FilesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFilesQuery, FilesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFilesStorageRepository, FilesStorageRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("linkme.domain.files.rootpath")));

            // Images.

            container.RegisterType<IImagesCommand, ImagesCommand>(new ContainerControlledLifetimeManager());

            // Spam.

            container.RegisterType<ISpamRepository, SpamRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISpamCommand, SpamCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISpamQuery, SpamQuery>(new ContainerControlledLifetimeManager());

            // Accounts.

            container.RegisterType<IUserAccountsRepository, UserAccountsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserAccountsCommand, UserAccountsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserAccountsQuery, UserAccountsQuery>(new ContainerControlledLifetimeManager());

            // Donations.

            container.RegisterType<IDonationsRepository, DonationsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDonationsCommand, DonationsCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDonationsQuery, DonationsQuery>(new ContainerControlledLifetimeManager());

            // Devices.

            container.RegisterType<IAppleDevicesQuery, AppleDevicesQuery>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAppleDevicesCommand, AppleDevicesCommand>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAppleDevicesRepository, AppleDevicesRepository>(new ContainerControlledLifetimeManager());
        }
    }
}
