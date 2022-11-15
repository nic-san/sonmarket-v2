using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sonmarket.Models
{
    public class Promocao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Produto Produto { get; set; }
        public float Porcentagem { get; set; }
        public bool Status { get; set; }
    }
}