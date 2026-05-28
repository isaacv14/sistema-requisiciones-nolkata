using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolkataInc.Clases
{
    public class ConexionBD
    {
        /*Credenciales de clever cloud*/
        private string cadenaConexion = "Server = 7obxsxp-mysql.services.clever-cloud.com;Port = 3306;Database=umn4cs3pvhstwttvqqat; Uid=umn4cs3pvhstwttvqqat; Pwd=cO9LOnYvYat8V6Z698gE;";


        private MySqlConnection conexion;

        public ConexionBD()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        //metodo para abrir la BD en la nube
        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
                return conexion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a Clever cloud:" + ex.Message );
            }
        }

        //Cerrar conexion y liberar memoria
        public void CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();

            }
        }

    }
}
