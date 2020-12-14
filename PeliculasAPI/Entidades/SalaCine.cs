using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Entidades
{
    public class SalaCine : IId
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }


        public List<PeliculasSalasCine> PeliculasSalasCines { get; set; }
    }
}
