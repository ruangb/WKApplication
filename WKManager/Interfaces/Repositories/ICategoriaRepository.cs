using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;

namespace WKManager.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAsync();
        Task<Categoria> GetAsync(int id);
        Task<Categoria> InsertAsync(Categoria Categoria);
        Task<Categoria> UpdateAsync(Categoria Categoria);
        Task<Categoria> DeleteAsync(int id);
    }
}
