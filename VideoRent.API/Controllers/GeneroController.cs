using Microsoft.AspNetCore.Mvc;
using VideoRent.Business;
using VideoRent.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        }
    }



        
    
