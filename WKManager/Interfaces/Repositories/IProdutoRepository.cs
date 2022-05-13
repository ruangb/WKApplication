using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;

namespace WKManager.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAsync();
        Task<Produto> GetAsync(int id);
        Task<Produto> InsertAsync(Produto produto);
        Task<Produto> UpdateAsync(Produto produto);
        Task<Produto> DeleteAsync(int id);
    }
}
