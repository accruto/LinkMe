using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Data;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Affiliations.Verticals
{
    [TestClass]
    public abstract class VerticalTests
        : TestClass
    {
        protected IVerticalsRepository _repository;
        protected IVerticalsCommand _verticalsCommand;
        protected IVerticalsQuery _verticalsQuery;

        protected const string VerticalNameFormat = "Vertical{0}";

        [TestInitialize]
        public void VerticalTestsInitialize()
        {
            var connectionFactory = Resolve<IDbConnectionFactory>();
            connectionFactory.DeleteAllTestData();

            _repository = new VerticalsRepository(Resolve<IDataContextFactory>());
            _verticalsCommand = new VerticalsCommand(_repository);
            _verticalsQuery = new VerticalsQuery(_repository);
        }

        protected Vertical CreateVertical(int index)
        {
            var vertical = new Vertical { Name = string.Format(VerticalNameFormat, index) };
            _verticalsCommand.CreateVertical(vertical);
            return vertical;
        }
    }
}