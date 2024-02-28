using eGarden.Dto;
using System.Text;
using System.Text.Json;

namespace eGarden.Views;

public partial class CreateRequestPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private string _username;
    private bool _hasCard;
    private RadioButton selectedMaintenanceOption;
    private RadioButton selectedDecorationOption;
    private RadioButton selectedLayoutOption;
    private string selectedMaintenanceText;
    private string selectedDecorationText;
    private string selectedLayoutText;
    private float maintenanceCost = 0;
    private float decorationCost = 0;
    private float layoutCost = 0;
    private float overallCost = 0;
    public CreateRequestPage(string username, bool hasCard)
	{
        InitializeComponent();
        optionPicker.SelectedIndexChanged += SelectedOptionIndex;
        BindingContext = this;
        _username = username;
        _hasCard = hasCard;
        string apiUrl = Constants.ApiBaseUrl;
        _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };
        if (_hasCard)
        {
            paymentContainer.IsVisible = true;
        }
        else {
            paymentContainer.IsVisible = false;
        }
        
    }

	private void SelectedOptionIndex(object sender, EventArgs e)
	{
        int selectedOption = optionPicker.SelectedIndex;
        option1Layout.IsVisible = selectedOption == 0;
        costContainer.IsVisible = selectedOption == 1;
        option2Layout.IsVisible = selectedOption == 1;
    }

    private void MaintenanceOption_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = (RadioButton)sender;

        if (e.Value)
        {
            if (selectedMaintenanceOption != null && selectedMaintenanceOption != radioButton)
            {
                selectedMaintenanceOption.IsChecked = false;
            }
            selectedMaintenanceOption = radioButton;
            float optionCost = 0;
            if (maintenanceOptionBasic.IsChecked)
            {
                selectedMaintenanceText = "Basic";
                optionCost = 25;
            }   
            else if (maintenanceOptionAdvanced.IsChecked)
            {
                selectedMaintenanceText = "Advanced";
                optionCost = 45;
            }
            else if (maintenanceOptionPro.IsChecked)
            {
                selectedMaintenanceText = "Pro";
                optionCost = 75;
            }
            maintenanceCost = optionCost;
            overallCost = maintenanceCost + decorationCost + layoutCost;
            overallCostLabel.Text = $"${overallCost}";
        }
    }

    private void DecorationOption_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = (RadioButton)sender;

        if (e.Value)
        {
            if (selectedDecorationOption != null && selectedDecorationOption != radioButton)
            {
                selectedDecorationOption.IsChecked = false;
            }
            selectedDecorationOption = radioButton;
            float optionCost = 0;
            if (decorationOptionRose.IsChecked)
            {
                selectedDecorationText = "Rose Package";
                optionCost = 120;
            }
            else if (decorationOptionSmallTrees.IsChecked)
            {
                selectedDecorationText = "Small Trees Package";
                optionCost = 450;
            }
            else if (decorationOptionEvergreen.IsChecked)
            {
                selectedDecorationText = "Evergreen Plants Package";
                optionCost = 250;
            }
            else if (decorationOptionMix.IsChecked)
            {
                selectedDecorationText = "Mix Package";
                optionCost = 850;
            } 
            decorationCost = optionCost;
            overallCost = maintenanceCost + decorationCost + layoutCost;
            overallCostLabel.Text = $"${overallCost}";
        }
    }

    private void LayoutOption_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = (RadioButton)sender;

        if (e.Value)
        {
            if (selectedLayoutOption != null && selectedLayoutOption != radioButton)
            {
                selectedLayoutOption.IsChecked = false;
            }
            selectedLayoutOption = radioButton;
            float optionCost = 0;
            if (layoutOptionStyleO.IsChecked)
            {
                selectedLayoutText = "O Style";
                optionCost = 50;
            }
            else if (layoutOptionStyleU.IsChecked)
            {
                selectedLayoutText = "U Style";
                optionCost = 40;
            }
            else if (layoutOptionSides.IsChecked)
            {
                selectedLayoutText = "Sides Style";
                optionCost = 70;
            }
            else if (layoutOptionAllAround.IsChecked)
            {
                selectedLayoutText = "All Around Style";
                optionCost = 100;
            }
            else if (layoutOptionWholeArea.IsChecked)
            {
                selectedLayoutText = "Whole Area Text";
                optionCost = 150;
            }
            layoutCost = optionCost;
            overallCost = maintenanceCost + decorationCost + layoutCost;
            overallCostLabel.Text = $"${overallCost}";
        }
    }

    private async void OnSubmitRequestClicked(object sender, EventArgs e)
    {
        if (optionPicker!=null)
        {
            RequestDto requestDto = new RequestDto();
            string allowAgency;
            string selectedOption = optionPicker.SelectedItem.ToString();
            if (selectedOption== "Allow Agency to do all the work")
            {
                allowAgency = "YES";
            }
            else
            {
                allowAgency = "NO";
            }
            string budget = budgetEntry.Text;
            string paymentMethod;
            if (_hasCard)
            {
                paymentMethod = paymentMethodPicker.SelectedItem.ToString();
            }
            else
            {
                paymentMethod = "Cash";
            }
            if (budget == null)
            {
                requestDto.plannedBudget = 0;
            }
            else
            {
                requestDto.plannedBudget = float.Parse(budget);
                overallCost = float.Parse(budget);
            }
            requestDto.allowAgency = allowAgency;
            requestDto.chosenMaintenance = selectedMaintenanceText;
            requestDto.chosenDecoration = selectedDecorationText;
            requestDto.chosenLayout = selectedLayoutText;
            requestDto.paymentMethod = paymentMethod;
            requestDto.price = overallCost;
            requestDto.city = City.Text;
            requestDto.address = Address.Text;
            requestDto.country = Country.Text;
            requestDto.username = _username;
            if (selectedOption != null)
            {
                string message = await CreateRequestAsync(requestDto);
                DisplayAlert("Info", message, "OK");
            }
            else
            {
                DisplayAlert("Error", "Please select an option.", "OK");
            }
        }
        
    }

    public async Task<string> CreateRequestAsync(RequestDto requestDto)
    {
        try
        {
            string jsonBody = JsonSerializer.Serialize(requestDto);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/api/create-request", content);
            if (response.IsSuccessStatusCode)
            {
                return "Request has been successfully made.";
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return $"Request creation failed: {errorMessage}";
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred: {ex.Message}";
        }
    }
}