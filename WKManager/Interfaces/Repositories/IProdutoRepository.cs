using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;

namespace WKManager.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto> GetProdutoAsync(int id);
        Task<Produto> InsertProdutoAsync(Produto Produto);
        Task<Produto> UpdateProdutoAsync(Produto Produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
