using System;
namespace SalaDeEscape.Models;

public class Palabras{

    private List<string> palabras;

    public Palabras(){
        BD BD = new BD();
        palabras = BD.ListaPalabras();
    }

    public string ObtenerPalabra(){
        Random random = new Random();
        int numeroRandom = random.Next(0, palabras.Count());
        return palabras[numeroRandom];
    }
    
    public string PalabraAGuion(string palabra){
        string PalabraConGuiones = "";
        for (int i = 0; i < palabra.Length; i++){
            PalabraConGuiones = PalabraConGuiones + "_";
        }
        return PalabraConGuiones;
    }
}