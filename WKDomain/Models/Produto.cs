using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WKDomain.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Column(TypeName = "decimal(7,2)")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "{0} obrigatória")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}
