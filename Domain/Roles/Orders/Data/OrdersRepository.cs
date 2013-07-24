using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Orders.Data
{
    public class OrdersRepository
        : Repository, IOrdersRepository
    {
        private readonly IAdjustmentPersister _adjustmentPersister;

        private static readonly DataLoadOptions OrderLoadOptions = DataOptions.CreateLoadOptions<ProductOrderEntity, ProductOrderEntity>(o => o.ProductOrderItemEntities, o => o.ProductOrderAdjustmentEntities);
        private static readonly DataLoadOptions CouponLoadOptions = DataOptions.CreateLoadOptions<ProductCouponEntity, ProductCouponEntity>(c => c.ProductCouponProductEntities, c => c.ProductCouponRedeemerEntities);

        private static readonly Func<OrdersDataContext, Guid, IAdjustmentPersister, Order> GetOrderQuery
            = CompiledQuery.Compile((OrdersDataContext dc, Guid id, IAdjustmentPersister adjustmentPersister)
                => (from o in dc.ProductOrderEntities
                    where o.id == id
                    select o.Map(adjustmentPersister)).SingleOrDefault());

        private static readonly Func<OrdersDataContext, string, IAdjustmentPersister, Order> GetOrderByConfirmationCodeQuery
            = CompiledQuery.Compile((OrdersDataContext dc, string confirmationCode, IAdjustmentPersister adjustmentPersister)
                => (from o in dc.ProductOrderEntities
                    where o.confirmationCode == confirmationCode
                    select o.Map(adjustmentPersister)).SingleOrDefault());

        private static readonly Func<OrdersDataContext, Guid, IAdjustmentPersister, Order> GetPurchasedOrderQuery
            = CompiledQuery.Compile((OrdersDataContext dc, Guid id, IAdjustmentPersister adjustmentPersister)
                => (from o in dc.ProductOrderEntities
                    where o.id == id
                    && (from r in dc.ProductReceiptEntities where r.orderId == o.id select r).Any()
                    select o.Map(adjustmentPersister)).SingleOrDefault());

        private static readonly Func<OrdersDataContext, Guid, IAdjustmentPersister, IQueryable<Order>> GetOrdersQuery
            = CompiledQuery.Compile((OrdersDataContext dc, Guid ownerId, IAdjustmentPersister adjustmentPersister)
                => from o in dc.ProductOrderEntities
                   where o.ownerId == ownerId
                   orderby o.time
                   select o.Map(adjustmentPersister));

        private static readonly Func<OrdersDataContext, string, IAdjustmentPersister, IQueryable<Order>> GetOrdersByIdsQuery
            = CompiledQuery.Compile((OrdersDataContext dc, string orderIds, IAdjustmentPersister adjustmentPersister)
                => from o in dc.ProductOrderEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, orderIds) on o.id equals i.value
                   orderby o.time
                   select o.Map(adjustmentPersister));

        private static readonly Func<OrdersDataContext, Guid, IAdjustmentPersister, IQueryable<Order>> GetPurchasedOrdersQuery
            = CompiledQuery.Compile((OrdersDataContext dc, Guid ownerId, IAdjustmentPersister adjustmentPersister)
                => from o in dc.ProductOrderEntities
                   where o.ownerId == ownerId
                   && (from r in dc.ProductReceiptEntities where r.orderId == o.id select r).Any()
                   orderby o.time
                   select o.Map(adjustmentPersister));

        private static readonly Func<OrdersDataContext, string, string, bool> DoesOrderAdjustmentExist
            = CompiledQuery.Compile((OrdersDataContext dc, string type, string code)
                => (from a in dc.ProductOrderAdjustmentEntities
                    where a.type == type
                    && Equals(a.code, code)
                    select a).Any());

        private static readonly Func<OrdersDataContext, Guid, Receipt> GetPurchaseReceipt
            = CompiledQuery.Compile((OrdersDataContext dc, Guid orderId)
                => (from r in dc.ProductReceiptEntities
                    where r.orderId == orderId
                    && r.type == (int)Mappings.ReceiptType.CreditCard
                    select r.Map()).SingleOrDefault());

        private static readonly Func<OrdersDataContext, Guid, IQueryable<Receipt>> GetReceipts
            = CompiledQuery.Compile((OrdersDataContext dc, Guid orderId)
                => from r in dc.ProductReceiptEntities
                   where r.orderId == orderId
                   orderby r.externalTransactionTime
                   select r.Map());

        private static readonly Func<OrdersDataContext, Guid, string, PurchaseTransactionEntity> GetPurchaseTransactionEntity
            = CompiledQuery.Compile((OrdersDataContext dc, Guid orderId, string transactionId)
                => (from t in dc.PurchaseTransactionEntities
                    where t.orderId == orderId
                    && t.transactionId == transactionId
                    select t).SingleOrDefault());

        private static readonly Func<OrdersDataContext, Guid, IQueryable<PurchaseTransaction>> GetPurchaseTransactions
            = CompiledQuery.Compile((OrdersDataContext dc, Guid orderId)
                => from t in dc.PurchaseTransactionEntities
                   where t.orderId == orderId
                   select t.Map());

        private static readonly Func<OrdersDataContext, Guid, Coupon> GetCouponQuery
            = CompiledQuery.Compile((OrdersDataContext dc, Guid id)
                => (from c in dc.ProductCouponEntities
                    where c.id == id
                    select c.Map()).SingleOrDefault());

        private static readonly Func<OrdersDataContext, string, Coupon> GetCouponByCodeQuery
            = CompiledQuery.Compile((OrdersDataContext dc, string code)
                => (from c in dc.ProductCouponEntities
                    where c.code == code
                    select c.Map()).SingleOrDefault());

        public OrdersRepository(IDataContextFactory dataContextFactory, IAdjustmentPersister adjustmentPersister)
            : base(dataContextFactory)
        {
            _adjustmentPersister = adjustmentPersister;
        }

        void IOrdersRepository.CreateOrder(Order order)
        {
            using (var dc = CreateContext())
            {
                dc.ProductOrderEntities.InsertOnSubmit(order.Map(_adjustmentPersister));
                dc.SubmitChanges();
            }
        }

        Order IOrdersRepository.GetOrder(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrder(dc, id);
            }
        }

        Order IOrdersRepository.GetOrder(string confirmationCode)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrder(dc, confirmationCode);
            }
        }

        Order IOrdersRepository.GetPurchasedOrder(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPurchasedOrder(dc, id);
            }
        }

        IList<Order> IOrdersRepository.GetOrders(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrders(dc, ownerId).ToList();
            }
        }

        IList<Order> IOrdersRepository.GetOrders(IEnumerable<Guid> orderIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetOrders(dc, orderIds).ToList();
            }
        }

        IList<Order> IOrdersRepository.GetPurchasedOrders(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPurchasedOrders(dc, ownerId).ToList();
            }
        }

        bool IOrdersRepository.DoesOrderAdjustmentExist(OrderAdjustment adjustment)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return DoesOrderAdjustmentExist(dc, _adjustmentPersister.GetAdjustmentType(adjustment), ((IPersistableAdjustment)adjustment).Code);
            }
        }

        void IOrdersRepository.CreateReceipt(Guid orderId, Receipt receipt)
        {
            using (var dc = CreateContext())
            {
                dc.ProductReceiptEntities.InsertOnSubmit(receipt.Map(orderId));
                dc.SubmitChanges();
            }
        }

        PurchaseReceipt IOrdersRepository.GetPurchaseReceipt(Guid orderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPurchaseReceipt(dc, orderId) as PurchaseReceipt;
            }
        }

        IList<Receipt> IOrdersRepository.GetReceipts(Guid orderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetReceipts(dc, orderId).ToList();
            }
        }

        void IOrdersRepository.CreatePurchaseRequest(Guid orderId, string transactionId, string provider, PurchaseRequest request)
        {
            using (var dc = CreateContext())
            {
                dc.PurchaseTransactionEntities.InsertOnSubmit(request.Map(orderId, transactionId, provider));
                dc.SubmitChanges();
            }
        }

        void IOrdersRepository.CreatePurchaseResponse(Guid orderId, string transactionId, PurchaseResponse response)
        {
            using (var dc = CreateContext())
            {
                var entity = GetPurchaseTransactionEntity(dc, orderId, transactionId);
                if (entity != null)
                {
                    entity.responseTime = response.Time;
                    entity.responseMessage = response.Message;
                    dc.SubmitChanges();
                }
            }
        }

        IList<PurchaseTransaction> IOrdersRepository.GetPurchaseTransactions(Guid orderId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetPurchaseTransactions(dc, orderId).ToList();
            }
        }

        void IOrdersRepository.CreateCoupon(Coupon coupon)
        {
            using (var dc = CreateContext())
            {
                dc.ProductCouponEntities.InsertOnSubmit(coupon.Map());
                dc.SubmitChanges();
            }
        }

        Coupon IOrdersRepository.GetCoupon(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCoupon(dc, id);
            }
        }

        Coupon IOrdersRepository.GetCoupon(string code)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCoupon(dc, code);
            }
        }

        void IOrdersRepository.EnableCoupon(Guid id)
        {
            EnableCoupon(id, true);
        }

        void IOrdersRepository.DisableCoupon(Guid id)
        {
            EnableCoupon(id, false);
        }

        private Order GetOrder(OrdersDataContext dc, Guid id)
        {
            dc.LoadOptions = OrderLoadOptions;
            return GetOrderQuery(dc, id, _adjustmentPersister);
        }

        private Order GetOrder(OrdersDataContext dc, string confirmationCode)
        {
            dc.LoadOptions = OrderLoadOptions;
            return GetOrderByConfirmationCodeQuery(dc, confirmationCode, _adjustmentPersister);
        }

        private Order GetPurchasedOrder(OrdersDataContext dc, Guid id)
        {
            dc.LoadOptions = OrderLoadOptions;
            return GetPurchasedOrderQuery(dc, id, _adjustmentPersister);
        }

        private IEnumerable<Order> GetOrders(OrdersDataContext dc, Guid ownerId)
        {
            dc.LoadOptions = OrderLoadOptions;
            return GetOrdersQuery(dc, ownerId, _adjustmentPersister);
        }

        private IEnumerable<Order> GetOrders(OrdersDataContext dc, IEnumerable<Guid> orderIds)
        {
            dc.LoadOptions = OrderLoadOptions;
            return GetOrdersByIdsQuery(dc, new SplitList<Guid>(orderIds.Distinct()).ToString(), _adjustmentPersister);
        }

        private IEnumerable<Order> GetPurchasedOrders(OrdersDataContext dc, Guid ownerId)
        {
            dc.LoadOptions = OrderLoadOptions;
            return GetPurchasedOrdersQuery(dc, ownerId, _adjustmentPersister);
        }

        private static Coupon GetCoupon(OrdersDataContext dc, Guid id)
        {
            dc.LoadOptions = CouponLoadOptions;
            return GetCouponQuery(dc, id);
        }

        private static Coupon GetCoupon(OrdersDataContext dc, string code)
        {
            dc.LoadOptions = CouponLoadOptions;
            return GetCouponByCodeQuery(dc, code);
        }

        private void EnableCoupon(Guid id, bool enabled)
        {
            using (var dc = CreateContext())
            {
                var entity = new ProductCouponEntity { id = id, enabled = !enabled };
                dc.ProductCouponEntities.Attach(entity);
                entity.enabled = enabled;
                dc.SubmitChanges();
            }
        }

        private OrdersDataContext CreateContext()
        {
            return CreateContext(c => new OrdersDataContext(c));
        }
    }
}
