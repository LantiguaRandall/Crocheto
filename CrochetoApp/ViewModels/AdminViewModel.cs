using CrochetoApp.Pages.Home;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrochetoApp.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        public ICommand GestionarUsuariosCommand { get; private set; }

        public AdminViewModel()
        {
            GestionarUsuariosCommand = new Command(() => GoToUsersPage());
        }

        private async void GoToUsersPage()
        {
            await Shell.Current.GoToAsync($"{nameof(EditUser)}");
        }
    }

    
}
