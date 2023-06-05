using CrudApp_MongoDb.ViewModels;

namespace CrudApp_MongoDb.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

   

    
}