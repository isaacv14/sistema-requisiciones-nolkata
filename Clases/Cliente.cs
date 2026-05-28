using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolkataInc.Clases
{
    public class Cliente : Persona
    {
        private string contrasenaBarco { get; set; }
        public long idBarcoAsociado { get; private set; }

        private bool permisoVerCosto { get; set; }

        private ConexionBD conexionBD = new ConexionBD();

        public Cliente(long idPersona, string nombrePersona, string emailPersona, string contrasenaBarco,
            long idBarcoAsociado, bool permisoVerCosto):base(idPersona, nombrePersona, emailPersona)
        {
            this.contrasenaBarco = contrasenaBarco;
            this.idBarcoAsociado = idBarcoAsociado;
            this.permisoVerCosto = permisoVerCosto;
        }

        //Validar contraseña en la tabla usuarios
        public bool Sesion(string contrasenaIntroducida)
        {

            MySqlConnection con = conexionBD.AbrirConexion();
            string query = "SELECT contrasena FROM usuarios WHERE idUsuario = @idUsuario";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {

                comando.Parameters.AddWithValue("@idUsuario", this.idPersona);
                object resultado = comando.ExecuteNonQuery();

                conexionBD.CerrarConexion();

                if(resultado != null)
                {
                    return resultado.ToString() == contrasenaIntroducida;
                }

            }

            return false;

        }

        //Crear nuevo pedido
        public void CrearRequisicion()
        {
            MySqlConnection con = conexionBD.AbrirConexion();
            string query = @"INSERT INTO requisiciones (idBarco, fechaCreacion, costoTotal, idUsuarioCreador)
                              VALUES (@idBarco, NOW(), 0.00, @idUsuarioCreador)";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@idBarco", this.idBarcoAsociado); //FK del barco
                comando.Parameters.AddWithValue("@idUsuarioCreador", this.idPersona); //FK del usuario que creo el pedido


                comando.ExecuteNonQuery();

            }

            conexionBD.CerrarConexion();
        }

        //Ver los pedidos que pertenecen al barco
        public  List<DetalleRequisicion> VerPedidos()
        {
            //HACER DESPUES DE TERMINAR LA CLASE DE DETALLE REQUISICION 
        }



    }
}
