using CrochetoApp._app;
using CrochetoApp.Models;
using CrochetoApp.Pages.Access;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace CrochetoApp.ViewModels
{
    public partial class HomeClientViewModel : ObservableObject
    {
        public ICommand UploadPdfCommand { get; }
        public ICommand ItemTappedCommand { get; private set; }

        public HomeClientViewModel()
        {
            User = CurrentUser.User;

            if (User == null)
            {
                IrALogin();
                return;
            }

            UploadPdfCommand = new Command(async () => await UploadPdfAsync());
            ItemTappedCommand = new Command<Pdf>(VerArchivo);

            CargarDatos();
        }

        private User? User = null;

        [ObservableProperty]
        private List<Pdf> listaPdf = new List<Pdf>();

        private async void IrALogin()
        {
            await Shell.Current.GoToAsync($"{nameof(Login)}", false);
        }

        private void CargarDatos()
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string rawSql = "SELECT PDFID, TITULO, AUTOR, FECHAPUBLICACION, USUARIOID FROM PDFS WHERE USUARIOID = @Valor1";

                using (SqlCommand command = new SqlCommand(rawSql, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", User?.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pdf = new Pdf();
                            
                            var id = reader["PDFID"].ToString();

                            if (!string.IsNullOrWhiteSpace(id))
                            {
                                pdf.Pdfid = int.Parse(id);
                            }

                            pdf.Titulo = reader["TITULO"].ToString();
                            
                            pdf.Autor = reader["AUTOR"].ToString();

                            var fecha = reader["FECHAPUBLICACION"].ToString();

                            if (!string.IsNullOrWhiteSpace(fecha))
                            {
                                pdf.Fecha = DateTime.Parse(fecha);
                            }

                            var userId = reader["USUARIOID"].ToString();

                            if (!string.IsNullOrWhiteSpace(userId))
                            {
                                pdf.Userid = int.Parse(userId);
                            }

                            ListaPdf.Add(pdf);
                        }
                    }
                }
            }
        }

        private async Task UploadPdfAsync()
        {
            try
            {
                var archivo = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Por favor, selecciona un PDF",
                    FileTypes = FilePickerFileType.Pdf
                });

                if (archivo != null)
                {

                    using var stream = await archivo.OpenReadAsync();
                    var pdfBytes = new byte[stream.Length];
                    await stream.ReadAsync(pdfBytes);

                    GuardarArchivo(pdfBytes, archivo.FileName);

                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                var elCampoDeTexto = "";
                var listaPrincipal = new List<Pdf>();
                var listaFiltrada = listaPrincipal;

                //Cuando busque en el filtro harias algo asi

                listaFiltrada = listaPrincipal
                    .Where(o => elCampoDeTexto.Contains(o.Autor) || elCampoDeTexto.Contains(o.Titulo))
                    .ToList();

                Debug.WriteLine(ex.ToString());
            }
        }

        private void GuardarArchivo(byte[] contenido, string nombrearchivo)
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string sqlInsert = "INSERT INTO PDFS (Titulo, Autor, FechaPublicacion, DatosPdf, UsuarioId) values (@Valor1, @Valor2, @Valor3, @Valor4, @Valor5)";

                using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", nombrearchivo);
                    command.Parameters.AddWithValue("@Valor2", User?.UserName);
                    command.Parameters.AddWithValue("@Valor3", DateTime.Now);
                    command.Parameters.AddWithValue("@Valor4", contenido);
                    command.Parameters.AddWithValue("@Valor5", User?.Id);

                    command.ExecuteNonQuery();
                }
                Shell.Current.DisplayAlert("Atención", "El archivo ha sido actualizado correctamente", "Ok");
            }
        }

        private async void VerArchivo(Pdf pdf)
        {
            if (pdf == null) { return; }
            
            
            var contenido = CargarPdf(pdf.Pdfid);
            
            if (contenido == null) { return; }

            var file = Path.Combine(FileSystem.CacheDirectory, pdf.Titulo + ".pdf");
            File.WriteAllBytes(file, contenido);
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(file)
            });
        }

        private byte[]? CargarPdf(int pdfId)
        {
            using (SqlConnection connection = new SqlConnection(ConneccionStringConst.ConneccionString))
            {
                connection.Open();

                string rawSql = "SELECT DATOSPDF FROM PDFS WHERE PDFID = @Valor1";

                using (SqlCommand command = new SqlCommand(rawSql, connection))
                {
                    command.Parameters.AddWithValue("@Valor1", pdfId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (byte[])reader["DATOSPDF"];
                        }
                    }
                }
            }

            return null;
        }
    }
}
