// crea la clase Partida que contiene IDPartida, NombreUsuario y Fechainicio
using System;

namespace SalaDeEscape.Models
{
    public class Partida
    {
        public int IDPartida { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fechainicio { get; set; }
        public int SalaActual { get; set; }
    }
}   