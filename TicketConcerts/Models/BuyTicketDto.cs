namespace TicketConcerts.Models
{
    public class BuyTicketDto
    {
        public required int UserId { get; set; }
        public required int ConcertId { get; set; }
        public required int Quantity { get; set; }
    }
}
