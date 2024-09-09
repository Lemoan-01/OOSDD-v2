using Camping.BLL.HomePage;
using Camping_BLL_ReservationFilter;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Camping_UI_DescriptionPop
{
    public partial class SpotDescriptionPop : Window
    {
        private int placeID;
        private SpotDescriptionLogic spotDescriptionLogic;

        public SpotDescriptionPop(int placeID)
        {
            InitializeComponent();
            this.placeID = placeID;
            spotDescriptionLogic = new SpotDescriptionLogic();
            string spotDescription = spotDescriptionLogic.GetSpotDescription(placeID);

            Label labelSpotID = new Label()
            {
                Content = "Plek: " + placeID,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(2, 5, 0, 0),
                Height = 36,
                Width = 120,
                FontFamily = new FontFamily("Yu Gothic"),
                FontSize = 22,
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
            };
            gridPop.Children.Add(labelSpotID);

            TextBlock DescriptionBlock = new TextBlock()
            {
                Text = spotDescription,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(8, 55, 0, 0),
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 92,
                Width = 276,
                FontSize = 15,
                Foreground = Brushes.White,
            };
            gridPop.Children.Add(DescriptionBlock);

            LoadImage();
        }

        private void LoadImage() //voor individuele imgs plek
        {
            byte[] bytingImage = spotDescriptionLogic.GetSpotImage(placeID);
            BitmapImage bitmapImage = spotDescriptionLogic.ByteArrayToBitmapImage(bytingImage);

            Image displayImage = new Image()
            {
                Source = bitmapImage,
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 159,
                Width = 194,
                Margin = new Thickness(190, 55, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
            };

            gridPop.Children.Add(displayImage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name == "Cancel")
            {
                this.Close();
            }
            else
            {
                DateTime startDate = ReservationFilterLogic.FirstDates;
                DateTime endDate = ReservationFilterLogic.LastDates;

                if (!startDate.Equals(DateTime.MinValue) && !endDate.Equals(DateTime.MinValue)) //als ze allebei niet hun default-waarde zijn
                {
                    Camping.BLL.MakeReservationPage.ReservationService mk = new Camping.BLL.MakeReservationPage.ReservationService(placeID, startDate, endDate, 5);
                    //mk.Show();
                }
                else
                {
                    MessageBox.Show("Please, set your stay duration in the filter tab first!\n(top-right calendar).");
                }
            }
        }

        private void pnlSelectionBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
        }

        private void pnlTopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnTerminate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}