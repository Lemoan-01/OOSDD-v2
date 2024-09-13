using System.Collections.Generic;
using System.Windows.Controls;

namespace ObserverDeck
{
	public interface FilterObserver
	{
		/// <summary>
		/// Updates the UI map, marking specific buttons as blocked or available.
		/// </summary>
		/// <param name="buttonId">The ID of the button to be updated.</param>
		/// <param name="isBlocked">True if the spot is blocked, false if available.</param>
		void UpdateMap(int buttonId, bool isBlocked);

		void GetMapGrid(Grid grid);
	}
}
