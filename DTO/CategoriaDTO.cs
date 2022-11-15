using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sonmarket.DTO
{
    public class CategoriaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, ErrorMessage = "Nome muito grande")]
        [MinLength(2, ErrorMessage = "Nome muito pequeno")]
        public string Nome { get; set; }
    }
}