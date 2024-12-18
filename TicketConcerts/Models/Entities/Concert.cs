using System.Net.Sockets;
using System;
using System.Collections.Generic;

namespace TicketConcerts.Models.Entities
{
    public class Concert
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Artist { get; set; }
        public DateTime Date { get; set; }
        public required string Location { get; set; }
        public required int Price { get; set; }  
        public int TotalTickets { get; set; } 
        public int SoldTickets { get; set; }  

        // Relación con Tickets
        public ICollection<Ticket> Tickets { get; set; }
    }
}