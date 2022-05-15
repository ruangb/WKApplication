using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKWebApp.Manager.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto> GetAsync(int id);
        Task<IEnumerable<Produto>> GetAsync();
        Task<Produto> InsertAsync(NovoProduto produto);
        Task<Produto> UpdateAsync(Produto produto);
        void Delete(int id);
    }
}
