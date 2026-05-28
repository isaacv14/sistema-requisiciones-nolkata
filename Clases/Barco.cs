using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NolkataInc.Clases
{
    public class Barco
    {
        private long idBarco;
        private string nombreBarco;
        private string numeroMatricula;
        public string contactoBarco { get; private set; }

        private ConexionBD conexionBD = new ConexionBD();

        public long IdBarco { get { return idBarco; }
            set
            {
                if (value <= 0)
                    MessageBox.Show("El id del barco no puede ser 0");
                idBarco = value;
            }
        }

        public string NombreBarco { get { return nombreBarco; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("El nombre del Barco no puede estar vacio");

                    nombreBarco = value;
                }

            }
        
        }

        public string NumeroMatricula { get { return numeroMatricula; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("La matricula no puede es valida");

                    numeroMatricula = value;
                }
            }
        
        }

        public Barco() { }

        public Barco(long idBarco, string nombreBarco, string numeroMatricula, string contactoBarco)
        {
            this.idBarco = idBarco;
            this.nombreBarco = nombreBarco;
            this.numeroMatricula = numeroMatricula;
            this.contactoBarco = contactoBarco;
        }

        //Metodos 
        //Agregar un barco
        public void AgregarBarco()
        {
            MySqlConnection con = conexionBD.AbrirConexion();
            string query = "INSERT INTO barcos (nombreBarco, numeroMatricula, contactoBarco) VALUES (@nombre, @matricula, @contacto)";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@nombre", this.nombreBarco);
                comando.Parameters.AddWithValue("@matricula", this.numeroMatricula);
                comando.Parameters.AddWithValue("@contacto", this.contactoBarco);

                comando.ExecuteNonQuery();
            }
            conexionBD.CerrarConexion();
                              
        }

        //Editar barco
        public void EditarBarco()
        {
            MySqlConnection con = conexionBD.AbrirConexion();
            string query = "UPDATE barcos SET nombreBarco = @nombre, numeroMatricula = @matricula, contactoBarco = @contacto WHERE idBarco = id";

            using(MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@nombre", this.nombreBarco);
                comando.Parameters.AddWithValue("@matricula", this.numeroMatricula);
                comando.Parameters.AddWithValue("@contacto", this.contactoBarco);
                comando.Parameters.AddWithValue("@id", this.idBarco);
                comando.ExecuteNonQuery();
            }
            conexionBD.CerrarConexion();
        }

        //eliminar barco
        public void EliminarBarco()
        {
            MySqlConnection con = conexionBD.AbrirConexion();
            string query = "DELETE FROM barcos WHERE idBarco = @id";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@id", this.idBarco);
                comando.ExecuteNonQuery();
            }

            conexionBD.CerrarConexion();
        }

        //Listar los barcos 
        public List<Barco> ListarBarcos()
        {
            List<Barco> lista = new List<Barco>();
            MySqlConnection con = conexionBD.AbrirConexion();

            string query = "SELECT idbarco, nombreBarco, numeroMatricula, contactoBarco FROM barcos";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Barco barco = new Barco();

                        barco.idBarco = Convert.ToInt64(lector["idBarco"]);
                        barco.nombreBarco = lector["nombreBarco"].ToString();
                        barco.numeroMatricula = lector["numeroMatricula"].ToString();
                        barco.contactoBarco = lector["contactoBarco"].ToString();
                        lista.Add(barco);

                    }
                }
            }
            conexionBD.CerrarConexion();
            return lista;
        }


        

    }
}
