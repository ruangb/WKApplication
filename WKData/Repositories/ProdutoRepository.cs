using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKManager.Interfaces.Repositories;
using WKDomain.Models;

namespace WKData.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly WKContext _context;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoRepository(WKContext context, ICategoriaRepository categoriaRepository)
        {
            _context = context;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> InsertProdutoAsync(Produto produto)
        {
            await _context.Produto.AddAsync(produto);
            await _context.SaveChangesAsync();

            produto.Categoria = _categoriaRepository.GetAsync(produto.CategoriaId).Result;

            return produto;
        }

        public async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            var searchedProduto = await _context.Produto.FindAsync(produto.Id);

            if (searchedProduto == null)
                return null;

            _context.Entry(searchedProduto).CurrentValues.SetValues(produto);

            await _context.SaveChangesAsync();

            produto.Categoria = _categoriaRepository.GetAsync(produto.CategoriaId).Result;

            return searchedProduto;
        }

        public async Task<Produto> DeleteProdutoAsync(int id)
        {
            var searchedProduto = await _context.Produto.FindAsync(id);

            if (searchedProduto == null)
                return null;

            var removedProduto = _context.Produto.Remove(searchedProduto);

            await _context.SaveChangesAsync();

            return removedProduto.Entity;
        }
    }
}
