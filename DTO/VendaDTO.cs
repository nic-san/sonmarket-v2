using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace sonmarket.DTO
{
    public class VendaDTO
    {
        public float total { get; set; }
        public float troco { get; set; }
        public SaidaDTO[] produtos { get; set; }
    }
}