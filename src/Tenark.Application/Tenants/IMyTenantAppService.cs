using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.TenantManagement;

namespace OpenId2Ids.Tenants
{
    public interface IMyTenantAppService : ITenantAppService
    {
        Task<TenantDto> MyCreateAsync(MyCreateTenantDto input);
    }
}
