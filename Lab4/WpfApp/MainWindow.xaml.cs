using Lab4.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly BookingSystem bookingSystem = new BookingSystem();

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EventDatePicker.SelectedDate = DateTime.Today;
        }

        private void TicketTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConcertPanel == null || MoviePanel == null)
                return;

            if (TicketTypeComboBox.SelectedIndex == 0) // Concert
            {
                ConcertPanel.Visibility = Visibility.Visible;
                MoviePanel.Visibility = Visibility.Collapsed;
            }
            else // Movie
            {
                ConcertPanel.Visibility = Visibility.Collapsed;
                MoviePanel.Visibility = Visibility.Visible;
            }
        }

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            string eventName = EventNameTextBox.Text;
            DateTime date = EventDatePicker.SelectedDate ?? DateTime.Today;
            string venue = VenueTextBox.Text;

            if (string.IsNullOrEmpty(eventName) || date == DateTime.MinValue || string.IsNullOrEmpty(venue))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля: назву події, дату та місце проведення.",
                                "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Ticket ticket;

            if (TicketTypeComboBox.SelectedIndex == 0) // Concert
            {
                string sector = SectorTextBox.Text;
                string performer = PerformerTextBox.Text;

                if (string.IsNullOrEmpty(sector) || string.IsNullOrEmpty(performer))
                {
                    MessageBox.Show("Будь ласка, заповніть усі поля для квитка на концерт (сектор і виконавець).",
                                    "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ticket = new ConcertTicket(eventName, date, venue, sector, performer);
            }
            else // Movie
            {
                string genre = GenreTextBox.Text;
                bool validDuration = int.TryParse(DurationTextBox.Text, out int duration);

                if (string.IsNullOrEmpty(genre) || !validDuration || duration <= 0)
                {
                    MessageBox.Show("Будь ласка, заповніть усі поля для квитка в кіно (жанр і коректна тривалість).",
                                    "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ticket = new MovieTicket(eventName, date, venue, genre, duration);
            }

            ticket.Book();
            bookingSystem.AddTicket(ticket);

            TicketsTodayTextBlock.Text = bookingSystem.GetTotalBookedToday(DateTime.Today).ToString();

            EventNameTextBox.Clear();
            VenueTextBox.Clear();
            SectorTextBox.Clear();
            PerformerTextBox.Clear();
            GenreTextBox.Clear();
            DurationTextBox.Clear();
            EventDatePicker.SelectedDate = DateTime.Today;
        }
    }
}