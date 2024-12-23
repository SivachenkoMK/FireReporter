namespace FireReporter.Views;

public partial class FireLocationQuestionPage : ContentPage
{
    public FireLocationQuestionPage()
    {
        InitializeComponent();
    }

    private async void OnInsideFireClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ThankYouPage());
    }
    
    private async void OnOutsideFireClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ThankYouPage());
    }
}