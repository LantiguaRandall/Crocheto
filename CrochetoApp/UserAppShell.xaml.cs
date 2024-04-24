using CrochetoApp.Pages.Access;
using CrochetoApp.Pages.Home;

namespace CrochetoApp
{
    public partial class UserAppShell : Shell
    {
        public UserAppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(HomeClient), typeof(HomeClient));
            Routing.RegisterRoute(nameof(HomeAdmin), typeof(HomeAdmin));
            Routing.RegisterRoute(nameof(SignUp), typeof(SignUp));
            Routing.RegisterRoute(nameof(EditUser), typeof(EditUser));
            Routing.RegisterRoute(nameof(EditUserT), typeof(EditUserT));
        }

    }
}