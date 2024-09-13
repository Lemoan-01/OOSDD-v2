using ObserverDeck;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Camping_UI_ReservationFilter;

namespace Camping_UI_Views_UserControls
{
	public partial class Map : UserControl
	{
		private Camping_BLL_Map.MapLogic _logic;
		private bool reservationFilterOpen = false;
		private ReservationFilter reservationFilter;
		private static Map _instance;

		public event EventHandler LoadingCompleted;

		public Map()
		{
			this.DataContext = new ViewModels.ReservationVM();
			InitializeComponent();

			// Pass 'this' as FilterObserver to MapLogic
			_logic = new Camping_BLL_Map.MapLogic(this);

			_instance = this;
		}

		public static Map Instance => _instance;

		private void spotBtn(object sender, RoutedEventArgs e)
		{
			_logic.HandleSpotButtonClick(sender);
		}

		private void btnInformation_Click(object sender, RoutedEventArgs e)
		{
			if (!reservationFilterOpen)
			{
				reservationFilter = new ReservationFilter(MapGrid);
				reservationFilter.Closed += ReservationFilter_Closed;
				reservationFilterOpen = true;
				reservationFilter.Owner = Window.GetWindow(this) as Window;
				reservationFilter.Show();
			}
		}

		private void ReservationFilter_Closed(object sender, EventArgs e)
		{
			reservationFilterOpen = false;
			reservationFilter.Closed -= ReservationFilter_Closed;
		}
	}
}
