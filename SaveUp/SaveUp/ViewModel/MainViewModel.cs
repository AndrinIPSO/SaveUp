using SaveUp.Model;
using SaveUp.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public List<EintragModel> EintragDaten = new List<EintragModel>();
        public Command OpenAdd { get; }
        public Command OpenList { get; }

        public MainViewModel()
        {
            OpenAdd = new Command(OpenAddPage);
            OpenList = new Command(OpenListPage);
        }

        async void OpenAddPage()
        {
            AddViewModel avm = new AddViewModel();
            avm.EintragDaten = this.EintragDaten;
            await Application.Current.MainPage.Navigation.PushAsync(new AddPage(avm));

        }

        async void OpenListPage()
        {
            ListViewModel lvm = new ListViewModel();
            lvm.EintragDaten = this.EintragDaten;
            await Application.Current.MainPage.Navigation.PushAsync(new ListPage(lvm));
        }

    }
}
