using CrudApp_MongoDb.ViewModels;

namespace CrudApp_MongoDb.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mv)
	{
		InitializeComponent();
		BindingContext = mv;
	}
}