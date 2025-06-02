using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Azure.Messaging.EventGrid;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using Microsoft.Extensions.Configuration;
using OpenId2Ids.Tenants;
using static Volo.Abp.TenantManagement.TenantManagementPermissions;
using Microsoft.AspNetCore.Components.Forms;

namespace Volo.Abp.TenantManagement.Web.Pages.TenantManagement.Tenants;

public class MyCreateModalModel : CreateModalModel
{
    //[BindProperty]
    //[Required]
    //[Display(Name = "Edition")]
    //public string Edition { get; set; }


    [BindProperty]
    public TenantInfoModel Tenant { get; set; }



    private readonly IConfiguration config;

    //private IMyTenantAppService tenantAppService;

    public MyCreateModalModel(ITenantAppService tenantAppService, 
                              IConfiguration config) : base(tenantAppService)
    {
        this.config = config;
        //this.tenantAppService = tenantAppService;
    }

    public override Task<IActionResult> OnGetAsync()
    {
        Tenant = new TenantInfoModel();
        return base.OnGetAsync();

        //Tenant = new TenantInfoModel();
        //return Task.FromResult<IActionResult>(Page());
    }

    public override async Task<IActionResult> OnPostAsync()
    {
        EventGridPublisherClient client = GetEventGridClient();

        // Add EventGridEvents to a list to publish to the topic
        EventGridEvent egEvent =
               new EventGridEvent(
                   "TenantCreated",
                   "TenantCreated",
                   "1.0",
                   Tenant);

        // Send the event
        await client.SendEventAsync(egEvent);

        //return await base.OnPostAsync();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Build the CreateTenantDto—this object will be serialized to JSON automatically.
        var input = new TenantCreateDto
        {
            Name = Tenant.Name,
            AdminEmailAddress = Tenant.AdminEmailAddress,
            AdminPassword = Tenant.AdminPassword,
            //Edition = Edition
        };

        // This call uses the ABP HTTP proxy under the hood, and will send JSON properly.
        await TenantAppService.CreateAsync(input);

        return NoContent();
    }

    private EventGridPublisherClient GetEventGridClient()
    {
        var endpoint = config["EventGrid:Endpoint"];
        var accessKey = config["EventGrid:AccessKey"];

        EventGridPublisherClient client = new EventGridPublisherClient(
            new Uri(endpoint),
            new AzureKeyCredential(accessKey));
        return client;
    }

    //public override async Task<IActionResult> OnPostAsync()
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest();
    //    }
    //    var input = new MyCreateTenantDto
    //    {
    //        Name = Tenant.Name,
    //        AdminEmailAddress = Tenant.AdminEmailAddress,
    //        AdminPassword = Tenant.AdminPassword,
    //        Edition = Tenant.Edition 
    //    };
    //    await this.tenantAppService.MyCreateAsync(input);
    //    return NoContent();
    //}

    public class TenantInfoModel : ExtensibleObject
    {
        [Required]
        [DynamicStringLength(typeof(TenantConsts), nameof(TenantConsts.MaxNameLength))]
        [Display(Name = "DisplayName:TenantName")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DynamicStringLength(typeof(TenantConsts), nameof(TenantConsts.MaxAdminEmailAddressLength))]
        public string AdminEmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DynamicStringLength(typeof(TenantConsts), nameof(TenantConsts.MaxPasswordLength))]
        public string AdminPassword { get; set; }

        // --- New Edition property ---
        [Required]
        [Display(Name = "Edition")]
        public string Edition { get; set; }
    }
}
