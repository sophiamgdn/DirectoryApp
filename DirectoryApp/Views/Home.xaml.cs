using DirectoryApp.ViewModel;

namespace DirectoryApp.Views;

public partial class HomePage : ContentPage
{
    ContactViewModel contactViewModel = new ContactViewModel();
    public HomePage()
	{
		InitializeComponent();
        BindingContext = contactViewModel;

    }

    private void OnbtnAdd_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddContact());
    }
}