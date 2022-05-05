using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;

namespace WKManager.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaAsync(int id);
        Task<Categoria> InsertCategoriaAsync(Categoria Categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria Categoria);
        Task<Categoria> DeleteCategoriaAsync(int id);
    }
}
