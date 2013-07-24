using System;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Web.Areas.Api.Models.Coupons
{
    public class JsonCouponModel
        : JsonResponseModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}
