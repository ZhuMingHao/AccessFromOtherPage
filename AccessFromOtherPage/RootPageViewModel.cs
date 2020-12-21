using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccessFromOtherPage
{
    public class RootPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly RootPageViewModel instance = new RootPageViewModel();

        //Explicit static consturctor to tell C# compiler 
        //not to mark type as beforefieldinit
        static RootPageViewModel()
        {
        }

        private RootPageViewModel()
        {
        }

        public static RootPageViewModel Instance
        {
            get
            {
                return instance;
            }
        }

     

        private string _mainStatusContent;
        public string MainStatusContent
        {
            get
            {
                return _mainStatusContent;
            }
            set
            {
                _mainStatusContent = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
