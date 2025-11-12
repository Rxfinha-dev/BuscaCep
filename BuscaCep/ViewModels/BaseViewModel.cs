using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BuscaCep.ViewModels
{
    abstract class BaseViewModel: INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;
        //protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    if(PropertyChanged is null)
        //    {
        //        return;
        //    }

        //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));






    }
}