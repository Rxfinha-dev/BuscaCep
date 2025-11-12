using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BuscaCep.ViewModels
{
    abstract class BaseViewModel: INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool _isBusy = false;
        public bool IsBusy {  get => _isBusy;
            set
            {
                _isBusy = value;
                onPropertyChanged();
                onPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !IsBusy;






    }
}