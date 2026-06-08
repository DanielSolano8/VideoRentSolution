using Microsoft.AspNetCore.Mvc;
using VideoRent.Business;
using VideoRent.Domain;

namespace VideoRent.API.Controllers
{
    [Route("api/genero")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public GeneroController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Genero>> Get()
        {
            var connectionString = configuration["ConnectionStrings:VideoRentDB"];
            GeneroBusiness generoBusiness = new GeneroBusiness(connectionString);
            IEnumerable<Genero> geneneros = await generoBusiness.GetGeneros();
            return geneneros;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}