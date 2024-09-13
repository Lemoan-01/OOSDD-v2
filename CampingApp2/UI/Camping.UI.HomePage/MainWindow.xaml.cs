using Camping_BLL_Index;
using System;
using System.Windows;
using System.Windows.Input;

namespace Camping_UI_Index
{
    public partial class MainWindow : Window
    {
        public static int userID { get; internal set; } = -1; // default value for guests
        private bool isAdmin = false;

        private BLLIndex businessLogic;

        public MainWindow()
        {
            this.DataContext = new ViewModels.NavigationVM();

            InitializeComponent();

            accInfo.Visibility = Visibility.Collapsed;
            btnAdminPage.Visibility = Visibility.Collapsed;
            businessLogic = new BLLIndex();
        }

        public MainWindow(int _userID, bool isAdmin)
        {
            InitializeComponent();
            userID = _userID;
            this.isAdmin = isAdmin;
            businessLogic = new BLLIndex();
            string email = businessLogic.GetUserEmail(userID);

            if (userID != -1)
            {
                loginBtn.Visibility = Visibility.Collapsed;
                accInfo.Visibility = Visibility.Visible;
                btnAdminPage.Visibility = Visibility.Collapsed;

                lblLoggedIn.Content = "Your email = " + email;
            }
            if (userID != -1 && isAdmin != false)
            {
                loginBtn.Visibility = Visibility.Collapsed;
                accInfo.Visibility = Visibility.Visible;
                btnAdminPage.Visibility = Visibility.Visible;

                lblLoggedIn.Content = "Your email = " + email;
            }
        }

        public void closeHomePage()
        {
            this.Close();
        }

        private void pnlSelectionBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
            if (e.LeftButton == MouseButtonState.Pressed && this.WindowState == WindowState.Maximized)
            {
                // Exit fullscreen mode
                this.WindowState = WindowState.Normal;
            }
        }

        private void pnlTopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
            if (e.LeftButton == MouseButtonState.Pressed && this.WindowState == WindowState.Maximized)
            {
                // Exit fullscreen mode
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnFullscreen_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        private void btnTerminate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Environment.Exit(1);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
