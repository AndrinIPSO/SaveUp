using SaveUp.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage(ListViewModel lvm)
        {
            BindingContext = lvm;
            InitializeComponent();
        }
    }
}