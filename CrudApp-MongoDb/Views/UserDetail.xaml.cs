using CrudApp_MongoDb.Helpers;
using CrudApp_MongoDb.Models;
using CrudApp_MongoDb.ViewModels;
using Microsoft.Maui.Controls;
using Realms;

namespace CrudApp_MongoDb.Views;

public partial class UserDetail : ContentPage
{
    //UserModel userModel = new UserModel();
    //private Realm realm;
    UserDetailViewModel vm;
    public UserDetail(UserModel user)
    {
        InitializeComponent();

        BindingContext = user;
         
        //vm = new UserDetailViewModel(user);
        //BindingContext = vm;
    }

    /*

    private void Button_Clicked(object sender, EventArgs e)
    {

        string newName = Name.Text;
        string newSurname = Surname.Text;
        string newMail = Mail.Text;
        string newTelefon = Telefon.Text;

        try
        {
            realm.Write(() =>
            {
                var foundUser = realm.Find<UserModel>(userModel.Id);
                foundUser.Name = GeneralHelper.UppercaseFirst(newName);
                foundUser.Surname = GeneralHelper.UppercaseFirst(newSurname);
                foundUser.Mail = GeneralHelper.UppercaseFirst(newMail);
                foundUser.Telefon = GeneralHelper.UppercaseFirst(newTelefon);
            });
            DisplayAlert("Success", "User Information Updated", "Ok");
        }
        catch (Exception ex)
        {

            DisplayAlert("Error", ex.Message, "OK");
        }

    }*/
}