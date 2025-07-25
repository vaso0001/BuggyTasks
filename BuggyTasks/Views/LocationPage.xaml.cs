using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel; // for Permissions

namespace BuggyTasks.Views;

public partial class LocationPage : ContentPage
{
    public LocationPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetLastKnownLocation();
    }

    async Task GetLastKnownLocation()
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status != PermissionStatus.Granted)
            {
                StatusLabel.Text = "Location permission not granted.";
                return;
            }

            StatusLabel.Text = "Getting last known location...";
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                StatusLabel.Text = "Last known location found";
                LatitudeLabel.Text = $"Latitude: {location.Latitude:F6}";
                LongitudeLabel.Text = $"Longitude: {location.Longitude:F6}";
                AccuracyLabel.Text = $"Accuracy: {location.Accuracy:F2}m";
            }
            else
            {
                StatusLabel.Text = "No last known location available";
                LatitudeLabel.Text = "Latitude: --";
                LongitudeLabel.Text = "Longitude: --";
                AccuracyLabel.Text = "Accuracy: --";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
        }
    }

    async void OnGetCurrentLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status != PermissionStatus.Granted)
            {
                StatusLabel.Text = "Location permission not granted.";
                return;
            }

            StatusLabel.Text = "Getting current location...";

            var request = new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(10)
            };

            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                StatusLabel.Text = "Current location found";
                LatitudeLabel.Text = $"Latitude: {location.Latitude:F6}";
                LongitudeLabel.Text = $"Longitude: {location.Longitude:F6}";
                AccuracyLabel.Text = $"Accuracy: {location.Accuracy:F2}m";
            }
            else
            {
                StatusLabel.Text = "Unable to get current location";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
        }
    }

    async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
