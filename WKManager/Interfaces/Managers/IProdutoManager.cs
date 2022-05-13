using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKManager.Interfaces.Managers
{
    public interface IProdutoManager
    {
        Task<Produto> GetAsync(int id);
        Task<IEnumerable<Produto>> GetAsync();
        Task<Produto> InsertAsync(NovoProduto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<Produto> DeleteAsync(int id);
    }
}
