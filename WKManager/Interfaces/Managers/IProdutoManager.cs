using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKManager.Interfaces.Managers
{
    public interface IProdutoManager
    {
        Task<Produto> GetProdutoAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto> InsertProdutoAsync(NovoProduto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
