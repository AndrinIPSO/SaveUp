using SaveUp.Utilities;
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
            REST rest = new REST();
            MainViewModel mvm = new MainViewModel();
            BindingContext = mvm;
            InitializeComponent();
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
