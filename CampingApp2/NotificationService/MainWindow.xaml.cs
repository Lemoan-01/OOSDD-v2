using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotificationService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void ShowError(string message)
        {
            MessageText.Text = message;

            gridNotif.Background = Brushes.OrangeRed; //differetiation 4 now
            InitializeComponent();
        }
        public void ShowInfo(string message)
        {
            MessageText.Text = message;
            InitializeComponent();
        }
    }
}