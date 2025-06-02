using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.TenantManagement;

namespace OpenId2Ids.Tenants
{
    public class MyTenantUpdateDto : TenantCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }

        public string Edition { get; set; }
    }
}
