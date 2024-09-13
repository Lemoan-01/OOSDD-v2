using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Reflection.Emit;
using Camping_BLL;
using ObserverDeck;

namespace Camping_BLL_ReservationFilter
{
    public class ReservationFilterLogic : BaseLogic
    {
        private readonly Window _window;
        private int _dateSelectionCounter = 0;
        private UIObserver uiObserver;

		public static DateTime FirstDates { get; set; }
        public static DateTime LastDates { get; set; }

        public ReservationFilterLogic(Window window, UIObserver _UIObserver)
        {
            _window = window;
            uiObserver = _UIObserver;
        }

        public void UpdatePosition()
        {
            _window.Left = _window.Owner.Left + _window.Owner.Width + 10; // Adjust the position as needed
            _window.Top = _window.Owner.Top;
        }

        public void HandleMainWindowSizeChanged(SizeChangedEventArgs e, ref double previousWidth, ref double previousHeight)
        {
            double widthDiff = _window.Owner.Width - previousWidth;
            double heightDiff = _window.Owner.Height - previousHeight;

            _window.Left += widthDiff;

            previousWidth = _window.Owner.Width;
            previousHeight = _window.Owner.Height;
        }

        public void HandleMainWindowStateChanged()
        {
            if (_window.Owner.WindowState == WindowState.Maximized)
            {
                _window.Topmost = true;
                _window.Topmost = false;
            }
        }

        public void HandleMainWindowIsVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                _window.Topmost = true;
                _window.Topmost = false;
            }
        }

        public void HandleDateSelection(DatePicker dateP)
        {
            DateTime date = (DateTime)dateP.SelectedDate;

            if (_dateSelectionCounter % 2 == 0)
            {
                FirstDates = date;
                _dateSelectionCounter++;
            }
            else
            {
                LastDates = date;
                TimeSpan diff = date - FirstDates;
                int stayLengthDays = diff.Days;

                if (diff > TimeSpan.Zero)
                {
                    ((System.Windows.Controls.Label)_window.FindName("LblStayDuration")).Content = "Selected " + stayLengthDays + " days";
                }
                else
                {
                    MessageBox.Show("You can't stay for " + stayLengthDays + " days");
                }
                _dateSelectionCounter++;
            }
        }

		public void HandleButtonClick(Window _window)
		{
			// Use _observer instead of directly accessing the Map
			var mapGrid = uiObserver.GetMapGridButtons();
			if (mapGrid != null && ((DatePicker)_window.FindName("StartDatePicker")).SelectedDate != null && ((DatePicker)_window.FindName("EndDatePicker")).SelectedDate != null)
			{
				if (dbFunctions.IsConnectionAvailable())
				{
					int btnCounter = 0;

					foreach (var button in mapGrid)
					{
						if (button.Name != "btnInformation")
						{
							bool isAvailable = dbFunctions.isAvailable(btnCounter, (DateTime)((DatePicker)_window.FindName("StartDatePicker")).SelectedDate, (DateTime)((DatePicker)_window.FindName("EndDatePicker")).SelectedDate);
							if (!isAvailable)
							{
								button.Background = Brushes.OrangeRed;
								button.IsHitTestVisible = false;
							}
							else
							{
								button.Background = Brushes.PaleGreen;
								button.IsHitTestVisible = true;
							}
						}

						btnCounter++;
					}
				}
				else
				{
					MessageBox.Show("No database connection. Please check your connection and try again.");
				}
			}
			else
			{
				MessageBox.Show("The reservation window is closed, or dates have not been selected.");
			}
		}
	}
}
