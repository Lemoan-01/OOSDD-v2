using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Camping_UI_DescriptionPop;
using ObserverDeck;
using Camping_BLL;

namespace Camping_BLL_Map
{
    public class ReservationLogic : BaseLogic
    {
        private readonly UIObserver _uiObserver;
        private int placeID = 0;
        private SpotDescriptionPop sdp;

        public ReservationLogic(UIObserver uiObserver)
        {
            _uiObserver = uiObserver;
        }

        public async Task LoadDataAsync()
        {
            var buttons = _uiObserver.GetMapGridButtons();
            int bID = 0;

            foreach (Button button in buttons)
            {
                if (int.TryParse(button.Content.ToString(), out bID))
                {
                    // Asynchronously wait for the result of isBlockedOnID
                    bool isBlocked = await Task.Run(() => dbFunctions.isBlockedOnID(bID));

                    // Notify the UI observer to update the UI
                    _uiObserver.UpdateMap(bID, isBlocked);
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
    }
}
