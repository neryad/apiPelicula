using APIPELICULA.Validaciones;
using Microsoft.AspNetCore.Http;

namespace APIPELICULA.DTOS
{
    public class PeliculaCreacionDto : PeliculaPachtDto
    {

        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(GrupotipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }
    }
}