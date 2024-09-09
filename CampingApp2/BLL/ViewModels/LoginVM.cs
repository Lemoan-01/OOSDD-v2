using System.Windows.Input;
using Menu;
using NotificationService;

namespace ViewModels
{
    class LoginVM : ViewModelBase
    {
        public ICommand NavigateToRegisterCommand { get; }

        public LoginVM()
        {
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
        }

        private void NavigateToRegister(object obj)
        {
            var navigationVM = App.Current.MainWindow.DataContext as NavigationVM;
            navigationVM.CurrentView = new RegisterVM();
        }
    }
}
