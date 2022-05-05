using WKDomain.Models;

namespace WKDomain.ModelViews
{
    /// <summary>
    /// Objeto usado para criar um novo produto
    /// </summary>
    public class NovoProduto
    {
        /// <summary>
        /// Nome do produto 
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <example>40,00</example>
        public decimal Preco { get; set; }

        public Produto Produto { get; set; }

    }
}
