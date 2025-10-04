using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class BookingSystem
    {
        List<Ticket> tickets = new List<Ticket>();

        public void AddTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }

        public int GetTotalBookedToday(DateTime date)
        {
            int count = 0;

            foreach (Ticket ticket in tickets)
            {
                if (ticket.Date.Date == date.Date && ticket.IsBooked) 
                    count++;
            }

            return count;
        }
    }
}
