using SaveUp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SaveUp.ViewModel
{
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



        public ListViewModel()
        {
            DeleteAll = new Command(delAll);

        }

        void delAll()
        {
            for (int i = 0; i < 10; i++)
            {
                EintragDaten.Add(new EintragModel() { Name = Convert.ToString(i), Betrag = 50 });
            }
        }

    }
}
