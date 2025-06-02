using Azure;
using Azure.Messaging.EventGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Local;
using Volo.Abp.TenantManagement;

namespace OpenId2Ids.Tenants
{
    // Tell ABP to replace the built-in TenantAppService with this one
    [Dependency(ReplaceServices = true)]
    public class MyTenantAppService : TenantAppService, ITenantAppService, IMyTenantAppService, ITransientDependency
    {
        private readonly IConfiguration config;

        public MyTenantAppService(
           ITenantRepository tenantRepository,
           ITenantManager tenantManager,
           IDataSeeder dataSeeder,
           IDistributedEventBus distributedEventBus,
           ILocalEventBus localEventBus,
           IConfiguration config) : base(tenantRepository,
               tenantManager, dataSeeder, distributedEventBus, localEventBus)
        {
            this.config = config;
        }

        [Authorize(TenantManagementPermissions.Tenants.Create)]
        public async Task<TenantDto> MyCreateAsync(MyCreateTenantDto input)
        {
            EventGridPublisherClient client = GetEventGridClient();

            // Add EventGridEvents to a list to publish to the topic
            EventGridEvent egEvent =
                new EventGridEvent(
                    "TenantCreated",
                    "TenantCreated",
                    "1.0",
                    input);

            return await base.CreateAsync(new TenantCreateDto()
            {
                AdminEmailAddress = input.AdminEmailAddress,
                AdminPassword = input.AdminPassword,
                //Edition = input.Edition,
                Name = input.Name
            });
        }

        [Authorize(TenantManagementPermissions.Tenants.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            // 1) You can do logging, permission checks, or custom business rules here
            Logger.LogInformation("Custom DeleteAsync called for tenant {TenantId}", id);

            var tenant = await TenantRepository.FindAsync(id);
            if (tenant == null)
            {
                return;
            }

            EventGridPublisherClient client = GetEventGridClient();

            // Add EventGridEvents to a list to publish to the topic
            EventGridEvent egEvent =
                new EventGridEvent(
                    "TenantDeleted",
                    "TenantDeleted",
                    "1.0",
                    tenant);

            // Send the event
            await client.SendEventAsync(egEvent);

            // 2) Add any pre-delete logic you need, for example:
            //    - Check if tenant has pending subscriptions
            //    - Clean up related data in another table
            //    - Call some remote API before actually deleting
            //
            //    If you want to prevent deletion under some condition, throw an exception here.

            // 3) Call base.DeleteAsync(id) to run the normal deletion steps:
            await base.DeleteAsync(id);

            // 4) Optionally, after deletion you can run “post-delete” logic:
            Logger.LogInformation("Tenant {TenantId} was successfully deleted.", id);
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
    }
}
