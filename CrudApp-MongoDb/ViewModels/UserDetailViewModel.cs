using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrudApp_MongoDb.Helpers;
using CrudApp_MongoDb.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp_MongoDb.ViewModels
{
    public partial class UserDetailViewModel :BaseViewModel
    {
        private Realm realm;

        private UserModel user;
        [ObservableProperty]
        string usersEntryText;

        [ObservableProperty]
        string surnameEntryText;

        [ObservableProperty]
        string telefonEntryText;

        [ObservableProperty]
        string mailEntryText;

        public UserDetailViewModel(UserModel user)
        {
            this.user= user;
            UsersEntryText = user.Name;
            SurnameEntryText= user.Surname;
            MailEntryText= user.Mail;
            TelefonEntryText= user.Telefon;
        }

        [RelayCommand]
        public async void EditUser(UserModel user)
        {
            try
            {
                realm.Write(() =>
                {
                    var foundfUser = realm.Find<UserModel>(user.Id);
                    foundfUser.Name = GeneralHelper.UppercaseFirst(UsersEntryText);
                    foundfUser.Surname = SurnameEntryText;
                    foundfUser.Mail= MailEntryText;
                    foundfUser.Telefon= TelefonEntryText;
                });
               await App.Current.MainPage.DisplayAlert("Success", "User information updated", "OK");
            }
            catch (Exception ex)
            {

               await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
