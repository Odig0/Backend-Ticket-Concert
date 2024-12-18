using System;

namespace TicketConcerts.Models.Entities
{
    public class Ticket
    {
        public required string Id { get; set; }  
        public required int UserId { get; set; } 
        public required int ConcertId { get; set; }  
        public required int Quantity { get; set; }  
        public DateTime PurchaseDate { get; set; } = DateTime.Now;  

        public User User { get; set; } 
        public Concert Concert { get; set; }  
    }
}
