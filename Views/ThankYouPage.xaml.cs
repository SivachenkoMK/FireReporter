namespace FireReporter.Views;

public partial class ThankYouPage : ContentPage
{
    public ThankYouPage()
    {
        InitializeComponent();
    }

    private async void OnReturnClicked(object sender, EventArgs e)
    {
        // Pop to root or do something else:
        await Navigation.PopToRootAsync();
    }
}