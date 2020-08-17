using System;
using System.ComponentModel.DataAnnotations;
using APIPELICULA.Validaciones;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.DTOS
{
    public class ActorCreacionDto
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public  DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(grupotipoArchivo: GrupotipoArchivo.Imagen )]
        public IFormFile Foto { get; set; }
    }
}