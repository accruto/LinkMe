using System;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    [TestClass]
    public abstract class ProductEmailTests
        : TestClass
    {
        private const string CreditCardNumber = "4444333322221111";
        private const string Cvv = "123";
        private const string CardHolderName = "Marge Simpson";

        protected IMockEmailServer _emailServer;
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IExercisedCreditsCommand _exercisedCreditsCommand = Resolve<IExercisedCreditsCommand>();
        protected readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IOrdersCommand _ordersCommand = Resolve<IOrdersCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();

        [TestInitialize]
        public void ProductEmailTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer = EmailHost.Start();
            _emailServer.ClearEmails();
        }

        protected static CreditCard CreateCreditCard()
        {
            return new CreditCard
            {
                CardHolderName = CardHolderName,
                CardNumber = CreditCardNumber,
                CardType = CreditCardType.MasterCard,
                Cvv = Cvv,
                ExpiryDate = new ExpiryDate(DateTime.Now.AddYears(1))
            };
        }
    }
}