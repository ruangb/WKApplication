using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKManager.Interfaces.Managers
{
    public interface ICategoriaManager
    {
        Task<Categoria> GetAsync(int id);
        Task<IEnumerable<Categoria>> GetAsync();
        Task<Categoria> InsertAsync(NovaCategoria categoria);
        Task<Categoria> UpdateAsync(Categoria categoria);
        Task<Categoria> DeleteAsync(int id);
    }
}
