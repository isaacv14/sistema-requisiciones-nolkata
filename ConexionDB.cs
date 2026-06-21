using Microsoft.Data.SqlClient;

namespace Nolkata_Final  
{
    public static class ConexionDB
    {
        // Cadena de conexión para SQL Server local
        private static string cadenaConexion = "Server=localhost;Database=NolkataDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
