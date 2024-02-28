using eGarden.Dto;
using System.Text;
using System.Text.Json;

namespace eGarden.Views;

public partial class AddCardInformationPage : ContentPage
{
    private readonly HttpClient _httpClient;
	private string _username;
    public AddCardInformationPage(string username)
	{
		_username = username;
		InitializeComponent();

        string apiUrl = Constants.ApiBaseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
    }

    private async void OnSaveInformationClicked(object sender, EventArgs e)
    {
		CardInformationDto cardInformationDto = new CardInformationDto();
		cardInformationDto.cardNumber = CardNumber.Text;
		cardInformationDto.pinCode = PinCode.Text;
		cardInformationDto.threeDigitNumber=ThreeDigitNumber.Text;
		DateTime expirationDate = ExpirationDate.Date;
		cardInformationDto.expirationDate = new DateTime(expirationDate.Year, expirationDate.Month, expirationDate.Day).ToString("yyyy-MM-dd");
		cardInformationDto.username = _username;
		string message = await RegisterCardInformationAsync(cardInformationDto);
        DisplayAlert("Info", message, "OK");
    }

	public async Task<string> RegisterCardInformationAsync(CardInformationDto cardInformationDto)
	{
		try
		{
			string jsonBody = JsonSerializer.Serialize(cardInformationDto);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/add-card-information", content);
			if (response.IsSuccessStatusCode)
			{
                return "Card information is registered successfully!";
            }
			else
			{
                string errorMessage = await response.Content.ReadAsStringAsync();
                return $"Adding Card Information failed: {errorMessage}";
            }
        }
		catch(Exception ex) 
		{
            return $"An error occurred: {ex.Message}";
        } 
	}
}