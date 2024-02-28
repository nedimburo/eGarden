using eGarden.Dto;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace eGarden.ViewModel
{
    public class AdminPageViewModel
    {
        public ObservableCollection<RequestResponseDto> Requests { get; set; }
        private readonly HttpClient _httpClient;

        public AdminPageViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
            Requests = new ObservableCollection<RequestResponseDto>();
        }

        public async Task FetchAllRequests()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/get-all-requests");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var requestList=JsonSerializer.Deserialize<List<RequestResponseDto>>(responseBody);
                    Requests.Clear();
                    foreach(var request in requestList)
                    {
                        Requests.Add(request);
                    }
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

        public async Task<string> ApproveRequest(int requestId)
        {
            try
            {
                string stringId = JsonSerializer.Serialize(new { requestId });
                HttpContent content = new StringContent(stringId, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("/api/approve-request", content);
                if (response.IsSuccessStatusCode)
                {
                    return "You have successfully approved the request.";
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return $"Approving request failed: {errorMessage}";
                }
            }
            catch(Exception ex) 
            {
                return $"Exception: {ex.Message}";
            }
        }

        public async Task<string> DenyRequest(int requestId)
        {
            try
            {
                string stringId = JsonSerializer.Serialize(new { requestId });
                HttpContent content = new StringContent(stringId, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("/api/deny-request", content);
                if (response.IsSuccessStatusCode)
                {
                    return "You have successfully denied the request.";
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return $"Approving request failed: {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
