using eGarden.Dto;
using System.Text.Json;
using System.Text;

namespace eGarden.Views;

public partial class LoginPage : ContentPage
{
    private readonly HttpClient _httpClient;
    public LoginPage()
	{
		InitializeComponent();

        string apiUrl = Constants.ApiBaseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        LoginDto loginDto = new LoginDto();
        loginDto.email = EmailEntry.Text;
        loginDto.password = PasswordEntry.Text;
        try
        {
            LoginResponseDto loginResponseDto= await AuthenticateUserAsync(loginDto);
            if (loginResponseDto.role == "ROLE_USER")
            {
                await Navigation.PushAsync(new UserPage(loginResponseDto.username, loginResponseDto.hasCard));
            }
            if (loginResponseDto.role == "ROLE_ADMIN")
            {
                await Navigation.PushAsync(new AdminPage());
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }

    public async Task<LoginResponseDto> AuthenticateUserAsync(LoginDto loginDto)
    {
        try
        {
            string jsonBody = JsonSerializer.Serialize(loginDto);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/login", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                LoginResponseDto loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseBody);
                return loginResponse;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Login failed: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}