using CrochetoApp.ViewModels;

namespace CrochetoApp.Pages.Access;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
        BindingContext = new SignUpViewModel();
	}
}