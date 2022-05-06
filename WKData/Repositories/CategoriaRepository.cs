using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKManager.Interfaces.Repositories;
using WKDomain.Models;

namespace WKData.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly WKContext context;

        public CategoriaRepository(WKContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await context.Categoria
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categoria> GetCategoriaAsync(int id)
        {
            return await context.Categoria
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Categoria> InsertCategoriaAsync(Categoria Categoria)
        {
            await context.Categoria.AddAsync(Categoria);
            await context.SaveChangesAsync();

            return Categoria;
        }

        public async Task<Categoria> UpdateCategoriaAsync(Categoria Categoria)
        {
            var searchedCategoria = await context.Categoria.FindAsync(Categoria.Id);

            if (searchedCategoria == null)
                return null;

            context.Entry(searchedCategoria).CurrentValues.SetValues(Categoria);

            await context.SaveChangesAsync();

            return searchedCategoria;
        }

        public async Task<Categoria> DeleteCategoriaAsync(int id)
        {
            var searchedCategoria = await context.Categoria.FindAsync(id);

            if (searchedCategoria == null)
                return null;

            var removedCategoria = context.Categoria.Remove(searchedCategoria);

            await context.SaveChangesAsync();

            return removedCategoria.Entity;
        }

    }
}
