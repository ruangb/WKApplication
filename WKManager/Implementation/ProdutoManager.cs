using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;
using WKManager.Interfaces.Managers;
using WKManager.Interfaces.Repositories;

namespace WKManager.Implementation
{
    public class ProdutoManager : IProdutoManager
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoManager(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Produto>> GetAsync()
        {
            return _mapper.Map<IEnumerable<Produto>>(await _produtoRepository.GetAsync());
        }

        public async Task<Produto> GetAsync(int id)
        {
            return _mapper.Map<Produto>(await _produtoRepository.GetAsync(id));
        }

        public async Task<Produto> InsertAsync(NovoProduto novoProduto)
        {
            var produto = _mapper.Map<Produto>(novoProduto);

            produto = await _produtoRepository.InsertAsync(produto);

            return _mapper.Map<Produto>(produto);
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            var produtoAtualizado = await _produtoRepository.UpdateAsync(produto);

            return _mapper.Map<Produto>(produtoAtualizado);
        }

        public async Task<Produto> DeleteAsync(int id)
        {
            var cliente = await _produtoRepository.DeleteAsync(id);

            return _mapper.Map<Produto>(cliente);
        }
    }
}
