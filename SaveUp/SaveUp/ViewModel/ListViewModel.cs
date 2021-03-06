using SaveUp.Model;
using SaveUp.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
    public class ListViewModel : ViewModelBase
    {
        /// <summary>
        /// REST objekt für alle HTTP opearationen
        /// </summary>
        REST rest = new REST();
        /// <summary>
        /// Daten kollektion (liste) -> wird an alle viremodels weitergegeben
        /// </summary>
        ObservableCollection<EintragModel> eintragdaten = new ObservableCollection<EintragModel>();
        /// <summary>
        /// Datenliste mit OnPropertyChanged(); -> verändert liste für weitergabe
        /// </summary>
        public ObservableCollection<EintragModel> EintragDaten
        {
            get 
            {
                
                return eintragdaten;
                 
            }
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
        /// Ruft delAll(); auf
        /// </summary>
        public Command DeleteAll { get; }
        /// <summary>
        /// ruft delete(id); auf. Nutzt Interface, damit ID übergeben werden kann
        /// </summary>
        public ICommand Delete { get; }
        /// <summary>
        /// Init der Commands
        /// </summary>
        public ListViewModel()
        {
            DeleteAll = new Command(delAll);
            Delete = new Command<int>(id => { delete(id); });
            ObservableCollection<EintragModel> tmp = rest.getModels();
            EintragDaten = tmp;
        }
        /// <summary>
        /// Löscht alle Daten in Datenliste und übergibt Liste zur Mainpage und welchslet zur Mainpage
        /// </summary>
        async void delAll()
        {

            if (eintragdaten.Count == 0)
            {
                return;
            }
            bool delall = await App.Current.MainPage.DisplayAlert("Warnung", "Alle Einträge werden Gelöscht!", "Passt", "Abbrechen");
            if (delall)
            {
                foreach (EintragModel item in EintragDaten)
                {
                    rest.Delete(item);
                }


                eintragdaten.Clear();
                JsonFileHelper.SyncToFile(EintragDaten);
                MainViewModel mvm = new MainViewModel();
                Application.Current.MainPage.BindingContext = mvm;
                mvm.EintragDaten = this.EintragDaten;
                App.Current.MainPage = new NavigationPage(new MainPage(mvm));
            }
        }
        /// <summary>
        /// Löscht eintrag von Datenliste (ID muss Stimmen), wechselt zu Mainpage
        /// </summary>
        /// <param name="id">Zu löschendes Element</param>
        async void delete(int id)
        {

            bool del = await App.Current.MainPage.DisplayAlert("Löschen", "Eintrag löschen?", "Ja", "Ah nei doch nöd");
            if (del)
            {
                ObservableCollection<EintragModel> templist = eintragdaten;
                for (int i = templist.Count - 1; i >= 0; i--)
                {
                    if (id == templist[i].id)
                    {
                        rest.Delete(templist[i]);
                        templist.Remove(templist[i]);
                    }
                }



            }
            else
            {
                return;
            }
            JsonFileHelper.SyncToFile(EintragDaten);
            MainViewModel mvm = new MainViewModel();
            Application.Current.MainPage.BindingContext = mvm;
            App.Current.MainPage = new NavigationPage(new MainPage(mvm));
        }
        /// <summary>
        /// Berechnen Gesamtbetrag für Mainpage
        /// </summary>
        public float? Gesamtbetrag
        {
            get
            {
                float? gesamtfloat = 0;
                foreach (var item in EintragDaten)
                {
                    gesamtfloat += item.Betrag;
                }
                return gesamtfloat;
            }
        }
    }
}
