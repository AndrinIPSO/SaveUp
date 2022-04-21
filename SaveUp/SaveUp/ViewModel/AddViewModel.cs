using SaveUp.Model;
using SaveUp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        /// <summary>
        /// Wird verwendet um später ein vollständiges Objekt der Liste anzufügen
        /// </summary>
        private EintragModel tempModel;
        ObservableCollection<EintragModel> eintragdaten = new ObservableCollection<EintragModel>();
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
        public Command Add { get; }
        public AddViewModel()
        {
            Add = new Command(AddItem);
            tempModel = new EintragModel();
            
        }

        async void AddItem()
        {
            Datum = DateTime.Now;
            eintragdaten.Add(tempModel);
            MainViewModel mvm = new MainViewModel(eintragdaten);
            Application.Current.MainPage.BindingContext = mvm;

            await Application.Current.MainPage.Navigation.PopAsync();

            //ListViewModel lvm = new ListViewModel();
            //lvm.EintragDaten.Add(tempModel);
            //await Application.Current.MainPage.Navigation.PushAsync(new ListPage(lvm));
        }

        public string Name
        {
            get { return tempModel.Name; }
            set
            {
                tempModel.Name = value;
                OnPropertyChanged();
            }
        }

        public string Beschreibung
        {
            get { return tempModel.Beschreibung; }
            set
            {
                tempModel.Beschreibung = value;
                OnPropertyChanged();
            }
        }

        public DateTime Datum
        {
            get { return tempModel.Datum; }
            set
            {
                tempModel.Datum = value;
                OnPropertyChanged();
            }
        }

        public float Betrag
        {
            get { return tempModel.Betrag; }
            set
            {
                tempModel.Betrag = value;
                OnPropertyChanged();
            }
        }
    }
}
