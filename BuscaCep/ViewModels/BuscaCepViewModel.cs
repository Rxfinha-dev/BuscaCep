using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    sealed partial class BuscaCepViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(BuscarCommand))]
        string? _CEP;
        
        
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


        
   

        private bool BuscarCanExecute()
            => !string.IsNullOrWhiteSpace(CEP) &&
            CEP.Length == 8 &&
            IsNotBusy;

        [RelayCommand(CanExecute = nameof(BuscarCanExecute))]

        private async Task Buscar()
        {
            try
            {
                         
                
                    if(IsBusy)
                        return;

                     IsBusy = true;
                     BuscarCommand.NotifyCanExecuteChanged();



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
                               
                OnPropertyChanged(nameof(Logradouro));
                OnPropertyChanged(nameof(Bairro));
                OnPropertyChanged(nameof(Localidade));
                OnPropertyChanged(nameof(UF));
                OnPropertyChanged(nameof(DDD));

                IsBusy = false;
                BuscarCommand.NotifyCanExecuteChanged();


            }
        }
    }
}
