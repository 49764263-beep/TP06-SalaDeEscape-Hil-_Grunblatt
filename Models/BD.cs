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
        
    public List<string> ListaPalabras()
    {
        List<string> palabras = new List<string>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT Palabra FROM Palabras";
            palabras = connection.Query<string>(query).ToList();
        }
        return palabras;
    }
        public void AgregarPalabra(string palabra){
        string query = "INSERT INTO Palabras (Palabra) VALUES (@Palabra)";
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            connection.Execute(query, new {Palabra = palabra});
        }
    }

    // Crea un metodo para traer el registro de las Partida, para poder iniciar una, mediante el string Query.
    public Partida ObtenerPartida(string nombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Partida WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<Partida>(query, new { NombreUsuario = nombreUsuario });
        }
    
    }
    // Crea un metodo para crear una nueva partida, mediante el string Query.
    public void CrearPartida(Partida partida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Partida (NombreUsuario, Fechainicio, SalaActual) VALUES (@NombreUsuario, @Fechainicio, @SalaActual)";
            connection.Execute(query, partida);
        }
    }
    // Crea un metodo para verificar si el nombre de usuario existe en la base de datos, mediante el string Query y sin usar el count,
    public bool VerificarUsuario(string nombreUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT 1 FROM Partida WHERE NombreUsuario = @NombreUsuario";
            return connection.QueryFirstOrDefault<bool>(query, new { NombreUsuario = nombreUsuario });
        }
    }


}