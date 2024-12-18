using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketConcerts.Data;

namespace TicketConcerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ConcertsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allConcerts = dbContext.Concerts.ToList();
            return Ok(allConcerts);
        }

    }
}
