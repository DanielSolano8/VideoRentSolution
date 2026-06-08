using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using VideoRent.Domain;

namespace VideoRent.Data
{
    public class PeliculaData

    {

        private readonly string connectionString;

        public PeliculaData(string connectionString)

        {

            this.connectionString = connectionString;

        }

        public void Insertar(Pelicula pelicula)

        {

            using (SqlConnection connection = new SqlConnection(connectionString))

            {

                connection.Open();

                // PRIMER COMMAND

                SqlCommand cmdPelicula = connection.CreateCommand();

                cmdPelicula.CommandText = "InsertPelicula";

                cmdPelicula.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parCodPelicula = new SqlParameter("@pelicula_id",

                    System.Data.SqlDbType.Int);

                parCodPelicula.Direction = System.Data.ParameterDirection.Output;

                cmdPelicula.Parameters.Add(parCodPelicula);

                cmdPelicula.Parameters.Add(new SqlParameter("@titulo", pelicula.Titulo));

                cmdPelicula.Parameters.Add(new SqlParameter("@subtitulada", pelicula.Subtitulada));

                cmdPelicula.Parameters.Add(new SqlParameter("@estreno", pelicula.Estreno));

                cmdPelicula.Parameters.Add(new SqlParameter("@generoId", pelicula.Genero.GeneroId));                // SEGUNDO COMMAND

                SqlCommand cmdPeliculaActor = connection.CreateCommand();

                cmdPeliculaActor.CommandText = "InsertPeliculaActor";

                cmdPeliculaActor.CommandType = System.Data.CommandType.StoredProcedure;

                // CREAR LA TRANSACCIÓN

                SqlTransaction transaction;

                // INICIAR UNA TRANSACCIÓN LOCAL

                transaction = connection.BeginTransaction();

                // ASIGNAR LA TRANSACCIÓN Y LA CONEXIÓN A AMBOS COMMANDS

                cmdPeliculaActor.Connection = connection;

                cmdPeliculaActor.Transaction = transaction;

                cmdPelicula.Connection = connection;

                cmdPelicula.Transaction = transaction;

                try

                {

                    // EJECUTAR EL PRIMER COMMAND

                    cmdPelicula.ExecuteNonQuery();

                    pelicula.PeliculaId = Int32.Parse(cmdPelicula.Parameters["@pelicula_id"].Value.ToString());

                    // EJECUTAR EL SEGUNDO COMMAND

                    List<Actor> actores = pelicula.Actores;

                    foreach (Actor actor in actores)

                    {

                        cmdPeliculaActor.Parameters.Add(new SqlParameter("@pelicula_id", pelicula.PeliculaId));

                        cmdPeliculaActor.Parameters.Add(new SqlParameter("@actor_id", actor.ActorId));

                        cmdPeliculaActor.ExecuteNonQuery();

                        cmdPeliculaActor.Parameters.Clear();

                    }

                    transaction.Commit();

                }

                catch (SqlException ex)

                {

                    if (transaction != null)

                        transaction.Rollback();

                    throw;

                }

            } // using

        } // Insert

    }


}