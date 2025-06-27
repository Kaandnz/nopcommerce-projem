
using Nop.Services.Plugins;
using Nop.Services.Cms;

namespace Nop.Plugin.Widgets.CouponDropdown
{
    public class CouponDropdownPlugin : BasePlugin, IWidgetPlugin
    {
        public bool HideInWidgetList => false;

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
        {
            "shopping_cart_discounts"
        });
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(Components.CouponDropdownViewComponent);
        }

        public override Task InstallAsync() => base.InstallAsync();

        public override Task UninstallAsync() => base.UninstallAsync();
    }


}
