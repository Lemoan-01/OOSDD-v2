using Camping_BLL_ReservationFilter;
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

namespace Camping.UI.ReservationFilter
{
    /// <summary>
    /// Interaction logic for ReservationFilter.xaml
    /// </summary>
    public partial class ReservationFilter : Window
    {
        private double _previousWidth;
        private double _previousHeight;
        private ReservationFilterLogic _logic;

        public ReservationFilter()
        {
            InitializeComponent();
            _logic = new ReservationFilterLogic(this);

            Owner = Application.Current.MainWindow; // Set the owner to MainWindow
            _logic.UpdatePosition(); // Set initial position

            Application.Current.MainWindow.LocationChanged += MainWindow_LocationChanged; // Subscribe to LocationChanged event
            Application.Current.MainWindow.SizeChanged += MainWindow_SizeChanged; // Subscribe to SizeChanged event
            Application.Current.MainWindow.StateChanged += MainWindow_StateChanged; // Subscribe to StateChanged event
            Application.Current.MainWindow.IsVisibleChanged += MainWindow_IsVisibleChanged; // Subscribe to IsVisibleChanged event

            _previousWidth = Owner.Width;
            _previousHeight = Owner.Height;
            StartDatePicker.DisplayDate = DateTime.Now;
            EndDatePicker.DisplayDate = DateTime.Now;
        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            _logic.UpdatePosition(); // Update position when MainWindow's location changes
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Adjust the position and size of the filter window
            _logic.HandleMainWindowSizeChanged(e, ref _previousWidth, ref _previousHeight);
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            _logic.HandleMainWindowStateChanged();
        }

        private void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _logic.HandleMainWindowIsVisibleChanged(e);
        }

        private void btnTerminate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _logic.HandleDateSelection((DatePicker)sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _logic.HandleButtonClick();
        }
    }
}