using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SaveUp.ViewModel
{
    public class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
