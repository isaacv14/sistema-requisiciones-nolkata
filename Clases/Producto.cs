using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NolkataInc.Clases
{
    public class Producto
    {
        private long idProducto;
        private string nombreProducto;
        private  string descripcionProducto;
        private string categoriaProducto;
        private int stockDisponible;
        private decimal precioProducto;


        private ConexionBD conexionBD = new ConexionBD();

        public long Idproducto { get { return idProducto; }
            set
            {
                if (value <= 0)
                {
                    MessageBox.Show("El id no puede ser 0");

                    idProducto = value;

                }
            }
        }

        public string Nombreproducto
        {
            get { return nombreProducto; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("el nombre del producto no puede estar vacio");

                    nombreProducto = value;
                 }
            }
        }
        
        public string DescripcionProducto
        {
            get { return descripcionProducto; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("La descripcion del producto no puede estar vacio");
                    descripcionProducto = value;

                }
            }
        }

        public string Categoriaproducto
        {
          get { return categoriaProducto; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("La categoria no puede estar vacia");
                    categoriaProducto = value;
                }
            }
        }

        public int Stockdisponible
        {
            get { return stockDisponible; }

            set
            {
                if(value < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo");

                    stockDisponible = value;
                }
            }
        }

        public decimal Precioproducto
        {
            get { return precioProducto; }

            set
            {
                if(value <= 0)
                {
                    MessageBox.Show("El precio no puede ser 0");
                    precioProducto = value;
                }
            }
        }

        public Producto()
        {

        }

        public Producto(long idProducto, string nombreProducto, string descripcionProducto, string categoriaProducto,int stockDisponible, decimal precioProducto)
        {
            this.idProducto = idProducto;
            this.nombreProducto = nombreProducto;
            this.descripcionProducto = descripcionProducto;
            this.categoriaProducto = categoriaProducto;
            this.stockDisponible = stockDisponible;
            this.precioProducto = precioProducto;
        }

        //Metodos

        //Agregar productos (FALTA LO DE la IMAGEN)
        public void AgregarProducto()
        {
            MySqlConnection con = conexionBD.AbrirConexion();

            string query = @"INSERT INTO productos(nombre, descripcion, categoria, precio) VALUES(@nombre, @categoria, @precio)";

            using(MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@nombre", this.nombreProducto);
                comando.Parameters.AddWithValue("@descripcion", this.descripcionProducto);
                comando.Parameters.AddWithValue("@categoria", this.precioProducto);
                comando.Parameters.AddWithValue("@precio", this.precioProducto);
                comando.ExecuteNonQuery();


            }

            conexionBD.CerrarConexion();
        }
        
        //Editar productos
        public void EditarProducto()
        {
            MySqlConnection con = conexionBD.AbrirConexion();
            string query = @"UPDATE productos SET nombre = @nombre, descripcion = @descripcion,
                            categoria = @categoria, precio = @precio";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@nombre", this.nombreProducto);
                comando.Parameters.AddWithValue("@descripcion", this.descripcionProducto);
                comando.Parameters.AddWithValue("@categoria", this.categoriaProducto);
                comando.Parameters.AddWithValue("@precio", this.precioProducto);

                comando.ExecuteNonQuery();
            }

            conexionBD.CerrarConexion();
        }

        //Mostrar todos los productos

        public List<Producto> MostrarProductos()
        {
            List<Producto> lista = new List<Producto>();

            MySqlConnection con = conexionBD.AbrirConexion();
            string query = "SELECT idProducto, nombre, descripcion, categoria, precio FROM productos";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                using(MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Producto p = new Producto();

                        p.idProducto = Convert.ToInt64(lector["idProducto"]);
                        p.nombreProducto = lector["nombre"].ToString();
                        p.descripcionProducto = lector["categoria"].ToString();
                        p.categoriaProducto = lector["categoria"].ToString();
                        p.precioProducto = Convert.ToDecimal(lector["precio"]);

                        lista.Add(p);
                    }
                }
            }
            conexionBD.CerrarConexion();
            return lista;
        }

        //Mostrar productos por categoria
        public List<Producto> CategoriaProducto(string categoriaFiltro)
        {
            List<Producto> lista = new List<Producto>();
            MySqlConnection con = conexionBD.AbrirConexion();

            string query = "SELECT idProducto, nombre, descripcion, categoria, precio FROM categoria = @cat";

            using (MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@cat", categoriaFiltro);

                using(MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Producto p = new Producto();


                        p.idProducto = Convert.ToInt64(lector["idProducto"]);
                        p.nombreProducto = lector["nombre"].ToString();
                        p.descripcionProducto = lector["descripcion"].ToString();
                        p.categoriaProducto = lector["categoria"].ToString();
                        p.precioProducto = Convert.ToDecimal(lector["precio"]);

                        lista.Add(p);
                      
                    }
                }
            }

            conexionBD.CerrarConexion();
            return lista;
        }

        //Mostrar producto por nombre
        public List<Producto>MostrarPorNombre(string filtroNombre)
        {
     
            List<Producto> lista = new List<Producto>();
            MySqlConnection con = conexionBD.AbrirConexion();

            string query = "SELECT idProducto, nombre, descripcion, categoria, precio FROM productos WHERE nombre like @nombre";

            using(MySqlCommand comando = new MySqlCommand(query, con))
            {
                comando.Parameters.AddWithValue("@nombre", "%" + filtroNombre + "%");

                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Producto p = new Producto();

                        p.idProducto = Convert.ToInt64(lector["idProducto"]);
                        p.nombreProducto = lector["nombre"].ToString();
                        p.descripcionProducto = lector["descripcion"].ToString();
                        p.categoriaProducto = lector["categoria"].ToString();
                        p.precioProducto = Convert.ToDecimal(lector["precio"]);

                        lista.Add(p);

                    }
                }
            }

            conexionBD.CerrarConexion();
            return lista;

        }




    }
}
