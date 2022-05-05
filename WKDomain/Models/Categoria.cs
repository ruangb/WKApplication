using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WKDomain.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório")]
        [Column(TypeName = "varchar(50)")]
        public string Nome { get; set; }
    }
}
