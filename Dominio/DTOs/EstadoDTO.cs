using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTOs
{
    public class EstadoDTO
    {
        [Required]
        [StringLength(150, ErrorMessage = "Máximo de 150 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Somente Sigla do Estado")]
        public string Uf { get; set; }
    }
}
