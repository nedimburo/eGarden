using eGarden.Dto;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace eGarden.ViewModel
{
    public class UserProfileViewModel
    {
        public UserProfileDto UserProfile { get; set; }
        private readonly HttpClient _httpClient;

        public UserProfileViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
            UserProfile = new UserProfileDto();
        }

        public async Task FetchUserProfile(string username)
        {
            try
            {
                string stringUsername = JsonSerializer.Serialize(new { username });
                HttpContent content = new StringContent(stringUsername, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("/api/user-profile", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    UserProfile = JsonSerializer.Deserialize<UserProfileDto>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
