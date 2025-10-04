using WpfApp.Models;

namespace UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void GetTotalBookedToday_ShouldReturnCorrectCount()
        {
            var bookingSystem = new BookingSystem();

            Ticket t1 = new ConcertTicket("Rock Nigth", DateTime.Today, "Arena", "A1", "Queen");
            Ticket t2 = new MovieTicket("Dune 2", DateTime.Today, "Cinema Lux", "Sci-Fi", 150);
            Ticket t3 = new MovieTicket("Old Movie", DateTime.Today.AddDays(-1), "Retro Hall", "Drama", 100);

            t1.Book();
            t2.Book();

            bookingSystem.AddTicket(t1);
            bookingSystem.AddTicket(t2);
            bookingSystem.AddTicket(t3);

            int result = bookingSystem.GetTotalBookedToday(DateTime.Today);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Book_ShouldSetIsBookedTrue()
        {
            Ticket t = new Ticket("Rock Nigth", DateTime.Today, "Arena");

            t.Book();

            Assert.IsTrue(t.IsBooked);
        }

        [TestMethod]
        public void Book_ShouldSetIsBookedFalse()
        {
            Ticket t = new Ticket("Rock Nigth", DateTime.Today, "Arena");

            t.CancelBooking();

            Assert.IsFalse(t.IsBooked);
        }

        [TestMethod]
        public void GetInfo_ShouldContailAllConcertDetails()
        {
            Ticket concertTicket = new ConcertTicket("Rock Nigth", DateTime.Today, "Arena", "A1", "Queen");

            string result = concertTicket.GetInfo();

            StringAssert.Contains(result, "A1");
            StringAssert.Contains(result, "Queen");
        }

        [TestMethod]
        public void GetTotalBookedToday_ShouldReturnZero_WhenNoneBooked()
        {
            var bookingSystem = new BookingSystem();

            Ticket t1 = new ConcertTicket("Rock Nigth", DateTime.Today.AddDays(-1), "Arena", "A1", "Queen");
            Ticket t2 = new MovieTicket("Dune 2", DateTime.Today.AddDays(-1), "Cinema Lux", "Sci-Fi", 150);
            Ticket t3 = new MovieTicket("Old Movie", DateTime.Today.AddDays(-1), "Retro Hall", "Drama", 100);

            t1.Book();
            t2.Book();

            bookingSystem.AddTicket(t1);
            bookingSystem.AddTicket(t2);
            bookingSystem.AddTicket(t3);

            int result = bookingSystem.GetTotalBookedToday(DateTime.Today);

            Assert.AreEqual(0, result);
        }
    }
}
