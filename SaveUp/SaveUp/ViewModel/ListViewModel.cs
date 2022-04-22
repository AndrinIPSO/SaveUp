using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{

    // TOTO : FIX BACK TO MAIN
    public class ListViewModel : ViewModelBase
    {
        ObservableCollection<EintragModel> eintragdaten = new ObservableCollection<EintragModel>();
        public ObservableCollection<EintragModel> EintragDaten { 
            get { return eintragdaten; } 
            set {
                if (eintragdaten != value)
                {
                    eintragdaten = value;
                    OnPropertyChanged();
                }
            } 
        }

        public Command DeleteAll { get; }
        public ICommand Delete { get; }




        public ListViewModel()
        {
            DeleteAll = new Command(delAll);
            Delete = new Command<int>(id => { delete(id); });
        }

        async void delAll()
        {
            if (eintragdaten.Count ==0)
            {
                return;
            }
            bool delall = await App.Current.MainPage.DisplayAlert("Warnung", "Alle Einträge werden Gelöscht!", "Passt", "Abbrechen");
            if (delall)
            {
                eintragdaten.Clear();
                MainViewModel mvm = new MainViewModel(eintragdaten);
                Application.Current.MainPage.BindingContext = mvm;
                mvm.EintragDaten = this.EintragDaten;
                App.Current.MainPage = new NavigationPage(new MainPage(mvm));
            }
        }

        async void delete(int id)
        {
            int index = 0;
            try
            {
                bool delall = await App.Current.MainPage.DisplayAlert("Löschen", "Eintrag löschen?", "Ja", "Ah nei doch nöd");
                if (delall)
                {
                    ObservableCollection<EintragModel> templist = eintragdaten;
                    foreach (var item in templist)
                    {
                        if (item.id == id)
                        {
                            
                            index = templist.IndexOf(item);
                            templist.RemoveAt(index);
                        }
                    }
                    EintragDaten = templist;
                }
                else
                {
                    return ;
                }
            } catch (Exception ex)
            {
                // Wirft einen Fehler, es funktioniert jedoch alles?
                return ;
            }
        }
    }
}
