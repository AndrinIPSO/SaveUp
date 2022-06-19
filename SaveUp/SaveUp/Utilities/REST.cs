using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Utilities
{
    public class REST
    {
        /// <summary>
        /// HTTP URL Konstante
        /// </summary>
        private const string BASE_URL = "http://andrinschellenberg.ch/rest/";
        /// <summary>
        /// HTTP Client Objekt
        /// </summary>
        private HttpClient _client = new HttpClient();

        public string contentString { get; set; }
        /// <summary>
        /// Collection von EintragModels zur weitergabe
        /// </summary>
        public ObservableCollection<EintragModel> models = new ObservableCollection<EintragModel>();
        /// <summary>
        /// Vereinfachte Nutzung der HTTP klasse und vermeidung Coderedundanz
        /// </summary>
        public REST()
        {
            _client.BaseAddress = new Uri(BASE_URL);
            initRead();
        }
        /// <summary>
        /// Fügt Model in DB ein
        /// </summary>
        /// <param name="Model">Model sollte EintragModel sein</param>
        public async void Add(object Model)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(Model), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/create.php", content);

                if (!response.IsSuccessStatusCode)
                {

                    await App.Current.MainPage.DisplayAlert("Server Fehler", response.StatusCode.ToString(), "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "OK");
            }
        }
        /// <summary>
        /// Löscht anhand von id Property
        /// </summary>
        /// <param name="Model">muss id enthalten</param>
        public async void Delete(object Model)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(Model), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/delete.php", content);
                if (!response.IsSuccessStatusCode)
                {

                    await App.Current.MainPage.DisplayAlert("Server Fehler", response.StatusCode.ToString(), "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "OK");
            }
        }
        /// <summary>
        /// Liest von API und speichert in Models
        /// </summary>
        private async void ReadAll()
        {
            try
            {
                models = new ObservableCollection<EintragModel>();
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _client.GetAsync("api/read.php");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ObservableCollection<EintragModel> eintraege = GetJsonValue<ObservableCollection<EintragModel>>(content, "body");
                    foreach (EintragModel eintragModel in eintraege)
                    {
                        if (eintragModel.Betrag == null || eintragModel.Name == null)
                        {
                            continue;
                        }
                        models.Add(eintragModel);
                    }
                }
                else
                {
                    //await App.Current.MainPage.DisplayAlert("Server Fehler", response.StatusCode.ToString(), "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "OK");
            }
        }
        /// <summary>
        /// Test Funktion um Readall auf zu teilen
        /// </summary>
        public async void initRead()
        {
            models = new ObservableCollection<EintragModel>();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _client.GetAsync("api/read.php");
            if (response.IsSuccessStatusCode)
            {
                contentString = await response.Content.ReadAsStringAsync();
                
            }
            else
            {
                //await App.Current.MainPage.DisplayAlert("Server Fehler", response.StatusCode.ToString(), "OK");
            }
        }
        /// <summary>
        /// Test Funktion um Readall aufzuteilen
        /// </summary>
        public void createModels()
        {
            if(contentString != null)
            {
                models = GetJsonValue<ObservableCollection<EintragModel>>(contentString, "body");
            }
        }

        /// <summary>
        /// Berechnet Gesamtbetrag der Models
        /// </summary>
        /// <returns>Gesamtbetrag</returns>
        public float? GetGesamtbetrag()
        {
            initRead();
            createModels();
            float? ges = 0;
            foreach (EintragModel item in models)
            {
                ges += item.Betrag;
            }
            return ges;
        }
        /// <summary>
        /// Wandelt JSON string in Model um
        /// </summary>
        /// <typeparam name="T">Typ des Models</typeparam>
        /// <param name="json">Content string</param>
        /// <param name="jsonPropertyName">Json Property</param>
        /// <returns></returns>
        private T GetJsonValue<T>(string json, string jsonPropertyName)
        {
            var parsedResult = JObject.Parse(json);
            return parsedResult.SelectToken(jsonPropertyName).ToObject<T>();
        }
        /// <summary>
        /// Gibt die Models aus
        /// </summary>
        /// <returns>Models (Collection von EintragModels)</returns>
        public ObservableCollection<EintragModel> getModels()
        {
            ReadAll();
            return models;
        }
    }
}
