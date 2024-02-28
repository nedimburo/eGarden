using eGarden.Dto;
using System.Text;
using System.Text.Json;

namespace eGarden.Views;

public partial class AddSubscriptionPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private string _username;
	public AddSubscriptionPage(string username)
	{
		_username = username;
		InitializeComponent();
        string apiUrl = Constants.ApiBaseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
    }

    private async void OnSubscriptionSubmitClicked(object sender, EventArgs e)
    {
        string selectedOption = optionGroupSubscription.SelectedItem as string;

        if (!string.IsNullOrEmpty(selectedOption))
        {
            AddSubscriptionDto subscriptionDto = new AddSubscriptionDto();
            subscriptionDto.username = _username;
            subscriptionDto.subscriptionName = selectedOption;
            string message=await RegisterSubscriptionAsync(subscriptionDto);
            DisplayAlert("Info", message, "OK");
        }
        else
        {
            DisplayAlert("Error", "Please select an option.", "OK");
        }
    }

    public async Task<string> RegisterSubscriptionAsync(AddSubscriptionDto addSubscriptionDto)
    {
        try
        {
            string jsonBody = JsonSerializer.Serialize(addSubscriptionDto);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/add-subscription", content);
            if (response.IsSuccessStatusCode)
            {
                return "Subscription is registered successfully!";
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return $"Adding Subscription failed: {errorMessage}";
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }
}