namespace CrochetoApp.Pages.Home;
using CrochetoApp.ViewModels;

public partial class HomeClient : ContentPage
{
    public HomeClient()
    {
        InitializeComponent();
        BindingContext = new HomeClientViewModel();
    }
}