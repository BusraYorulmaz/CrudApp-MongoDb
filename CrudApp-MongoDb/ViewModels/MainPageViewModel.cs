﻿
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrudApp_MongoDb.Helpers;
using CrudApp_MongoDb.Models;
using CrudApp_MongoDb.Views;
using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp_MongoDb.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private Realm realm;
        private PartitionSyncConfiguration config;

        public MainPageViewModel()
        {
            userList = new ObservableCollection<UserModel>();
            EmptyText = "No Users information here";
        }
        [ObservableProperty]
        ObservableCollection<UserModel> userList;

        [ObservableProperty]
        string emptyText;

        [ObservableProperty]
        string usersEntryText;

        [ObservableProperty]
        string surnameEntryText;

        [ObservableProperty]
        string telefonEntryText;

        [ObservableProperty]
        string mailEntryText;

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task DetailUser(UserModel user)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserDetail(user));
        }


        [RelayCommand]
        async Task AddUser()
        {
            if (string.IsNullOrWhiteSpace(UsersEntryText))
                return;
            IsBusy = true;
            try
            {
                var user =
                    new UserModel
                    {
                        Surname = GeneralHelper.UppercaseFirst(SurnameEntryText),
                        Name = GeneralHelper.UppercaseFirst(UsersEntryText),
                        Telefon = GeneralHelper.UppercaseFirst(TelefonEntryText),
                        Mail = GeneralHelper.UppercaseFirst(MailEntryText),
                        Partition = App.RealmApp.CurrentUser.Id,
                        Owner = App.RealmApp.CurrentUser.Profile.Email
                    };
                realm.Write(() =>
                {
                    realm.Add(user);
                });

                UsersEntryText = "";
                SurnameEntryText = "";
                TelefonEntryText = "";
                MailEntryText = "";

                GetUsers();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy = false;
        }


        [RelayCommand]
        async Task SignOut()
        {
            IsBusy = true;
            try
            {
                var currentuserId = App.RealmApp.CurrentUser.Id;
                await App.RealmApp.RemoveUserAsync(App.RealmApp.CurrentUser);
                var noMoreCurrentUser = App.RealmApp.AllUsers.FirstOrDefault(u => u.Id == currentuserId);
                await Shell.Current.GoToAsync("///Login");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy = false;
        }



        public async Task InitialiseRealm()
        {
            config = new PartitionSyncConfiguration($"{App.RealmApp.CurrentUser.Id}", App.RealmApp.CurrentUser);
            realm = Realm.GetInstance(config);

            GetUsers();
            if (UserList.Count == 0)
            {
                EmptyText = "Loading projects..";
                await Task.Delay(2000);
                GetUsers();
                EmptyText = "No users here";
            }
        }

        [RelayCommand]
        public async void GetUsers()
        {
            IsRefreshing = true;
            IsBusy = true;

            try
            {
                var tuser = realm.All<UserModel>().ToList().Reverse<UserModel>();
                UserList = new ObservableCollection<UserModel>(tuser);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsRefreshing = false;
            IsBusy = false;
        }

        [RelayCommand]
        public async void EditUser(UserModel user)
        {
            string newString = await App.Current.MainPage.DisplayPromptAsync("Edit", user.Name);
            

            if (newString is null || string.IsNullOrWhiteSpace(newString.ToString()))
                return;
            try
            {
                realm.Write(() =>
                {
                    var foundUser = realm.Find<UserModel>(user.Id);
                    foundUser.Name = GeneralHelper.UppercaseFirst(newString.ToString());
                });

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
        }

        [RelayCommand]
        async Task DeleteTask(UserModel user)
        {
            IsBusy = true;
            try
            {
                realm.Write(() =>
                {
                    realm.Remove(user);
                });
                UserList.Remove(user);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayPromptAsync("Error", ex.Message);
            }
            IsBusy = false;
        }

    }
}
