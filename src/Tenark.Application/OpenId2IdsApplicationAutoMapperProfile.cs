using AutoMapper;
using OpenId2Ids.Books;

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
    }
}
