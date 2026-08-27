// Crea la clase Respuesta que contiene IDRespuesta, SalaID, PartidaID, Respuesta y Acertada que es boolean
using System;

namespace SalaDeEscape.Models
{
    public class Respuesta
    {
        public int IDRespuesta { get; set; }
        public int SalaID { get; set; }
        public int PartidaID { get; set; }
        public string TextoRespuesta { get; set; }
        public bool Acertada { get; set; }
    }
}