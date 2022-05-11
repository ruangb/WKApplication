using System.Collections.Generic;
using System.Threading.Tasks;
using WKDomain.Models;
using WKDomain.ModelViews;

namespace WKWebApp.Manager.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto> GetProdutoAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto> InsertProdutoAsync(NovoProduto produto);
        Task<Produto> UpdateProdutoAsync(Produto produto);
        void DeleteProdutoAsync(int id);
    }
}
