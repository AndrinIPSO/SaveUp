using SaveUp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        /// <summary>
        /// Aufruf per Seitenwechsel
        /// </summary>
        /// <param name="avm">Übergabe ViewModel</param>
        public AddPage(AddViewModel avm)
        {
            BindingContext = avm;
            InitializeComponent();
        }
    }
}