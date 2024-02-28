using eGarden.ViewModel;
using System.Text;
using System.Text.Json;

namespace eGarden.Views;

public partial class UserProfile : ContentPage
{
    private readonly HttpClient _httpClient;
    private UserProfileViewModel _viewModel;
    private string _username;
    public UserProfile(string username)
	{
        InitializeComponent();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
        _username = username;
        _viewModel = new UserProfileViewModel();
        InitializeAsync(username);
    }
    private async void InitializeAsync(string username)
    {
        await _viewModel.FetchUserProfile(username);
        BindingContext = _viewModel;
    }

    private async void OnAddEditCardInformationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCardInformationPage(_username));
    }

    private async void OnCancelSubscriptionClicked(object sender, EventArgs e)
    {
        string message = await CancelSubscriptionAsync(_username);
        DisplayAlert("Info", message, "OK");
    }

    public async Task<string> CancelSubscriptionAsync(string username)
    {
        try
        {
            string stringUsername = JsonSerializer.Serialize(new { username });
            HttpContent content = new StringContent(stringUsername, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/cancel-subscription", content);
            if (response.IsSuccessStatusCode)
            {
                return "You have cancelled the subscription successfully.";
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return $"Canceling subscription failed: {errorMessage}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception: {ex.Message}";
        }
    }

    private async void OnViewYourRequestsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UsersRequestsPage(_username));
    }

    private async void OnAddEditWorkDetailsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWorkDetailsPage(_username));
    }
}