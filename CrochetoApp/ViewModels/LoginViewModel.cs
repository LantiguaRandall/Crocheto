using CrochetoApp._app;
using CrochetoApp.Models;
using CrochetoApp.Pages.Access;
using CrochetoApp.Pages.Home;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace CrochetoApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        public ICommand LoginCommand { get; private set; }
        public ICommand SingUpCommand { get; private set; }
        public ICommand BiometricLoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(() => Login());
            SingUpCommand = new Command(() => GoToSignUpPage());
            BiometricLoginCommand = new Command(async () => await Task.Delay(100));

        }

        [ObservableProperty]
        private User user = new User();

        public async void Login()
        {
            if (User == null) { return; }

            if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.Pass))
            {
                await Shell.Current.DisplayAlert("Validación", "Debe especificar el usuario y contraseña", "Ok");
                return;
            }


            if (VerificarUsuario(User.UserName, User.Pass) == false)
            {
                await Shell.Current.DisplayAlert("Validación", "El usuario y / o contraseña son incorrectos", "Ok");
                return;
            }

            CurrentUser.User = User;

            if (User.Estado == 0)
            {
                await Shell.Current.DisplayAlert("Validación", "El usuario está inactivo", "Ok");
                return;
            }

            if (User.Rol == 0)
            {
                await Shell.Current.GoToAsync($"{nameof(HomeClient)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(HomeAdmin)}");
            }
        }

        private async void GoToSignUpPage()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUp)}");
        }

        private bool VerificarUsuario(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string rawSql = "SELECT * FROM USUARIO WHERE USERNAME = @Valor1 and PASS = @Valor2";

                using (SqlCommand command = new SqlCommand(rawSql, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", userName);
                    command.Parameters.AddWithValue("@Valor2", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader["ID"].ToString();

                            if (!string.IsNullOrWhiteSpace(id))
                            {
                                User.Id = int.Parse(id);
                            }

                            User.Correo = reader["CORREO"].ToString();

                            var rol = reader["ROL"].ToString();

                            if (!string.IsNullOrWhiteSpace(rol))
                            {
                                User.Rol = int.Parse(rol);
                            }

                            var estado = reader["Estado"].ToString();

                            if (!string.IsNullOrWhiteSpace(estado))
                            {
                                User.Estado = int.Parse(estado);
                            }

                            return !string.IsNullOrEmpty(id);
                        }
                    }
                }
            }

            return false;
        }

        //private async Task BiometricLoginAsync()
        //{
        //    var isAuthenticated = await Services.AuthenticationService.AuthenticateAsync();
        //    if (isAuthenticated)
        //    {
        //        // Proceder a la siguiente vista o lógica de negocio
        //    }
        //    else
        //    {
        //        // Manejo de error o fallback a otro método de autenticación
        //    }
        //}
    }

   
}
