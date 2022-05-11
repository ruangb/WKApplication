using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKWebApp.Manager.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetAsync(int id);
        Task<IList<Categoria>> GetAsync();
        Task<Categoria> InsertAsync(NovaCategoria Categoria);
        Task<Categoria> UpdateAsync(Categoria Categoria);
        void DeleteAsync(int id);
    }
}
