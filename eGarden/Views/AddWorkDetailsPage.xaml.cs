using eGarden.Dto;
using System.Text;
using System.Text.Json;

namespace eGarden.Views;

public partial class AddWorkDetailsPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private string _username;
	public AddWorkDetailsPage(string username)
	{
		InitializeComponent();
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
        _username = username;
    }

    private async void OnSaveDetailsClicked(object sender, EventArgs e)
    {
        AddWorkerDto addWorkerDto=new AddWorkerDto();
        addWorkerDto.description = Description.Text;
        addWorkerDto.phoneNumber = PhoneNumber.Text;
        addWorkerDto.city = City.Text;
        addWorkerDto.country = Country.Text;
        addWorkerDto.username = _username;
        string message=await SaveWorkDetailsAsync(addWorkerDto);
        DisplayAlert("Info", message, "OK");
    }

    public async Task<string> SaveWorkDetailsAsync(AddWorkerDto addWorkerDto)
    {
        try
        {
            string jsonBody = JsonSerializer.Serialize(addWorkerDto);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/sign-up-work", content);
            if (response.IsSuccessStatusCode)
            {
                return "Work details have been registered successfully!";
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return $"Adding Work Details failed: {errorMessage}";
            }
        }
        catch(Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }
}