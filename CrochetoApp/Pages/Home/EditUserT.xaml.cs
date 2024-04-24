using CrochetoApp.ViewModels;
namespace CrochetoApp.Pages.Home;

public partial class EditUserT : ContentPage
{
	public EditUserT()
    {
        InitializeComponent();
        this.BindingContext = new EditUserTViewModel();
	}
}