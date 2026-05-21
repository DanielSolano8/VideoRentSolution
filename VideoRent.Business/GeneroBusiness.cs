using VideoRent.Domain;
using VideoRent.Data;


namespace VideoRent.Business
{
    public class GeneroBusiness
    {

        private GeneroData generoData;

        public GeneroBusiness(String connectionString)
        {
            generoData = new GeneroData(connectionString);

        }

        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            return await generoData.GetGeneros();

        }

        }
}
