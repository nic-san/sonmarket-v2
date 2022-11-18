using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class SaidaDTO
    {
        public int produto { get; set; }
        public int quantidade { get; set; }
        public float subtotal { get; set; }
    }
}