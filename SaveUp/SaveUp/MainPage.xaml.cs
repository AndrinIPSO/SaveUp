using SaveUp.ViewModel;
using Xamarin.Forms;

namespace SaveUp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainViewModel();
            InitializeComponent();
        }
        public MainPage(MainViewModel mvm)
        {
            BindingContext = mvm;
            InitializeComponent();

        }

    }
}
