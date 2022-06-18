using Newtonsoft.Json;
using SaveUp.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        /// <summary>
        /// HTTP URL Konstante
        /// </summary>
        private const string BASE_URL = "http://andrinschellenberg.ch/rest/";
        /// <summary>
        /// HTTP Client Objekt
        /// </summary>
        private HttpClient _client = new HttpClient();
        /// <summary>
        /// Wird verwendet um später ein vollständiges Objekt der Liste anzufügen
        /// </summary>
        private EintragModel tempModel;
        /// <summary>
        /// Daten kollektion (liste) -> wird an alle viremodels weitergegeben
        /// </summary>
        ObservableCollection<EintragModel> eintragdaten = new ObservableCollection<EintragModel>();
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
        /// Ruft addItem funktion auf
        /// </summary>
        public Command Add { get; }
        /// <summary>
        /// Init von Command und tempörärem Model
        /// </summary>
        public AddViewModel()
        {
            Add = new Command(AddItem);
            tempModel = new EintragModel();
            _client.BaseAddress = new Uri(BASE_URL);

        }
        /// <summary>
        /// Fügt neues Model ein, wechselt zur Mainpage
        /// </summary>
        async void AddItem()
        {
            Datum = DateTime.Now.ToString("dd.MM.yyyy");

            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(tempModel), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/create.php", content);
                
                if (!response.IsSuccessStatusCode)
                {

                    await App.Current.MainPage.DisplayAlert("Server Fehler",response.StatusCode.ToString(),"OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "OK");
            }


            if (eintragdaten.Count == 0)
            {
                ID = 0;
            }
            else
            {
                ID = eintragdaten[eintragdaten.Count - 1].id + 1;
            }



            eintragdaten.Add(tempModel);
            MainViewModel mvm = new MainViewModel(eintragdaten);
            Application.Current.MainPage.BindingContext = mvm; // << Villeicht weglassen?
            mvm.Gesamtbetrag = this.Gesamtbetrag;
            App.Current.MainPage = new NavigationPage(new MainPage(mvm));
        }
        /// <summary>
        /// Verändert Name des tempörären Models (typ = EintragModel)
        /// </summary>
        public string Name
        {
            get { return tempModel.Name; }
            set
            {
                tempModel.Name = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Verändert Datum des tempörären Models (typ = EintragModel)
        /// </summary>
        public string Datum
        {
            get { return tempModel.Datum; }
            set
            {
                tempModel.Datum = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Verändert Betrag des tempörären Models (typ = EintragModel)
        /// </summary>
        public float Betrag
        {
            get { return tempModel.Betrag; }
            set
            {
                tempModel.Betrag = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Verändert ID des tempörären Models (typ = EintragModel) --> wird für delete benötigt
        /// </summary>
        public int ID
        {
            get { return tempModel.id; }
            set
            {
                tempModel.id = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gesamtbetrag berechnen für MainViewModel
        /// </summary>
        public float Gesamtbetrag
        {
            get
            {
                float gesamtfloat = 0;
                foreach (var item in EintragDaten)
                {
                    gesamtfloat += item.Betrag;
                }
                return gesamtfloat;
            }
        }
    }
}
