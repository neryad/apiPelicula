using System;
using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.DTOS
{
    public class ActorPacthDTO
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public  DateTime FechaNacimiento { get; set; }
    }
}