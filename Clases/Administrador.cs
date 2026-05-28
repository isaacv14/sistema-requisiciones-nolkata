using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NolkataInc.Clases
{
    public class Administrador : Persona
    {
        //Atributos propios del administrador
        private string contrasenaAdministrador { get; set; }
        private string cargoUsuario { get; set; }

        private bool permisoVerCosto { get; set; }

        //la instancia de la clase de conexion para poder usarla en los metodos
        private ConexionBD conexionBd = new ConexionBD();

        //Constructor para inicializar la clase y enciar los datos a la clase padre
        public Administrador(long idPersona, string nombrePersona, string emailPersona, 
            string contrasenaAdministrador, string cargoUsuario, bool permisoVerCosto) : base (idPersona, nombrePersona, emailPersona)
        {
            this.contrasenaAdministrador = contrasenaAdministrador;
            this.cargoUsuario = cargoUsuario;
            this.permisoVerCosto = permisoVerCosto;
        }

        //Validar la contraseña 
        public bool Sesion (string contrasenaPuesta)
        {
            //usamos el metodo de conexionBd para entar a clevercloud
            MySqlConnection con = conexionBd.AbrirConexion();

            //Buscamos la contraseña del administrador por medio del id
            string query = "SELECT contrasena FROM usuarios WHERE idUsuario = @idUsuario";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                //Pasamos el parametro
                comando.Parameters.AddWithValue("@idUsuario", this.idPersona);

                    //Ejecuta el query y manda la primera columna de la primera fila
                    object resultado = comando.ExecuteScalar();

                    //Cerramos la conexion cuando terminamos
                    conexionBd.CerrarConexion();

                if(resultado != null)
                {
                    string contrasenaGuardada = resultado.ToString();

                    //Comparamos lo que mando el usuario con lo que hay en la bd
                    return contrasenaGuardada == contrasenaPuesta;
                }

                
            }
            return false;
        }

        //Cerrar sesion 
        public void CerrarSesion()
        {

        }

        //Modificar el estado de un recurso en la clase detalleRequisicion
        public void Cambiarestado(long idDetalle, string nuevoEstado)
        {
            MySqlConnection con = conexionBd.AbrirConexion();

            //query que apunta a las columnas estado y la fecha

            string query = @"UPDATE detallerequisicion SET estado = @nuevoEstado, fechaUltimoCambio = NOW()
                                  WHERE idDetalle = @idDetalle";

            using (MySqlCommand comando = new MySqlCommand(query, con)) {

                comando.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
                comando.Parameters.AddWithValue("@idDetalle", idDetalle);

                comando.ExecuteNonQuery();
            }

            conexionBd.CerrarConexion();

        }

        //Generar reporte
        public void GenerarReporte (string tipoReporte)
        {

            MySqlConnection con = conexionBd.AbrirConexion();

            //preguntar de como debe ser el reporte esto es un ejemplo 

            string query = "SELECT idDetalle, idProducto, cantidad, estado FROM detallesrequisicion";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                List<DetalleRequisicion> listaDetalle = new List<DetalleRequisicion>();
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        //se crea un obj para convertir al tipo de dato en cada vuelta
                        DetalleRequisicion detalle = new DetalleRequisicion();

                        //terminar cuando tenga la clase de detalle y poder acceder a sus atributos
                        detalle.IdDetalle = lector["idDetalle"].ToString();
                        string producto = lector["idProducto"].ToString();
                        string cantidad = lector["cantidad"].ToString();
                        string estado = lector["estado"].ToString();
                    }
                       

                }
                    
            }
            conexionBd.CerrarConexion();

        }
        

        
    }
}
