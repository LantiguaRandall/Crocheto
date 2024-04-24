using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CrochetoApp.Models
{
    public partial class Pdf : ObservableObject
    {
        [ObservableProperty]
        private int pdfid;

        [ObservableProperty]
        private string? titulo;

        [ObservableProperty]
        private string? autor;

        [ObservableProperty]
        private DateTime fecha;

        [ObservableProperty]
        private byte[]? datos;

        [ObservableProperty]
        private int? userid;
    }
}
