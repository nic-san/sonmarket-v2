using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sonmarket.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public float Total { get; set; }
        public float ValorPago { get; set; }
        public float Troco { get; set; }
    }
}