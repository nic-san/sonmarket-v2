using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sonmarket.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Produto Produto { get; set; }
        public float Quantidade { get; set; }
        public bool Status { get; set; }
    }
}