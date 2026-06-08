using VideoRent.Domain;
using VideoRent.Data;

namespace VideoRent.Business
{
    public class GeneroBusiness
    {
        private readonly GeneroData generoData;

        public GeneroBusiness(string connectionString)
        {
            this.generoData = new GeneroData(connectionString);
        }

        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            return await this.generoData.GetGeneros();
        }
    }
}
