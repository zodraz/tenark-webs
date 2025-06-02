using AutoMapper;
using OpenId2Ids.Books;
using OpenId2Ids.Tenants;
using Volo.Abp.TenantManagement;

namespace OpenId2Ids;

public class OpenId2IdsApplicationAutoMapperProfile : Profile
{
    public OpenId2IdsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();


    //    CreateMap<Tenant, MyTenantDto>()
    //.ForMember(d => d.Edition, opt => opt.MapFrom(s => s.Edition))
    //    // … other mappings …
    //;
    //    CreateMap<CreateTenantDto, Tenant>()
    //        .ForMember(e => e.Edition, opt => opt.MapFrom(s => s.Edition))
    //        // … 
    //        ;
    //    CreateMap<UpdateTenantDto, Tenant>()
    //        .ForMember(e => e.Edition, opt => opt.MapFrom(s => s.Edition))
    //        // …
    //        ;

    }
}
