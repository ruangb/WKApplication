using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKManager.Interfaces.Managers
{
    public interface ICategoriaManager
    {
        Task<Categoria> GetCategoriaAsync(int id);
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> InsertCategoriaAsync(NovaCategoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<Categoria> DeleteCategoriaAsync(int id);
    }
}
