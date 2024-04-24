using CrochetoApp.ViewModels;

namespace CrochetoApp.Pages.Access;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }

   
}

