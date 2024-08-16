using System;
using System.Windows;
using System.Windows.Controls;
using Camping.BLL.MakeReservationPage;

namespace Camping.UI.MakeReservationPage
{
    public partial class MainWindow : Window
    {
        private int maxPeoplePerPlace = 12;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 1; i <= maxPeoplePerPlace; i++)
            {
                aantPersonen.Items.Add(i);
            }
        }

        public void AddElement(UIElement element)
        {
            MainWindowGrid.Children.Add(element);
        }

        public void EditElementContent(string elementName, string edit)
        {
            var element = MainWindowGrid.FindName(elementName);

            if (element != null)
            {
                var type = element.GetType();
                var contentProperty = type.GetProperty("Content");

                if (contentProperty != null)
                {
                    contentProperty.SetValue(element, edit);
                }
                else
                {
                    // Handle cases where the element does not have a Content property
                    MessageBox.Show($"{elementName} does not have a Content property");
                }
            }
            else
            {
                // Handle cases where the element is not found
                MessageBox.Show($"{elementName} not found in the grid");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button; //computer zeurt over nullable maar mnu

            if (button.Name == "Cancel")
            {
                this.Close();
            }
            else if (button.Name == "Pay")
            {
                if (aantPersonen.SelectedItem != null)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://www.paypal.com/us/home",
                        UseShellExecute = true
                    });

                    int aantalPersonen = (int)aantPersonen.SelectedItem;
                    new ReservationService(1, DateTime.Now, DateTime.Now.AddDays(1), aantalPersonen);
                }
            }
        }
    }
}
