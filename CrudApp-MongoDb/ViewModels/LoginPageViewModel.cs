using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            EmailText = "test@test.com";
            PasswordText = "testtest";
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
                    await Shell.Current.GoToAsync("///Main");
                    EmailText = "";
                    PasswordText = "";
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
