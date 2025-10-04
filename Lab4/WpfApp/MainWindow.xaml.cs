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
using WpfApp.Models;

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
                MessageBox.Show("Усі поля мають бути заповнені",
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
                    MessageBox.Show("Усі поля мають бути заповнені",
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
                    MessageBox.Show("Усі поля мають бути заповнені (жанр і коректна тривалість).",
                                    "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ticket = new MovieTicket(eventName, date, venue, genre, duration);
            }

            ticket.Book();
            bookingSystem.AddTicket(ticket);

            TicketsListBox.Items.Add(ticket);

            TicketsTodayTextBlock.Text = bookingSystem.GetTotalBookedToday(DateTime.Today).ToString();

            ClearInputs();
        }
            private void ClearInputs()
            {
                EventNameTextBox.Clear();
                VenueTextBox.Clear();
                SectorTextBox.Clear();
                PerformerTextBox.Clear();
                GenreTextBox.Clear();
                DurationTextBox.Clear();
                EventDatePicker.SelectedDate = DateTime.Today;
            }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsListBox.SelectedItem is not Ticket selected)
            {
                MessageBox.Show("Оберіть квиток для перегляду.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string info = selected.GetInfo();
            MessageBox.Show(info, "Деталі квитка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelBooking_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsListBox.SelectedItem is not Ticket selected)
            {
                MessageBox.Show("Оберіть квиток для скасування.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            selected.CancelBooking();
            bookingSystem.RemoveTicket(selected);
            TicketsListBox.Items.Remove(selected);
            TicketsTodayTextBlock.Text = bookingSystem.GetTotalBookedToday(DateTime.Today).ToString();

            MessageBox.Show("Бронювання скасовано.", "Успішно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TicketsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // використовується для ViewDetails_Click
        }
    }
 }