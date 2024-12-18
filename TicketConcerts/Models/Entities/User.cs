using System.Net.Sockets;

namespace TicketConcerts.Models.Entities
{
    public class User
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relación con Tickets
        public ICollection<Ticket> Tickets { get; set; }
    }
}
