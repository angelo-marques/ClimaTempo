using Dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.DTOs
{
    public class PrevisaoClimaDTO
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataPrevisao { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Máximo de 150 caracteres")]
        public string Clima { get; set; }

        [Required]
        [Range(2,2, ErrorMessage = "Erro no valor informado")]
        public decimal TemperaturaMinima { get; set; }

        [Required]
        [Range(2, 2, ErrorMessage = "Erro no valor informado")]
        public decimal TemperaturaMaxima { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Máximo de 150 caracteres")]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Somente Sigla do Estado")]
        public string Uf { get; set; }
    }
}
