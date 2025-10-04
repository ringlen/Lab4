using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class MovieTicket : Ticket
    {
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }

        public MovieTicket(string eventName, DateTime date, string venue, string genre, int durationMinutes)
            : base(eventName, date, venue)
        {
            Genre = genre;
            DurationMinutes = durationMinutes;
        }

        public int CalculateDuration()
        {
            return DurationMinutes;
        }

        public override string GetInfo() =>
            base.GetInfo() + $", Жанр: {Genre}, Тривалість: {DurationMinutes} хв";

        public override string ToString()
        {
            return $"{EventName} ({Genre}) - {Date:d}";
        }
    }
}
