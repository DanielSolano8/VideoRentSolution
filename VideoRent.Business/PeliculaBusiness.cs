using System;
using System.Collections.Generic;
using System.Text;
using VideoRent.Data;
using VideoRent.Domain;

namespace VideoRent.Business
{
    public class PeliculaBusiness
    {
        private PeliculaData peliculaData;
        public PeliculaBusiness(string connectionString)
        {
            this.peliculaData = new PeliculaData(connectionString);
        }
        public void Insertar(Pelicula pelicula)
        {
            if (pelicula.Actores.Count >= 1)
            {
                this.peliculaData.Insertar(pelicula);
            }
            else
            {
                //TODO: Lanzar una excepción personalizada indicando que la película debe tener al menos un actor.
            }
        }
    }
}