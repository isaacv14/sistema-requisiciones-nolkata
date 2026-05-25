using System;

namespace NolkataInc.Clases
{
    // Clase padre abstracta - base para los dos tipos de usuario
    public abstract class Usuario
    {
        // Atributos privados
        private string nombre;
        private string correo;

        // Propiedades publicas
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        // Constructor
        public Usuario(string nombre, string correo)
        {
            this.nombre = nombre;
            this.correo = correo;
        }

        // Metodo virtual que cada tipo de usuario implementa diferente
        public abstract bool PuedeVerCostos();
    }
}
