using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace BuggyTasks.Views;

public partial class DeviceInfoPage : ContentPage
{
    public DeviceInfoPage()
    {
        InitializeComponent();
        ShowDeviceDetails();
    }

    void ShowDeviceDetails()
    {
        try
        {
            ModelLabel.Text = $"Device Model: {DeviceInfo.Model}";
            PlatformLabel.Text = $"Platform: {DeviceInfo.Platform}";
            VersionLabel.Text = $"OS Version: {DeviceInfo.VersionString}";
            ManufacturerLabel.Text = $"Brand: {DeviceInfo.Manufacturer}";
        }
        catch (Exception ex)
        {
            ModelLabel.Text = $"Could not load device info: {ex.Message}";
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}