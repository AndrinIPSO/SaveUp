using SaveUp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public AddPage(AddViewModel avm)
        {
            BindingContext = avm;
            InitializeComponent();
        }
    }
}