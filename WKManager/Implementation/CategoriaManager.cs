using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKManager.Interfaces.Managers;
using WKManager.Interfaces.Repositories;

namespace WKManager.Implementation
{
    public class CategoriaManager : ICategoriaManager
    {
        private readonly ICategoriaRepository CategoriaRepository;
        private readonly IMapper mapper;

        public CategoriaManager(ICategoriaRepository CategoriaRepository, IMapper mapper)
        {
            this.CategoriaRepository = CategoriaRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return mapper.Map<IEnumerable<Categoria>>(await CategoriaRepository.GetCategoriasAsync());
        }

        public async Task<Categoria> GetCategoriaAsync(int id)
        {
            return mapper.Map<Categoria>(await CategoriaRepository.GetCategoriaAsync(id));
        }

        public async Task<Categoria> InsertCategoriaAsync(NovaCategoria NovaCategoria)
        {
            var Categoria = mapper.Map<Categoria>(NovaCategoria);

            Categoria = await CategoriaRepository.InsertCategoriaAsync(Categoria);

            return mapper.Map<Categoria>(Categoria);
        }

        public async Task<Categoria> UpdateCategoriaAsync(AtualizaCategoria atualizaCategoria)
        {
            var Categoria = mapper.Map<Categoria>(atualizaCategoria);

            Categoria = await CategoriaRepository.UpdateCategoriaAsync(Categoria);

            return mapper.Map<Categoria>(Categoria);
        }

        public async Task<Categoria> DeleteCategoriaAsync(int id)
        {
            var cliente = await CategoriaRepository.DeleteCategoriaAsync(id);

            return mapper.Map<Categoria>(cliente);
        }
    }
}
