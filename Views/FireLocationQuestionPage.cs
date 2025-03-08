using System.Net.Http.Json;
using FireReporter.DTOs;
using FireReporter.Reporting;

namespace FireReporter.Views;

public partial class FireLocationQuestionPage : ContentPage
{
    private readonly UserState _userState;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public FireLocationQuestionPage(UserState userState, IHttpClientFactory httpClientFactory)
    {
        InitializeComponent();
        _userState = userState;
        _httpClientFactory = httpClientFactory;
    }

    private async void OnInsideFireClicked(object sender, EventArgs e)
    {
        await SendLocationAsync(RelativeLocation.Inside);
    }
    
    private async void OnOutsideFireClicked(object sender, EventArgs e)
    {
        await SendLocationAsync(RelativeLocation.Outside);
    }

    private async Task SendLocationAsync(RelativeLocation location)
    {
        try
        {
            var client = _httpClientFactory.CreateClient(Constants.HttpClientName);
            var locationRequest = new LocationRequest
            {
                Location = location,
                FireId = _userState.IncidentId
            };

            var response = await client.PostAsJsonAsync("api/track/location", locationRequest);
            response.EnsureSuccessStatusCode();

            await Navigation.PushAsync(new ThankYouPage());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to send location data: {ex.Message}", "OK");
        }
    }
}