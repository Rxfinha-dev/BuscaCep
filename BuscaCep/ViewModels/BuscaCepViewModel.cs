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
                BuscarCommand.ChangeCanExecute();
            }
        }

        ViaCepDto? _dto = null;



     
        public string? Logradouro { get => _dto?.logradouro;}  
        public string? Bairro { get => _dto?.bairro;}       
        public string? Localidade { get => _dto?.localidade;}       
        public string? UF { get => _dto?.uf;}    
        public string? DDD { get => _dto?.ddd;}

        public BuscaCepViewModel()
        {
            //Isso seria um construtor, mas nem sempre é a mais viável, uma vez 
            //cada vez que chamamos o new, instanciamos uma nova class, o que pode acabar
            //pesando o nosso sistema.

            // _BuscarCommand = new Command();
        }

        private Command _BuscarCommand;
        public Command BuscarCommand
            => _BuscarCommand ??= new Command(async () => await BuscarCommandExecute(),()=> BuscarCommandCanExecute());

        private bool BuscarCommandCanExecute()
            => !string.IsNullOrWhiteSpace(CEP) &&
            CEP.Length == 8 &&
            IsNotBusy;

        private async Task BuscarCommandExecute()
        {
            try
            {
                         
                
                    if(IsBusy)
                        return;

                     IsBusy = true;
                    BuscarCommand.ChangeCanExecute();


                _dto = await new HttpClient()
                           .GetFromJsonAsync<ViaCepDto>(requestUri: $"https://viacep.com.br/ws/{CEP}/json/") ??
                           throw new InvalidOperationException(message: "ALGO DEU ERRADO");

                    if (_dto.erro)
                        throw new InvalidOperationException(message: "ALGO DEU ERRADO");                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("OPS", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                BuscarCommand.ChangeCanExecute();
                onPropertyChanged(nameof(Logradouro));
                onPropertyChanged(nameof(Bairro));
                onPropertyChanged(nameof(Localidade));
                onPropertyChanged(nameof(UF));
                onPropertyChanged(nameof(DDD));
            }
        }
    }
}
