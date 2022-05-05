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
        private readonly IProdutoRepository ProdutoRepository;
        private readonly IMapper mapper;

        public ProdutoManager(IProdutoRepository ProdutoRepository, IMapper mapper)
        {
            this.ProdutoRepository = ProdutoRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return mapper.Map<IEnumerable<Produto>>(await ProdutoRepository.GetProdutosAsync());
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {
            return mapper.Map<Produto>(await ProdutoRepository.GetProdutoAsync(id));
        }

        public async Task<Produto> InsertProdutoAsync(NovoProduto novoProduto)
        {
            var Produto = mapper.Map<Produto>(novoProduto);

            Produto = await ProdutoRepository.InsertProdutoAsync(Produto);

            return mapper.Map<Produto>(Produto);
        }

        public async Task<Produto> UpdateProdutoAsync(AtualizaProduto atualizaProduto)
        {
            var Produto = mapper.Map<Produto>(atualizaProduto);

            Produto = await ProdutoRepository.UpdateProdutoAsync(Produto);

            return mapper.Map<Produto>(Produto);
        }

        public async Task<Produto> DeleteProdutoAsync(int id)
        {
            var cliente = await ProdutoRepository.DeleteProdutoAsync(id);

            return mapper.Map<Produto>(cliente);
        }
    }
}
