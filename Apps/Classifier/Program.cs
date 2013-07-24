using System;
using System.Windows.Forms;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Data;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;

namespace Classifier
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new ContainerConfigurer()
                .RegisterType<IJobAdsQuery, JobAdsQuery>(new ContainerControlledLifetimeManager())
                .RegisterType<IJobAdsRepository, JobAdsRepository>(new ContainerControlledLifetimeManager(),new InjectionConstructor(
                    new ResolvedParameter<IDbConnectionFactory>(),
                    new ResolvedParameter<ILocationQuery>(),
                    new ResolvedParameter<IIndustriesQuery>(),
                    new TimeSpan(0, 5, 0)))
                .RegisterType<ILocationRepository, LocationRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IIndustriesRepository, IndustriesRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IIndustriesQuery, IndustriesQuery>(new ContainerControlledLifetimeManager())
                .RegisterType<ILocationQuery, LocationQuery>(new ContainerControlledLifetimeManager())
                .RegisterType<IDbConnectionFactory, SqlConnectionFactory>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor("Initial Catalog=LinkMe;Data Source=DEVDB01;user id=linkme_owner;password=linkme;"))

                .Configure(Container.Current);

            if (args != null && args.Length > 0)
            {
                // just to get access to the funcitons
                var classifer = new Classifier();

                classifer.RewriteClassifications();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.Run(new Classifier());
            }
        }
    }
}
