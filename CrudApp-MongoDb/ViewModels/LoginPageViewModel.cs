using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp_MongoDb.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel()
        {
            //EmailText = "by@gmail.com";
            //PasswordText = "1234";
        }

        [ObservableProperty]
        string emailText;

        [ObservableProperty]
        string passwordText;

        public static async void StartDashboard()
        {
            await Shell.Current.GoToAsync("///Main");
        }

        [RelayCommand]
        async void CreateAccount()
        {
            try
            {
                await App.RealmApp.EmailPasswordAuth.RegisterUserAsync(EmailText, PasswordText);

                await Login();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error creating account!", "Error: " + ex.Message, "OK");
            }
        }


        [RelayCommand]
        public async Task Login()
        {
            try
            {
                var user = await App.RealmApp.LogInAsync(Credentials.EmailPassword(EmailText, PasswordText));

                if (user != null)
                {
                    Preferences.Set("Email", EmailText);
                    Preferences.Set("Password", PasswordText);
                    await Shell.Current.GoToAsync("///Main");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error Logging In", ex.Message, "OK");
            }
        }
    }
}
