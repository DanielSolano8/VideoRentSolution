using Microsoft.AspNetCore.Mvc;
using VideoRent.Business;
using VideoRent.Domain;

namespace VideoRent.API.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly PeliculaBusiness peliculaBusiness;

        // Constructor - Inyección de dependencia
        public PeliculaController(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:VideoRentDB"];
            this.peliculaBusiness = new PeliculaBusiness(connectionString);
        }

        /// <summary>
        /// Gets a specific pelicula by ID.
        /// GET: api/pelicula/{id}
        /// </summary>
        /// <param name="id">The ID of the pelicula to retrieve.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pelicula), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Pelicula> GetPeliculaById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pelicula ID must be a positive integer.");
            }

            try
            {
                // TODO: Implement GetPeliculaById in PeliculaBusiness
                return StatusCode(
                    StatusCodes.Status501NotImplemented,
                    "GetPeliculaById method not yet implemented in PeliculaBusiness."
                );
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database."
                );
            }
        }

        /// <summary>
        /// Creates a new pelicula.
        /// POST: api/pelicula
        /// </summary>
        /// <param name="pelicula">The pelicula object to create.</param>
        [HttpPost]
        [ProducesResponseType(typeof(Pelicula), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Pelicula> AddPelicula([FromBody] Pelicula pelicula)
        {
            // TODO: Improve with a DTO

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pelicula == null)
            {
                return BadRequest("Pelicula data is required.");
            }

            if (string.IsNullOrWhiteSpace(pelicula.Titulo))
            {
                return BadRequest("Pelicula's title is required.");
            }

            if (pelicula.Genero == null || pelicula.Genero.GeneroId <= 0)
            {
                return BadRequest("Pelicula must have a valid genre.");
            }

            if (pelicula.Actores == null || pelicula.Actores.Count < 1)
            {
                return BadRequest("Pelicula must have at least one actor.");
            }

            try
            {
                // The Insertar method in PeliculaBusiness will set the PeliculaId
                peliculaBusiness.Insertar(pelicula);

                // Returns 201 Created status and a link to the newly created resource
                return CreatedAtAction(
                    nameof(GetPeliculaById),
                    new { id = pelicula.PeliculaId },
                    pelicula
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.ToString()
                );
            }
        }

        // GET: api/pelicula
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET: api/pelicula/5
        [HttpGet("test/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/pelicula
        [HttpPost("test")]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/pelicula/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/pelicula/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}