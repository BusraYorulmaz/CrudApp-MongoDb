using CrudApp_MongoDb.ViewModels;
using Realms;
using ZstdSharp.Unsafe;

namespace CrudApp_MongoDb.Views;

public partial class MainPage : ContentPage
{
    MainPageViewModel mv;

    public MainPage()
    {
        InitializeComponent();
        mv = new MainPageViewModel();
        BindingContext = mv;
    }

    protected override async void OnAppearing()
    {
        await mv.InitialiseRealm();
        // Bu satýr, MainPageViewModel sýnýfýndaki InitializeRealm() yöntemini asenkron olarak çaðýrýr. 
        //InitializeRealm() metodu, bir veritabaný iþlemini(Realm gibi) baþlatýr ve baþlangýç ayarlarýný yapar.
    }
}