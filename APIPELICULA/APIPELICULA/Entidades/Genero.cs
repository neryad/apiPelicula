using System.ComponentModel.DataAnnotations;

namespace APIPELICULA.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Nombre { get; set; }
    }
}