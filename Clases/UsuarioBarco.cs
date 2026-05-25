using System;

namespace NolkataInc.Clases
{
    // Usuario de barco: NO puede ver costos
    public class UsuarioBarco : Usuario
    {
        public UsuarioBarco(string nombre, string correo)
            : base(nombre, correo)
        {
        }

        public override bool PuedeVerCostos()
        {
            return false;
        }
    }
}
