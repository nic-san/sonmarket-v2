using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sonmarket.DTO
{
    public class ProdutoDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, ErrorMessage = "Nome muito grande")]
        [MinLength(2, ErrorMessage = "Nome muito pequeno")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Categoria obrigatória")]
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "Fornecedor obrigatório")]
        public int FornecedorID { get; set; }

        [Required(ErrorMessage = "Preço de custo obrigatório")]
        public float PrecoDeCusto { get; set; }


        [Required(ErrorMessage = "Preço de venda obrigatório")]
        public float PrecoDeVenda { get; set; }


        [Required(ErrorMessage = "Medição obrigatória")]
        [Range(0, 2, ErrorMessage = "Medição inválida")]
        public int Medicao { get; set; }
    }
}