using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKManager.Interfaces.Repositories;
using WKDomain.Models;

namespace WKData.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly WKContext context;

        public ProdutoRepository(WKContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await context.Produto
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Produto> GetProdutoAsync(int id)
        {
            return await context.Produto
                .Include(p => p.Categoria)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> InsertProdutoAsync(Produto Produto)
        {
            await context.Produto.AddAsync(Produto);
            await context.SaveChangesAsync();

            return Produto;
        }

        public async Task<Produto> UpdateProdutoAsync(Produto Produto)
        {
            var searchedProduto = await context.Produto.FindAsync(Produto.Id);

            if (searchedProduto == null)
                return null;

            context.Entry(searchedProduto).CurrentValues.SetValues(Produto);

            await context.SaveChangesAsync();

            return searchedProduto;
        }

        public async Task<Produto> DeleteProdutoAsync(int id)
        {
            var searchedProduto = await context.Produto.FindAsync(id);

            if (searchedProduto == null)
                return null;

            var removedProduto = context.Produto.Remove(searchedProduto);

            await context.SaveChangesAsync();

            return removedProduto.Entity;
        }

    }
}
