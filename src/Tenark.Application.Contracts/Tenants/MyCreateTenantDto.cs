using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.TenantManagement;

namespace OpenId2Ids.Tenants
{
    public class MyCreateTenantDto : TenantCreateOrUpdateDtoBase
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public virtual string AdminEmailAddress { get; set; }

        [Required]
        [MaxLength(128)]
        [DisableAuditing]
        public virtual string AdminPassword { get; set; }

        [Required]
        public string Edition { get; set; }
    }
}
