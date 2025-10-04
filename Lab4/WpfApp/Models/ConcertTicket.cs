using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class ConcertTicket : Ticket
    {
        public string Sector {  get; set; }
        public string Performer { get; set; }

        public ConcertTicket(string eventName, DateTime date, string venue, string sector, string performer)
            : base (eventName, date, venue)
        {
            Sector = sector;
            Performer = performer;
        }

        public string GetPerformerInfo() => $"Виконавець: {Performer}";

        public override string GetInfo() =>
            base.GetInfo() + $", Сектор: {Sector}, {GetPerformerInfo()}";
    }
}
