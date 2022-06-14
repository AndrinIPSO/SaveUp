using SaveUp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaveUp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        /// <summary>
        /// Aufrufen Per Seitenwechsel
        /// </summary>
        /// <param name="lvm">Übergabe ViewModel</param>
        public ListPage(ListViewModel lvm)
        {
            BindingContext = lvm;
            InitializeComponent();
        }
    }
}