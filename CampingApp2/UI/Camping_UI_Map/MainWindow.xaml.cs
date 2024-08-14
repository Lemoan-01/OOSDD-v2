﻿using Camping_BLL_Map;
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

namespace Camping_UI_Map
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        private ReservationLogic _logic;
        private bool reservationFilterOpen = false;
        private Windows.ReservationFilter reservationFilter;
        private static MainWindow _instance;

        public event EventHandler LoadingCompleted;

        public MainWindow()
        {
            InitializeComponent();
            _logic = new ReservationLogic(this);
            LoadDataAsync();

            _instance = this;
        }

        public static MainWindow Instance
        {
            get { return _instance; }
        }

        public Grid GetMapGrid()
        {
            return MapGrid;
        }

        public async void LoadDataAsync()
        {
            // Show the loading screen when loading starts
            imgEmptyView.Visibility = Visibility.Visible;
            loadingScreen.Visibility = Visibility.Visible;

            try
            {
                await _logic.LoadDataAsync();
            }
            finally
            {
                // Hide the loading screen when loading is complete (even if there's an exception)
                loadingScreen.Visibility = Visibility.Collapsed;
                imgEmptyView.Visibility = Visibility.Collapsed;
            }
        }

        private void spotBtn(object sender, RoutedEventArgs e)
        {
            _logic.HandleSpotButtonClick(sender);
        }

        private void btnInformation_Click(object sender, RoutedEventArgs e)
        {
            // Check if the reservation filter window is already open
            if (!reservationFilterOpen)
            {
                // Create a new instance of the reservation filter window
                reservationFilter = new Windows.ReservationFilter();
                reservationFilter.Closed += ReservationFilter_Closed; // Subscribe to the Closed event
                reservationFilterOpen = true; // Mark the reservation filter window as open
                                              // Set the main window as the owner
                reservationFilter.Owner = Window.GetWindow(this) as Window;
                reservationFilter.Show(); // Use Show instead of ShowDialog
            }
        }

        private void ReservationFilter_Closed(object sender, EventArgs e)
        {
            // When the reservation filter window is closed, enable the button and mark the window as closed
            reservationFilterOpen = false;
            reservationFilter.Closed -= ReservationFilter_Closed; // Unsubscribe from the Closed event
        }
    }
}