using CrochetoApp._app;
using CrochetoApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace CrochetoApp.ViewModels
{
    public partial class EditUserTViewModel : ObservableObject
    {
        public ICommand ActualizarCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }

        public EditUserTViewModel()
        {
            User = EditUserViewModel.SelectedUser;

            GoBackCommand = new Command(() => GoBack());
            ActualizarCommand = new Command(() => Actualizar());
        }
        
        [ObservableProperty]
        private User? user = new User();

        private void Actualizar()
        {
            if (User == null) { return; }

            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string sqlInsert = "UPDATE USUARIO SET USERNAME = @valor1, CORREO = @valor2 WHERE ID = @valor3";

                using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", User.UserName);
                    command.Parameters.AddWithValue("@Valor2", User.Correo);
                    command.Parameters.AddWithValue("@Valor3", User.Id);

                    command.ExecuteNonQuery();
                }
                Shell.Current.DisplayAlert("Atencion", "El usuario ha sido actualizado correctamente", "Ok");
                GoBack();
            }
        }

        private async void GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        
    }
}

