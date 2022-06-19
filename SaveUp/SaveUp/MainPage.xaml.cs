using SaveUp.ViewModel;
using Xamarin.Forms;

namespace SaveUp
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Beim Eresten start aufgerufen, erstellt ViewModel
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
        /// <summary>
        /// Beim Seitenwechsel aufegrufen, erhält ViewModel
        /// </summary>
        /// <param name="mvm">Übergabe von ViewModel</param>
        public MainPage(MainViewModel mvm)
        {
            InitializeComponent();
            BindingContext = mvm;
        }
    }
}
