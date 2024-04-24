using CrochetoApp._app;
using CrochetoApp.Models;
using CrochetoApp.Pages.Home;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace CrochetoApp.ViewModels
{
    public partial class EditUserViewModel : ObservableObject
    {
        public static User? SelectedUser { get; set; }

        public ICommand ItemTappedCommand { get; private set; }

        public EditUserViewModel()
        {
            ItemTappedCommand = new Command<User>(HandleItemTapped);
            LoadUsers();
        }

        [ObservableProperty]
        private List<User> users = new List<User>();


        private void LoadUsers()
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string rawSql = "SELECT * FROM USUARIO";

                using (SqlCommand command = new SqlCommand(rawSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User();

                            user.Correo = reader["CORREO"].ToString();
                            user.Pass = reader["PASS"].ToString();
                            user.UserName = reader["USERNAME"].ToString();

                            if (int.TryParse(reader["ID"].ToString(), out int id))
                            {
                                user.Id = id;
                            }

                            if (int.TryParse(reader["ROL"].ToString(), out int rol))
                            {
                                user.Rol = rol;
                            }

                            if (int.TryParse(reader["ESTADO"].ToString(), out int estado))
                            {
                                user.Estado = estado;
                            }

                            Users.Add(user);
                        }
                    }
                }
            }
        }



        private async void HandleItemTapped(User user)
        {
            SelectedUser = user;
            await Shell.Current.GoToAsync($"{nameof(EditUserT)}");
        }
    }
}

