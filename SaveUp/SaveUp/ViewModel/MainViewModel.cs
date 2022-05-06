using SaveUp.Model;
using SaveUp.View;
using System.Collections.ObjectModel;
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
        /// <summary>
        /// Gesamtbetrag wird in andern Viewmodels berechnet. 
        /// </summary>
        private float _gesamtbetrag = 0;
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
            OpenAdd = new Command(OpenAddPage);
            OpenList = new Command(OpenListPage);
        }
        /// <summary>
        /// Init Commands und übergabe Datenmodel(liste)
        /// </summary>
        /// <param name="list">Model liste</param>
        public MainViewModel(ObservableCollection<EintragModel> list)
        {
            EintragDaten = list;
            OpenAdd = new Command(OpenAddPage);
            OpenList = new Command(OpenListPage);
        }
        /// <summary>
        /// Verändern Gesamtbetrag (get/set)
        /// </summary>
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
        /// <summary>
        /// Öffnet AddSeite und übergibt Datenliste
        /// </summary>
        async void OpenAddPage()
        {
            AddViewModel avm = new AddViewModel();
            avm.EintragDaten = this.EintragDaten;
            await Application.Current.MainPage.Navigation.PushAsync(new AddPage(avm));

        }
        /// <summary>
        /// Öffnet ListenSeite und übergibt Datenliste
        /// </summary>
        async void OpenListPage()
        {
            ListViewModel lvm = new ListViewModel();
            lvm.EintragDaten = this.EintragDaten;
            await Application.Current.MainPage.Navigation.PushAsync(new ListPage(lvm));
        }
    }
}
