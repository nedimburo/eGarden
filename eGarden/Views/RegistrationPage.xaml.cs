using eGarden.Dto;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace eGarden.Views;

public partial class RegistrationPage : ContentPage
{
    private readonly HttpClient _httpClient;
    public RegistrationPage()
	{
		InitializeComponent();

        string apiUrl = Constants.ApiBaseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        RegisterDto registerDto = new RegisterDto();
        registerDto.firstName = FirstNameEntry.Text;
        registerDto.lastName = LastNameEntry.Text;
        registerDto.email = EmailEntry.Text;
        registerDto.username = UsernameEntry.Text;
        registerDto.password = PasswordEntry.Text;
        registerDto.gender = GenderPicker.SelectedItem as string;
        DateTime birthDate = BirthDatePicker.Date;
        registerDto.birthDate = new DateTime(birthDate.Year, birthDate.Month, birthDate.Day).ToString("yyyy-MM-dd");
        string message=await RegisterUserAsync(registerDto);
        DisplayAlert("Info", message, "OK");
    }

    public async Task<string> RegisterUserAsync(RegisterDto registerDto)
    {
        try
        {
            string jsonBody = JsonSerializer.Serialize(registerDto);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/register", content);
            if (response.IsSuccessStatusCode)
            {
                return "User is registered successfully!";
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return $"Registration failed: {errorMessage}";
            }
        }
        catch(Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }

}