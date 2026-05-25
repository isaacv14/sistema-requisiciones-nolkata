using System;

namespace NolkataInc.Clases
{
    // Usuario de oficina: puede ver costos
    public class UsuarioOficina : Usuario
    {
        public UsuarioOficina(string nombre, string correo)
            : base(nombre, correo)
        {
        }

        public override bool PuedeVerCostos()
        {
            return true;
        }
    }
}
