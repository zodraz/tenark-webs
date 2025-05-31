using OpenId2Ids.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tenark.Web.Portal.Pages;

public abstract class TenarkPageModel : AbpPageModel
{
    protected TenarkPageModel()
    {
        LocalizationResourceType = typeof(OpenId2IdsResource);
    }
}
