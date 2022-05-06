using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKWebApp.Manager.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> ObterCategoriaAsync(int id);
        Task<IEnumerable<Categoria>> ObterCategoriasAsync();
        Task<Categoria> InserirCategoriaAsync(NovaCategoria Categoria);
        Task<Categoria> AtualizarCategoriaAsync(AtualizaCategoria Categoria);
        void DeletarCategoriaAsync(int id);
    }
}
