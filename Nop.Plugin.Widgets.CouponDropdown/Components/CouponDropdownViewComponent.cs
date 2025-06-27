using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Discounts;
using Nop.Plugin.Widgets.CouponDropdown.Models;
using Nop.Services.Discounts;
using Nop.Web.Framework.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.CouponDropdown.Components
{
    public class CouponDropdownViewComponent : NopViewComponent
    {
        private readonly IDiscountService _discountService;

        public CouponDropdownViewComponent(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var now = DateTime.UtcNow;
            var discounts = await _discountService.GetAllDiscountsAsync(null ,null,null, false);

            var activeCoupons = discounts
                .Where(d => !string.IsNullOrEmpty(d.CouponCode) &&
                            (!d.StartDateUtc.HasValue || d.StartDateUtc <= now) &&
                            (!d.EndDateUtc.HasValue || d.EndDateUtc >= now))
                .Select(d => new CouponModel
                {
                    Code = d.CouponCode,
                    Name = $"{d.Name} ({d.CouponCode})"
                })
                .ToList();


            return View("~/Plugins/Widgets.CouponDropdown/Views/PublicInfo.cshtml", activeCoupons);
        }
    }
}
