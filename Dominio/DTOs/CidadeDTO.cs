using System.ComponentModel.DataAnnotations;

namespace Dominio.DTOs
{
    public class CidadeDTO
    {
        [Required]
        [StringLength(150, ErrorMessage = "Máximo de 150 caracteres")]
        public string Cidade { get; set; }
        
        [Required]
        [StringLength(2, ErrorMessage ="Somente Sigla do Estado")]
        public string Uf { get; set; }
    }
}
