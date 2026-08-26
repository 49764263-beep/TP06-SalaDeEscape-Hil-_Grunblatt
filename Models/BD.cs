using Microsoft.Data.SqlClient;
using Dapper;
namespace SalaDeEscape.Models;

public class BD
{
    private string _connectionString;
    public BD()
    {
        _connectionString = @"Server=localhost; DataBase=TP06-SalaDeEscape;Integrated Security=True;TrustServerCertificate=True";
    }

    // Crea un metodo para traer el registro de las Partidas, para poder iniciar una, mediante el string Query.
    public Partida ObtenerPartida(string nombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Partidas WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<Partida>(query, new { NombreUsuario = nombreUsuario });
        }
    }
    // Crea un metodo para crear una nueva partida, mediante el string Query.
    public void CrearPartida(Partida partida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Partidas (NombreUsuario, Fechainicio) VALUES (@NombreUsuario, @Fechainicio)";
            connection.Execute(query, partida);
        }
    }
    // Crea un metodo para verificar si el nombre de usuario existe en la base de datos, mediante el string Query.
    public bool VerificarUsuario(string nombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT COUNT(*) FROM Partidas WHERE NombreUsuario = @NombreUsuario";
            int count = connection.ExecuteScalar<int>(query, new { NombreUsuario = nombreUsuario });
            return count > 0;
        }
    }
}