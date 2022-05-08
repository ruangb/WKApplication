using System.ComponentModel.DataAnnotations;
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
        /// <example>Televisão</example>
        [Required(ErrorMessage = "{0} obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <example>40,00</example>
        [Required(ErrorMessage = "{0} obrigatório")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        /// <summary>
        /// Id da Categoria
        /// </summary>
        /// <example>1/example>
        [Required(ErrorMessage = "{0} obrigatória")]
        [Display(Name = "Categoria")]
        public decimal CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

    }
}
