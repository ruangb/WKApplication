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
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaManager(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _categoriaRepository.GetCategoriasAsync();
        }

        public async Task<Categoria> GetCategoriaAsync(int id)
        {
            return _mapper.Map<Categoria>(await _categoriaRepository.GetCategoriaAsync(id));
        }

        public async Task<Categoria> InsertCategoriaAsync(NovaCategoria novaCategoria)
        {
            var categoria = _mapper.Map<Categoria>(novaCategoria);

            categoria = await _categoriaRepository.InsertCategoriaAsync(categoria);

            return _mapper.Map<Categoria>(categoria);
        }

        public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
        {
            categoria = await _categoriaRepository.UpdateCategoriaAsync(categoria);

            return _mapper.Map<Categoria>(categoria);
        }

        public async Task<Categoria> DeleteCategoriaAsync(int id)
        {
            var categoria = await _categoriaRepository.DeleteCategoriaAsync(id);

            return _mapper.Map<Categoria>(categoria);
        }
    }
}
