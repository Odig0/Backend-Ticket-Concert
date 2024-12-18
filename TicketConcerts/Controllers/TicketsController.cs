using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketConcerts.Data;
using TicketConcerts.Models;
using TicketConcerts.Models.Entities;

namespace TicketConcerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TicketsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetTickets()
        {
            var allConcertsTickets = dbContext.Tickets.ToList();
            return Ok(allConcertsTickets);
        }
        [HttpPost]
        public IActionResult Buy(BuyTicketDto buyTicketDto)
        {

            var concert = dbContext.Concerts.FirstOrDefault(c => c.Id == buyTicketDto.ConcertId);
            var userExists = dbContext.Users.Any(u => u.Id == buyTicketDto.UserId);

            if (!userExists)
            {
                return BadRequest($"El usuario con Id {buyTicketDto.UserId} no existe.");
            }

            if (concert == null)
            {
                return NotFound("Concierto no encontrado.");
            }

            if (concert.SoldTickets + buyTicketDto.Quantity > concert.TotalTickets)
            {
                return BadRequest("No hay suficientes tickets disponibles.");
            }


            // 3. Generar ticketId: Nombre del concierto + número incremental
            var existingTickets = dbContext.Tickets
            .Where(t => t.ConcertId == buyTicketDto.ConcertId)
            .ToList();

            int nextNumber = existingTickets.Count + 1;
            string ticketId = $"{concert.Name.Replace(" ", "")}{nextNumber}";


            var ticketEntity = new Ticket
            {
                Id = ticketId,
                UserId = buyTicketDto.UserId,
                ConcertId = buyTicketDto.ConcertId,
                Quantity = buyTicketDto.Quantity,
                PurchaseDate = DateTime.UtcNow
            };
            dbContext.Tickets.Add(ticketEntity);

            concert.SoldTickets += buyTicketDto.Quantity;

            dbContext.SaveChanges();

            return Ok(new
            {
                Message = "TicMicrosoft.EntityFrameworkCore.DbUpdateException: 'An error occurred while saving the entity changes. See the inner exception for details.'ket comprado exitosamente.",
                Ticket = new
                {
                    ticketEntity.Id,
                    ticketEntity.UserId,
                    ticketEntity.ConcertId,
                    ticketEntity.Quantity,
                    ticketEntity.PurchaseDate
                }
            });
        }
    }
}

