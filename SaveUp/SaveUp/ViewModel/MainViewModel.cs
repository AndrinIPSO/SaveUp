using SaveUp.Model;
using SaveUp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<EintragModel> eintragdaten = new ObservableCollection<EintragModel>();
        public ObservableCollection<EintragModel> EintragDaten
        {
            get { return eintragdaten; }
            set
            {
                if (eintragdaten != value)
                {
                    eintragdaten = value;
                    OnPropertyChanged();
                }
            }
        }

        private float _gesamtbetrag = 0;
        public Command OpenAdd { get; }
        public Command OpenList { get; }

        public MainViewModel()
        {
            OpenAdd = new Command(OpenAddPage);
            OpenList = new Command(OpenListPage);
        }

        public MainViewModel(ObservableCollection<EintragModel> list)
        {
            EintragDaten = list;
            OpenAdd = new Command(OpenAddPage);
            OpenList = new Command(OpenListPage);
        }

        //string calcGesamt()
        //{
        //    float gesamtfloat = 0;
        //    foreach (var item in EintragDaten)
        //    {
        //        gesamtfloat += item.Betrag;
        //    }
        //    Debug.WriteLine("Gesamtbetrag");
        //    return gesamtfloat.ToString();
        //}

        public float Gesamtbetrag
        {
            get
            {
                return _gesamtbetrag;
            }
            set
            {
                _gesamtbetrag = value;
                OnPropertyChanged();
            }
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
