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

        public ObservableCollection<EintragModel> models = new ObservableCollection<EintragModel>();

        public REST()
        {
            _client.BaseAddress = new Uri(BASE_URL);
        }

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
                    await App.Current.MainPage.DisplayAlert("Server Fehler", response.StatusCode.ToString(), "OK");
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Fehler", ex.Message, "OK");
            }
        }

        private T GetJsonValue<T>(string json, string jsonPropertyName)
        {
            var parsedResult = JObject.Parse(json);
            return parsedResult.SelectToken(jsonPropertyName).ToObject<T>();
        }

        public ObservableCollection<EintragModel> getModels()
        {
            ReadAll();
            return models;
        }

        
    }
}
