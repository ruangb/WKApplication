using AutoMapper;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace LC.Manager.Mappings
{
    public class AtualizaProdutoMappingProfile : Profile
    {
        public AtualizaProdutoMappingProfile()
        {
            CreateMap<AtualizaProduto, Produto>()
                 .ForMember(d => d.Nome, o => o.MapFrom(x => x.Nome))
                 .ForMember(d => d.Preco, o => o.MapFrom(x => x.Preco));
        }
    }
}
