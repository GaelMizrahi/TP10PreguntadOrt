namespace TP10_PreguntadORT.Models;

using Microsoft.Data.SqlClient;
using Dapper;
 public static class BD
    {
        private static string _connectionString =
            "Server=localhost;Database=PreguntadOrt;Integrated Security=True;TrustServerCertificate=True;";

        public static List<Categoria> ObtenerCategorias()
        {
            List<Categoria> categorias;
            using (SqlConnection connection  = new SqlConnection(_connectionString))
            {
                  string sql = "SELECT * FROM Categorias ORDER BY Nombre";
                  categorias = connection.Query<Categoria>(sql).ToList();
            }
            return categorias;
        }
        public static List<Pregunta> ObtenerPreguntas(int categoria)
        {
             using (SqlConnection connection  = new SqlConnection(_connectionString))
            {        
                if (categoria == -1)
                {
                    string sql = "SELECT IdPregunta, IdCategoria, Enunciado, Foto FROM Preguntas ORDER BY IdPregunta";
                    return connection.Query<Pregunta>(sql).ToList();
                }
                else
                {
                    string sql = "SELECT IdPregunta, IdCategoria, Enunciado, Foto FROM Preguntas WHERE IdCategoria = @categoria ORDER BY IdPregunta";
                    return connection.Query<Pregunta>(sql, new { categoria }).ToList();
                }
            }
        }
        

         public static List<Respuesta> ObtenerRespuestas(int idPregunta)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                         string sql = "SELECT IdRespuesta, IdPregunta, Opcion, Contenido, Correcta, Foto FROM Respuestas WHERE IdPregunta = @idPregunta ORDER BY Opcion";

                        return connection.Query<Respuesta>(sql, new { idPregunta }).ToList();
                    }
                }
    }
        
        
    


