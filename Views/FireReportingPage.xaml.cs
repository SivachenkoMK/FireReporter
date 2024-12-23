using FireReporter.Reporting;

namespace FireReporter.Views;

public partial class FireReportingPage : ContentPage
{
    private readonly UserState _userState;
    
    public FireReportingPage(UserState userState)
    {
        _userState = userState;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        ResetState();

        // On page load, check if location is already granted.
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted)
        {
            ShowFireInstructions();
        }
        else
        {
            ShowLocationRequest();
        }
    }

    private void ResetState()
    {
        ChkEvacuate.IsChecked = false;
        ChkReport.IsChecked = false;
        ChkCallEmergency.IsChecked = false;
        BtnReport.IsEnabled = false;
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        ResetState();
    }

    protected override bool OnBackButtonPressed()
    {
        ResetState();
        return base.OnBackButtonPressed();
    }

    private void ShowFireInstructions()
    {
        // Hide the location request portion, show the instructions
        LocationRequestContainer.IsVisible = false;
        FireInstructionsLayout.IsVisible = true;
    }

    private void ShowLocationRequest()
    {
        LocationRequestContainer.IsVisible = true;
        FireInstructionsLayout.IsVisible = false;
    }

    private async void OnAcceptClicked(object sender, EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        if (status == PermissionStatus.Granted)
        {
            ShowFireInstructions();
        }
        else
        {
            await DisplayAlert("Permission Required", 
                "Without location, we cannot assist properly.", 
                "OK");
        }
    }

    private async void OnDeclineClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Location Declined", 
            "You have declined location access. Please grant it for full functionality.", 
            "OK");
    }

    private async void OnReportClicked(object sender, EventArgs e)
    {
        // To collect location here
        var isNewSession = _userState.TryNewSessionCreation();
        if (isNewSession)
        {
            // Send user location + id
        }
        
        await Navigation.PushAsync(new PhotoPage());
    }
    
    private void OnChkEvacuateTapped(object sender, EventArgs e)
    {
        ChkEvacuate.IsChecked = !ChkEvacuate.IsChecked;
        BtnReport.IsEnabled = ChkEvacuate.IsChecked && ChkCallEmergency.IsChecked;
    }
    
    private void OnChkCallEmergencyTapped(object sender, EventArgs e)
    {
        ChkCallEmergency.IsChecked = !ChkCallEmergency.IsChecked;
        BtnReport.IsEnabled = ChkEvacuate.IsChecked && ChkCallEmergency.IsChecked;
    }
    
    private void OnChkReportTapped(object sender, EventArgs e)
    {
        ChkReport.IsChecked = !ChkReport.IsChecked;
    }
}