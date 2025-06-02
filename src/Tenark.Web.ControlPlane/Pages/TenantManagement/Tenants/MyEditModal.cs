using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Volo.Abp.TenantManagement.Web.Pages.TenantManagement.Tenants;

public class MyEditModalModel : EditModalModel
{
    public MyEditModalModel(ITenantAppService tenantAppService):base(tenantAppService)
    {
    }

    public override Task<IActionResult> OnGetAsync(Guid id)
    {
        return base.OnGetAsync(id);
    }

    public override  Task<IActionResult> OnPostAsync()
    {
        return base.OnPostAsync();
    }
}
