using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuscaCep.ViewModels
{
    sealed class BuscaCepViewModel : BaseViewModel
    {
        
        private string? _CEP;

        public string? CEP
        {
            get => _CEP;
            set
            {
                _CEP = value;
                onPropertyChanged();
            }
        }
        private string? _Logradouro;
        public string? Logradouro 
        { 
            get => _Logradouro;
            set
            {
                _Logradouro = value;
                onPropertyChanged();
            }
        }
        private string? _Bairro;
        public string? Bairro 
        { 
            get => _Bairro;
            set
            {
                _Bairro = value;
                onPropertyChanged();
            }
        }
        private string? _Localidade;
        public string? Localidade 
        { 
            get => _Localidade;
            set
            {
                _Localidade = value;
                onPropertyChanged();
            }
        }
        private string? _UF;
        public string? UF 
        { 
            get => _UF;
            set
            {
                _UF = value;
                onPropertyChanged();
            }
        }
        private string? _DDD;
        public string? DDD 
        { 

            get => _DDD;
            set
            {
                _DDD = value;
                onPropertyChanged();
            }
        }

        public BuscaCepViewModel()
        {
            //Isso seria um construtor, mas nem sempre é a mais viável, uma vez 
            //cada vez que chamamos o new, instanciamos uma nova class, o que pode acabar
            //pesando o nosso sistema.

            // _BuscarCommand = new Command();
        }

        private Command _BuscarCommand;
        public Command BuscarCommand
            => _BuscarCommand ?? (_BuscarCommand = new Command(async () => await BuscarCommandExecute()));
        //{
        //    get
        //    {
        //        if(_BuscarCommand == null)
        //        {
        //            _BuscarCommand = new Command(() => await BuscarCommandExecute());    

        //            return _BuscarCommand;

        //        }
        //    }
        //}

        private async Task BuscarCommandExecute()
        {
            try
            {
                if (string.IsNullOrEmpty(CEP))
                {
                    throw new InvalidOperationException(message: "VOCÊ PRECISA INFORMAR O CEP");
                }
                else
                {                 

                    var ViaCepResult = await new HttpClient()
                           .GetFromJsonAsync<ViaCepDto>(requestUri: $"https://viacep.com.br/ws/{CEP}/json/") ??
                           throw new InvalidOperationException(message: "ALGO DEU ERRADO");

                    if (ViaCepResult.erro)
                        throw new InvalidOperationException(message: "ALGO DEU ERRADO");

                    Logradouro = ViaCepResult.logradouro;
                    Localidade = ViaCepResult.localidade;
                    Bairro = ViaCepResult?.bairro;
                    UF = ViaCepResult.uf; ;
                    DDD = ViaCepResult.ddd;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("OPS", ex.Message, "OK");
            }
        }
    }
}
