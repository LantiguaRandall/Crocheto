using CrochetoApp.ViewModels;

namespace CrochetoApp.Pages.Home;

public partial class HomeAdmin : ContentPage
{
	public HomeAdmin()
	{
		InitializeComponent();
        BindingContext = new AdminViewModel();
    }
}


