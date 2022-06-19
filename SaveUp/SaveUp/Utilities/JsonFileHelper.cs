using Newtonsoft.Json;
using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace SaveUp.Utilities
{
    public class JsonFileHelper
    {
        /// <summary>
        /// Vermeidung Code Redundanz
        /// </summary>
        public JsonFileHelper()
        {

        }
        /// <summary>
        /// Liest objekte von lokaler datei
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<EintragModel> readFromFile()
        {
            var file = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "eintraege.json");
            var json = File.ReadAllText(file);

            ObservableCollection<EintragModel> dataList = JsonConvert.DeserializeObject<ObservableCollection<EintragModel>>(json);
            return new ObservableCollection<EintragModel>(dataList);
        }
        /// <summary>
        /// Schreibt liste in lokale datei
        /// </summary>
        /// <param name="ModelList">Liste der Models</param>
        public static void SyncToFile(object ModelList)
        {
            var file = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "eintraege.json");

            string input = JsonConvert.SerializeObject(ModelList);
            File.WriteAllText(file, input);
        }
    }
}
