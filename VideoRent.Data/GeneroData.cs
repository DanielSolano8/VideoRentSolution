using Microsoft.Data.SqlClient;
using VideoRent.Domain;

namespace VideoRent.Data
{
    public class GeneroData
    {
        private readonly string connectionString;

        public GeneroData(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            var generos = new List<Genero>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT genero_id, nombre_genero FROM Genero order by nombre_genero ASC", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var genero = new Genero
                        {
                            GeneroId = reader.GetInt32(0),
                            NombreGenero = reader.GetString(1)
                        };
                        generos.Add(genero);
                    }
                }
            }

            return generos;
        }
    }
}