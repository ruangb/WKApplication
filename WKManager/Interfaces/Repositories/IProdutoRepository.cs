using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;

namespace WKManager.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto> GetProdutoAsync(int id);
        Task<Produto> InsertProdutoAsync(Produto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
