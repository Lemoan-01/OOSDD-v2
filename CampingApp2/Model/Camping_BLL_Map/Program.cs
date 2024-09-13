using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Camping_UI_DescriptionPop;
using ObserverDeck;
using Camping_BLL;
using System.Windows.Media;
using System.Data;

namespace Camping_BLL_Map
{
    public class MapLogic : BaseLogic
    {
        private FilterObserver _FilterObserver;
        private int placeID = 0;
        private SpotDescriptionPop sdp;

        public MapLogic(FilterObserver FilterObserver)
        {
            _FilterObserver = FilterObserver;
        }

        public async Task LoadDataAsync()
        {
            var buttons = _FilterObserver.GetMapGridButtons();
            int bID = 0;

            foreach (Button button in buttons)
            {
                if (int.TryParse(button.Content.ToString(), out bID))
                {
                    // Asynchronously wait for the result of isBlockedOnID
                    bool isBlocked = await Task.Run(() => dbFunctions.isBlockedOnID(bID));

                    // Notify the UI observer to update the UI
                    _FilterObserver.UpdateMap(bID, isBlocked);
                }
            }
        }

        public void HandleSpotButtonClick(object sender)
        {
            try
            {
                var senderBtn = sender as Button;
                placeID = Convert.ToInt32(senderBtn.Content.ToString());

                // Get the position of the mouse relative to the current button
                Point relativePosition = Mouse.GetPosition(senderBtn);

                // Convert the relative position to the position relative to the entire window
                Point absolutePosition = senderBtn.PointToScreen(relativePosition);

                if (sdp == null || !sdp.IsVisible)
                {
                    DescriptionPop(placeID, absolutePosition); // not window detected so make new
                }
                else
                {
                    sdp.Close();
                    sdp = null; // close old window

                    DescriptionPop(placeID, absolutePosition); // after start new
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error during conversion: " + ex.Message);
            }
        }

        private void DescriptionPop(int placeID, Point clickPosition)
        {
            sdp = new SpotDescriptionPop(placeID); // Instantiate the popup window

            // Set the left and top position relative to the entire screen
            sdp.Left = clickPosition.X;
            sdp.Top = clickPosition.Y;

            // Adjust the position to align the top-left corner with the spot where you click
            sdp.Left -= sdp.ActualWidth / 2;
            sdp.Top -= sdp.ActualHeight;

            sdp.Show();
        }

        public void Filter(Grid MapGrid, DateTime FirstDates, DateTime LastDates)
        {
			Button[] mapBtns = MapGrid.Children.OfType<Button>().ToArray();

			if (mapBtns != null && FirstDates != null && LastDates != null)
				{
					if (dbFunctions.IsConnectionAvailable())
					{
						int spotCounter = 0;

						foreach (Button spot in mapBtns)
						{
							if (spot is Button button && button.Name != "btnInformation")
							{
                            bool isAvailable = dbFunctions.isAvailable(spotCounter,
                                FirstDates,
                                LastDates);

								if (!isAvailable)
								{
									spot.Background = Brushes.OrangeRed;
									spot.IsHitTestVisible = false;
								}
								else
								{
									spot.Background = Brushes.PaleGreen;
									spot.IsHitTestVisible = true;
								}
							}

							spotCounter++;
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
}
