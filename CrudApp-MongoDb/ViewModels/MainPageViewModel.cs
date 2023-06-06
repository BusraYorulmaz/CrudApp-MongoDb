
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp_MongoDb.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private string savedEmail;
        public string SavedEmail
        {
            get { return savedEmail; }
            set
            {
                if (savedEmail != value)
                {
                    savedEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        private string savedPassword;
        public string SavedPassword
        {
            get { return savedPassword; }
            set
            {
                if (savedPassword != value)
                {
                    savedPassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainPageViewModel()
        {
            SavedEmail = Preferences.Get("Email",string.Empty);
            SavedPassword = Preferences.Get("Password", string.Empty);

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
