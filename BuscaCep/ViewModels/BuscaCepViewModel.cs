using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuscaCep.ViewModels
{
    sealed class BuscaCepViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        
        

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged is null)
            {
                return;
            }

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

    }
}
