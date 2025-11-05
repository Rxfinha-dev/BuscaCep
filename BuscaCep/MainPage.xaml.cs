using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace BuscaCep
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCep.Text))
            {
                await this.DisplayAlert("OPS", "VOCÊ PRECISA INFORMAR O CEP", "OK");
            }
            else
            {
                lblLogradouro.Text = "CARREGANDO...";
                lblLocalidade.Text = "CARREGANDO...";
                lblBairro.Text = "CARREGANDO...";
                lblLUF.Text = "CARREGANDO...";
                lblDDD.Text = "CARREGANDO...";

                var ViaCepResult = await new HttpClient()
                       .GetFromJsonAsync<ViaCepDto>(requestUri: $"https://viacep.com.br/ws/{txtCep.Text}/json/");

                if (ViaCepResult is null)
                {
                    await DisplayAlert("OPS", "ALGO DEU ERRADO", "OK");
                }
                else
                {
                    lblLogradouro.Text = ViaCepResult.logradouro;
                    lblLocalidade.Text = ViaCepResult.localidade;
                     lblBairro.Text = ViaCepResult?.bairro;
                    lblLUF.Text = ViaCepResult.uf; ;
                    lblDDD.Text = ViaCepResult.ddd;




                }
            }
        }
    }

   public class ViaCepDto
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string unidade { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string estado { get; set; }
        public string regiao { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }
    }


}
