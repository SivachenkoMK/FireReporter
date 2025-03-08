using FireReporter.Reporting;

namespace FireReporter.Views;

public partial class PhotoPage : ContentPage
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly UserState _userState;
    private readonly HttpClient _httpClient;
    
    public PhotoPage(UserState userState, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _userState = userState;

        _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        InitializeComponent();
    }

    private async void OnSkipClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FireLocationQuestionPage(_userState, _httpClientFactory));
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            // Check and request camera permission
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            // If permission is granted, proceed with taking a photo
            if (status == PermissionStatus.Granted)
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    await SendPhotoToServerAsync(photo);
                }

                // Navigate to the next page
                await Navigation.PushAsync(new FireLocationQuestionPage(_userState, _httpClientFactory));
            }
            else
            {
                // Handle the case where permission is denied
                await DisplayAlert("Permission Denied", "Camera permission is required to take photos.", "OK");
            }
        }
        catch (Exception ex)
        {
            // Handle unexpected exceptions
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    
    private async Task SendPhotoToServerAsync(FileResult photo)
    {
        try
        {
            await using var stream = await photo.OpenReadAsync();
            using var content = new MultipartFormDataContent();

            using var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            content.Add(fileContent, "Photo", "photo.jpg");
            content.Add(new StringContent(_userState.IncidentId.ToString()), "SessionGuid");

            var response = await _httpClient.PostAsync("api/track/photo", content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Upload Error", $"Failed to upload photo: {ex.Message}", "OK");
        }
    }
}