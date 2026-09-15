using Microsoft.Data.SqlClient;
using Dapper;

namespace SalaDeEscape.Models;

public class BD
{
    private string _connectionString;

    public BD()
    {
        _connectionString = @"Server=localhost;DataBase=TP06-SalaDeEscape;Integrated Security=True;TrustServerCertificate=True";
    }

    public List<string> ListaPalabras()
    {
        List<string> palabras = new List<string>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Palabra FROM Palabras";
            palabras = connection.Query<string>(query).ToList();
        }
        return palabras;
    }

    public void AgregarPalabra(string palabra)
    {
        string query = "INSERT INTO Palabras (Palabra) VALUES (@Palabra)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Palabra = palabra });
        }
    }

    public Partida ObtenerPartida(string nombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Partida WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<Partida>(query, new { NombreUsuario = nombreUsuario });
        }
    }

    public void CrearPartida(Partida partida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Partida (NombreUsuario, FechaInicio, SalaActual) VALUES (@NombreUsuario, @FechaInicio, @SalaActual)";
            connection.Execute(query, partida);
        }
    }

    public bool VerificarUsuario(string nombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT 1 FROM Partida WHERE NombreUsuario = @NombreUsuario";
            int resultado = connection.QueryFirstOrDefault<int>(query, new { NombreUsuario = nombreUsuario });
            return resultado == 1;
        }
    }

    public void ActualizarSala(string nombreUsuario, int salaActual)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Partida SET SalaActual = @SalaActual WHERE NombreUsuario = @NombreUsuario";
            connection.Execute(query, new { NombreUsuario = nombreUsuario, SalaActual = salaActual });
        }
    }
}
