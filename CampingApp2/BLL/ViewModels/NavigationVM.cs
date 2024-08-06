using System.Windows.Input;

namespace ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ReservationCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand AdminCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Reservation(object obj) => CurrentView = new ReservationVM();
        private void Login(object obj) => CurrentView = new LoginVM();
        private void Account(object obj) => CurrentView = new AccountVM();
        private void Register(object obj) => CurrentView = new RegisterVM();
        private void Admin(object obj) => CurrentView = new AdminVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            ReservationCommand = new RelayCommand(Reservation);
            LoginCommand = new RelayCommand(Login);
            AccountCommand = new RelayCommand(Account);
            RegisterCommand = new RelayCommand(Register);
            AdminCommand = new RelayCommand(Admin);

            // This sets the Homepage as default on startup
            CurrentView = new HomeVM();
        }
    }
}
