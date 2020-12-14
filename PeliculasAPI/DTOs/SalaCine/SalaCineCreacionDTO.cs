using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.SalaCine
{
    public class SalaCineCreacionDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
    }
}
