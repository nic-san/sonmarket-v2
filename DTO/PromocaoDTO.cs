using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class PromocaoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome é muito grande")]
        [MinLength(2, ErrorMessage = "Nome é muito pequeno")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Produto é obrigatório")]
        public int ProdutoID { get; set; }

        [Required(ErrorMessage = "Porcentagem é obrigatória")]
        [Range(0, 100, ErrorMessage = "Porcentagem invália")]
        public float Porcentagem { get; set; }
    }
}