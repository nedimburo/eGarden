using eGarden.Dto;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace eGarden.ViewModel
{
    public class AllWorkersViewModel
    {
        public ObservableCollection<WorkerResponseDto> Workers { get; set; }
        private readonly HttpClient _httpClient;

        public AllWorkersViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.ApiBaseUrl);
            Workers = new ObservableCollection<WorkerResponseDto>();
        }

        public async Task FetchAllWorkers()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/get-all-workers");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var workersList = JsonSerializer.Deserialize<List<WorkerResponseDto>>(responseBody);
                    Workers.Clear();
                    foreach(var worker in workersList)
                    {
                        Workers.Add(worker);
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
    }
}
