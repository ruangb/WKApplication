using AutoMapper;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace LC.Manager.Mappings
{
    public class NovaCategoriaMappingProfile : Profile
    {
        public NovaCategoriaMappingProfile()
        {
            CreateMap<NovaCategoria, Categoria>();
            CreateMap<AtualizaCategoria, Categoria>().ReverseMap();
        }
    }
}
