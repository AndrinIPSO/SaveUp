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

         void AddItem()
        {
            Datum = DateTime.Now.ToString("dd.mm.yyyy");
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
            Application.Current.MainPage.BindingContext = mvm;
            mvm.Gesamtbetrag = this.Gesamtbetrag;
            App.Current.MainPage = new NavigationPage(new MainPage(mvm));

            

            //await Application.Current.MainPage.Navigation.PushAsync(App.Current.MainPage);
            //Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[Application.Current.MainPage.Navigation.NavigationStack.Count -1]);
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

        public string Datum
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

        public int ID
        {
            get { return tempModel.id; }
            set
            {
                tempModel.id = value;
                OnPropertyChanged();
            }
        }

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
