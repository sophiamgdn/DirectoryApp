using CommunityToolkit.Maui.Views;

namespace DirectoryApp.Views;

public partial class StudentFormResults : Popup
{
    public StudentFormResults()
    {
        InitializeComponent();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}