using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WKManager.Interfaces.Repositories;
using WKDomain.Models;

namespace WKData.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly WKContext _context;

        public CategoriaRepository(WKContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAsync()
        {
            return await _context.Categoria
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Categoria> GetAsync(int id)
        {
            return await _context.Categoria
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Categoria> InsertAsync(Categoria Categoria)
        {
            await _context.Categoria.AddAsync(Categoria);
            await _context.SaveChangesAsync();

            return Categoria;
        }

        public async Task<Categoria> UpdateAsync(Categoria Categoria)
        {
            var searchedCategoria = await _context.Categoria.FindAsync(Categoria.Id);

            if (searchedCategoria == null)
                return null;

            _context.Entry(searchedCategoria).CurrentValues.SetValues(Categoria);

            await _context.SaveChangesAsync();

            return searchedCategoria;
        }

        public async Task<Categoria> DeleteAsync(int id)
        {
            var searchedCategoria = await _context.Categoria.FindAsync(id);

            if (searchedCategoria == null)
                return null;

            var removedCategoria = _context.Categoria.Remove(searchedCategoria);

            try
            {
                await _context.SaveChangesAsync();  
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }

            return removedCategoria.Entity;
        }

    }
}
