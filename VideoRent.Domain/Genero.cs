using System;
using System.Collections.Generic;
using System.Text;

namespace VideoRent.Domain
{
    public class Genero
    {

        private int generoId;
        private String nombreGenero; 


        public Genero() { }

        public int GeneroId { get => generoId; set => generoId = value; }

        public string NombreGenero { get => nombreGenero; set => nombreGenero = value; }

        public Genero(int generoId, string nombreGenero)
        {
            this.generoId = generoId;
            this.nombreGenero = nombreGenero;
        }
    }
}
