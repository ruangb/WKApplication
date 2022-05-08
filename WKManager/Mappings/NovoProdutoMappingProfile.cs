using AutoMapper;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace LC.Manager.Mappings
{
    public class NovoProdutoMappingProfile : Profile
    {
        public NovoProdutoMappingProfile()
        {
            CreateMap<NovoProduto, Produto>();
            CreateMap<AtualizaProduto, Produto>().ReverseMap();
        }
    }
}
