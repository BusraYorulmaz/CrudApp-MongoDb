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
        // Bu sat�r, MainPageViewModel s�n�f�ndaki InitializeRealm() y�ntemini asenkron olarak �a��r�r. 
        //InitializeRealm() metodu, bir veritaban� i�lemini(Realm gibi) ba�lat�r ve ba�lang�� ayarlar�n� yapar.
    }
}