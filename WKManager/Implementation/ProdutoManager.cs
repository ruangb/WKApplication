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

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return _mapper.Map<IEnumerable<Produto>>(await _produtoRepository.GetProdutosAsync());
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {
            return _mapper.Map<Produto>(await _produtoRepository.GetProdutoAsync(id));
        }

        public async Task<Produto> InsertProdutoAsync(NovoProduto novoproduto)
        {
            var produto = _mapper.Map<Produto>(novoproduto);

            produto = await _produtoRepository.InsertProdutoAsync(produto);

            return _mapper.Map<Produto>(produto);
        }

        public async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            var produtoAtualizado = await _produtoRepository.UpdateProdutoAsync(produto);

            return _mapper.Map<Produto>(produtoAtualizado);
        }

        public async Task<Produto> DeleteProdutoAsync(int id)
        {
            var cliente = await _produtoRepository.DeleteProdutoAsync(id);

            return _mapper.Map<Produto>(cliente);
        }
    }
}
