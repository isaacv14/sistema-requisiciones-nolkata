using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolkataInc.Clases
{
    public abstract class Persona
    {
        public long idPersona { get; }
        private string nombrePersona { get; }
        private string emailPersona { get; }

        public Persona (long idPersona, string nombrePersona, string emailPersona)
        {
            this.idPersona = idPersona;
            this.nombrePersona = nombrePersona;
            this.emailPersona = emailPersona;

        }

    }
}
