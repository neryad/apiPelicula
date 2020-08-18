using System;
using System.ComponentModel.DataAnnotations;
using APIPELICULA.Validaciones;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.DTOS
{
    public class ActorCreacionDto : ActorPacthDTO
    {

        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(grupotipoArchivo: GrupotipoArchivo.Imagen )]
        public IFormFile Foto { get; set; }
    }
}