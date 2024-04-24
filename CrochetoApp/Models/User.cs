using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CrochetoApp.Models
{
    public partial class User : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string correo;

        [ObservableProperty]
        public string pass;

        [ObservableProperty]
        public int rol;

        [ObservableProperty]
        public int estado;

        [ObservableProperty]
        public string userName;
    }
}
