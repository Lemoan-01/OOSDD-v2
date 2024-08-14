using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Camping.DataAccess.Functions;
using Camping.UI.DescriptionPop;

namespace Camping_BLL_Map
{
    public class ReservationLogic
    {
        private readonly MainWindow _reservation;
        private int placeID = 0;
        private SpotDescriptionPop sdp;

        public ReservationLogic(Reservation reservation)
        {
            _reservation = reservation;
        }

        public async Task LoadDataAsync()
        {
            DBFunctions dbFunc = new DBFunctions();
            var buttons = _reservation.GetMapGrid().Children.OfType<Button>();
            int bID = 0;

            foreach (var button in buttons)
            {
                if (int.TryParse(button.Content.ToString(), out bID))
                {
                    // Asynchronously wait for the result of isBlockedOnID
                    bool isBlocked = await Task.Run(() => dbFunc.isBlockedOnID(bID));

                    if (isBlocked)
                    {
                        // UI updates need to be performed on the UI thread
                        _reservation.Dispatcher.Invoke(() =>
                        {
                            button.IsHitTestVisible = false;
                            button.Background = Brushes.Red;
                        });
                    }
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
