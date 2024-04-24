using CrochetoApp.Pages.Access;

namespace CrochetoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new UserAppShell();

        }
    }

}
