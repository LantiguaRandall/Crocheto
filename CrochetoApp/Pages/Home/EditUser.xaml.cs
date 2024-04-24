using CrochetoApp.ViewModels;
namespace CrochetoApp.Pages.Home;

public partial class EditUser : ContentPage
{
	public EditUser()
	{
		InitializeComponent();
        BindingContext = new EditUserViewModel();
    }

}