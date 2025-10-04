using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class Ticket
    {
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public bool IsBooked { get; private set; }

        public Ticket(string eventName, DateTime date, string venue)
        {
            EventName = eventName;
            Date = date;
            Venue = venue;
            IsBooked = false;
        }

        public void Book()
        {
            IsBooked = true;
        }

        public void CheckBooking()
        {
            if (!IsBooked)
            {
                throw new InvalidOperationException("Бронювання скасовано");
            }
        }

        public void CancelBooking()
        {
            IsBooked = false;
        }

        public virtual string GetInfo()
        {
            return $"{EventName} - {Date:d} - {Venue}";
        }

        public override string ToString()
        {
            return $"{EventName} - {Date:d}";
        }
    }
}
