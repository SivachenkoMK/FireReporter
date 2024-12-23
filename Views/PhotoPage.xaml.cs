namespace FireReporter.Views;

public partial class PhotoPage : ContentPage
{
    public PhotoPage()
    {
        InitializeComponent();
    }

    private async void OnSkipClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FireLocationQuestionPage());
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
                    // Handle the photo (e.g., save or display)
                }

                // Navigate to the next page
                await Navigation.PushAsync(new FireLocationQuestionPage());
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
}