using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Camping_BLL_ReservationFilter;
using System.Windows.Media;
using ObserverDeck;

namespace Camping_UI_ReservationFilter
{
	public partial class ReservationFilter : Window, FilterObserver
	{
		private double _previousWidth;
		private double _previousHeight;
		private ReservationFilterLogic _logic;
		private int dateSelectionCounter = 0;
		private Grid _MapGrid;
		public static DateTime firstDates { get; set; }
		public static DateTime lastDates { get; set; }

		public ReservationFilter(Grid MapGrid)
		{
			InitializeComponent();
			_logic = new ReservationFilterLogic(this);
			this._MapGrid = MapGrid;

			Owner = Application.Current.MainWindow; // Set the owner to MainWindow

			Application.Current.MainWindow.LocationChanged += MainWindow_LocationChanged; // Subscribe to LocationChanged event
			Application.Current.MainWindow.SizeChanged += MainWindow_SizeChanged; // Subscribe to SizeChanged event
			Application.Current.MainWindow.StateChanged += MainWindow_StateChanged; // Subscribe to StateChanged event
			Application.Current.MainWindow.IsVisibleChanged += MainWindow_IsVisibleChanged; // Subscribe to IsVisibleChanged event

			_previousWidth = Owner.Width;
			_previousHeight = Owner.Height;
			StartDatePicker.DisplayDate = DateTime.Now;
			EndDatePicker.DisplayDate = DateTime.Now;
		}

		// Implementing UpdateMap from FilterObserver interface
		public void UpdateMap(int buttonId, bool isBlocked)
		{
			var buttons = _MapGrid.Children.OfType<Button>;
			foreach (Button button in buttons)
			{
				if (int.TryParse(button.Content.ToString(), out int bID) && bID == buttonId)
				{
					if (isBlocked)
					{
						button.IsHitTestVisible = false;
						button.Background = Brushes.Red;
					}
				}
			}
		}

		private void MainWindow_LocationChanged(object sender, EventArgs e)
		{
			Left = Owner.Left + Owner.Width + 10; // Adjust the position as needed
			Top = Owner.Top;
		}

		private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			// Calculate the difference in width and height of the main window
			double widthDiff = Owner.Width - _previousWidth;
			double heightDiff = Owner.Height - _previousHeight;

			// Adjust the position and size of the filter window
			Left += widthDiff;

			// Update the previous width and height
			_previousWidth = Owner.Width;
			_previousHeight = Owner.Height;
		}

		private void MainWindow_StateChanged(object sender, EventArgs e)
		{
			if (Owner.WindowState == WindowState.Maximized)
			{
				// Bring the filter window to the foreground when the main window is maximized
				this.Topmost = true;  // Bring to front
				this.Topmost = false; // Allow other windows to come to front
			}
		}

		private void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			// Check if the main window is now visible
			if ((bool)e.NewValue == true)
			{
				// Bring the filter window to the foreground when the main window becomes visible
				this.Topmost = true;  // Bring to front
				this.Topmost = false; // Allow other windows to come to front
			}
		}

		private void btnTerminate_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			DatePicker dateP = (DatePicker)sender;
			DateTime date = (DateTime)dateP.SelectedDate;

			if (dateSelectionCounter % 2 == 0)
			{
				firstDates = date;
				dateSelectionCounter++; // maak getal oneven
			}
			else
			{
				lastDates = date;
				TimeSpan diff = date - firstDates; // Gebruik TimeSpan om het verschil te berekenen
				int stayLengthDays = diff.Days;

				if (diff > TimeSpan.Zero)
				{
					LblStayDuration.Content = "Selected " + stayLengthDays + " days";
				}
				else
				{
					MessageBox.Show("You can't stay for " + stayLengthDays + " days");
				}
				dateSelectionCounter++; // maak getal weer even
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button btn = (Button)sender;

			if (btn.Name == "applyFilter")
			{
				_logic.HandleButtonClick(this);
			}
		}
	}
}
