using eGarden.Dto;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace eGarden.ViewModel
{
    public class UsersRequestPageViewModel
    {
        public ObservableCollection<UsersRequestDto> UsersRequests { get; set; }
        private readonly HttpClient _httpClient;
        
        public UsersRequestPageViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
            UsersRequests= new ObservableCollection<UsersRequestDto>();
        }

        public async Task FetchAllUsersRequests(string username)
        {
            try
            {
                string stringUsername = JsonSerializer.Serialize(new { username });
                HttpContent content = new StringContent(stringUsername, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("/api/get-user-requests", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var userRequestList=JsonSerializer.Deserialize<List<UsersRequestDto>>(responseBody);
                    UsersRequests.Clear();
                    foreach(var request in userRequestList)
                    {
                        UsersRequests.Add(request);
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
