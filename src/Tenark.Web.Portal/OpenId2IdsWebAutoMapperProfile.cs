using AutoMapper;
using OpenId2Ids.Books;

namespace OpenId2Ids.Web.Portal;

public class OpenId2IdsWebAutoMapperProfile : Profile
{
    public OpenId2IdsWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<BookDto, CreateUpdateBookDto>();
    }
}
