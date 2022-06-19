using Newtonsoft.Json;
using SaveUp.Model;
using SaveUp.Utilities;
using SaveUp.View;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Daten kollektion (liste) -> wird an alle viremodels weitergegeben
        /// </summary>
        private ObservableCollection<EintragModel> eintragdaten = new ObservableCollection<EintragModel>();
        /// <summary>
        /// Datenliste mit OnPropertyChanged(); -> verändert liste für weitergabe
        /// </summary>
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
        REST rest = new REST();


        /// <summary>
        /// Öffnet AddSeite
        /// </summary>
        public Command OpenAdd { get; }
        /// <summary>
        /// Öffnet ListSeite
        /// </summary>
        public Command OpenList { get; }
        /// <summary>
        /// Init der Commands
        /// </summary>
        public MainViewModel()
        {
            Gesamtbetrag = 0;
            EintragDaten = JsonFileHelper.readFromFile();
            OpenAdd = new Command(OpenAddPage);
            OpenList = new Command(OpenListPage);
            foreach (var item in EintragDaten)
            {
                Gesamtbetrag += item.Betrag;
            }

        }
        /// <summary>
        /// Verändern Gesamtbetrag (get/set)
        /// </summary>
        private float? _gesamtbetrag { get; set; }

        public float? Gesamtbetrag
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
        /// <summary>
        /// Öffnet AddSeite und übergibt Datenliste
        /// </summary>
        async void OpenAddPage()
        {
            AddViewModel avm = new AddViewModel();
            avm.EintragDaten = EintragDaten;
            await Application.Current.MainPage.Navigation.PushAsync(new AddPage(avm));

        }
        /// <summary>
        /// Öffnet ListenSeite und übergibt Datenliste
        /// </summary>
        async void OpenListPage()
        {
            ListViewModel lvm = new ListViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ListPage(lvm));
        }
    }
}
