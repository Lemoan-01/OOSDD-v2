using ObserverDeck;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Camping_BLL_ReservationFilter
{
	public class ReservationFilterLogic : Camping_BLL.BaseLogic
	{
		private FilterObserver _observer;
		public DateTime FirstDates;
		public DateTime LastDates;

		public ReservationFilterLogic(FilterObserver observer)
		{
			_observer = observer;
		}

		public void HandleButtonClick(Window _window)
		{
			Camping_BLL_Map
		}
	}
}
