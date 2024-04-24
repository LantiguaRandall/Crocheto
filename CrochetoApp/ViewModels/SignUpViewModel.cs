using CrochetoApp._app;
using CrochetoApp.Models;
using CrochetoApp.Pages.Access;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace CrochetoApp.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {

        public ICommand SignUpCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public SignUpViewModel()
        {
            SignUpCommand = new Command(() => SignUp());
            LoginCommand = new Command(() => GoToLogin());
        }

        [ObservableProperty]
        private User user = new User();

        public async void SignUp()
        {
            if (User == null) { return; }

            if (string.IsNullOrWhiteSpace(User.UserName) ||
                string.IsNullOrWhiteSpace(User.Pass) ||
                string.IsNullOrWhiteSpace(User.Correo))
            {
                await Shell.Current.DisplayAlert("Validación", "Debe especificar el UserName, PassWord y Correo", "Ok");
                return;
            }

            if (VerificarExistenciaUsuario(User.UserName))
            {
                await Shell.Current.DisplayAlert("Validación", "Ese Username ya existe", "Ok");
                return;
            }

            Registrar(User);

            await Shell.Current.GoToAsync($"{nameof(Login)}");
        }

        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync($"{nameof(Login)}");
        }

        private void Registrar(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string sqlInsert = "INSERT INTO USUARIO (USERNAME, PASS, CORREO, ESTADO, ROL) VALUES (@Valor1, @Valor2, @Valor3, @Valor4, @Valor5)";

                using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", user.UserName);
                    command.Parameters.AddWithValue("@Valor2", user.Pass);
                    command.Parameters.AddWithValue("@Valor3", user.Correo);
                    command.Parameters.AddWithValue("@Valor4", 1);
                    command.Parameters.AddWithValue("@Valor5", 0);

                    command.ExecuteNonQuery();
                }
            }
        }

        private bool VerificarExistenciaUsuario(string userName)
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string rawSql = "SELECT * FROM USUARIO WHERE USERNAME = @Valor1";

                using (SqlCommand command = new SqlCommand(rawSql, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", userName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = reader["ID"].ToString();

                            return string.IsNullOrEmpty(id);
                        }
                    }
                }
            }

            return false;
        }


    }
}
