﻿using Menu;
using System.Windows.Input;
using NotificationService;

namespace ViewModels
{
    public class RegisterVM : ViewModelBase
    {
        public ICommand NavigateToLoginCommand { get; }

        public RegisterVM()
        {
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
        }

        private void NavigateToLogin(object obj)
        {
            var navigationVM = App.Current.MainWindow.DataContext as NavigationVM;
            navigationVM.CurrentView = new LoginVM();
        }
    }
}
