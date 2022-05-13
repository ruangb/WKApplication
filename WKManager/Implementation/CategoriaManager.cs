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

        public async Task<IEnumerable<Categoria>> GetAsync()
        {
            return await _categoriaRepository.GetAsync();
        }

        public async Task<Categoria> GetAsync(int id)
        {
            return _mapper.Map<Categoria>(await _categoriaRepository.GetAsync(id));
        }

        public async Task<Categoria> InsertAsync(NovaCategoria novaCategoria)
        {
            var categoria = _mapper.Map<Categoria>(novaCategoria);

            categoria = await _categoriaRepository.InsertAsync(categoria);

            return _mapper.Map<Categoria>(categoria);
        }

        public async Task<Categoria> UpdateAsync(Categoria categoria)
        {
            categoria = await _categoriaRepository.UpdateAsync(categoria);

            return _mapper.Map<Categoria>(categoria);
        }

        public async Task<Categoria> DeleteAsync(int id)
        {
            var categoria = await _categoriaRepository.DeleteAsync(id);

            return _mapper.Map<Categoria>(categoria);
        }
    }
}
