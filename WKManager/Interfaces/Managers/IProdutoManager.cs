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
        Task<Produto> InsertProdutoAsync(NovoProduto Produto);
        Task<Produto> UpdateProdutoAsync(AtualizaProduto Produto);
        Task<Produto> DeleteProdutoAsync(int id);
    }
}
